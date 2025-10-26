using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidSlopesPatch.Common;

/// <summary>
///     Responsible for special rendering of liquid edges/slopes for the rewritten
///     liquid slope handling.
/// </summary>
/// <remarks>
///     See the related pull request:
///     https://github.com/tModLoader/tModLoader/pull/4714
/// </remarks>
public sealed class LiquidEdgeRenderer : ModSystem
{
    public static readonly BlendState MaskingBlendState = new()
    {
        ColorSourceBlend = Blend.Zero,
        AlphaSourceBlend = Blend.Zero,
        ColorDestinationBlend = Blend.InverseSourceAlpha,
        AlphaDestinationBlend = Blend.InverseSourceAlpha,
    };

    /// <summary>
    ///     Whether the special edge rendering logic is enabled.
    ///     <br />
    ///     Even if it's enabled, it will only apply if <see cref="Active" />
    ///     is <see langword="true" />.
    /// </summary>
    public static bool Enabled => ModContent.GetInstance<Config>().Enabled;

    /// <summary>
    ///     Whether the new rendering is actually active for this frame.
    /// </summary>
    public static bool Active => Enabled && Lighting.Mode is LightMode.Color or LightMode.White;

    private static List<Point> BlockWaterBehindLocations { get; } = [];

    public override void PostSetupContent()
    {
        base.PostSetupContent();

        if (RewrittenLiquidRenderer.IsUpdatedAndDoesNotNeedToApplyAnythingBecauseItsInTerrariaNow)
        {
            return;
        }

        TileID.Sets.BlocksWaterDrawingBehindSelf[TileID.SnowFallBlock] = true;
    }

    public static void Clear()
    {
        BlockWaterBehindLocations.Clear();
    }

    public static void DrawTileMask(SpriteBatch spriteBatch, RenderTarget2D tileTarget, Vector2 tileTargetOffset)
    {
        var shader = Assets.Shaders.LiquidMask.Asset;
        {
            shader.Wait();
        }

        spriteBatch.End();
        spriteBatch.Begin(SpriteSortMode.Deferred, MaskingBlendState, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shader.Value);

        spriteBatch.Draw(tileTarget, tileTargetOffset, Color.White);
        foreach (var pt in BlockWaterBehindLocations)
        {
            DrawSingleTileMask(spriteBatch, pt.X, pt.Y);
        }
    }

    private static void DrawSingleTileMask(SpriteBatch spriteBatch, int tileX, int tileY)
    {
        var tileCache = Main.tile[tileX, tileY];
        var position = new Vector2(tileX * 16, tileY * 16) + new Vector2(Main.drawToScreen ? 0 : Main.offScreenRange) - Main.screenPosition;
        var texture = Assets.Images.DefaultTileLiquidMask.Asset.Value;

        if (tileCache.Slope != SlopeType.Solid && !TileID.Sets.HasSlopeFrames[tileCache.TileType])
        {
            var slopeType = (int)tileCache.Slope;
            for (var i = 0; i < 8; i++)
            {
                var slopePosY = i * -2;
                var slopeHeight = 16 - i * 2;
                var slopeOffsetY = 16 - slopeHeight;
                int slopePosX;
                switch (slopeType)
                {
                    case 1:
                        slopePosY = 0;
                        slopePosX = i * 2;
                        slopeHeight = 14 - i * 2;
                        slopeOffsetY = 0;
                        break;

                    case 2:
                        slopePosY = 0;
                        slopePosX = 16 - i * 2 - 2;
                        slopeHeight = 14 - i * 2;
                        slopeOffsetY = 0;
                        break;

                    case 3:
                        slopePosX = i * 2;
                        break;

                    default:
                        slopePosX = 16 - i * 2 - 2;
                        break;
                }

                spriteBatch.Draw(texture, position + new Vector2(slopePosX, i * 2 + slopePosY), new Rectangle(tileCache.TileFrameX + slopePosX, tileCache.TileFrameY + slopeOffsetY, 2, slopeHeight), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
            }

            var slopeTopOrBottom = slopeType <= 2 ? 14 : 0;
            spriteBatch.Draw(texture, position + new Vector2(0f, slopeTopOrBottom), new Rectangle(tileCache.TileFrameX, tileCache.TileFrameY + slopeTopOrBottom, 16, 2), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
        }
        else
        {
            var fullTileHeight = 0;
            if (tileCache.IsHalfBlock)
            {
                fullTileHeight += 8;
            }

            spriteBatch.Draw(texture, position + new Vector2(0, fullTileHeight), new Rectangle(tileCache.TileFrameX, tileCache.TileFrameY + fullTileHeight, 16, 16 - fullTileHeight), Color.White, 0f, Vector2.Zero, 1f, 0, 0f);
        }
    }

    public static unsafe void CollectEdgeData(RewrittenLiquidRenderer.LiquidCache* pCache, Tile tileCache, int tileX, int tileY)
    {
        if (!tileCache.HasTile || tileCache.IsActuated || Main.tileSolidTop[tileCache.type] || !Main.tileSolid[tileCache.type])
        {
            return;
        }

        var tileRightCache = Main.tile[tileX + 1, tileY];
        var tileLeftCache = Main.tile[tileX - 1, tileY];
        if (tileCache.IsHalfBlock && (tileLeftCache.liquid > 160 || tileRightCache.liquid > 160) && Main.instance.waterfallManager.CheckForWaterfall(tileX, tileY))
        {
            return;
        }

        var tileUpCache = Main.tile[tileX, tileY - 1];
        var tileDownCache = Main.tile[tileX, tileY + 1];

        var liquidType = 0;

        var highLiquid = 0;
        var left = false;
        var right = false;
        var up = false;
        var down = false;
        var slope = tileCache.Slope;
        var blockType = tileCache.BlockType;

        if (tileCache.type == TileID.Grate && tileCache.LiquidAmount > 0)
        {
            down = true;
            left = true;
            right = true;
            highLiquid = tileCache.LiquidAmount;
            liquidType = tileCache.LiquidType;
        }
        else
        {
            if (tileCache.LiquidAmount > 0 && blockType != BlockType.Solid && (blockType != BlockType.HalfBlock || tileCache.liquid > 160))
            {
                if (tileCache.LiquidAmount >= highLiquid)
                {
                    highLiquid = tileCache.LiquidAmount;
                    liquidType = tileCache.LiquidType;
                }
            }

            if (tileLeftCache.LiquidAmount > 0)
            {
                left = true;

                if (tileLeftCache.LiquidAmount >= highLiquid)
                {
                    highLiquid = tileLeftCache.LiquidAmount;
                    liquidType = tileLeftCache.LiquidType;
                }
            }

            if (tileRightCache.LiquidAmount > 0)
            {
                right = true;

                if (tileRightCache.LiquidAmount >= highLiquid)
                {
                    highLiquid = tileRightCache.LiquidAmount;
                    liquidType = tileRightCache.LiquidType;
                }
            }

            if (tileUpCache.LiquidAmount > 0)
            {
                up = true;

                // Always treat directly above as most important.
                highLiquid = 255;
                liquidType = tileUpCache.LiquidType;
            }

            if (tileDownCache.LiquidAmount > 252)
            {
                if (tileDownCache.LiquidType == liquidType || !up)
                {
                    down = true;
                    liquidType = tileDownCache.LiquidType;
                }
            }

            if (!up && !down && !left && !right)
            {
                return;
            }
        }

        var tileUpLeftCache = Main.tile[tileX - 1, tileY - 1];
        var tileUpRightCache = Main.tile[tileX + 1, tileY - 1];

        var leftEmpty = !left && !WorldGen.SolidTile(tileLeftCache)
                              && !(tileLeftCache.BlockType is not BlockType.Solid && tileUpLeftCache.LiquidAmount > 0);

        var rightEmpty = !right && !WorldGen.SolidTile(tileRightCache)
                                && !(tileRightCache.BlockType is not BlockType.Solid && tileUpRightCache.LiquidAmount > 0);

        if (slope == SlopeType.SlopeUpLeft && !left && rightEmpty)
        {
            return;
        }

        if (slope == SlopeType.SlopeUpRight && !right && leftEmpty)
        {
            return;
        }

        // If on both sides, make sure liquids are close in level
        var similarHeights = !left || !right || slope != SlopeType.Solid || Math.Abs(tileLeftCache.LiquidAmount - tileRightCache.LiquidAmount) < 100;

        var noLiquidInDiagonals = slope switch
        {
            SlopeType.SlopeUpLeft or SlopeType.SlopeDownLeft => tileUpRightCache.LiquidAmount <= 0,
            SlopeType.SlopeUpRight or SlopeType.SlopeDownRight => tileUpLeftCache.LiquidAmount <= 0,
            _ => tileUpLeftCache.LiquidAmount <= 0 && tileUpRightCache.LiquidAmount <= 0,
        };

        // If air or a top slope is above itself, like half bricks next to covered full liquids
        var airAbove = !WorldGen.SolidOrSlopedTile(tileUpCache) || tileUpCache.Slope is SlopeType.SlopeUpLeft or SlopeType.SlopeUpRight;
        // See if either side has a surface via not being full or not having a tile above
        var surfaceOnSide = (left && (tileLeftCache.LiquidAmount < 250 || !WorldGen.SolidOrSlopedTile(tileUpLeftCache)))
                         || (right && (tileRightCache.LiquidAmount < 250 || !WorldGen.SolidOrSlopedTile(tileUpRightCache)));

        var isSurfaceLiquid = !up && similarHeights && noLiquidInDiagonals && (surfaceOnSide || airAbove);

        (int Width, int Height) size = (16, 16);
        var offset = Vector2.Zero;

        if (up)
        {
            if (left || right)
            {
                if (!tileCache.IsHalfBlock && !down && !WorldGen.SolidOrSlopedTile(tileDownCache))
                {
                    size = (16, 12);
                }
                else
                {
                    size = (16, 16);
                }
            }
            else if (down)
            {
                size = (16, 16);
            }
            else if (tileCache.IsHalfBlock || slope != SlopeType.Solid)
            {
                size = slope switch
                {
                    SlopeType.SlopeUpLeft or SlopeType.SlopeUpRight => (16, 2),
                    SlopeType.SlopeDownLeft or SlopeType.SlopeDownRight => (16, 12),
                    _ => (16, 16),
                };
            }
            else
            {
                size = (16, 10);
            }
        }
        else if (down && !left && !right)
        {
            offset = new Vector2(0, 12);
            size = (16, 4);
            highLiquid = 255;
            isSurfaceLiquid = false;
        }
        else
        {
            var width = down && tileDownCache.LiquidAmount > 250 ? 16 : 4;
            var depthPush = Math.Min((256 - highLiquid) / 16, 12);

            if (slope != SlopeType.Solid)
            {
                offset = new Vector2(0, depthPush);
                size = (16, 16 - depthPush);

                if (left && right)
                {
                    if (slope is SlopeType.SlopeUpLeft or SlopeType.SlopeDownLeft)
                    {
                        highLiquid = tileRightCache.LiquidAmount;
                    }
                    else if (slope is SlopeType.SlopeUpRight or SlopeType.SlopeDownRight)
                    {
                        highLiquid = tileLeftCache.LiquidAmount;
                    }

                    depthPush = (256 - highLiquid) / 16;
                    offset = new Vector2(0, depthPush);
                    size = (16, 16 - depthPush);
                }
                else if (left)
                {
                    if (slope is SlopeType.SlopeDownLeft or SlopeType.SlopeUpLeft)
                    {
                        offset = new Vector2(0, depthPush);
                        size = (2, 16 - depthPush);
                    }

                    if (slope is SlopeType.SlopeDownRight or SlopeType.SlopeUpRight)
                    {
                        offset = new Vector2(0, depthPush);
                        size = (14, 16 - depthPush);
                    }
                }
                else if (right)
                {
                    if (slope is SlopeType.SlopeDownLeft or SlopeType.SlopeUpLeft)
                    {
                        offset = new Vector2(2, depthPush);
                        size = (14, 16 - depthPush);
                    }

                    if (slope is SlopeType.SlopeDownRight or SlopeType.SlopeUpRight)
                    {
                        offset = new Vector2(14, depthPush);
                        size = (2, 16 - depthPush);
                    }
                }
            }
            else if ((left && right) || tileCache.IsHalfBlock)
            {
                if (left && right && highLiquid < 255)
                {
                    highLiquid = (tileLeftCache.LiquidAmount + tileRightCache.LiquidAmount) / 2;
                }

                if (!tileCache.IsHalfBlock)
                {
                    depthPush = Math.Min((256 - highLiquid) / 16, 12);
                }

                offset = new Vector2(0, depthPush);
                size = (16, 16 - depthPush);
            }
            else if (left)
            {
                offset = new Vector2(0, depthPush);
                size = (width, 16 - depthPush);
                if (rightEmpty && down)
                {
                    size.Width -= 4;
                }
            }
            else if (right)
            {
                offset = new Vector2(16 - width, depthPush);
                size = (width, 16 - depthPush);
                if (leftEmpty && down)
                {
                    offset.X += 4;
                    size.Width -= 4;
                }
            }
        }

        pCache->EdgeData = new RewrittenLiquidRenderer.LiquidEdgeData
        {
            LiquidOffset = offset,
            SourceRectangle = new Rectangle(16, isSurfaceLiquid ? 0 : 64, size.Width, size.Height),
        };

        if (TileID.Sets.BlocksWaterDrawingBehindSelf[tileCache.type])
        {
            BlockWaterBehindLocations.Add(new Point(tileX, tileY));
        }

        if (blockType is BlockType.HalfBlock)
        {
            if (!pCache->IsHalfBrick)
            {
                pCache->LiquidLevel = highLiquid / 255f;
                pCache->Type = (byte)liquidType;
            }
        }
        else if (blockType is not BlockType.Solid)
        {
            Debug.Assert(pCache->IsSolid);

            pCache->LiquidLevel = highLiquid / 255f;
            pCache->Type = (byte)liquidType;
        }
        else
        {
            Debug.Assert(pCache->IsSolid);

            pCache->LiquidLevel = highLiquid / 255f;
            pCache->Type = (byte)liquidType;
        }
    }
}

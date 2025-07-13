using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

using Daybreak.Common.Features.Tiles;
using Daybreak.Core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoMod.Cil;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Liquids;

/// <summary>
///     Handles the rendering of liquids.
/// </summary>
public sealed class LiquidRendering : ModSystem
{
    /// <summary>
    ///     Invoked before liquid is rendered.
    /// </summary>
    public static event Action? PreRenderLiquid;

    /// <summary>
    ///     Invoked after liquid is rendered.
    /// </summary>
    public static event Action? PostRenderLiquid;

    /// <summary>
    ///     Whether the liquid slope rendering fix is enabled.
    ///     <br />
    ///     Even if it's enabled, slopes may not be fixed due to other
    ///     conditions.  See <see cref="ShouldFixSlopes"/>.
    /// </summary>
    public static bool SlopeFixEnabled { get; set; } = true;

    /// <summary>
    ///     Whether liquid slope rendering should be fixed.  May be manually
    ///     disabled by mods by setting <see cref="SlopeFixEnabled"/>.
    ///     <br />
    ///     Determines whether, for a frame, the slope rendering fix should be
    ///     applied.  Certain rendering conditions or config options may prevent
    ///     the fix even if <see cref="SlopeFixEnabled"/> is
    ///     <see langword="true"/>.
    /// </summary>
    public static bool ShouldFixSlopes => SlopeFixEnabled && !Main.drawToScreen;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        Main.OnRenderTargetsInitialized += InitTargets;
        Main.OnRenderTargetsReleased += ReleaseTargets;

        On_Main.DoDraw_UpdateCameraPosition += DoDraw_UpdateCameraPosition_PrepareTargets;

        IL_Main.DoDraw += DoDraw_AddEventsToDraw;
        On_TileDrawing.DrawTile_LiquidBehindTile += DrawTile_LiquidBehindTile_StopDrawingLiquidsBehindTiles;
        On_Main.DrawLiquid += DrawLiquid_DrawLiquid;
        On_Main.RenderWater += RenderWater_RenderWater;

        if (ModLoader.TryGetMod("SpiritReforged", out var spiritMod))
        {
            SpiritCompat(spiritMod);
        }
    }

    /// <inheritdoc />
    public override void Unload()
    {
        base.Unload();

        Main.OnRenderTargetsInitialized -= InitTargets;
        Main.OnRenderTargetsReleased -= ReleaseTargets;
    }

    private static RenderTarget2D? liquidTargetNoCut;
    private static RenderTarget2D? liquidTarget;
    private static RenderTarget2D? liquidMaskTarget;

    private static bool ready;

    private static void InitTargets(int width, int height)
    {
        width += Main.offScreenRange * 2;
        height += Main.offScreenRange * 2;

        try
        {
            var gd = Main.instance.GraphicsDevice;
            var format = gd.PresentationParameters.BackBufferFormat;

            liquidTarget = new RenderTarget2D(gd, width, height, mipMap: false, format, DepthFormat.None);
            liquidTargetNoCut = new RenderTarget2D(gd, width, height, mipMap: false, format, DepthFormat.None);
            liquidMaskTarget = new RenderTarget2D(gd, width, height, mipMap: false, format, DepthFormat.None);

            ready = true;
        }
        catch (Exception e)
        {
            ModContent.GetInstance<ModImpl>().Logger.Error("Error creating liquid render targets", e);

            Lighting.Mode = LightMode.Retro;
            ready = false;
        }
    }

    private static void ReleaseTargets()
    {
        ready = false;

        try
        {
            liquidTargetNoCut?.Dispose();
            liquidTarget?.Dispose();
            liquidMaskTarget?.Dispose();
        }
        catch (Exception e)
        {
            ModContent.GetInstance<ModImpl>().Logger.Error("Error disposing liquid render targets", e);
            SlopeFixEnabled = false;
        }

        liquidTargetNoCut = null;
        liquidTarget = null;
        liquidMaskTarget = null;
    }

    private static readonly HashSet<Point> edge_tiles = [];
    private static readonly HashSet<Point> water_plants = [];

    private static void DoDraw_UpdateCameraPosition_PrepareTargets(On_Main.orig_DoDraw_UpdateCameraPosition orig)
    {
        orig();

        if (Main.renderCount != 1 || !ready)
        {
            return;
        }

        // var unscaledPosition = Main.Camera.UnscaledPosition;
        var offScreen = new Vector2(Main.drawToScreen ? 0 : Main.offScreenRange);

        GetCuttingTiles();

        Main.instance.GraphicsDevice.SetRenderTarget(liquidMaskTarget);
        Main.instance.GraphicsDevice.Clear(Color.Transparent);
        Main.tileBatch.Begin();

        foreach (var point in edge_tiles)
        {
            DrawSingleTile(point.X, point.Y, Main.screenPosition - offScreen);
        }

        Main.tileBatch.End();
        Main.spriteBatch.Begin();

        foreach (var point in water_plants)
        {
            Main.DrawTileInWater(-Main.screenPosition - offScreen, point.X, point.Y);
        }

        Main.spriteBatch.End();
        Main.instance.GraphicsDevice.SetRenderTarget(liquidTargetNoCut);
        Main.instance.GraphicsDevice.Clear(Color.Transparent);
        Main.spriteBatch.Begin();

        Main.spriteBatch.Draw(liquidTarget, Main.sceneWaterPos - Main.screenPosition + offScreen, Color.White);

        Main.spriteBatch.End();
        Main.instance.GraphicsDevice.SetRenderTarget(Main.waterTarget);
        Main.instance.GraphicsDevice.Clear(Color.Transparent);
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

        AssetReferences.Assets.Shaders.ImageMask.Asset.Wait();
        var mask = AssetReferences.Assets.Shaders.ImageMask.CreateMaskShader();
        mask.Parameters.uMaskTexture = liquidMaskTarget;
        mask.Parameters.invert = true;
        mask.Apply();

        Main.spriteBatch.Draw(liquidTargetNoCut, Vector2.Zero, Color.White);

        Main.spriteBatch.End();
        Main.instance.GraphicsDevice.SetRenderTarget(null);
        Main.instance.GraphicsDevice.Clear(Color.Transparent);

        return;

        static void GetCuttingTiles()
        {
            var unscaledPosition = Main.Camera.UnscaledPosition;
            var screenOff = new Vector2(Main.drawToScreen ? 0 : Main.offScreenRange);
            Main.instance.TilesRenderer.GetScreenDrawArea(unscaledPosition, screenOff, out var left, out var right, out var top, out var bottom);

            water_plants.Clear();
            edge_tiles.Clear();

            for (var i = left; i < right; i++)
            {
                for (var j = top; j < bottom; j++)
                {
                    if (!WorldGen.InWorld(i, j))
                    {
                        continue;
                    }

                    if (Main.tile[i, j].HasTile && (Main.tile[i, j].TileType == TileID.LilyPad || TileLoader.GetTile(Main.tile[i, j].TileType) is ILilyPad { VanillaDrawTileInWater: true }))
                    {
                        water_plants.Add(new Point(i, j));
                    }

                    if (!WorldGen.SolidOrSlopedTile(i, j))
                    {
                        continue;
                    }

                    var foundValid = false;

                    if (WorldGen.InWorld(i, j + 1))
                    {
                        if (Main.tile[i, j + 1].LiquidAmount >= 255)
                        {
                            foundValid = true;
                        }
                    }
                    if (WorldGen.InWorld(i, j - 1))
                    {
                        if (Main.tile[i, j - 1].LiquidAmount > 0)
                        {
                            foundValid = true;
                        }
                    }
                    if (WorldGen.InWorld(i - 1, j))
                    {
                        if (Main.tile[i - 1, j].LiquidAmount > 0)
                        {
                            foundValid = true;
                        }
                    }
                    if (WorldGen.InWorld(i + 1, j))
                    {
                        if (Main.tile[i + 1, j].LiquidAmount > 0)
                        {
                            foundValid = true;
                        }
                    }
                    if (WorldGen.InWorld(i - 1, j - 1))
                    {
                        if (Main.tile[i - 1, j - 1].LiquidAmount > 0)
                        {
                            foundValid = true;
                        }
                    }
                    if (WorldGen.InWorld(i + 1, j - 1))
                    {
                        if (Main.tile[i + 1, j - 1].LiquidAmount > 0)
                        {
                            foundValid = true;
                        }
                    }

                    if (foundValid)
                    {
                        edge_tiles.Add(new Point(i, j));
                    }
                }
            }
        }
    }

    private static void DoDraw_AddEventsToDraw(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(x => x.MatchLdsfld<Main>(nameof(Main.waterTarget)));
        c.GotoPrev(MoveType.Before, x => x.MatchLdsfld<Main>(nameof(Main.spriteBatch)));
        c.EmitDelegate(() =>
            {
                PreRenderLiquid?.Invoke();
            }
        );

        c.GotoNext(MoveType.After, x => x.MatchCallvirt<SpriteBatch>(nameof(SpriteBatch.Draw)));
        c.EmitDelegate(() =>
            {
                PostRenderLiquid?.Invoke();
            }
        );
    }

    private static bool drawTileLiquid;

    private static void DrawTile_LiquidBehindTile_StopDrawingLiquidsBehindTiles(On_TileDrawing.orig_DrawTile_LiquidBehindTile orig, TileDrawing self, bool solidLayer, bool inFrontOfPlayers, int waterStyleOverride, Vector2 screenPosition, Vector2 screenOffset, int tileX, int tileY, Tile tileCache)
    {
        if (!ShouldFixSlopes)
        {
            orig(self, solidLayer, inFrontOfPlayers, waterStyleOverride, screenPosition, screenOffset, tileX, tileY, tileCache);
            return;
        }

        if ((IsInDrawBlack(tileX, tileY, tileCache) || (drawTileLiquid && !solidLayer)) && tileY < Main.worldSurface && tileCache.WallType == WallID.None)
        {
            orig(self, solidLayer, inFrontOfPlayers, waterStyleOverride, screenPosition, screenOffset, tileX, tileY, tileCache);
        }

        return;

        // TODO: Compatibility with mods that modify the threshold?!
        static bool IsInDrawBlack(int tileX, int tileY, Tile tileCache)
        {
            var totalColor = (Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3;
            var color = (float)(totalColor * 0.4) / 255f;
            var underworld = tileY >= Main.UnderworldLayer;
            if (underworld)
            {
                color = 0.2f;
            }

            var brightness = Lighting.Brightness(tileX, tileY);
            brightness = (float)Math.Floor(brightness * 255f) / 255f;
            var liquidAmount = tileCache.LiquidAmount;
            return brightness <= color && ((!underworld && liquidAmount < 250) || WorldGen.SolidTile(tileCache) || (liquidAmount >= 200 && brightness == 0f));
        }
    }

    private static void DrawLiquid_DrawLiquid(On_Main.orig_DrawLiquid orig, Main self, bool bg, int waterStyle, float alpha, bool drawSinglePassLiquids)
    {
        if (!ShouldFixSlopes)
        {
            orig(self, bg, waterStyle, alpha, drawSinglePassLiquids);
            return;
        }

        if (!Lighting.NotRetro)
        {
            Main.instance.oldDrawWater(bg, waterStyle, alpha);
            return;
        }

        drawTileLiquid = false;

        var stopwatch = Stopwatch.StartNew();
        var drawOffset = (Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange)) - Main.screenPosition;

        //Main.instance.TilesRenderer.DrawLiquidBehindTiles();
        DrawLiquidOverTiles(waterStyle, alpha, bg);

        LiquidRenderer.Instance.DrawNormalLiquids(Main.spriteBatch, drawOffset, waterStyle, alpha, bg);

        if (drawSinglePassLiquids)
        {
            LiquidRenderer.Instance.DrawShimmer(Main.spriteBatch, drawOffset, bg);
        }

        if (!bg)
        {
            TimeLogger.DrawTime(4, stopwatch.Elapsed.TotalMilliseconds);
        }

        drawTileLiquid = false;

        return;

        static void DrawLiquidOverTiles(int waterStyle, float alpha, bool bg = false)
        {
            var unscaledPosition = Main.Camera.UnscaledPosition;
            var screenOff = new Vector2(Main.drawToScreen ? 0 : Main.offScreenRange);

            Main.instance.TilesRenderer.GetScreenDrawArea(unscaledPosition, screenOff + (Main.Camera.UnscaledPosition - Main.Camera.ScaledPosition), out var left, out var right, out var top, out var bottom);

            for (var j = top; j < bottom; j++)
            for (var i = left; i < right; i++)
            {
                if (!WorldGen.SolidOrSlopedTile(i, j))
                {
                    continue;
                }

                var liquidType = -1;
                var liquidAmount = 0;
                var onRight = false;
                var onLeft = false;
                var onTop = false;
                var onBottom = false;
                var liquidPos = new Vector2(i * 16, j * 16);
                var liquidFrame = new Rectangle(0, 4, 16, 16);

                if (Main.tile[i, j + 1].LiquidAmount >= 240) // basically guarantees a full liquid block
                {
                    liquidAmount = 255;
                    liquidType = Main.tile[i, j + 1].LiquidType;
                    onBottom = true;
                }

                if (Main.tile[i, j - 1].LiquidAmount > 0) // can't really determine how much liquid to have
                {
                    liquidAmount = Math.Max(liquidAmount, Main.tile[i, j].LiquidAmount);
                    liquidType = Main.tile[i, j - 1].LiquidType;
                    onTop = true;
                }

                if (Main.tile[i - 1, j].LiquidAmount > 0) // copy from side
                {
                    liquidAmount = Main.tile[i - 1, j].LiquidAmount < 240 ? Main.tile[i - 1, j].LiquidAmount : 255;
                    liquidType = Main.tile[i - 1, j].LiquidType;
                    onLeft = true;
                }

                if (Main.tile[i + 1, j].LiquidAmount > 0) // copy from side
                {
                    liquidAmount = Main.tile[i + 1, j].LiquidAmount < 240 ? Main.tile[i + 1, j].LiquidAmount : 255;
                    liquidType = Main.tile[i + 1, j].LiquidType;
                    onRight = true;
                }

                if (!onLeft && !onRight && !onTop && !onBottom)
                {
                    continue;
                }

                if (Main.tile[i, j - 1].Slope != 0)
                {
                    liquidFrame.Height -= 4;
                    liquidPos.Y += 4;
                }

                if (Main.tile[i, j].IsHalfBlock)
                {
                    if (!onTop && (onLeft || onRight))
                    {
                        liquidFrame.Height = (int)(liquidAmount / 255f * 16f);
                        // ReSharper disable once PossibleLossOfFraction
                        liquidPos.Y += 16 - liquidAmount / 16;
                    }
                    else
                    {
                        liquidFrame.Height = 8;
                        liquidPos.Y += 8;
                    }
                }
                else
                {
                    if (Main.tile[i, j].Slope == 0)
                    {
                        if (onTop && !(onBottom || onLeft || onRight))
                        {
                            liquidFrame.Height = 8;
                        }

                        if (onBottom && !(onTop || onLeft || onRight))
                        {
                            liquidFrame.Height = 8;
                            liquidPos.Y += 8;
                        }

                        if (onLeft && !(onTop || onBottom || onRight))
                        {
                            liquidFrame.Width = 8;
                        }

                        if (onRight && !(onTop || onBottom || onLeft))
                        {
                            liquidFrame.Width = 8;
                            liquidPos.X += 8;
                        }
                    }
                    else if (!onBottom)
                    {
                        liquidFrame.Height += 4;
                    }

                    if ((onLeft || onRight) && !onTop)
                    {
                        liquidFrame.Height += (int)(liquidAmount / 255f * 16f) - 16;
                        liquidPos.Y += 16 - (int)(liquidAmount / 255f * 16f);
                    }
                }

                var realStyle = liquidType switch
                {
                    LiquidID.Water => waterStyle,
                    LiquidID.Lava => WaterStyleID.Lava,
                    LiquidID.Honey => WaterStyleID.Honey,
                    LiquidID.Shimmer => 14,
                    _ => throw new NotImplementedException(),
                };

                DrawLiquidTile(i, j, liquidType, realStyle, liquidPos - Main.screenPosition + new Vector2(Main.drawToScreen ? 0 : Main.offScreenRange), liquidFrame, alpha, bg);
            }
        }

        static void DrawLiquidTile(int i, int j, int liquidType, int waterStyle, Vector2 position, Rectangle frame, float alpha, bool bg)
        {
            SetIsLiquid(true);
            Lighting.GetCornerColors(i, j, out var colors);
            SetIsLiquid(false);

            var liquidOpacity = LiquidRenderer.DEFAULT_OPACITY[liquidType];

            if (liquidType == LiquidID.Shimmer)
            {
                LiquidRenderer.SetShimmerVertexColors(ref colors, bg ? 1f : alpha * liquidOpacity, i, j);
            }
            else
            {
                if (!bg)
                {
                    colors.TopLeftColor *= liquidOpacity;
                    colors.TopRightColor *= liquidOpacity;
                    colors.BottomLeftColor *= liquidOpacity;
                    colors.BottomRightColor *= liquidOpacity;

                    if (Main.tile[i, j].IsHalfBlock && Main.tile[i, j - 1].LiquidAmount > 0)
                    {
                        colors.TopLeftColor = colors.TopLeftColor.MultiplyRGBA(new Color(215, 215, 215));
                        colors.TopRightColor = colors.TopRightColor.MultiplyRGBA(new Color(215, 215, 215));
                        colors.BottomLeftColor = colors.BottomLeftColor.MultiplyRGBA(new Color(215, 215, 215));
                        colors.BottomRightColor = colors.BottomRightColor.MultiplyRGBA(new Color(215, 215, 215));
                    }
                }
            }

            try
            {
                disableSpiritColors = true;
                Main.instance.TilesRenderer.DrawPartialLiquid(bg, Main.tile[i, j], ref position, ref frame, waterStyle, ref colors);
            }
            finally
            {
                disableSpiritColors = false;
            }
        }
    }

    private static void RenderWater_RenderWater(On_Main.orig_RenderWater orig, Main self)
    {
        if (!ShouldFixSlopes)
        {
            orig(self);
            return;
        }

        self.GraphicsDevice.SetRenderTarget(liquidTarget);
        self.GraphicsDevice.Clear(Color.Transparent);
        Main.spriteBatch.Begin();

        try
        {
            self.DrawWaters();
        }
        catch (Exception)
        {
            //
        }

        TimeLogger.DetailedDrawReset();
        Main.spriteBatch.End();
        TimeLogger.DetailedDrawTime(31);

        self.GraphicsDevice.SetRenderTarget(null);
    }

    private static void DrawSingleTile(int i, int j, Vector2 offset)
    {
        var tile = Main.tile[i, j];
        {
            Main.instance.LoadTiles(tile.TileType);
        }

        var tileTexture = TextureAssets.Tile[tile.TileType].Value;
        var tileFrame = new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16);
        var tilePos = new Vector2(i * 16f, j * 16f);
        var color = new VertexColors(Color.White);

        if ((tile.Slope == SlopeType.Solid || TileID.Sets.HasSlopeFrames[tile.TileType]) && !tile.IsHalfBlock)
        {
            if (!TileID.Sets.IgnoresNearbyHalfbricksWhenDrawn[tile.TileType] && (Main.tile[i - 1, j].IsHalfBlock || Main.tile[i + 1, j].IsHalfBlock))
            {
                var frameOff = 4;
                if (TileID.Sets.AllBlocksWithSmoothBordersToResolveHalfBlockIssue[tile.TileType])
                {
                    frameOff = 2;
                }

                if (Main.tile[i - 1, j].IsHalfBlock)
                {
                    Main.tileBatch.Draw(tileTexture, tilePos + new Vector2(0f, 8f) - offset, new Rectangle(tile.TileFrameX, tile.TileFrameY + 8, 16, 8), color, Vector2.Zero, 1f, 0);
                    Main.tileBatch.Draw(tileTexture, tilePos + new Vector2(frameOff, 0f) - offset, new Rectangle(tile.TileFrameX + frameOff, tile.TileFrameY, 16 - frameOff, 16), color, Vector2.Zero, 1f, 0);
                    Main.tileBatch.Draw(tileTexture, tilePos - offset, new Rectangle(144, 0, frameOff, 8), color, Vector2.Zero, 1f, 0);

                    if (frameOff == 2)
                    {
                        Main.tileBatch.Draw(tileTexture, tilePos - offset, new Rectangle(148, 0, 2, 2), color, Vector2.Zero, 1f, 0);
                    }
                }
                else if (Main.tile[i + 1, j].IsHalfBlock)
                {
                    Main.tileBatch.Draw(tileTexture, tilePos + new Vector2(0f, 8f) - offset, new Rectangle(tile.TileFrameX, tile.TileFrameY + 8, 16, 8), color, Vector2.Zero, 1f, 0);
                    Main.tileBatch.Draw(tileTexture, tilePos - offset, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16 - frameOff, 16), color, Vector2.Zero, 1f, 0);
                    Main.tileBatch.Draw(tileTexture, tilePos + new Vector2(16 - frameOff, 0f) - offset, new Rectangle(144 + (16 - frameOff), 0, frameOff, 8), color, Vector2.Zero, 1f, 0);

                    if (frameOff == 2)
                    {
                        Main.tileBatch.Draw(tileTexture, tilePos + new Vector2(14f, 0f) - offset, new Rectangle(156, 0, 2, 2), color, Vector2.Zero, 1f, 0);
                    }
                }
            }
            else
            {
                Main.tileBatch.Draw(tileTexture, tilePos - offset, tileFrame, color, Vector2.Zero, 1f, 0);
            }
        }
        else if (tile.IsHalfBlock)
        {
            tilePos.Y += 8;
            tileFrame.Height -= 8;
            Main.tileBatch.Draw(tileTexture, tilePos - offset, tileFrame, color, Vector2.Zero, 1f, 0);
        }
        else
        {
            for (var iSlope = 0; iSlope < 8; iSlope++)
            {
                var num3 = iSlope * -2;
                var num4 = 16 - iSlope * 2;
                var num5 = 16 - num4;
                int num6;
                switch ((int)tile.Slope)
                {
                    case 1:
                        num3 = 0;
                        num6 = iSlope * 2;
                        num4 = 14 - iSlope * 2;
                        num5 = 0;
                        break;

                    case 2:
                        num3 = 0;
                        num6 = 16 - iSlope * 2 - 2;
                        num4 = 14 - iSlope * 2;
                        num5 = 0;
                        break;

                    case 3:
                        num6 = iSlope * 2;
                        break;

                    default:
                        num6 = 16 - iSlope * 2 - 2;
                        break;
                }
                Main.tileBatch.Draw(tileTexture, tilePos + new Vector2(num6, iSlope * 2 + num3) - offset, new Rectangle(tile.TileFrameX + num6, tile.TileFrameY + num5, 2, num4), color, Vector2.Zero, 1f, 0);
            }

            var bottomOff = (int)tile.Slope <= 2 ? 14 : 0;
            Main.tileBatch.Draw(tileTexture, tilePos + new Vector2(0, bottomOff) - offset, new Rectangle(tile.TileFrameX, tile.TileFrameY + bottomOff, 16, 2), color, Vector2.Zero, 1f, 0);
        }
    }

    private static bool disableSpiritColors;
    private static FieldInfo? isLiquidField;

    private static void SpiritCompat(Mod mod)
    {
        if (mod.Code.GetType("SpiritReforged.Common.Visuals.WaterAlpha") is not { } waterAlphaType)
        {
            return;
        }

        if (waterAlphaType.GetMethod("ModifyColors", BindingFlags.NonPublic | BindingFlags.Static) is not { } modifyColors)
        {
            return;
        }

        MonoModHooks.Add(
            modifyColors,
            ModifyColors_DontModifyColorsWhenDisabled
        );

        isLiquidField = waterAlphaType.GetField("IsLiquid", BindingFlags.NonPublic | BindingFlags.Static);
    }

    private delegate void ModifyColorsOrig(int x, int y, ref VertexColors colors, bool isPartial);

    private static void ModifyColors_DontModifyColorsWhenDisabled(ModifyColorsOrig orig, int x, int y, ref VertexColors colors, bool isPartial)
    {
        if (!disableSpiritColors)
        {
            orig(x, y, ref colors, isPartial);
        }
    }

    private static void SetIsLiquid(bool value)
    {
        isLiquidField?.SetValue(null, value);
    }
}
using System;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Mono.Cecil;
using Mono.Cecil.Cil;

using MonoMod.Cil;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Liquid;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidSlopesPatch.Common;

internal sealed class MiscHooks : ModSystem
{
    public override void Load()
    {
        base.Load();

        On_Main.DrawWaters += DrawWaters;
        IL_Main.DrawLiquid += DrawLiquid;

        On_Main.DrawBlack += DrawBlack;

        IL_TileDrawing.DrawPartialLiquid += DrawPartialLiquid;
        IL_TileDrawing.Draw += Draw;
    }

    private static void DrawBlack(On_Main.orig_DrawBlack orig, Main self, bool force)
    {
        if (Main.shimmerAlpha == 1f)
            return;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Vector2 vector = (Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange, Main.offScreenRange));
        int num = (Main.tileColor.R + Main.tileColor.G + Main.tileColor.B) / 3;
        float num2 = (float)((double)num * 0.4) / 255f;
        if (Lighting.Mode == LightMode.Retro)
        {
            num2 = (float)(Main.tileColor.R - 55) / 255f;
            if (num2 < 0f)
                num2 = 0f;
        }
        else if (Lighting.Mode == LightMode.Trippy)
        {
            num2 = (float)(num - 55) / 255f;
            if (num2 < 0f)
                num2 = 0f;
        }

        Microsoft.Xna.Framework.Point screenOverdrawOffset = Main.GetScreenOverdrawOffset();
        Microsoft.Xna.Framework.Point point = new Microsoft.Xna.Framework.Point(-Main.offScreenRange / 16 + screenOverdrawOffset.X, -Main.offScreenRange / 16 + screenOverdrawOffset.Y);
        int num3 = (int)((Main.screenPosition.X - vector.X) / 16f - 1f) + point.X;
        int num4 = (int)((Main.screenPosition.X + (float)Main.screenWidth + vector.X) / 16f) + 2 - point.X;
        int num5 = (int)((Main.screenPosition.Y - vector.Y) / 16f - 1f) + point.Y;
        int num6 = (int)((Main.screenPosition.Y + (float)Main.screenHeight + vector.Y) / 16f) + 5 - point.Y;
        if (num3 < 0)
            num3 = point.X;

        if (num4 > Main.maxTilesX)
            num4 = Main.maxTilesX - point.X;

        if (num5 < 0)
            num5 = point.Y;

        if (num6 > Main.maxTilesY)
            num6 = Main.maxTilesY - point.Y;

        if (!force)
        {
            if (num5 < Main.maxTilesY / 2)
            {
                num6 = Math.Min(num6, (int)Main.worldSurface + 1);
                num5 = Math.Min(num5, (int)Main.worldSurface + 1);
            }
            else
            {
                num6 = Math.Max(num6, Main.UnderworldLayer);
                num5 = Math.Max(num5, Main.UnderworldLayer);
            }
        }

        bool liquidSlopeFix = LiquidEdgeRenderer.Active;
        bool flag = Main.ShouldShowInvisibleWalls();
        for (int i = num5; i < num6; i++)
        {
            bool flag2 = i >= Main.UnderworldLayer;
            if (flag2)
                num2 = 0.2f;

            for (int j = num3; j < num4; j++)
            {
                int num7 = j;
                for (; j < num4; j++)
                {
                    if (!WorldGen.InWorld(j, i))
                        return;

                    if (Main.tile[j, i] == null)
                        Main.tile[j, i] = new Tile();

                    Tile tile = Main.tile[j, i];
                    float num8 = Lighting.Brightness(j, i);
                    num8 = (float)Math.Floor(num8 * 255f) / 255f;

                    // TML(liquid-slopes): Avoid drawing over liquid edges.
                    if (liquidSlopeFix && LiquidRenderer.Instance.HasFullWater(j, i) && ((tile.Slope != SlopeType.Solid || tile.IsHalfBlock) && num8 >= (5f / 255f) || num8 > (5f / 255f)))
                        break;

                    byte b = tile.liquid;
                    bool num9 = num8 <= num2 && ((!flag2 && b < 250) || WorldGen.SolidTile(tile) || (b >= 200 && num8 == 0f));
                    bool flag3 = tile.active() && Main.tileBlockLight[tile.type] && (!tile.invisibleBlock() || flag);
                    bool flag4 = !WallID.Sets.Transparent[tile.wall] && (!tile.invisibleWall() || flag);
                    if (!num9 || (!flag4 && !flag3) || (!Main.drawToScreen && LiquidRenderer.Instance.HasFullWater(j, i) && tile.wall == 0 && !tile.halfBrick() && !((double)i <= Main.worldSurface)))
                        break;
                }

                if (j - num7 > 0)
                    Main.spriteBatch.Draw(TextureAssets.BlackTile.Value, new Vector2(num7 << 4, i << 4) - Main.screenPosition + vector, new Microsoft.Xna.Framework.Rectangle(0, 0, j - num7 << 4, 16), Microsoft.Xna.Framework.Color.Black);
            }
        }

        TimeLogger.DrawTime(5, stopwatch.Elapsed.TotalMilliseconds);
    }

    private static void DrawWaters(On_Main.orig_DrawWaters orig, Main self, bool isBackground)
    {
        orig(self, isBackground);

        if (LiquidEdgeRenderer.Active)
        {
            Main.spriteBatch.End();

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, LiquidEdgeRenderer.MaskingBlendState, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, Assets.Shaders.LiquidMask.Asset.Value);

            Main.spriteBatch.Draw(self.tileTarget, Main.sceneTilePos - Main.screenPosition + new Vector2(Main.drawToScreen ? 0 : Main.offScreenRange), Color.White);

            foreach (Point edge in LiquidEdgeRenderer.Edges)
            {
                int tileType = Main.tile[edge.X, edge.Y].TileType;
                if (TileID.Sets.BlocksWaterDrawingBehindSelf[tileType])
                {
                    LiquidEdgeRenderer.DrawSingleTileMask(Main.spriteBatch, edge.X, edge.Y);
                }
            }
        }
    }

    private static void DrawLiquid(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(x => x.MatchLdarg1());
        c.GotoNext(MoveType.After, x => x.MatchLdarg1());
        c.EmitDelegate((bool bg) => bg && !LiquidEdgeRenderer.Active);
    }

    private static void DrawPartialLiquid(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(x => x.MatchLdloc0());
        c.GotoPrev(MoveType.After, x => x.MatchLdloc1());
        c.EmitDelegate((bool flag) => flag || LiquidEdgeRenderer.Active);
    }

    private static void Draw(ILContext il)
    {
        var c = new ILCursor(il);

        var liquidSlopeFixVar = AddVariable(il.Body, il.Import(typeof(bool)));

        c.EmitDelegate(() => LiquidEdgeRenderer.Active);
        c.EmitStloc(liquidSlopeFixVar);

        c.GotoNext(x => x.MatchLdarg1());
        c.GotoNext(x => x.MatchLdarg1());
        c.GotoNext(x => x.MatchLdarg1());
        c.GotoNext(MoveType.After, x => x.MatchLdarg1());

        c.EmitLdloc(liquidSlopeFixVar);
        c.EmitDelegate((bool solidLayer, bool liquidSlopeFix) => solidLayer && !liquidSlopeFix);
    }

    private static VariableDefinition AddVariable(MethodBody @this, TypeReference type)
    {
        var variable = new VariableDefinition(type);
        {
            @this.Variables.Add(variable);
        }

        return variable;
    }
}
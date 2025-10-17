using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Liquid;
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

        IL_Main.DrawBlack += DrawBlack;

        IL_TileDrawing.DrawPartialLiquid += DrawPartialLiquid;
        IL_TileDrawing.Draw += Draw;
    }

    private static void DrawWaters(On_Main.orig_DrawWaters orig, Main self, bool isBackground)
    {
        orig(self, isBackground);

        if (LiquidEdgeRenderer.Active)
        {
            Main.spriteBatch.End();

            Main.spriteBatch.Begin(SpriteSortMode.Deferred, LiquidEdgeRenderer.MaskingBlendState, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, Assets.Shaders.LiquidMask.Asset.Value);

            Main.spriteBatch.Draw(self.tileTarget, Main.sceneTilePos - Main.screenPosition + new Vector2(Main.drawToScreen ? 0 : Main.offScreenRange), Color.White);

            foreach (var edge in LiquidEdgeRenderer.Edges)
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

    private static void DrawBlack(ILContext il)
    {
        var c = new ILCursor(il);

        var liquidSlopeFixVar = AddVariable(il.Body, il.Import(typeof(bool)));

        c.EmitDelegate(() => LiquidEdgeRenderer.Active);
        c.EmitStloc(liquidSlopeFixVar);

        var iLoc = -1;
        var jLoc = -1;
        var num8Loc = -1;
        c.GotoNext(x => x.MatchCall<Lighting>(nameof(Lighting.Brightness)));
        c.GotoPrev(x => x.MatchLdloc(out iLoc));
        c.GotoPrev(x => x.MatchLdloc(out jLoc));
        c.GotoNext(x => x.MatchStloc(out num8Loc));

        var tileLoc = -1;
        c.GotoNext(x => x.MatchCall(typeof(Math), nameof(Math.Floor)));
        c.GotoNext(MoveType.Before, x => x.MatchLdloca(out tileLoc));

        // IL_03b0
        // var breakLabel = c.DefineLabel();
        c.EmitLdloc(liquidSlopeFixVar);
        c.EmitLdloc(iLoc);
        c.EmitLdloc(jLoc);
        c.EmitLdloc(num8Loc);
        c.EmitLdloc(tileLoc);
        c.EmitDelegate(
            (
                bool liquidSlopeFix,
                int i,
                int j,
                float num8,
                Tile tile
            ) => liquidSlopeFix && LiquidRenderer.Instance.HasFullWater(j, i) && (((tile.Slope != SlopeType.Solid || tile.IsHalfBlock) && num8 >= 5f / 255f) || num8 > 5f / 255f)
        );
        var savedIndex = c.Index;
        // c.EmitBrtrue(breakLabel);

        ILLabel? foundBreakLabel = null;
        c.GotoNext(x => x.MatchLdsfld<Main>(nameof(Main.worldSurface)));
        c.GotoNext(x => x.MatchBgtUn(out foundBreakLabel));
        /*if (foundBreakLabel is null)
        {
            throw new Exception("what");
        }

        breakLabel.Target = foundBreakLabel.Target;*/

        c.Index = savedIndex;
        c.EmitBrtrue(foundBreakLabel);
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

using System.Reflection;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using SpiritMod.Effects.SurfaceWaterModifications;
using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace LiquidSlopesPatch.Common.ModCompat;

[ExtendsFromMod("SpiritMod")]
internal sealed class SpiritClassic : ModSystem
{
    public override void Load()
    {
        base.Load();

        var swmType = typeof(SurfaceWaterModifications);

        MonoModHooks.Add(swmType.GetMethod(nameof(SurfaceWaterModifications.LiquidRenderer_InternalDraw), BindingFlags.Static | BindingFlags.NonPublic), InternalDraw_UseOurLocals);
        MonoModHooks.Add(swmType.GetMethod(nameof(SurfaceWaterModifications.HideSlopedBlack), BindingFlags.Static | BindingFlags.NonPublic), HideSlopedBlack_NoOp);
        MonoModHooks.Add(swmType.GetMethod(nameof(SurfaceWaterModifications.On_TileDrawing_DrawPartialLiquid), BindingFlags.Static | BindingFlags.NonPublic), DrawPartialLiquid_NoOp);
    }

    private static void InternalDraw_UseOurLocals(ILContext il)
    {
        // no-op

        /*ILCursor c = new ILCursor(il);

        if (!c.TryGotoNext(MoveType.After, x => x.MatchCall<Main>(nameof(Main.DrawTileInWater))))
            return;

        c.EmitLdloc(9); //i
        c.EmitLdloc(10); //j
        c.EmitLdloc(11); //vertex colours
        c.EmitLdarg(5); //isBackgroundDraw

        c.EmitDelegate(SurfaceWaterModifications.DrawPartialWater);*/
    }

    private static void HideSlopedBlack_NoOp(ILContext il)
    {
        // no-op
    }

    private static void DrawPartialLiquid_NoOp(
        On_TileDrawing.orig_DrawPartialLiquid orig,
        TileDrawing self,
        bool behindBlocks,
        Tile tileCache,
        ref Vector2 position,
        ref Rectangle liquidSize,
        int liquidType,
        ref VertexColors colors
    )
    {
        // no-op
    }
}

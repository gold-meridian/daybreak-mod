using Daybreak.Common.CIL;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.IDs;

using MonoMod.Cil;

using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Tiles;

/// <summary>
///     Behaves like a Terraria lily pad.
/// </summary>
public interface ILilyPad
{
    /// <summary>
    ///     Whether to use vanilla's <see cref="Main.DrawTileInWater"/> logic.
    /// </summary>
    /// <remarks>
    ///     If enabled, this also disables regular solid tile drawing.
    /// </remarks>
    bool VanillaDrawTileInWater { get; }

    /// <summary>
    ///     Checks validity of the tile and kills it if it isn't valid.
    /// </summary>
    void CheckLilyPad(int x, int y);
}

/// <summary>
///     Lily pad implementation.
/// </summary>
public abstract class LilyPadTile : ModTile, ILilyPad
{
    bool ILilyPad.VanillaDrawTileInWater => true;

    /// <inheritdoc />
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;

        Main.tileCut[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileNoFail[Type] = true;

        DaybreakTileSets.OtherTileDrawDataToCopy[Type] = TileID.LilyPad;
    }

    /// <inheritdoc />
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        CheckLilyPad(i, j);
        return false;
    }

    /// <inheritdoc />
    public abstract void CheckLilyPad(int x, int y);
}

internal static partial class WaterFoliageHandler
{
    [OnLoad]
    public static void Load_LilyPads()
    {
        IL_TileDrawing.DrawSingleTile += DrawSingleTile_SkipDrawingForLilyPads;
        IL_Main.DrawTileInWater += DrawTileInWater_UseLilyPadDrawLogicForAllLilyPads;

        IL_Liquid.DelWater += DelWater_CheckLilyPads;
        On_WorldGen.CheckLilyPad += CheckLilyPad_UseModdedChecks;

        IL_WorldGen.UpdateWorld_OvergroundTile += UpdateWorld_OvergroundTile_AllowBambooToGrowThroughLilyPads;
    }

    private static void DrawSingleTile_SkipDrawingForLilyPads(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.Before, x => x.MatchLdcI4(TileID.LilyPad));
        c.Substitute(TileID.LilyPad, type => TileLoader.GetTile(type) is ILilyPad { VanillaDrawTileInWater: true });
    }

    private static void DrawTileInWater_UseLilyPadDrawLogicForAllLilyPads(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.Before, x => x.MatchLdcI4(TileID.LilyPad));
        c.Substitute(TileID.LilyPad, type => TileLoader.GetTile(type) is ILilyPad { VanillaDrawTileInWater: true });
    }

    private static void DelWater_CheckLilyPads(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.Before, x => x.MatchLdcI4(TileID.LilyPad));
        c.Substitute(TileID.LilyPad, type => TileLoader.GetTile(type) is ILilyPad);
    }

    private static void CheckLilyPad_UseModdedChecks(On_WorldGen.orig_CheckLilyPad orig, int x, int y)
    {
        if (TileLoader.GetTile(Main.tile[x, y].TileType) is ILilyPad lilyPad)
        {
            lilyPad.CheckLilyPad(x, y);
        }
        else
        {
            orig(x, y);
        }
    }

    private static void UpdateWorld_OvergroundTile_AllowBambooToGrowThroughLilyPads(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.Before, x => x.MatchLdcI4(TileID.LilyPad));
        c.Substitute(TileID.LilyPad, type => TileLoader.GetTile(type) is ILilyPad);
    }
}
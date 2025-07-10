using System.Diagnostics;

using Daybreak.Common.Features.Hooks;

using MonoMod.Cil;

using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Tiles;

/// <summary>
///     Implement on a <see cref="ModTile"/> to hook into
///     <see cref="WorldGen.PlaceTile"/> and execute custom logic.
/// </summary>
public interface IPlaceTileHook
{
    /// <summary>
    ///     
    /// </summary>
    /// <param name="i">The X coordinate.</param>
    /// <param name="j">The Y coordinate.</param>
    /// <param name="mute">Inverse to whether a sound should be made.</param>
    /// <param name="forced">
    ///     Attempts to place even if a tile is already present.
    /// </param>
    /// <param name="plr">The player placing the tile, if applicable.</param>
    /// <param name="style">The provided style of the tile.</param>
    /// <remarks>
    ///     <paramref name="plr"/> is often not provided and is used only in
    ///     vanilla for placing bathtubs, which use the player to determine the
    ///     direction of the tile being placed.
    /// </remarks>
    void PlaceTile(int i, int j, bool mute, bool forced, int plr, int style);

    [OnLoad]
    private static void Hook()
    {
        IL_WorldGen.PlaceTile += PlaceTile_CallCustomPlaceHookIfApplicable;
    }

    private static void PlaceTile_CallCustomPlaceHookIfApplicable(ILContext il)
    {
        var c = new ILCursor(il);

        // Position all the way at the end of the switch statement (default
        // case).
        c.GotoNext(x => x.MatchLdsfld<Main>(nameof(Main.tenthAnniversaryWorld)));

        // Step back to the start of the default case.
        c.GotoPrev(x => x.MatchCall<Tile>(nameof(Tile.active)));

        // Find where the previous case falls through so we can also jump to it
        // if necessary.
        ILLabel? fallthrough = null;
        c.GotoPrev(x => x.MatchBr(out fallthrough));
        Debug.Assert(fallthrough is not null);

        // Jump to the actual start of the default case now.
        c.GotoNext(MoveType.AfterLabel, x => x.MatchLdloca(out _));

        c.EmitLdarg(0); // int  i
        c.EmitLdarg(1); // int  j
        c.EmitLdarg(2); // int  Type
        c.EmitLdarg(3); // bool mute
        c.EmitLdarg(4); // bool forced
        c.EmitLdarg(5); // int  plr
        c.EmitLdarg(6); // int  style
        c.EmitDelegate((int i, int j, int type, bool mute, bool forced, int plr, int style) =>
            {
                if (TileLoader.GetTile(type) is not IPlaceTileHook placeTile)
                {
                    return false;
                }

                placeTile.PlaceTile(i, j, mute, forced, plr, style);
                return true;
            }
        );

        // Jump to the end of the switch statement if the tile has its own place
        // logic instead of running vanilla's logic.
        c.EmitBrtrue(fallthrough);
    }
}
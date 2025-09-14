using System.Diagnostics.CodeAnalysis;

using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.PotLoot;

/// <summary>
///     Describes the behavior of a pot.
/// </summary>
public abstract partial class PotBehavior
{
    /// <summary>
    ///     Behavior for Terraria's existing pots.
    /// </summary>
    public static PotBehavior Vanilla { get; } = new VanillaPotBehavior(echo: false);

    /// <summary>
    ///     Behavior for the loot-less echo variants of Terraria's pots.
    /// </summary>
    public static PotBehavior VanillaEcho { get; } = new VanillaPotBehavior(echo: true);

    /// <summary>
    ///     Attempts to get the <see cref="PotBehavior"/> of a tile, if present.
    /// </summary>
    /// <param name="type">The tile type.</param>
    /// <param name="potBehavior">The associated pot behavior.</param>
    public static bool TryGetPotBehavior(
        int type,
        [NotNullWhen(returnValue: true)] out PotBehavior? potBehavior
    )
    {
        switch (type)
        {
            case TileID.Pots:
                potBehavior = Vanilla;
                return true;

            case TileID.PotsEcho:
                potBehavior = VanillaEcho;
                return true;
        }

        if (TileLoader.GetTile(type) is IHasPotBehavior potTile)
        {
            potBehavior = potTile.Behavior;
            return true;
        }

        potBehavior = null;
        return false;
    }
}
using System.Collections.Generic;

using Daybreak.Common.Features.Rarities;

using JetBrains.Annotations;

using Terraria.ModLoader;

namespace Daybreak.Common.IDs;

/// <summary>
///     Provides rarity ID sets.
/// </summary>
[PublicAPI]
public sealed class DaybreakRaritySets : ModSystem
{
    /// <summary>
    ///     Allows you to map a raw implementation of
    ///     <see cref="IRarityTextRenderer"/> to an existing rarity ID
    ///     (either from vanilla, another mod, or your own if you're
    ///     weak-referencing DAYBREAK).
    /// </summary>
    public static Dictionary<int, IRarityTextRenderer> SpecialRarity { get; } = [];

    /// <inheritdoc />
    public override void ResizeArrays()
    {
        base.ResizeArrays();

        SpecialRarity.Clear();
    }
}
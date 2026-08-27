using System;
using Terraria;
using Terraria.ID;

namespace Daybreak.Common.Features.NPCs;

/// <summary>
///     Kinds of behavior exhibited by <see cref="NPC.GetShimmered" />.
/// </summary>
[Flags]
public enum NpcShimmerBehaviorFlags
{
    /// <summary>
    ///     Resets all <see cref="NPC.ai" /> values to <c>0</c>, aside from
    ///     index <c>0</c>, which is set to <c>25f</c>.
    /// </summary>
    ResetAi = 1 << 0,

    /// <summary>
    ///     Resets <see cref="NPC.netUpdate" /> to <see langword="true" />.
    /// </summary>
    NetUpdate = 1 << 1,

    /// <summary>
    ///     Sets <see cref="NPC.shimmerTransparency" />.
    /// </summary>
    ShimmerTransparency = 1 << 2,

    /// <summary>
    ///     Removes the <see cref="BuffID.Shimmer" /> debuf.
    /// </summary>
    RemoveShimmerDebuff = 1 << 3,

    /// <summary>
    ///     No behavior; skips all logic.
    /// </summary>
    None = 0,

    /// <summary>
    ///     All behavior; performs vanilla logic.
    /// </summary>
    All = ResetAi | NetUpdate | ShimmerTransparency | RemoveShimmerDebuff,
}

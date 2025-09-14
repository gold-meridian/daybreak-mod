using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.NPCs;

/// <summary>
///     <see cref="ModNPC"/>s implementing this interface will be granted
///     control over how their town NPC initiates its shimmer transformation.
///     <br />
///     Useful for town NPCs with custom AI.
/// </summary>
public interface INpcCustomShimmerAi
{
    /// <summary>
    ///     Invoked in <see cref="NPC.GetShimmered"/>.
    ///     <br />
    ///     Implementors need to explicitly replicate relevant logic, such as
    ///     net syncing, shimmer transparency, and buff handling.
    /// </summary>
    /// <remarks>
    ///     A <see cref="NpcShimmerBehaviorFlags"/> bitmask, indicating what
    ///     behaviors to preserve and which to skip.
    /// </remarks>
    NpcShimmerBehaviorFlags GetShimmered();
}
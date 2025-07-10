using System;

using Daybreak.Common.Features.Hooks;

using Terraria;
using Terraria.ID;
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
    ///     Kinds of behavior exhibited by <see cref="NPC.GetShimmered"/>.
    /// </summary>
    [Flags]
    public enum Behavior
    {
        /// <summary>
        ///     Resets all <see cref="NPC.ai"/> values to <c>0</c>, aside from
        ///     index <c>0</c>, which is set to <c>25f</c>.
        /// </summary>
        ResetAi = 1 << 0,

        /// <summary>
        ///     Resets <see cref="NPC.netUpdate"/> to <see langword="true"/>.
        /// </summary>
        NetUpdate = 1 << 1,

        /// <summary>
        ///     Sets <see cref="NPC.shimmerTransparency"/>.
        /// </summary>
        ShimmerTransparency = 1 << 2,

        /// <summary>
        ///     Removes the <see cref="BuffID.Shimmer"/> debuf.
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

    /// <summary>
    ///     Invoked in <see cref="NPC.GetShimmered"/>.
    ///     <br />
    ///     Implementors need to explicitly replicate relevant logic, such as
    ///     net syncing, shimmer transparency, and buff handling.
    /// </summary>
    /// <remarks>
    ///     A <see cref="Behavior"/> bitmask, indicating what behaviors to
    ///     preserve and which to skip.
    /// </remarks>
    Behavior GetShimmered();

    [OnLoad]
    private static void ApplyHooks()
    {
        On_NPC.GetShimmered += HandleCustomShimmer;
    }

    private static void HandleCustomShimmer(On_NPC.orig_GetShimmered orig, NPC self)
    {
        if (self.ModNPC is not INpcCustomShimmerAi shimmerHandler)
        {
            orig(self);
            return;
        }

        if (self.SpawnedFromStatue
         || NPCID.Sets.ShimmerTransformToNPC[self.type] >= 0
         || NPCID.Sets.ShimmerTransformToItem[self.type] >= 0
         || !NPCID.Sets.ShimmerTownTransform[self.type])
        {
            return;
        }

        var behavior = shimmerHandler.GetShimmered();

        if (behavior == INpcCustomShimmerAi.Behavior.None)
        {
            return;
        }

        if (behavior.HasFlag(INpcCustomShimmerAi.Behavior.ResetAi))
        {
            self.ai[0] = 25f;
            self.ai[1] = 0f;
            self.ai[2] = 0f;
            self.ai[3] = 0f;
        }

        if (behavior.HasFlag(INpcCustomShimmerAi.Behavior.NetUpdate))
        {
            self.netUpdate = true;
        }

        if (behavior.HasFlag(INpcCustomShimmerAi.Behavior.ShimmerTransparency))
        {
            self.shimmerTransparency = 0.89f;
        }

        if (behavior.HasFlag(INpcCustomShimmerAi.Behavior.RemoveShimmerDebuff))
        {
            var idx = self.FindBuffIndex(BuffID.Shimmer);
            if (idx != -1)
            {
                self.DelBuff(idx);
            }
        }
    }
}
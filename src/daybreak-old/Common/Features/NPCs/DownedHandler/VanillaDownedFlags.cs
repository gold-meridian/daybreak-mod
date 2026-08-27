using Terraria;
using Terraria.ID;

namespace Daybreak.Common.Features.NPCs;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
///     Defined, well-known APIs for named accesses to vanilla downed flags.
/// </summary>
public static class VanillaDownedFlags
{
    private delegate ref bool GetValue();

    public static DownedFlagHandle KingSlime { get; } = Vanilla(NPCID.KingSlime, () => ref NPC.downedSlimeKing);

    public static DownedFlagHandle EyeOfCthulhu { get; } = Vanilla(NPCID.EyeofCthulhu, () => ref NPC.downedBoss1);

    /// <summary>
    ///     Same as <see cref="BrainOfCthulhu" />.
    /// </summary>
    public static DownedFlagHandle EaterOfWorlds { get; } = Vanilla(NPCID.EaterofWorldsHead, () => ref NPC.downedBoss2);

    /// <summary>
    ///     Same as <see cref="EaterOfWorlds" />.
    /// </summary>
    public static DownedFlagHandle BrainOfCthulhu { get; } = Vanilla(NPCID.BrainofCthulhu, () => ref NPC.downedBoss2);

    public static DownedFlagHandle QueenBee { get; } = Vanilla(NPCID.QueenBee, () => ref NPC.downedQueenBee);

    public static DownedFlagHandle Deerclops { get; } = Vanilla(NPCID.Deerclops, () => ref NPC.downedDeerclops);

    public static DownedFlagHandle Skeletron { get; } = Vanilla(NPCID.SkeletronHead, () => ref NPC.downedBoss3);

    public static DownedFlagHandle WallOfFlesh { get; } = Vanilla(NPCID.WallofFlesh, () => ref Main.hardMode);

    public static DownedFlagHandle QueenSlime { get; } = Vanilla(NPCID.QueenSlimeBoss, () => ref NPC.downedQueenSlime);

    public static DownedFlagHandle TheTwins { get; } = Vanilla("TheTwins", () => ref NPC.downedMechBoss2);

    public static DownedFlagHandle TheDestroyer { get; } = Vanilla(NPCID.TheDestroyer, () => ref NPC.downedMechBoss1);

    public static DownedFlagHandle SkeletronPrime { get; } = Vanilla(NPCID.SkeletronPrime, () => ref NPC.downedMechBoss3);

    public static DownedFlagHandle AnyMechBoss { get; } = Vanilla("AnyMechBoss", () => ref NPC.downedMechBossAny);

    public static DownedFlagHandle Plantera { get; } = Vanilla(NPCID.Plantera, () => ref NPC.downedPlantBoss);

    public static DownedFlagHandle Golem { get; } = Vanilla(NPCID.Golem, () => ref NPC.downedGolemBoss);

    public static DownedFlagHandle DukeFishron { get; } = Vanilla(NPCID.DukeFishron, () => ref NPC.downedFishron);

    public static DownedFlagHandle EmpressOfLight { get; } = Vanilla(NPCID.HallowBoss, () => ref NPC.downedEmpressOfLight);

    public static DownedFlagHandle LunaticCultist { get; } = Vanilla(NPCID.CultistBoss, () => ref NPC.downedAncientCultist);

    public static DownedFlagHandle MoonLord { get; } = Vanilla(NPCID.MoonLordCore, () => ref NPC.downedMoonlord);

    // public static DownedFlagHandle DARK_MAGE { get; } = Vanilla(NPCID.KingSlime, () => ref NPC.);

    // public static DownedFlagHandle OGRE { get; } = Vanilla(NPCID.KingSlime, () => ref NPC.);

    // public static DownedFlagHandle BETSY { get; } = Vanilla(NPCID.KingSlime, () => ref NPC);

    // public static DownedFlagHandle FLYING_DUTCHMAN { get; } = Vanilla(NPCID.KingSlime, () => ref NPC.);

    public static DownedFlagHandle MourningWood { get; } = Vanilla(NPCID.MourningWood, () => ref NPC.downedHalloweenTree);

    public static DownedFlagHandle Pumpking { get; } = Vanilla(NPCID.Pumpking, () => ref NPC.downedHalloweenKing);

    public static DownedFlagHandle Everscream { get; } = Vanilla(NPCID.Everscream, () => ref NPC.downedChristmasTree);

    public static DownedFlagHandle SantaNk1 { get; } = Vanilla(NPCID.SantaNK1, () => ref NPC.downedChristmasSantank);

    public static DownedFlagHandle IceQueen { get; } = Vanilla(NPCID.IceQueen, () => ref NPC.downedChristmasIceQueen);

    // public static DownedFlagHandle MARTIAN_SAUCER { get; } = Vanilla(NPCID.KingSlime, () => ref NPC.);

    public static DownedFlagHandle SolarPillar { get; } = Vanilla(NPCID.LunarTowerSolar, () => ref NPC.downedTowerSolar);

    public static DownedFlagHandle NebulaPillar { get; } = Vanilla(NPCID.LunarTowerNebula, () => ref NPC.downedTowerNebula);

    public static DownedFlagHandle VortexPillar { get; } = Vanilla(NPCID.LunarTowerVortex, () => ref NPC.downedTowerVortex);

    public static DownedFlagHandle StardustPillar { get; } = Vanilla(NPCID.LunarTowerStardust, () => ref NPC.downedTowerStardust);

    public static DownedFlagHandle Clown { get; } = Vanilla(NPCID.Clown, () => ref NPC.downedClown);

    public static DownedFlagHandle GoblinArmy { get; } = Vanilla("GoblinArmy", () => ref NPC.downedGoblins);

    public static DownedFlagHandle FrostLegion { get; } = Vanilla("FrostLegion", () => ref NPC.downedFrost);

    public static DownedFlagHandle PirateInvasion { get; } = Vanilla("PirateInvasion", () => ref NPC.downedPirates);

    public static DownedFlagHandle MartianMadness { get; } = Vanilla("MartianMadness", () => ref NPC.downedMartians);

    private static DownedFlagHandle Vanilla(int npcId, GetValue valueProvider)
    {
        var npcName = NPCID.Search.GetName(npcId);
        return Vanilla(npcName, valueProvider);
    }

    private static DownedFlagHandle Vanilla(string name, GetValue valueProvider)
    {
        return DownedFlagHandler.RegisterCustomHandle("Terraria", name, () => valueProvider(), v => valueProvider() = v);
    }
}

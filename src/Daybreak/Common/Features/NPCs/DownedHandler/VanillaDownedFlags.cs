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

    public static readonly DownedFlagHandle KING_SLIME = Vanilla(NPCID.KingSlime, () => ref NPC.downedSlimeKing);

    public static readonly DownedFlagHandle EYE_OF_CTHULHU = Vanilla(NPCID.EyeofCthulhu, () => ref NPC.downedBoss1);

    /// <summary>
    ///     Same as <see cref="BRAIN_OF_CTHULHU"/>.
    /// </summary>
    public static readonly DownedFlagHandle EATER_OF_WORLDS = Vanilla(NPCID.EaterofWorldsHead, () => ref NPC.downedBoss2);

    /// <summary>
    ///     Same as <see cref="EATER_OF_WORLDS"/>.
    /// </summary>
    public static readonly DownedFlagHandle BRAIN_OF_CTHULHU = Vanilla(NPCID.BrainofCthulhu, () => ref NPC.downedBoss2);

    public static readonly DownedFlagHandle QUEEN_BEE = Vanilla(NPCID.QueenBee, () => ref NPC.downedQueenBee);

    public static readonly DownedFlagHandle DEERCLOPS = Vanilla(NPCID.Deerclops, () => ref NPC.downedDeerclops);

    public static readonly DownedFlagHandle SKELETRON = Vanilla(NPCID.SkeletronHead, () => ref NPC.downedBoss3);

    public static readonly DownedFlagHandle WALL_OF_FLESH = Vanilla(NPCID.WallofFlesh, () => ref Main.hardMode);

    public static readonly DownedFlagHandle QUEEN_SLIME = Vanilla(NPCID.QueenSlimeBoss, () => ref NPC.downedQueenSlime);

    public static readonly DownedFlagHandle THE_TWINS = Vanilla("TheTwins", () => ref NPC.downedMechBoss2);

    public static readonly DownedFlagHandle THE_DESTROYER = Vanilla(NPCID.TheDestroyer, () => ref NPC.downedMechBoss1);

    public static readonly DownedFlagHandle SKELETRON_PRIME = Vanilla(NPCID.SkeletronPrime, () => ref NPC.downedMechBoss3);

    public static readonly DownedFlagHandle ANY_MECH_BOSS = Vanilla("AnyMechBoss", () => ref NPC.downedMechBossAny);

    public static readonly DownedFlagHandle PLANTERA = Vanilla(NPCID.Plantera, () => ref NPC.downedPlantBoss);

    public static readonly DownedFlagHandle GOLEM = Vanilla(NPCID.Golem, () => ref NPC.downedGolemBoss);

    public static readonly DownedFlagHandle DUKE_FISHRON = Vanilla(NPCID.DukeFishron, () => ref NPC.downedFishron);

    public static readonly DownedFlagHandle EMPRESS_OF_LIGHT = Vanilla(NPCID.HallowBoss, () => ref NPC.downedEmpressOfLight);

    public static readonly DownedFlagHandle LUNATIC_CULTIST = Vanilla(NPCID.CultistBoss, () => ref NPC.downedAncientCultist);

    public static readonly DownedFlagHandle MOON_LORD = Vanilla(NPCID.MoonLordCore, () => ref NPC.downedMoonlord);

    // public static readonly DownedFlagHandle DARK_MAGE = Vanilla(NPCID.KingSlime, () => ref NPC.);

    // public static readonly DownedFlagHandle OGRE = Vanilla(NPCID.KingSlime, () => ref NPC.);

    // public static readonly DownedFlagHandle BETSY = Vanilla(NPCID.KingSlime, () => ref NPC);

    // public static readonly DownedFlagHandle FLYING_DUTCHMAN = Vanilla(NPCID.KingSlime, () => ref NPC.);

    public static readonly DownedFlagHandle MOURNING_WOOD = Vanilla(NPCID.MourningWood, () => ref NPC.downedHalloweenTree);

    public static readonly DownedFlagHandle PUMPKING = Vanilla(NPCID.Pumpking, () => ref NPC.downedHalloweenKing);

    public static readonly DownedFlagHandle EVERSCREAM = Vanilla(NPCID.Everscream, () => ref NPC.downedChristmasTree);

    public static readonly DownedFlagHandle SANTA_NK1 = Vanilla(NPCID.SantaNK1, () => ref NPC.downedChristmasSantank);

    public static readonly DownedFlagHandle ICE_QUEEN = Vanilla(NPCID.IceQueen, () => ref NPC.downedChristmasIceQueen);

    // public static readonly DownedFlagHandle MARTIAN_SAUCER = Vanilla(NPCID.KingSlime, () => ref NPC.);

    public static readonly DownedFlagHandle SOLAR_PILLAR = Vanilla(NPCID.LunarTowerSolar, () => ref NPC.downedTowerSolar);

    public static readonly DownedFlagHandle NEBULA_PILLAR = Vanilla(NPCID.LunarTowerNebula, () => ref NPC.downedTowerNebula);

    public static readonly DownedFlagHandle VORTEX_PILLAR = Vanilla(NPCID.LunarTowerVortex, () => ref NPC.downedTowerVortex);

    public static readonly DownedFlagHandle STARDUST_PILLAR = Vanilla(NPCID.LunarTowerStardust, () => ref NPC.downedTowerStardust);

    public static readonly DownedFlagHandle CLOWN = Vanilla(NPCID.Clown, () => ref NPC.downedClown);

    public static readonly DownedFlagHandle GOBLIN_ARMY = Vanilla("GoblinArmy", () => ref NPC.downedGoblins);

    public static readonly DownedFlagHandle FROST_LEGION = Vanilla("FrostLegion", () => ref NPC.downedFrost);

    public static readonly DownedFlagHandle PIRATE_INVASION = Vanilla("PirateInvasion", () => ref NPC.downedPirates);

    public static readonly DownedFlagHandle MARTIAN_MADNESS = Vanilla("MartianMadness", () => ref NPC.downedMartians);

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
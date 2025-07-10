using System;
using System.Collections.Generic;

using Daybreak.Common.Features.Hooks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.Achievements;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Achievements;

// TODO: [DependsOn<AchievementImpl>]
internal static class VanillaAchievements
{
    private sealed class VanillaCategory : AchievementCategory
    {
        public override string Name { get; }

        public override LocalizedText DisplayName => Language.GetText(category_keys[key]);

        private readonly int key;

        private static readonly Asset<Texture2D> category_texture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Categories", AssetRequestMode.ImmediateLoad);

        public VanillaCategory(string name, Terraria.Achievements.AchievementCategory vanillaCategory)
        {
            Name = name;
            key = (int)vanillaCategory;
            VANILLA_CATEGORIES_BY_NAME[name] = this;
        }

        public override Asset<Texture2D> GetIcon(out Rectangle frame)
        {
            frame = category_texture.Frame(4, 2, key);
            return category_texture;
        }
    }

    private sealed class VanillaAchievement : Achievement
    {
        public override string Name { get; }

        public override LocalizedText DisplayName => Language.GetText($"Achievements.{key}_Name");

        public override LocalizedText Description => Language.GetText($"Achievements.{key}_Description");

        public override float AdvisorOrder => orders.GetValueOrDefault(key, -1f);

        private readonly string key;

        private const int icon_size = 64;
        private const int icon_size_with_space = 66;
        private const int icons_per_row = 8;

        public VanillaAchievement(string name)
        {
            Name = name;
            VANILLA_ACHIEVEMENTS_BY_NAME[key = name] = this;
        }

        public override IEnumerable<AchievementCategory> GetCategories()
        {
            if (!categories.TryGetValue(key, out var category))
            {
                yield return IDs.VanillaAchievements.Categories.Slayer;
                yield break;
            }

            yield return category switch
            {
                Terraria.Achievements.AchievementCategory.None => throw new InvalidOperationException("Vanilla category '" + key + "' is none."),
                Terraria.Achievements.AchievementCategory.Slayer => IDs.VanillaAchievements.Categories.Slayer,
                Terraria.Achievements.AchievementCategory.Collector => IDs.VanillaAchievements.Categories.Collector,
                Terraria.Achievements.AchievementCategory.Explorer => IDs.VanillaAchievements.Categories.Explorer,
                Terraria.Achievements.AchievementCategory.Challenger => IDs.VanillaAchievements.Categories.Challenger,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        public override bool IsPresentlyAvailable()
        {
            return key switch
            {
                "MASTERMIND" => WorldGen.crimson,
                "WORM_FODDER" => !WorldGen.crimson,
                "PLAY_ON_A_SPECIAL_SEED" => Main.specialSeedWorld,
                _ => true,
            };
        }

        public override Asset<Texture2D> GetIcon(out Rectangle frame, out int lockedOffset)
        {
            var idx = icon_indices[key];
            frame = new Rectangle(
                idx % icons_per_row * icon_size_with_space,
                idx / icons_per_row * icon_size_with_space,
                icon_size,
                icon_size
            );
            lockedOffset = 528;
            return Main.Assets.Request<Texture2D>("Images/UI/Achievements");
        }

        public override float? GetProgress(out string progressText)
        {
            progressText = string.Empty;

            var vanillaAchievement = Main.Achievements.GetAchievement(key);
            if (vanillaAchievement is null)
            {
                return null;
            }

            if (!vanillaAchievement.HasTracker)
            {
                return null;
            }

            var tracker = vanillaAchievement.GetTracker();
            switch (tracker.GetTrackerType())
            {
                case TrackerType.Float:
                {
                    if (tracker is not AchievementTracker<float> floatTracker)
                    {
                        return null;
                    }

                    progressText = $"{(int)floatTracker.Value}/{(int)floatTracker.MaxValue}";
                    return floatTracker.Value / floatTracker.MaxValue;
                }

                case TrackerType.Int:
                {
                    if (tracker is not AchievementTracker<int> intTracker)
                    {
                        return null;
                    }

                    progressText = $"{intTracker.Value}/{intTracker.MaxValue}";
                    return intTracker.Value / (float)intTracker.MaxValue;
                }

                default:
                    return null;
            }
        }
    }

    internal static readonly Dictionary<string, AchievementCategory> VANILLA_CATEGORIES_BY_NAME = [];
    internal static readonly Dictionary<string, Achievement> VANILLA_ACHIEVEMENTS_BY_NAME = [];

    private static readonly Dictionary<string, int> icon_indices = [];
    private static readonly Dictionary<string, float> orders = [];
    private static readonly Dictionary<string, Terraria.Achievements.AchievementCategory> categories = [];

    private static readonly Dictionary<string, Terraria.Achievements.AchievementCategory> vanilla_categories = new()
    {
        { "Slayer", Terraria.Achievements.AchievementCategory.Slayer },
        { "Collector", Terraria.Achievements.AchievementCategory.Collector },
        { "Explorer", Terraria.Achievements.AchievementCategory.Explorer },
        { "Challenger", Terraria.Achievements.AchievementCategory.Challenger },
    };

    private static readonly string[] category_keys =
    [
        "Achievements.SlayerCategory",
        "Achievements.CollectorCategory",
        "Achievements.ExplorerCategory",
        "Achievements.ChallengerCategory",
    ];

    [OnLoad]
    public static void Load()
    {
        foreach (var (name, category) in vanilla_categories)
        {
            ModContent.GetInstance<ModImpl>().AddContent(new VanillaCategory(name, category));
        }

        var num = 0;
        icon_indices["TIMBER"] = num++;
        icon_indices["NO_HOBO"] = num++;
        icon_indices["OBTAIN_HAMMER"] = num++;
        icon_indices["HEART_BREAKER"] = num++;
        icon_indices["OOO_SHINY"] = num++;
        icon_indices["HEAVY_METAL"] = num++;
        icon_indices["I_AM_LOOT"] = num++;
        icon_indices["STAR_POWER"] = num++;
        icon_indices["HOLD_ON_TIGHT"] = num++;
        icon_indices["EYE_ON_YOU"] = num++;
        icon_indices["SMASHING_POPPET"] = num++;
        icon_indices["WORM_FODDER"] = num++;
        icon_indices["MASTERMIND"] = num++;
        icon_indices["WHERES_MY_HONEY"] = num++;
        icon_indices["STING_OPERATION"] = num++;
        icon_indices["BONED"] = num++;
        icon_indices["DUNGEON_HEIST"] = num++;
        icon_indices["ITS_GETTING_HOT_IN_HERE"] = num++;
        icon_indices["MINER_FOR_FIRE"] = num++;
        icon_indices["STILL_HUNGRY"] = num++;
        icon_indices["ITS_HARD"] = num++;
        icon_indices["BEGONE_EVIL"] = num++;
        icon_indices["EXTRA_SHINY"] = num++;
        icon_indices["HEAD_IN_THE_CLOUDS"] = num++;
        icon_indices["LIKE_A_BOSS"] = num++;
        icon_indices["BUCKETS_OF_BOLTS"] = num++;
        icon_indices["DRAX_ATTAX"] = num++;
        icon_indices["PHOTOSYNTHESIS"] = num++;
        icon_indices["GET_A_LIFE"] = num++;
        icon_indices["THE_GREAT_SOUTHERN_PLANTKILL"] = num++;
        icon_indices["TEMPLE_RAIDER"] = num++;
        icon_indices["LIHZAHRDIAN_IDOL"] = num++;
        icon_indices["ROBBING_THE_GRAVE"] = num++;
        icon_indices["BIG_BOOTY"] = num++;
        icon_indices["FISH_OUT_OF_WATER"] = num++;
        icon_indices["OBSESSIVE_DEVOTION"] = num++;
        icon_indices["STAR_DESTROYER"] = num++;
        icon_indices["CHAMPION_OF_TERRARIA"] = num++;
        icon_indices["BLOODBATH"] = num++;
        icon_indices["GOBLIN_PUNTER"] = num++;
        icon_indices["KILL_THE_SUN"] = num++;
        icon_indices["WALK_THE_PLANK"] = num++;
        icon_indices["DO_YOU_WANT_TO_SLAY_A_SNOWMAN"] = num++;
        icon_indices["TIN_FOIL_HATTER"] = num++;
        icon_indices["BALEFUL_HARVEST"] = num++;
        icon_indices["ICE_SCREAM"] = num++;
        icon_indices["SLIPPERY_SHINOBI"] = num++;
        icon_indices["STICKY_SITUATION"] = num++;
        icon_indices["REAL_ESTATE_AGENT"] = num++;
        icon_indices["NOT_THE_BEES"] = num++;
        icon_indices["JEEPERS_CREEPERS"] = num++;
        icon_indices["FUNKYTOWN"] = num++;
        icon_indices["INTO_ORBIT"] = num++;
        icon_indices["ROCK_BOTTOM"] = num++;
        icon_indices["MECHA_MAYHEM"] = num++;
        icon_indices["GELATIN_WORLD_TOUR"] = num++;
        icon_indices["FASHION_STATEMENT"] = num++;
        icon_indices["VEHICULAR_MANSLAUGHTER"] = num++;
        icon_indices["BULLDOZER"] = num++;
        icon_indices["THERE_ARE_SOME_WHO_CALL_HIM"] = num++;
        icon_indices["DECEIVER_OF_FOOLS"] = num++;
        icon_indices["SWORD_OF_THE_HERO"] = num++;
        icon_indices["LUCKY_BREAK"] = num++;
        icon_indices["THROWING_LINES"] = num++;
        icon_indices["DYE_HARD"] = num++;
        icon_indices["FREQUENT_FLYER"] = num++;
        icon_indices["THE_CAVALRY"] = num++;
        icon_indices["COMPLETELY_AWESOME"] = num++;
        icon_indices["TIL_DEATH"] = num++;
        icon_indices["ARCHAEOLOGIST"] = num++;
        icon_indices["PRETTY_IN_PINK"] = num++;
        icon_indices["RAINBOWS_AND_UNICORNS"] = num++;
        icon_indices["YOU_AND_WHAT_ARMY"] = num++;
        icon_indices["PRISMANCER"] = num++;
        icon_indices["IT_CAN_TALK"] = num++;
        icon_indices["WATCH_YOUR_STEP"] = num++;
        icon_indices["MARATHON_MEDALIST"] = num++;
        icon_indices["GLORIOUS_GOLDEN_POLE"] = num++;
        icon_indices["SERVANT_IN_TRAINING"] = num++;
        icon_indices["GOOD_LITTLE_SLAVE"] = num++;
        icon_indices["TROUT_MONKEY"] = num++;
        icon_indices["FAST_AND_FISHIOUS"] = num++;
        icon_indices["SUPREME_HELPER_MINION"] = num++;
        icon_indices["TOPPED_OFF"] = num++;
        icon_indices["SLAYER_OF_WORLDS"] = num++;
        icon_indices["YOU_CAN_DO_IT"] = num++;
        icon_indices["SICK_THROW"] = num++;
        icon_indices["MATCHING_ATTIRE"] = num++;
        icon_indices["BENCHED"] = num++;
        icon_indices["DEFEAT_QUEEN_SLIME"] = num++;
        icon_indices["DEFEAT_EMPRESS_OF_LIGHT"] = num++;
        icon_indices["GET_ZENITH"] = num++;
        icon_indices["FIND_A_FAIRY"] = num++;
        icon_indices["DEFEAT_DREADNAUTILUS"] = num++;
        icon_indices["DEFEAT_OLD_ONES_ARMY_TIER3"] = num++;
        icon_indices["FLY_A_KITE_ON_A_WINDY_DAY"] = num++;
        icon_indices["TURN_GNOME_TO_STATUE"] = num++;
        icon_indices["TALK_TO_NPC_AT_MAX_HAPPINESS"] = num++;
        icon_indices["GET_TERRASPARK_BOOTS"] = num++;
        icon_indices["THROW_A_PARTY"] = num++;
        icon_indices["PET_THE_PET"] = num++;
        icon_indices["GO_LAVA_FISHING"] = num++;
        icon_indices["FOUND_GRAVEYARD"] = num++;
        icon_indices["DIE_TO_DEAD_MANS_CHEST"] = num++;
        icon_indices["DEFEAT_DEERCLOPS"] = num++;
        icon_indices["GET_GOLDEN_DELIGHT"] = num++;
        icon_indices["DRINK_BOTTLED_WATER_WHILE_DROWNING"] = num++;
        icon_indices["GET_CELL_PHONE"] = num++;
        icon_indices["GET_ANKH_SHIELD"] = num++;
        icon_indices["GAIN_TORCH_GODS_FAVOR"] = num++;
        icon_indices["PLAY_ON_A_SPECIAL_SEED"] = num++;
        icon_indices["ALL_TOWN_SLIMES"] = num++;
        icon_indices["TRANSMUTE_ITEM"] = num++;
        icon_indices["PURIFY_ENTIRE_WORLD"] = num++;
        icon_indices["TO_INFINITY_AND_BEYOND"] = num++;
        _ = num;

        categories["EYE_ON_YOU"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["SLIPPERY_SHINOBI"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["WORM_FODDER"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["MASTERMIND"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["STING_OPERATION"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["DEFEAT_DEERCLOPS"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["BONED"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["STILL_HUNGRY"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["DEFEAT_DREADNAUTILUS"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["DEFEAT_QUEEN_SLIME"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["BUCKETS_OF_BOLTS"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["THE_GREAT_SOUTHERN_PLANTKILL"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["LIHZAHRDIAN_IDOL"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["FISH_OUT_OF_WATER"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["DEFEAT_EMPRESS_OF_LIGHT"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["OBSESSIVE_DEVOTION"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["STAR_DESTROYER"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["CHAMPION_OF_TERRARIA"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["GOBLIN_PUNTER"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["DO_YOU_WANT_TO_SLAY_A_SNOWMAN"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["WALK_THE_PLANK"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["BALEFUL_HARVEST"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["ICE_SCREAM"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["TIN_FOIL_HATTER"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["DEFEAT_OLD_ONES_ARMY_TIER3"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["TIL_DEATH"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["THERE_ARE_SOME_WHO_CALL_HIM"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["ARCHAEOLOGIST"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["PRETTY_IN_PINK"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["DECEIVER_OF_FOOLS"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["VEHICULAR_MANSLAUGHTER"] = Terraria.Achievements.AchievementCategory.Slayer;
        categories["SMASHING_POPPET"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["BEGONE_EVIL"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["FOUND_GRAVEYARD"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["ITS_HARD"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["FUNKYTOWN"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["WATCH_YOUR_STEP"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["YOU_CAN_DO_IT"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["BLOODBATH"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["KILL_THE_SUN"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["STICKY_SITUATION"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["NO_HOBO"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["IT_CAN_TALK"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["HEART_BREAKER"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["I_AM_LOOT"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["ROBBING_THE_GRAVE"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["GET_A_LIFE"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["FIND_A_FAIRY"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["TRANSMUTE_ITEM"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["JEEPERS_CREEPERS"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["WHERES_MY_HONEY"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["DUNGEON_HEIST"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["BIG_BOOTY"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["ITS_GETTING_HOT_IN_HERE"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["INTO_ORBIT"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["ROCK_BOTTOM"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["OOO_SHINY"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["EXTRA_SHINY"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["PHOTOSYNTHESIS"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["PLAY_ON_A_SPECIAL_SEED"] = Terraria.Achievements.AchievementCategory.Explorer;
        categories["GELATIN_WORLD_TOUR"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["SLAYER_OF_WORLDS"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["REAL_ESTATE_AGENT"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["ALL_TOWN_SLIMES"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["YOU_AND_WHAT_ARMY"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["TOPPED_OFF"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["MECHA_MAYHEM"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["BULLDOZER"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["PURIFY_ENTIRE_WORLD"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["NOT_THE_BEES"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["FLY_A_KITE_ON_A_WINDY_DAY"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["DIE_TO_DEAD_MANS_CHEST"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["GO_LAVA_FISHING"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["RAINBOWS_AND_UNICORNS"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["THROWING_LINES"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["TURN_GNOME_TO_STATUE"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["TALK_TO_NPC_AT_MAX_HAPPINESS"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["FREQUENT_FLYER"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["LUCKY_BREAK"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["MARATHON_MEDALIST"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["PET_THE_PET"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["THROW_A_PARTY"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["DRINK_BOTTLED_WATER_WHILE_DROWNING"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["TO_INFINITY_AND_BEYOND"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["SERVANT_IN_TRAINING"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["GOOD_LITTLE_SLAVE"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["TROUT_MONKEY"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["FAST_AND_FISHIOUS"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["SUPREME_HELPER_MINION"] = Terraria.Achievements.AchievementCategory.Challenger;
        categories["OBTAIN_HAMMER"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["BENCHED"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["HEAVY_METAL"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["STAR_POWER"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["GET_GOLDEN_DELIGHT"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["MINER_FOR_FIRE"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["HEAD_IN_THE_CLOUDS"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["GET_TERRASPARK_BOOTS"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["GET_CELL_PHONE"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["GET_ANKH_SHIELD"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["DRAX_ATTAX"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["PRISMANCER"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["SWORD_OF_THE_HERO"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["GET_ZENITH"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["HOLD_ON_TIGHT"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["THE_CAVALRY"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["DYE_HARD"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["MATCHING_ATTIRE"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["FASHION_STATEMENT"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["COMPLETELY_AWESOME"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["TIMBER"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["SICK_THROW"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["GLORIOUS_GOLDEN_POLE"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["TEMPLE_RAIDER"] = Terraria.Achievements.AchievementCategory.Collector;
        categories["LIKE_A_BOSS"] = Terraria.Achievements.AchievementCategory.Collector;

        foreach (var achievement in icon_indices.Keys)
        {
            ModContent.GetInstance<ModImpl>().AddContent(new VanillaAchievement(achievement));
        }

        // Vanilla sets this to 0, but we want mods to be able to add
        // achievements before Timber!!, theoretically.
        num = 1;
        orders["TIMBER"] = num++;
        orders["BENCHED"] = num++;
        orders["OBTAIN_HAMMER"] = num++;
        orders["NO_HOBO"] = num++;
        orders["YOU_CAN_DO_IT"] = num++;
        orders["OOO_SHINY"] = num++;
        orders["HEAVY_METAL"] = num++;
        orders["MATCHING_ATTIRE"] = num++;
        orders["HEART_BREAKER"] = num++;
        orders["I_AM_LOOT"] = num++;
        orders["HOLD_ON_TIGHT"] = num++;
        orders["STAR_POWER"] = num++;
        orders["EYE_ON_YOU"] = num++;
        orders["SMASHING_POPPET"] = num++;
        orders["WHERES_MY_HONEY"] = num++;
        orders["STING_OPERATION"] = num++;
        orders["BONED"] = num++;
        orders["DUNGEON_HEIST"] = num++;
        orders["ITS_GETTING_HOT_IN_HERE"] = num++;
        orders["MINER_FOR_FIRE"] = num++;
        orders["STILL_HUNGRY"] = num++;
        orders["ITS_HARD"] = num++;
        orders["BEGONE_EVIL"] = num++;
        orders["EXTRA_SHINY"] = num++;
        orders["HEAD_IN_THE_CLOUDS"] = num++;
        orders["BUCKETS_OF_BOLTS"] = num++;
        orders["DRAX_ATTAX"] = num++;
        orders["PHOTOSYNTHESIS"] = num++;
        orders["GET_A_LIFE"] = num++;
        orders["THE_GREAT_SOUTHERN_PLANTKILL"] = num++;
        orders["TEMPLE_RAIDER"] = num++;
        orders["LIHZAHRDIAN_IDOL"] = num++;
        orders["ROBBING_THE_GRAVE"] = num++;
        orders["OBSESSIVE_DEVOTION"] = num++;
        orders["STAR_DESTROYER"] = num++;
        orders["CHAMPION_OF_TERRARIA"] = num++;
        _ = num;
    }
}
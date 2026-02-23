using Daybreak.Common.Features.Configuration;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameInput;
using Terraria.Graphics.Light;
using Terraria.ModLoader;
using static Daybreak.Content.Configuration.TerrariaConfig.Video;

namespace Daybreak.Content.Configuration;

internal static class TerrariaConfig
{
    private static Mod? Mod => null;

    private static ConfigRepository Config => ConfigRepository.Default;

    private delegate ref T? RefProvider<T>();

    private static ConfigEntryOptions<T> Define<T>(RefProvider<T> value)
    {
        return ConfigEntry<T>
              .Define()
               // These values are handled separately and should not be synced
               // by us.
              .WithConfigSide(ConfigSide.NoSync)
               // These values already exist elsewhere, so we just need to
               // provide a way to get and set them.
              .WithValueTransformer(
                   getter: (_, layer, localValue) =>
                   {
                       if (layer == ConfigValueLayer.User)
                       {
                           return ConfigValue<T>.Set(value()!);
                       }

                       return localValue;
                   },
                   setter: (_, layer, ref storedValue, newValue) =>
                   {
                       if (layer == ConfigValueLayer.User)
                       {
                           value() = newValue.Value;
                       }

                       storedValue = newValue;
                   }
               )
               // These values are saved externally, and we should never have to
               // read or write them.
              .WithSerialization(
                   serializer: (_, _) => null,
                   deserializer: (entry, _) => entry.GetLayerValue(ConfigValueLayer.Default)
               );
    }

    public static class General
    {
        public static ConfigCategoryHandle Category { get; } =
            ConfigCategory
               .Define()
               .WithDisplayName(Lang.menu[114]) // General
               .Register(Config, Mod, nameof(General));

        // Autosave - On/Off
        // Autopause - On/Off
        // Map Enabled - On/Off
        // Hide Password - On/Off

        // TODO: Localization

        // LegacyMenu.67/LegacyMenu.68
        public static ConfigEntry<bool> Autosave { get; } =
            Define(() => ref Main.autoSave)
               .WithCategories(Category)
               .Register(Config, Mod);

        // LegacyMenu.69/LegacyMenu.70
        public static ConfigEntry<bool> Autopause { get; } =
            Define(() => ref Main.autoPause)
               .WithCategories(Category)
               .Register(Config, Mod);

        // LegacyMenu.112/LegacyMenu.113
        public static ConfigEntry<bool> MapEnabled { get; } =
            Define(() => ref Main.mapEnabled)
               .WithCategories(Category)
               .Register(Config, Mod);

        // LegacyMenu.211/LegacyMenu.212
        public static ConfigEntry<bool> HidePassword { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Interface
    {
        public static ConfigCategoryHandle Category { get; } =
            ConfigCategory
               .Define()
               .WithDisplayName(Lang.menu[210]) // Interface
               .Register(Config, Mod, nameof(Interface));

        //  Pickup Text - On/Off
        //  Event Progress Bar - On/Off
        //  Placement Preview - On/Off
        //  Highlight New Items - On/Off
        //  Tile Grid - On/Off
        //  Gamepad Instructions - On/Off
        //  ??? Minimap Border: {}
        //  ??? Health and Mana Style: {}
        //  Boss Health Bar Numbers - On/Off
        //  ??? Boss bar Style: {}

        // TODO: Localization

        public static ConfigEntry<bool> LoadStub1 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Video
    {
        public static ConfigCategoryHandle Category { get; } =
            ConfigCategory
               .Define()
               .WithDisplayName(Terraria.Localization.Language.GetText("UI.Video")) // Video
               .Register(Config, Mod, nameof(Video));

        //  Resolution - ???
        //  Fullscreen Toggle - On/Off
        // Show Background - On/Off
        // Background Parallax - 0-100%
        // Lighting Mode - White, Retro, Trippy, Color
        // Quality - Auto, Low, Medium, High
        //  Frame Skip - Off, On, Subtle
        // Storm Effects - On/Off
        // Heat Distortion - On/Off
        //  Windy Environment - On/Off
        // Wave Quality - Off, Low, Medium, High
        //  Blood and Gore - On/Off
        //  Miner's Wobble - On/Off
        
        // TODO: Localization

#region Background

        // LegacyMenu.100/LegacyMenu.101
        public static ConfigEntry<bool> ShowBackground { get; } =
            Define(() => ref Main.BackgroundEnabled)
               .WithCategories(Category)
               .Register(Config, Mod);

        // Min 0f
        // Max 1f
        public static ConfigEntry<float> BackgroundParallax { get; } =
           ConfigEntry<float>
              .Define()
              .WithConfigSide(ConfigSide.NoSync)
              .WithValueTransformer(
                   getter: (_, layer, localValue) =>
                   {
                       if (layer == ConfigValueLayer.User)
                       {
                           return ConfigValue<float>.Set(Main.bgScroll);
                       }

                       return localValue;
                   },
                   setter: (_, layer, ref storedValue, newValue) =>
                   {
                       if (layer == ConfigValueLayer.User)
                       {
                           Main.bgScroll = (int)(newValue.Value * 100f);
                           Main.caveParallax = 1f - Main.bgScroll / 500f;
                       }

                       storedValue = newValue;
                   }
               )
              .WithSerialization(
                   serializer: (_, _) => null,
                   deserializer: (entry, _) => entry.GetLayerValue(ConfigValueLayer.Default)
               )
               .WithCategories(Category)
               .WithDisplayName(Lang.menu[52]) // Parallax
               .Register(Config, Mod);

#endregion

        public static ConfigEntry<LightMode> LightingMode { get; } =
            ConfigEntry<LightMode>
               .Define()
               .WithConfigSide(ConfigSide.NoSync)
               .WithValueTransformer(
                    getter: (_, layer, localValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            return ConfigValue<LightMode>.Set(Lighting.Mode);
                        }

                        return localValue;
                    },
                    setter: (_, layer, ref storedValue, newValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            Lighting.Mode = newValue.Value;
                        }

                        storedValue = newValue;
                    }
                )
               .WithSerialization(
                    serializer: (_, _) => null,
                    deserializer: (entry, _) => entry.GetLayerValue(ConfigValueLayer.Default)
                )
                .WithCategories(Category)
                .Register(Config, Mod);

        public enum QualityMode : int
        {
            Auto,
            Low,
            Medium,
            High
        }

        public static ConfigEntry<QualityMode> Quality { get; } =
            ConfigEntry<QualityMode>
              .Define()
              .WithConfigSide(ConfigSide.NoSync)
              .WithValueTransformer(
                   getter: (_, layer, localValue) =>
                   {
                       if (layer == ConfigValueLayer.User)
                       {
                           return ConfigValue<QualityMode>.Set((QualityMode)Main.qaStyle);
                       }

                       return localValue;
                   },
                   setter: (_, layer, ref storedValue, newValue) =>
                   {
                       if (layer == ConfigValueLayer.User)
                       {
                           Main.qaStyle = (int)newValue.Value;
                       }

                       storedValue = newValue;
                   }
               )
              .WithSerialization(
                   serializer: (_, _) => null,
                   deserializer: (entry, _) => entry.GetLayerValue(ConfigValueLayer.Default)
               )
               .WithCategories(Category)
               .Register(Config, Mod);

#region Effects
        // GameUI.StormEffects
        public static ConfigEntry<bool> StormEffects { get; } =
            Define(() => ref Main.UseStormEffects)
               .WithCategories(Category)
               .Register(Config, Mod);

        // GameUI.HeatDistortion
        public static ConfigEntry<bool> HeatDistortion { get; } =
            Define(() => ref Main.UseHeatDistortion)
               .WithCategories(Category)
               .Register(Config, Mod);

        public enum WaveQualityMode : int
        {
            Off,
            Low,
            Medium,
            High
        }

        // GameUI.WaveQuality
        public static ConfigEntry<WaveQualityMode> WaveQuality { get; } =
            ConfigEntry<WaveQualityMode>
               .Define()
               .WithConfigSide(ConfigSide.NoSync)
               .WithValueTransformer(
                    getter: (_, layer, localValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            return ConfigValue<WaveQualityMode>.Set((WaveQualityMode)Main.WaveQuality);
                        }

                        return localValue;
                    },
                    setter: (_, layer, ref storedValue, newValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            Main.WaveQuality = (int)newValue.Value;
                        }

                        storedValue = newValue;
                    }
               )
               .WithSerialization(
                    serializer: (_, _) => null,
                    deserializer: (entry, _) => entry.GetLayerValue(ConfigValueLayer.Default)
               )
               .WithCategories(Category)
               .Register(Config, Mod);
#endregion
    }

    public static class Volume
    {
        public static ConfigCategoryHandle Category { get; } =
            ConfigCategory
               .Define()
               .Register(Config, Mod, nameof(Volume));

        // Music Volume - 0-100%
        // Sound Volume - 0-100%
        // Ambient Volume - 0-100%

        // TODO: Localization

        // Will need to display these as percentages.

        // Min 0f
        // Max 1f
        // LegacyMenu.99/GameUI.Music
        public static ConfigEntry<float> MusicVolume { get; } =
            Define(() => ref Main.musicVolume)
               .WithCategories(Category)
               .Register(Config, Mod);

        // Min 0f
        // Max 1f
        // LegacyMenu.98
        public static ConfigEntry<float> SoundVolume { get; } =
            Define(() => ref Main.soundVolume)
               .WithCategories(Category)
               .Register(Config, Mod);

        // Min 0f
        // Max 1f
        // LegacyMenu.119
        public static ConfigEntry<float> AmbientVolume { get; } =
            Define(() => ref Main.ambientVolume)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Cursor
    {
        public static ConfigCategoryHandle Category { get; } =
            ConfigCategory
               .Define()
               .WithDisplayName(Lang.menu[218]) // Cursor
               .Register(Config, Mod, nameof(Cursor));

        // Smart Cursor Usage - Hold, Toggle
        // Smart Cursor Priority - Pickaxe to Axe, Axe to Pickaxe
        // Smart Cursor Block Placement - On/Off
        // Cursor Color - Color
        // Cursor Outline Color - Color
        // Lock-on Priority - FocusTarget, TargetClosest, ThreeDS

        // TODO: Localization

#region Smart Cursor
        public enum SmartCursorUsageMode : int
        {
            Hold,
            Toggle
        }

        // LegacyMenu.121/LegacyMenu.122
        public static ConfigEntry<SmartCursorUsageMode> SmartCursorUsage { get; } =
            ConfigEntry<SmartCursorUsageMode>
               .Define()
               .WithConfigSide(ConfigSide.NoSync)
               .WithValueTransformer(
                    getter: (_, layer, localValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            return ConfigValue<SmartCursorUsageMode>.Set((SmartCursorUsageMode)Main.cSmartCursorModeIsToggleAndNotHold.ToInt());
                        }

                        return localValue;
                    },
                    setter: (_, layer, ref storedValue, newValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            Main.cSmartCursorModeIsToggleAndNotHold = (int)newValue.Value >= 1;
                        }

                        storedValue = newValue;
                    }
               )
               .WithSerialization(
                    serializer: (_, _) => null,
                    deserializer: (entry, _) => entry.GetLayerValue(ConfigValueLayer.Default)
               )
               .WithCategories(Category)
               .Register(Config, Mod);

        public enum SmartCursorPriorityMode : int
        {
            PickaxeToAxe,
            AxeToPickaxe,
        }

        //LegacyMenu.213/LegacyMenu.214
        public static ConfigEntry<SmartCursorPriorityMode> SmartCursorPriority { get; } =
            ConfigEntry<SmartCursorPriorityMode>
               .Define()
               .WithConfigSide(ConfigSide.NoSync)
               .WithValueTransformer(
                    getter: (_, layer, localValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            return ConfigValue<SmartCursorPriorityMode>.Set((SmartCursorPriorityMode)Player.SmartCursorSettings.SmartAxeAfterPickaxe.ToInt());
                        }

                        return localValue;
                    },
                    setter: (_, layer, ref storedValue, newValue) =>
                    {
                        if (layer == ConfigValueLayer.User)
                        {
                            Player.SmartCursorSettings.SmartAxeAfterPickaxe = (int)newValue.Value >= 1;
                        }

                        storedValue = newValue;
                    }
               )
               .WithSerialization(
                    serializer: (_, _) => null,
                    deserializer: (entry, _) => entry.GetLayerValue(ConfigValueLayer.Default)
               )
               .WithCategories(Category)
               .Register(Config, Mod);

        //LegacyMenu.215/LegacyMenu.216
        public static ConfigEntry<bool> SmartBlockPlacement { get; } =
            Define(() => ref Player.SmartCursorSettings.SmartBlocksEnabled)
               .WithCategories(Category)
               .Register(Config, Mod);
#endregion

#region Cursor Color
        public static ConfigEntry<Color> CursorColor { get; } =
            Define(() => ref Main.cursorColor)
               .WithCategories(Category)
               .WithDisplayName(Lang.menu[64]) // Cursor Color
               .Register(Config, Mod);

        public static ConfigEntry<Color> CursorOutlineColor { get; } =
            Define(() => ref Main.MouseBorderColor)
               .WithCategories(Category)
               .WithDisplayName(Lang.menu[217]) // Border Color
               .Register(Config, Mod);
        #endregion

        // LegacyMenu.232/LegacyMenu.233/LegacyMenu.234
        public static ConfigEntry<LockOnHelper.LockOnMode> LockOnPriority { get; } =
            Define(() => ref LockOnHelper.UseMode)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Controls
    {
        public static ConfigCategoryHandle Category { get; } =
            ConfigCategory
               .Define()
               .Register(Config, Mod, nameof(Controls));

        // ??? Activate Set Bonuses: {}
        // ??? Quick Trash: {}
        // Keybindings sub-menu
        // - See about porting 1:1 as best as possible

        public static ConfigEntry<bool> LoadStub5 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Language
    {
        public static ConfigCategoryHandle Category { get; } =
            ConfigCategory
               .Define()
               .Register(Config, Mod, nameof(Language) + "Category");

        // Generic selection menu we can reimplement.
        // See about compatibility with other mods?

        public static ConfigEntry<bool> LoadStub6 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }
}

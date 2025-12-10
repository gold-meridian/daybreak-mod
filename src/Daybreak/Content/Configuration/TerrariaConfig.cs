using System;
using Daybreak.Common.Features.Configuration;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Content.Configuration;

internal static class TerrariaConfig
{
    private static Mod? Mod => null;

    private static ConfigRepository Config => ConfigRepository.Default;

    private static ConfigEntryDescriptor<T> Define<T>(ConfigEntryDescriptor<T>.RefProvider value)
    {
        return new ConfigEntryDescriptor<T>()
               // These values are handled separately and should not be synced
               // by us.
              .WithConfigSide(ConfigSide.NoSync)
               // These values already exist elsewhere, so we just need to
               // provide a way to get and set them.
              .WithLocalValueProvider(value)
               // These values are saved externally, and we should never have to
               // read or write them.
              .WithSerialization(
                   serializer: (_, _, _) => null,
                   deserializer: (entry, _, _) => entry.LocalValue
               );
    }

    public static class General
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(General));

        // bool Autosave On/Off
        /*
                int num22 = 0;
                if (autoSave)
                    array9[num22] = Lang.menu[67].Value;
                else
                    array9[num22] = Lang.menu[68].Value;

                if (selectedMenu == num22) {
                    SoundEngine.PlaySound(12);
                    if (autoSave)
                        autoSave = false;
                    else
                        autoSave = true;
                }
         */
        public static ConfigEntry<bool> Autosave { get; } =
            Define(() => ref Main.autoSave)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Autopause On/Off
        /*
                num22++;
                if (autoPause)
                    array9[num22] = Lang.menu[69].Value;
                else
                    array9[num22] = Lang.menu[70].Value;

                if (selectedMenu == num22) {
                    SoundEngine.PlaySound(12);
                    if (autoPause)
                        autoPause = false;
                    else
                        autoPause = true;
                }
         */
        public static ConfigEntry<bool> Autopause { get; } =
            Define(() => ref Main.autoPause)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Map Enabled/Disabled
        /*
                num22++;
                if (mapEnabled)
                    array9[num22] = Lang.menu[112].Value;
                else
                    array9[num22] = Lang.menu[113].Value;

                if (selectedMenu == num22) {
                    SoundEngine.PlaySound(12);
                    if (mapEnabled)
                        mapEnabled = false;
                    else
                        mapEnabled = true;
                }
         */
        public static ConfigEntry<bool> MapEnabled { get; } =
            Define(() => ref Main.mapEnabled)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Passwords: Visible/Hidden
        /*

                num22++;
                array9[num22] = (HidePassword ? Lang.menu[212].Value : Lang.menu[211].Value);
                if (selectedMenu == num22) {
                    SoundEngine.PlaySound(12);
                    HidePassword = !HidePassword;
                }

                num22++;
                array9[num22] = Lang.menu[5].Value;
                if (selectedMenu == num22 || flag5) {
                    flag5 = false;
                    menuMode = 11;
                    SoundEngine.PlaySound(11);
                }
         */
        public static ConfigEntry<bool> HidePassword { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Interface
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Interface));

        // bool Pickup Text On/Off
        // bool Event Progress Bar On/Off
        // bool Placement Preview On/Off
        // bool Highlight New Items On/Off
        // bool Tile Grid On/Off
        // bool Gamepad Instructions On/Off
        // ??? Minimap Border: {}
        // ??? Health and Mana Style: {}
        // bool Boss Health Bar Numbers: On/Off
        // ??? Boss bar Style: {}

        public static ConfigEntry<bool> LoadStub1 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Video
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Video));

        // Resolution sub-menu
        // - Fullscreen resolution selection
        // - Fullscreen/Windowed
        // Parallax sub-menu
        // - Parallax slider (0-100)
        // bool Frame Skip On/Off
        // ??? Lighting: {}
        // ??? Quality: {}
        // bool Background On/Off
        // bool Blood and Gore On/Off
        // bool Miner's Wobble: Enabled/Disabled
        // bool Windy Environment: Enabled/Disabled

        public static ConfigEntry<bool> LoadStub2 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Volume
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Volume));

        // Music: 0%-100%
        // Sound: 0%-100%
        // Ambient: 0%-100%

        public static ConfigEntry<bool> LoadStub3 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Cursor
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Cursor));

        // Cursor Color sub-menu
        // - various color sliders
        // Border Color sub-menu
        // - various color sliders
        // ??? Smart Cursor Mode: {}
        // ??? Smart Cursor Priority: {}
        // bool Smart Block Placement: Enabled/Disabled
        // ??? Lock On Priority: {}

        public static ConfigEntry<bool> LoadStub4 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }

    public static class Controls
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
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
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Language) + "Category");

        // Generic selection menu we can reimplement.
        // See about compatibility with other mods?

        public static ConfigEntry<bool> LoadStub6 { get; } =
            Define(() => ref Main.HidePassword)
               .WithCategories(Category)
               .Register(Config, Mod);
    }
}

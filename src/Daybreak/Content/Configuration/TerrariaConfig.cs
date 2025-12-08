using Daybreak.Common.Features.Configuration;
using Terraria.ModLoader;

namespace Daybreak.Content.Configuration;

internal static class TerrariaConfig
{
    private static Mod? Mod => null;

    private static ConfigRepository Config => ConfigRepository.Default;

    public static class General
    {
        public static ConfigCategory Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(General));

        // bool Autosave On/Off
        // bool Autopause On/Off
        // bool Map Enabled/Disabled
        // bool Passwords: Visible/Hidden
    }

    public static class Interface
    {
        public static ConfigCategory Category { get; } =
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
    }

    public static class Video
    {
        public static ConfigCategory Category { get; } =
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
    }

    public static class Volume
    {
        public static ConfigCategory Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Volume));

        // Music: 0%-100%
        // Sound: 0%-100%
        // Ambient: 0%-100%
    }

    public static class Cursor
    {
        public static ConfigCategory Category { get; } =
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
    }

    public static class Controls
    {
        public static ConfigCategory Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Controls));

        // ??? Activate Set Bonuses: {}
        // ??? Quick Trash: {}
        // Keybindings sub-menu
        // - See about porting 1:1 as best as possible
    }

    public static class Language
    {
        public static ConfigCategory Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(Language) + "Category");

        // Generic selection menu we can reimplement.
        // See about compatibility with other mods?
    }
}

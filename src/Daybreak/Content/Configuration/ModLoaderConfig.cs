using Daybreak.Common.Features.Configuration;
using Terraria.ModLoader;

namespace Daybreak.Content.Configuration;

internal static class ModLoaderConfig
{
    private static Mod Mod => ModLoader.GetMod("ModLoaderMod");

    private static ConfigRepository Config => ConfigRepository.Default;

    public static class General
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(General));

        // bool Download Mods From Servers: On/Off
        // ModNet.downloadModsFromServers
        public static ConfigEntry<bool> DownloadModsFromServers { get; } =
            new ConfigEntryDescriptor<bool>()
               .WithCategories(Category)
               .Register(Config, Mod, nameof(DownloadModsFromServers));

        // bool Automatically Reload Required Mods When Leaving Mods Screen: On/Off
        // ModLoader.autoReloadRequiredModsLeavingModsScreen
        public static ConfigEntry<bool> AutoReloadRequiredModsLeavingModsScreen { get; } =
            new ConfigEntryDescriptor<bool>()
               .WithCategories(Category)
               .Register(Config, Mod, nameof(AutoReloadRequiredModsLeavingModsScreen));

        // bool Remove Forced Minimum Zoom: On/Off
        // ModLoader.removeForcedMinimumZoom
        public static ConfigEntry<bool> RemoveForcedMinimumZoom { get; } =
            new ConfigEntryDescriptor<bool>()
               .WithCategories(Category)
               .Register(Config, Mod, nameof(RemoveForcedMinimumZoom));

        // ??? Attack Speed Effect Tooltips: {}
        // ModLoader.attackSpeedScalingTooltipVisibility
        public static ConfigEntry<int> AttackSpeedScalingTooltipVisibility { get; } =
            new ConfigEntryDescriptor<int>()
               .WithCategories(Category)
               .Register(Config, Mod, nameof(AttackSpeedScalingTooltipVisibility));

        // bool Notify When a New Main Menu Theme Is Unlocked: On/Off
        // ModLoader.notifyNewMainMenuThemes
        public static ConfigEntry<bool> NotifyNewMainMenuThemes { get; } =
            new ConfigEntryDescriptor<bool>()
               .WithCategories(Category)
               .Register(Config, Mod, nameof(NotifyNewMainMenuThemes));

        // bool Show Which Workshop Mods Updated Since Last Launch: On/Off
        // ModLoader.showNewUpdatedModsInfo
        public static ConfigEntry<bool> ShowNewUpdatedModsInfo { get; } =
            new ConfigEntryDescriptor<bool>()
               .WithCategories(Category)
               .Register(Config, Mod, nameof(ShowNewUpdatedModsInfo));

        // bool Show Confirmation Window For Enable/Disable All Mods: On/Off
        // ModLoader.showConfirmationWindowWhenEnableDisableAllMods
        public static ConfigEntry<bool> ShowConfirmationWindowWhenEnableDisableAllMods { get; } =
            new ConfigEntryDescriptor<bool>()
               .WithCategories(Category)
               .Register(Config, Mod, nameof(ShowConfirmationWindowWhenEnableDisableAllMods));
    }
}

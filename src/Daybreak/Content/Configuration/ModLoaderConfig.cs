using Daybreak.Common.Features.Configuration;
using Terraria.ModLoader;

namespace Daybreak.Content.Configuration;

internal static class ModLoaderConfig
{
    private static Mod Mod => ModLoader.GetMod("ModLoaderMod");

    private static ConfigRepository Config => ConfigRepository.Default;

    private static ConfigEntryDescriptor<T> Define<T>(ConfigEntryDescriptor<T>.RefProvider value)
    {
        return new ConfigEntryDescriptor<T>()
              .WithConfigSide(ConfigSide.NoSync)
              .WithLocalValueProvider(value);
    }

    public static class General
    {
        public static ConfigCategoryHandle Category { get; } =
            new ConfigCategoryDescriptor()
               .Register(Config, Mod, nameof(General));

        // bool Download Mods From Servers: On/Off
        public static ConfigEntry<bool> DownloadModsFromServers { get; } =
            Define(() => ref ModNet.downloadModsFromServers)
               .WithCategories(Category)
               .Register(Config, Mod, nameof(DownloadModsFromServers));

        // bool Automatically Reload Required Mods When Leaving Mods Screen: On/Off
        public static ConfigEntry<bool> AutoReloadRequiredModsLeavingModsScreen { get; } =
            Define(() => ref ModLoader.autoReloadRequiredModsLeavingModsScreen)
               .WithCategories(Category)
               .Register(Config, Mod, nameof(AutoReloadRequiredModsLeavingModsScreen));

        // bool Remove Forced Minimum Zoom: On/Off
        public static ConfigEntry<bool> RemoveForcedMinimumZoom { get; } =
            Define(() => ref ModLoader.removeForcedMinimumZoom)
               .WithCategories(Category)
               .Register(Config, Mod, nameof(RemoveForcedMinimumZoom));

        // ??? Attack Speed Effect Tooltips: {}
        public static ConfigEntry<int> AttackSpeedScalingTooltipVisibility { get; } =
            Define(() => ref ModLoader.attackSpeedScalingTooltipVisibility)
               .WithCategories(Category)
               .Register(Config, Mod, nameof(AttackSpeedScalingTooltipVisibility));

        // bool Notify When a New Main Menu Theme Is Unlocked: On/Off
        public static ConfigEntry<bool> NotifyNewMainMenuThemes { get; } =
            Define(() => ref ModLoader.notifyNewMainMenuThemes)
               .WithCategories(Category)
               .Register(Config, Mod, nameof(NotifyNewMainMenuThemes));

        // bool Show Which Workshop Mods Updated Since Last Launch: On/Off
        public static ConfigEntry<bool> ShowNewUpdatedModsInfo { get; } =
            Define(() => ref ModLoader.showNewUpdatedModsInfo)
               .WithCategories(Category)
               .Register(Config, Mod, nameof(ShowNewUpdatedModsInfo));

        // bool Show Confirmation Window For Enable/Disable All Mods: On/Off
        public static ConfigEntry<bool> ShowConfirmationWindowWhenEnableDisableAllMods { get; } =
            Define(() => ref ModLoader.showConfirmationWindowWhenEnableDisableAllMods)
               .WithCategories(Category)
               .Register(Config, Mod, nameof(ShowConfirmationWindowWhenEnableDisableAllMods));
    }
}

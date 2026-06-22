using Daybreak.Common.Features.Configuration;
using Terraria.ModLoader;

namespace Daybreak.Content.Configuration;

internal static class ModLoaderConfig
{
    private static Mod Mod => ModLoader.GetMod("ModLoader");

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
                // .WithIcon(c => Assets.Images.Configuration.GeneraltModLoaderIcon.Asset)
               .Register(Config, Mod, nameof(General));

        // bool Download Mods From Servers: On/Off
        public static ConfigEntry<bool> DownloadModsFromServers { get; } =
            Define(() => ref ModNet.downloadModsFromServers)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Automatically Reload Required Mods When Leaving Mods Screen: On/Off
        public static ConfigEntry<bool> AutoReloadRequiredModsLeavingModsScreen { get; } =
            Define(() => ref ModLoader.autoReloadRequiredModsLeavingModsScreen)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Remove Forced Minimum Zoom: On/Off
        public static ConfigEntry<bool> RemoveForcedMinimumZoom { get; } =
            Define(() => ref ModLoader.removeForcedMinimumZoom)
               .WithCategories(Category)
               .Register(Config, Mod);

        // ??? Attack Speed Effect Tooltips: {}
        public static ConfigEntry<int> AttackSpeedScalingTooltipVisibility { get; } =
            Define(() => ref ModLoader.attackSpeedScalingTooltipVisibility)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Notify When a New Main Menu Theme Is Unlocked: On/Off
        public static ConfigEntry<bool> NotifyNewMainMenuThemes { get; } =
            Define(() => ref ModLoader.notifyNewMainMenuThemes)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Show Which Workshop Mods Updated Since Last Launch: On/Off
        public static ConfigEntry<bool> ShowNewUpdatedModsInfo { get; } =
            Define(() => ref ModLoader.showNewUpdatedModsInfo)
               .WithCategories(Category)
               .Register(Config, Mod);

        // bool Show Confirmation Window For Enable/Disable All Mods: On/Off
        public static ConfigEntry<bool> ShowConfirmationWindowWhenEnableDisableAllMods { get; } =
            Define(() => ref ModLoader.showConfirmationWindowWhenEnableDisableAllMods)
               .WithCategories(Category)
               .Register(Config, Mod);
    }
}

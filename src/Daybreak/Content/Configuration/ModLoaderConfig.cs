using Daybreak.Common.Features.Configuration;
using Terraria.ModLoader;

namespace Daybreak.Content.Configuration;

internal static class ModLoaderConfig
{
    private static Mod Mod => ModLoader.GetMod("ModLoaderMod");

    private static ConfigRepository Config => ConfigRepository.Default;

    public static class General
    {
        public static ConfigCategoryHandle Category { get; } = Config.RegisterCategory(Mod, nameof(General));
        
        // bool Download Mods From Servers: On/Off
        // bool Automatically Reload Required Mods When Leaving Mods Screen: On/Off
        // bool Remove Forced Minimum Zoom: On/Off
        // ??? Attack Speed Effect Tooltips: {}
        // bool Notify When a New Main Menu Theme Is Unlocked: On/Off
        // bool Show Which Workshop Mods Updated Since Last Launch: On/Off
        // bool Show Confirmation Window For Enable/Disable All Mods: On/Off
    }
}

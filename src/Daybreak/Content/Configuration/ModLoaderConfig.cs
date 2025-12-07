using Daybreak.Common.Features.Configuration;
using Terraria.ModLoader;

namespace Daybreak.Content.Configuration;

internal static class ModLoaderConfig
{
    private static Mod ModLoaderMod => ModLoader.GetMod("ModLoaderMod");

    public static ConfigCategoryHandle CategoryGeneral { get; } = ConfigSystem.RegisterCategory(ModLoaderMod, "General");
}

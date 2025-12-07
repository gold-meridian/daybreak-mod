using Daybreak.Common.Features.Configuration;

namespace Daybreak.Content.Configuration;

internal static class TerrariaConfig
{
    public static ConfigCategoryHandle CategoryGeneral { get; } = ConfigSystem.RegisterCategory(null, "General");

    public static ConfigCategoryHandle CategoryInterface { get; } = ConfigSystem.RegisterCategory(null, "Interface");

    public static ConfigCategoryHandle CategoryVideo { get; } = ConfigSystem.RegisterCategory(null, "Video");

    public static ConfigCategoryHandle CategoryVolume { get; } = ConfigSystem.RegisterCategory(null, "Volume");

    public static ConfigCategoryHandle CategoryCursor { get; } = ConfigSystem.RegisterCategory(null, "Cursor");

    public static ConfigCategoryHandle CategoryControls { get; } = ConfigSystem.RegisterCategory(null, "Controls");

    public static ConfigCategoryHandle CategoryLanguage { get; } = ConfigSystem.RegisterCategory(null, "Language");
}

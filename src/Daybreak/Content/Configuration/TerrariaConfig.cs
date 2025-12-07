using Daybreak.Common.Features.Configuration;

namespace Daybreak.Content.Configuration;

internal static class TerrariaConfig
{
    public static ConfigCategoryHandle CategoryGeneral { get; } = ConfigSystem.CreateCategory(null, "General");

    public static ConfigCategoryHandle CategoryInterface { get; } = ConfigSystem.CreateCategory(null, "Interface");

    public static ConfigCategoryHandle CategoryVideo { get; } = ConfigSystem.CreateCategory(null, "Video");

    public static ConfigCategoryHandle CategoryVolume { get; } = ConfigSystem.CreateCategory(null, "Volume");

    public static ConfigCategoryHandle CategoryCursor { get; } = ConfigSystem.CreateCategory(null, "Cursor");

    public static ConfigCategoryHandle CategoryControls { get; } = ConfigSystem.CreateCategory(null, "Controls");

    public static ConfigCategoryHandle CategoryLanguage { get; } = ConfigSystem.CreateCategory(null, "Language");
}

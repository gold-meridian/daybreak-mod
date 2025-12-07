using Daybreak.Common.Features.Configuration;

namespace Daybreak.Content.Configuration;

internal static class TerrariaConfig
{
    public static class General
    {
        public static ConfigCategoryHandle Category { get; } = ConfigSystem.RegisterCategory(null, nameof(General));
    }

    public static class Interface
    {
        public static ConfigCategoryHandle Category { get; } = ConfigSystem.RegisterCategory(null, nameof(Interface));
    }

    public static class Video
    {
        public static ConfigCategoryHandle Category { get; } = ConfigSystem.RegisterCategory(null, nameof(Video));
    }

    public static class Volume
    {
        public static ConfigCategoryHandle Category { get; } = ConfigSystem.RegisterCategory(null, nameof(Volume));
    }

    public static class Cursor
    {
        public static ConfigCategoryHandle Category { get; } = ConfigSystem.RegisterCategory(null, nameof(Cursor));
    }

    public static class Controls
    {
        public static ConfigCategoryHandle Category { get; } = ConfigSystem.RegisterCategory(null, nameof(Controls));
    }

    public static class Language
    {
        public static ConfigCategoryHandle Category { get; } = ConfigSystem.RegisterCategory(null, nameof(Language));
    }
}

using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     Uniquely identifies a config category.
///     <br />
///     Handles may be freely shared across mods without referencing assemblies
///     of other mods, and can be used to inspect the status of a category
///     without requiring it be registered.
/// </summary>
public readonly record struct ConfigCategoryHandle(
    ConfigRepository Repository,
    Mod? Mod,
    string Name
)
{
    /// <summary>
    ///     The config repository that should own this category.
    /// </summary>
    public ConfigRepository Repository { get; } = Repository;

    /// <summary>
    ///     The mod this category belongs to.  If the mod is
    ///     <see langword="null"/>, then this category is considered as belonging
    ///     to vanilla.
    /// </summary>
    public Mod? Mod { get; } = Mod;

    /// <summary>
    ///     The unique key which identifies this category (sub-categorized with
    ///     <see cref="Mod"/>).
    ///     <br />
    ///     This key should <b>not</b> contain the mod name. The key needs to
    ///     only be unique when compared against other keys in the same mod.
    /// </summary>
    public string Name { get; } = Name;
}

/// <summary>
///     Represents a config category, which is akin to a base
///     <see cref="ModConfig"/> definition.  Mods may have top-level categories
///     that serve as individual pages, and entries may be added to any number
///     of category pages.
/// </summary>
public sealed class ConfigCategory : ILocalizedModType
{
    /// <summary>
    ///     The config category handle which may be used to uniquely identify
    ///     this category and obtain it as necessary.
    /// </summary>
    public ConfigCategoryHandle Id { get; }

    /// <summary>
    ///     The display name of this category.
    /// </summary>
    public LocalizedText DisplayName { get; }

    string ILocalizedModType.LocalizationCategory => "ConfigCategory";

    Mod? IModType.Mod => Id.Mod;

    string IModType.Name => Id.Name;

    string IModType.FullName => $"{ConfigRepository.GetModName(Id.Mod)}.{Id.Name}";

    internal ConfigCategory(ConfigCategoryHandle handle, LocalizedText? displayName)
    {
        Id = handle;
        DisplayName = displayName ?? this.GetLocalization(nameof(DisplayName), () => Id.Name);
    }
}

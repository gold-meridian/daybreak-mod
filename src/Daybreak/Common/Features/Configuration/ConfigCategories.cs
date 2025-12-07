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
public readonly struct ConfigCategoryHandle
{
    /// <summary>
    ///     The mod this category belongs to.  If the mod is
    ///     <see langword="null"/>, then this category is considered as belonging
    ///     to vanilla.
    /// </summary>
    public Mod? Mod { get; private init; }

    /// <summary>
    ///     The unique key which identifies this category (sub-categorized with
    ///     <see cref="Mod"/>).
    ///     <br />
    ///     This key should <b>not</b> contain the mod name. The key needs to
    ///     only be unique when compared against other keys in the same mod.
    /// </summary>
    public string Name { get; private init; } = string.Empty;

    internal ConfigCategoryHandle(Mod? mod, string name)
    {
        Mod = mod;
        Name = name;
    }
}

/// <summary>
///     Represents a config category, which is akin to a base
///     <see cref="ModConfig"/> definition.  Mods may have top-level categories
///     that serve as individual pages, and entries may be added to any number
///     of category pages.
/// </summary>
public interface IConfigCategory : ILocalizedModType
{
    /// <summary>
    ///     The config category handle which may be used to uniquely identify
    ///     this category and obtain it as necessary.
    /// </summary>
    ConfigCategoryHandle Id { get; }

    /// <summary>
    ///     The display name of this category.
    /// </summary>
    LocalizedText DisplayName { get; }
}
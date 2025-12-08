using System;
using System.Runtime.CompilerServices;
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
public sealed class ConfigCategory(
    ConfigCategoryHandle handle,
    ConfigCategoryDescriptor descriptor
)
{
    /// <summary>
    ///     The config category handle which may be used to uniquely identify
    ///     this category and obtain it as necessary.
    /// </summary>
    public ConfigCategoryHandle Handle { get; } = handle;

    /// <summary>
    ///     The descriptor which dictates the behavior of this category.
    /// </summary>
    public ConfigCategoryDescriptor Descriptor { get; } = descriptor;

    /// <summary>
    ///     The display name of this category.
    /// </summary>
    public LocalizedText DisplayName { get; } =
        descriptor.DisplayNameProvider?.Invoke()
     ?? LanguageHelpers.GetLocalization(handle.Mod, nameof(ConfigCategory), nameof(DisplayName), () => handle.Name);

    /// <summary>
    ///     Derives the handle of this category.
    /// </summary>
    public static implicit operator ConfigCategoryHandle(ConfigCategory category)
    {
        return category.Handle;
    }
}

/// <summary>
///     A descriptor for dynamically constructing a
///     <see cref="ConfigCategory"/> with arbitrary behavior.
/// </summary>
public sealed class ConfigCategoryDescriptor
{
    /// <summary>
    ///     Provides the display name of the category.
    /// </summary>
    public Func<LocalizedText>? DisplayNameProvider { get; set; }

    /// <summary>
    ///     Provides the display name of the category.
    /// </summary>
    public ConfigCategoryDescriptor WithDisplayName(Func<LocalizedText>? displayNameProvider)
    {
        DisplayNameProvider = displayNameProvider;
        return this;
    }

    /// <summary>
    ///     Provides the display name of the category.
    /// </summary>
    public ConfigCategoryDescriptor WithDisplayName(LocalizedText displayName)
    {
        DisplayNameProvider = () => displayName;
        return this;
    }

    /// <summary>
    ///     Creates a category and registers the category to the repository.
    /// </summary>
    public ConfigCategory Register(
        ConfigRepository repository,
        Mod? mod,
        [CallerMemberName] string uniqueKey = ""
    )
    {
        return repository.RegisterCategory(
            new ConfigCategory(
                new ConfigCategoryHandle(repository, mod, uniqueKey),
                this
            )
        );
    }
}

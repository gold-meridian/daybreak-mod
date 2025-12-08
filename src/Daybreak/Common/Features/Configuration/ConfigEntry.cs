using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     Uniquely identifies a config entry.
///     <br />
///     Handles may be freely shared across mods without referencing assemblies
///     of other mods, and can be used to inspect the status of an entry without
///     requiring it be registered.
/// </summary>
public readonly record struct ConfigEntryHandle(
    ConfigRepository Repository,
    Mod? Mod,
    string Name
)
{
    /// <summary>
    ///     The config repository that should own this entry.
    /// </summary>
    public ConfigRepository Repository { get; } = Repository;

    /// <summary>
    ///     The mod this entry belongs to.  If the mod is
    ///     <see langword="null"/>, then this entry is considered as belonging
    ///     to vanilla.
    /// </summary>
    public Mod? Mod { get; } = Mod;

    /// <summary>
    ///     The unique key which identifies this entry (sub-categorized with
    ///     <see cref="Mod"/>).
    ///     <br />
    ///     This key should <b>not</b> contain the mod name. The key needs to
    ///     only be unique when compared against other keys in the same mod.
    /// </summary>
    public string Name { get; } = Name;
}

/// <summary>
///     The type-generic config entry contract.
/// </summary>
public interface IConfigEntry
{
    /// <summary>
    ///     The config entry handle which may be used to uniquely identify this
    ///     entry and obtain it as necessary.
    /// </summary>
    ConfigEntryHandle Handle { get; }

    /// <summary>
    ///     This entry's side, which controls syncing and which version of the
    ///     config to use and display.
    /// </summary>
    ConfigSide Side { get; }

    /// <summary>
    ///     The type this entry stores.
    /// </summary>
    Type ValueType { get; }

    /// <summary>
    ///     The actual value being worked with.  Depending on the game context,
    ///     we either use <see cref="LocalValue"/> or <see cref="RemoteValue"/>
    ///     as its backing property.  Mutating this value with modify either the
    ///     local or remote value according to the context.
    ///     <br />
    ///     On servers, the remote and local value are always the same.
    /// </summary>
    object? Value { get; set; }

    /// <summary>
    ///     The local (client-sided) value.
    ///     <br />
    ///     On servers, this is the same as <see cref="RemoteValue"/>.
    /// </summary>
    object? LocalValue { get; set; }

    /// <summary>
    ///     The remote (server-sided) value.
    ///     <br />
    ///     On servers, this is the same as <see cref="LocalValue"/>.
    /// </summary>
    object? RemoteValue { get; set; }

    /// <summary>
    ///     The default value.
    /// </summary>
    object? DefaultValue { get; set; }

    /// <summary>
    ///     The display name of this config entry.
    /// </summary>
    LocalizedText DisplayName { get; }

    /// <summary>
    ///     The short-form tooltip briefly summarizing this entry, to be
    ///     displayed next to the cursor on hover.
    /// </summary>
    LocalizedText Tooltip { get; }

    /// <summary>
    ///     The long-form description which may be rendered at the bottom of a
    ///     config UI.
    /// </summary>
    LocalizedText Description { get; }

    /// <summary>
    ///     The categories this entry belongs to.  The first category is the
    ///     &quot;main&quot; category to which this entry canonically belongs
    ///     to.
    /// </summary>
    /// <remarks>
    ///     There must be <b>at least 1</b> category.
    /// </remarks>
    ConfigCategoryHandle[] Categories { get; }

    /// <summary>
    ///     The main category of this entry, derived from the first item in
    ///     <see cref="Categories"/>.
    /// </summary>
    ConfigCategoryHandle MainCategory => Categories[0];

    /// <summary>
    ///     Called when this entry is registered.
    /// </summary>
    void OnRegister();
}

/// <summary>
///     The type-safe implementation of <see cref="IConfigEntry"/>.
/// </summary>
public sealed class ConfigEntry<T>(ConfigEntryHandle handle, ConfigEntryDescriptor<T> descriptor) : IConfigEntry
{
    Type IConfigEntry.ValueType => typeof(T);

    object? IConfigEntry.Value
    {
        get => Value;
        set => Value = (T?)value;
    }

    object? IConfigEntry.LocalValue
    {
        get => LocalValue;
        set => LocalValue = (T?)value;
    }

    object? IConfigEntry.RemoteValue
    {
        get => RemoteValue;
        set => RemoteValue = (T?)value;
    }

    object? IConfigEntry.DefaultValue
    {
        get => DefaultValue;
        set => DefaultValue = (T?)value;
    }

    /// <inheritdoc />
    public ConfigEntryHandle Handle { get; } = handle;

    /// <summary>
    ///     The descriptor which dictates the behavior of this entry.
    /// </summary>
    public ConfigEntryDescriptor<T> Descriptor { get; } = descriptor;

    /// <inheritdoc />
    public ConfigSide Side { get; }
        = descriptor.ConfigSideProvider?.Invoke()
       ?? ConfigSide.Both;

    /// <inheritdoc />
    public LocalizedText DisplayName { get; } =
        descriptor.DisplayNameProvider?.Invoke()
     ?? LanguageHelpers.GetLocalization(handle.Mod, nameof(ConfigEntry<>), nameof(DisplayName), () => handle.Name);

    /// <inheritdoc />
    public LocalizedText Tooltip { get; } =
        descriptor.DisplayNameProvider?.Invoke()
     ?? LanguageHelpers.GetLocalization(handle.Mod, nameof(ConfigEntry<>), nameof(Tooltip), () => "");

    /// <inheritdoc />
    public LocalizedText Description { get; } =
        descriptor.DisplayNameProvider?.Invoke()
     ?? LanguageHelpers.GetLocalization(handle.Mod, nameof(ConfigEntry<>), nameof(Description), () => "");

    /// <inheritdoc />
    public ConfigCategoryHandle[] Categories { get; } =
        descriptor.CategoriesProvider?.Invoke().ToArray() is { Length: > 1 } categories
            ? categories
            : throw new InvalidOperationException("Config entries must be supplied at least one category: " + handle);

    public void OnRegister()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     The actual value being worked with.  Depending on the game context,
    ///     we either use <see cref="LocalValue"/> or <see cref="RemoteValue"/>
    ///     as its backing property.  Mutating this value with modify either the
    ///     local or remote value according to the context.
    ///     <br />
    ///     On servers, the remote and local value are always the same.
    /// </summary>
    public T? Value { get; set; }

    /// <summary>
    ///     The local (client-sided) value.
    ///     <br />
    ///     On servers, this is the same as <see cref="RemoteValue"/>.
    /// </summary>
    public T? LocalValue { get; set; }

    /// <summary>
    ///     The remote (server-sided) value.
    ///     <br />
    ///     On servers, this is the same as <see cref="LocalValue"/>.
    /// </summary>
    public T? RemoteValue { get; set; }

    /// <summary>
    ///     The default value.
    /// </summary>
    public T? DefaultValue { get; set; }
}

/// <summary>
///     A descriptor for dynamically constructing a
///     <see cref="ConfigEntry{T}"/> with arbitrary behavior.
/// </summary>
public sealed class ConfigEntryDescriptor<T>
{
    /// <summary>
    ///     Provides the display name of the entry.
    /// </summary>
    public Func<LocalizedText>? DisplayNameProvider { get; set; }

    /// <summary>
    ///     Provides the tooltip of the entry.
    /// </summary>
    public Func<LocalizedText>? TooltipProvider { get; set; }

    /// <summary>
    ///     Provides the description of the entry.
    /// </summary>
    public Func<LocalizedText>? DescriptionProvider { get; set; }

    /// <summary>
    ///     Provides the config side of the entry.
    /// </summary>
    public Func<ConfigSide>? ConfigSideProvider { get; set; }

    /// <summary>
    ///     Provides the categories of the entry.
    /// </summary>
    public Func<IEnumerable<ConfigCategoryHandle>>? CategoriesProvider { get; set; }

    /// <summary>
    ///     Provides the display name of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDisplayName(Func<LocalizedText>? displayNameProvider)
    {
        DisplayNameProvider = displayNameProvider;
        return this;
    }

    /// <summary>
    ///     Provides the display name of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDisplayName(LocalizedText displayName)
    {
        DisplayNameProvider = () => displayName;
        return this;
    }

    /// <summary>
    ///     Provides the display name of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithTooltip(Func<LocalizedText>? tooltipProvider)
    {
        TooltipProvider = tooltipProvider;
        return this;
    }

    /// <summary>
    ///     Provides the tooltip of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithTooltip(LocalizedText tooltip)
    {
        TooltipProvider = () => tooltip;
        return this;
    }

    /// <summary>
    ///     Provides the description of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDescription(Func<LocalizedText>? descriptionProvider)
    {
        DescriptionProvider = descriptionProvider;
        return this;
    }

    /// <summary>
    ///     Provides the description of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDescription(LocalizedText description)
    {
        DescriptionProvider = () => description;
        return this;
    }

    /// <summary>
    ///     Provides the config side of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithConfigSide(Func<ConfigSide>? configSideProvider)
    {
        ConfigSideProvider = configSideProvider;
        return this;
    }

    /// <summary>
    ///     Provides the config side of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithConfigSide(ConfigSide configSide)
    {
        ConfigSideProvider = () => configSide;
        return this;
    }

    /// <summary>
    ///     Provides the categories of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithCategories(params ConfigCategoryHandle[] categories)
    {
        CategoriesProvider = () => categories;
        return this;
    }

    /// <summary>
    ///     Creates an entry and registers the entry to the repository.
    /// </summary>
    public ConfigEntry<T> Register(ConfigRepository repository, Mod? mod, string uniqueKey)
    {
        return repository.RegisterEntry(
            new ConfigEntry<T>(
                new ConfigEntryHandle(repository, mod, uniqueKey),
                this
            )
        );
    }
}

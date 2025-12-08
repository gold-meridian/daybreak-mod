using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
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
    ///     The config value local to this instance.  Multiplayer clients,
    ///     singleplayer clients, and servers all have a local value.
    /// </summary>
    object? LocalValue { get; set; }

    /// <summary>
    ///     The config value supplied by a remote host.  Only used when
    ///     receiving a server-controlled value from a multiplayer client.
    /// </summary>
    object? RemoteValue { get; set; }

    /// <summary>
    ///     If an entry is queued to be modified, this value will be populated
    ///     with the new value.
    /// </summary>
    object? PendingValue { get; set; }

    /// <summary>
    ///     The default value.
    /// </summary>
    object? DefaultValue { get; }

    /// <summary>
    ///     Whether this entry's value does not match its pending value.
    /// </summary>
    bool Dirty { get; }

    /// <summary>
    ///     Whether a modification to this entry necessitates a reload.
    /// </summary>
    bool ReloadRequired { get; }

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
}

/// <summary>
///     The type-safe implementation of <see cref="IConfigEntry"/>.
/// </summary>
public sealed class ConfigEntry<T>(
    ConfigEntryHandle handle,
    ConfigEntryDescriptor<T> descriptor
) : IConfigEntry
    where T : IEquatable<T>
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

    object? IConfigEntry.PendingValue
    {
        get => PendingValue;
        set => PendingValue = (T?)value;
    }

    object? IConfigEntry.DefaultValue => DefaultValue;

    /// <inheritdoc />
    public ConfigEntryHandle Handle { get; } = handle;

    /// <summary>
    ///     The descriptor which dictates the behavior of this entry.
    /// </summary>
    public ConfigEntryDescriptor<T> Descriptor { get; } = descriptor;

    /// <inheritdoc />
    public ConfigSide Side =>
        Descriptor.ConfigSideProvider.Function?.Invoke(this)
     ?? ConfigSideFromModSide(Handle.Mod?.Side ?? ModSide.NoSync);

    /// <inheritdoc />
    public LocalizedText DisplayName =>
        Descriptor.DisplayNameProvider.Function?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(Handle.Mod, nameof(ConfigEntry<>), nameof(DisplayName), () => Handle.Name);

    /// <inheritdoc />
    public LocalizedText Tooltip =>
        Descriptor.TooltipProvider.Function?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(Handle.Mod, nameof(ConfigEntry<>), nameof(Tooltip), () => "");

    /// <inheritdoc />
    public LocalizedText Description =>
        Descriptor.DescriptionProvider.Function?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(Handle.Mod, nameof(ConfigEntry<>), nameof(Description), () => "");

    /// <inheritdoc />
    public ConfigCategoryHandle[] Categories =>
        Descriptor.CategoriesProvider.Function?.Invoke(this).ToArray() is { Length: > 1 } categories
            ? categories
            : throw new InvalidOperationException("Config entries must be supplied at least one category: " + Handle);

    /// <inheritdoc cref="IConfigEntry.Value"/>
    public T? Value
    {
        get
        {
            if (Side == ConfigSide.Both && Main.netMode == NetmodeID.MultiplayerClient)
            {
                return RemoteValue;
            }

            return LocalValue;
        }

        set => LocalValue = value;
    }

    /// <inheritdoc cref="IConfigEntry.LocalValue"/>
    public T? LocalValue
    {
        get => GetValue(field, Descriptor.LocalValueProvider?.Getter);
        set => field = SetValue(field, value, Descriptor.LocalValueProvider?.Setter);
    }

    /// <inheritdoc cref="IConfigEntry.RemoteValue"/>
    public T? RemoteValue
    {
        get => GetValue(field, Descriptor.RemoteValueProvider?.Getter);
        set => field = SetValue(field, value, Descriptor.RemoteValueProvider?.Setter);
    }

    /// <inheritdoc cref="IConfigEntry.PendingValue"/>
    public T? PendingValue
    {
        get => GetValue(field, Descriptor.PendingValueProvider?.Getter);
        set => field = SetValue(field, value, Descriptor.PendingValueProvider?.Setter);
    }

    // The compiler doesn't know how to lift a conditional access
    // (provider?.Invoke()) to a T? for some reason.
    /// <inheritdoc cref="IConfigEntry.DefaultValue"/>
    public T? DefaultValue =>
        Descriptor.DefaultValueProvider.Function is { } provider
            ? provider.Invoke(this)
            : default(T);

    /// <inheritdoc />
    public bool Dirty =>
        Descriptor.DirtiedProvider.Function?.Invoke(this) ?? !Equals(LocalValue, PendingValue);

    /// <inheritdoc />
    public bool ReloadRequired =>
        Descriptor.ReloadRequiredProvider.Function?.Invoke(this) ?? false;

    private T? GetValue(T? storedValue, ConfigEntryDescriptor<T>.Getter? getter)
    {
        if (getter is not null)
        {
            return getter(storedValue);
        }

        return storedValue;
    }

    private T? SetValue(T? storedValue, T? incomingValue, ConfigEntryDescriptor<T>.Setter? setter)
    {
        if (setter is not null)
        {
            return setter(storedValue, incomingValue);
        }

        return incomingValue;
    }

    private static ConfigSide ConfigSideFromModSide(ModSide modSide)
    {
        return modSide switch
        {
            ModSide.Both => ConfigSide.Both,
            ModSide.Client => ConfigSide.ClientSide,
            ModSide.Server => ConfigSide.ServerSide,
            ModSide.NoSync => ConfigSide.NoSync,
            _ => throw new ArgumentOutOfRangeException(nameof(modSide), modSide, null),
        };
    }
}

/// <summary>
///     A descriptor for dynamically constructing a
///     <see cref="ConfigEntry{T}"/> with arbitrary behavior.
/// </summary>
public sealed class ConfigEntryDescriptor<T>
    where T : IEquatable<T>
{
    /// <summary>
    ///     Represents a value provider.
    /// </summary>
    public readonly struct Provider<TValue>(Func<ConfigEntry<T>, TValue>? provider)
    {
        /// <summary>
        ///     The provider function.
        /// </summary>
        public Func<ConfigEntry<T>, TValue>? Function { get; } = provider;

        /// <inheritdoc />
        public Provider(Func<TValue> provider) : this(_ => provider()) { }

        /// <inheritdoc />
        public Provider(TValue value) : this(_ => value) { }

        /// <summary>
        ///     Creates a value provider.
        /// </summary>
        public static implicit operator Provider<TValue>(Func<ConfigEntry<T>, TValue> provider)
        {
            return new Provider<TValue>(provider);
        }

        /// <summary>
        ///     Creates a value provider.
        /// </summary>
        public static implicit operator Provider<TValue>(Func<TValue> provider)
        {
            return new Provider<TValue>(provider);
        }

        /// <summary>
        ///     Creates a value provider.
        /// </summary>
        public static implicit operator Provider<TValue>(TValue value)
        {
            return new Provider<TValue>(value);
        }
    }

    /// <summary>
    ///     Controls the get operation for a value.
    /// </summary>
    public delegate T? Getter(T? storedValue);

    /// <summary>
    ///     Controls the set operation for a value.
    /// </summary>
    public delegate T? Setter(T? storedValue, T? incomingValue);

    /// <summary>
    ///     Controls the get and set operations for a value by providing a
    ///     reference, convenient for completely ignoring the default,
    ///     entry-provided value when aliasing an existing field.
    /// </summary>
    public delegate ref T? RefProvider();

    /// <summary>
    ///     Provides the display name of the entry.
    /// </summary>
    public Provider<LocalizedText> DisplayNameProvider { get; set; }

    /// <summary>
    ///     Provides the tooltip of the entry.
    /// </summary>
    public Provider<LocalizedText> TooltipProvider { get; set; }

    /// <summary>
    ///     Provides the description of the entry.
    /// </summary>
    public Provider<LocalizedText> DescriptionProvider { get; set; }

    /// <summary>
    ///     Provides the config side of the entry.
    /// </summary>
    public Provider<ConfigSide> ConfigSideProvider { get; set; }

    /// <summary>
    ///     Provides the categories of the entry.
    /// </summary>
    public Provider<IEnumerable<ConfigCategoryHandle>> CategoriesProvider { get; set; }

    /// <summary>
    ///     Provides the default value of the entry.
    /// </summary>
    public Provider<T?> DefaultValueProvider { get; set; }

    /// <summary>
    ///     Provides control over getting and setting the local value of an
    ///     entry.
    /// </summary>
    public (Getter Getter, Setter Setter)? LocalValueProvider { get; set; }

    /// <summary>
    ///     Provides control over getting and setting the remote value of an
    ///     entry.
    /// </summary>
    public (Getter Getter, Setter Setter)? RemoteValueProvider { get; set; }

    /// <summary>
    ///     Provides control over getting and setting the pending value of an
    ///     entry.
    /// </summary>
    public (Getter Getter, Setter Setter)? PendingValueProvider { get; set; }

    /// <summary>
    ///     Provides control over determining whether a value has been dirtied.
    /// </summary>
    public Provider<bool> DirtiedProvider { get; set; }

    /// <summary>
    ///     Provides control over whether a reload is required for the entry's
    ///     state to properly update.
    /// </summary>
    public Provider<bool> ReloadRequiredProvider { get; set; }

    /// <summary>
    ///     Provides the display name of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDisplayName(Provider<LocalizedText> displayNameProvider)
    {
        DisplayNameProvider = displayNameProvider;
        return this;
    }

    /// <summary>
    ///     Provides the display name of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithTooltip(Provider<LocalizedText> tooltipProvider)
    {
        TooltipProvider = tooltipProvider;
        return this;
    }

    /// <summary>
    ///     Provides the description of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDescription(Provider<LocalizedText> descriptionProvider)
    {
        DescriptionProvider = descriptionProvider;
        return this;
    }

    /// <summary>
    ///     Provides the config side of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithConfigSide(Provider<ConfigSide> configSideProvider)
    {
        ConfigSideProvider = configSideProvider;
        return this;
    }

    /// <summary>
    ///     Provides the categories of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithCategories(params ConfigCategoryHandle[] categories)
    {
        CategoriesProvider = new Provider<IEnumerable<ConfigCategoryHandle>>(_ => categories.AsEnumerable());
        return this;
    }

    /// <summary>
    ///     Provides the default value of the entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDefaultValue(Provider<T?> defaultValueProvider)
    {
        DefaultValueProvider = defaultValueProvider;
        return this;
    }

    /// <summary>
    ///     Provides control over getting and setting the local value of an
    ///     entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithLocalValueProvider(
        Getter getter,
        Setter setter
    )
    {
        LocalValueProvider = (getter, setter);
        return this;
    }

    /// <summary>
    ///     Provides control over getting and setting the local value of an
    ///     entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithLocalValueProvider(RefProvider provider)
    {
        return WithLocalValueProvider(
            getter: _ => provider(),
            setter: (_, value) => provider() = value
        );
    }

    /// <summary>
    ///     Provides control over getting and setting the remote value of an
    ///     entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithRemoteValueProvider(
        Getter getter,
        Setter setter
    )
    {
        RemoteValueProvider = (getter, setter);
        return this;
    }

    /// <summary>
    ///     Provides control over getting and setting the remote value of an
    ///     entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithRemoteValueProvider(RefProvider provider)
    {
        return WithRemoteValueProvider(
            getter: _ => provider(),
            setter: (_, value) => provider() = value
        );
    }

    /// <summary>
    ///     Provides control over getting and setting the pending value of an
    ///     entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithPendingValueProvider(
        Getter getter,
        Setter setter
    )
    {
        PendingValueProvider = (getter, setter);
        return this;
    }

    /// <summary>
    ///     Provides control over getting and setting the pending value of an
    ///     entry.
    /// </summary>
    public ConfigEntryDescriptor<T> WithLPendingValueProvider(RefProvider provider)
    {
        return WithPendingValueProvider(
            getter: _ => provider(),
            setter: (_, value) => provider() = value
        );
    }

    /// <summary>
    ///     Provides control over determining whether a value has been dirtied.
    /// </summary>
    public ConfigEntryDescriptor<T> WithDirtied(Provider<bool> dirtiedProvider)
    {
        DirtiedProvider = dirtiedProvider;
        return this;
    }

    /// <summary>
    ///     Provides control over whether a reload is required for the entry's
    ///     state to properly update.
    /// </summary>
    public ConfigEntryDescriptor<T> WithReloadRequired(Provider<bool> reloadRequiredProvider)
    {
        ReloadRequiredProvider = reloadRequiredProvider;
        return this;
    }

    /// <summary>
    ///     Provides a default implementation that necessitates a reload if the
    ///     entry has changed.
    /// </summary>
    public ConfigEntryDescriptor<T> WithReloadRequired()
    {
        ReloadRequiredProvider = new Provider<bool>(entry => entry.Dirty);
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

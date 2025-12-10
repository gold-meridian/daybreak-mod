using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
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

    /// <summary>
    ///     The full name of this handle.
    /// </summary>
    public string FullName => $"{LanguageHelpers.GetModName(Mod)}/{Name}";
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
    ///     The most up-to-date value of this entry.  If this entry is synced
    ///     to both the client and server, it will use the value the server
    ///     provides.  If this value is not synced, it will solely use the local
    ///     value provided by this instance.
    ///     <br />
    ///     This value cannot be directly modified, you should queue your
    ///     changes to <see cref="PendingValue"/> and commit them.
    /// </summary>
    object? Value { get; }

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
    ///     The pending value represents the future state of a config entry, and
    ///     is stored until it is committed as a real value.
    ///     <br />
    ///     This should be kept up-to-date with <see cref="LocalValue"/> if it's
    ///     changed, and is preferred over <see cref="LocalValue"/> when
    ///     retrieving the current state from <see cref="Value"/>.
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

    /// <summary>
    ///     Commits pending changes from <see cref="PendingValue"/> to
    ///     <see cref="LocalValue"/>.
    ///     <br />
    ///     This triggers the owning <see cref="ConfigRepository"/> to save and
    ///     synchronize itself as necessary to reflect these changes.
    /// </summary>
    /// <param name="bulk">
    ///     Whether this pending commit change is part of a bulk operation over
    ///     the entire <see cref="ConfigRepository"/>.
    ///     <br />
    ///     If this value is <see langword="true"/>, only values should be
    ///     updated.
    ///     <br />
    ///     If this value is <see langword="false"/>, it is valid to manually
    ///     call <see cref="ConfigRepository.SerializeCategories"/> and
    ///     <see cref="ConfigRepository.SynchronizeEntries"/>.
    /// </param>
    /// <returns>
    ///     Whether an observable change has occured to <see cref="LocalValue"/>
    ///     based on the value of <see cref="PendingValue"/>, necessitating this
    ///     value to be synchronized and overwritten in a
    ///     <paramref name="bulk"/> operation.
    /// </returns>
    /// <remarks>
    ///     <see cref="CommitPendingChanges"/> may be called despite no changes
    ///     between <see cref="PendingValue"/> and <see cref="LocalValue"/>.  It
    ///     is expected in this scenario to make no observable changes and to
    ///     return <see langword="false"/>.
    /// </remarks>
    bool CommitPendingChanges(bool bulk);

    /// <summary>
    ///     Serializes the value to a token.  If the token is
    ///     <see langword="null"/>, then this entry is considered as providing
    ///     no value, and it will be skipped.
    /// </summary>
    JToken? Serialize(ConfigSerialization.Mode mode, object? value);

    /// <summary>
    ///     Deserializes the token to a value.
    /// </summary>
    object? Deserialize(ConfigSerialization.Mode mode, JToken? token);
}

/// <summary>
///     Static APIs relating to <see cref="ConfigEntry{T}"/>.
/// </summary>
public static class ConfigEntry
{
    /// <summary>
    ///     Creates a new object for configuring the config entry.
    /// </summary>
    public static ConfigEntryOptions<T> Options<T>()
    {
        return new ConfigEntryOptions<T>();
    }
}

/// <summary>
///     The type-safe implementation of <see cref="IConfigEntry"/>.
/// </summary>
public sealed class ConfigEntry<T> : IConfigEntry
{
    Type IConfigEntry.ValueType => typeof(T);

    object? IConfigEntry.Value => Value;

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
    public ConfigEntryHandle Handle { get; }

    /// <summary>
    ///     The descriptor which dictates the behavior of this entry.
    /// </summary>
    public ConfigEntryOptions<T> Options { get; }

    /// <inheritdoc />
    public ConfigSide Side =>
        Options.ConfigSide?.Invoke(this)
     ?? ConfigSideFromModSide(Handle.Mod?.Side ?? ModSide.NoSync);

    /// <inheritdoc />
    public LocalizedText DisplayName =>
        Options.DisplayName?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(Handle.Mod, nameof(ConfigEntry<>), nameof(DisplayName), () => Handle.Name);

    /// <inheritdoc />
    public LocalizedText Tooltip =>
        Options.Tooltip?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(Handle.Mod, nameof(ConfigEntry<>), nameof(Tooltip), () => "");

    /// <inheritdoc />
    public LocalizedText Description =>
        Options.Description?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(Handle.Mod, nameof(ConfigEntry<>), nameof(Description), () => "");

    /// <inheritdoc />
    public ConfigCategoryHandle[] Categories { get; }

    /// <inheritdoc cref="IConfigEntry.Value"/>
    public T? Value
    {
        get
        {
            T? value;
            if (Side == ConfigSide.Both && Main.netMode == NetmodeID.MultiplayerClient)
            {
                value = RemoteValue;
            }
            else if (!Equals(LocalValue, PendingValue))
            {
                value = PendingValue;
            }
            else
            {
                value = LocalValue;
            }

            if (Options.Value is { } valueProvider)
            {
                return valueProvider(this, value);
            }

            return value;
        }
    }

    /// <inheritdoc cref="IConfigEntry.LocalValue"/>
    public T? LocalValue
    {
        get => GetValue(field, Options.LocalValue?.Getter);
        set => field = SetValue(field, value, Options.LocalValue?.Setter);
    }

    /// <inheritdoc cref="IConfigEntry.RemoteValue"/>
    public T? RemoteValue
    {
        get => GetValue(field, Options.RemoteValue?.Getter);
        set => field = SetValue(field, value, Options.RemoteValue?.Setter);
    }

    /// <inheritdoc cref="IConfigEntry.PendingValue"/>
    public T? PendingValue
    {
        get => GetValue(field, Options.PendingValue?.Getter);
        set => field = SetValue(field, value, Options.PendingValue?.Setter);
    }

    // The compiler doesn't know how to lift a conditional access
    // (provider?.Invoke()) to a T? for some reason.
    /// <inheritdoc cref="IConfigEntry.DefaultValue"/>
    public T? DefaultValue =>
        Options.DefaultValue is { } provider
            ? provider.Invoke(this)
            : default(T);

    /// <inheritdoc />
    public bool Dirty =>
        Options.Dirtied?.Invoke(this) ?? !Equals(LocalValue, PendingValue);

    /// <inheritdoc />
    public bool ReloadRequired =>
        Options.ReloadRequired?.Invoke(this) ?? false;

    /// <summary>
    ///     Initializes this entry with its default value.
    /// </summary>
    public ConfigEntry(
        ConfigEntryHandle handle,
        ConfigEntryOptions<T> options
    )
    {
        Handle = handle;
        Options = options;
        LocalValue = DefaultValue;
        RemoteValue = DefaultValue;
        PendingValue = DefaultValue;
        Categories = Options.Categories?.Invoke(this).ToArray() is { Length: >= 1 } categories
            ? categories
            : throw new InvalidOperationException("Config entries must be supplied at least one category: " + Handle);
    }

    private T? GetValue(T? storedValue, ConfigEntryOptions<T>.Getter? getter)
    {
        if (getter is not null)
        {
            return getter(this, storedValue);
        }

        return storedValue;
    }

    private T? SetValue(T? storedValue, T? incomingValue, ConfigEntryOptions<T>.Setter? setter)
    {
        if (setter is not null)
        {
            setter(this, ref storedValue, incomingValue);
            return storedValue;
        }

        return incomingValue;
    }

    /// <inheritdoc />
    public bool CommitPendingChanges(bool bulk)
    {
        var dirty = Dirty;
        {
            LocalValue = PendingValue;
        }

        if (dirty && !bulk)
        {
            Handle.Repository.SerializeCategories(((IConfigEntry)this).MainCategory);
            Handle.Repository.SynchronizeEntries(Handle);
        }

        return dirty;
    }

    JToken? IConfigEntry.Serialize(ConfigSerialization.Mode mode, object? value)
    {
        if (Options.Serialization is { Serializer: { } serializer })
        {
            return serializer(this, mode, (T?)value);
        }

        if (value is null)
        {
            return JValue.CreateNull();
        }

        try
        {
            return JToken.FromObject(value);
        }
        catch
        {
            return new JValue(value.ToString());
        }
    }

    object? IConfigEntry.Deserialize(ConfigSerialization.Mode mode, JToken? token)
    {
        if (Options.Serialization is { Deserializer: { } deserializer })
        {
            return deserializer(this, mode, token);
        }

        if (token is null || token.Type == JTokenType.Null)
        {
            return DefaultValue;
        }

        try
        {
            return token.ToObject<T>();
        }
        catch
        {
            return FallbackParse();
        }

        object? FallbackParse()
        {
            var targetType = typeof(T);

            try
            {
                var stringVal = token.ToString();

                // Enum
                if (targetType.IsEnum)
                {
                    if (Enum.TryParse(targetType, stringVal, ignoreCase: true, out var enumVal))
                    {
                        return enumVal;
                    }

                    if (long.TryParse(stringVal, out var num))
                    {
                        return Enum.ToObject(targetType, num);
                    }
                }

                // Nullable and primitives
                var underlying = Nullable.GetUnderlyingType(targetType);
                return Convert.ChangeType(stringVal, underlying ?? targetType);
            }
            catch
            {
                return DefaultValue;
            }
        }
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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ConfigEntryOptions<T>
{
    public delegate T? Getter(ConfigEntry<T> entry, T? storedValue);

    public delegate void Setter(ConfigEntry<T> entry, ref T? storedValue, T? newValue);

    public Func<ConfigEntry<T>, LocalizedText>? DisplayName { get; set; }

    public Func<ConfigEntry<T>, LocalizedText>? Tooltip { get; set; }

    public Func<ConfigEntry<T>, LocalizedText>? Description { get; set; }

    public Func<ConfigEntry<T>, ConfigSide>? ConfigSide { get; set; }

    public Func<ConfigEntry<T>, IEnumerable<ConfigCategoryHandle>>? Categories { get; set; }

    public Func<ConfigEntry<T>, T?>? DefaultValue { get; set; }

    public (Getter Getter, Setter Setter)? LocalValue { get; set; }

    public (Getter Getter, Setter Setter)? RemoteValue { get; set; }

    public (Getter Getter, Setter Setter)? PendingValue { get; set; }

    public (ConfigSerialization.Serialize<T> Serializer, ConfigSerialization.Deserialize<T> Deserializer)? Serialization { get; set; }

    public Getter? Value { get; set; }

    public Func<ConfigEntry<T>, bool>? Dirtied { get; set; }

    public Func<ConfigEntry<T>, bool>? ReloadRequired { get; set; }

    public ConfigEntryOptions<T> With(Action<ConfigEntryOptions<T>> apply)
    {
        apply(this);
        return this;
    }

    /// <summary>
    ///     Creates an entry and registers the entry to the repository.
    /// </summary>
    public ConfigEntry<T> Register(
        ConfigRepository repository,
        Mod? mod,
        [CallerMemberName] string uniqueKey = ""
    )
    {
        return repository.RegisterEntry(
            new ConfigEntry<T>(
                new ConfigEntryHandle(repository, mod, uniqueKey),
                this
            )
        );
    }
}

public static class ConfigEntryOptionsExtensions
{
    extension<T>(ConfigEntryOptions<T> entry)
    {
        public ConfigEntryOptions<T> WithDisplayName(Func<ConfigEntry<T>, LocalizedText>? displayName)
        {
            return entry.With(x => x.DisplayName = displayName);
        }

        public ConfigEntryOptions<T> WithTooltip(Func<ConfigEntry<T>, LocalizedText>? tooltip)
        {
            return entry.With(x => x.Tooltip = tooltip);
        }

        public ConfigEntryOptions<T> WithDescription(Func<ConfigEntry<T>, LocalizedText>? description)
        {
            return entry.With(x => x.Description = description);
        }

        public ConfigEntryOptions<T> WithConfigSide(Func<ConfigEntry<T>, ConfigSide>? configSide)
        {
            return entry.With(x => x.ConfigSide = configSide);
        }

        public ConfigEntryOptions<T> WithCategories(Func<ConfigEntry<T>, IEnumerable<ConfigCategoryHandle>>? categories)
        {
            return entry.With(x => x.Categories = categories);
        }

        public ConfigEntryOptions<T> WithCategories(params ConfigCategoryHandle[] categories)
        {
            return entry.With(x => x.Categories = _ => categories);
        }

        public ConfigEntryOptions<T> WithDefaultValue(Func<ConfigEntry<T>, T?>? defaultValue)
        {
            return entry.With(x => x.DefaultValue = defaultValue);
        }

        public ConfigEntryOptions<T> WithLocalValue(ConfigEntryOptions<T>.Getter getter, ConfigEntryOptions<T>.Setter setter)
        {
            return entry.With(x => x.LocalValue = (getter, setter));
        }

        public ConfigEntryOptions<T> WithRemoteValue(ConfigEntryOptions<T>.Getter getter, ConfigEntryOptions<T>.Setter setter)
        {
            return entry.With(x => x.RemoteValue = (getter, setter));
        }

        public ConfigEntryOptions<T> WithPendingValue(ConfigEntryOptions<T>.Getter getter, ConfigEntryOptions<T>.Setter setter)
        {
            return entry.With(x => x.PendingValue = (getter, setter));
        }

        public ConfigEntryOptions<T> WithSerialization(ConfigSerialization.Serialize<T> serializer, ConfigSerialization.Deserialize<T> deserializer)
        {
            return entry.With(x => x.Serialization = (serializer, deserializer));
        }

        public ConfigEntryOptions<T> WithValue(ConfigEntryOptions<T>.Getter? getter)
        {
            return entry.With(x => x.Value = getter);
        }

        public ConfigEntryOptions<T> WithDirtied(Func<ConfigEntry<T>, bool>? dirtied)
        {
            return entry.With(x => x.Dirtied = dirtied);
        }

        public ConfigEntryOptions<T> WithReloadRequired(Func<ConfigEntry<T>, bool>? reloadRequired)
        {
            return entry.With(x => x.ReloadRequired = reloadRequired);
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

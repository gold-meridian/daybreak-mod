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
///     Untyped config entry contract.
/// </summary>
public interface IConfigEntry : IConfigEntryMetadata
{
    /// <summary>
    ///     The type this config entry wraps.
    /// </summary>
    Type EntryType { get; }

    /// <summary>
    ///     The config entry handle which may be used to uniquely identify this
    ///     entry and obtain it as necessary.
    /// </summary>
    ConfigEntryHandle Handle { get; }

    /// <summary>
    ///     Commits any pending values to this entry and performs necessary
    ///     saving and syncing of data.
    /// </summary>
    /// <param name="bulk">
    ///     Whether this entry is being committed as part of a bulk operation
    ///     over multiple entries.
    /// </param>
    /// <returns>Whether any changes were made.</returns>
    bool CommitPendingChanges(bool bulk);

    /// <summary>
    ///     Untyped <see cref="IConfigEntry{T}.Serialize"/> proxy.
    /// </summary>
    /// <param name="layer"></param>
    /// <returns></returns>
    JToken? BoxedDefaultSerialize(ConfigValueLayer layer);
}

/// <summary>
///     Untyped metadata for a config entry.
/// </summary>
public interface IConfigEntryMetadata
{
    /// <summary>
    ///     This entry's side, which controls syncing and which version of the
    ///     config to use and display.
    /// </summary>
    ConfigSide Side { get; }

    /// <summary>
    ///     The nullability contract for this config entry.
    /// </summary>
    ConfigNullability Nullability { get; }

    /// <summary>
    ///     The display name of this config entry.
    /// </summary>
    LocalizedText DisplayName { get; }

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
    ConfigCategoryHandle MainCategory { get; }
}

/// <summary>
///     Typed data pertaining to config values.
/// </summary>
public interface IConfigEntryValues<T> : IConfigEntry
{
    Type IConfigEntry.EntryType => typeof(T);

    /// <summary>
    ///     The resolved config value.
    /// </summary>
    ConfigResolvedValue<T> Resolved { get; }

    /// <summary>
    ///     The layer of the resolved config value.
    /// </summary>
    ConfigValueLayer ActiveLayer { get; }

    /// <summary>
    ///     The direct value of the resolved config value.
    /// </summary>
    T Value { get; }

    /// <summary>
    ///     Whether a value is set for the layer.
    /// </summary>
    bool HasLayerValue(ConfigValueLayer layer);

    /// <summary>
    ///     Gets the config value for the layer.
    /// </summary>
    ConfigValue<T> GetLayerValue(ConfigValueLayer layer);
}

/// <summary>
///     The pending state of a config entry, for manipulation through external
///     sources such as a UI.
/// </summary>
public sealed class ConfigEditState<T>
{
    /// <summary>
    ///     The pending, edited value.
    /// </summary>
    public ConfigValue<T> EditedValue { get; set; } = ConfigValue<T>.Unset();

    /// <summary>
    ///     Whether the option is currently being edited.
    /// </summary>
    public bool IsEditing { get; set; }
}

/// <summary>
///     The dirty state for a config entry.
/// </summary>
public enum DirtyState
{
    /// <summary>
    ///     No changes have been made.
    /// </summary>
    Clean,

    /// <summary>
    ///     The entry is dirtied and needs saving.
    /// </summary>
    Dirty,

    /// <summary>
    ///     The entry is dirtied and needs saving, but requires the mod to
    ///     reload to safely apply the changes.
    /// </summary>
    DirtyRequiresReload,
}

/// <summary>
///     The complete description of a config entry's behavior.
/// </summary>
public interface IConfigEntry<T> : IConfigEntryValues<T>
{
    /// <summary>
    ///     The editing state of this entry.
    /// </summary>
    ConfigEditState<T> EditState { get; }

    /// <summary>
    ///     The state of the value being edited, or just the
    ///     <see cref="IConfigEntryValues{T}.Value"/> if nothing is being
    ///     edited.
    /// </summary>
    ConfigValue<T> EditValue { get; set; }

    /// <summary>
    ///     The value to display, which may be overridden when the value is
    ///     being edited.
    /// </summary>
    ConfigValue<T> DisplayValue { get; }

    /// <summary>
    ///     Whether this entry is dirtied and needs saving or syncing.
    /// </summary>
    DirtyState DirtyState { get; }

    /// <summary>
    ///     Serializes the value to a token.  If the token is
    ///     <see langword="null"/>, then this entry is considered as providing
    ///     no value, and it will be skipped.
    /// </summary>
    JToken? Serialize(ConfigValue<T> value);

    JToken? IConfigEntry.BoxedDefaultSerialize(ConfigValueLayer layer)
    {
        return Serialize(GetLayerValue(layer));
    }

    /// <summary>
    ///     Deserializes the token to a value.
    /// </summary>
    ConfigValue<T> Deserialize(JToken? token);

    /// <summary>
    ///     Applies the value as a preset.
    /// </summary>
    void ApplyPreset(ConfigValue<T> value);

    /// <summary>
    ///     Clears the preset value.
    /// </summary>
    void ClearPreset();
}

internal sealed class TransformableValueStack<T>(ConfigEntry<T> entry) : ConfigValueStack<T>
{
    public override ConfigValue<T> Get(ConfigValueLayer layer)
    {
        var value = base.Get(layer);

        if (entry.Options.ValueTransformer is not { } transformer)
        {
            return value;
        }

        return transformer.Getter(entry, layer, value);
    }

    public override void Set(ConfigValueLayer layer, T value)
    {
        if (entry.Options.ValueTransformer is not { } transformer)
        {
            base.Set(layer, value);
            return;
        }

        transformer.Setter(entry, layer, ref Values[(int)layer], ConfigValue<T>.Set(value));
    }

    public override void Unset(ConfigValueLayer layer)
    {
        if (entry.Options.ValueTransformer is not { } transformer)
        {
            base.Unset(layer);
            return;
        }

        transformer.Setter(entry, layer, ref Values[(int)layer], ConfigValue<T>.Unset());
    }

    public override bool IsSet(ConfigValueLayer layer)
    {
        return Get(layer).IsSet;
    }
}

/// <summary>
///     The type-safe implementation of <see cref="IConfigEntry"/>.
/// </summary>
public class ConfigEntry<T> : IConfigEntry<T>
{
#region IConfigEntry
    /// <inheritdoc />
    public ConfigEntryHandle Handle { get; }
#endregion

#region IConfigEntryMetadata
    /// <inheritdoc />
    public virtual ConfigSide Side =>
        Options.Side?.Invoke(this)
     ?? ConfigSideFromModSide(Handle.Mod?.Side ?? ModSide.NoSync);

    /// <inheritdoc />
    public virtual ConfigNullability Nullability
    {
        get
        {
            if (Options.Nullability is null)
            {
                return default(T) is null
                    ? ConfigNullability.AllowNull
                    : ConfigNullability.DisallowNull;
            }

            return Options.Nullability.Invoke(this);
        }
    }

    /// <inheritdoc />
    public LocalizedText DisplayName =>
        Options.DisplayName?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(
            Handle.Mod,
            Handle.Name,
            nameof(ConfigEntry<>),
            nameof(DisplayName),
            () => Handle.Name
        );

    /// <inheritdoc />
    public LocalizedText Description =>
        Options.Description?.Invoke(this)
     ?? LanguageHelpers.GetLocalization(
            Handle.Mod,
            Handle.Name,
            nameof(ConfigEntry<>),
            nameof(Description),
            () => ""
        );

    /// <inheritdoc />
    public ConfigCategoryHandle[] Categories { get; }

    /// <inheritdoc />
    public ConfigCategoryHandle MainCategory => Categories.First();
#endregion

#region IConfigEntryValues
    /// <inheritdoc />
    public ConfigResolvedValue<T> Resolved
    {
        get
        {
            var resolved = ConfigValueResolver.Resolve(
                values,
                Side,
                Main.netMode == NetmodeID.MultiplayerClient
            );

            if (Options.ValueTransformer is null)
            {
                return resolved;
            }

            return resolved with { Value = Options.ValueTransformer.Value.Getter(this, resolved.Layer, resolved.Value) };
        }
    }

    /// <inheritdoc />
    public ConfigValueLayer ActiveLayer => Resolved.Layer;

    /// <inheritdoc />
    public T Value => Resolved.Value.Value;

    /// <inheritdoc />
    public bool HasLayerValue(ConfigValueLayer layer)
    {
        return values.IsSet(layer);
    }

    /// <inheritdoc />
    public ConfigValue<T> GetLayerValue(ConfigValueLayer layer)
    {
        var value = values.Get(layer);

        if (Options.ValueTransformer is null)
        {
            return value;
        }

        return Options.ValueTransformer.Value.Getter(this, layer, value);
    }
#endregion

#region IConfigEntry
    /// <inheritdoc />
    public ConfigEditState<T> EditState { get; } = new();

    /// <inheritdoc />
    public ConfigValue<T> EditValue
    {
        get => EditState.IsEditing
            ? EditState.EditedValue
            : ConfigValue<T>.Set(Value);

        set
        {
            EditState.EditedValue = ValidateValue(value);
            EditState.IsEditing = true;
        }
    }

    /// <inheritdoc />
    public virtual ConfigValue<T> DisplayValue =>
        EditState.IsEditing
            ? EditState.EditedValue
            : GetLayerValue(ActiveLayer);

    /// <inheritdoc />
    public virtual DirtyState DirtyState
    {
        get
        {
            if (!EditState.IsEditing)
            {
                return DirtyState.Clean;
            }

            var userValue = GetLayerValue(ConfigValueLayer.User);
            var editedValue = EditState.EditedValue;

            if (!editedValue.IsSet)
            {
                return DirtyState.Clean;
            }

            if (Options.DirtyState is not null)
            {
                return Options.DirtyState.Invoke(this, userValue, editedValue);
            }

            return Equals(userValue.Value, editedValue.Value) ? DirtyState.Clean : DirtyState.Dirty;
        }
    }

    /// <inheritdoc />
    public bool CommitPendingChanges(bool bulk)
    {
        var changed = DirtyState != DirtyState.Clean;
        if (!changed)
        {
            return false;
        }

        // var old = values.Get(ConfigValueLayer.User);
        var next = EditState.EditedValue;

        if (next.IsSet)
        {
            values.Set(ConfigValueLayer.User, next.Value);
        }
        else
        {
            values.Unset(ConfigValueLayer.User);
        }

        EditState.IsEditing = false;

        if (bulk)
        {
            return true;
        }

        Handle.Repository.SerializeCategories(MainCategory);
        Handle.Repository.SynchronizeEntries(Handle);
        return true;
    }

    /// <inheritdoc />
    public JToken? Serialize(ConfigValue<T> value)
    {
        if (Options.Serialization is { Serializer: { } serializer })
        {
            return serializer(this, value);
        }

        if (!value.IsSet)
        {
            return null;
        }

        if (value.Value is null)
        {
            return JValue.CreateNull();
        }

        try
        {
            return JToken.FromObject(value.Value);
        }
        catch
        {
            return new JValue(value.Value.ToString());
        }
    }

    /// <inheritdoc />
    public ConfigValue<T> Deserialize(JToken? token)
    {
        if (Options.Serialization is { Deserializer: { } deserializer })
        {
            return deserializer(this, token);
        }

        if (token is null || token.Type == JTokenType.Null)
        {
            return ValidateValue(GetLayerValue(ConfigValueLayer.Default));
        }

        try
        {
            return ValidateValue(ConfigValue<T>.Set(token.ToObject<T>()!));
        }
        catch
        {
            return ValidateValue(ConfigValue<T>.Set(ConfigSerialization.FallbackDeserialize(token, this)!));
        }
    }

    /// <inheritdoc />
    public void ApplyPreset(ConfigValue<T> value)
    {
        if (value.IsSet)
        {
            values.Set(ConfigValueLayer.Preset, value.Value);
        }
        else
        {
            values.Unset(ConfigValueLayer.Preset);
        }
    }

    /// <inheritdoc />
    public void ClearPreset()
    {
        values.Unset(ConfigValueLayer.Preset);
    }
#endregion

    /// <summary>
    ///     Options that configure the default behavior of this entry.
    /// </summary>
    public ConfigEntryOptions<T> Options { get; }

    private readonly TransformableValueStack<T> values;

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
        Categories = Options.Categories?.Invoke(this).ToArray() is { Length: >= 1 } categories
            ? categories
            : throw new InvalidOperationException("Config entries must be supplied at least one category: " + Handle);

        values = new TransformableValueStack<T>(this);

        var defaultValue = Options.DefaultValue?.Invoke(this) ?? ConfigValue<T>.Set(default(T)!);

        values.Set(
            ConfigValueLayer.Default,
            ValidateValue(defaultValue).Value
        );
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

    private ConfigValue<T> ValidateValue(ConfigValue<T> value)
    {
        if (!value.IsSet)
        {
            return value;
        }

        if (value.Value is null && Nullability == ConfigNullability.DisallowNull)
        {
            throw new InvalidOperationException(
                $"Entry \"{Handle.FullName}\" does not allow null values"
            );
        }

        return value;
    }

    /// <summary>
    ///     Creates a new object for configuring the config entry.
    /// </summary>
    public static ConfigEntryOptions<T> Define()
    {
        return new ConfigEntryOptions<T>();
    }
}

/// <summary>
///     A descriptor for dynamically constructing a
///     <see cref="ConfigEntry{T}"/> with arbitrary behavior.
/// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ConfigEntryOptions<T>
{
    public delegate ConfigValue<T> Getter(
        ConfigEntry<T> entry,
        ConfigValueLayer layer,
        ConfigValue<T> storedValue
    );

    public delegate void Setter(
        ConfigEntry<T> entry,
        ConfigValueLayer layer,
        ref ConfigValue<T> storedValue,
        ConfigValue<T> newValue
    );

    public delegate DirtyState DirtyEvaluator(
        ConfigEntry<T> entry,
        ConfigValue<T> before,
        ConfigValue<T> after
    );

#region IConfigEntryMetadata
    public Func<ConfigEntry<T>, ConfigSide>? Side { get; set; }

    public Func<ConfigEntry<T>, ConfigNullability>? Nullability { get; set; }

    public Func<ConfigEntry<T>, LocalizedText>? DisplayName { get; set; }

    public Func<ConfigEntry<T>, LocalizedText>? Description { get; set; }

    public Func<ConfigEntry<T>, IEnumerable<ConfigCategoryHandle>>? Categories { get; set; }
#endregion

#region IConfigEntryValues
    public Func<ConfigEntry<T>, ConfigValue<T>>? DefaultValue { get; set; }

    public (Getter Getter, Setter Setter)? ValueTransformer { get; set; }
#endregion

#region IConfigEntry
    public DirtyEvaluator? DirtyState { get; set; }

    public (ConfigSerialization.Serialize<T> Serializer, ConfigSerialization.Deserialize<T> Deserializer)? Serialization { get; set; }
#endregion

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
    extension<T>(ConfigEntryOptions<T> options)
    {
        public ConfigEntryOptions<T> WithConfigSide(ConfigSide side)
        {
            return options.With(x => x.Side = _ => side);
        }

        public ConfigEntryOptions<T> AllowNull()
        {
            return options.With(x => x.Nullability = _ => ConfigNullability.AllowNull);
        }

        public ConfigEntryOptions<T> DisallowNull()
        {
            return options.With(x => x.Nullability = _ => ConfigNullability.DisallowNull);
        }

        public ConfigEntryOptions<T> WithDisplayName(Func<ConfigEntry<T>, LocalizedText>? displayName)
        {
            return options.With(x => x.DisplayName = displayName);
        }

        public ConfigEntryOptions<T> WithDescription(Func<ConfigEntry<T>, LocalizedText>? description)
        {
            return options.With(x => x.Description = description);
        }

        public ConfigEntryOptions<T> WithCategories(Func<ConfigEntry<T>, IEnumerable<ConfigCategoryHandle>>? categories)
        {
            return options.With(x => x.Categories = categories);
        }

        public ConfigEntryOptions<T> WithCategories(params ConfigCategoryHandle[] categories)
        {
            return options.With(x => x.Categories = _ => categories);
        }

        public ConfigEntryOptions<T> WithDefaultValue(Func<ConfigEntry<T>, ConfigValue<T>>? defaultValue)
        {
            return options.With(x => x.DefaultValue = defaultValue);
        }
        
        public ConfigEntryOptions<T> WithDefaultValue(Func<ConfigEntry<T>, T> defaultValue)
        {
            return options.With(x => x.DefaultValue = e => ConfigValue<T>.Set(defaultValue(e)));
        }

        public ConfigEntryOptions<T> WithValueTransformer(ConfigEntryOptions<T>.Getter getter, ConfigEntryOptions<T>.Setter setter)
        {
            return options.With(x => x.ValueTransformer = (getter, setter));
        }

        public ConfigEntryOptions<T> WithDirtyPolicy(ConfigEntryOptions<T>.DirtyEvaluator dirtyProvider)
        {
            return options.With(x => x.DirtyState = dirtyProvider);
        }

        public ConfigEntryOptions<T> WithSerialization(ConfigSerialization.Serialize<T> serializer, ConfigSerialization.Deserialize<T> deserializer)
        {
            return options.With(x => x.Serialization = (serializer, deserializer));
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

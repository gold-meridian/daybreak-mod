using System;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     The nullability contract for a config entry.
/// </summary>
public enum ConfigNullability
{
    /// <summary>
    ///     Disallow null values.
    /// </summary>
    DisallowNull,

    /// <summary>
    ///     Allow null values.
    /// </summary>
    AllowNull,
}

/// <summary>
///     The layer a config value falls under, determining its importance.
/// </summary>
public enum ConfigValueLayer
{
    /// <summary>
    ///     The default value provided by a config entry.
    /// </summary>
    Default,

    /// <summary>
    ///     A preset value applied externally by a config repository, which
    ///     overrides the default value.
    /// </summary>
    Preset,

    /// <summary>
    ///     A user-specified value applied manually by the user, which overrides
    ///     any presets.
    /// </summary>
    User,

    /// <summary>
    ///     The value provided by the server, which takes priority over all
    ///     values.
    /// </summary>
    Server,
}

/// <summary>
///     Represents a resolved config value for a given
///     <see cref="ConfigValueLayer"/>.
/// </summary>
public readonly record struct ConfigResolvedValue<T>(
    ConfigValue<T> Value,
    ConfigValueLayer Layer
);

/// <summary>
///     Wraps a config value to provide information on whether a value is set.
/// </summary>
public readonly struct ConfigValue<T>
{
    /// <summary>
    ///     Whether this value is set.
    /// </summary>
    public bool IsSet { get; }

    /// <summary>
    ///     The value represented by this wrapper.
    /// </summary>
    public T Value { get; }

    private ConfigValue(bool isSet, T value)
    {
        IsSet = isSet;
        Value = value;
    }

    /// <summary>
    ///     Initializes an unset value.
    /// </summary>
    public static ConfigValue<T> Unset()
    {
        return new ConfigValue<T>(false, default(T)!);
    }

    /// <summary>
    ///     Initializes a set value.
    /// </summary>
    public static ConfigValue<T> Set(T value)
    {
        return new ConfigValue<T>(true, value);
    }

    /// <summary>
    ///     A string representation of this value.  <see langword="null"/> will
    ///     produce a special <c>&lt;null&gt;</c> string and unset values will
    ///     produce a special <c>&lt;unset&gt;</c> value.
    /// </summary>
    public override string ToString()
    {
        return IsSet ? Value?.ToString() ?? "<null>" : "<unset>";
    }
}

/// <summary>
///     Consolidates possible values provided by <see cref="ConfigValueLayer"/>s
///     into a single object.
/// </summary>
public class ConfigValueStack<T>
{
    /// <summary>
    ///     The values provided by this stack.
    /// </summary>
    protected ConfigValue<T>[] Values { get; } = new ConfigValue<T>[ConfigValueResolver.LayerCount];

    /// <summary>
    ///     Gets the <see cref="ConfigValue{T}"/> for the
    ///     <see cref="ConfigValueLayer"/>.
    /// </summary>
    public virtual ConfigValue<T> Get(ConfigValueLayer layer)
    {
        return Values[(int)layer];
    }

    /// <summary>
    ///     Sets the <see cref="ConfigValue{T}"/> for the
    ///     <see cref="ConfigValueLayer"/>.
    /// </summary>
    public virtual void Set(ConfigValueLayer layer, T value)
    {
        Values[(int)layer] = ConfigValue<T>.Set(value);
    }

    /// <summary>
    ///     Unsets the <see cref="ConfigValue{T}"/> for the
    ///     <see cref="ConfigValueLayer"/>.
    /// </summary>
    public virtual void Unset(ConfigValueLayer layer)
    {
        Values[(int)layer] = ConfigValue<T>.Unset();
    }

    /// <summary>
    ///     Whether the <see cref="ConfigValue{T}"/> is set for the
    ///     <see cref="ConfigValueLayer"/>.
    /// </summary>
    public virtual bool IsSet(ConfigValueLayer layer)
    {
        return Values[(int)layer].IsSet;
    }
}

/// <summary>
///     Responsible for resolving <see cref="ConfigValue{T}"/>s in a
///     <see cref="ConfigValueStack{T}"/>.
/// </summary>
public static class ConfigValueResolver
{
    internal static int LayerCount { get; } = Enum.GetValues<ConfigValueLayer>().Length;

    internal static ConfigValueLayer[] PendingLayersByPriority { get; } =
    [
        ConfigValueLayer.User,
        ConfigValueLayer.Preset,
    ];

    /// <summary>
    ///     Resolves a <see cref="ConfigResolvedValue{T}"/> from a
    ///     <see cref="ConfigValueStack{T}"/>.
    /// </summary>
    public static ConfigResolvedValue<T> Resolve<T>(
        ConfigValueStack<T> stack,
        ConfigSide side,
        bool isMultiplayerClient
    )
    {
        if (side == ConfigSide.Both && isMultiplayerClient && stack.IsSet(ConfigValueLayer.Server))
        {
            return new ConfigResolvedValue<T>(stack.Get(ConfigValueLayer.Server), ConfigValueLayer.Server);
        }

        for (var i = (int)ConfigValueLayer.User; i >= 0; i--)
        {
            var layer = (ConfigValueLayer)i;
            if (stack.IsSet(layer))
            {
                return new ConfigResolvedValue<T>(stack.Get(layer), layer);
            }
        }

        // TODO: May warrant an exception.
        return new ConfigResolvedValue<T>(ConfigValue<T>.Unset(), ConfigValueLayer.Default);
    }
}

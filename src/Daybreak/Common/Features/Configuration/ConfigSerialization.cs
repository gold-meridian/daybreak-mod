using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     APIs for handling the serialization and deserialization of
///     <see cref="IConfigEntry"/> instances for file IO and network
///     synchronization.
/// </summary>
public static class ConfigSerialization
{
    /// <summary>
    ///     For serializing entry values.
    /// </summary>
    public delegate JToken? Serialize<T>(ConfigEntry<T> entry, ConfigValue<T> value);

    /// <summary>
    ///     For deserializing entry values.
    /// </summary>
    public delegate ConfigValue<T> Deserialize<T>(ConfigEntry<T> entry, JToken? token);

    /// <summary>
    ///     Serializes an entire category into a usable string.
    /// </summary>
    public static JObject SerializeCategory(
        ConfigRepository repository,
        ConfigCategory category,
        IEnumerable<IConfigEntry> entries
    )
    {
        var obj = new JObject();
        foreach (var entry in entries)
        {
            if (SerializeEntry(entry) is not { } token)
            {
                continue;
            }

            obj[entry.Handle.FullName] = token;
        }

        return obj;
    }

    /// <summary>
    ///     Serializes the local value of the entry.
    /// </summary>
    public static JToken? SerializeEntry(IConfigEntry entry)
    {
        return entry.BoxedDefaultSerialize(ConfigValueLayer.User);
    }

    internal static T? FallbackDeserialize<T>(JToken token, IConfigEntry<T> entry)
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
                    return (T?)enumVal;
                }

                if (long.TryParse(stringVal, out var num))
                {
                    return (T?)Enum.ToObject(targetType, num);
                }
            }

            // Nullable and primitives
            var underlying = Nullable.GetUnderlyingType(targetType);
            return (T?)Convert.ChangeType(stringVal, underlying ?? targetType);
        }
        catch
        {
            return entry.GetLayerValue(ConfigValueLayer.Default).Value;
        }
    }
}

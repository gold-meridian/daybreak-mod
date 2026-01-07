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
    ///     Serializes an entire category into a <see cref="JObject"/>.
    /// </summary>
    public static JObject SerializeCategory(
        ConfigRepository repository,
        ConfigCategory category,
        ConfigValueLayer layer,
        IEnumerable<IConfigEntry> entries
    )
    {
        var obj = new JObject();
        foreach (var entry in entries)
        {
            if (SerializeEntry(entry, layer) is not { } token)
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
    public static JToken? SerializeEntry(IConfigEntry entry, ConfigValueLayer layer)
    {
        return entry.BoxedDefaultSerialize(layer);
    }

    /// <summary>
    ///     Deserializes a token to the entry, updating its pending state.
    /// </summary>
    public static void DeserializeToEntry(IConfigEntry entry, ConfigValueLayer layer, JToken? token)
    {
        entry.BoxedDefaultDeserialize(layer, token);
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

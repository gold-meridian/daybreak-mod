using System.Collections.Generic;
using Newtonsoft.Json;
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
    ///     The mode in which it's running.
    /// </summary>
    public enum Mode
    {
        /// <summary>
        ///     The (de)serialization is for writing to or reading from a file
        ///     on disk.
        /// </summary>
        File,

        /// <summary>
        ///     The (de)serialization is for sending data over a network or
        ///     interpreting received data.
        /// </summary>
        Network,
    }

    /// <summary>
    ///     For serializing entry values.
    /// </summary>
    public delegate JToken? Serialize<T>(ConfigEntry<T> entry, Mode mode, T? value);

    /// <summary>
    ///     For deserializing entry values.
    /// </summary>
    public delegate T? Deserialize<T>(ConfigEntry<T> entry, Mode mode, JToken? token);

    /// <summary>
    ///     Serializes an entire category into a usable string.
    /// </summary>
    public static string SerializeCategory(
        ConfigRepository repository,
        ConfigCategory category,
        Mode mode,
        IEnumerable<IConfigEntry> entries
    )
    {
        var obj = new JObject();
        foreach (var entry in entries)
        {
            if (SerializeEntry(entry, mode) is not { } token)
            {
                continue;
            }
            
            obj[entry.Handle.FullName] = token;
        }

        return obj.ToString(mode == Mode.File ? Formatting.Indented : Formatting.None);
    }

    /// <summary>
    ///     Serializes the local value of the entry.
    /// </summary>
    public static JToken? SerializeEntry(IConfigEntry entry, Mode mode)
    {
        return entry.Serialize(mode, entry.LocalValue);
    }
}

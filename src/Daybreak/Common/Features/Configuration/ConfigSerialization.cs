using System;
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
    public delegate JToken? Serialize<T>(ConfigEntry<T> entry, Mode mode, T? value)
        where T : IEquatable<T>;

    /// <summary>
    ///     For deserializing entry values.
    /// </summary>
    public delegate T? Deserialize<T>(ConfigEntry<T> entry, Mode mode, JToken? token)
        where T : IEquatable<T>;
}

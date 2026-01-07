using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using Terraria.ModLoader.IO;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     Responsible for reading and writing config data of an arbitrary format.
///     <br />
///     Uses <see cref="JObject"/>s and <see cref="JToken"/>s as a unified
///     representation.
/// </summary>
public interface IConfigFormat
{
    /// <summary>
    ///     Writes the <paramref name="data"/> to the <paramref name="stream"/>.
    /// </summary>
    void Write(Stream stream, ConfigValueLayer layer, JObject data);

    /// <summary>
    ///     Reads the <paramref name="stream"/> into deserializable data.
    /// </summary>
    JObject Read(Stream stream, ConfigValueLayer layer);
}

/// <summary>
///     Well-known config format definitions.
/// </summary>
public static class WellKnownConfigFormats
{
    private static readonly JsonConfigFormat json = new();
    private static readonly BsonConfigFormat bson = new();
    // private static readonly NbtConfigFormat nbt = new();

    /// <summary>
    ///     Reads and writes files in JSON, a direct conversion to and from
    ///     <see cref="JObject"/>s.
    /// </summary>
    public static IConfigFormat Json => json;

    /// <summary>
    ///     Reads and writes files in BSON, a direct conversion to and from
    ///     <see cref="JObject"/>s.
    /// </summary>
    public static IConfigFormat Bson => bson;

    /*
    /// <summary>
    ///     Reads and writes files in Minecraft NBT, which tModLoader uses in
    ///     the form of <see cref="TagCompound"/>s.
    /// </summary>
    public static IConfigFormat Nbt => nbt;
    */
}

internal sealed class JsonConfigFormat : IConfigFormat
{
    void IConfigFormat.Write(Stream stream, ConfigValueLayer layer, JObject data)
    {
        var json = data.ToString(layer != ConfigValueLayer.Server ? Formatting.Indented : Formatting.None);
        var bytes = Encoding.UTF8.GetBytes(json);
        {
            stream.Write(bytes);
        }
    }

    public JObject Read(Stream stream, ConfigValueLayer layer)
    {
        using var sr = new StreamReader(stream);
        using var jr = new JsonTextReader(sr)
        {
            CloseInput = false,
        };

        try
        {
            return JObject.Load(jr);
        }
        catch
        {
            return new JObject();
        }
    }
}

#pragma warning disable CS0618 // Type or member is obsolete
internal sealed class BsonConfigFormat : IConfigFormat
{
    private static readonly JsonSerializer serializer = new();

    void IConfigFormat.Write(Stream stream, ConfigValueLayer layer, JObject data)
    {
        using var writer = new BsonWriter(stream)
        {
            CloseOutput = false,
        };

        serializer.Serialize(writer, data);
    }

    public JObject Read(Stream stream, ConfigValueLayer layer)
    {
        using var reader = new BsonReader(stream)
        {
            CloseInput = false,
        };

        try
        {
            return JObject.Load(reader);
        }
        catch
        {
            return new JObject();
        }
    }
}
#pragma warning restore CS0618 // Type or member is obsolete

/*
internal sealed class NbtConfigFormat : IConfigFormat
{
    void IConfigFormat.Write(Stream stream, ConfigValueLayer layer, JObject data)
    {
        throw new System.NotImplementedException();
    }

    public JObject Read(Stream stream, ConfigValueLayer layer)
    {
        throw new System.NotImplementedException();
    }
}
*/

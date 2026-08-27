using System.IO;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Networking.V1;

/// <summary>
///     A packet object that may be sent.
/// </summary>
public interface IPacket
{
    /// <summary>
    ///     Sends the packet to the <paramref name="destination"/>.
    /// </summary>
    /// <param name="destination">The packet destination.</param>
    void Send(PacketDestination destination);
}

/// <summary>
///     A strongly typed packet object that is automatically registered to the
///     containing mod.
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public interface IPacket<TSelf> : IPacket, ILoadable
    where TSelf : struct, IPacket<TSelf>
{
    /// <summary>
    ///     The ID of the packet.
    /// </summary>
    static virtual uint Id => PacketHandler.PacketId<TSelf>.Value;

    /// <summary>
    ///     The name of the packet.
    /// </summary>
    static virtual string Name => PacketHandler.PacketId<TSelf>.Name;

    void ILoadable.Load(Mod mod)
    {
        PacketHandler.Register<TSelf>(mod);
    }

    void ILoadable.Unload() { }

    void IPacket.Send(PacketDestination destination)
    {
        var packet = new FastModPacket(PacketHandler.PacketId<TSelf>.Mod, MessageID.ModPacket);
        PacketHandler.CreatePacket<TSelf>(in packet);
        {
            Write(packet.Writer);
        }
        packet.Send(destination);
    }

    /// <summary>
    ///     Serializes this packet's payload to <paramref name="w"/>.
    /// </summary>
    void Write(BinaryWriter w);

    /// <summary>
    ///     Deserializes the payload from <paramref name="r"/> and handles it.
    /// </summary>
    static abstract void Receive(BinaryReader r, int fromWho);
}

/// <summary>
///     Extensions to the packet interfaces.
/// </summary>
public static class PacketExtensions
{
    extension<T>(T packet)
        where T : IPacket
    {
        /// <inheritdoc cref="IPacket.Send"/>
        public void Send(PacketDestination dest)
        {
            packet.Send(dest);
        }

        /// <summary>
        ///     Sends the packet.  If sent from the client, sends it to the
        ///     server.  If sent from the server, sends it to all clients.
        /// </summary>
        public void Send()
        {
            packet.Send(PacketDestination.Broadcast);
        }
    }

    extension<TSelf>(IVanillaPacket<TSelf> packet)
        where TSelf : struct, IVanillaPacket<TSelf>
    {
        /// <inheritdoc cref="IPacket.Send"/>
        public void Send(PacketDestination destination)
        {
            packet.Send(destination);
        }

        /// <summary>
        ///     Sends the packet.  If sent from the client, sends it to the
        ///     server.  If sent from the server, sends it to all clients.
        /// </summary>
        public void Send()
        {
            packet.Send(PacketDestination.Broadcast);
        }
    }

    extension<TSelf>(IPacket<TSelf>)
        where TSelf : struct, IPacket<TSelf>
    {
        /// <inheritdoc cref="IPacket{TSelf}.Id"/>
        public static uint Id => TSelf.Id;

        /// <inheritdoc cref="IPacket{TSelf}.Name"/>
        public static string Name => TSelf.Name;
    }
}

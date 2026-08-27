using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Daybreak.Hooks;
using Daybreak.Hooks.V1;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Networking.V1;

/// <summary>
///     Central packet registry that assigns deterministic packet IDs and links
///     <see cref="Mod.HandlePacket"/> and <see cref="ModPacket"/> with the
///     corresponding <see cref="IPacket{TSelf}"/> abstractions.
/// </summary>
public static class PacketHandler
{
    internal static class PacketId<T> where T : struct, IPacket<T>
    {
        // ReSharper disable StaticMemberInGenericType
        public static uint Value { get; set; } = unregistered_id;

        public static string Name { get; set; } = typeof(T).FullName ?? typeof(T).Name;

        public static Mod? Mod { get; set; }
        // ReSharper restore StaticMemberInGenericType
    }

    private delegate void PacketDispatcher(BinaryReader r, int fromWho);

    private sealed record PendingPacket(
        string Name,
        PacketDispatcher Dispatcher,
        Action<uint> AssignId
    );

    private sealed record FinalizedPacket(
        string Name,
        PacketDispatcher? Dispatcher
    );

    private sealed class ModState
    {
        public FinalizedPacket[]? Packets { get; set; }

        public List<PendingPacket> Pending { get; } = [];

        public HashSet<string> Names { get; } = new(StringComparer.Ordinal);

        public byte PacketIdByteCount { get; set; } = 1;

        public bool HandleHandshakePacket { get; set; }
    }

    private const uint default_packet_count = 1;
    private const uint handshake_id = 0;
    private const uint unregistered_id = uint.MaxValue;

    private static readonly Dictionary<Mod, ModState> state_by_mod = [];

    /// <summary>
    ///     Registers the packet of type <typeparamref name="T" /> to the
    ///     <paramref name="mod" />.
    /// </summary>
    public static void Register<T>(Mod mod)
        where T : struct, IPacket<T>
    {
        var state = GetStateForMod(mod);
        if (state.Packets is not null)
        {
            throw new InvalidOperationException($"Cannot register packet type '{typeof(T).Name}' after packets have been finalized!");
        }

        PacketId<T>.Mod = mod;

        var packetName = T.Name;
        if (!state.Names.Add(packetName))
        {
            throw new InvalidOperationException($"Packet type '{typeof(T).Name}' was registered more than once!");
        }

        state.Pending.Add(
            new PendingPacket(
                packetName,
                T.Receive,
                SetPacketId
            )
        );

        return;

        // Separate function to avoid generating an anonymous delegate with a
        // pointless object instance.
        static void SetPacketId(uint id)
        {
            PacketId<T>.Value = id;
        }
    }

    [ModSystemHooks.PostSetupContent]
    private static void BuildPackets()
    {
        foreach (var (_, state) in state_by_mod)
        {
            // This is a possible solution, but kinda dumb.  We use a handshake
            // packet for validation, and we should just rely on tModLoader's
            // guaranteed load ordering.
            /*
            // We can reliably reorder these since it's just for syncing.  This
            // ensures misalignments only occur when there is actually a
            // discrepancy in which packets are present, rather than just load
            // order.
            state.Pending.Sort(static (a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            */

            var count = state.Pending.Count;
            state.PacketIdByteCount = GetPacketIdSize(count);

            state.Packets = new FinalizedPacket[count + default_packet_count];
            {
                state.Packets[handshake_id] = new FinalizedPacket("Handshake", null);
            }

            for (var i = 0; i < count; i++)
            {
                var id = i + default_packet_count;
                var pending = state.Pending[i];

                pending.AssignId((uint)id);
                state.Packets[id] = new FinalizedPacket(pending.Name, pending.Dispatcher);
            }

            state.Pending.Clear();
            state.Names.Clear();
        }
    }

    /// <summary>
    ///     Handles any incoming packets for the <paramref name="mod"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Handle(Mod mod, BinaryReader reader, int fromWho)
    {
        var state = GetStateForMod(mod);
        if (state.Packets is not { } dispatchers)
        {
            throw new InvalidOperationException($"Never finalized packets; cannot handle packets for mod: {mod.Name}");
        }

        if (Main.netMode == NetmodeID.MultiplayerClient && !state.HandleHandshakePacket)
        {
            mod.Logger.Debug("Received packet before handshake has been verified; assuming handshake packet.");

            try
            {
                var packetIdByteSize = reader.ReadByte();
                if (packetIdByteSize != state.PacketIdByteCount)
                {
                    DisconnectClient($"Packet ID size mismatch: expected '{state.PacketIdByteCount}' but got '{packetIdByteSize}'");
                    return;
                }

                var packetCount = reader.ReadInt32();
                if (packetCount != state.Packets.Length)
                {
                    DisconnectClient($"Packet count mismatch: expected '{state.Packets.Length}' but got '{packetCount}'");
                    return;
                }

                for (var i = 0; i < packetCount; i++)
                {
                    var packetName = reader.ReadString();
                    if (string.Equals(packetName, state.Packets[i].Name))
                    {
                        continue;
                    }

                    DisconnectClient($"Packet name mismatch: expected '{state.Packets[i].Name}' but got '{packetName}'");
                    return;
                }
            }
            catch (Exception e)
            {
                mod.Logger.Error("Failed to decode anticipated handshake packet!", e);
                DisconnectClient($"Handshake packet for mod '{mod.Name}' failed; check logs");
                throw;
            }

            state.HandleHandshakePacket = true;
            return;
        }

        var id = ReadPacketId(reader, state.PacketIdByteCount);
        if (id >= (uint)dispatchers.Length)
        {
            // This will cause a read underflow, but whatever.
            mod.Logger.Error($"Unknown packet ID '{id}' from whoAmI={fromWho}; this likely indicates a registration mismatch between the client and the server.");
            return;
        }

        state.Packets[id].Dispatcher?.Invoke(reader, fromWho);

        return;

        static void DisconnectClient(string reason)
        {
            Netplay.Disconnect = true;
            Main.statusText = reason; // TODO: Localization NetworkText.Deserialize(reader).ToString();

            // Added by TML.
            Main.menuMode = MenuID.MultiplayerJoining;
        }
    }

    internal static void CreatePacket<T>(in FastModPacket packet)
        where T : struct, IPacket<T>
    {
        var id = T.Id;
        if (id == unregistered_id)
        {
            throw new InvalidOperationException($"The packet '{T.Name}' has not been registered and cannot be sent (was it loaded?).");
        }

        var mod = PacketId<T>.Mod;
        if (mod is null)
        {
            throw new InvalidOperationException($"The packet '{T.Name}' has no corresponding mod registered and cannot be sent (was it loaded?).");
        }

        var writer = packet.Writer;
        {
            if (ModNet.NetModCount < 256)
            {
                writer.Write((byte)mod.netID);
            }
            else
            {
                writer.Write(mod.netID);
            }
        }

        WritePacketId(writer, id, GetStateForMod(mod).PacketIdByteCount);
    }

    [OnLoad]
    private static void ApplyHooks()
    {
        On_NetMessage.SendData += SendHandshakePacket;
    }

    private static void SendHandshakePacket(
        On_NetMessage.orig_SendData orig,
        int msgType,
        int remoteClient,
        int ignoreClient,
        NetworkText text,
        int number,
        float number2,
        float number3,
        float number4,
        int number5,
        int number6,
        int number7
    )
    {
        orig(
            msgType,
            remoteClient,
            ignoreClient,
            text,
            number,
            number2,
            number3,
            number4,
            number5,
            number6,
            number7
        );

        if (Main.netMode != NetmodeID.Server)
        {
            return;
        }

        if (msgType != MessageID.PlayerInfo)
        {
            return;
        }

        foreach (var (mod, state) in state_by_mod)
        {
            mod.Logger.Debug($"[Daybreak.Networking] Sending handshake packet to: {remoteClient}");

            if (state.Packets is null)
            {
                mod.Logger.Warn("[Daybreak.Networking] Packets are uninitialized?!");
                continue;
            }

            var packet = new FastModPacket(mod, MessageID.ModPacket);
            var writer = packet.Writer;
            {
                if (ModNet.NetModCount < 256)
                {
                    writer.Write((byte)mod.netID);
                }
                else
                {
                    writer.Write(mod.netID);
                }
            }

            writer.Write(state.PacketIdByteCount);
            writer.Write(state.Packets.Length);
            foreach (var packetDef in state.Packets)
            {
                writer.Write(packetDef.Name);
            }

            // whoAmI is more correct, but I'm paranoid.
            packet.Send(PacketDestination.Only(remoteClient));
        }
    }

    [ModSystemHooks.HijackGetData]
    private static void AnticipateHandshakePacket(ref byte messageType, ref BinaryReader reader, int playerNumber)
    {
        if (Main.netMode != NetmodeID.MultiplayerClient || messageType != MessageID.PlayerInfo)
        {
            return;
        }

        foreach (var (_, state) in state_by_mod)
        {
            state.HandleHandshakePacket = false;
        }
    }

    private static ModState GetStateForMod(Mod mod)
    {
        if (!state_by_mod.TryGetValue(mod, out var state))
        {
            state_by_mod[mod] = state = new ModState();
        }

        return state;
    }

    private static byte GetPacketIdSize(int packetCount)
    {
        if (packetCount <= byte.MaxValue)
        {
            return 1;
        }

        if (packetCount <= ushort.MaxValue)
        {
            return 2;
        }

        if (packetCount <= 0xFFFFFF)
        {
            return 3;
        }

        return 4;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void WritePacketId(BinaryWriter writer, uint id, byte byteCount)
    {
        switch (byteCount)
        {
            case 1:
                writer.Write((byte)id);
                break;

            case 2:
                writer.Write((ushort)id);
                break;

            case 3:
                writer.Write((byte)id);
                writer.Write((ushort)(id >> 8));
                break;

            case 4:
                writer.Write(id);
                break;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint ReadPacketId(BinaryReader reader, byte byteCount)
    {
        return byteCount switch
        {
            1 => reader.ReadByte(),
            2 => reader.ReadUInt16(),
            3 => (uint)(reader.ReadByte() | (reader.ReadUInt16() << 8)),
            4 => reader.ReadUInt32(),
            _ => throw new InvalidOperationException(),
        };
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Daybreak.Hooks;
using Terraria.ModLoader;

namespace Daybreak.Networking;

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
        public static ushort Value { get; set; } = unregistered_id;

        public static string Name { get; set; } = typeof(T).FullName ?? typeof(T).Name;

        public static Mod? Mod { get; set; }
        // ReSharper restore StaticMemberInGenericType
    }

    private delegate void PacketDispatcher(BinaryReader r, int fromWho);

    private sealed record PendingPacket(
        string Name,
        PacketDispatcher Dispatcher,
        Action<ushort> AssignId
    );

    private sealed class ModState
    {
        public PacketDispatcher[]? Dispatchers { get; set; }

        public List<PendingPacket> Pending { get; } = [];

        public HashSet<string> Names { get; } = new(StringComparer.Ordinal);
    }

    // TODO: Add a handshake packet to confirm packet presence and order... guh
    // private const ushort HANDSHAKE_ID = 0;
    private const ushort unregistered_id = ushort.MaxValue;

    private static readonly Dictionary<Mod, ModState> state_by_mod = [];

    /// <summary>
    ///     Registers the packet of type <typeparamref name="T" /> to the
    ///     <paramref name="mod" />.
    /// </summary>
    public static void Register<T>(Mod mod)
        where T : struct, IPacket<T>
    {
        var state = GetStateForMod(mod);
        if (state.Dispatchers is not null)
        {
            throw new InvalidOperationException($"Cannot register packet type '{typeof(T).Name}' after dispatchers have been finalized!");
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
        static void SetPacketId(ushort id)
        {
            PacketId<T>.Value = id;
        }
    }

    [ModSystemHooks.PostSetupContent]
    private static void BuildPackets()
    {
        // For if we ever add the handshake packet.
        const int default_packet_count = 0;

        foreach (var (_, state) in state_by_mod)
        {
            // We can reliably reorder these since it's just for syncing.  This
            // ensures misalignments only occur when there is actually a
            // discrepancy in which packets are present, rather than just load
            // order.
            state.Pending.Sort(static (a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

            var count = state.Pending.Count;
            if (count > ushort.MaxValue - 1)
            {
                throw new InvalidOperationException($"Too many packet types registered ({count}); maximum is {ushort.MaxValue - 1}.");
            }

            state.Dispatchers = new PacketDispatcher[count + default_packet_count];

            for (var i = 0; i < count; i++)
            {
                var id = i + default_packet_count;
                var pending = state.Pending[id];
                pending.AssignId((ushort)id);
                state.Dispatchers[id] = pending.Dispatcher;
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
        if (state.Dispatchers is not { } dispatchers)
        {
            throw new InvalidOperationException($"Never finalized packet dispatchers; cannot handle packets for mod: {mod.Name}");
        }

        var id = reader.ReadUInt16();
        if (id >= (uint)dispatchers.Length)
        {
            // This will cause a read underflow, but whatever.
            mod.Logger.Error($"Unknown packet ID '{id}' from whoAmI={fromWho}; this likely indicates a registration mismatch between the client and the server.");
            return;
        }

        state.Dispatchers[id].Invoke(reader, fromWho);
    }

    // TODO: Decide whether to keep this API as-is or provide custom
    //       implementation?
    internal static ModPacket CreatePacket<T>()
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

        var mp = mod.GetPacket();
        mp.Write(id);

        return mp;
    }

    private static ModState GetStateForMod(Mod mod)
    {
        if (!state_by_mod.TryGetValue(mod, out var state))
        {
            state_by_mod[mod] = state = new ModState();
        }

        return state;
    }
}

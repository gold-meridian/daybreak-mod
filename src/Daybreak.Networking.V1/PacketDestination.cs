using System.Runtime.CompilerServices;

namespace Daybreak.Networking.V1;

/// <summary>
///     Describes the routing target of a packet send request.
/// </summary>
public readonly struct PacketDestination
{
    /// <summary>
    ///     From a client: sends to the server.
    ///     <br />
    ///     From the server: broadcasts to every connected client.
    /// </summary>
    public static PacketDestination Broadcast { get; } = new(toClient: -1, ignoreClient: -1);

    /// <summary>
    ///     The client ID to send to, if sending from the server.
    /// </summary>
    public int ToClient { get; }

    /// <summary>
    ///     The client ID to ignore if sending from the server.
    /// </summary>
    public int IgnoreClient { get; }

    private PacketDestination(int toClient, int ignoreClient)
    {
        ToClient = toClient;
        IgnoreClient = ignoreClient;
    }

    /// <summary>
    ///     Sends the packet from the server to only the specified client.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketDestination Only(int clientId)
    {
        return new PacketDestination(toClient: clientId, ignoreClient: -1);
    }

    /// <summary>
    ///     Sends the packet from the server to all clients but the specified
    ///     client.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketDestination AllExcept(int clientId)
    {
        return new PacketDestination(toClient: -1, ignoreClient: clientId);
    }

    /// <summary>
    ///     Dangerously constructs a packet destination from vanilla values.
    ///     <br />
    ///     <br />
    ///     This should only be used when converting from vanilla paremeters, as
    ///     it may be used to represent illegal state.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketDestination DangerouslyFromVanillaValues(int toClient, int ignoreClient)
    {
        return new PacketDestination(toClient, ignoreClient);
    }
}

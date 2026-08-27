using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace Daybreak.Networking.V1;

/// <summary>
///     A chiefly stack-allocated record of vanilla <see cref="NetMessage"/>
///     parameters.
/// </summary>
public ref struct VanillaPacketData
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public NetworkText? Text;
    public int Number1;
    public float Number2;
    public float Number3;
    public float Number4;
    public int Number5;
    public int Number6;
    public int Number7;
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

/// <summary>
///     A wrapper around a vanilla packet, allowing it to be constructed and
///     sent with type safety.
/// </summary>
/// <typeparam name="TSelf"></typeparam>
public interface IVanillaPacket<TSelf> : IPacket
    where TSelf : struct, IVanillaPacket<TSelf>
{
    /// <summary>
    ///     The corresponding vanilla <see cref="MessageID"/>.
    /// </summary>
    static abstract int NetMessageId { get; }

    void IPacket.Send(PacketDestination destination)
    {
        var data = new VanillaPacketData();
        {
            Write(ref data);
        }

        NetMessage.TrySendData(
            TSelf.NetMessageId,
            remoteClient: destination.ToClient,
            ignoreClient: destination.IgnoreClient,
            text: data.Text,
            number: data.Number1,
            number2: data.Number2,
            number3: data.Number3,
            number4: data.Number4,
            number5: data.Number5,
            number6: data.Number6,
            number7: data.Number7
        );
    }

    /// <summary>
    ///     Writes the packet data to the provided vanilla buffer.
    /// </summary>
    void Write(ref VanillaPacketData data);
}

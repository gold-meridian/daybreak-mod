using System;
using System.Buffers;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Networking;

/// <summary>
///     Reimplements the functionality of <see cref="ModPacket"/>.
/// </summary>
internal readonly ref struct FastModPacket
{
    private static readonly MemoryStream stream = new([]);
    private static readonly BinaryWriter writer = new(stream);

    public BinaryWriter Writer => writer;

    private readonly int netId;

    public FastModPacket(Mod? mod, byte messageId)
    {
        if (mod is not null)
        {
            if (mod.netID < 0)
            {
                if (Main.netMode == NetmodeID.SinglePlayer)
                {
                    throw new InvalidOperationException("Cannot create packet buffer in singleplayer");
                }

                throw new InvalidOperationException($"Cannot create packet buffer for '{mod.Name}' because it does not exist on the {(Main.dedServ ? "client" : "server")}");
            }

            netId = mod.netID;
        }
        else
        {
            netId = -1;
        }

        ResetBuffer();
        WriteModPacketHeader(messageId);
    }

    public void Send(PacketDestination destination)
    {
        var length = Finish();

        if (ModNet.DetailedLogging)
        {
            ModNet.LogSend(destination.ToClient, destination.IgnoreClient, $"ModPacket.Send {ModNet.GetMod(netId)?.Name ?? "ModLoader"}({netId})", length);
        }

        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            Netplay.Connection.Socket.AsyncSend(buffer, 0, length, SendCallback, buffer);

            if (netId >= 0)
            {
                // TODO: Better?!
                ModNet.ModNetDiagnosticsUI.CountSentMessage(netId, length);
            }
        }
        else if (destination.ToClient != -1)
        {
            Netplay.Clients[destination.ToClient].Socket.AsyncSend(buffer, 0, length, SendCallback, buffer);
        }
        else
        {
            for (var i = 0; i < 256; i++)
            {
                if (i != destination.IgnoreClient && Netplay.Clients[i].IsConnected() && NetMessage.buffer[i].broadcast)
                {
                    Netplay.Clients[i].Socket.AsyncSend(buffer, 0, length, SendCallback, buffer);
                }
            }
        }

        return;

        static void SendCallback(object obj)
        {
            if (obj is not byte[] usedBuffer)
            {
                return;
            }

            ArrayPool<byte>.Shared.Return(usedBuffer);
        }
    }

    private static ushort Finish(out byte[] buffer)
    {
        if (stream.Position > ushort.MaxValue)
        {
            throw new Exception(Language.GetTextValue("tModLoader.MPPacketTooLarge", stream.Position, ushort.MaxValue));
        }

        var length = (ushort)stream.Position;

        stream.Seek(0, SeekOrigin.Begin);
        {
            writer.Write(length);
        }

        buffer = MemoryStreamAccessor.GetBufferRef(stream);
        return length;
    }

    private static void WriteModPacketHeader(byte messageId)
    {
        writer.Write((ushort)0);
        writer.Write(messageId);
    }

    private static void ResetBuffer()
    {
        ref var buffer = ref MemoryStreamAccessor.GetBufferRef(stream);

        // Special case upon initialization.
        if (buffer.Length == 0)
        {
            buffer = InitBuffer();
            MemoryStreamAccessor.GetLengthRef(stream) = buffer.Length;
            return;
        }

        var written = (int)stream.Position;
        if (written == 0 && stream.Length == 0)
        {
            return;
        }

        // Clearing is not necessary under normal circumstances, since
        // BinaryWriter itself does not expose incrementing Position.
        // Unfortunately, the underlying Stream can be accessed, which does
        // expose an API for this.
        Array.Clear(buffer, 0, written);

        stream.Position = 0;
        stream.SetLength(0);
    }

    private static byte[] InitBuffer()
    {
        return ArrayPool<byte>.Shared.Rent(ushort.MaxValue);
    }
}

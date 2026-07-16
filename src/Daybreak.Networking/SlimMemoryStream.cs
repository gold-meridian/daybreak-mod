using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Daybreak.Networking;

internal sealed class SlimMemoryStream : Stream
{
    public override bool CanRead => true;

    public override bool CanSeek => true;

    public override bool CanWrite => true;

    public override long Length => highestPos;

    public override long Position
    {
        get => pos;

        set
        {
            pos = (int)value;

            // Handle users possibly jumping ahead, expecting the array to
            // already be cleared.
            var diff = pos - highestPos;
            if (diff > 0)
            {
                Array.Clear(buf, highestPos, diff);
            }

            highestPos = int.Max(pos, highestPos);
        }
    }

    private byte[] buf = [];
    private int len;

    private int pos;
    private int highestPos;

    public void SetBuffer(byte[] buffer, int length)
    {
        buf = buffer;
        len = length;

        pos = 0;
        highestPos = 0;
    }

    internal byte[] GetBuffer()
    {
        return buf;
    }

    public override void Flush() { }

    public override Task FlushAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        return Read(new Span<byte>(buffer, offset, count));
    }

    public override int Read(Span<byte> buffer)
    {
        var endPos = pos + buffer.Length;
        if (endPos > len)
        {
            throw new ArgumentOutOfRangeException(nameof(buffer), "Buffer is too large");
        }

        var diff = endPos - highestPos;
        if (diff > 0)
        {
            Array.Clear(buf, highestPos, diff);
        }

        buf.AsSpan(pos, buffer.Length).CopyTo(buffer);
        {
            pos = endPos;
            highestPos = int.Max(highestPos, pos);
        }

        return buffer.Length;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        switch (origin)
        {
            case SeekOrigin.Begin:
                pos = int.Max(0, int.Min(len, (int)offset));
                break;

            case SeekOrigin.Current:
                pos = int.Max(0, int.Min(len, pos + (int)offset));
                break;

            case SeekOrigin.End:
                pos = int.Max(0, int.Min(len, len + (int)offset));
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(origin), origin, null);
        }

        return pos;
    }

    public override void SetLength(long value)
    {
        highestPos = (int)value;

        if (pos > highestPos)
        {
            pos = highestPos;
        }
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        Write(new ReadOnlySpan<byte>(buffer, offset, count));
    }

    public override void Write(ReadOnlySpan<byte> buffer)
    {
        if (pos + buffer.Length > len)
        {
            throw new ArgumentOutOfRangeException(nameof(buffer), "Buffer is too large");
        }

        buffer.CopyTo(buf.AsSpan(pos));
        {
            pos += buffer.Length;
            highestPos = int.Max(highestPos, pos);
        }
    }
}

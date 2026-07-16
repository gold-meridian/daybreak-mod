using System.IO;
using System.Runtime.CompilerServices;

namespace Daybreak.Networking;

internal static class MemoryStreamAccessor
{
    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_buffer")]
    public static extern ref byte[] GetBufferRef(MemoryStream self);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_length")]
    public static extern ref int GetLengthRef(MemoryStream self);
}

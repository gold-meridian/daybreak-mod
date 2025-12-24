using System.Runtime.CompilerServices;

namespace Daybreak.Common.Mathematics;

internal static class NoiseHelper
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Hash(uint x)
    {
        x ^= x >> 16;
        x *= 0x7feb352d;
        x ^= x >> 15;
        x *= 0x846ca68b;
        x ^= x >> 16;
        return x;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Hash(int x, int y, int seed)
    {
        var h = Hash((uint)x * 0x45d9f3u ^ (uint)y * 0x27d4eb2du ^ (uint)seed * 0x165667b1u);
        return h;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float HashFloat(int x, int y, int seed)
    {
        return (Hash(x, y, seed) & 0xffffffu) / 16777215f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Fade(float t)
    {
        return t * t * t * (t * (t * 6f - 15f) + 10f);
    }
}

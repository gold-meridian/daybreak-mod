using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     Generic operations on functions implementing
///     <see cref="INoise2d{TSelf,TSettings}"/>.
/// </summary>
public static class NoiseOperations
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Fbm<TNoise>(
        Vector2 p,
        int octaves = 4,
        float lacunarity = 2f,
        float gain = 0.5f,
        float scale = 1f
    ) where TNoise : INoise2d<TNoise>
    {
        return Fbm(
            p,
            TNoise.DefaultSettings(),
            octaves,
            lacunarity,
            gain,
            scale
        );
    }

    public static float Fbm<TNoise>(
        Vector2 p,
        TNoise settings,
        int octaves = 4,
        float lacunarity = 2f,
        float gain = 0.5f,
        float scale = 1f
    ) where TNoise : INoise2d<TNoise>
    {
        var amp = 1f;
        var freq = 1f;
        var sum = 0f;
        var norm = 0f;

        var seed = settings.Seed;
        for (var i = 0; i < octaves; i++)
        {
            settings.Seed = seed + i * 53;

            sum += TNoise.Sample(p * freq * scale, settings) * amp;
            norm += amp;
            amp *= gain;
            freq *= lacunarity;
        }

        return norm > 0f ? sum / norm : 0.5f;
    }
}

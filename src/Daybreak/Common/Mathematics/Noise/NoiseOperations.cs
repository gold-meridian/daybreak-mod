using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     Generic operations on functions implementing
///     <see cref="INoise2d{TSelf}"/>.
/// </summary>
public static class NoiseOperations
{
#region fBm
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Fbm<TNoise>(
        Vector2 p,
        int octaves = 4,
        float lacunarity = 2f,
        float gain = 0.5f,
        float scale = 1f
    ) where TNoise : unmanaged, INoise2d<TNoise>
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
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        var amp = 1f;
        var freq = 1f;
        var sum = 0f;
        var norm = 0f;

        for (var i = 0; i < octaves; i++)
        {
            sum += TNoise.Sample(p * freq * scale, settings with { Seed = settings.Seed + i * 53 }) * amp;
            norm += amp;
            amp *= gain;
            freq *= lacunarity;
        }

        return norm > 0f ? sum / norm : 0.5f;
    }
#endregion

#region Billow
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Billow<TNoise>(
        Vector2 p,
        int octaves = 5,
        float lacunarity = 2f,
        float gain = 0.5f,
        float scale = 1f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        return Billow(
            p,
            TNoise.DefaultSettings(),
            octaves,
            lacunarity,
            gain,
            scale
        );
    }

    public static float Billow<TNoise>(
        Vector2 p,
        TNoise settings,
        int octaves = 4,
        float lacunarity = 2f,
        float gain = 0.5f,
        float scale = 1f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        var amp = 1f;
        var freq = 1f;
        var sum = 0f;
        var norm = 0f;

        for (var i = 0; i < octaves; i++)
        {
            var sample = TNoise.Sample(p * freq * scale, settings with { Seed = settings.Seed + i * 73 });
            {
                sample = 2f * MathF.Abs(sample - 0.5f);
            }

            sum += sample * amp;
            norm += amp;
            amp *= gain;
            freq *= lacunarity;
        }

        return norm > 0f ? Math.Clamp(sum / norm, 0f, 1f) : 0.5f;
    }
#endregion

#region Turbulence
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Turbulence<TNoise>(
        Vector2 p,
        int octaves = 5,
        float lacunarity = 2f,
        float gain = 0.5f,
        float scale = 1f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        return Turbulence(
            p,
            TNoise.DefaultSettings(),
            octaves,
            lacunarity,
            gain,
            scale
        );
    }

    public static float Turbulence<TNoise>(
        Vector2 p,
        TNoise settings,
        int octaves = 5,
        float lacunarity = 2f,
        float gain = 0.5f,
        float scale = 1f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        var amp = 1f;
        var freq = 1f;
        var sum = 0f;
        var norm = 0f;

        for (var i = 0; i < octaves; i++)
        {
            var sample = TNoise.Sample(p * freq * scale, settings with { Seed = settings.Seed + i * 97 });
            sum += MathF.Abs(sample - 0.5f) * 2f * amp;
            norm += amp;
            amp *= gain;
            freq *= lacunarity;
        }

        return norm > 0f ? Math.Clamp(sum / norm, 0f, 1f) : 0.5f;
    }
#endregion
}

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

#region Hybrid Multifractal
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float HybridMultifractal(
        Vector2 p,
        int octaves = 5,
        float lacunarity = 2f,
        float gain = 0.5f,
        float offset = 0.7f,
        float scale = 1f
    )
    {
        return HybridMultifractal<FastSimplexNoise>(
            p,
            octaves,
            lacunarity,
            gain,
            scale
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float HybridMultifractal(
        Vector2 p,
        FastSimplexNoise settings,
        int octaves = 5,
        float lacunarity = 2f,
        float gain = 0.5f,
        float offset = 0.7f,
        float scale = 1f
    )
    {
        return HybridMultifractal<FastSimplexNoise>(
            p,
            settings,
            octaves,
            lacunarity,
            gain,
            scale
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float HybridMultifractal<TNoise>(
        Vector2 p,
        int octaves = 5,
        float lacunarity = 2f,
        float gain = 0.5f,
        float offset = 0.7f,
        float scale = 1f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        return HybridMultifractal(
            p,
            TNoise.DefaultSettings(),
            octaves,
            lacunarity,
            gain,
            scale
        );
    }

    public static float HybridMultifractal<TNoise>(
        Vector2 p,
        TNoise settings,
        int octaves = 5,
        float lacunarity = 2f,
        float gain = 0.5f,
        float offset = 0.7f,
        float scale = 1f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        var freq = 1f;
        var weight = 1f;
        var value = TNoise.Sample(p * freq * scale, settings) + offset;
        var amp = 1f;

        for (var i = 1; i < octaves; i++)
        {
            if (weight <= 0f)
            {
                break;
            }

            freq *= lacunarity;
            amp *= gain;

            var signal = TNoise.Sample(p * freq * scale, settings with { Seed = settings.Seed + i * 131 }) + offset;
            signal *= amp;
            value += weight * signal;
            weight *= signal;
        }

        return Math.Clamp(value * 0.5f, 0f, 1f);
    }
#endregion
}

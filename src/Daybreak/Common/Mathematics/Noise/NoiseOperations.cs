using System;
using System.Buffers;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

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
            offset,
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
            offset,
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
            offset,
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

#region DomainWarp
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float DomainWarp(
        Vector2 p,
        float amplitude = 1.5f,
        int iterations = 2
    )
    {
        return DomainWarp<FastSimplexNoise>(
            p,
            amplitude,
            iterations
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float DomainWarp(
        Vector2 p,
        FastSimplexNoise settings,
        float amplitude = 1.5f,
        int iterations = 2
    )
    {
        return DomainWarp<FastSimplexNoise>(
            p,
            settings,
            amplitude,
            iterations
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float DomainWarp<TNoise>(
        Vector2 p,
        float amplitude = 1.5f,
        int iterations = 2
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        return DomainWarp(
            p,
            TNoise.DefaultSettings(),
            amplitude,
            iterations
        );
    }

    public static float DomainWarp<TNoise>(
        Vector2 p,
        TNoise settings,
        float amplitude = 1.5f,
        int iterations = 2
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        var warp = DomainWarpVector2(p, settings, amplitude, iterations);
        return TNoise.Sample(p + warp, settings with { Seed = settings.Seed + iterations * 97 });
    }
#endregion

#region DomainWarpVector2
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 DomainWarpVector2(
        Vector2 p,
        float amplitude = 1.5f,
        int iterations = 2
    )
    {
        return DomainWarpVector2<FastSimplexNoise>(
            p,
            amplitude,
            iterations
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 DomainWarpVector2(
        Vector2 p,
        FastSimplexNoise settings,
        float amplitude = 1.5f,
        int iterations = 2
    )
    {
        return DomainWarpVector2<FastSimplexNoise>(
            p,
            settings,
            amplitude,
            iterations
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 DomainWarpVector2<TNoise>(
        Vector2 p,
        float amplitude = 1.5f,
        int iterations = 2
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        return DomainWarpVector2(
            p,
            TNoise.DefaultSettings(),
            amplitude,
            iterations
        );
    }

    public static Vector2 DomainWarpVector2<TNoise>(
        Vector2 p,
        TNoise settings,
        float amplitude = 1.5f,
        int iterations = 2
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        var warp = Vector2.Zero;
        var amp = amplitude;
        var freq = 1f;

        for (var i = 0; i < iterations; i++)
        {
            var nx = TNoise.Sample(p * freq + warp, settings with { Seed = settings.Seed + i * 61 });
            var ny = TNoise.Sample(p * freq + warp + new Vector2(17.3f, 43.7f), settings with { Seed = settings.Seed + i * 61 + 1 });

            warp += new Vector2(nx, ny) * amp;
            freq *= 2f;
            amp *= 0.5f;
        }

        return warp;
    }
#endregion

#region Curl
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Curl(
        Vector2 p,
        float epsilon = 0.5f
    )
    {
        return Curl<FastSimplexNoise>(
            p,
            epsilon
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Curl(
        Vector2 p,
        FastSimplexNoise settings,
        float epsilon = 0.5f
    )
    {
        return Curl<FastSimplexNoise>(
            p,
            settings,
            epsilon
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Curl<TNoise>(
        Vector2 p,
        float epsilon = 0.5f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        return Curl(
            p,
            TNoise.DefaultSettings(),
            epsilon
        );
    }

    public static Vector2 Curl<TNoise>(
        Vector2 p,
        TNoise settings,
        float epsilon = 0.5f
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        var dx = new Vector2(epsilon, 0f);
        var dy = new Vector2(0f, epsilon);

        var n1 = TNoise.Sample(p + dy, settings);
        var n2 = TNoise.Sample(p - dy, settings);
        var n3 = TNoise.Sample(p + dx, settings with { Seed = settings.Seed + 17 });
        var n4 = TNoise.Sample(p - dx, settings with { Seed = settings.Seed + 17 });

        var dnx = (n1 - n2) / (2f * epsilon);
        var dny = (n3 - n4) / (2f * epsilon);
        return new Vector2(dnx, -dny);
    }
#endregion

#region FillNoiseMap
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void FillNoiseMap<TNoise>(
        Rectangle area,
        Span<float> destination,
        bool parallel = true
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        FillNoiseMap(
            area,
            TNoise.DefaultSettings(),
            destination,
            parallel
        );
    }

    public static void FillNoiseMap<TNoise>(
        Rectangle area,
        TNoise settings,
        Span<float> destination,
        bool parallel = true
    ) where TNoise : unmanaged, INoise2d<TNoise>
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, area.Width * area.Height);

        var width = area.Width;

        if (parallel && area.Height > 8)
        {
            var temp = ArrayPool<float>.Shared.Rent(destination.Length);

            Parallel.For(
                area.Top,
                area.Bottom,
                y =>
                {
                    var rowIndex = (y - area.Top) * width;
                    for (var x = area.Left; x < area.Right; x++)
                    {
                        temp[rowIndex + (x - area.Left)] = TNoise.Sample(new Vector2(x, y), settings);
                    }
                }
            );

            temp.AsSpan().CopyTo(destination);
            ArrayPool<float>.Shared.Return(temp);

            return;
        }

        for (var y = area.Top; y < area.Bottom; y++)
        {
            var rowIndex = (y - area.Top) * width;

            for (var x = area.Left; x < area.Right; x++)
            {
                destination[rowIndex + (x - area.Left)] = TNoise.Sample(new Vector2(x, y), settings);
            }
        }
    }
#endregion

#region Buffer operations
    public static void Normalize(Span<float> buffer)
    {
        if (buffer.IsEmpty)
        {
            return;
        }

        var length = buffer.Length;
        var vecWidth = Vector<float>.Count;
        var i = 0;

        var vMin = new Vector<float>(float.MaxValue);
        var vMax = new Vector<float>(float.MinValue);

        for (; i <= length - vecWidth; i += vecWidth)
        {
            var v = new Vector<float>(buffer[i..]);
            vMin = Vector.Min(vMin, v);
            vMax = Vector.Max(vMax, v);
        }

        var min = vMin[0];
        var max = vMax[0];
        for (var j = 1; j < vecWidth; j++)
        {
            min = MathF.Min(min, vMin[j]);
            max = MathF.Max(max, vMax[j]);
        }

        for (; i < length; i++)
        {
            var v = buffer[i];
            min = MathF.Min(min, v);
            max = MathF.Max(max, v);
        }

        var range = MathF.Max(1e-6f, max - min);

        vMin = new Vector<float>(min);
        var vRange = new Vector<float>(range);
        var vZero = Vector<float>.Zero;
        var vOne = Vector<float>.One;

        for (i = 0; i <= length - vecWidth; i += vecWidth)
        {
            var v = new Vector<float>(buffer[i..]);
            v = (v - vMin) / vRange;
            v = Vector.Min(Vector.Max(v, vZero), vOne);
            v.CopyTo(buffer[i..]);
        }

        for (; i < length; i++)
        {
            buffer[i] = Math.Clamp((buffer[i] - min) / range, 0f, 1f);
        }
    }

    public static void ApplyCurve(Span<float> buffer, Func<float, float> curve)
    {
        for (var i = 0; i < buffer.Length; i++)
        {
            buffer[i] = Math.Clamp(curve(buffer[i]), 0f, 1f);
        }
    }
#endregion
}

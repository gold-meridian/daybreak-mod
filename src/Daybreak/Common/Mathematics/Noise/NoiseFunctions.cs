using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     Settings for a <see cref="INoise2d{TSelf,TSettings}"/> function.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
public interface INoise2dSettings<out TSelf>
{
    /// <summary>
    ///     The seed to use when sampling.
    /// </summary>
    int Seed { get; set; }

    /// <summary>
    ///     Initializes an instance of the settings with its default
    ///     configuration.
    /// </summary>
    static abstract TSelf DefaultSettings();
}

/// <summary>
///     A 2-dimensional noise function.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
/// <typeparam name="TSettings">The settings that may configure a sample.</typeparam>
public interface INoise2d<TSelf, in TSettings>
    where TSelf : INoise2d<TSelf, TSettings>
    where TSettings : INoise2dSettings<TSettings>
{
    /// <inheritdoc cref="Sample(Vector2, TSettings)"/>
    static virtual float Sample(Vector2 p)
    {
        return TSelf.Sample(p, TSettings.DefaultSettings());
    }

    /// <summary>
    ///     Samples the noise function at the point <paramref name="p"/>,
    ///     returning a scalar value.
    ///     <br />
    ///     This is a statically-provided 
    /// </summary>
    /// <param name="p">The point to sample at.</param>
    /// <param name="settings">The settings to use for this sample.</param>
    /// <returns>The scalar value resulting from the sample.</returns>
    static abstract float Sample(
        Vector2 p,
        TSettings settings
    );
}

/// <summary>
///     A fast implementation of Value noise.
/// </summary>
public readonly struct FastValueNoise : INoise2d<FastValueNoise, FastValueNoise.Settings>
{
    /// <inheritdoc cref="INoise2dSettings{TSelf}"/>
    public record struct Settings(
        int Seed = 0
    ) : INoise2dSettings<Settings>
    {
        /// <inheritdoc />
        public static Settings DefaultSettings()
        {
            return new Settings(Seed: 0);
        }
    }

    /// <inheritdoc />
    public static float Sample(
        Vector2 p,
        Settings settings
    )
    {
        var ix = (int)MathF.Floor(p.X);
        var iy = (int)MathF.Floor(p.Y);

        var fx = (float)ix;
        var fy = (float)iy;

        var v00 = NoiseOperations.HashFloat(ix, iy, settings.Seed);
        var v10 = NoiseOperations.HashFloat(ix + 1, iy, settings.Seed);
        var v01 = NoiseOperations.HashFloat(ix, iy + 1, settings.Seed);
        var v11 = NoiseOperations.HashFloat(ix + 1, iy + 1, settings.Seed);

        var u = NoiseOperations.Fade(fx);
        var v = NoiseOperations.Fade(fy);

        var x0 = Interpolate.Lerp(v00, v10, u);
        var x1 = Interpolate.Lerp(v01, v11, u);
        return Interpolate.Lerp(x0, x1, v);
    }
}

/// <summary>
///     A fast implementation of Simplex noise.
/// </summary>
public readonly struct FastSimplexNoise : INoise2d<FastSimplexNoise, FastSimplexNoise.Settings>
{
    private static readonly Vector2[] simplex_gradients =
    [
        new(1f, 1f), new(-1f, 1f), new(1f, -1f), new(-1f, -1f),
        new(1f, 0f), new(-1f, 0f), new(0f, 1f), new(0f, -1f),
    ];

    /// <inheritdoc cref="INoise2dSettings{TSelf}"/>
    public record struct Settings(
        int Seed = 0
    ) : INoise2dSettings<Settings>
    {
        /// <inheritdoc />
        public static Settings DefaultSettings()
        {
            return new Settings(Seed: 0);
        }
    }

    /// <inheritdoc />
    public static float Sample(
        Vector2 p,
        Settings settings
    )
    {
        const float f2 = 0.366025403f; // (sqrt(3) - 1) / 2
        const float g2 = 0.211324865f; // (3 - sqrt(3)) / 6

        var s = (p.X + p.Y) * f2;
        var i = (int)MathF.Floor(p.X + s);
        var j = (int)MathF.Floor(p.Y + s);

        var t = (i + j) * g2;
        var x0 = p.X - (i - t);
        var y0 = p.Y - (j - t);

        var i1 = x0 > y0 ? 1 : 0;
        var j1 = x0 > y0 ? 0 : 1;

        var x1 = x0 - i1 + g2;
        var y1 = y0 - j1 + g2;
        var x2 = x0 - 1f + 2f * g2;
        var y2 = y0 - 1f + 2f * g2;

        float n0 = 0f, n1 = 0f, n2 = 0f;

        var gi0 = (int)(NoiseOperations.Hash(i, j, settings.Seed) % simplex_gradients.Length);
        var gi1 = (int)(NoiseOperations.Hash(i + i1, j + j1, settings.Seed) % simplex_gradients.Length);
        var gi2 = (int)(NoiseOperations.Hash(i + 1, j + 1, settings.Seed) % simplex_gradients.Length);

        var t0 = 0.5f - x0 * x0 - y0 * y0;
        if (t0 > 0f)
        {
            t0 *= t0;
            var g = simplex_gradients[gi0];
            n0 = t0 * t0 * (g.X * x0 + g.Y * y0);
        }

        var t1 = 0.5f - x1 * x1 - y1 * y1;
        if (t1 > 0f)
        {
            t1 *= t1;
            var g = simplex_gradients[gi1];
            n1 = t1 * t1 * (g.X * x1 + g.Y * y1);
        }

        var t2 = 0.5f - x2 * x2 - y2 * y2;
        if (t2 > 0f)
        {
            t2 *= t2;
            var g = simplex_gradients[gi2];
            n2 = t2 * t2 * (g.X * x2 + g.Y * y2);
        }

        var value = 70f * (n0 + n1 + n2);
        return Math.Clamp(value * 0.5f + 0.5f, 0f, 1f);
    }
}

// Partially based on <https://lygia.xyz/generative/worley>.
/// <summary>
///     A fast implementation of Cellular noise.
/// </summary>
public readonly struct FastCellularNoise : INoise2d<FastCellularNoise, FastCellularNoise.Settings>
{
    /// <inheritdoc cref="INoise2dSettings{TSelf}"/>
    public record struct Settings(
        int Seed = 0,
        float Jitter = 0.9f
    ) : INoise2dSettings<Settings>
    {
        /// <inheritdoc />
        public static Settings DefaultSettings()
        {
            return new Settings(Seed: 0);
        }
    }

    /// <inheritdoc />
    public static float Sample(
        Vector2 p,
        Settings settings
    )
    {
        var ix = (int)MathF.Floor(p.X);
        var iy = (int)MathF.Floor(p.Y);

        var fx = p.X - ix;
        var fy = p.Y - iy;

        var minDist = float.MaxValue;

        for (var y = -1; y <= 1; y++)
        for (var x = -1; x <= 1; x++)
        {
            var cx = ix + x;
            var cy = iy + y;

            var h = NoiseOperations.Hash(cx, cy, settings.Seed);
            var rx = ((h & 0xffu) / 255f - 0.5f) * settings.Jitter + x;
            var ry = (((h >> 8) & 0xffu) / 255f - 0.5f) * settings.Jitter + y;

            var dx = fx - rx;
            var dy = fy - ry;
            var dist = dx * dx + dy * dy;
            if (dist < minDist)
            {
                minDist = dist;
            }
        }

        return Math.Clamp(MathF.Sqrt(minDist) * 1.4142f, 0f, 1f);
    }
}

internal static class NoiseOperations
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

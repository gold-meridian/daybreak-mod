using System;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/10fe993da52d558d3bba2fe49237195701a2b6a4/Common/Worldgen.cs>.
 *
 * Generously licensed to us under AGPL v3.0.
 */

/// <summary>
///     A fast implementation of Simplex noise.
/// </summary>
public record struct FastSimplexNoise(
    int Seed
) : INoise2d<FastSimplexNoise>
{
    private static readonly Vector2[] simplex_gradients =
    [
        new(1f, 1f), new(-1f, 1f), new(1f, -1f), new(-1f, -1f),
        new(1f, 0f), new(-1f, 0f), new(0f, 1f), new(0f, -1f),
    ];

    /// <inheritdoc />
    public static FastSimplexNoise DefaultSettings()
    {
        return new FastSimplexNoise(Seed: 0);
    }

    /// <inheritdoc />
    public static float Sample(
        Vector2 p,
        FastSimplexNoise settings
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

        var gi0 = (int)(NoiseHelper.Hash(i, j, settings.Seed) % simplex_gradients.Length);
        var gi1 = (int)(NoiseHelper.Hash(i + i1, j + j1, settings.Seed) % simplex_gradients.Length);
        var gi2 = (int)(NoiseHelper.Hash(i + 1, j + 1, settings.Seed) % simplex_gradients.Length);

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

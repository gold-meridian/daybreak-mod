using System;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

// Partially based on <https://lygia.xyz/generative/worley>.

/// <summary>
///     A fast implementation of Cellular noise.
/// </summary>
public record struct FastCellularNoise(
    int Seed = 0,
    float Jitter = 0.9f
) : INoise2d<FastCellularNoise>
{
    /// <inheritdoc />
    public static FastCellularNoise DefaultSettings()
    {
        return new FastCellularNoise(Seed: 0);
    }

    /// <inheritdoc />
    public static float Sample(
        Vector2 p,
        FastCellularNoise settings
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

            var h = NoiseHelper.Hash(cx, cy, settings.Seed);
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

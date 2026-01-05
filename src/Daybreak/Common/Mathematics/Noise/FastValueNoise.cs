using System;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/10fe993da52d558d3bba2fe49237195701a2b6a4/Common/Worldgen.cs>.
 *
 * Generously licensed to us under AGPL v3.0.
 */

/// <summary>
///     A fast implementation of Value noise.
/// </summary>
public record struct FastValueNoise(
    int Seed = 0
) : INoise2d<FastValueNoise>
{
    /// <inheritdoc />
    public static FastValueNoise DefaultSettings()
    {
        return new FastValueNoise(Seed: 0);
    }

    /// <inheritdoc />
    public static float Sample(
        Vector2 p,
        FastValueNoise settings
    )
    {
        var ix = (int)MathF.Floor(p.X);
        var iy = (int)MathF.Floor(p.Y);

        var fx = (float)ix;
        var fy = (float)iy;

        var v00 = NoiseHelper.HashFloat(ix, iy, settings.Seed);
        var v10 = NoiseHelper.HashFloat(ix + 1, iy, settings.Seed);
        var v01 = NoiseHelper.HashFloat(ix, iy + 1, settings.Seed);
        var v11 = NoiseHelper.HashFloat(ix + 1, iy + 1, settings.Seed);

        var u = NoiseHelper.Fade(fx);
        var v = NoiseHelper.Fade(fy);

        var x0 = Interpolate.Lerp(v00, v10, u);
        var x1 = Interpolate.Lerp(v01, v11, u);
        return Interpolate.Lerp(x0, x1, v);
    }
}

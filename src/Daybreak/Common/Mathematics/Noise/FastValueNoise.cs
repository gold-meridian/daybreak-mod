using System;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

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

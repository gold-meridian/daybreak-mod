using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     Provides common operations on <see cref="SdfSample"/>s.
/// </summary>
public static class SdfOperations
{
    /// <summary>
    ///     Combines shapes by taking the minimum of two SDFs.  The propagated
    ///     gradient is also the same as the smallest SDF sample.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Min(SdfSample a, SdfSample b)
    {
        return a.Distance < b.Distance ? a : b;
    }

    // For more information on smin functions, as well as where these particular
    // functions were adapted from:
    // https://iquilezles.org/articles/smin/

    /// <summary>
    ///     Computes a smooth minimum between SDFs <paramref name="a"/> and
    ///     <paramref name="b"/>, returning a sample with the smoothed value and
    ///     a new gradient reflecting the derivation of this function.
    /// </summary>
    /// <param name="a">The first SDF sample, from shape 1.</param>
    /// <param name="b">The second SDF sample, from shape 2.</param>
    /// <param name="k">
    ///     The tolerance of the function, in which shapes should be merged.
    ///     Measured in regular, distance units.
    /// </param>
    /// <returns>The merged SDF sample.</returns>
    /// <remarks>
    ///     This smooth minimum function is derived from IQ's implementations,
    ///     articles for which may be found here:
    ///     https://iquilezles.org/articles/smin/,
    ///     https://iquilezles.org/articles/distgradfunctions2d/.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample ExponentialSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        // k *= 1f;
        
        var ea = MathF.Pow(2f, -a.Distance / k);
        var eb = MathF.Pow(2f, -b.Distance / k);
        var r = ea + eb;

        var d = -k * MathF.Log2(r);
        var w = ea / r;

        return new SdfSample(
            d,
            Vector2.Lerp(b.Gradient, a.Gradient, w)
        );
    }

    /// <inheritdoc cref="ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample RootSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        k *= 2f;

        var x = b.Distance - a.Distance;
        var s = MathF.Sqrt(x * x + k * k);

        var d = 0.5f * (a.Distance + b.Distance - s);
        var w = 0.5f * (1f + x / s);

        return new SdfSample(
            d,
            Vector2.Lerp(b.Gradient, a.Gradient, w)
        );
    }

    /// <inheritdoc cref="ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample SigmoidSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        k *= MathF.Log(2f);

        var x = b.Distance - a.Distance;
        var e = MathF.Pow(2f, x / k);
        var d = a.Distance + x / (1f - e);

        var w = 1f / (1f + e);

        return new SdfSample(
            d,
            Vector2.Lerp(b.Gradient, a.Gradient, w)
        );
    }

    /// <inheritdoc cref="ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample QuadraticPolynomialSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        k *= 4f;

        var d = a.Distance - b.Distance;
        var h = MathF.Max(k - MathF.Abs(d), 0f) / k;

        var m = h * h * k * 0.25f;
        var w = 0.5f * h;

        return new SdfSample(
            MathF.Min(a.Distance, b.Distance) - m,
            Vector2.Lerp(a.Gradient, b.Gradient, d < 0f ? w : 1f - w)
        );
    }

    /// <inheritdoc cref="ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample CubicPolynomialSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        k *= 6f;

        var d = a.Distance - b.Distance;
        var h = MathF.Max(k - MathF.Abs(d), 0f) / k;

        var m = h * h * h * k * (1f / 6f);
        var w = 0.5f * h * h;

        return new SdfSample(
            MathF.Min(a.Distance, b.Distance) - m,
            Vector2.Lerp(a.Gradient, b.Gradient, d < 0f ? w : 1f - w)
        );
    }

    /// <inheritdoc cref="ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample QuarticPolynomialSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        k *= 16f / 3f;

        var d = a.Distance - b.Distance;
        var h = MathF.Max(k - MathF.Abs(d), 0f) / k;

        var m = h * h * h * (4f - h) * k * (1f / 16f);
        var w = h * h * (3f - h) * 0.25f;

        return new SdfSample(
            MathF.Min(a.Distance, b.Distance) - m,
            Vector2.Lerp(a.Gradient, b.Gradient, d < 0f ? w : 1f - w)
        );
    }

    /// <inheritdoc cref="ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample CircularSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        k *= 1f / (1f - MathF.Sqrt(0.5f));

        var d = a.Distance - b.Distance;
        var h = MathF.Max(k - MathF.Abs(d), 0f) / k;

        var s = MathF.Sqrt(1f - h * (h - 2f));
        var m = k * 0.5f * (1f + h - s);
        var w = h / s;

        return new SdfSample(
            MathF.Min(a.Distance, b.Distance) - m,
            Vector2.Lerp(a.Gradient, b.Gradient, d < 0f ? w : 1f - w)
        );
    }

    /// <inheritdoc cref="ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample CircularGeometricalSmoothMin(
        SdfSample a,
        SdfSample b,
        float k
    )
    {
        k *= 1f / (1f - MathF.Sqrt(0.5f));

        var da = MathF.Max(k - a.Distance, 0f);
        var db = MathF.Max(k - b.Distance, 0f);

        var len = MathF.Sqrt(da * da + db * db);
        var d = MathF.Max(k, MathF.Min(a.Distance, b.Distance)) - len;

        var w = da / len;

        return new SdfSample(
            d,
            Vector2.Lerp(b.Gradient, a.Gradient, w)
        );
    }

    /*
    /// <summary>
    ///     Creates a new SDF sample which attempts to merge the two samples
    ///     <paramref name="a"/> and <paramref name="b"/>, creating a smoothed
    ///     connection between two shapes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample SmoothMin(SdfSample a, SdfSample b, float k)
    {
        var h = Math.Clamp(0.5f + 0.5f * (b.Distance - a.Distance) / k, 0f, 1f);
        var dist = Interpolate.Lerp(b.Distance, a.Distance, h) - k * h * (1f - h);
        var grad = Vector2.Lerp(b.Gradient, a.Gradient, h);
        return new SdfSample(dist, grad);
    }
    */

    /// <summary>
    ///     Computes the exclusive-or (XOR) of two signed distance fields.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Xor(SdfSample a, SdfSample b)
    {
        var min1 = MathF.Min(a.Distance, -b.Distance);
        var min2 = MathF.Min(-a.Distance, b.Distance);

        var grad1 = a.Distance < -b.Distance ? a.Gradient : -b.Gradient;
        var grad2 = -a.Distance < b.Distance ? -a.Gradient : b.Gradient;

        if (min1 > min2)
        {
            return new SdfSample(min1, grad1);
        }

        if (min2 > min1)
        {
            return new SdfSample(min2, grad2);
        }

        var blended = grad1 + grad2;
        return new SdfSample(min1, blended);
    }

    /// <summary>
    ///     Computes the intersection of two signed distance fields.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Intersection(SdfSample a, SdfSample b)
    {
        if (a.Distance > b.Distance)
        {
            return a;
        }

        if (b.Distance > a.Distance)
        {
            return b;
        }

        var blended = a.Gradient + b.Gradient;
        return new SdfSample(a.Distance, blended);
    }

    /// <summary>
    ///     Subtracts one signed distance field from another.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Subtraction(SdfSample a, SdfSample b)
    {
        var da = a.Distance;
        var db = -b.Distance;

        if (da > db)
        {
            return a;
        }

        if (db > da)
        {
            return new SdfSample(db, -b.Gradient);
        }

        var blended = a.Gradient - b.Gradient;
        return new SdfSample(da, blended);
    }

    /// <summary>
    ///     Applies a rounding operation to a signed distance field.
    ///     <br />
    ///     This offsets the distance field inward by a constant radius,
    ///     effectively rounding sharp features such as edges and corners while
    ///     preserving the original gradient direction.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Round(SdfSample value, float radius)
    {
        return new SdfSample(value.Distance - radius, value.Gradient);
    }

    /// <summary>
    ///     Creates an onion (shell) effect from a signed distance field; see
    ///     also, annulus.
    ///     <br />
    ///     This operation produces a hollow shell of thickness
    ///     <paramref name="radius"/>
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Onion(SdfSample value, float radius)
    {
        return new SdfSample(MathF.Abs(value.Distance) - radius, MathF.Sign(value.Distance) * value.Gradient);
    }
}

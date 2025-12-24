using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     A sample of an SDF function providing the signed distance as well as a
///     directional gradient computed through automatic differentiation.
/// </summary>
/// <param name="Distance"></param>
/// <param name="Gradient"></param>
public readonly record struct SdfSample(float Distance, Vector2 Gradient)
{
    /// <summary>
    ///     The distance of the sample from the shape.
    /// </summary>
    public float Distance { get; } = Distance;

    /// <summary>
    ///     The gradient of the sample.
    /// </summary>
    public Vector2 Gradient { get; } = Gradient.LengthSquared() > float.Epsilon
        ? Vector2.Normalize(Gradient)
        : Vector2.UnitY;

    /// <inheritdoc cref="SdfOperations.Min"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample Min(SdfSample b)
    {
        return SdfOperations.Min(this, b);
    }

    /// <inheritdoc cref="SdfOperations.ExponentialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample ExponentialSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.ExponentialSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.RootSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample RootSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.RootSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.SigmoidSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample SigmoidSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.SigmoidSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.QuadraticPolynomialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample QuadraticPolynomialSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.QuadraticPolynomialSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.CubicPolynomialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample CubicPolynomialSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.CubicPolynomialSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.QuarticPolynomialSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample QuarticPolynomialSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.QuarticPolynomialSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.CircularSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample CircularSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.CircularSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.CircularGeometricalSmoothMin"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample CircularGeometricalSmoothMin(SdfSample b, float k)
    {
        return SdfOperations.CircularGeometricalSmoothMin(this, b, k);
    }

    /// <inheritdoc cref="SdfOperations.Xor"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample Xor(SdfSample b)
    {
        return SdfOperations.Xor(this, b);
    }

    /// <inheritdoc cref="SdfOperations.Intersection"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample Intersection(SdfSample b)
    {
        return SdfOperations.Intersection(this, b);
    }

    /// <inheritdoc cref="SdfOperations.Subtraction"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample Subtraction(SdfSample b)
    {
        return SdfOperations.Subtraction(this, b);
    }

    /// <inheritdoc cref="SdfOperations.Round"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public SdfSample Round(float radius)
    {
        return SdfOperations.Round(this, radius);
    }
}

public static class SignedDistance
{
    // https://iquilezles.org/articles/distgradfunctions2d/

#region Circle
    // https://www.shadertoy.com/view/WltSDj
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Circle(Vector2 p, float radius)
    {
        return Circle(p, radius, Angle.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SdfSample Circle(Vector2 p, float radius, Angle rotation)
    {
        p = p.RotatedBy(rotation);

        var len = p.Length();
        var dist = len - radius;
        var grad = len > float.Epsilon ? p / len : Vector2.UnitY;
        return new SdfSample(dist, grad);
    }
#endregion

#region Pie
    // https://www.shadertoy.com/view/3tGXRc
#endregion

#region Arc
    // https://www.shadertoy.com/view/WtGXRc
#endregion

#region Segment
    // https://www.shadertoy.com/view/WtdSDj
#endregion

#region Vesica
    // https://www.shadertoy.com/view/3lGXRc
#endregion

#region Box
    // https://www.shadertoy.com/view/wlcXD2
#endregion

#region Cross
    // https://www.shadertoy.com/view/WtdXWj
#endregion

#region Pentagon
    // https://www.shadertoy.com/view/3lySRc
#endregion

#region Hexagon
    // https://www.shadertoy.com/view/WtySRc
#endregion

#region Isosceles Triangle
    // https://www.shadertoy.com/view/3dyfDd
#endregion

#region Triangle
    // https://www.shadertoy.com/view/tlVyWh
#endregion

#region Quad
    // https://www.shadertoy.com/view/WtVcD1
#endregion

#region Ellipse
    // https://www.shadertoy.com/view/3lcfR8
#endregion

#region Moon
    // https://www.shadertoy.com/view/ddX3WH
#endregion

#region Parabola
    // https://www.shadertoy.com/view/mdX3WH
#endregion

#region Trapezoid
    // https://www.shadertoy.com/view/ddt3Rs
#endregion

#region Heart
    // https://www.shadertoy.com/view/DldXRf
#endregion

#region Rounded Box
#endregion

#region Annulus
#endregion

#region Polygon
#endregion
}

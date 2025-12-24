using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     A sample of an SDF function providing the signed distance as well as a
///     directional gradient computed through automatic differentiation.
/// </summary>
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

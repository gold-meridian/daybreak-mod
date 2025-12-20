using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;

namespace Daybreak.Common;

/// <summary>
///     Provides an interpolation function for type
///     <typeparamref name="TValue"/>.
/// </summary>
/// <typeparam name="TValue">
///     The type for which values may be interpolated.
/// </typeparam>
public interface IInterpolator<TValue>
{
    /// <summary>
    ///     Interpolates values <paramref name="a"/> and <paramref name="b"/>
    ///     between a normalized <c>0-1</c> value <paramref name="t"/>.
    /// </summary>
    /// <param name="a">The start point.</param>
    /// <param name="b">The end point.</param>
    /// <param name="t">
    ///     The progress value, between <c>0</c> and <c>1</c>.
    /// </param>
    /// <returns>The interpolated value.</returns>
    [MustUseReturnValue]
    [System.Diagnostics.Contracts.Pure]
    TValue Invoke(TValue a, TValue b, float t);
}

/// <summary>
///     Enables direct static reference to the interpolation function
///     <see cref="IInterpolator{TValue}.Invoke"/> through the static function
///     <see cref="Interpolate"/>.  This promotes JIT inlining, turning these
///     references into zero-cost abstractions which require no instance.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
/// <typeparam name="TValue">
///     The type for which values may be interpolated.
/// </typeparam>
public interface IInterpolator<TSelf, TValue> : IInterpolator<TValue>
    where TSelf : IInterpolator<TSelf, TValue>
{
    TValue IInterpolator<TValue>.Invoke(TValue a, TValue b, float t)
    {
        return TSelf.Interpolate(a, b, t);
    }

    /// <inheritdoc cref="IInterpolator{TValue}.Invoke"/>
    [MustUseReturnValue]
    [System.Diagnostics.Contracts.Pure]
    static abstract TValue Interpolate(
        TValue a,
        TValue b,
        float t
    );
}

/// <summary>
///     Provides a linear interpolation function (&quot;lerp&quot;).
/// </summary>
public readonly struct Linear : IInterpolator<Linear, float>,
                                IInterpolator<Linear, Vector2>
{
    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Interpolate(float a, float b, float t)
    {
        return a + (b - a) * t;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 Interpolate(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
    }
}

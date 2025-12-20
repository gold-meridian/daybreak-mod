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
///     Provides common interpolation and easing functions.
/// </summary>
public static class Easings
{
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

    /*
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp<TInterpolator>(float a, float b, float t)
        where TInterpolator : IInterpolator<TInterpolator, float>
    {
        return TInterpolator.Interpolate(a, b, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Lerp(float a, float b, float t)
    {
        return Lerp<Linear>(a, b, t);
    }
    */

#region QuadraticBezier
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float QuadraticBezier(
        float p0,
        float p1,
        float p2,
        float t
    )
    {
        return QuadraticBezier<Linear, float>(p0, p1, p2, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 QuadraticBezier(
        Vector2 p0,
        Vector2 p1,
        Vector2 p2,
        float t
    )
    {
        return QuadraticBezier<Linear, Vector2>(p0, p1, p2, t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TValue QuadraticBezier<TInterp, TValue>(
        TValue p0,
        TValue p1,
        TValue p2,
        float t
    ) where TInterp : IInterpolator<TInterp, TValue>
    {
        var a = TInterp.Interpolate(p0, p1, t);
        var b = TInterp.Interpolate(p2, p0, t);
        return TInterp.Interpolate(a, b, t);
    }
#endregion

#region CubicBezier
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TValue QuadraticBezier<TInterp, TValue>(
        TValue p0,
        TValue c0,
        TValue c1,
        TValue p1,
        float t
    ) where TInterp : IInterpolator<TInterp, TValue>
    {
        var a = TInterp.Interpolate(p0, c0, t);
        var b = TInterp.Interpolate(c0, c1, t);
        var c = TInterp.Interpolate(c1, p1, t);

        var d = TInterp.Interpolate(a, b, t);
        var e = TInterp.Interpolate(b, c, t);

        return TInterp.Interpolate(d, e, t);
    }
#endregion
    
#region CubicHermite
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TValue CubicHermite<TValue>(
        TValue p0,
        TValue m0,
        TValue p1,
        TValue m1,
        float t
    )
    {
    }
#endregion

#region CatmullRom
    
#endregion
}

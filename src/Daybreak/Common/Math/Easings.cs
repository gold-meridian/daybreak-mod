using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Daybreak.Common;

/// <summary>
///     Provides common interpolation and easing functions.
/// </summary>
public static class Easings
{
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

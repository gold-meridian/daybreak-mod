using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

namespace Daybreak.Common;

/// <summary>
///     Defines the signature for an interpolator function which takes
///     parameters <paramref name="a"/> amd <paramref name="b"/> and produces a
///     return value based on progress <paramref name="t"/>.
/// </summary>
/// <typeparam name="TValue">The type to be operated on.</typeparam>
public delegate TValue Interpolator<TValue>(TValue a, TValue b, float t);

/// <summary>
///     Provides core interpolation primitives.
/// </summary>
public static partial class Interpolate
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane Lerp<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        float t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return a + (b - a) * t;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane VectorLerp<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return a + (b - a) * t;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="value"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InverseLerp<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        TLane value
    ) where TLane : unmanaged, ILane<TLane>
    {
        return (value - a) / (b - a);
    }
    
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="value"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane VectorInverseLerp<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        TLane value
    ) where TLane : unmanaged, ILane<TLane>
    {
        return (value - a) / (b - a);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="inMin"></param>
    /// <param name="inMax"></param>
    /// <param name="outMin"></param>
    /// <param name="outMax"></param>
    /// <param name="value"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane Remap<[LaneParameter] TLane>(
        TLane inMin,
        TLane inMax,
        TLane outMin,
        TLane outMax,
        TLane value
    ) where TLane : unmanaged, ILane<TLane>
    {
        var t = InverseLerp(inMin, inMax, value);
        return VectorLerp(outMin, outMax, t);
    }
}

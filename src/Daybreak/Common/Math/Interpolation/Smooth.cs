using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

namespace Daybreak.Common;

/// <summary>
///     Interpolation primitives performed through time shaping.
/// </summary>
public static partial class Smooth
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane TimeStep<[LaneParameter] TLane>(
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return t * t * (3f - 2f * t);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane SmootherTimeStep<[LaneParameter] TLane>(
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return t * t * t * (t * (6f * t - 15f) + 10f);
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
    public static TLane Step<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        float t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return Interpolate.Lerp(a, b, TimeStep(t));
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
    public static TLane VectorStep<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return Interpolate.VectorLerp(a, b, TimeStep(t));
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
    public static TLane SmootherStep<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        float t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return Interpolate.Lerp(a, b, SmootherTimeStep(t));
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
    public static TLane VectorSmootherStep<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return Interpolate.VectorLerp(a, b, SmootherTimeStep(t));
    }
}

using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

namespace Daybreak.Common;

/// <summary>
/// TODO
/// </summary>
public static partial class Bezier
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane Cubic<[LaneParameter] TLane>(
        TLane p0,
        TLane p1,
        TLane p2,
        TLane p3,
        float t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var u = 1f - t;
        var u2 = u * u;
        var u3 = u2 * u;
        var t2 = t * t;
        var t3 = t2 * t;

        return
            p0 * u3
          + p1 * (3f * u2 * t)
          + p2 * (3f * u * t2)
          + p3 * t3;
    }
    
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane VectorCubic<[LaneParameter] TLane>(
        TLane p0,
        TLane p1,
        TLane p2,
        TLane p3,
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var u = 1f - t;
        var u2 = u * u;
        var u3 = u2 * u;
        var t2 = t * t;
        var t3 = t2 * t;

        return
            p0 * u3
          + p1 * (3f * u2 * t)
          + p2 * (3f * u * t2)
          + p3 * t3;
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane Quadratic<[LaneParameter] TLane>(
        TLane p0,
        TLane p1,
        TLane p2,
        float t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var u = 1f - t;

        return
            p0 * (u * u)
          + p1 * (2f * u * t)
          + p2 * (t * t);
    }

    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="t"></param>
    /// <typeparam name="TLane"></typeparam>
    /// <returns></returns>
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane VectorQuadratic<[LaneParameter] TLane>(
        TLane p0,
        TLane p1,
        TLane p2,
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var u = 1f - t;

        return
            p0 * (u * u)
          + p1 * (2f * u * t)
          + p2 * (t * t);
    }
}

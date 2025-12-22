using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Daybreak.Common.Math;

/// <summary>
///     Provides Bézier curve interpolation primitives.
/// </summary>
public static partial class Bezier
{
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

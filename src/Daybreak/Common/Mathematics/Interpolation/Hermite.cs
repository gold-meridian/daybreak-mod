using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Daybreak.Common.Mathematics;

/// <summary>
///     Provides Hermite spline interpolation primitives.
/// </summary>
public static partial class Hermite
{
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane Cubic<[LaneParameter] TLane>(
        TLane p0,
        TLane m0,
        TLane p1,
        TLane m1,
        float t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var t2 = t * t;
        var t3 = t2 * t;

        var h00 = 2f * t3 - 3f * t2 + 1f;
        var h10 = t3 - 2f * t2 + t;
        var h01 = -2f * t3 + 3f * t2;
        var h11 = t3 - t2;

        return
            p0 * h00
          + m0 * h10
          + p1 * h01
          + m1 * h11;
    }

    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane VectorCubic<[LaneParameter] TLane>(
        TLane p0,
        TLane m0,
        TLane p1,
        TLane m1,
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var t2 = t * t;
        var t3 = t2 * t;

        var h00 = 2f * t3 - 3f * t2 + 1f;
        var h10 = t3 - 2f * t2 + t;
        var h01 = -2f * t3 + 3f * t2;
        var h11 = t3 - t2;

        return
            p0 * h00
          + m0 * h10
          + p1 * h01
          + m1 * h11;
    }

    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane CatmullRom<[LaneParameter] TLane>(
        TLane p0,
        TLane p1,
        TLane p2,
        TLane p3,
        float t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var m1 = (p2 - p0) * 0.5f;
        var m2 = (p3 - p1) * 0.5f;

        return Cubic(p1, m1, p2, m2, t);
    }

    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane VectorCatmullRom<[LaneParameter] TLane>(
        TLane p0,
        TLane p1,
        TLane p2,
        TLane p3,
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        var m1 = (p2 - p0) * 0.5f;
        var m2 = (p3 - p1) * 0.5f;

        return VectorCubic(p1, m1, p2, m2, t);
    }
}

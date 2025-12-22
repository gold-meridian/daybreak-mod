using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Daybreak.Common.Mathematics;

/// <summary>
///     Provides smooth time-shaping and eased interpolation primitives.
/// </summary>
public static partial class Smooth
{
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane TimeStep<[LaneParameter] TLane>(
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return t * t * (3f - 2f * t);
    }

    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane SmootherTimeStep<[LaneParameter] TLane>(
        TLane t
    ) where TLane : unmanaged, ILane<TLane>
    {
        return t * t * t * (t * (6f * t - 15f) + 10f);
    }

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

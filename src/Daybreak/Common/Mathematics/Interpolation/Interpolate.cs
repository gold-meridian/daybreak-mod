using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Daybreak.Common.Mathematics;

// public delegate TValue Interpolator<TValue>(TValue a, TValue b, float t);

/// <summary>
///     Provides core interpolation and remapping primitives.
/// </summary>
public static partial class Interpolate
{
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

    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float InverseLerp<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        TLane value
    ) where TLane : unmanaged, ILane<TLane>
    {
        return ((value - a) / (b - a)).Average();
    }

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
        var t = VectorInverseLerp(inMin, inMax, value);
        return VectorLerp(outMin, outMax, t);
    }
    
    // TODO: Implement when follow-up lane APIs are merged.
    /*
    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane Blend<[LaneParameter] TLane>(
        TLane a,
        TLane b,
        TLane mask,
        TLane feather
    ) where TLane : unmanaged, ILane<TLane>
    {
        var t = TLane.Clamp((mask - feather * 0.5f) / TLane.Max(TLane.CreateFromSingle(1e-6f, feather), TLane.CreateFromSingle(0f), TLane.CreateFromSingle(1f);
        return VectorLerp(a, b, t);
    }
    */
}

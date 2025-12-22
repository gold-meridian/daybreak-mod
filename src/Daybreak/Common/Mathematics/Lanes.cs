using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Daybreak.Core.SourceGen;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Vector4 = Microsoft.Xna.Framework.Vector4;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     An abstraction over a mathematically-operable, vector value which may be
///     SIMD-accelerated.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
public interface ILane<TSelf>
    where TSelf : unmanaged, ILane<TSelf>
{
    /// <summary>
    ///     The number of lanes the stored value occupies.
    /// </summary>
    static abstract int LaneCount { get; }

    /// <summary>
    ///     Creates a new lane from the provided scalar values, assuming the
    ///     span is of length <see cref="LaneCount"/>.
    /// </summary>
    static abstract TSelf ReadScalars(Span<float> scalars);

    /// <summary>
    ///     Writes all scalar components of this lane into
    ///     <paramref name="destination"/>.
    ///     <br />
    ///     Returns the number of floats written.
    /// </summary>
    int WriteScalars(Span<float> destination);

    /// <summary>
    ///     Performs an addition operation over the values.
    /// </summary>
    static abstract TSelf Add(TSelf a, TSelf b);

    /// <inheritdoc cref="Add(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Add(TSelf a, float b)
    {
        return TSelf.Add(a, TSelf.CreateFromSingle(b));
    }

    /// <inheritdoc cref="Add(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Add(float a, TSelf b)
    {
        return TSelf.Add(TSelf.CreateFromSingle(a), b);
    }

    /// <inheritdoc cref="Add(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator +(TSelf a, TSelf b)
    {
        return TSelf.Add(a, b);
    }

    /// <inheritdoc cref="Add(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator +(TSelf a, float b)
    {
        return TSelf.Add(a, b);
    }

    /// <inheritdoc cref="Add(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator +(float a, TSelf b)
    {
        return TSelf.Add(a, b);
    }

    /// <summary>
    ///     Performs a subtraction operation over the values.
    /// </summary>
    static abstract TSelf Sub(TSelf a, TSelf b);

    /// <inheritdoc cref="Sub(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Sub(TSelf a, float b)
    {
        return TSelf.Sub(a, TSelf.CreateFromSingle(b));
    }

    /// <inheritdoc cref="Sub(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Sub(float a, TSelf b)
    {
        return TSelf.Sub(TSelf.CreateFromSingle(a), b);
    }

    /// <inheritdoc cref="Sub(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator -(TSelf a, TSelf b)
    {
        return TSelf.Sub(a, b);
    }

    /// <inheritdoc cref="Sub(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator -(TSelf a, float b)
    {
        return TSelf.Sub(a, b);
    }

    /// <inheritdoc cref="Sub(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator -(float a, TSelf b)
    {
        return TSelf.Sub(a, b);
    }

    /// <summary>
    ///     Performs a multiplication operation over the values.
    /// </summary>
    static abstract TSelf Mul(TSelf a, TSelf b);

    /// <inheritdoc cref="Mul(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Mul(TSelf a, float b)
    {
        return TSelf.Mul(a, TSelf.CreateFromSingle(b));
    }

    /// <inheritdoc cref="Mul(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Mul(float a, TSelf b)
    {
        return TSelf.Mul(TSelf.CreateFromSingle(a), b);
    }

    /// <inheritdoc cref="Mul(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator *(TSelf a, TSelf b)
    {
        return TSelf.Mul(a, b);
    }

    /// <inheritdoc cref="Mul(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator *(TSelf a, float b)
    {
        return TSelf.Mul(a, b);
    }

    /// <inheritdoc cref="Mul(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator *(float a, TSelf b)
    {
        return TSelf.Mul(a, b);
    }

    /// <summary>
    ///     Performs a division operation over the values.
    /// </summary>
    static abstract TSelf Div(TSelf a, TSelf b);

    /// <inheritdoc cref="Div(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Div(TSelf a, float b)
    {
        return TSelf.Div(a, TSelf.CreateFromSingle(b));
    }

    /// <inheritdoc cref="Div(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Div(float a, TSelf b)
    {
        return TSelf.Div(TSelf.CreateFromSingle(a), b);
    }

    /// <inheritdoc cref="Div(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator /(TSelf a, TSelf b)
    {
        return TSelf.Div(a, b);
    }

    /// <inheritdoc cref="Div(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator /(TSelf a, float b)
    {
        return TSelf.Div(a, b);
    }

    /// <inheritdoc cref="Div(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator /(float a, TSelf b)
    {
        return TSelf.Div(a, b);
    }

    /// <summary>
    ///     Negates the values.
    /// </summary>
    static abstract TSelf Neg(TSelf a);

    /// <inheritdoc cref="Neg(TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf operator -(TSelf a)
    {
        return TSelf.Neg(a);
    }

    /// <summary>
    ///     Returns the minimum value of each lane.
    /// </summary>
    static abstract TSelf Min(TSelf a, TSelf b);

    /// <inheritdoc cref="Min(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Min(TSelf a, float b)
    {
        return TSelf.Min(a, TSelf.CreateFromSingle(b));
    }

    /// <inheritdoc cref="Min(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Min(float a, TSelf b)
    {
        return TSelf.Min(TSelf.CreateFromSingle(a), b);
    }

    /// <summary>
    ///     Returns the maximum value of each lane.
    /// </summary>
    static abstract TSelf Max(TSelf a, TSelf b);

    /// <inheritdoc cref="Max(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Max(TSelf a, float b)
    {
        return TSelf.Max(a, TSelf.CreateFromSingle(b));
    }

    /// <inheritdoc cref="Max(TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Max(float a, TSelf b)
    {
        return TSelf.Max(TSelf.CreateFromSingle(a), b);
    }

    /// <summary>
    ///     Computes the absolute value of each value.
    /// </summary>
    static abstract TSelf Abs(TSelf a);

    /// <summary>
    ///     Clamps each value to the given range.
    /// </summary>
    static abstract TSelf Clamp(TSelf value, TSelf min, TSelf max);

    /// <inheritdoc cref="Clamp(TSelf, TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Clamp(TSelf value, float min, TSelf max)
    {
        return TSelf.Clamp(value, TSelf.CreateFromSingle(min), max);
    }

    /// <inheritdoc cref="Clamp(TSelf, TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Clamp(TSelf value, TSelf min, float max)
    {
        return TSelf.Clamp(value, min, TSelf.CreateFromSingle(max));
    }

    /// <inheritdoc cref="Clamp(TSelf, TSelf, TSelf)"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Clamp(TSelf value, float min, float max)
    {
        return TSelf.Clamp(value, TSelf.CreateFromSingle(min), TSelf.CreateFromSingle(max));
    }

    /// <summary>
    ///     Clamps each value in the vector to [0, 1].
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static virtual TSelf Saturate(TSelf value)
    {
        return TSelf.Clamp(value, TSelf.CreateFromSingle(0f), TSelf.CreateFromSingle(1f));
    }

    /// <summary>
    ///     Initializes a lane object filled with the single for each valid
    ///     lane.
    /// </summary>
    static abstract TSelf CreateFromSingle(float f);
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of one.
/// </summary>
public readonly struct Lane1(float x) : ILane<Lane1>
{
    /// <inheritdoc />
    public static int LaneCount => 1;

    /// <summary>
    ///     Accesses the first element of the lane.
    /// </summary>
    public float X { get; } = x;

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 ReadScalars(Span<float> scalars)
    {
        Debug.Assert(scalars.Length == LaneCount);
        return new Lane1(scalars[0]);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int WriteScalars(Span<float> destination)
    {
        destination[0] = X;
        return LaneCount;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Add(Lane1 a, Lane1 b)
    {
        return new Lane1(a.X + b.X);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Sub(Lane1 a, Lane1 b)
    {
        return new Lane1(a.X - b.X);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Mul(Lane1 a, Lane1 b)
    {
        return new Lane1(a.X * b.X);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Div(Lane1 a, Lane1 b)
    {
        return new Lane1(a.X / b.X);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Neg(Lane1 a)
    {
        return new Lane1(-a.X);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Min(Lane1 a, Lane1 b)
    {
        return new Lane1(MathF.Min(a.X, b.X));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Max(Lane1 a, Lane1 b)
    {
        return new Lane1(MathF.Max(a.X, b.X));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Abs(Lane1 a)
    {
        return new Lane1(MathF.Abs(a.X));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Clamp(Lane1 value, Lane1 min, Lane1 max)
    {
        return new Lane1(Math.Clamp(value.X, min.X, max.X));
    }

    static Lane1 ILane<Lane1>.CreateFromSingle(float f)
    {
        return new Lane1(f);
    }
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of two.
/// </summary>
public readonly struct Lane2 : ILane<Lane2>
{
    /// <inheritdoc />
    public static int LaneCount => 2;

    /// <summary>
    ///     The lane's data.
    /// </summary>
    private Vector128<float> Vector { get; }

    /// <summary>
    ///     Accesses the first element of the lane.
    /// </summary>
    public float X => Vector[0];

    /// <summary>
    ///     Accesses the second element of the lane.
    /// </summary>
    public float Y => Vector[1];

    /// <summary/>
    public Lane2(float x, float y)
    {
        Vector = Vector128.Create(x, y, 0f, 0f);
    }

    private Lane2(Vector128<float> v)
    {
        Vector = v;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 ReadScalars(Span<float> scalars)
    {
        Debug.Assert(scalars.Length == LaneCount);
        return new Lane2(scalars[0], scalars[1]);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int WriteScalars(Span<float> destination)
    {
        destination[0] = X;
        destination[1] = Y;
        return LaneCount;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Add(Lane2 a, Lane2 b)
    {
        return new Lane2(a.Vector + b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Sub(Lane2 a, Lane2 b)
    {
        return new Lane2(a.Vector - b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Mul(Lane2 a, Lane2 b)
    {
        return new Lane2(a.Vector * b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Div(Lane2 a, Lane2 b)
    {
        return new Lane2(a.Vector / b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Neg(Lane2 a)
    {
        return new Lane2(-a.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Min(Lane2 a, Lane2 b)
    {
        return new Lane2(Vector128.Min(a.Vector, b.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Max(Lane2 a, Lane2 b)
    {
        return new Lane2(Vector128.Max(a.Vector, b.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Abs(Lane2 a)
    {
        return new Lane2(Vector128.Abs(a.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Clamp(Lane2 value, Lane2 min, Lane2 max)
    {
        return new Lane2(LaneExtensions.Clamp(value.Vector, min.Vector, max.Vector));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Lane2 ILane<Lane2>.CreateFromSingle(float f)
    {
        return new Lane2(f, f);
    }
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of three.
/// </summary>
public readonly struct Lane3 : ILane<Lane3>
{
    /// <inheritdoc />
    public static int LaneCount => 3;

    /// <summary>
    ///     The lane's data.
    /// </summary>
    private Vector128<float> Vector { get; }

    /// <summary>
    ///     Accesses the first element of the lane.
    /// </summary>
    public float X => Vector[0];

    /// <summary>
    ///     Accesses the second element of the lane.
    /// </summary>
    public float Y => Vector[1];

    /// <summary>
    ///     Accesses the third element of the lane.
    /// </summary>
    public float Z => Vector[2];

    /// <summary/>
    public Lane3(float x, float y, float z)
    {
        Vector = Vector128.Create(x, y, z, 0f);
    }

    private Lane3(Vector128<float> v)
    {
        Vector = v;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 ReadScalars(Span<float> scalars)
    {
        Debug.Assert(scalars.Length == LaneCount);
        return new Lane3(scalars[0], scalars[1], scalars[2]);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int WriteScalars(Span<float> destination)
    {
        destination[0] = X;
        destination[1] = Y;
        destination[2] = Z;
        return LaneCount;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Add(Lane3 a, Lane3 b)
    {
        return new Lane3(a.Vector + b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Sub(Lane3 a, Lane3 b)
    {
        return new Lane3(a.Vector - b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Mul(Lane3 a, Lane3 b)
    {
        return new Lane3(a.Vector * b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Div(Lane3 a, Lane3 b)
    {
        return new Lane3(a.Vector / b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Neg(Lane3 a)
    {
        return new Lane3(-a.Vector);
    }
    
    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Min(Lane3 a, Lane3 b)
    {
        return new Lane3(Vector128.Min(a.Vector, b.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Max(Lane3 a, Lane3 b)
    {
        return new Lane3(Vector128.Max(a.Vector, b.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Abs(Lane3 a)
    {
        return new Lane3(Vector128.Abs(a.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Clamp(Lane3 value, Lane3 min, Lane3 max)
    {
        return new Lane3(LaneExtensions.Clamp(value.Vector, min.Vector, max.Vector));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Lane3 ILane<Lane3>.CreateFromSingle(float f)
    {
        return new Lane3(f, f, f);
    }
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of four.
/// </summary>
public readonly struct Lane4 : ILane<Lane4>
{
    /// <inheritdoc />
    public static int LaneCount => 4;

    /// <summary>
    ///     The lane's data.
    /// </summary>
    private Vector128<float> Vector { get; }

    /// <summary>
    ///     Accesses the first element of the lane.
    /// </summary>
    public float X => Vector[0];

    /// <summary>
    ///     Accesses the second element of the lane.
    /// </summary>
    public float Y => Vector[1];

    /// <summary>
    ///     Accesses the third element of the lane.
    /// </summary>
    public float Z => Vector[2];

    /// <summary>
    ///     Accesses the fourth element of the lane.
    /// </summary>
    public float W => Vector[3];

    /// <summary/>
    public Lane4(float x, float y, float z, float w)
    {
        Vector = Vector128.Create(x, y, z, w);
    }

    private Lane4(Vector128<float> v)
    {
        Vector = v;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 ReadScalars(Span<float> scalars)
    {
        Debug.Assert(scalars.Length == LaneCount);
        return new Lane4(scalars[0], scalars[1], scalars[2], scalars[3]);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int WriteScalars(Span<float> destination)
    {
        /*
        destination[0] = X;
        destination[1] = Y;
        destination[2] = Z;
        destination[3] = W;
        */

        Unsafe.WriteUnaligned(
            ref Unsafe.As<float, byte>(ref destination[0]),
            Vector
        );
        return LaneCount;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Add(Lane4 a, Lane4 b)
    {
        return new Lane4(a.Vector + b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Sub(Lane4 a, Lane4 b)
    {
        return new Lane4(a.Vector - b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Mul(Lane4 a, Lane4 b)
    {
        return new Lane4(a.Vector * b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Div(Lane4 a, Lane4 b)
    {
        return new Lane4(a.Vector / b.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Neg(Lane4 a)
    {
        return new Lane4(-a.Vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Min(Lane4 a, Lane4 b)
    {
        return new Lane4(Vector128.Min(a.Vector, b.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Max(Lane4 a, Lane4 b)
    {
        return new Lane4(Vector128.Max(a.Vector, b.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Abs(Lane4 a)
    {
        return new Lane4(Vector128.Abs(a.Vector));
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Clamp(Lane4 value, Lane4 min, Lane4 max)
    {
        return new Lane4(LaneExtensions.Clamp(value.Vector, min.Vector, max.Vector));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Lane4 ILane<Lane4>.CreateFromSingle(float f)
    {
        return new Lane4(f, f, f, f);
    }
}

/// <summary>
///     Provides default marshalling for default <see cref="ILane{TSelf}"/>s.
/// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class LaneMarshalling
{
    [ToLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 ToLane(float x)
    {
        return new Lane1(x);
    }

    [FromLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float FromLane(Lane1 lane)
    {
        return lane.X;
    }

    [ToLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 ToLane(Vector2 v)
    {
        return new Lane2(v.X, v.Y);
    }

    [FromLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector2 FromLane(Lane2 lane)
    {
        return new Vector2(lane.X, lane.Y);
    }

    [ToLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 ToLane(Vector3 v)
    {
        return new Lane3(v.X, v.Y, v.Z);
    }

    [FromLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector3 FromLane(Lane3 lane)
    {
        return new Vector3(lane.X, lane.Y, lane.Z);
    }

    [ToLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 ToLane(Vector4 v)
    {
        return new Lane4(v.X, v.Y, v.Z, v.W);
    }

    [FromLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector4 FromLane(Lane4 lane)
    {
        return new Vector4(lane.X, lane.Y, lane.Z, lane.W);
    }

    /*
    [ToLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 ToLane(Color c)
    {
        return new Lane4(c.R, c.G, c.B, c.A);
    }

    [FromLane]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color ColorFromLane(Lane4 lane)
    {
        return new Color(lane.X, lane.Y, lane.Z, lane.W);
    }
    */
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

/// <summary>
///     Provides generic extensions for lanes.
/// </summary>
public static class LaneExtensions
{
    extension<TLane>(TLane lane)
        where TLane : unmanaged, ILane<TLane>
    {
        /// <summary>
        ///     Returns the first component of the lane.
        /// </summary>
        public float X
        {
            get
            {
                var components = (Span<float>)stackalloc float[TLane.LaneCount];
                lane.WriteScalars(components);

                return components[0];
            }
        }

        /// <summary>
        ///     Returns the mean average of every component.
        /// </summary>
        /// <returns></returns>
        public float Average()
        {
            var components = (Span<float>)stackalloc float[TLane.LaneCount];
            var written = lane.WriteScalars(components);
            {
                Debug.Assert(written == TLane.LaneCount);
            }

            var sum = 0f;
            foreach (var f in components)
            {
                sum += f;
            }

            return sum / written;
        }

        /// <summary>
        ///     Returns the value of the smallest component.
        /// </summary>
        public float Min()
        {
            var components = (Span<float>)stackalloc float[TLane.LaneCount];
            var written = lane.WriteScalars(components);
            {
                Debug.Assert(written == TLane.LaneCount);
            }

            var min = components[0];
            for (var i = 1; i < written; i++)
            {
                min = MathF.Min(min, components[i]);
            }

            return min;
        }

        /// <summary>
        ///     Returns the value of the largest component.
        /// </summary>
        public float Max()
        {
            var components = (Span<float>)stackalloc float[TLane.LaneCount];
            var written = lane.WriteScalars(components);
            {
                Debug.Assert(written == TLane.LaneCount);
            }

            var max = components[0];
            for (var i = 1; i < written; i++)
            {
                max = MathF.Max(max, components[i]);
            }

            return max;
        }

        /// <summary>
        ///     Returns the total value of the components.
        /// </summary>
        public float Sum()
        {
            var components = (Span<float>)stackalloc float[TLane.LaneCount];
            var written = lane.WriteScalars(components);
            {
                Debug.Assert(written == TLane.LaneCount);
            }

            var total = 0f;
            for (var i = 0; i < written; i++)
            {
                total += components[i];
            }

            return total;
        }
    }

#if NET9_0_OR_GREATER
    #error REPLACE ME
#else
    // https://github.com/dotnet/dotnet/blob/b0f34d51fccc69fd334253924abd8d6853fad7aa/src/runtime/src/libraries/System.Private.CoreLib/src/System/Runtime/Intrinsics/Vector128.cs#L347
    internal static Vector128<T> Clamp<T>(Vector128<T> value, Vector128<T> min, Vector128<T> max)
    {
        // // We must follow HLSL behavior in the case user specified min value
        // is bigger than max value.
        return Vector128.Min(Vector128.Max(value, min), max);
    }
#endif
}

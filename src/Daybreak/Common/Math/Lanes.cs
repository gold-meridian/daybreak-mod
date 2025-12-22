using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Daybreak.Core.SourceGen;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Vector4 = Microsoft.Xna.Framework.Vector4;

namespace Daybreak.Common;

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
    ///     The values of the vector.
    /// </summary>
    ReadOnlySpan<float> Values { get; }

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

    public ReadOnlySpan<float> Values
    {
        get
        {
            return 
        }
    }

    /// <summary>
    ///     Accesses the first element of the lane.
    /// </summary>
    public float X { get; } = x;

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
    public Vector128<float> Vector { get; }

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
    public Vector128<float> Vector { get; }

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
    public Vector128<float> Vector { get; }

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

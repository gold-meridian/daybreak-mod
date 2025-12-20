using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using Microsoft.Xna.Framework;

namespace Daybreak.Common;

/// <summary>
///     An abstraction over a mathematically-operable value which may be
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
    ///     Performs an addition operation over the values.
    /// </summary>
    static abstract TSelf Add(TSelf a, TSelf b);

    /// <summary>
    ///     Performs a subtraction operation over the values.
    /// </summary>
    static abstract TSelf Sub(TSelf a, TSelf b);

    /// <summary>
    ///     Performs a multiplication operation over the values.
    /// </summary>
    static abstract TSelf Mul(TSelf a, TSelf b);

    /// <summary>
    ///     Performs a division operation over the values.
    /// </summary>
    static abstract TSelf Div(TSelf a, TSelf b);

    /// <summary>
    ///     Performs a linear interpolation over the values.
    /// </summary>
    static abstract TSelf Lerp(TSelf a, TSelf b, float t);
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of one.
/// </summary>
public readonly struct Lane1(float value) : ILane<Lane1>
{
    /// <inheritdoc />
    public static int LaneCount => 1;

    /// <summary>
    ///     The value wrapped by this lane.
    /// </summary>
    public float Value { get; } = value;

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Add(Lane1 a, Lane1 b)
    {
        return new Lane1(a.Value + b.Value);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Sub(Lane1 a, Lane1 b)
    {
        return new Lane1(a.Value - b.Value);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Mul(Lane1 a, Lane1 b)
    {
        return new Lane1(a.Value * b.Value);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Div(Lane1 a, Lane1 b)
    {
        return new Lane1(a.Value / b.Value);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane1 Lerp(Lane1 a, Lane1 b, float t)
    {
        return new Lane1(a.Value + (b.Value - a.Value) * t);
    }
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of two.
/// </summary>
public readonly struct Lane2 : ILane<Lane2>
{
    /// <inheritdoc />
    public static int LaneCount => 2;

    private readonly Vector128<float> vector;

    /// <summary/>
    public Lane2(float x, float y)
    {
        vector = Vector128.Create(x, y, 0f, 0f);
    }

    /// <summary/>
    public Lane2(Vector2 v)
    {
        vector = Vector128.Create(v.X, v.Y, 0f, 0f);
    }

    private Lane2(Vector128<float> v)
    {
        vector = v;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Add(Lane2 a, Lane2 b)
    {
        return new Lane2(a.vector + b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Sub(Lane2 a, Lane2 b)
    {
        return new Lane2(a.vector - b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Mul(Lane2 a, Lane2 b)
    {
        return new Lane2(a.vector * b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Div(Lane2 a, Lane2 b)
    {
        return new Lane2(a.vector / b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane2 Lerp(Lane2 a, Lane2 b, float t)
    {
        return new Lane2(a.vector + (b.vector - a.vector) * t);
    }
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of three.
/// </summary>
public readonly struct Lane3 : ILane<Lane3>
{
    /// <inheritdoc />
    public static int LaneCount => 3;

    private readonly Vector128<float> vector;

    /// <summary/>
    public Lane3(float x, float y, float z)
    {
        vector = Vector128.Create(x, y, z, 0f);
    }

    /// <summary/>
    public Lane3(Vector3 v)
    {
        vector = Vector128.Create(v.X, v.Y, v.Z, 0f);
    }

    private Lane3(Vector128<float> v)
    {
        vector = v;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Add(Lane3 a, Lane3 b)
    {
        return new Lane3(a.vector + b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Sub(Lane3 a, Lane3 b)
    {
        return new Lane3(a.vector - b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Mul(Lane3 a, Lane3 b)
    {
        return new Lane3(a.vector * b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Div(Lane3 a, Lane3 b)
    {
        return new Lane3(a.vector / b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane3 Lerp(Lane3 a, Lane3 b, float t)
    {
        return new Lane3(a.vector + (b.vector - a.vector) * t);
    }
}

/// <summary>
///     Implements a <see cref="ILane{TSelf}"/> with a count of four.
/// </summary>
public readonly struct Lane4 : ILane<Lane4>
{
    /// <inheritdoc />
    public static int LaneCount => 4;

    private readonly Vector128<float> vector;

    /// <summary/>
    public Lane4(float x, float y, float z, float w)
    {
        vector = Vector128.Create(x, y, z, w);
    }

    /// <summary/>
    public Lane4(Vector4 v)
    {
        vector = Vector128.Create(v.X, v.Y, v.Z, v.W);
    }

    /// <summary/>
    public Lane4(Color c) : this(c.ToVector4()) { }

    private Lane4(Vector128<float> v)
    {
        vector = v;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Add(Lane4 a, Lane4 b)
    {
        return new Lane4(a.vector + b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Sub(Lane4 a, Lane4 b)
    {
        return new Lane4(a.vector - b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Mul(Lane4 a, Lane4 b)
    {
        return new Lane4(a.vector * b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Div(Lane4 a, Lane4 b)
    {
        return new Lane4(a.vector / b.vector);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Lane4 Lerp(Lane4 a, Lane4 b, float t)
    {
        return new Lane4(a.vector + (b.vector - a.vector) * t);
    }
}

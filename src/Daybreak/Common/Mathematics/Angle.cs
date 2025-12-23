using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     A type-safe representation of an angle, stored in radians.
///     <br />
///     Angles may accumulate freely; use <see cref="NormalizedPositive"/> or
///     <see cref="NormalizedSigned"/> when a bounded domain is required.
///     <br />
///     All built-in operators operate directly on <see cref="Radians"/>.
///     This includes comparison operators and <see cref="CompareTo"/>,
///     which perform linear (non-wrapping) comparisons.
///     <br />
///     For circular or directional semantics, see
///     <see cref="ShortestDeltaTo"/>, <see cref="ShortestDistance"/>,
///     <see cref="IsClockwiseFrom"/>, <see cref="IsCounterClockwiseFrom"/>,
///     and <see cref="IsBetween"/>.
/// </summary>
public readonly record struct Angle : IAdditionOperators<Angle, Angle, Angle>,
                                      ISubtractionOperators<Angle, Angle, Angle>,
                                      IUnaryNegationOperators<Angle, Angle>,
                                      IMultiplyOperators<Angle, float, Angle>,
                                      IDivisionOperators<Angle, float, Angle>,
                                      IComparisonOperators<Angle, Angle, bool>,
                                      IComparable<Angle>
{
    /// <summary>
    ///     Represents the direction of a rotation.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        ///     A clockwise rotation.
        /// </summary>
        Clockwise,

        /// <summary>
        ///     A counter-clockwise rotation.
        /// </summary>
        CounterClockwise,
    }

    private const float default_equality_tolerance = 1e-5f;

    /// <summary>
    ///     An angle of 0 radians (0 degrees).
    /// </summary>
    public static Angle Zero => new(0f);

    /// <summary>
    ///     An angle of π radians (180 degrees).
    /// </summary>
    public static Angle Pi => new(MathF.PI);

    /// <summary>
    ///     An angle of 2π (or τ) radians (360 degrees).
    /// </summary>
    /// <seealso cref="Tau"/>
    public static Angle TwoPi => new(MathF.Tau);

    /// <summary>
    ///     An angle of 2π (or τ) radians (360 degrees).
    /// </summary>
    /// <seealso cref="TwoPi"/>
    public static Angle Tau => new(MathF.Tau);

    /// <summary>
    ///     An angle of π radians (90 degrees); a right angle.
    /// </summary>
    public static Angle HalfPi => new(MathF.PI * 0.5f);

    /// <summary>
    ///     The value of this angle in radians.
    /// </summary>
    public float Radians { get; }

    private Angle(float radians)
    {
        Radians = radians;
    }

    /// <summary>
    ///     Returns the value of this angle in degrees.  Unlike
    ///     <see cref="Radians"/>, degrees must be converted to, so only use
    ///     this if it's necessary.
    /// </summary>
    /// <returns></returns>
    [MustUseReturnValue]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float ToDegrees()
    {
        return Radians * (180f / MathF.PI);
    }

    /// <summary>
    ///     Normalizes this angle to the range [0, 2π) and returns the value as
    ///     a new angle.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Angle NormalizedPositive()
    {
        var r = Radians % MathF.Tau;
        if (r < 0f)
        {
            r += MathF.Tau;
        }

        return new Angle(r);
    }

    /// <summary>
    ///     Normalizes this angle to the range (-π, π] and returns the value as
    ///     a new angle.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Angle NormalizedSigned()
    {
        var r = MathF.IEEERemainder(Radians, MathF.Tau);
        return new Angle(r);
    }

    /// <summary>
    ///     Returns the signed, shortest rotation required to rotate from this
    ///     angle to <paramref name="other"/>.
    /// </summary>
    /// <remarks>
    ///     The result is normalized to the range (-π, π].
    ///     A positive value indicates a counter-clockwise rotation;
    ///     a negative value indicates a clockwise rotation.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Angle ShortestDeltaTo(Angle other)
    {
        var delta = other.Radians - Radians;
        {
            delta = MathF.IEEERemainder(delta, MathF.Tau);
        }

        return new Angle(delta);
    }

    /// <summary>
    ///     Determines whether reaching this angle from
    ///     <paramref name="other"/> requires a clockwise rotation,
    ///     using the shortest path in a <see cref="NormalizedSigned"/> domain.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsClockwiseFrom(Angle other)
    {
        return ShortestDeltaTo(other).Radians < 0f;
    }

    /// <summary>
    ///     Determines whether reaching this angle from
    ///     <paramref name="other"/> requires a counter-clockwise rotation,
    ///     using the shortest path in a <see cref="NormalizedSigned"/> domain.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsCounterClockwiseFrom(Angle other)
    {
        return ShortestDeltaTo(other).Radians > 0f;
    }

    /// <summary>
    ///     Determines whether <paramref name="value"/> lies between
    ///     <paramref name="start"/> and <paramref name="end"/> when traversing
    ///     the circle in the specified <paramref name="direction"/>.
    /// </summary>
    /// <remarks>
    ///     This method operates on the raw radian values of the angles.
    ///     Callers may wish to normalize inputs to a common domain
    ///     (for example, <see cref="NormalizedPositive"/>) before calling.
    /// </remarks>
    public static bool IsBetween(
        Angle value,
        Angle start,
        Angle end,
        Direction direction
    )
    {
        var v = value.Radians;
        var s = start.Radians;
        var e = end.Radians;

        switch (direction)
        {
            case Direction.Clockwise:
                if (s < e)
                {
                    s += MathF.Tau;
                }

                if (v < s)
                {
                    v += MathF.Tau;
                }

                return v <= e + MathF.Tau;

            case Direction.CounterClockwise:
                if (e < s)
                {
                    e += MathF.Tau;
                }

                if (v < e)
                {
                    v += MathF.Tau;
                }

                return v <= s + MathF.Tau;

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }

#region Arithmetic
    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle operator +(Angle left, Angle right)
    {
        return new Angle(left.Radians + right.Radians);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle operator -(Angle left, Angle right)
    {
        return new Angle(left.Radians - right.Radians);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle operator -(Angle value)
    {
        return new Angle(-value.Radians);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle operator *(Angle left, float right)
    {
        return new Angle(left.Radians * right);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle operator /(Angle left, float right)
    {
        return new Angle(left.Radians / right);
    }
#endregion

#region Comparison
    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Angle left, Angle right)
    {
        return left.Radians > right.Radians;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Angle left, Angle right)
    {
        return left.Radians >= right.Radians;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Angle left, Angle right)
    {
        return left.Radians < right.Radians;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Angle left, Angle right)
    {
        return left.Radians <= right.Radians;
    }

    /// <summary>
    ///     Compares <see cref="Radians"/> as float values (in no particular
    ///     normalized space).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Angle other)
    {
        return Radians.CompareTo(other.Radians);
    }

    /// <summary>
    ///     Determines whether this angle is approximately equal to
    ///     <paramref name="other"/> within the given tolerance, using
    ///     raw radian values (no wrapping).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ApproximatelyEquals(Angle other, float tolerance = default_equality_tolerance)
    {
        return MathF.Abs(Radians - other.Radians) <= tolerance;
    }

    /// <summary>
    ///     Determines whether this angle is approximately equal to
    ///     <paramref name="other"/> within the given tolerance,
    ///     accounting for circular wrap-around.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ApproximatelyEqualsCircular(Angle other, float tolerance = default_equality_tolerance)
    {
        return MathF.Abs(ShortestDeltaTo(other).Radians) <= tolerance;
    }
#endregion

    /// <summary>
    ///     Explicitly casts this <see cref="Angle"/> down to its radian value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator float(Angle value)
    {
        return value.Radians;
    }

#region Factories
    /// <summary>
    ///     Initializes this angle with the value <paramref name="radians"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle FromRadians(float radians)
    {
        return new Angle(radians);
    }

    /// <summary>
    ///     Initializes this angle with the value <paramref name="degrees"/>,
    ///     converted to radians.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle FromDegrees(float degrees)
    {
        return new Angle(degrees * (MathF.PI / 180f));
    }

    /// <summary>
    ///     Returns the signed shortest angular separation between
    ///     <paramref name="a"/> and <paramref name="b"/>.
    /// </summary>
    /// <remarks>
    ///     The result is normalized to the range (-π, π].
    ///     To compute a directed rotation from one angle to another,
    ///     prefer <see cref="ShortestDeltaTo"/>.
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle ShortestDistance(Angle a, Angle b)
    {
        var delta = a.Radians - b.Radians;
        {
            delta = MathF.IEEERemainder(delta, MathF.Tau);
        }

        return new Angle(delta);
    }

    /// <summary>
    ///     Returns the absolute value of the signed, shortest angular
    ///     separation between <paramref name="a"/> and <paramref name="b"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Angle AbsShortestDistance(Angle a, Angle b)
    {
        return new Angle(MathF.Abs(ShortestDistance(a, b).Radians));
    }
#endregion
}

/// <summary>
///     Additional extensions for <see cref="Angle"/>s.
/// </summary>
public static class AngleExtensions
{
    extension(Angle a)
    {
        /// <inheritdoc cref="MathF.Sin"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Sin()
        {
            return MathF.Sin(a.Radians);
        }

        /// <inheritdoc cref="MathF.Cos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Cos()
        {
            return MathF.Cos(a.Radians);
        }

        /// <inheritdoc cref="MathF.SinCos"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (float Sin, float Cos) SinCos()
        {
            return MathF.SinCos(a.Radians);
        }

        /// <summary>
        ///     Creates a vector of length 1 with this angle's rotation.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2 ToUnitVector()
        {
            var (sin, cos) = a.SinCos();
            return new Vector2(cos, sin);
        }

        /// <summary>
        ///     Initializes this angle with the value of the rotation of the
        ///     vector <paramref name="v"/>.
        /// </summary>
        public static Angle FromVector(Vector2 v)
        {
            return Angle.FromRadians(MathF.Atan2(v.Y, v.X));
        }

        /// <summary>
        ///     Returns a new angle rounded to the nearest multiple of the
        ///     <paramref name="increment"/>, effectively quantizing it.
        /// </summary>
        public Angle Snap(Angle increment)
        {
            var r = MathF.Round(a.Radians / increment.Radians) * increment.Radians;
            return Angle.FromRadians(r);
        }

        /// <summary>
        ///     Linearly interpolates from this angle to <paramref name="target"/>
        ///     along the shortest path.
        /// </summary>
        /// <param name="target">The target angle to lerp toward.</param>
        /// <param name="t">
        ///     Interpolation factor in [0, 1].
        /// </param>
        public Angle LerpTo(Angle target, float t)
        {
            var delta = a.ShortestDeltaTo(target);
            return a + delta * t;
        }

        /// <summary>
        ///     Rotates this angle toward <paramref name="target"/> by at most
        ///     <paramref name="maxDelta"/> radians, using the shortest path.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Angle MoveToward(Angle target, Angle maxDelta)
        {
            var delta = a.ShortestDeltaTo(target).Radians;
            var clamped = Math.Clamp(delta, -maxDelta.Radians, maxDelta.Radians);
            return Angle.FromRadians(a.Radians + clamped);
        }
    }
}

using System.Runtime.CompilerServices;
using Daybreak.Core.SourceGen;

namespace Daybreak.Common;

/// <summary>
///     Provides common curve and interpolation/easing functions.
/// </summary>
public static partial class Curves
{
    /// <summary>
    ///     Defines the signature for an interpolator function which takes
    ///     parameters <paramref name="a"/> amd <paramref name="b"/> and
    ///     produces a return value based on progress <paramref name="t"/>.
    /// </summary>
    /// <typeparam name="TValue">The type to be operated on.</typeparam>
    public delegate TValue Interpolator<TValue>(TValue a, TValue b, float t);

    [GenerateLaneOverloads]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TLane Lerp<TLane>(TLane a, TLane b, float t)
        where TLane : unmanaged, ILane<TLane>
    {
        Interpolator<TLane> c = Lerp;

        return TLane.Lerp(a, b, t);
    }
}

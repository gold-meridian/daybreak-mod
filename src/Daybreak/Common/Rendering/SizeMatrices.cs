using Microsoft.Xna.Framework;

namespace Daybreak.Common.Rendering;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/f33541642d1f2aec575b2a4f580afe13a2de2cfa/Common/Buffers.cs>.
 *
 * Generously licensed to us under AGPL v3.0.
 */

/// <summary>
///     Utilities to help with scaling and applying matrices to vectors.
/// </summary>
public static class SizeMatrices
{
    /// <summary>
    ///     Half-sized matrix, omitting the Z axis.
    /// </summary>
    public static Matrix Half { get; } = Matrix.CreateScale(0.5f, 0.5f, 1f);

    /// <summary>
    ///     Double-sized matrix, omitting the Z axis.
    /// </summary>
    public static Matrix Double { get; } = Matrix.CreateScale(2f, 2f, 1f);

    /// <summary>
    ///     Scales the given vector by the scale matrix.
    /// </summary>
    public static Vector2 Scale(this Vector2 vector, Matrix matrix)
    {
        return new Vector2(
            matrix.M11 * vector.X,
            matrix.M22 * vector.Y
        );
    }

    /// <summary>
    ///     Transforms the given vector by the matrix.
    /// </summary>
    public static Vector2 Transform(this Vector2 vector, Matrix matrix)
    {
        return Vector2.Transform(vector, matrix);
    }
}

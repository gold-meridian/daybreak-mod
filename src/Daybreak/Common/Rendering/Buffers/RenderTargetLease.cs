using System;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Rendering;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/f33541642d1f2aec575b2a4f580afe13a2de2cfa/Common/Buffers.cs>.
 *
 * Generously leased to us under AGPL v3.0.
 */

/// <summary>
///     A leased target from a pool, to be returned back.
/// </summary>
/// <param name="pool">The pool leasing the target.</param>
/// <param name="target">The target being leased.</param>
public sealed class RenderTargetLease(
    RenderTarget2D target,
    RenderTargetPool pool
) : IDisposable
{
    /// <summary>
    ///     The target being leased.
    /// </summary>
    public RenderTarget2D Target { get; set; } = target;

    /// <summary>
    ///     Returns the target back to the pool.
    /// </summary>
    public void Dispose()
    {
        pool.Return(Target);
    }
}

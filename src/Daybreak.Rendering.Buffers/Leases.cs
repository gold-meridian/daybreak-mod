using System;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Rendering.Buffers;

/// <summary>
///     Represents a lease on a buffer.
/// </summary>
public interface IBufferLease<TBuffer> : IDisposable
{
    /// <summary>
    ///     The owned buffer.
    /// </summary>
    TBuffer Buffer { get; set; }
}

/// <summary>
///     A leased <see cref="RenderTarget2D"/>.
/// </summary>
/// <param name="provider">The provider leasing the target.</param>
/// <param name="target">The target being leased.</param>
public sealed class RenderTargetLease(
    RenderTarget2D target,
    IBufferProvider<RenderTarget2D> provider
) : IBufferLease<RenderTarget2D>
{
    /// <summary>
    ///     The target being leased.
    /// </summary>
    public RenderTarget2D Target { get; set; } = target;

    RenderTarget2D IBufferLease<RenderTarget2D>.Buffer
    {
        get => Target;
        set => Target = value;
    }

    /// <summary>
    ///     Returns the target to the pool.
    /// </summary>
    public void Dispose()
    {
        provider.Return(this);
    }
}

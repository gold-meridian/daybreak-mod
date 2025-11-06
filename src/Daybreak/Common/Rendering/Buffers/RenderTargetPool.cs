using System;
using System.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Rendering;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/ba401cf5ca519e26fb8043cdf81ccefce57d2f3d/Common/Buffers.cs>.
 *
 * Generously leased to us under AGPL v3.0.
 */

/// <summary>
///     Provides a resource pool that enables reusing instances of render
///     targets.
/// </summary>
/// <remarks>
///     Renting and returning buffers with a <see cref="RenderTargetPool"/> can
///     increase performance in situations where targets are created and
///     destroyed frequently, resulting in excessive use of VRAM.
///     <br />
///     This class is not thread-safe, and all methods and their implementations
///     are expected to be invoked solely on the Main Thread unless explicitly
///     specified otherwise.
/// </remarks>
public abstract class RenderTargetPool : IDisposable
{
    // Store as field of derived shared type so the JIT can de-virtualize calls
    // to it when the Shared property gets inlined.
    private static readonly SharedRenderTargetPool shared = new();

    /// <summary>
    ///     Retrieves a shared <see cref="RenderTargetPool"/> instance.
    /// </summary>
    /// <remarks>
    ///     The shared pool provides a default implementation of
    ///     <see cref="RenderTargetPool"/> that's intended for general
    ///     applicability.  It assumes targets should not be manually cleared
    ///     and always returns a target of the exact request descriptor and
    ///     dimensions.
    /// </remarks>
    public static RenderTargetPool Shared => shared;

    /// <summary>
    ///     Creates a new <see cref="RenderTargetPool"/> instance using default
    ///     configurable options.
    /// </summary>
    /// <returns>A new <see cref="RenderTargetPool"/> instance.</returns>
    public static RenderTargetPool Create()
    {
        return new ConfigurableRenderTargetPool();
    }

    /// <summary>
    ///     Retrieves a buffer that is of the exact specified dimensions
    ///     <paramref name="width"/> and <paramref name="height"/> with the
    ///     given render target <paramref name="descriptor"/>.
    /// </summary>
    /// <param name="device">The device to initialize with.</param>
    /// <param name="width">The width of the target.</param>
    /// <param name="height">The height of the target.</param>
    /// <param name="descriptor">The initialization parameters.</param>
    /// <returns>
    ///     A leased target which should be disposed upon use, generally
    ///     returning it to the pool automatically.
    /// </returns>
    /// <remarks>
    ///     This buffer is loaned to the caller and should be returned to the
    ///     same pool via, <see cref="Return"/> so that it may be reused in
    ///     subsequent usage of <see cref="Rent"/>.  It is not a fatal error to
    ///     not return a rented buffer, but failure to do so may lead to
    ///     decreased application performance, as the pool may need to create a
    ///     new buffer to replace the one lost.  The default
    ///     <see cref="RenderTargetPool"/> implementation returns a leased
    ///     target that will automatically return the pool on disposal, but
    ///     different implementations or configurations are not required to do
    ///     so.
    /// </remarks>
    public abstract RenderTargetLease Rent(
        GraphicsDevice device,
        int width,
        int height,
        RenderTargetDescriptor descriptor
    );

    /// <summary>
    ///     Returns to the pool a render target that was previously obtained via
    ///     <see cref="Rent"/> on the same <see name="RenderTargetPool"/>.
    /// </summary>
    /// <param name="target">
    ///     The buffer previously obtained from <see cref="Rent"/> to return to
    ///     the pool.
    /// </param>
    /// <param name="clearTarget">
    ///     If <see langword="true"/> and if the pool will store the buffer to
    ///     enable subsequent reuse, <see name="Return"/> will clear the buffer
    ///     <b>regardless of the usage parameter</b> so that a subsequent
    ///     consumer via <see cref="Rent"/> will not see the previous consumer's
    ///     content.  If <see langword="false"/> or if the pool will release the
    ///     buffer, the target's contents are left unchanged.
    /// </param>
    /// <remarks>
    ///     Once a buffer has been returned to the pool, the caller gives up all
    ///     ownership of the buffer and must not use it.  The reference returned
    ///     from a given call to <see cref="Rent"/> must only be returned via
    ///     <see cref="Return"/> once.  The default
    ///     <see cref="RenderTargetPool"/> may hold onto the returned buffer in
    ///     order to rent it again, or it my release the returned buffer if it's
    ///     determined that the pool already has enough buffers stored.
    /// </remarks>
    public abstract void Return(RenderTarget2D target, bool clearTarget = false);
}

/// <summary>
///     Extension methods for <see cref="RenderTargetPool"/> that operate
///     agnostically on any implementation.
/// </summary>
public static class RenderTargetPoolExtensions
{
    // TODO: Vector2 scale extensions and individual float width/height
    //       extensions.
    
    /// <summary>
    ///     Retrieves a buffer of size <paramref name="baseSize"/> scaled by
    ///     <paramref name="scale"/> rounded up to the nearest integer value and
    ///     with the given render target <paramref name="descriptor"/>.
    /// </summary>
    /// <param name="pool">The pool to rent from.</param>
    /// <param name="device">The device to initialize with.</param>
    /// <param name="baseSize">The base (unscaled) size of the target.</param>
    /// <param name="scale">The scale factor of the target.</param>
    /// <param name="descriptor">The initialization parameters.</param>
    /// <returns>
    ///     A leased target which should be disposed upon use, generally
    ///     returning it to the pool automatically.
    /// </returns>
    public static RenderTargetLease RentScaled(
        this RenderTargetPool pool,
        GraphicsDevice device,
        Point baseSize,
        float scale,
        RenderTargetDescriptor descriptor
    )
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(scale, 0f);

        var width = Math.Max(1, (int)MathF.Ceiling(baseSize.X * scale));
        var height = Math.Max(1, (int)MathF.Ceiling(baseSize.Y * scale));
        return pool.Rent(device, width, height, descriptor);
    }
}

internal sealed class SharedRenderTargetPool : RenderTargetPool { }

internal sealed class ConfigurableRenderTargetPool : RenderTargetPool { }

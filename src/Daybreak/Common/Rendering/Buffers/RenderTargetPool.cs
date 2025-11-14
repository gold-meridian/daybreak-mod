using System;
using System.Buffers;
using System.Collections.Generic;
using Daybreak.Common.Features.Hooks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Rendering;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/f33541642d1f2aec575b2a4f580afe13a2de2cfa/Common/Buffers.cs>.
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
    ///     <br />
    ///     Disposed on unload.
    /// </remarks>
    public static RenderTargetPool Shared => shared;

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
    ///     A leased target which should be disposed upon use, automatically
    ///     returning the target to the pool.
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
    /// <param name="lease">
    ///     The lease previously obtained from <see cref="Rent"/> to return to
    ///     the pool.
    /// </param>
    /// <remarks>
    ///     This is automatically called by
    ///     <see cref="RenderTargetLease.Dispose"/> and generally should not be
    ///     called manually without being explicitly told to do so.
    ///     <br />
    ///     Once a buffer has been returned to the pool, the caller gives up all
    ///     ownership of the buffer and must not use it.  The reference returned
    ///     from a given call to <see cref="Rent"/> must only be returned via
    ///     <see cref="Return"/> once.  The default
    ///     <see cref="RenderTargetPool"/> may hold onto the returned buffer in
    ///     order to rent it again, or it my release the returned buffer if it's
    ///     determined that the pool already has enough buffers stored.
    /// </remarks>
    public abstract void Return(RenderTargetLease lease);

    /// <summary>
    ///     Disposes of the pool and releases any owned render targets,
    ///     including ones still potentially leased.
    ///     <remarks />
    ///     Generally, disposal should only be performed when either the
    ///     consumer has ownership over the pool and knows when all buffers have
    ///     been returned, or at the very end of execution and disposal must
    ///     occur (such as during mod unloading).
    /// </summary>
    public abstract void Dispose();

    private static readonly List<RenderTargetLease> leases_to_clear = [];

    /// <summary>
    ///     Queues a lease to be disposed of on the next render frame for cases
    ///     where ownership of the lease is given up for rendering during a
    ///     frame.
    /// </summary>
    /// <param name="lease">
    ///     The lease to dispose of at the start of the next frame.
    /// </param>
    public static void ReturnNextFrame(RenderTargetLease lease)
    {
        leases_to_clear.Add(lease);
    }

    [OnLoad(Side = ModSide.Client)]
    private static void RegisterFrameLeaseDisposal()
    {
        On_Main.DoDraw += (orig, self, time) =>
        {
            foreach (var lease in leases_to_clear)
            {
                lease.Dispose();
            }

            leases_to_clear.Clear();

            orig(self, time);
        };
    }

    [OnUnload(Side = ModSide.Client)]
    private static void UnloadShared()
    {
        Main.RunOnMainThread(
            () =>
            {
                Shared.Dispose();
            }
        );
    }
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
    ///     A leased target which should be disposed upon use, automatically
    ///     returning the target to the pool.
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

// TODO: Account for different graphics devices?
internal sealed class SharedRenderTargetPool : RenderTargetPool
{
    private readonly record struct Key(
        int Width,
        int Height,
        RenderTargetDescriptor Descriptor
    )
    {
        public static Key From(RenderTarget2D target)
        {
            return new Key(target.Width, target.Height, RenderTargetDescriptor.From(target));
        }
    }

    private readonly Dictionary<Key, Stack<RenderTarget2D>> cache = [];
    private bool disposed;

    public override RenderTargetLease Rent(GraphicsDevice device, int width, int height, RenderTargetDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(device);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0);
        ObjectDisposedException.ThrowIf(disposed, this);

        var key = new Key(width, height, descriptor);
        if (!cache.TryGetValue(key, out var stack))
        {
            cache[key] = stack = [];
        }

        var target = stack.Count > 0
            ? stack.Pop()
            : descriptor.Create(device, width, height);

        return new RenderTargetLease(target, this);
    }

    public override void Return(RenderTargetLease lease)
    {
        ArgumentNullException.ThrowIfNull(lease);
        ObjectDisposedException.ThrowIf(disposed, this);

        var key = Key.From(lease.Target);
        if (!cache.TryGetValue(key, out var stack))
        {
            cache[key] = stack = [];
        }

        stack.Push(lease.Target);
    }

    public override void Dispose()
    {
        if (disposed)
        {
            return;
        }

        Trim();
        disposed = true;
    }

    private void Trim()
    {
        foreach (var stack in cache.Values)
        {
            while (stack.Count > 0)
            {
                stack.Pop().Dispose();
            }
        }

        cache.Clear();
    }
}

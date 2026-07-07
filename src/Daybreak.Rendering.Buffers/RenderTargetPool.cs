using System;
using System.Collections.Generic;
using System.Diagnostics;
using Daybreak.Hooks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Rendering.Buffers;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/f33541642d1f2aec575b2a4f580afe13a2de2cfa/Common/Buffers.cs>.
 *
 * Generously licensed to us under AGPL v3.0.
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
///     are expected to be invoked solely on the main thread unless explicitly
///     specified otherwise.
/// </remarks>
public abstract class RenderTargetPool : IBufferProvider<RenderTarget2D>, IDisposable
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
    public abstract void Return(IBufferLease<RenderTarget2D> lease);

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

    private static readonly List<IBufferLease<RenderTarget2D>> leases_to_clear = [];

    /// <summary>
    ///     Queues a lease to be disposed of on the next render frame for cases
    ///     where ownership of the lease is given up for rendering during a
    ///     frame.
    /// </summary>
    /// <param name="lease">
    ///     The lease to dispose of at the start of the next frame.
    /// </param>
    /// <remarks>
    ///     This API should generally be avoided when possible.  It's a
    ///     last-resort option for when you cannot guarantee ownership over a
    ///     target.
    /// </remarks>
    public static void ReturnNextFrame(IBufferLease<RenderTarget2D> lease)
    {
        leases_to_clear.Add(lease);
    }

    [OnLoad(Side = ModSide.Client)]
    private static void HandlePreFrameActions()
    {
        Main.RunOnMainThread(
            () =>
            {
                On_Main.DoDraw += [StackTraceHidden](orig, self, time) =>
                {
                    ClearAndDisposeOfLeases();
                    TrimOldLeasesFromSharedPool();

                    orig(self, time);
                };
            }
        );
    }

    private static void ClearAndDisposeOfLeases()
    {
        foreach (var lease in leases_to_clear)
        {
            lease.Dispose();
        }

        leases_to_clear.Clear();
    }

    private static void TrimOldLeasesFromSharedPool()
    {
        shared.TrimAged();
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

    /// <param name="pool">The pool to rent from.</param>
    extension(RenderTargetPool pool)
    {
        /// <summary>
        ///     See <see cref="RenderTargetPool.Rent"/>.
        /// </summary>
        public RenderTargetLease Rent(
            GraphicsDevice device,
            int width,
            int height
        )
        {
            return pool.Rent(
                device,
                width,
                height,
                RenderTargetDescriptor.Default
            );
        }

        /// <summary>
        ///     Retrieves a buffer of size <paramref name="baseSize"/> scaled by
        ///     <paramref name="scale"/> rounded up to the nearest integer
        ///     value.
        /// </summary>
        /// <param name="device">The device to initialize with.</param>
        /// <param name="baseSize">The base (unscaled) size of the target.</param>
        /// <param name="scale">The scale factor of the target.</param>
        /// <returns>
        ///     A leased target that should be disposed upon use, automatically
        ///     returning the target to the pool.
        /// </returns>
        public RenderTargetLease RentScaled(
            GraphicsDevice device,
            Point baseSize,
            float scale
        )
        {
            return pool.RentScaled(
                device,
                baseSize,
                scale,
                RenderTargetDescriptor.Default
            );
        }

        /// <summary>
        ///     Retrieves a buffer of size <paramref name="baseSize"/> scaled by
        ///     <paramref name="scale"/> rounded up to the nearest integer value and
        ///     with the given render target <paramref name="descriptor"/>.
        /// </summary>
        /// <param name="device">The device to initialize with.</param>
        /// <param name="baseSize">The base (unscaled) size of the target.</param>
        /// <param name="scale">The scale factor of the target.</param>
        /// <param name="descriptor">The initialization parameters.</param>
        /// <returns>
        ///     A leased target that should be disposed upon use, automatically
        ///     returning the target to the pool.
        /// </returns>
        public RenderTargetLease RentScaled(
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

    private sealed class Entry
    {
        public Stack<RenderTarget2D> Targets { get; } = [];

        public DateTime LastUsed { get; set; } = DateTime.UtcNow;
    }

    // TODO: We can tweak these later for performance?
    private const int max_per_key = 4;         // Max identical targets.
    private const int max_total_targets = 128; // Max targets stored.
    private static readonly TimeSpan max_idle_time = TimeSpan.FromSeconds(5);
    private static readonly TimeSpan minimum_trim_time = TimeSpan.FromSeconds(1);

    private readonly Dictionary<Key, Entry> cache = [];
    private DateTime lastTrimmed = DateTime.UtcNow;
    private int totalCached;
    private bool disposed;

    public override RenderTargetLease Rent(GraphicsDevice device, int width, int height, RenderTargetDescriptor descriptor)
    {
        ArgumentNullException.ThrowIfNull(device);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0);
        ObjectDisposedException.ThrowIf(disposed, this);

        var key = new Key(width, height, descriptor);
        if (!cache.TryGetValue(key, out var entry))
        {
            cache[key] = entry = new Entry();
        }
        else
        {
            entry.LastUsed = DateTime.UtcNow;
        }

        RenderTarget2D target;
        if (entry.Targets.Count > 0)
        {
            target = entry.Targets.Pop();
            totalCached--;
        }
        else
        {
            target = descriptor.Create(device, width, height);
        }

        return new RenderTargetLease(target, this);
    }

    public override void Return(IBufferLease<RenderTarget2D> lease)
    {
        ArgumentNullException.ThrowIfNull(lease);
        ObjectDisposedException.ThrowIf(disposed, this);

        var key = Key.From(lease.Buffer);
        if (!cache.TryGetValue(key, out var entry))
        {
            cache[key] = entry = new Entry();
        }
        else
        {
            // Mark it as used on return because that's realistically where the
            // countdown should begin.
            entry.LastUsed = DateTime.UtcNow;
        }

        if (entry.Targets.Count < max_per_key)
        {
            if (totalCached >= max_total_targets)
            {
                lease.Buffer.Dispose();
                return;
            }

            entry.Targets.Push(lease.Buffer);
            totalCached++;
        }
        else
        {
            lease.Buffer.Dispose();
        }
    }

    public override void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        foreach (var entry in cache.Values)
        {
            while (entry.Targets.Count > 0)
            {
                entry.Targets.Pop().Dispose();
                totalCached--;
            }
        }

        cache.Clear();
    }

    internal void TrimAged()
    {
        var now = DateTime.UtcNow;

        if (now - lastTrimmed < minimum_trim_time)
        {
            return;
        }

        lastTrimmed = now;

        var removed = new List<Key>();
        foreach (var (key, entry) in cache)
        {
            if (now - entry.LastUsed <= max_idle_time)
            {
                continue;
            }

            while (entry.Targets.Count > 0)
            {
                entry.Targets.Pop().Dispose();
                totalCached--;
            }

            removed.Add(key);
        }

        foreach (var key in removed)
        {
            cache.Remove(key);
        }
    }
}

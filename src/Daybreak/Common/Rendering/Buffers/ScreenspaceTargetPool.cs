using System;
using System.Collections.Generic;
using Daybreak.Common.Features.Hooks;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Rendering;

/// <summary>
///     A <see cref="RenderTargetPool"/> that provides APIs for creating
///     references to render targets that are dependent on the screen size.
///     <br />
///     Targets created by this pool are not cached for reuse later.  This API
///     is designed to leverage the strong reference to a target provided by
///     <see cref="RenderTargetLease"/>.
/// </summary>
public sealed class ScreenspaceTargetPool : RenderTargetPool
{
    /// <summary>
    ///     Retrieves the shared <see cref="ScreenspaceTargetPool"/> instance.
    /// </summary>
    /// <remarks>
    ///     Disposed on unload.
    /// </remarks>
    public static ScreenspaceTargetPool Shared { get; } = new();

    /// <summary>
    /// 
    /// </summary>
    public delegate (int Width, int Height) GetTargetSize(
        int backbufferWidth,
        int backbufferHeight,
        int offscreenTargetWidth,
        int offscreenTargetHeight
    );

    private readonly Dictionary<RenderTargetLease, GetTargetSize> cache = [];
    private bool disposed;

    private ScreenspaceTargetPool() { }

    /// <inheritdoc/>
    public override RenderTargetLease Rent(
        GraphicsDevice device,
        int width,
        int height,
        RenderTargetDescriptor descriptor
    )
    {
        ArgumentNullException.ThrowIfNull(device);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0);
        ObjectDisposedException.ThrowIf(disposed, this);

        return Rent(device, (_, _, _, _) => (width, height), descriptor);
    }

    /// <summary>
    ///     Rents a target with varying width and height, to be recalculated
    ///     whenever the screen is resized.  For the lifetime of the lease, this
    ///     pool will re-initialize the given target whenever the computed width
    ///     and height do not match the current size of the target on screen
    ///     size change/vanilla RT invalidation.
    /// </summary>
    public RenderTargetLease Rent(
        GraphicsDevice device,
        GetTargetSize targetSizeCallback,
        RenderTargetDescriptor descriptor
    )
    {
        ArgumentNullException.ThrowIfNull(device);
        ArgumentNullException.ThrowIfNull(targetSizeCallback);
        ObjectDisposedException.ThrowIf(disposed, this);

        GetTargetSizes(
            device,
            out var backbufferWidth,
            out var backbufferHeight,
            out var offscreenTargetWidth,
            out var offscreenTargetHeight
        );
        var (width, height) = targetSizeCallback(
            backbufferWidth,
            backbufferHeight,
            offscreenTargetWidth,
            offscreenTargetHeight
        );

        var target = descriptor.Create(device, width, height);
        var lease = new RenderTargetLease(target, this);
        {
            cache[lease] = targetSizeCallback;
        }

        return lease;
    }

    /// <inheritdoc/>
    public override void Return(RenderTargetLease lease)
    {
        ArgumentNullException.ThrowIfNull(lease);
        ObjectDisposedException.ThrowIf(disposed, this);

        if (!cache.Remove(lease))
        {
            return;
        }

        lease.Target.Dispose();
    }

    /// <inheritdoc/>
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
        foreach (var lease in cache.Keys)
        {
            lease.Target.Dispose();
        }

        cache.Clear();
    }

    private static void GetTargetSizes(
        GraphicsDevice device,
        out int backbufferWidth,
        out int backbufferHeight,
        out int offscreenTargetWidth,
        out int offscreenTargetHeight
    )
    {
        backbufferWidth = device.PresentationParameters.BackBufferWidth;
        backbufferHeight = device.PresentationParameters.BackBufferHeight;
        offscreenTargetWidth = Main.instance.tileTarget.Width;
        offscreenTargetHeight = Main.instance.tileTarget.Height;
    }

    [OnUnload(Side = ModSide.Client)]
    private static void AddHooks()
    {
        On_Main.EnsureRenderTargetContent += (orig, self) =>
        {
            // Let it run first to ensure tileTarget is initialized.  We depend
            // on it as an arbitrary target to provide us a fully-sized target
            // when includes offscreenRage in the target size.
            orig(self);

            GetTargetSizes(
                self.GraphicsDevice,
                out var backbufferWidth,
                out var backbufferHeight,
                out var offscreenTargetWidth,
                out var offscreenTargetHeight
            );

            foreach (var (lease, sizeCallback) in Shared.cache)
            {
                var (width, height) = sizeCallback(
                    backbufferWidth,
                    backbufferHeight,
                    offscreenTargetWidth,
                    offscreenTargetHeight
                );

                if (lease.Target.Width == width && lease.Target.Height == height)
                {
                    continue;
                }

                lease.Target.Dispose();
                lease.Target = RenderTargetDescriptor.From(lease.Target).Create(self.GraphicsDevice, width, height);
            }
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

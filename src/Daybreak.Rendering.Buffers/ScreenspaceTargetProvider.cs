using System;
using System.Collections.Generic;
using System.Diagnostics;
using Daybreak.Hooks;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Rendering.Buffers;

/// <summary>
///     Provides <see cref="RenderTarget2D"/>s that are dependent on the screen
///     size.
///     <br />
///     Leased targets will automatically be resized upon viewport change.
/// </summary>
public sealed class ScreenspaceTargetProvider : IBufferProvider<RenderTarget2D>, IDisposable
{
    /// <summary>
    ///     Retrieves the shared <see cref="ScreenspaceTargetProvider"/> instance.
    /// </summary>
    public static ScreenspaceTargetProvider Shared { get; } = new();

    /// <summary>
    ///     The callback for determining the size of a target based on the
    ///     current backbuffer.
    /// </summary>
    public delegate (int Width, int Height) GetTargetSize(
        int backbufferWidth,
        int backbufferHeight,
        int offscreenTargetWidth,
        int offscreenTargetHeight
    );

    private readonly Dictionary<IBufferLease<RenderTarget2D>, GetTargetSize> cache = [];
    private bool disposed;

    private ScreenspaceTargetProvider() { }

    /// <inheritdoc cref="Create(GraphicsDevice, GetTargetSize, RenderTargetDescriptor?)"/>
    public RenderTargetLease Create(
        GraphicsDevice device,
        RenderTargetDescriptor? descriptor = null
    )
    {
        return Create(
            device,
            (width, height) => (width, height),
            descriptor
        );
    }

    /// <inheritdoc cref="Create(GraphicsDevice, GetTargetSize, RenderTargetDescriptor?)"/>
    public RenderTargetLease Create(
        GraphicsDevice device,
        Func<int, int, (int, int)> targetSizeCallback,
        RenderTargetDescriptor? descriptor = null
    )
    {
        return Create(device, (width, height, _, _) => targetSizeCallback(width, height), descriptor);
    }

    /// <summary>
    ///     Creates a target with varying width and height, to be recalculated
    ///     whenever the screen is resized.  For the lifetime of the lease, this
    ///     provider will re-initialize the given target whenever the computed
    ///     width and height do not match the current size of the target on
    ///     screen size change/vanilla RT invalidation.
    /// </summary>
    public RenderTargetLease Create(
        GraphicsDevice device,
        GetTargetSize targetSizeCallback,
        RenderTargetDescriptor? descriptor = null
    )
    {
        ArgumentNullException.ThrowIfNull(device);
        ArgumentNullException.ThrowIfNull(targetSizeCallback);
        ObjectDisposedException.ThrowIf(disposed, this);

        descriptor ??= RenderTargetDescriptor.Default;

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

        var target = descriptor.Value.Create(device, width, height);
        var lease = new RenderTargetLease(target, this);
        {
            cache[lease] = targetSizeCallback;
        }

        return lease;
    }

    /// <summary>
    ///     Disposes of this lease and target.
    /// </summary>
    public void Return(IBufferLease<RenderTarget2D> lease)
    {
        ArgumentNullException.ThrowIfNull(lease);
        ObjectDisposedException.ThrowIf(disposed, this);

        if (!cache.Remove(lease))
        {
            return;
        }

        lease.Buffer.Dispose();
    }

    void IDisposable.Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;

        foreach (var lease in cache.Keys)
        {
            lease.Buffer.Dispose();
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
        offscreenTargetWidth = Main.tileTarget.Texture.Width;
        offscreenTargetHeight = Main.tileTarget.Texture.Height;
    }

    [OnLoad(Side = ModSide.Client)]
    private static void AddHooks()
    {
        On_Main.EnsureRenderTargetContent += [StackTraceHidden](orig, self) =>
        {
            // Let it run first to ensure tileTarget is initialized.  We depend
            // on it as an arbitrary target to provide us a fully sized target
            // when includes offscreenRage in the target size.
            orig(self);

            EnsureTargetSizes(self);
        };
    }

    private static void EnsureTargetSizes(Main self)
    {
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

            if (lease.Buffer.Width == width && lease.Buffer.Height == height)
            {
                continue;
            }

            lease.Buffer.Dispose();
            lease.Buffer = RenderTargetDescriptor.From(lease.Buffer).Create(self.GraphicsDevice, width, height);
        }
    }

    [OnUnload(Side = ModSide.Client)]
    private static void UnloadShared()
    {
        Main.RunOnMainThread(
            () =>
            {
                ((IDisposable)Shared).Dispose();
            }
        );
    }
}

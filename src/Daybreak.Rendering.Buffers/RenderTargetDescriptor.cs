using System;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Rendering.Buffers;

/// <summary>
///     Describes the creation parameters of a render target.
/// </summary>
/// <param name="Format"><see cref="RenderTarget2D.Format"/></param>
/// <param name="Depth"><see cref="RenderTarget2D.DepthStencilFormat"/></param>
/// <param name="MultiSampleCount"><see cref="RenderTarget2D.MultiSampleCount"/></param>
/// <param name="Usage"><see cref="RenderTarget2D.RenderTargetUsage"/></param>
/// <param name="GenerateMipmaps"><see cref="RenderTarget2D.LevelCount"/></param>
public readonly record struct RenderTargetDescriptor(
    SurfaceFormat Format,
    DepthFormat Depth,
    int MultiSampleCount,
    RenderTargetUsage Usage,
    bool GenerateMipmaps
)
{
    /// <summary>
    ///     Default creation parameters.
    /// </summary>
    public static RenderTargetDescriptor Default { get; } = new(
        SurfaceFormat.Color,
        DepthFormat.None,
        0,
        RenderTargetUsage.DiscardContents,
        false
    );

    /// <summary>
    ///     <see cref="Default"/> with usage set to
    ///     <see cref="RenderTargetUsage.PreserveContents"/>.
    /// </summary>
    public static RenderTargetDescriptor DefaultPreserveContents { get; } = new(
        SurfaceFormat.Color,
        DepthFormat.None,
        0,
        RenderTargetUsage.PreserveContents,
        false
    );

    /// <summary>
    ///     <see cref="RenderTarget2D.MultiSampleCount"/>
    /// </summary>
    public int MultiSampleCount { get; } = Math.Max(0, MultiSampleCount);

    /// <inheritdoc cref="Create(GraphicsDevice, int, int)"/>
    public RenderTarget2D Create(int width, int height)
    {
        return Create(Graphics.Device, width, height);
    }

    /// <summary>
    ///     Creates a new 2D render target from the descriptor.
    /// </summary>
    /// <param name="device">The device to create the target from.</param>
    /// <param name="width">The width of the target.</param>
    /// <param name="height">The height of the target.</param>
    public RenderTarget2D Create(GraphicsDevice device, int width, int height)
    {
        return new RenderTarget2D(device, width, height, GenerateMipmaps, Format, Depth, MultiSampleCount, Usage);
    }

    /// <inheritdoc cref="CreateCube(GraphicsDevice, int)"/>
    public RenderTargetCube CreateCube(int size)
    {
        return CreateCube(Graphics.Device, size);
    }

    /// <summary>
    ///     Creates a new cube render target from the descriptor.
    /// </summary>
    /// <param name="device">The device to create the target from.</param>
    /// <param name="size">The width and height of the cube faces.</param>
    public RenderTargetCube CreateCube(GraphicsDevice device, int size)
    {
        return new RenderTargetCube(device, size, GenerateMipmaps, Format, Depth, MultiSampleCount, Usage);
    }

    /// <summary>
    ///     Constructs a descriptor from an existing target.
    /// </summary>
    public static RenderTargetDescriptor From(RenderTarget2D target)
    {
        return new RenderTargetDescriptor(
            target.Format,
            target.DepthStencilFormat,
            target.MultiSampleCount,
            RenderTargetUsage.DiscardContents,
            target.LevelCount > 1
        );
    }

    /// <inheritdoc cref="From(RenderTarget2D)"/>
    public static RenderTargetDescriptor From(RenderTargetCube target)
    {
        return new RenderTargetDescriptor(
            target.Format,
            target.DepthStencilFormat,
            target.MultiSampleCount,
            RenderTargetUsage.DiscardContents,
            target.LevelCount > 1
        );
    }
}

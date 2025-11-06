using System;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Rendering;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/ba401cf5ca519e26fb8043cdf81ccefce57d2f3d/Common/Buffers.cs>.
 *
 * Generously leased to us under AGPL v3.0.
 */

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
    ///     <see cref="RenderTarget2D.MultiSampleCount"/>
    /// </summary>
    public int MultiSampleCount { get; } = Math.Max(0, MultiSampleCount);

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
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Rendering;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/f33541642d1f2aec575b2a4f580afe13a2de2cfa/Common/Buffers.cs>.
 *
 * Generously leased to us under AGPL v3.0.
 */

/// <summary>
///     Manages the scope of a render target to be rendered to, swapping out the
///     currently-used targets of a device on creation and replacing it with the
///     given target.  Switches back to the old targets upon disposal.
/// </summary>
public readonly struct RenderTargetScope : IDisposable
{
    private readonly GraphicsDevice graphicsDevice;
    private readonly RenderTargetBinding[] previous;

    /// <summary>
    ///     Creates a new scope, saving the current device targets and starts
    ///     rendering to the new one instead.
    /// </summary>
    /// <param name="target">The target to render to.</param>
    /// <param name="preserveContents">
    ///     Whether to ensure swapped targets preserve their contents.
    /// </param>
    /// <param name="clearColor">
    ///     If not null, clears the target to the given color.
    /// </param>
    public RenderTargetScope(
        RenderTarget2D target,
        bool preserveContents = true,
        Color? clearColor = null
    )
    {
        ArgumentNullException.ThrowIfNull(target);

        graphicsDevice = target.GraphicsDevice;
        previous = graphicsDevice.GetRenderTargets();

        if (preserveContents)
        {
            RenderTargetPreserver.PreserveBindings(previous);
        }

        graphicsDevice.SetRenderTarget(target);

        if (clearColor.HasValue)
        {
            graphicsDevice.Clear(clearColor.Value);
        }
    }

    /// <summary>
    ///     Sets the device to use the targets that were in use before this
    ///     scope was instantiated.
    /// </summary>
    public void Dispose()
    {
        graphicsDevice.SetRenderTargets(previous);
    }
}

/// <summary>
///     Extensions supporting <see cref="RenderTargetScope"/>.
/// </summary>
public static class RenderTargetScopeExtensions
{
    /// <summary>
    ///     Creates a new scope, saving the current device targets and starts
    ///     rendering to the new one instead.
    /// </summary>
    /// <param name="target">The target to render to.</param>
    /// <param name="preserveContents">
    ///     Whether to ensure swapped targets preserve their contents.
    /// </param>
    /// <param name="clearColor">
    ///     If not null, clears the target to the given color.
    /// </param>
    public static RenderTargetScope Scope(
        this RenderTarget2D target,
        bool preserveContents = true,
        Color? clearColor = null
    )
    {
        return new RenderTargetScope(
            target,
            preserveContents,
            clearColor
        );
    }

    /// <summary>
    ///     Creates a new scope, saving the current device targets and starts
    ///     rendering to the new one instead.
    /// </summary>
    /// <param name="target">The target to render to.</param>
    /// <param name="preserveContents">
    ///     Whether to ensure swapped targets preserve their contents.
    /// </param>
    /// <param name="clearColor">
    ///     If not null, clears the target to the given color.
    /// </param>
    public static RenderTargetScope Scope(
        this RenderTargetLease target,
        bool preserveContents = true,
        Color? clearColor = null
    )
    {
        return new RenderTargetScope(
            target.Target,
            preserveContents,
            clearColor
        );
    }
}

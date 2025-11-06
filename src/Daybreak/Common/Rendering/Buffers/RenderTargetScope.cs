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
    private readonly GraphicsDevice device;
    private readonly RenderTargetBinding[] previous;

    /// <summary>
    ///     Creates a new scope, saving the current device targets and starts
    ///     rendering to the new one instead.
    /// </summary>
    /// <param name="device">The device to manage targets for.</param>
    /// <param name="target">The target to render to.</param>
    /// <param name="preserveContents">
    ///     Whether to ensure swapped targets preserve their contents.
    /// </param>
    /// <param name="clear">Whether to clear the target.</param>
    /// <param name="clearColor">The color to clear the target to.</param>
    public RenderTargetScope(
        GraphicsDevice device,
        RenderTarget2D target,
        bool preserveContents = true,
        bool clear = false,
        Color? clearColor = null
    )
    {
        ArgumentNullException.ThrowIfNull(device);
        ArgumentNullException.ThrowIfNull(target);

        this.device = device;
        previous = device.GetRenderTargets();

        if (preserveContents)
        {
            RenderTargetPreserver.PreserveBindings(previous);
        }
        
        device.SetRenderTargets(target);

        if (clear)
        {
            device.Clear(clearColor ?? Color.Transparent);
        }
    }

    /// <summary>
    ///     Sets the device to use the targets that were in use before this
    ///     scope was instantiated.
    /// </summary>
    public void Dispose()
    {
        device.SetRenderTargets(previous);
    }
}

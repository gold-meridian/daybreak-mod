using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Rendering.Buffers;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/f33541642d1f2aec575b2a4f580afe13a2de2cfa/Common/Buffers.cs>.
 *
 * Generously licensed to us under AGPL v3.0.
 */

// TODO: Support binding multiple targets, including cubes?
//       Multiple targets would require allocations generally.  Graphics
//       extensions exist in Daybreak.Rendering to avoid it on the FNA side, but
//       scopes would still need to track them.  Provide scoped and unscoped
//       overloads?

/// <summary>
///     Manages the scope of a render target to be rendered to, swapping out the
///     currently used targets of a device on creation and replacing it with the
///     given target.  Switches back to the old targets upon disposal.
/// </summary>
public readonly struct RenderTargetScope : IDisposable
{
    private static GraphicsDevice GraphicsDevice => Graphics.Device;

    private readonly RenderTargetBinding[] previous;
    private readonly RenderTargetUsage? oldUsage;

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

        // PERF: If we're going to be clearing it anyway, no reason to permit
        // the default discard behavior!
        // The default overload for Clear mirrors the settings explicitly used
        // in SetRenderTargets.
        preserveContents |= clearColor.HasValue;

        previous = GraphicsDevice.GetRenderTargets();

        // If you're coming here from BufferPreserver, you'll see we can just
        // inline out logic here without issue.  RenderTargetUsage is only
        // acknowledged by SetRenderTarget, so we just need our logic to happen
        // before it.
        if (preserveContents)
        {
            if (previous.Length > 0)
            {
                // Whether targets are cleared is entirely dependent on the
                // first target.

                Debug.Assert(previous[0].RenderTarget is IRenderTarget);

                oldUsage = ((IRenderTarget)previous[0].RenderTarget).RenderTargetUsage;
            }
            else
            {
                // In the case of the backbuffer.

                oldUsage = GraphicsDevice.PresentationParameters.RenderTargetUsage;
            }

            // Debug.Assert(oldUsage.HasValue);
        }

        GraphicsDevice.SetRenderTargets(target);

        if (clearColor.HasValue)
        {
            GraphicsDevice.Clear(clearColor.Value);
        }
    }

    /// <summary>
    ///     Sets the device to use the targets that were in use before this
    ///     scope was instantiated.
    /// </summary>
    public void Dispose()
    {
        GraphicsDevice.SetRenderTargets(previous);

        // Restore target usage now that we've run SetRenderTargets a second
        // time.
        if (oldUsage is not { } usage)
        {
            return;
        }

        if (previous.Length > 0)
        {
            switch (previous[0].RenderTarget)
            {
                case RenderTarget2D target2D:
                    target2D.RenderTargetUsage = usage;
                    break;

                case RenderTargetCube targetCube:
                    targetCube.RenderTargetUsage = usage;
                    break;

                default:
                    throw new InvalidOperationException($"Unknown render target type: {previous[0].RenderTarget.GetType()}");
            }
        }
        else
        {
            GraphicsDevice.PresentationParameters.RenderTargetUsage = usage;
        }
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

    /// <inheritdoc cref="Scope(RenderTarget2D, bool, Color?)"/>
    public static RenderTargetScope Scope(
        this IBufferLease<RenderTarget2D> target,
        bool preserveContents = true,
        Color? clearColor = null
    )
    {
        return new RenderTargetScope(
            target.Buffer,
            preserveContents,
            clearColor
        );
    }
}

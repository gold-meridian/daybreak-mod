using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Daybreak.Rendering;

/// <summary>
///     Utilities for FNA graphics.
/// </summary>
public static class Graphics
{
    /// <summary>
    ///     The <see cref="Device"/> of Terraria.
    /// </summary>
    public static GraphicsDevice Device => Main.graphics.GraphicsDevice;

    // As per XNA.  FNA mirrors this.
    private const int max_render_target_bindings = 4;
    private static readonly RenderTargetBinding[] bindings = new RenderTargetBinding[max_render_target_bindings];

    extension(GraphicsDevice device)
    {
        /// <inheritdoc cref="GraphicsDevice.SetRenderTargets"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetRenderTargets(ReadOnlySpan<RenderTarget2D> targets)
        {
            if (targets.Length == 0)
            {
                device.SetRenderTargets(null);
                return;
            }

            ArgumentOutOfRangeException.ThrowIfGreaterThan(targets.Length, max_render_target_bindings);

            var bindings = Graphics.bindings.AsSpan(0, targets.Length);
            {
                for (var i = 0; i < targets.Length; i++)
                {
                    bindings[i] = new RenderTargetBinding(targets[i]);
                }
            }

            device.SetRenderTargets(bindings);
        }

        /// <inheritdoc cref="GraphicsDevice.SetRenderTargets"/>
        public void SetRenderTargets(ReadOnlySpan<RenderTargetBinding> renderTargets)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(renderTargets.Length, max_render_target_bindings);

            // Duplicated from GraphicsDevice::SetRenderTargets.

            // Flush scissor state - using a rect outside of the viewport has been observed
            // causing errors in Metal on iOS (via SDLGPU), for example when scissoring was just
            // disabled and we're changing viewport size.
            FNA3D.FNA3D_ApplyRasterizerState(
                device.GLDevice,
                ref device.RasterizerState.state
            );

            // D3D11 requires our sampler state to be valid (i.e. not point to any of our new RTs)
            //  before we call SetRenderTargets. At this point FNA3D does not have a current copy
            //  of the managed sampler state, so we need to apply our current state now instead of
            //  before our next Clear or Draw operation.
            device.ApplySamplers();

            // Checking for redundant SetRenderTargets...
            if (renderTargets.IsEmpty && device.renderTargetCount == 0)
            {
                return;
            }

            if (!renderTargets.IsEmpty && renderTargets.Length == device.renderTargetCount)
            {
                var isRedundant = true;
                for (var i = 0; i < renderTargets.Length; i += 1)
                {
                    if (
                        renderTargets[i].RenderTarget == device.renderTargetBindings[i].RenderTarget &&
                        renderTargets[i].CubeMapFace == device.renderTargetBindings[i].CubeMapFace
                    )
                    {
                        continue;
                    }

                    isRedundant = false;
                    break;
                }

                if (isRedundant)
                {
                    return;
                }
            }

            int newWidth;
            int newHeight;
            RenderTargetUsage clearTarget;
            if (!renderTargets.IsEmpty)
            {
                FNA3D.FNA3D_SetRenderTargets(
                    device.GLDevice,
                    nint.Zero,
                    0,
                    nint.Zero,
                    DepthFormat.None,
                    (byte)(device.PresentationParameters.RenderTargetUsage != RenderTargetUsage.DiscardContents ? 1 : 0) /* lol c# */
                );

                // Set the viewport/scissor to the size of the backbuffer.
                newWidth = device.PresentationParameters.BackBufferWidth;
                newHeight = device.PresentationParameters.BackBufferHeight;
                clearTarget = device.PresentationParameters.RenderTargetUsage;

                // Resolve previous targets, if needed
                for (var i = 0; i < device.renderTargetCount; i += 1)
                {
                    FNA3D.FNA3D_ResolveTarget(device.GLDevice, ref device.nativeTargetBindings[i]);
                }

                Array.Clear(device.renderTargetBindings, 0, device.renderTargetBindings.Length);
                Array.Clear(device.nativeTargetBindings, 0, device.nativeTargetBindings.Length);
                device.renderTargetCount = 0;
            }
            else
            {
                var target = (IRenderTarget)renderTargets[0].RenderTarget;
                unsafe
                {
                    fixed (FNA3D.FNA3D_RenderTargetBinding* rt = &device.nativeTargetBindingsNext[0])
                    {
                        GraphicsDevice.PrepareRenderTargetBindings(rt, renderTargets);
                        FNA3D.FNA3D_SetRenderTargets(
                            device.GLDevice,
                            rt,
                            renderTargets.Length,
                            target.DepthStencilBuffer,
                            target.DepthStencilFormat,
                            (byte)(target.RenderTargetUsage != RenderTargetUsage.DiscardContents ? 1 : 0) /* lol c# */
                        );
                    }
                }

                // Set the viewport/scissor to the size of the first render target.
                newWidth = target.Width;
                newHeight = target.Height;
                clearTarget = target.RenderTargetUsage;

                // Resolve previous targets, if needed
                for (var i = 0; i < device.renderTargetCount; i += 1)
                {
                    // We only need to resolve if the target is no longer bound.
                    var stillBound = false;
                    for (var j = 0; j < renderTargets.Length; j += 1)
                    {
                        if (device.renderTargetBindings[i].RenderTarget != renderTargets[j].RenderTarget)
                        {
                            continue;
                        }

                        stillBound = true;
                        break;
                    }

                    if (stillBound)
                    {
                        continue;
                    }

                    FNA3D.FNA3D_ResolveTarget(device.GLDevice, ref device.nativeTargetBindings[i]);
                }

                Array.Clear(device.renderTargetBindings, 0, device.renderTargetBindings.Length);
                renderTargets.CopyTo(device.renderTargetBindings); // Array.Copy(renderTargets, device.renderTargetBindings, renderTargets.Length);
                Array.Clear(device.nativeTargetBindings, 0, device.nativeTargetBindings.Length);
                Array.Copy(device.nativeTargetBindingsNext, device.nativeTargetBindings, renderTargets.Length);
                device.renderTargetCount = renderTargets.Length;
            }

            // Apply new GL state, clear target if requested
            device.Viewport = new Viewport(0, 0, newWidth, newHeight);
            device.ScissorRectangle = new Rectangle(0, 0, newWidth, newHeight);
            if (clearTarget == RenderTargetUsage.DiscardContents)
            {
                device.Clear(
                    ClearOptions.Target | ClearOptions.DepthBuffer | ClearOptions.Stencil,
                    GraphicsDevice.DiscardColor,
                    device.Viewport.MaxDepth,
                    0
                );
            }
        }
    }

    extension(GraphicsDevice)
    {
        private static unsafe void PrepareRenderTargetBindings(
            FNA3D.FNA3D_RenderTargetBinding* b,
            ReadOnlySpan<RenderTargetBinding> bindings
        )
        {
            // Duplicated from GraphicsDevice::PrepareRenderTargetBindings.

            for (var i = 0; i < bindings.Length; i += 1, b += 1)
            {
                var texture = bindings[i].RenderTarget;
                var rt = (IRenderTarget)texture;

                if (texture is RenderTargetCube)
                {
                    b->type = 1;
                    b->data1 = rt.Width;
                    b->data2 = (int)bindings[i].CubeMapFace;
                }
                else
                {
                    b->type = 0;
                    b->data1 = rt.Width;
                    b->data2 = rt.Height;
                }

                b->levelCount = rt.LevelCount;
                b->multiSampleCount = rt.MultiSampleCount;
                b->texture = texture.texture;
                b->colorBuffer = rt.ColorBuffer;
            }
        }
    }
}

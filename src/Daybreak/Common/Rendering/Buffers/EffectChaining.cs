using System;
using System.Collections.Generic;
using System.Linq;
using Daybreak.Common.Assets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;

namespace Daybreak.Common.Rendering;

/* Credit to Verminoid Creature for the original implementation, based on:
 * <https://github.com/JasperDawg/Cataphract/blob/f33541642d1f2aec575b2a4f580afe13a2de2cfa/Common/Buffers.cs>.
 *
 * Generously leased to us under AGPL v3.0.
 */

/// <summary>
///     An effect in an effect chain, denoting how an image should be rendered
///     with a given <paramref name="Effect"/>.
/// </summary>
/// <param name="Effect">The effect, or <see langword="null"/> for default.</param>
/// <param name="Parameters">
///     The parameters to use to render the texture with the effect.
/// </param>
public readonly record struct EffectChainEntry(
    ShaderData Effect,
    SpriteBatchParameters Parameters
)
{
    // Really simple wrapper over shader data to accept an arbitrary Effect
    // object.
    private sealed class EffectShaderData(Effect effect) : ShaderData(
        DummyAssetProvider.From(effect),
        effect.CurrentTechnique.Passes.First().Name
    );

    /// <summary>
    ///     An effect in an effect chain, denoting how an image should be
    ///     rendered with a given <paramref name="effect"/>.
    /// </summary>
    /// <param name="effect">The effect, or <see langword="null"/> for default.</param>
    /// <param name="parameters">
    ///     The parameters to use to render the texture with the effect.
    /// </param>
    public EffectChainEntry(
        Effect effect,
        SpriteBatchParameters parameters
    ) : this(
        new EffectShaderData(effect),
        parameters
    ) { }
}

/// <summary>
///     A scope which captures rendered data and applies multiple effects to it
///     as its output.
/// </summary>
public sealed class DrawWithEffectsScope : IDisposable
{
    private static readonly SpriteBatchSnapshot default_target_snapshot = new(
        SpriteSortMode.Deferred,
        BlendState.AlphaBlend,
        SamplerState.LinearClamp,
        DepthStencilState.None,
        RasterizerState.CullCounterClockwise,
        null,
        Matrix.Identity
    );

    private static readonly SpriteBatchSnapshot effect_target_snapshot = new(
        SpriteSortMode.Immediate,
        BlendState.AlphaBlend,
        SamplerState.LinearClamp,
        DepthStencilState.None,
        RasterizerState.CullCounterClockwise,
        null,
        Matrix.Identity
    );

    private readonly SpriteBatch spriteBatch;
    private readonly GraphicsDevice graphicsDevice;
    private readonly RenderTargetPool pool;
    private readonly IEnumerable<EffectChainEntry> effects;

    private readonly RenderTargetDescriptor renderDesc;
    private readonly int width;
    private readonly int height;
    private readonly SpriteBatchScope sbScope;
    private readonly RenderTargetScope rtScope;

    /// <summary>
    ///     The final lease containing the rendered data as the result of all
    ///     applied effects.
    /// </summary>
    public RenderTargetLease Lease { get; private set; }

    /// <summary>
    ///     Initializes a scope to handle rendering with multiple effects.
    ///     <br />
    ///     After creating the scope, proceed to handle rendering how you'd like
    ///     and then dispose of the object to render the final state to multiple
    ///     effects.
    /// </summary>
    /// <param name="spriteBatch">
    ///     The <see cref="SpriteBatch"/> to render with.
    /// </param>
    /// <param name="graphicsDevice">The device to create targets from.</param>
    /// <param name="pool">
    ///     The <see cref="RenderTargetPool"/> to retrieve targets from.
    /// </param>
    /// <param name="targetSize">The size of the target to render to.</param>
    /// <param name="preserveContents">
    ///     Whether to preserve contents of existing targets before swapping to
    ///     the initial new target.
    /// </param>
    /// <param name="clear">
    ///     Whether to clear the initial target before use.
    /// </param>
    /// <param name="clearColor">
    ///     What color to clear the initial target to before use.
    /// </param>
    /// <param name="descriptor">
    ///     The descriptor of the initial target.
    /// </param>
    /// <param name="parameters">
    ///     The parameters to initialize the <see cref="SpriteBatch"/> to.
    /// </param>
    /// <param name="effects">The effects to render with.</param>
    public DrawWithEffectsScope(
        SpriteBatch spriteBatch,
        GraphicsDevice graphicsDevice,
        RenderTargetPool pool,
        Point targetSize,
        bool preserveContents = true,
        bool clear = true,
        Color? clearColor = null,
        RenderTargetDescriptor? descriptor = null,
        SpriteBatchParameters parameters = default,
        params IEnumerable<EffectChainEntry> effects
    )
    {
        ArgumentNullException.ThrowIfNull(spriteBatch);
        ArgumentNullException.ThrowIfNull(graphicsDevice);
        ArgumentNullException.ThrowIfNull(pool);

        ArgumentOutOfRangeException.ThrowIfLessThan(targetSize.X, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(targetSize.Y, 1);

        this.spriteBatch = spriteBatch;
        this.graphicsDevice = graphicsDevice;
        this.pool = pool;
        this.effects = effects;

        renderDesc = descriptor ?? RenderTargetDescriptor.Default;
        width = Math.Max(1, targetSize.X);
        height = Math.Max(1, targetSize.Y);

        Lease = pool.Rent(graphicsDevice, width, height, renderDesc);

        sbScope = new SpriteBatchScope(spriteBatch);

        rtScope = new RenderTargetScope(graphicsDevice, Lease.Target, preserveContents, clear, clearColor);
        sbScope.Begin(parameters.ToSnapshot(default_target_snapshot));
    }

    /// <summary>
    ///     Ends the rendering context, renders the data to targets with the
    ///     given effects applied, and prepares the leased target as the output
    ///     data.
    /// </summary>
    public void Dispose()
    {
        spriteBatch.End();
        rtScope.Dispose();

        foreach (var effect in effects)
        {
            var nextLease = pool.Rent(graphicsDevice, width, height, renderDesc);
            using (new RenderTargetScope(graphicsDevice, nextLease.Target, true, true, Color.Transparent))
            {
                spriteBatch.Begin(effect.Parameters.ToSnapshot(effect_target_snapshot));
                effect.Effect.Apply();
                
                spriteBatch.Draw(Lease.Target, Vector2.Zero, Color.White);
                
                spriteBatch.End();
            }
            
            Lease.Dispose();
            Lease = nextLease;
        }

        sbScope.Dispose();
    }
}

/// <summary>
///     Extensions to types for chaining effects.
/// </summary>
public static class EffectChainingExtensions
{
    /// <summary>
    ///     Initializes a <see cref="DrawWithEffectsScope"/> with the given
    ///     parameters and returns it.
    ///     <br />
    ///     See <see cref="DrawWithEffectsScope"/> for information.
    /// </summary>
    /// <returns>
    ///     The scope, which should be disposed of to finalize rendering.
    /// </returns>
    public static DrawWithEffectsScope DrawWithEffects(
        this SpriteBatch spriteBatch,
        GraphicsDevice graphicsDevice,
        RenderTargetPool pool,
        Point targetSize,
        bool clear = true,
        Color? clearColor = null,
        RenderTargetDescriptor? descriptor = null,
        SpriteBatchParameters parameters = default,
        params EffectChainEntry[] effects
    )
    {
        return new DrawWithEffectsScope(
            spriteBatch,
            graphicsDevice,
            pool,
            targetSize,
            clear: clear,
            clearColor: clearColor,
            descriptor: descriptor,
            parameters: parameters,
            effects: effects
        );
    }
}

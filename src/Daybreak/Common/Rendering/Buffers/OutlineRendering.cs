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
///     Outline directions kinds for <see cref="DrawOutlinedScope"/>.
/// </summary>
public enum OutlineDirections
{
    /// <summary>
    ///     The four cardinal directions.
    /// </summary>
    Four,

    /// <summary>
    ///     The four cardinal directions and their four diagonals.
    /// </summary>
    Eight,
}

/// <summary>
///     A scope which captures rendered data and applies an outline to it as its
///     output.
/// </summary>
public sealed class DrawOutlinedScope : IDisposable
{
    private static readonly Point[] eight_directions =
    [
        new(-1, -1),
        new(0, -1),
        new(1, -1),
        new(-1, 0),
        new(1, 0),
        new(-1, 1),
        new(0, 1),
        new(1, 1),
    ];

    private static readonly Point[] four_directions =
    [
        new(0, -1),
        new(-1, 0),
        new(1, 0),
        new(0, 1),
    ];

    private static readonly SpriteBatchSnapshot default_snapshot = new(
        SpriteSortMode.Deferred,
        BlendState.AlphaBlend,
        SamplerState.LinearClamp,
        DepthStencilState.None,
        RasterizerState.CullCounterClockwise,
        null,
        Matrix.Identity
    );

    private readonly SpriteBatch spriteBatch;
    private readonly Point outputSize;
    private readonly Vector2 destination;
    private readonly Color outlineColor;
    private readonly int thickness;
    private readonly Color? fillTint;
    private readonly SpriteBatchParameters outlineParameters;
    private readonly SpriteBatchParameters targetParameters;
    private readonly OutlineDirections directions;

    private readonly RenderTargetLease contentLease;
    private readonly RenderTargetLease outlineLease;
    private readonly SpriteBatchScope sbScope;
    private readonly RenderTargetScope rtScope;

    /// <summary>
    ///     Begins capturing data to render with outlines.
    /// </summary>
    public DrawOutlinedScope(
        SpriteBatch spriteBatch,
        RenderTargetPool pool,
        Point outputSize,
        Vector2 destination,
        Color outlineColor,
        int thickness = 2,
        float contentScale = 1f,
        Color? fillTint = null,
        SpriteBatchParameters initParameters = default,
        SpriteBatchParameters outlineParameters = default,
        SpriteBatchParameters targetParameters = default,
        OutlineDirections directions = OutlineDirections.Four
    )
    {
        ArgumentNullException.ThrowIfNull(pool);
        ArgumentOutOfRangeException.ThrowIfLessThan(thickness, 1);

        this.spriteBatch = spriteBatch;
        this.outputSize = outputSize;
        this.destination = destination;
        this.outlineColor = outlineColor;
        this.thickness = thickness;
        this.fillTint = fillTint;
        this.outlineParameters = outlineParameters;
        this.targetParameters = targetParameters;
        this.directions = directions;

        contentLease = pool.RentScaled(spriteBatch.GraphicsDevice, outputSize, contentScale);
        outlineLease = pool.Rent(spriteBatch.GraphicsDevice, contentLease.Target.Width, contentLease.Target.Height);

        sbScope = spriteBatch.Scope();
        {
            rtScope = contentLease.Scope(clearColor: Color.Transparent);
            sbScope.Begin(initParameters.ToSnapshot(default_snapshot));
        }
    }

    /// <summary>
    ///     Finishes capturing render data and renders it with outlines.
    /// </summary>
    public void Dispose()
    {
        spriteBatch.End();

        var outlineDirections = directions switch
        {
            OutlineDirections.Four => four_directions,
            OutlineDirections.Eight => eight_directions,
            _ => throw new ArgumentOutOfRangeException(nameof(directions), directions, null),
        };

        using (outlineLease.Scope(clearColor: Color.Transparent))
        {
            spriteBatch.Begin(outlineParameters.ToSnapshot(default_snapshot));

            for (var i = 1; i <= thickness; i++)
            {
                foreach (var dir in outlineDirections)
                {
                    var offset = new Vector2(dir.X * i, dir.Y * i);
                    spriteBatch.Draw(contentLease.Target, offset, outlineColor);
                }
            }

            spriteBatch.End();
        }

        var scale = new Vector2(
            outputSize.X / (float)contentLease.Target.Width,
            outputSize.Y / (float)contentLease.Target.Height
        );

        spriteBatch.Begin(targetParameters.ToSnapshot(default_snapshot));
        spriteBatch.Draw(outlineLease.Target, destination, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        spriteBatch.Draw(contentLease.Target, destination, null, fillTint ?? Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        spriteBatch.End();

        rtScope.Dispose();
        sbScope.Dispose();
        outlineLease.Dispose();
        contentLease.Dispose();
    }
}

/// <summary>
///     Extensions to types for outline rendering.
/// </summary>
public static class OutlineRenderingExtensions
{
    /// <summary>
    ///     See <see cref="DrawOutlinedScope"/>.
    /// </summary>
    public static DrawOutlinedScope DrawOutlined(
        this SpriteBatch spriteBatch,
        RenderTargetPool pool,
        Point outputSize,
        Vector2 destination,
        Color outlineColor,
        int thickness = 2,
        float contentScale = 1f,
        Color? fillTint = null,
        SpriteBatchParameters initParameters = default,
        SpriteBatchParameters outlineParameters = default,
        SpriteBatchParameters targetParameters = default,
        OutlineDirections directions = OutlineDirections.Four
    )
    {
        return new DrawOutlinedScope(
            spriteBatch,
            pool,
            outputSize,
            destination,
            outlineColor,
            thickness,
            contentScale,
            fillTint,
            initParameters,
            outlineParameters,
            targetParameters,
            directions
        );
    }
}

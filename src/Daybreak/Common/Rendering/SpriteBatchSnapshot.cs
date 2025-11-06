using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Rendering;

/// <summary>
///     A "snapshot" of the current state of a <see cref="SpriteBatch" />.
///     <br />
///     These values may be manipulated freely.
/// </summary>
/// <remarks>
///     This API exists for making preservation of a <see cref="SpriteBatch" />'s
///     state trivial.
///     <br />
///     The act of taking a snapshot through this object's constructor is pure
///     (that is, it has no side effects).  It will not mutate the state of the
///     <see cref="SpriteBatch" /> being analyzed.  If you intend to modify the
///     <see cref="SpriteBatch" />, use the APIs provided in
///     <see cref="SpriteBatchSnapshotExtensions" />.
/// </remarks>
[PublicAPI]
public struct SpriteBatchSnapshot
{
    /// <summary>
    ///     The sort mode.
    /// </summary>
    public SpriteSortMode SortMode { get; set; }

    /// <summary>
    ///     The blend state.
    /// </summary>
    public BlendState BlendState { get; set; }

    /// <summary>
    ///     The sampler state.
    /// </summary>
    public SamplerState SamplerState { get; set; }

    /// <summary>
    ///     The depth stencil state.
    /// </summary>
    public DepthStencilState DepthStencilState { get; set; }

    /// <summary>
    ///     The rasterizer state.
    /// </summary>
    public RasterizerState RasterizerState { get; set; }

    /// <summary>
    ///     The custom effect, if applicable.
    /// </summary>
    public Effect? CustomEffect { get; set; }

    /// <summary>
    ///     The transformation matrix.
    /// </summary>
    public Matrix TransformMatrix { get; set; }

    /// <summary>
    ///     Creates a new <see cref="SpriteBatch"/> snapshot from raw
    ///     parameters.
    /// </summary>
    public SpriteBatchSnapshot(
        SpriteSortMode sortMode,
        BlendState blendState,
        SamplerState samplerState,
        DepthStencilState depthStencilState,
        RasterizerState rasterizerState,
        Effect? customEffect,
        Matrix transformMatrix
    )
    {
        SortMode = sortMode;
        BlendState = blendState;
        SamplerState = samplerState;
        DepthStencilState = depthStencilState;
        RasterizerState = rasterizerState;
        CustomEffect = customEffect;
        TransformMatrix = transformMatrix;
    }

    /// <summary>
    ///     Creates a new <see cref="SpriteBatch"/> snapshot from the current
    ///     settings of the given <see cref="SpriteBatch"/>.
    /// </summary>
    /// <param name="spriteBatch">
    ///     The <see cref="SpriteBatch" /> to take a snapshot of.
    /// </param>
    public SpriteBatchSnapshot(SpriteBatch spriteBatch)
    {
        SortMode = spriteBatch.sortMode;
        BlendState = spriteBatch.blendState;
        SamplerState = spriteBatch.samplerState;
        DepthStencilState = spriteBatch.depthStencilState;
        RasterizerState = spriteBatch.rasterizerState;
        CustomEffect = spriteBatch.customEffect;
        TransformMatrix = spriteBatch.transformMatrix;
    }

    /// <summary>
    ///     Initializes a set of parameters from this snapshot.
    /// </summary>
    public SpriteBatchParameters ToParameters()
    {
        return new SpriteBatchParameters(
            SortMode,
            BlendState,
            SamplerState,
            DepthStencilState,
            RasterizerState,
            CustomEffect,
            TransformMatrix
        );
    }
}

/// <summary>
///     Extensions to <see cref="SpriteBatch" /> using
///     <see cref="SpriteBatchSnapshot" /> instances.
/// </summary>
[PublicAPI]
public static class SpriteBatchSnapshotExtensions
{
    /// <summary>
    ///     Takes a snapshot of the <see cref="SpriteBatch" /> and then ends the
    ///     <see cref="SpriteBatch" />/
    /// </summary>
    /// <param name="this">The <see cref="SpriteBatch" />.</param>
    /// <param name="ss">The produced <see cref="SpriteBatchSnapshot" />.</param>
    public static void End(this SpriteBatch @this, out SpriteBatchSnapshot ss)
    {
        ss = new SpriteBatchSnapshot(@this);
        @this.End();
    }

    /// <summary>
    ///     Starts a <see cref="SpriteBatch" /> with the parameters from the
    ///     given <see cref="SpriteBatchSnapshot" />.
    /// </summary>
    /// <param name="this">The <see cref="SpriteBatch" />.</param>
    /// <param name="ss">The <see cref="SpriteBatchSnapshot" /> to use.</param>
    public static void Begin(this SpriteBatch @this, in SpriteBatchSnapshot ss)
    {
        @this.Begin(
            ss.SortMode,
            ss.BlendState,
            ss.SamplerState,
            ss.DepthStencilState,
            ss.RasterizerState,
            ss.CustomEffect,
            ss.TransformMatrix
        );
    }

    /// <summary>
    ///     Immediately ends and then starts the given <see cref="SpriteBatch" />
    ///     with the parameters from the given
    ///     <see cref="SpriteBatchSnapshot" />.
    /// </summary>
    /// <param name="this">The <see cref="SpriteBatch" />.</param>
    /// <param name="ss">The <see cref="SpriteBatchSnapshot" /> to use.</param>
    public static void Restart(this SpriteBatch @this, in SpriteBatchSnapshot ss)
    {
        @this.End();
        @this.Begin(ss);
    }
}

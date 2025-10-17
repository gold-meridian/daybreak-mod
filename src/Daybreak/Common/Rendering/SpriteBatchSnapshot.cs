using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Rendering;

/// <summary>
///     A "snapshot" of the current state of a <see cref="SpriteBatch" />.
///     <br />
///     These values may be manipulated freely.
/// </summary>
/// <param name="spriteBatch">
///     The <see cref="SpriteBatch" /> to take a snapshot of.
/// </param>
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
public struct SpriteBatchSnapshot(SpriteBatch spriteBatch)
{
    /// <summary>
    ///     The sort mode.
    /// </summary>
    public SpriteSortMode SortMode { get; set; } = spriteBatch.sortMode;

    /// <summary>
    ///     The blend state.
    /// </summary>
    public BlendState BlendState { get; set; } = spriteBatch.blendState;

    /// <summary>
    ///     The sampler state.
    /// </summary>
    public SamplerState SamplerState { get; set; } = spriteBatch.samplerState;

    /// <summary>
    ///     The depth stencil state.
    /// </summary>
    public DepthStencilState DepthStencilState { get; set; } = spriteBatch.depthStencilState;

    /// <summary>
    ///     The rasterizer state.
    /// </summary>
    public RasterizerState RasterizerState { get; set; } = spriteBatch.rasterizerState;

    /// <summary>
    ///     The custom effect, if applicable.
    /// </summary>
    public Effect? CustomEffect { get; set; } = spriteBatch.customEffect;

    /// <summary>
    ///     The transformation matrix.
    /// </summary>
    public Matrix TransformMatrix { get; set; } = spriteBatch.transformMatrix;
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

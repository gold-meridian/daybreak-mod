using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Rendering.V1;

/// <summary>
///     Wraps the parameters of a <see cref="SpriteBatch"/> <c>Begin</c> call
///     (&quot;initialization&quot;), permitting <see langword="null"/> values
///     which may be replaced with defaults when being converted to a snapshot.
///     <br />
///     This parameter collection represents an indefinite composition of the
///     <see cref="SpriteBatch"/> state (that is, a state with possibly
///     undefined values).  For a definite composition, see
///     <see cref="SpriteBatchSnapshot"/>.
/// </summary>
public struct SpriteBatchParameters
{
    /// <summary>
    ///     The sort mode.
    /// </summary>
    public SpriteSortMode? SortMode { get; set; }

    /// <summary>
    ///     The blend state.
    /// </summary>
    public BlendState? BlendState { get; set; }

    /// <summary>
    ///     The sampler state.
    /// </summary>
    public SamplerState? SamplerState { get; set; }

    /// <summary>
    ///     The depth stencil state.
    /// </summary>
    public DepthStencilState? DepthStencilState { get; set; }

    /// <summary>
    ///     The rasterizer state.
    /// </summary>
    public RasterizerState? RasterizerState { get; set; }

    /// <summary>
    ///     The custom effect, if applicable.
    /// </summary>
    public Effect? CustomEffect { get; set; }

    /// <summary>
    ///     The transformation matrix.
    /// </summary>
    public Matrix? TransformMatrix { get; set; }

    /// <summary>
    ///     Initializes a new set of parameters.
    /// </summary>
    public SpriteBatchParameters(
        SpriteSortMode? sortMode = null,
        BlendState? blendState = null,
        SamplerState? samplerState = null,
        DepthStencilState? depthStencilState = null,
        RasterizerState? rasterizerState = null,
        Effect? customEffect = null,
        Matrix? transformMatrix = null
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
    ///     Creates a new <see cref="SpriteBatchSnapshot"/>, with
    ///     <see langword="null"/> values being replaced with the values
    ///     provided by <paramref name="defaultValues"/>.
    /// </summary>
    /// <param name="defaultValues">
    ///     Default values to populate <see langword="null"/>s with.
    /// </param>
    /// <returns>The new snapshot.</returns>
    public readonly SpriteBatchSnapshot ToSnapshot(in SpriteBatchSnapshot defaultValues)
    {
        return new SpriteBatchSnapshot(
            SortMode ?? defaultValues.SortMode,
            BlendState ?? defaultValues.BlendState,
            SamplerState ?? defaultValues.SamplerState,
            DepthStencilState ?? defaultValues.DepthStencilState,
            RasterizerState ?? defaultValues.RasterizerState,
            CustomEffect ?? defaultValues.CustomEffect,
            TransformMatrix ?? defaultValues.TransformMatrix
        );
    }
}

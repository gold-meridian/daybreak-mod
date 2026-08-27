using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Daybreak.Rendering.Buffers.Entities.V1;

/// <summary>
///     Provides mechanics for dynamically modifying the texture of an item.
///     <br />
///     This is useful for applying animations or effects to items globally,
///     rather than per-item instance.
///     <br />
///     The resulting render is cacheable.
/// </summary>
public interface IBufferedItemTexture
{
    /// <summary>
    ///     Determines whether the cached texture should be re-rendered.
    /// </summary>
    /// <param name="itemType">The item type.</param>
    /// <param name="originalAsset">The original asset.</param>
    /// <param name="renderedTexture">The current cached texture.</param>
    bool ShouldRenderCachedTexture(
        int itemType,
        Asset<Texture2D> originalAsset,
        Texture2D? renderedTexture
    );

    /// <summary>
    ///     Renders the cached texture.
    /// </summary>
    /// <param name="spriteBatch">The <see cref="SpriteBatch"/>.</param>
    /// <param name="itemType">The item type.</param>
    /// <param name="originalAsset">The original asset.</param>
    void RenderCachedTexture(
        SpriteBatch spriteBatch,
        int itemType,
        Asset<Texture2D> originalAsset
    );
}

/// <summary>
///     Provides mechanisms for dynamically modifying the texture of an item.
///     <br />
///     This is useful for applying animations or effects to items globally,
///     rather than per-item instance.
///     <br />
///     This render will never be cached for later frames, prefer
///     <see cref="IBufferedItemTexture"/> where possible.
/// </summary>
[Obsolete("While this API is supported, IBufferedItemTexture is preferred")]
public interface IPreRenderedItem
{
    /// <summary>
    ///     Renders the item's texture for use in the current frame.
    /// </summary>
    /// <param name="sourceTexture">The actual texture of the item.</param>
    void PreRender(Texture2D sourceTexture);
}

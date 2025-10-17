using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Features.Rendering;

/// <summary>
///     Implementing this interface allows the item to pre-render a texture for
///     the frame which all item rendering will use instead of the original
///     texture.
/// </summary>
public interface IPreRenderedItem
{
    /// <summary>
    ///     Renders the item's texture for use for the current frame.
    /// </summary>
    /// <param name="sourceTexture">The actual texture of the item.</param>
    void PreRender(Texture2D sourceTexture);
}

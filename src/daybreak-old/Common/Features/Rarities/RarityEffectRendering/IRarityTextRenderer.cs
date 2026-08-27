using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;

namespace Daybreak.Common.Features.Rarities;

/// <summary>
///     A rarity which is responsible for rendering its own text in all
///     contexts.
/// </summary>
#pragma warning disable CS0618 // Type or member is obsolete
public interface IRarityTextRenderer
{
    /// <summary>
    ///     Renders the rarity text arbitrarily.  Invoked for all contexts in
    ///     which rarity text is rendered: popup text, item tooltips, and mouse
    ///     hovers.
    /// </summary>
    /// <param name="sb">The <see cref="SpriteBatch" /> used to render.</param>
    /// <param name="font">The font to use.</param>
    /// <param name="text">The text to render.</param>
    /// <param name="position">The position to draw to.</param>
    /// <param name="color">The color to draw with.</param>
    /// <param name="rotation">The rotation to use.</param>
    /// <param name="origin">The origin of the text.</param>
    /// <param name="scale">The scale of the text.</param>
    /// <param name="effects">Any sprite effects (uncommon).</param>
    /// <param name="maxWidth">The max width (uncommon).</param>
    /// <param name="spread">The spread (uncommon).</param>
    /// <param name="drawContext">The context of the render request.</param>
    void RenderText(
        SpriteBatch sb,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        RarityDrawContext drawContext,
        float maxWidth = -1f,
        float spread = 2f
    );
}
#pragma warning restore CS0618 // Type or member is obsolete

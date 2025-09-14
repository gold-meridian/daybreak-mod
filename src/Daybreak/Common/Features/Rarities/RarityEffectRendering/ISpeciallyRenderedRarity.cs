using System;

using Daybreak.Common.IDs;

using JetBrains.Annotations;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Graphics;

using Terraria.ModLoader;

namespace Daybreak.Common.Features.Rarities;

/// <summary>
///     Enables special rendering of this rarity in the tooltip and pickup text
///     displays.
///     <br />
///     Automatically applied when implemented by a <see cref="ModRarity"/>.  If
///     you need to apply it to a type not defined in your assembly or in a
///     weak-referenced scenario, see
///     <see cref="DaybreakRaritySets.SpecialRarity"/>.
/// </summary>
[PublicAPI]
[Obsolete($"Use {nameof(IRarityTextRenderer)}")]
public interface ISpeciallyRenderedRarity : IRarityTextRenderer
{
    /// <summary>
    ///     Renders the rarity text with special logic.  Invoked for both the
    ///     item's tooltip and the item's pickup text.
    /// </summary>
    /// <param name="sb">The <see cref="SpriteBatch"/> used to render.</param>
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
    /// <param name="ui">Whether this is rendering to the UI or in-world.</param>
    void RenderRarityText(
        SpriteBatch sb,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        SpriteEffects effects,
        float maxWidth,
        float spread,
        bool ui
    );

    void IRarityTextRenderer.RenderText(
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
        float maxWidth,
        float spread
    )
    {
        RenderRarityText(
            sb,
            font,
            text,
            position,
            color,
            rotation,
            origin,
            scale,
            effects,
            maxWidth,
            spread,
            drawContext.Ui
        );
    }
}
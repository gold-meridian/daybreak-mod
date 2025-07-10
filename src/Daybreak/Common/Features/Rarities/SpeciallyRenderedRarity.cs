using System;
using System.Reflection;

using Daybreak.Common.Features.Hooks;

using JetBrains.Annotations;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoMod.Cil;

using ReLogic.Graphics;

using Terraria;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.Rarities;

/// <summary>
///     Enables special rendering of this rarity in the tooltip and pickup text
///     displays.
/// </summary>
[PublicAPI]
public interface ISpeciallyRenderedRarity
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

    [OnLoad]
    private static void Load()
    {
        GlobalItemHooks.PreDrawTooltipLine.Event += RenderSpecialRaritiesInTooltips;
        IL_Main.DrawItemTextPopups += RenderSpecialRaritiesInPopupText;
        IL_Main.MouseTextInner += RenderSpecialRaritiesInMouseText;
    }

    private static bool RenderSpecialRaritiesInTooltips(GlobalItem self, Item item, DrawableTooltipLine line, ref int yOffset)
    {
        if (line is not { Mod: "Terraria", Name: "ItemName" })
        {
            return true;
        }

        if (RarityLoader.GetRarity(item.rare) is not ISpeciallyRenderedRarity rarity)
        {
            return true;
        }

        rarity.RenderRarityText(
            Main.spriteBatch,
            line.Font,
            line.Text,
            new Vector2(line.X, line.Y),
            line.Color,
            line.Rotation,
            line.Origin,
            line.BaseScale,
            SpriteEffects.None,
            line.MaxWidth,
            line.Spread,
            true
        );
        return false;
    }

    private static void RenderSpecialRaritiesInPopupText(ILContext il)
    {
        var c = new ILCursor(il);

        // Tomat: This IL edit is awesome.  Match into the second loop that
        // renders the text and its outline, modify it to only run the last
        // operation to render the colored text of the popup text's rarity
        // is of our specially-rendered kind, and override the relevant
        // draw operations to use our custom API instead.

        // Get popup text.
        var popupTextLoc = -1;
        c.GotoNext(x => x.MatchLdsfld<Main>(nameof(Main.popupText)));
        c.GotoNext(x => x.MatchStloc(out popupTextLoc));

        // Jump to the end of the method.
        c.Next = null;

        ILLabel? secondLoopBegin = null;
        c.GotoPrev(x => x.MatchBlt(out _)); // Skip outer loop.
        c.GotoPrev(x => x.MatchBlt(out secondLoopBegin)); // Find inner loop.
        if (secondLoopBegin is null)
        {
            // Shouldn't be possible to reach here, but oh well.
            throw new InvalidOperationException("Failed to find second loop begin");
        }

        // Jump to the start of the body of the loop (convenience).
        c.GotoLabel(secondLoopBegin);

        // Here we can modify the loop to run only once.
        c.GotoPrev(MoveType.After, x => x.MatchLdcI4(0));

        c.EmitPop();
        c.EmitLdloc(popupTextLoc);
        c.EmitDelegate((PopupText text) => RarityLoader.GetRarity(text.rarity) is ISpeciallyRenderedRarity ? 4 : 0);

        // Go to the end again (could also manually match DrawString three
        // times).
        c.Next = null;

        // Actually take control of rendering by intercepting the call.
        c.GotoPrev(MoveType.Before, x => x.MatchCall(typeof(DynamicSpriteFontExtensionMethods), nameof(DynamicSpriteFontExtensionMethods.DrawString)));
        c.Remove();
        c.EmitLdloc(popupTextLoc); // So we can check the rarity.
        c.EmitDelegate(
            (SpriteBatch spriteBatch, DynamicSpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, PopupText popupText) =>
            {
                if (RarityLoader.GetRarity(popupText.rarity) is not ISpeciallyRenderedRarity rarity)
                {
                    spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, effects, layerDepth);
                    return;
                }

                rarity.RenderRarityText(spriteBatch, font, text, position, color, rotation, origin, new Vector2(scale), effects, -1, 2, false);
            }
        );
    }

    private static void RenderSpecialRaritiesInMouseText(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.Before, x => x.MatchCall("Terraria.UI.Chat.ChatManager", "DrawColorCodedStringWithShadow"));
        c.Remove();

        c.EmitLdarg1(); // info
        c.EmitLdfld(typeof(Main.MouseTextCache).GetField(nameof(Main.MouseTextCache.rare), BindingFlags.Public | BindingFlags.Instance)!);
        c.EmitDelegate(
            (
                SpriteBatch spriteBatch,
                DynamicSpriteFont font,
                string text,
                Vector2 position,
                Color baseColor,
                float rotation,
                Vector2 origin,
                Vector2 baseScale,
                float maxWidth,
                float spread,
                int rare
            ) =>
            {
                if (RarityLoader.GetRarity(rare) is not ISpeciallyRenderedRarity rarity)
                {
                    ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, position, baseColor, rotation, origin, baseScale, maxWidth, spread);
                    return Vector2.Zero;
                }

                rarity.RenderRarityText(spriteBatch, font, text, position, baseColor, rotation, origin, baseScale, SpriteEffects.None, maxWidth, spread, false);
                return Vector2.Zero;
            }
        );
    }
}
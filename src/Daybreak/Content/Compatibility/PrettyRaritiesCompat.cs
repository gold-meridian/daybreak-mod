using System.Reflection;

using Daybreak.Common.Features.Rarities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using PrettyRarities.GlobalItems;

using ReLogic.Graphics;

using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Content.Compatibility;

/// <summary>
///     Rewrites &quot;Better Tooltips&quot; to integrate with our rarity
///     system.  Disables any of their edits and redirects their APIs to use
///     ours.
/// </summary>
[ExtendsFromMod("PrettyRarities")]
internal sealed class PrettyRaritiesCompat : ModSystem
{
    private readonly struct PrettyRaritiesSpecialRarity : IRarityTextRenderer
    {
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
        ) { }
    }

    public static Mod RarityMod => ModLoader.GetMod("PrettyRarities");

    public override void Load()
    {
        base.Load();

        MonoModHooks.Add(
            typeof(GlobalItem_Rarities).GetMethod(nameof(Load), BindingFlags.Public | BindingFlags.Instance),
            RarityModifierGlobalItem_Load_Disable
        );

        MonoModHooks.Add(
            typeof(GlobalItem_Rarities).GetMethod(nameof(GlobalItem.PreDrawTooltipLine), BindingFlags.Public | BindingFlags.Instance),
            RarityModifierGlobalItem_PreDrawTooltipLine_Disable
        );
    }

    private static void RarityModifierGlobalItem_Load_Disable(GlobalItem self) { }

    private static bool RarityModifierGlobalItem_PreDrawTooltipLine_Disable(GlobalItem self, Item item, DrawableTooltipLine line, ref int yOffset)
    {
        return true;
    }
}
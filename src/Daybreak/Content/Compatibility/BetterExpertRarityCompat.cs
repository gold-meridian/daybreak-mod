using System.Reflection;

using BetterExpertRarity.Common.Rarities;

using Daybreak.Common.Features.Rarities;
using Daybreak.Common.IDs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Content.Compatibility;

/// <summary>
///     Rewrites &quot;Better Expert Rarity&quot; to integrate with our rarity
///     system.  Disables any of their edits and redirects their APIs to use
///     ours.
/// </summary>
[ExtendsFromMod("BetterExpertRarity")]
internal sealed class BetterExpertRarityCompat : ModSystem
{
    private readonly struct BetterExpertRaritySpecialRarity(RarityModifier rarityMod) : ISpeciallyRenderedRarity
    {
        void ISpeciallyRenderedRarity.RenderRarityText(
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
        )
        {
            rarityMod.Draw(
                new RarityModifier.DrawData
                {
                    Text = text,
                    Position = position,
                    Color = color,
                    Rotation = rotation,
                    Origin = origin,
                    Scale = scale,
                    MaxWidth = maxWidth,
                    ShadowSpread = spread,
                }
            );
        }
    }

    public static Mod RarityMod => ModLoader.GetMod("BetterExpertRarity");

    public override void Load()
    {
        base.Load();

        MonoModHooks.Add(
            typeof(RarityModifierGlobalItem).GetMethod(nameof(Load), BindingFlags.Public | BindingFlags.Instance),
            RarityModifierGlobalItem_Load_Disable
        );

        MonoModHooks.Add(
            typeof(RarityModifierGlobalItem).GetMethod(nameof(GlobalItem.PreDrawTooltipLine), BindingFlags.Public | BindingFlags.Instance),
            RarityModifierGlobalItem_PreDrawTooltipLine_Disable
        );
    }

    public override void PostSetupContent()
    {
        base.PostSetupContent();

        foreach (var rarityMod in RarityModifierSystem.Modifiers)
        {
            DaybreakRaritySets.SpecialRarity[rarityMod.RarityType] = new BetterExpertRaritySpecialRarity(rarityMod);
        }
    }

    private static void RarityModifierGlobalItem_Load_Disable(GlobalItem self) { }

    private static bool RarityModifierGlobalItem_PreDrawTooltipLine_Disable(GlobalItem self, Item item, DrawableTooltipLine line, ref int yOffset)
    {
        return true;
    }
}
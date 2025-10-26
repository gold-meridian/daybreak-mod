using System;
using System.Reflection;
using Daybreak.Common.Features.Rarities;
using Daybreak.Common.IDs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrettyRarities.Core;
using PrettyRarities.GlobalItems;
using PrettyRarities.VanillaRarities;
using PrettyRarities.VanillaRarities.Modded;
using ReLogic.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using RarityDrawContext = Daybreak.Common.Features.Rarities.RarityDrawContext;

namespace Daybreak.Content.Compatibility;

/// <summary>
///     Rewrites &quot;Better Tooltips&quot; to integrate with our rarity
///     system.  Disables any of their edits and redirects their APIs to use
///     ours.
/// </summary>
[ExtendsFromMod("PrettyRarities")]
internal sealed class PrettyRaritiesCompat : ModSystem
{
    private readonly struct PrettyRaritiesSpecialRarity(
        Action<RarityDrawData, PrettyRarities.Core.RarityDrawContext> drawFunc
    ) : IRarityTextRenderer
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
        )
        {
            drawFunc(
                new RarityDrawData(
                    text,
                    position.X,
                    position.Y,
                    font,
                    rotation,
                    origin,
                    scale
                ),
                ToCompatContext(drawContext)
            );
        }

        private static PrettyRarities.Core.RarityDrawContext ToCompatContext(RarityDrawContext ctx)
        {
            return ctx.DrawKind switch
            {
                RarityDrawContext.Kind.ItemTooltip => PrettyRarities.Core.RarityDrawContext.Tooltip,
                RarityDrawContext.Kind.MouseText => PrettyRarities.Core.RarityDrawContext.MouseHover,
                RarityDrawContext.Kind.PopupText => PrettyRarities.Core.RarityDrawContext.PopupText,
                _ => PrettyRarities.Core.RarityDrawContext.Tooltip,
            };
        }
    }

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

    public override void PostSetupContent()
    {
        base.PostSetupContent();

        DaybreakRaritySets.SpecialRarity[ItemRarityID.Master] = new PrettyRaritiesSpecialRarity(FieryRed.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Expert] = new PrettyRaritiesSpecialRarity(Rainbow.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Quest] = new PrettyRaritiesSpecialRarity(Amber.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Gray] = new PrettyRaritiesSpecialRarity(Gray.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.White] = new PrettyRaritiesSpecialRarity(White.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Blue] = new PrettyRaritiesSpecialRarity(Blue.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Green] = new PrettyRaritiesSpecialRarity(Green.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Orange] = new PrettyRaritiesSpecialRarity(Orange.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.LightRed] = new PrettyRaritiesSpecialRarity(LightRed.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Pink] = new PrettyRaritiesSpecialRarity(Pink.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.LightPurple] = new PrettyRaritiesSpecialRarity(LightPurple.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Lime] = new PrettyRaritiesSpecialRarity(Lime.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Yellow] = new PrettyRaritiesSpecialRarity(Yellow.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Cyan] = new PrettyRaritiesSpecialRarity(Cyan.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Red] = new PrettyRaritiesSpecialRarity(Red.DrawTooltipLine);
        DaybreakRaritySets.SpecialRarity[ItemRarityID.Purple] = new PrettyRaritiesSpecialRarity(Purple.DrawTooltipLine);

        TryAddModRarity("CalamityMod", "Turquoise", new PrettyRaritiesSpecialRarity(Turquoise.DrawTooltipLine));
        TryAddModRarity("CalamityMod", "PureGreen", new PrettyRaritiesSpecialRarity(PureGreen.DrawTooltipLine));
        TryAddModRarity("CalamityMod", "DarkBlue", new PrettyRaritiesSpecialRarity(DarkBlue.DrawTooltipLine));
        TryAddModRarity("CalamityMod", "Violet", new PrettyRaritiesSpecialRarity(Violet.DrawTooltipLine));
        TryAddModRarity("CalamityMod", "HotPink", new PrettyRaritiesSpecialRarity(HotPink.DrawTooltipLine));
        TryAddModRarity("CalamityMod", "CalamityRed", new PrettyRaritiesSpecialRarity(CalamityRed.DrawTooltipLine));
        TryAddModRarity("CalamityMod", "DarkOrange", new PrettyRaritiesSpecialRarity(DarkOrange.DrawTooltipLine));
        TryAddModRarity("CalamityMod", "Rainbow", new PrettyRaritiesSpecialRarity(Rainbow.DrawTooltipLine));

        TryAddModRarity("ThoriumMod", "BloodOrangeRarity", new PrettyRaritiesSpecialRarity(BloodOrange.DrawTooltipLine));
        TryAddModRarity("ThoriumMod", "DonatorRarity", new PrettyRaritiesSpecialRarity(Teal.DrawTooltipLine));

        return;

        static void TryAddModRarity(string modName, string rarityName, PrettyRaritiesSpecialRarity rarity)
        {
            if (!ModLoader.TryGetMod(modName, out var mod))
            {
                return;
            }

            if (!mod.TryFind<ModRarity>(rarityName, out var modRarity))
            {
                return;
            }

            DaybreakRaritySets.SpecialRarity[modRarity.Type] = rarity;
        }
    }

    private static void RarityModifierGlobalItem_Load_Disable(GlobalItem self) { }

    private static bool RarityModifierGlobalItem_PreDrawTooltipLine_Disable(GlobalItem self, Item item, DrawableTooltipLine line, ref int yOffset)
    {
        return true;
    }
}

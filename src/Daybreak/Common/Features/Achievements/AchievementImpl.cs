using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

using Daybreak.Common.Features.Hooks;
using Daybreak.Content.Config;
using Daybreak.Content.UI;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoMod.Cil;

using Newtonsoft.Json;

using ReLogic.Content;

using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Chat;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.Achievements;

/// <summary>
///     Implementation for achievements.
/// </summary>
internal sealed class AchievementImpl : ModSystem
{
#region Re-impls
    private sealed class CompatibleAchievementSnippet : TextSnippet
    {
        private Achievement achievement;

        public CompatibleAchievementSnippet(Achievement achievement) : base(achievement.DisplayName.Value, Color.LightBlue)
        {
            CheckForHover = true;
            this.achievement = achievement;
        }

        public override void OnClick()
        {
            base.OnClick();

            IngameOptions.Close();
            OpenAchievementsAndGoto(achievement);
        }
    }

    private sealed class CompatibleAchievementUnlockedPopup : IInGameNotification
    {
        public bool ShouldBeRemoved { get; private set; }

        private float Scale
        {
            get
            {
                if (timeLeft < 30)
                {
                    return MathHelper.Lerp(0f, 1f, timeLeft / 30f);
                }

                if (timeLeft > 285)
                {
                    return MathHelper.Lerp(1f, 0f, (timeLeft - 285f) / 15f);
                }

                return 1f;
            }
        }

        private float Opacity
        {
            get
            {
                var scale = Scale;
                if (scale <= 0.5f)
                {
                    return 0f;
                }

                return (scale - 0.5f) / 0.5f;
            }
        }

        private int timeLeft;

        private readonly Achievement achievement;
        private readonly string title;
        private readonly Rectangle frame;
        private readonly Asset<Texture2D> texture;

        private static readonly Asset<Texture2D> border_texture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders");

        public CompatibleAchievementUnlockedPopup(Achievement achievement)
        {
            this.achievement = achievement;
            timeLeft = 300;
            title = achievement.DisplayName.Value;
            texture = achievement.GetIcon(out frame, out _);
        }

        public void Update()
        {
            timeLeft--;

            if (timeLeft < 0)
            {
                timeLeft = 0;
            }
        }

        public void DrawInGame(SpriteBatch spriteBatch, Vector2 bottomAnchorPosition)
        {
            var opacity = Opacity;
            if (opacity <= 0f)
            {
                return;
            }

            var textScale = Scale * 1.1f;
            var textSize = (FontAssets.ItemStack.Value.MeasureString(title) + new Vector2(58f, 10f)) * textScale;
            var panelBackground = Utils.CenteredRectangle(bottomAnchorPosition + new Vector2(0f, (0f - textSize.Y) * 0.5f), textSize);
            var mouseOver = panelBackground.Contains(Main.MouseScreen.ToPoint());
            Utils.DrawInvBG(c: mouseOver ? new Color(64, 109, 164) * 0.75f : new Color(64, 109, 164) * 0.5f, sb: spriteBatch, R: panelBackground);
            var iconScale = textScale * 0.3f;
            var drawPos = panelBackground.Right() - Vector2.UnitX * textScale * (12f + iconScale * frame.Width);
            spriteBatch.Draw(texture.Value, drawPos, frame, Color.White * opacity, 0f, new Vector2(0f, frame.Height / 2f), iconScale, SpriteEffects.None, 0f);
            spriteBatch.Draw(border_texture.Value, drawPos, null, Color.White * opacity, 0f, new Vector2(0f, frame.Height / 2f), iconScale, SpriteEffects.None, 0f);
            Utils.DrawBorderString(
                color: new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor / 5, Main.mouseTextColor) * opacity,
                sb: spriteBatch,
                text: title,
                pos: drawPos - Vector2.UnitX * 10f,
                scale: textScale * 0.9f,
                anchorx: 1f,
                anchory: 0.4f
            );

            if (mouseOver)
            {
                OnMouseOver();
            }
        }

        public void PushAnchor(ref Vector2 positionAnchorBottom)
        {
            positionAnchorBottom.Y -= 50f * Opacity;
        }

        private void OnMouseOver()
        {
            if (PlayerInput.IgnoreMouseInterface)
            {
                return;
            }

            Main.LocalPlayer.mouseInterface = true;
            if (!Main.mouseLeft || !Main.mouseLeftRelease)
            {
                return;
            }

            Main.mouseLeftRelease = false;
            OpenAchievementsAndGoto(achievement);
            timeLeft = 0;
            ShouldBeRemoved = true;
        }
    }

    private static class CustomAchievementAdvisor
    {
        private static readonly Asset<Texture2D> achievements_border_texture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders");
        private static readonly Asset<Texture2D> achievements_border_mouse_hover_fat_texture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders_MouseHover");
        private static readonly Asset<Texture2D> achievements_border_mouse_hover_thin_texture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders_MouseHoverThin");

        internal static Achievement[] Cards = [];

        private static Achievement? hoveredCard;

        [SubscribesTo<ModSystemHooks.PostSetupContent>]
        public static void InitializeAchievementAdvisorHooks(
            ModSystemHooks.PostSetupContent.Original orig,
            ModSystem self
        )
        {
            if (!AchievementConfig.AreAchievementsEnabled())
            {
                return;
            }

            On_AchievementAdvisor.DrawOneAchievement += (_, _, batch, position, large) =>
            {
                DrawOneAchievement(batch, position, large);
            };

            On_AchievementAdvisor.Update += (_, _) =>
            {
                Update();
            };

            On_AchievementAdvisor.DrawOptionsPanel += (_, _, batch, position, rightPosition) =>
            {
                DrawOptionsPanel(batch, position, rightPosition);
            };

            On_AchievementAdvisor.DrawMouseHover += (_, _) =>
            {
                DrawMouseHover();
            };

            // Trigger a re-JIT of this method to ensure Update runs.
            IL_Main.DoUpdate += _ => { };
        }

        private static void DrawOneAchievement(SpriteBatch spriteBatch, Vector2 position, bool large)
        {
            var bestCard = GetBestCards(cardsAmount: 1).FirstOrDefault();
            if (bestCard is null)
            {
                return;
            }

            hoveredCard = null;

            var scale = large ? 0.75f : 0.35f;
            DrawCard(bestCard, spriteBatch, position + new Vector2(8f) * scale, scale, out var hovered);

            if (!hovered)
            {
                return;
            }

            hoveredCard = bestCard;

            if (PlayerInput.IgnoreMouseInterface)
            {
                return;
            }

            Main.LocalPlayer.mouseInterface = true;

            if (Main.mouseLeft && Main.mouseLeftRelease)
            {
                Main.ingameOptionsWindow = false;
                OpenAchievementsAndGoto(hoveredCard);
            }
        }

        private static void Update()
        {
            hoveredCard = null;
        }

        private static void DrawOptionsPanel(SpriteBatch spriteBatch, Vector2 leftPosition, Vector2 rightPosition)
        {
            var bestCards = GetBestCards().ToArray();
            hoveredCard = null;

            var numCardsToConsider = bestCards.Length;
            if (numCardsToConsider > 5)
            {
                numCardsToConsider = 5;
            }

            bool hovered;
            for (var i = 0; i < numCardsToConsider; i++)
            {
                DrawCard(bestCards[i], spriteBatch, leftPosition + new Vector2(42 * i, 0f), 0.5f, out hovered);
                if (hovered)
                {
                    hoveredCard = bestCards[i];
                }
            }

            for (var i = 5; i < bestCards.Length; i++)
            {
                DrawCard(bestCards[i], spriteBatch, rightPosition + new Vector2(42 * i, 0f), 0.5f, out hovered);
                if (hovered)
                {
                    hoveredCard = bestCards[i];
                }
            }

            if (hoveredCard is null)
            {
                return;
            }

            if (hoveredCard.IsCompleted)
            {
                hoveredCard = null;
            }
            else if (!PlayerInput.IgnoreMouseInterface)
            {
                Main.LocalPlayer.mouseInterface = true;
                if (!Main.mouseLeft || !Main.mouseLeftRelease)
                {
                    return;
                }

                Main.ingameOptionsWindow = false;
                OpenAchievementsAndGoto(hoveredCard);
            }
        }

        private static void DrawMouseHover()
        {
            if (hoveredCard is null)
            {
                return;
            }

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);

            PlayerInput.SetZoom_UI();

            var item = new Item();
            {
                item.SetDefaults(0, noMatCheck: true);
                item.SetNameOverride(hoveredCard.DisplayName.Value);
                item.ToolTip = ItemTooltip.FromLanguageKey(hoveredCard.Description.Key);
                item.type = ItemID.IronPickaxe;
                item.scale = 0f;
                item.rare = ItemRarityID.Red;
                item.value = -1;
            }

            Main.HoverItem = item;
            Main.instance.MouseText("");
            Main.mouseText = true;
        }

        private static void DrawCard(Achievement card, SpriteBatch spriteBatch, Vector2 position, float scale, out bool hovered)
        {
            hovered = false;

            var texture = card.GetIcon(out var cardFrame, out var lockedOffset);

            if (Main.MouseScreen.Between(position, position + cardFrame.Size() * scale))
            {
                Main.LocalPlayer.mouseInterface = true;
                hovered = true;
            }

            var color = Color.White;
            if (!hovered)
            {
                color = new Color(220, 220, 220, 220);
            }

            var vector = new Vector2(-4f) * scale;
            var vector2 = new Vector2(-8f) * scale;
            var value = achievements_border_mouse_hover_fat_texture.Value;
            if (scale > 0.5f)
            {
                value = achievements_border_mouse_hover_thin_texture.Value;
                vector2 = new Vector2(-5f) * scale;
            }

            var frame = cardFrame;
            frame.X += lockedOffset;
            spriteBatch.Draw(texture.Value, position, frame, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(achievements_border_texture.Value, position + vector, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            if (hovered)
            {
                spriteBatch.Draw(value, position + vector2, null, Main.OurFavoriteColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        private static IEnumerable<Achievement> GetBestCards(int cardsAmount = 10)
        {
            var added = 0;
            foreach (var card in Cards)
            {
                if (card.IsCompleted || !card.IsPresentlyAvailable())
                {
                    continue;
                }

                yield return card;
                added++;

                if (added >= cardsAmount)
                {
                    break;
                }
            }
        }
    }
#endregion

    public static readonly List<Achievement> ACHIEVEMENTS = [];
    public static readonly List<AchievementCategory> CATEGORIES = [];

    private static readonly HashSet<string> known_completed_achievements = [];

    private static string SavePath => Path.Combine(Main.SavePath, "daybreak", "achievements.json");

    public override void Load()
    {
        base.Load();

        ReadCompletedAchievements();
    }

    public override void PostSetupContent()
    {
        base.PostSetupContent();

        if (!AchievementConfig.AreAchievementsEnabled())
        {
            return;
        }

        CustomAchievementAdvisor.Cards = ACHIEVEMENTS.Where(x => x.AdvisorOrder >= 0f).OrderBy(x => x.AdvisorOrder).ToArray();

        On_AchievementTagHandler.Terraria_UI_Chat_ITagHandler_Parse += UseCompatibleTextSnippetForAchievementTag;
        On_InGameNotificationsTracker.AddCompleted += AddModdedAchievementsAsCompletedInPlaceOfVanilla;
        On_IngameFancyUI.OpenAchievements += OpenOurAchievementsMenu;
        On_IngameFancyUI.OpenAchievementsAndGoto += OpenAchievementsAndGotoOurMenu;

        IL_Main.DrawMenu += IL_ReplaceAchievementsMenuReference;
        IL_Main.CanPauseGame += IL_ReplaceAchievementsMenuReference;
    }

    private static void IL_ReplaceAchievementsMenuReference(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.After, x => x.MatchLdsfld<Main>("AchievementsMenu"));

        c.EmitPop();
        c.EmitDelegate(ModContent.GetInstance<AchievementsMenu>);
    }

    private static TextSnippet UseCompatibleTextSnippetForAchievementTag(
        On_AchievementTagHandler.orig_Terraria_UI_Chat_ITagHandler_Parse orig,
        AchievementTagHandler self,
        string text,
        Color baseColor,
        string options
    )
    {
        if (!TrySplitName(text, out var modName, out var achName))
        {
            return VanillaAchievements.VANILLA_ACHIEVEMENTS_BY_NAME.TryGetValue(text, out var vanillaAch)
                ? new CompatibleAchievementSnippet(vanillaAch)
                : new TextSnippet(text);
        }

        if (!ModLoader.TryGetMod(modName, out var mod))
        {
            return new TextSnippet(text);
        }

        if (!mod.TryFind<Achievement>(achName, out var ach))
        {
            return new TextSnippet(text);
        }

        return new CompatibleAchievementSnippet(ach);

        static bool TrySplitName(
            string name,
            [NotNullWhen(returnValue: true)] out string? domain,
            [NotNullWhen(returnValue: true)] out string? subName
        )
        {
            var length = name.IndexOfAny(ModContent.nameSplitters);
            if (length < 0)
            {
                domain = null;
                subName = null;
                return false;
            }

            domain = name[..length];
            subName = name[(length + 1)..];
            return true;
        }
    }

    private static void AddModdedAchievementsAsCompletedInPlaceOfVanilla(
        On_InGameNotificationsTracker.orig_AddCompleted orig,
        Terraria.Achievements.Achievement achievement
    )
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        if (!VanillaAchievements.VANILLA_ACHIEVEMENTS_BY_NAME.TryGetValue(achievement.Name, out var ach))
        {
            return;
        }

        InGameNotificationsTracker.AddNotification(new CompatibleAchievementUnlockedPopup(ach));
    }

    private static void OpenOurAchievementsMenu(On_IngameFancyUI.orig_OpenAchievements orig)
    {
        IngameFancyUI.CoverNextFrame();
        Main.playerInventory = false;
        Main.editChest = false;
        Main.npcChatText = "";
        Main.inFancyUI = true;
        IngameFancyUI.ClearChat();
        Main.InGameUI.SetState(ModContent.GetInstance<AchievementsMenu>());
    }

    private static void OpenAchievementsAndGotoOurMenu(On_IngameFancyUI.orig_OpenAchievementsAndGoto orig, Terraria.Achievements.Achievement achievement)
    {
        if (!VanillaAchievements.VANILLA_ACHIEVEMENTS_BY_NAME.TryGetValue(achievement.Name, out var ach))
        {
            return;
        }

        IngameFancyUI.OpenAchievements();
        ModContent.GetInstance<AchievementsMenu>().GotoAchievement(ach);
    }

    private static void OpenAchievementsAndGoto(Achievement achievement)
    {
        IngameFancyUI.OpenAchievements();
        ModContent.GetInstance<AchievementsMenu>().GotoAchievement(achievement);
    }

    public static void Register(Achievement achievement)
    {
        achievement.Id = ACHIEVEMENTS.Count;
        ACHIEVEMENTS.Add(achievement);
    }

    public static void RegisterCategory(AchievementCategory category)
    {
        category.Id = CATEGORIES.Count;
        CATEGORIES.Add(category);
    }

    public static bool GetCompletedStatus(Achievement achievement)
    {
        ModContent.SplitName(achievement.FullName, out var modName, out var achievementName);

        if (modName is "Daybreak" or "Terraria" && VanillaAchievements.VANILLA_ACHIEVEMENTS_BY_NAME.ContainsKey(achievementName) && Main.Achievements.GetAchievement(achievementName).IsCompleted)
        {
            return true;
        }

        return known_completed_achievements.Contains(achievement.FullName);
    }

    public static void Complete(Achievement achievement)
    {
        if (achievement.IsCompleted)
        {
            return;
        }

        known_completed_achievements.Add(achievement.FullName);
        SaveCompletedAchievements();

        // TODO: OnAchievementComplete?
        DoCompleteEvents(achievement);
    }

    private static void DoCompleteEvents(Achievement achievement)
    {
        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        Main.NewText(Language.GetTextValue("Achievements.Completed", "[a:" + achievement.FullName + "]"));

        if (SoundEngine.FindActiveSound(SoundID.AchievementComplete) == null)
        {
            SoundEngine.PlayTrackedSound(SoundID.AchievementComplete);
        }

        InGameNotificationsTracker.AddNotification(new CompatibleAchievementUnlockedPopup(achievement));
    }

    private static void ReadCompletedAchievements()
    {
        if (!File.Exists(SavePath))
        {
            return;
        }

        try
        {
            var json = File.ReadAllText(SavePath);
            var loaded = JsonConvert.DeserializeObject<List<string>>(json);
            if (loaded is null)
            {
                return;
            }

            known_completed_achievements.Clear();

            foreach (var ach in loaded.Where(ach => !string.IsNullOrEmpty(ach)))
            {
                known_completed_achievements.Add(ach);
            }
        }
        catch (Exception e)
        {
            Main.NewText($"Failed to read achievements: {e}");
        }
    }

    private static void SaveCompletedAchievements()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(SavePath)!);

        try
        {
            var json = JsonConvert.SerializeObject(known_completed_achievements.ToList());
            File.WriteAllText(SavePath, json);
        }
        catch (Exception e)
        {
            Main.NewText($"Failed to save achievements: {e}");
        }
    }
}
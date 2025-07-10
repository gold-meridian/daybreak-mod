using Daybreak.Common.Features.Achievements;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Content.UI;

internal sealed class AchievementListItem : UIPanel
{
    public Achievement Achievement { get; }

    private readonly UIImageFramed? achievementIcon;
    private readonly UIImage achievementIconBorders;

    private Rectangle iconFrame;
    private readonly Rectangle iconFrameUnlocked;
    private readonly Rectangle iconFrameLocked;

    private readonly Asset<Texture2D> innerPanelBottomTexture;

    private bool locked;
    private readonly bool large;

    private static readonly Asset<Texture2D> inner_panel_top_texture = Main.Assets.Request<Texture2D>("Images/UI/Achievement_InnerPanelTop");

    public AchievementListItem(Achievement achievement, bool largeForOtherLanguages)
    {
        large = largeForOtherLanguages;
        BackgroundColor = new Color(26, 40, 89) * 0.8f;
        BorderColor = new Color(13, 20, 44) * 0.8f;
        float num = 16 + large.ToInt() * 20;
        float num2 = large.ToInt() * 6;
        float num3 = large.ToInt() * 12;
        Achievement = achievement;
        Height.Set(66f + num, 0f);
        Width.Set(0f, 1f);
        PaddingTop = 8f;
        PaddingLeft = 9f;
        // var num4 = Main.Achievements.GetIconIndex(achievement.Name);
        var texture = Achievement.GetIcon(out iconFrameUnlocked, out var lockedOffset);
        iconFrameLocked = iconFrameUnlocked;
        iconFrameLocked.X += lockedOffset;
        iconFrame = iconFrameLocked;
        UpdateIconFrame();
        achievementIcon = new UIImageFramed(texture, iconFrame);
        achievementIcon.Left.Set(num2, 0f);
        achievementIcon.Top.Set(num3, 0f);
        Append(achievementIcon);
        achievementIconBorders = new UIImage(Main.Assets.Request<Texture2D>("Images/UI/Achievement_Borders"));
        achievementIconBorders.Left.Set(-4f + num2, 0f);
        achievementIconBorders.Top.Set(-4f + num3, 0f);
        Append(achievementIconBorders);

        innerPanelBottomTexture = Main.Assets.Request<Texture2D>(
            large
                ? "Images/UI/Achievement_InnerPanelBottom_Large"
                : "Images/UI/Achievement_InnerPanelBottom"
        );
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        var num = large.ToInt() * 6;
        var vector = new Vector2(num, 0f);
        locked = !Achievement.IsCompleted;
        UpdateIconFrame();
        var innerDimensions = GetInnerDimensions();
        var dimensions = achievementIconBorders.GetDimensions();
        var num2 = dimensions.X + dimensions.Width;
        var vector2 = new Vector2(num2 + 7f, innerDimensions.Y);

        var maxDescriptionLineWidth = innerDimensions.Width - dimensions.Width + 1f - num * 2;

        var displayNameScale = new Vector2(0.85f);

        var descriptionTextScale = new Vector2(0.92f);
        var descriptionText = FontAssets.ItemStack.Value.CreateWrappedText(Achievement.Description.Value, (maxDescriptionLineWidth - 20f) * (1f / descriptionTextScale.X), Language.ActiveCulture.CultureInfo);
        var descriptionSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, descriptionText, descriptionTextScale, maxDescriptionLineWidth);
        if (!large)
        {
            descriptionSize = ChatManager.GetStringSize(FontAssets.ItemStack.Value, Achievement.Description.Value, descriptionTextScale, maxDescriptionLineWidth);
        }

        var descriptionScaleHeightToFit = 38f + (large ? 20 : 0);
        if (descriptionSize.Y > descriptionScaleHeightToFit)
        {
            descriptionTextScale.Y *= descriptionScaleHeightToFit / descriptionSize.Y;
        }

        var value = locked ? Color.Silver : Color.Gold;
        value = Color.Lerp(value, Color.White, IsMouseHovering ? 0.5f : 0f);
        var value2 = locked ? Color.DarkGray : Color.Silver;
        value2 = Color.Lerp(value2, Color.White, IsMouseHovering ? 1f : 0f);
        var color = IsMouseHovering ? Color.White : Color.Gray;
        var vector3 = vector2 - Vector2.UnitY * 2f + vector;
        DrawPanelTop(spriteBatch, vector3, maxDescriptionLineWidth, color);

        vector3.Y += 2f;
        vector3.X += 4f;
        var lastCategoryWidth = 0f;
        foreach (var category in Achievement.GetCategories())
        {
            var categoryTexture = category.GetIcon(out var categoryFrame);
            spriteBatch.Draw(categoryTexture.Value, vector3, categoryFrame, IsMouseHovering ? Color.White : Color.Silver, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

            vector3.X += lastCategoryWidth = categoryFrame.Width * 0.5f + 2f;
        }
        vector3.X -= lastCategoryWidth;

        vector3.X += 4f;
        vector3.X += 17f;
        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, Achievement.DisplayName.Value, vector3, value, 0f, Vector2.Zero, displayNameScale, maxDescriptionLineWidth);
        vector3.X -= 17f;
        var position = vector2 + Vector2.UnitY * 27f + vector;
        DrawPanelBottom(spriteBatch, position, maxDescriptionLineWidth, color);
        position.X += 8f;
        position.Y += 4f;
        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, descriptionText, position, value2, 0f, Vector2.Zero, descriptionTextScale);

        var progress = Achievement.GetProgress(out var progressText);
        if (!progress.HasValue)
        {
            return;
        }

        var vector4 = vector3 + Vector2.UnitX * maxDescriptionLineWidth + Vector2.UnitY;
        var baseScale3 = new Vector2(0.75f);
        var stringSize2 = ChatManager.GetStringSize(FontAssets.ItemStack.Value, progressText, baseScale3);

        var color2 = new Color(100, 255, 100);
        if (!IsMouseHovering)
        {
            color2 = Color.Lerp(color2, Color.Black, 0.25f);
        }

        var color3 = new Color(255, 255, 255);
        if (!IsMouseHovering)
        {
            color3 = Color.Lerp(color3, Color.Black, 0.25f);
        }

        DrawProgressBar(spriteBatch, progress.Value, vector4 - Vector2.UnitX * 80f * 0.7f, 80f, color3, color2, color2.MultiplyRGBA(new Color(new Vector4(1f, 1f, 1f, 0.5f))));
        vector4.X -= 80f * 1.4f + stringSize2.X;
        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, progressText, vector4, value, 0f, new Vector2(0f, 0f), baseScale3, 90f);
    }

    private void UpdateIconFrame()
    {
        iconFrame = locked ? iconFrameLocked : iconFrameUnlocked;
        achievementIcon?.SetFrame(iconFrame);
    }

    private void DrawPanelTop(
        SpriteBatch spriteBatch,
        Vector2 position,
        float width,
        Color color
    )
    {
        spriteBatch.Draw(
            inner_panel_top_texture.Value,
            position,
            new Rectangle(0, 0, 2, inner_panel_top_texture.Height()),
            color
        );
        spriteBatch.Draw(
            inner_panel_top_texture.Value,
            new Vector2(position.X + 2f, position.Y),
            new Rectangle(2, 0, 2, inner_panel_top_texture.Height()),
            color,
            0f,
            Vector2.Zero,
            new Vector2((width - 4f) / 2f, 1f),
            SpriteEffects.None,
            0f
        );
        spriteBatch.Draw(
            inner_panel_top_texture.Value,
            new Vector2(position.X + width - 2f, position.Y),
            new Rectangle(4, 0, 2, inner_panel_top_texture.Height()),
            color
        );
    }

    private void DrawPanelBottom(
        SpriteBatch spriteBatch,
        Vector2 position,
        float width,
        Color color
    )
    {
        spriteBatch.Draw(
            innerPanelBottomTexture.Value,
            position,
            new Rectangle(0, 0, 6, innerPanelBottomTexture.Height()),
            color
        );
        spriteBatch.Draw(
            innerPanelBottomTexture.Value,
            new Vector2(position.X + 6f, position.Y),
            new Rectangle(6, 0, 7, innerPanelBottomTexture.Height()),
            color,
            0f,
            Vector2.Zero,
            new Vector2((width - 12f) / 7f, 1f),
            SpriteEffects.None,
            0f
        );
        spriteBatch.Draw(
            innerPanelBottomTexture.Value,
            new Vector2(position.X + width - 6f, position.Y),
            new Rectangle(13, 0, 6, innerPanelBottomTexture.Height()),
            color
        );
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        base.MouseOver(evt);

        BackgroundColor = new Color(46, 60, 119);
        BorderColor = new Color(20, 30, 56);
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        base.MouseOut(evt);

        BackgroundColor = new Color(26, 40, 89) * 0.8f;
        BorderColor = new Color(13, 20, 44) * 0.8f;
    }

    private void DrawProgressBar(
        SpriteBatch spriteBatch,
        float progress,
        Vector2 spot,
        float width = 169f,
        Color backColor = default,
        Color fillingColor = default,
        Color blipColor = default
    )
    {
        if (blipColor == Color.Transparent)
        {
            blipColor = new Color(255, 165, 0, 127);
        }

        if (fillingColor == Color.Transparent)
        {
            fillingColor = new Color(255, 241, 51);
        }

        if (backColor == Color.Transparent)
        {
            fillingColor = new Color(255, 255, 255);
        }

        var colorBar = TextureAssets.ColorBar.Value;
        var magicPixel = TextureAssets.MagicPixel.Value;
        var clampedProgress = MathHelper.Clamp(progress, 0f, 1f);
        var normalizedWidth = width / 169f;
        var position = spot + Vector2.UnitY * 8f + Vector2.UnitX * 1f;

        spriteBatch.Draw(colorBar, spot, new Rectangle(5, 0, colorBar.Width - 9, colorBar.Height), backColor, 0f, new Vector2(84.5f, 0f), new Vector2(normalizedWidth, 1f), SpriteEffects.None, 0f);
        spriteBatch.Draw(colorBar, spot + new Vector2((0f - normalizedWidth) * 84.5f - 5f, 0f), new Rectangle(0, 0, 5, colorBar.Height), backColor, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        spriteBatch.Draw(colorBar, spot + new Vector2(normalizedWidth * 84.5f, 0f), new Rectangle(colorBar.Width - 4, 0, 4, colorBar.Height), backColor, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

        position += Vector2.UnitX * (clampedProgress - 0.5f) * width;
        position.X -= 1f;

        spriteBatch.Draw(magicPixel, position, new Rectangle(0, 0, 1, 1), fillingColor, 0f, new Vector2(1f, 0.5f), new Vector2(width * clampedProgress, 8f), SpriteEffects.None, 0f);

        if (progress != 0f)
        {
            spriteBatch.Draw(magicPixel, position, new Rectangle(0, 0, 1, 1), blipColor, 0f, new Vector2(1f, 0.5f), new Vector2(2f, 8f), SpriteEffects.None, 0f);
        }

        spriteBatch.Draw(magicPixel, position, new Rectangle(0, 0, 1, 1), Color.Black, 0f, new Vector2(0f, 0.5f), new Vector2(width * (1f - clampedProgress), 8f), SpriteEffects.None, 0f);
    }

    public override int CompareTo(object? obj)
    {
        if (obj is not AchievementListItem that)
        {
            return 0;
        }

        if (Achievement.IsCompleted && !that.Achievement.IsCompleted)
        {
            return -1;
        }

        if (!Achievement.IsCompleted && that.Achievement.IsCompleted)
        {
            return 1;
        }

        return Achievement.Id.CompareTo(that.Achievement.Id);
    }
}
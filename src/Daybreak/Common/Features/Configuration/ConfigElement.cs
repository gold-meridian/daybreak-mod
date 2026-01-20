using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Diagnostics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.Configuration;

public class ConfigElement<T> : ConfigElement
{
    protected ConfigEntry<T> ConfigEntry;

    public ConfigValue<T> Value
    {
        get => ConfigEntry.PendingState.GetPending(ConfigValueLayer.User);
        set => ConfigEntry.PendingState.SetPending(ConfigValueLayer.User, value);
    }

    public ConfigElement(IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        ConfigEntry = entry as ConfigEntry<T> ??
            throw new InvalidOperationException($"Entry does not wrap value of type {typeof(T)}: " + entry.Handle);
    }
}

public class ConfigElement : UIElement
{
    protected const float DEFAULT_HEIGHT = 38;

    protected static readonly Color COLOR = UICommon.DefaultUIBlue.MultiplyRGB(new Color(180, 180, 180));
    protected static readonly Color HOVER_COLOR = UICommon.DefaultUIBlue;
    protected static readonly Color FLASHING_COLOR = UICommon.DefaultUIBlue * 1.15f;

    protected Color PanelColor { get; set; }

    public bool Flashing { get; set; }

    public IConfigEntry Entry { get; set; }

    protected bool ForceDescription
    {
        get;

        set
        {
            Flashing = value;

            PanelColor = IsMouseHovering ? HOVER_COLOR : COLOR;

            field = value;
        }
    }

    public Action<IConfigEntry?>? OnShowDescription;

    public Action<IConfigEntry?>? OnHideDescription;

    protected UIElement LabelContainer;

    protected Icon? InfoIcon;

    protected UIAutoScaleTextTextPanel<LocalizedText> Label;

    private bool wasLeftMouseDown;

    public ConfigElement(IConfigEntry entry, bool showIcon) : base()
    {
        Entry = entry;

        const float upper_height = 30f;

        PanelColor = COLOR;

        Width.Set(0f, 1f);
        Height.Set(DEFAULT_HEIGHT, 0f);

        SetPadding(4f);

        LabelContainer = new UIElement();
        {
            LabelContainer.Width.Set(0f, 0.6f);
            LabelContainer.Height.Set(upper_height, 0f);

            LabelContainer.MinWidth.Set(30f, 0f);

            LabelContainer.IgnoresMouseInteraction = true;
        }
        Append(LabelContainer);

        if (showIcon && GetModSmallIcon(Entry.Handle.Mod) is { } icon)
        {
            const float icon_padding = 4f;

            LabelContainer.PaddingLeft += 30f + icon_padding;

            InfoIcon = new Icon();
            {
                InfoIcon.Width.Set(30, 0f);
                InfoIcon.Height.Set(30, 0f);

                InfoIcon.MarginLeft = -30f - icon_padding;

                InfoIcon.Texture = icon;
            }
            LabelContainer.Append(InfoIcon);
        }

        Label = new UIAutoScaleTextTextPanel<LocalizedText>(Entry.DisplayName);
        {
            Label.BackgroundColor = Color.Transparent;
            Label.BorderColor = Color.Transparent;

            Label.Width.Set(0f, 1f);
            Label.Height.Set(0f, 1f);

            Label.TextScaleMax = 0.9f;

            Label.TextOriginX = 0f;
            Label.TextOriginY = 0.5f;

            Label.SetPadding(0f);
        }
        LabelContainer.Append(Label);
    }

    private static Asset<Texture2D>? GetModSmallIcon(Mod? mod)
    {
        if (mod is null)
        {
            return AssetReferences.Assets.Images.Configuration.ModIcon_Terraria.Asset;
        }

        if (mod is ModLoaderMod)
        {
            return AssetReferences.Assets.Images.Configuration.ModIcon_ModLoader.Asset;
        }

        if (mod.RequestAssetIfExists<Texture2D>("icon_small", out var iconSmall))
        {
            return iconSmall;
        }

        return mod.RequestAssetIfExists<Texture2D>("icon", out var icon) ? icon : null;
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        base.MouseOver(evt);

        OnShowDescription?.Invoke(Entry);

        if (!ForceDescription)
        {
            PanelColor = HOVER_COLOR;

            Flashing = false;
        }
    }

    public override void MouseOut(UIMouseEvent evt)
    {
        base.MouseOut(evt);

        if (!ForceDescription)
        {
            PanelColor = COLOR;

            OnHideDescription?.Invoke(Entry);
        }
    }

    public override void LeftClick(UIMouseEvent evt)
    {
        base.LeftClick(evt);

        if (evt.Target != this)
        {
            return;
        }

        OnHideDescription?.Invoke(null);
        OnShowDescription?.Invoke(Entry);

        ForceDescription = !ForceDescription;

        SoundEngine.PlaySound(ForceDescription ? SoundID.MenuOpen : SoundID.MenuClose);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        // Checking for 'Main.mouseLeft && Main.mouseLeftRelease' doesn't seem to work here.
        var clickedOff = !Main.hasFocus || (wasLeftMouseDown && !Main.mouseLeft && !IsMouseHovering);

        wasLeftMouseDown = Main.mouseLeft;

        if (clickedOff && ForceDescription)
        {
            OnHideDescription?.Invoke(Entry);

            ForceDescription = false;
        }

        if (Flashing)
        {
            float ratio = Utils.Turn01ToCyclic010(Main.GlobalTimeWrappedHourly * 60 % 120 / 120f) * 0.5f + 0.5f;
            PanelColor = Color.Lerp(HOVER_COLOR, FLASHING_COLOR, MathF.Pow(ratio, 2f));
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        DrawPanel(spriteBatch, PanelColor, this.Dimensions);
    }

    private static void DrawPanel(
        SpriteBatch sb,
        Color color,
        Rectangle dims
    )
    {
        const int highlight_size = (int)(DEFAULT_HEIGHT * 0.5f);

        TextureAssets.SettingsPanel.Wait();
        var texture = TextureAssets.SettingsPanel.Value;

        // Left/Right bars.
        sb.Draw(texture, new Rectangle(dims.X, dims.Y + 2, 2, dims.Height - 4), new Rectangle(0, 2, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + dims.Width - 2, dims.Y + 2, 2, dims.Height - 4), new Rectangle(0, 2, 1, 1), color);

        // Up/Down bars.
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y, dims.Width - 4, 2), new Rectangle(2, 0, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + dims.Height - 2, dims.Width - 4, 2), new Rectangle(2, 0, 1, 1), color);

        // Inner Panel.
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + 2, dims.Width - 4, highlight_size - 2), new Rectangle(2, 2, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + highlight_size, dims.Width - 4, dims.Height - highlight_size - 2), new Rectangle(2, 16, 1, 1), color);
    }

    protected sealed class Icon : UIElement
    {
        public Asset<Texture2D>? Texture { get; set; }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (Texture is null)
            {
                return;
            }

            var dims = this.Dimensions;

            spriteBatch.Draw(
                Texture.Value,
                dims,
                null,
                Color.White,
                0f,
                Vector2.Zero,
                SpriteEffects.None,
                0f
            );
        }
    }
}

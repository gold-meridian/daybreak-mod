using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.Configuration;

public class ConfigElement : UIPanel
{
    protected UIElement LabelContainer;

    protected Icon InfoIcon;

    protected UIAutoScaleTextTextPanel<LocalizedText> Label;

    public ConfigElement(IConfigEntry entry) : base()
    {
        const float default_height = 42f;
        const float upper_height = 30f;

        _backgroundTexture = AssetReferences.Assets.Images.UI.FullPanel.Asset;
        BorderColor = Color.Transparent;

        Width.Set(0f, 1f);
        Height.Set(default_height, 0f);

        SetPadding(6f);

        LabelContainer = new UIElement();
        {
            LabelContainer.Width.Set(0f, 0.8f);
            LabelContainer.Height.Set(upper_height, 0f);

            // Padding doesn't seem to apply automatically
            LabelContainer.Left.Set(PaddingLeft, 0f);
            LabelContainer.Top.Set(PaddingTop, 0f);

            LabelContainer.MinWidth.Set(30f, 0f);
        }
        Append(LabelContainer);

        if (GetModSmallIcon(entry.Handle.Mod) is { } icon)
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

        Label = new UIAutoScaleTextTextPanel<LocalizedText>(entry.DisplayName);
        {
            Label.BackgroundColor = Color.Transparent;
            Label.BorderColor = Color.Transparent;

            Label.Width.Set(0f, 1f);
            Label.Height.Set(0f, 1f);
            // Label.MinWidth.Set(120f, 0f);

            Label.TextOriginX = 0f;

            SetPadding(0f);
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

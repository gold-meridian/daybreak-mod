using System;
using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.UI;

namespace Daybreak.Content.UI;

/// <summary>
///     A search bar based on <see cref="InputField"/>.
/// </summary>
internal sealed class SearchBar : InputField
{
    private sealed class BetterImageButton(Asset<Texture2D> asset) : UIElement
    {
        public Asset<Texture2D> Asset
        {
            get;

            set
            {
                field = value;

                Width.Set(value.Width(), 0f);
                Height.Set(value.Height(), 0f);
            }
        } = asset;

        public Asset<Texture2D>? HoverImage { get; set; }

        public float VisibilityActive { get; set; } = 1f;

        public float VisibilityInactive { get; set; } = 0.4f;

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            SoundEngine.PlaySound(12);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            // Automatically truncated.
            var dims = this.Dimensions;

            spriteBatch.Draw(
                new DrawParameters(Asset)
                {
                    Position = dims.TopLeft(),
                    Color = Color.White * (IsMouseHovering ? VisibilityActive : VisibilityInactive),
                }
            );

            if (HoverImage is not null && IsMouseHovering)
            {
                spriteBatch.Draw(
                    new DrawParameters(HoverImage)
                    {
                        Position = dims.TopLeft(),
                    }
                );
            }
        }
    }

    /// <summary>
    ///     Initializes this search bar with some default styling.
    /// </summary>
    public SearchBar(Func<string> hint) : base(hint)
    {
        const float button_margin = 22f;

        PaddingLeft += button_margin;
        PaddingRight += button_margin;

        OnEscape += OnEscape_CancelText;

        var searchButtonAsset = AssetReferences.Assets.Images.UI.SearchIcon.Asset;
        {
            searchButtonAsset.Wait();
        }
        var searchButton = new BetterImageButton(searchButtonAsset);
        {
            searchButton.VAlign = 0.5f;
            searchButton.Left = new StyleDimension(-button_margin, 0f);
        }
        Append(searchButton);

        var searchCancelAsset = AssetReferences.Assets.Images.UI.SearchCancel.Asset;
        {
            searchCancelAsset.Wait();
        }
        var searchCancelButton = new BetterImageButton(searchCancelAsset);
        {
            searchCancelButton.VAlign = 0.5f;
            searchCancelButton.HAlign = 1f;
            searchCancelButton.Left = new StyleDimension(button_margin, 0f);
            searchCancelButton.OnLeftClick += SearchCancelButton_CancelText;
        }
        Append(searchCancelButton);
    }

    /// <inheritdoc />
    public SearchBar(string hint) : this(() => hint) { }

    /// <inheritdoc />
    public SearchBar(LocalizedText text) : this(() => text.Value) { }

    private void OnEscape_CancelText(InputField input)
    {
        Text = string.Empty;
    }

    private void SearchCancelButton_CancelText(UIMouseEvent evt, UIElement listeningElement)
    {
        Text = string.Empty;
    }
}

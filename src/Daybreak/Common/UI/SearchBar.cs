using Daybreak.Core;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;

namespace Daybreak.Common.UI;

public class SearchBar : InputField
{
    public SearchBar(LocalizedText hint, int maxChars = 50, float textScale = 1f)
        : base(hint.ToString(), maxChars, textScale)
    {
        const float button_margin = 22f;

        PaddingLeft += button_margin;
        PaddingRight += button_margin;

        OnEscape += OnEscape_CancelText;

        AssetReferences.Assets.Images.UI.SearchIcon.Asset.Wait();

        // This button is useless.
        var searchButton = new UIImageButton(AssetReferences.Assets.Images.UI.SearchIcon.Asset);
        {
            searchButton.VAlign = 0.5f;

            searchButton.Left = new StyleDimension(-button_margin, 0f);
        }
        Append(searchButton);

        AssetReferences.Assets.Images.UI.SearchCancel.Asset.Wait();

        var searchCancelButton = new UIImageButton(AssetReferences.Assets.Images.UI.SearchCancel.Asset);
        {
            searchCancelButton.VAlign = 0.5f;
            searchCancelButton.HAlign = 1f;
            searchCancelButton.Left = new StyleDimension(button_margin, 0f);
            searchCancelButton.OnLeftClick += SearchCancelButton_CancelText;
        }
        Append(searchCancelButton);
    }

    private void OnEscape_CancelText(InputField input)
    {
        Text = string.Empty;
    }

    private void SearchCancelButton_CancelText(UIMouseEvent evt, UIElement listeningElement)
    {
        Text = string.Empty;
    }
}

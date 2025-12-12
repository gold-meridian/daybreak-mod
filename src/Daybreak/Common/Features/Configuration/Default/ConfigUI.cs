using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Daybreak.Common.Features.Configuration;

internal abstract class ConfigState : UIState, IHaveBackButtonCommand
{
    public static Color DescriptionPanelColor { get; } = Color.LightGray * 0.7f;

    public ConfigRepository Repository { get; protected set; }

    public ConfigCategory? TargetCategory { get; protected set; }

    public IConfigEntry? TargetEntry { get; protected set; }

    protected UIElement? baseElement;
    protected UIPanel? backPanel;
    protected UIPanel? descriptionPanel;
    protected UIText? descriptionText;
    protected UIPanel? metadataPanel;
    protected UIText? metadataText;
    protected CategoryTabList? tabs;
    protected UITextPanel<LocalizedText>? headerPanel;
    protected UITextPanel<LocalizedText>? backButton;
    protected UITextPanel<LocalizedText>? saveButton;
    protected UIVerticalSeparator? separator;

    private readonly Action? exitAction;

    // Not used in favor of exitAction.
    UIState? IHaveBackButtonCommand.PreviousUIState { get; set; } = null;

    public ConfigState(
        ConfigRepository repository,
        ConfigCategoryHandle? category = null,
        ConfigEntryHandle? entry = null,
        Action? onExit = null
    )
    {
        Repository = repository;

        if (category.HasValue && Repository.TryGetCategory(category.Value, out var theCategory))
        {
            TargetCategory = theCategory;
        }

        if (entry.HasValue && Repository.TryGetEntry(entry.Value, out var theEntry))
        {
            TargetEntry = theEntry;

            if (TargetCategory is not null && !theEntry.Categories.Contains(TargetCategory))
            {
                TargetCategory = null;
            }

            if (TargetCategory is null && Repository.TryGetCategory(theEntry.MainCategory, out var mainCategory))
            {
                TargetCategory = mainCategory;
            }
        }

        exitAction = onExit;
    }

    public override void OnInitialize()
    {
        const float min_panel_width = 600f;
        const float max_panel_width = 800f;
        const float vertical_margin = 160f;

        baseElement = new UIElement();
        {
            baseElement.Width.Set(0f, 0.8f);
            baseElement.MinWidth.Set(min_panel_width, 0f);
            baseElement.MaxWidth.Set(max_panel_width, 0f);

            baseElement.Top.Set(vertical_margin, 0f);
            baseElement.Height.Set(0f, 1f);

            baseElement.HAlign = 0.5f;
        }
        Append(baseElement);

        backPanel = new UIPanel();
        {
            backPanel.Width = StyleDimension.Fill;
            backPanel.Height.Set(-vertical_margin * 1.75f, 1f);
            backPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
        }
        baseElement.Append(backPanel);

        separator = new UIVerticalSeparator();
        {
            separator.Height.Set(0f, 1f);
            separator.HAlign = 0.25f;
            separator.VAlign = 1f;
            separator.Color = new Color(85, 88, 159);
        }
        backPanel.Append(separator);

        descriptionPanel = new UIPanel();
        {
            descriptionPanel.Width.Set(-backPanel.PaddingRight, 0.75f);
            descriptionPanel.Height.Set(64, 0f);
            descriptionPanel.HAlign = 1f;
            descriptionPanel.VAlign = 1f;
            descriptionPanel._backgroundTexture = AssetReferences.Assets.Images.UI.ConfigDescriptionPanel.Asset;
            descriptionPanel.BackgroundColor = DescriptionPanelColor;
            descriptionPanel.BorderColor = Color.Transparent;

            descriptionText = new UIText(
                Mods.Daybreak.UI.DefaultConfigDescription.GetText()
            );
            {
                descriptionText.Width.Set(0f, 1f);
                descriptionText.Height.Set(0f, 1f);
                descriptionText.IsWrapped = true;
            }
            descriptionPanel.Append(descriptionText);
        }
        backPanel.Append(descriptionPanel);

        metadataPanel = new UIPanel();
        {
            metadataPanel.Width.Set(-backPanel.PaddingRight, 0.25f);
            metadataPanel.Height.Set(64, 0f);
            metadataPanel.HAlign = 0f;
            metadataPanel.VAlign = 1f;
            metadataPanel._backgroundTexture = AssetReferences.Assets.Images.UI.ConfigDescriptionPanel.Asset;
            metadataPanel.BackgroundColor = DescriptionPanelColor;
            metadataPanel.BorderColor = Color.Transparent;

            metadataText = new UIText(
                Mods.Daybreak.UI.DefaultConfigDescription.GetText()
            );
            {
                metadataText.Width.Set(0f, 1f);
                metadataText.Height.Set(0f, 1f);
                metadataText.IsWrapped = true;
            }
            metadataPanel.Append(metadataText);
        }
        backPanel.Append(metadataPanel);

        // Panel tabs
        const float tabs_width = 160f;
        {
            var verticalMargin = vertical_margin + backPanel._cornerSize;

            var container = new UIElement();
            {
                container.Top.Set(verticalMargin, 0f);
                container.Height.Set(-(verticalMargin * 2f), 1f);

                // Effectively the inverse of the panel width calculation.
                container.Width.Set(0f, 0.2f);
                container.MinWidth.Set(-(min_panel_width * 0.5f), 0.5f);
            }
            baseElement.Append(container);

            tabs = new CategoryTabList(Repository);
            {
                tabs.Width.Set(tabs_width, 0f);
                tabs.Height.Set(0, 1f);
                tabs.HAlign = 1f;
            }
            container.Append(tabs);
        }

        // Header
        headerPanel = new UITextPanel<LocalizedText>(
            Repository.DisplayName,
            textScale: 0.8f,
            large: true
        );
        {
            headerPanel.SetPadding(13f);
            headerPanel.Top.Set(-44f, 0f);
            headerPanel.HAlign = 0.5f;
            headerPanel.BackgroundColor = UICommon.DefaultUIBlue;
        }
        backPanel.Append(headerPanel);

        backButton = new UITextPanel<LocalizedText>(
            Language.GetText("UI.Back"),
            textScale: 0.7f,
            large: true
        );
        {
            backButton.Width.Set(-8f, 0.5f);
            backButton.Height.Set(50f, 0f);
            backButton.Top.Set(-vertical_margin - 50, 0f);
            backButton.VAlign = 1f;
            backButton.HAlign = 0f;
            backButton.OnLeftMouseDown += GoBackClick;
            backButton.WithFadedMouseOver();
        }
        baseElement.Append(backButton);
        
        saveButton = new UITextPanel<LocalizedText>(
            Language.GetText("tModLoader.ModConfigSaveConfig"),
            textScale: 0.7f,
            large: true
        );
        {
            saveButton.Width.Set(-8f, 0.5f);
            saveButton.Height.Set(50, 0f);
            saveButton.Top.Set(-vertical_margin - 50, 0f);
            saveButton.VAlign = 1f;
            saveButton.HAlign = 1f;
            saveButton.OnLeftMouseDown += GoBackClick;
            saveButton.WithFadedMouseOver();
        }
        baseElement.Append(saveButton);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
    }

    protected void ExitState()
    {
        SoundEngine.PlaySound(in SoundID.MenuClose);

        if (Main.gameMenu)
        {
            IngameFancyUI.Close();
        }
        else
        {
            Main.MenuUI.SetState(null);
        }

        exitAction?.Invoke();
    }

#region Buttons
    private void DescriptionMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
        // TODO
    }

    private void DescriptionMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
        descriptionText?.SetText(Mods.Daybreak.UI.DefaultConfigDescription.GetText());
    }

    void IHaveBackButtonCommand.HandleBackButtonUsage()
    {
        ExitState();
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
    {
        ((IHaveBackButtonCommand)this).HandleBackButtonUsage();
    }
#endregion
}

public class CategoryTabList : UIList
{
    public event Action<ConfigCategory>? OnCategorySelected;

    public ConfigCategory Category
    {
        get => field;

        set
        {
            this[Category]?.Selected = false;
            this[value]?.Selected = true;

            field = value;
        }
    }

    public CategoryTab? this[ConfigCategory category]
    {
        get
        {
            foreach (var elem in this)
            {
                if (elem is CategoryTab tab && tab.Category == category)
                {
                    return tab;
                }
            }

            return null;
        }
    }

    public CategoryTabList(ConfigRepository repository) : base()
    {
        ListPadding = 2f;

        foreach (var category in repository.Categories)
        {
            var tab = new CategoryTab(category);

            tab.OnLeftClick += OnClickTab;

            Add(tab);
        }

        Category = repository.Categories.First();
    }

    private void OnClickTab(UIMouseEvent evt, UIElement listeningElement)
    {
        if (listeningElement is CategoryTab tab)
        {
            Category = tab.Category;
            OnCategorySelected?.Invoke(Category);
        }
    }

    public class CategoryTab : UIAutoScaleTextTextPanel<LocalizedText>
    {
        public ConfigCategory Category;

        public bool Selected
        {
            get => field;

            set
            {
                field = value;

                BorderColor = UICommon.DefaultUIBorder;

                if (value)
                {
                    BackgroundColor = UICommon.DefaultUIBlue;
                }
                else
                {
                    BackgroundColor = UICommon.MainPanelBackground;
                }
            }
        }

        public CategoryTab(ConfigCategory category) : base(category.DisplayName)
        {
            this.Category = category;

            _backgroundTexture = AssetReferences.Assets.Images.UI.ConfigTabPanel.Asset;
            _borderTexture = AssetReferences.Assets.Images.UI.ConfigTabPanelOutline.Asset;

            BackgroundColor = UICommon.MainPanelBackground;
            BorderColor = UICommon.DefaultUIBorder;

            this.WithFadedMouseOver();

            MinWidth.Set(0f, 1f);

            MinHeight.Set(38f, 0f);

            HAlign = 1f;

            TextOriginX = 0f;

            SetPadding(4f);

            // Makes the text respond to padding.
            UseInnerDimensions = true;

            if (category.Icon is not null)
            {
                var icon = category.Icon;

                // If an icon is not loaded the width values used are 0.
                icon.Wait();

                float iconMargin = icon.Width();

                PaddingLeft += iconMargin;

                UIImage tabIcon = new(category.Icon)
                {
                    VAlign = 0.5f,
                    HAlign = 0f,
                    MarginLeft = -iconMargin,
                    MarginTop = -2f
                };

                Append(tabIcon);
            }

            Recalculate();
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            if (!Selected)
            {
                SoundEngine.PlaySound(in SoundID.MenuTick);

                BackgroundColor = UICommon.DefaultUIBlue;
                BorderColor = UICommon.DefaultUIBorderMouseOver;
            }
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            if (!Selected)
            {
                SoundEngine.PlaySound(in SoundID.MenuTick);

                BackgroundColor = UICommon.MainPanelBackground;
                BorderColor = UICommon.DefaultUIBorder;
            }
        }
    }
}

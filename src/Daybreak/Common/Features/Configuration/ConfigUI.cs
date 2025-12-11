using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.Configuration;

public class ConfigState : UIState, IHaveBackButtonCommand
{
    private readonly Action? exitAction;

    UIState IHaveBackButtonCommand.PreviousUIState { get; set; }

    protected ConfigRepository repository;

    protected ConfigCategoryHandle? targetCategory;

    protected ConfigEntryHandle? targetEntry;

    protected UIPanel? basePanel;

    protected UITextPanel<LocalizedText>? headerPanel;

    protected CategoryTabList? tabs;

    protected UITextPanel<LocalizedText>? backButton;

    public ConfigState(ConfigRepository repository, ConfigCategoryHandle? category = null, ConfigEntryHandle? entry = null, Action? onExit = null)
    {
        this.repository = repository;

        targetCategory = category;
        targetEntry = entry;

        if (targetEntry is not null)
        {
            targetCategory ??= this.repository.GetEntry(targetEntry.Value).MainCategory;
        }

        exitAction = onExit;
    }

    public override void OnInitialize()
    {
        const float vertical_margin = 160f;

        // Base panel
        basePanel = new();

        basePanel.Width.Set(0f, 0.8f);
        basePanel.MaxWidth.Set(600f, 0f);

        basePanel.Top.Set(vertical_margin, 0f);
        basePanel.Height.Set(-(vertical_margin * 2f), 1f);

        basePanel.HAlign = 0.5f;

        basePanel.BackgroundColor = UICommon.MainPanelBackground;

        Append(basePanel);

        // Panel tabs
        float tabsMargin = vertical_margin + basePanel._cornerSize;

        tabs = new(repository, basePanel);

        tabs.Top.Set(tabsMargin, 0f);
        tabs.Height.Set(-(tabsMargin * 2f), 1f);

        Append(tabs);

        // Header
        headerPanel = new(repository.DisplayName, 0.8f, large: true);

        headerPanel.SetPadding(15f);

        headerPanel.Top.Set(vertical_margin - 50f, 0f);

        headerPanel.HAlign = 0.5f;

        headerPanel.BackgroundColor = UICommon.DefaultUIBlue;

        Append(headerPanel);

        const float button_vertical_margin = 10f;

        // Back button
        backButton = new(Language.GetText("UI.Back"), 0.7f, true);

        backButton.Top.Set(-vertical_margin + button_vertical_margin, 1f);

        backButton.Width.Set(0f, 0.4f);
        backButton.MaxWidth.Set(300f, 0f);

        backButton.Height.Set(50f, 0f);

        backButton.HAlign = 0.5f;

        backButton.OnLeftMouseDown += GoBackClick;

        backButton.WithFadedMouseOver();

        Append(backButton);
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

    public void HandleBackButtonUsage()
    {
        ExitState();
    }

    private void GoBackClick(UIMouseEvent evt, UIElement listeningElement)
    {
        ExitState();
    }

    #endregion
}

public class CategoryTabList : UIList
{
    protected UIPanel attachedPanel;

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

    public CategoryTabList(ConfigRepository repository, UIPanel attachedPanel) : base()
    {
        this.attachedPanel = attachedPanel;

        ListPadding = 2f;

        foreach (var category in repository.Categories)
        {
            var tab = new CategoryTab(category);

            tab.OnLeftClick += OnClickTab;

            Add(tab);
        }

        Category = repository.Categories.First();
    }

    public override void Update(GameTime gameTime)
    {
        // Update the width so it'll always match the left edge of the attached panel.
        Rectangle dims = attachedPanel.Dimensions;

        Width.Set(dims.X, 0f);
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

            MinWidth.Set(160f, 0f);

            MinHeight.Set(38f, 0f);

            HAlign = 1f;

            TextOriginX = 0f;

            SetPadding(4f);

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

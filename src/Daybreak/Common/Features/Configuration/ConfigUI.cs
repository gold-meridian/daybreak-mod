using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
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

        basePanel = new();

        basePanel.Width.Set(0f, 0.8f);
        basePanel.MaxWidth.Set(600f, 0f);

        basePanel.Top.Set(vertical_margin, 0f);
        basePanel.Height.Set(-(vertical_margin * 2f), 1f);

        basePanel.HAlign = 0.5f;

        Append(basePanel);

        float tabsMargin = vertical_margin + basePanel._cornerSize;

        tabs = new(repository, basePanel);

        tabs.Top.Set(tabsMargin, 0f);
        tabs.Height.Set(-(tabsMargin * 2f), 1f);

        backButton = new(Language.GetText("UI.Back"), 0.7f, true);


    }

    #region Buttons

    void IHaveBackButtonCommand.HandleBackButtonUsage()
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

    #endregion
}

public class CategoryTabList : UIList
{
    protected UIPanel attachedPanel;

    public CategoryTabList(ConfigRepository repository, UIPanel attachedPanel) : base()
    {
        this.attachedPanel = attachedPanel;

        foreach (var category in repository.GetCategories())
        {
            Add(new CategoryTab(category));
        }
    }

    public override void Update(GameTime gameTime)
    {
        // Update the width so it'll always match the left edge of the attached panel.
        Rectangle dims = attachedPanel.Dimensions;

        Width.Set(dims.X, 0f);
    }

    public class CategoryTab : UITextPanel<LocalizedText>
    {
        public CategoryTab(ConfigCategory category) : base(category.DisplayName)
        {
            _backgroundTexture = AssetReferences.Assets.Images.UI.ConfigTabPanel.Asset;
            _borderTexture = AssetReferences.Assets.Images.UI.ConfigTabPanelOutline.Asset;
        }
    }
}

using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Daybreak.Common.Rendering;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.Default;
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
    protected UIVerticalSeparator? separator;
    protected UIPanel? descriptionPanel;
    protected UIText? descriptionText;
    protected UIPanel? metadataPanel;
    protected UIText? metadataText;
    protected CategoryTabList? tabs;
    protected UIScrollbar? tabScrollbar;
    protected UIScrollbar? configScrollbar;
    protected UITextPanel<LocalizedText>? headerPanel;
    protected UITextPanel<LocalizedText>? backButton;
    protected UITextPanel<LocalizedText>? saveButton;

    // Search bar
    protected UISearchBar searchBar;
    protected UIPanel searchBoxPanel;
    protected string searchString;
    protected bool didClickSomething;
    protected bool didClickSearchBar;

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

        const float category_margin = 1f / 3f;
        const float config_margin = 2f / 3f;

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
            separator.HAlign = category_margin;
            separator.VAlign = 1f;
            separator.Color = new Color(85, 88, 159);
        }
        backPanel.Append(separator);

        descriptionPanel = new UIPanel();
        {
            descriptionPanel.Width.Set(-backPanel.PaddingRight, config_margin);
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
            metadataPanel.Width.Set(-backPanel.PaddingRight, category_margin);
            metadataPanel.Height.Set(64, 0f);
            metadataPanel.HAlign = 0f;
            metadataPanel.VAlign = 1f;
            metadataPanel._backgroundTexture = AssetReferences.Assets.Images.UI.ConfigDescriptionPanel.Asset;
            metadataPanel.BackgroundColor = DescriptionPanelColor;
            metadataPanel.BorderColor = Color.Transparent;

            metadataText = new UIText(
                Mods.Daybreak.UI.DefaultMetadataDescription.GetText()
            );
            {
                metadataText.Width.Set(0f, 1f);
                metadataText.Height.Set(0f, 1f);
                metadataText.IsWrapped = true;
                metadataText.TextColor = Color.DarkGray;
            }
            metadataPanel.Append(metadataText);
        }
        backPanel.Append(metadataPanel);

        const float search_bar_height = 24f;
        const float search_bar_offset = search_bar_height + 6f;
        var searchBarContainer = new UIElement();
        {
            searchBarContainer.Height.Set(search_bar_height, 0f);
            searchBarContainer.Width.Set(-backPanel.PaddingRight, category_margin);
            AddSearchBar(searchBarContainer);
        }
        backPanel.Append(searchBarContainer);

        tabScrollbar = new UIScrollbar();
        {
            // It overflows by this amount of pixels vertically on the top and
            // bottom, for some reason.
            const float vertical_adjust = 6f;

            tabScrollbar.Height.Set(-64f - backPanel.PaddingTop - search_bar_offset - (vertical_adjust * 2f), 1f);
            tabScrollbar.Top.Set(search_bar_offset + vertical_adjust, 0f);
            tabScrollbar.HAlign = category_margin;
            tabScrollbar.Left.Set(-tabScrollbar.Width.Pixels - 6f, 0f);
        }
        backPanel.Append(tabScrollbar);

        // Panel tabs
        {
            var container = new UIElement();
            {
                var widthReduction = tabScrollbar.Width.Pixels + backPanel.PaddingRight / 2f;

                container.Width.Set(-backPanel.PaddingRight - widthReduction, category_margin);
                container.Height.Set(-64f - backPanel.PaddingTop - search_bar_offset, 1f);
                container.Top.Set(search_bar_offset, 0f);
                container.Left.Set(0f, 0f);
            }
            backPanel.Append(container);

            tabs = new CategoryTabList(Repository);
            {
                tabs.Width.Set(0f, 1f);
                tabs.Height.Set(0f, 1f);
                tabs.HAlign = 1f;
                tabs.SetScrollbar(tabScrollbar);
            }
            container.Append(tabs);
        }

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

    private void AddSearchBar(UIElement searchArea)
    {
        var searchButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search"));
        {
            searchButton.VAlign = 0.5f;
            searchButton.HAlign = 0f;
            searchButton.OnLeftClick += ClickSearchArea;
            searchButton.SetHoverImage(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Button_Search_Border"));
            searchButton.SetVisibility(1f, 1f);
        }
        searchArea.Append(searchButton);

        searchBoxPanel = new UIPanel();
        {
            searchBoxPanel.Width = new StyleDimension(0f - searchButton.Width.Pixels - 3f, 1f);
            searchBoxPanel.Height = new StyleDimension(0f, 1f);
            searchBoxPanel.VAlign = 0.5f;
            searchBoxPanel.HAlign = 1f;
            searchBoxPanel.BackgroundColor = new Color(35, 40, 83);
            searchBoxPanel.BorderColor = new Color(35, 40, 83);
            searchBoxPanel.SetPadding(0f);
            searchBoxPanel.OnLeftClick += ClickSearchArea;
        }
        searchArea.Append(searchBoxPanel);

        searchBar = new UISearchBar(Language.GetText("UI.PlayerNameSlot"), 0.8f);
        {
            searchBar.Width = new StyleDimension(0f, 1f);
            searchBar.Height = new StyleDimension(0f, 1f);
            searchBar.HAlign = 0f;
            searchBar.VAlign = 0.5f;
            searchBar.Left = new StyleDimension(0f, 0f);
            searchBar.IgnoresMouseInteraction = true;
            searchBar.OnContentsChanged += SearchBarContentsChanged;
            searchBar.OnStartTakingInput += SearchBarStartTakingInput;
            searchBar.OnEndTakingInput += SearchBarEndTakingInput;
            searchBar.OnNeedingVirtualKeyboard += SearchBarOpenVirtualKeyboardWhenNeeded;
            searchBar.OnCanceledTakingInput += SearchBarCanceledInput;
        }
        searchBoxPanel.Append(searchBar);

        var searchCancelButton = new UIImageButton(Main.Assets.Request<Texture2D>("Images/UI/SearchCancel"));
        {
            searchCancelButton.HAlign = 1f;
            searchCancelButton.VAlign = 0.5f;
            searchCancelButton.Left = new StyleDimension(-2f, 0f);
            searchCancelButton.OnMouseOver += SearchCancelButtonOnMouseOver;
            searchCancelButton.OnLeftClick += SearchCancelButtonOnClick;
        }
        searchBoxPanel.Append(searchCancelButton);

        return;

        void ClickSearchArea(UIMouseEvent e, UIElement el)
        {
            if (e.Target.Parent == searchBoxPanel)
            {
                return;
            }

            searchBar.ToggleTakingText();
            didClickSearchBar = true;
        }

        void SearchBarContentsChanged(string contents)
        {
            searchString = contents;
            // _filterer.SetSearchFilter(contents);
            // UpdateContents();
        }

        void SearchBarStartTakingInput()
        {
            searchBoxPanel.BorderColor = Main.OurFavoriteColor;
        }

        void SearchBarEndTakingInput()
        {
            searchBoxPanel.BorderColor = new Color(35, 40, 83);
        }

        void SearchBarOpenVirtualKeyboardWhenNeeded()
        {
            const int max_input_length = 40;
            var uIVirtualKeyboard = new UIVirtualKeyboard(
                Language.GetText("UI.PlayerNameSlot").Value,
                searchString,
                OnFinishedSettingName,
                GoBackHere,
                3,
                allowEmpty: true
            );
            {
                uIVirtualKeyboard.SetMaxInputLength(max_input_length);
                uIVirtualKeyboard.CustomEscapeAttempt = EscapeVirtualKeyboard;
            }

            IngameFancyUI.OpenUIState(uIVirtualKeyboard);
        }

        void SearchBarCanceledInput()
        {
            // Main.LocalPlayer.ToggleInv();
        }

        void OnFinishedSettingName(string name)
        {
            var contents = name.Trim();
            searchBar.SetContents(contents);
            GoBackHere();
        }

        void GoBackHere()
        {
            // TODO: ?!?!
            IngameFancyUI.OpenUIState(this);
        }

        bool EscapeVirtualKeyboard()
        {
            // TODO: ?!?!
            if (searchBar.IsWritingText)
                searchBar.ToggleTakingText();

            GoBackHere();
            return true;
        }

        void SearchCancelButtonOnMouseOver(UIMouseEvent e, UIElement el)
        {
            SoundEngine.PlaySound(12);
        }

        void SearchCancelButtonOnClick(UIMouseEvent e, UIElement el)
        {
            if (searchBar.HasContents)
            {
                searchBar.SetContents(null, forced: true);
                SoundEngine.PlaySound(11);
            }
            else
            {
                SoundEngine.PlaySound(12);
            }
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (didClickSomething && !didClickSearchBar && searchBar.isWritingText)
        {
            searchBar.ToggleTakingText();
        }

        didClickSomething = false;
        didClickSearchBar = false;
    }

    public override void LeftClick(UIMouseEvent evt)
    {
        base.LeftClick(evt);

        AttemptToStopUsingSearchBar();
    }

    public override void RightClick(UIMouseEvent evt)
    {
        base.RightClick(evt);

        AttemptToStopUsingSearchBar();
    }

    private void AttemptToStopUsingSearchBar()
    {
        didClickSomething = true;
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

        var categoriesByMod = new Dictionary<ValueTuple<Mod?>, List<ConfigCategory>>();
        foreach (var category in repository.Categories)
        {
            if (!categoriesByMod.TryGetValue(new ValueTuple<Mod?>(category.Handle.Mod), out var categories))
            {
                categoriesByMod[new ValueTuple<Mod?>(category.Handle.Mod)] = categories = [];
            }

            categories.Add(category);
        }

        foreach (var (mod, categories) in categoriesByMod)
        {
            var headerElement = new UIElement();
            {
                headerElement.Width = StyleDimension.Fill;
                headerElement.Height.Set(40f, 0f);

                var header = new ModHeader(mod.Item1);
                headerElement.Append(header);

                var divider = new UIHorizontalSeparator();
                {
                    divider.Width = StyleDimension.Fill;
                    divider.VAlign = 1f;
                    divider.Top.Set(-2f, 0f);
                    divider.Color = new Color(85, 88, 159);
                }
                headerElement.Append(divider);
            }
            Add(headerElement);

            foreach (var category in categories)
            {
                var tab = new CategoryTab(category);
                {
                    tab.OnLeftClick += OnClickTab;
                }
                Add(tab);
            }

            foreach (var category in categories)
            {
                var tab = new CategoryTab(category);
                {
                    tab.OnLeftClick += OnClickTab;
                }
                Add(tab);
            }
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

    protected override void DrawChildren(SpriteBatch spriteBatch)
    {
        AssetReferences.Assets.Shaders.UI.SlightListFade.Asset.Wait();

        using var rtLease = ScreenspaceTargetPool.Shared.Rent(
            Main.instance.GraphicsDevice,
            RenderTargetDescriptor.DefaultPreserveContents
        );

        spriteBatch.End(out var ss);

        using (rtLease.Scope(preserveContents: true, clearColor: Color.Transparent))
        {
            spriteBatch.Begin(ss);
            base.DrawChildren(spriteBatch);
            spriteBatch.End();
        }

        spriteBatch.Begin(ss with { SortMode = SpriteSortMode.Immediate });

        var fadeShader = AssetReferences.Assets.Shaders.UI.SlightListFade.CreateFadeShader();
        fadeShader.Parameters.uPanelDimensions = new Vector4(_dimensions.X, _dimensions.Y, _dimensions.Width, _dimensions.Height);
        fadeShader.Parameters.uScreenSize = new Vector2(rtLease.Target.Width, rtLease.Target.Height);
        fadeShader.Apply();

        spriteBatch.Draw(rtLease.Target, _dimensions.Position(), _dimensions.ToRectangle(), Color.White);
        spriteBatch.Restart(ss);
    }

    public class ModHeader : UIAutoScaleTextTextPanel<string>
    {
        public ModHeader(Mod? mod) : base(mod?.DisplayName ?? "Terraria")
        {
            // _backgroundTexture = AssetReferences.Assets.Images.UI.ConfigTabPanel.Asset;
            // _borderTexture = AssetReferences.Assets.Images.UI.ConfigTabPanelOutline.Asset;

            BackgroundColor = Color.Transparent;
            BorderColor = Color.Transparent;

            Width.Set(0f, 1f);
            Height.Set(38f, 0f);

            HAlign = 1f;

            TextOriginX = 0f;

            SetPadding(4f);

            // Makes the text respond to padding.
            UseInnerDimensions = true;

            if (GetModSmallIcon(mod) is { } icon)
            {
                // If an icon is not loaded the width values used are 0.
                icon.Wait();

                float iconMargin = icon.Width();

                PaddingLeft += iconMargin;

                UIImage tabIcon = new(icon)
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

        private static Asset<Texture2D>? GetModSmallIcon(Mod? mod)
        {
            if (mod is null)
            {
                return AssetReferences.Assets.Images.Configuration.ModIcon_Terraria.Asset;
            }
            else if (mod is ModLoaderMod)
            {
                return AssetReferences.Assets.Images.Configuration.ModIcon_ModLoader.Asset;
            }

            return mod.HasAsset("icon_small") ? mod.Assets.Request<Texture2D>("icon_small") : null;
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

            // _backgroundTexture = AssetReferences.Assets.Images.UI.ConfigTabPanel.Asset;
            // _borderTexture = AssetReferences.Assets.Images.UI.ConfigTabPanelOutline.Asset;

            BackgroundColor = UICommon.MainPanelBackground;
            BorderColor = UICommon.DefaultUIBorder;

            this.WithFadedMouseOver();

            Width.Set(0f, 1f);
            Height.Set(38f, 0f);

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

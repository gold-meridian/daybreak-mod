using Daybreak.Common.UI;
using Daybreak.Content.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Daybreak.Common.Features.Configuration;

internal abstract class ConfigState : UIState, IHaveBackButtonCommand
{
    public static Color DescriptionPanelColor { get; } = Color.LightGray * 0.7f;

    public ConfigRepository Repository { get; protected set; }

    public ConfigValue<Mod?> TargetMod { get; protected set; }

    public ConfigCategory? TargetCategory { get; protected set; }

    public IConfigEntry? TargetEntry { get; protected set; }

#region Elements
    protected UIElement? BaseElement { get; set; }

    protected UIPanel? BackPanel { get; set; }

    protected SplitDraggableElement? SplitElement { get; set; }

    protected UIPanel? DescriptionPanel { get; set; }

    protected UIText? DescriptionText { get; set; }

    protected UIPanel? MetadataPanel { get; set; }

    protected UIText? MetadataText { get; set; }

    protected TabList? Tabs { get; set; }

    protected UIScrollbar? TabScrollbar { get; set; }

    protected ConfigList Config { get; set; }

    protected UIScrollbar? ConfigScrollbar { get; set; }

    protected UITextPanel<LocalizedText>? HeaderPanel { get; set; }

    protected UITextPanel<LocalizedText>? BackButton { get; set; }

    protected UITextPanel<LocalizedText>? SaveButton { get; set; }

    protected SearchBar? SearchBar { get; set; }
#endregion

    private readonly Action? exitAction;

    // Not used in favor of exitAction.
    UIState? IHaveBackButtonCommand.PreviousUIState { get; set; } = null;

    protected ConfigState(
        ConfigRepository repository,
        ConfigValue<Mod?> mod,
        ConfigCategoryHandle? category = null,
        ConfigEntryHandle? entry = null,
        Action? onExit = null
    )
    {
        Repository = repository;
        TargetMod = mod;

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

        const float default_min_ratio = 0f;
        const float default_max_ratio = 0.5f;
        const float default_ratio = 1f / 3f;

        BaseElement = new UIElement();
        {
            BaseElement.Width.Set(0f, 0.8f);
            BaseElement.MinWidth.Set(min_panel_width, 0f);
            BaseElement.MaxWidth.Set(max_panel_width, 0f);

            BaseElement.Top.Set(vertical_margin, 0f);
            BaseElement.Height.Set(0f, 1f);

            BaseElement.HAlign = 0.5f;
        }
        Append(BaseElement);

        BackPanel = new UIPanel();
        {
            BackPanel.Width = StyleDimension.Fill;
            BackPanel.Height.Set(-vertical_margin * 1.75f, 1f);
            BackPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
        }
        BaseElement.Append(BackPanel);

        SplitElement = new SplitDraggableElement(
            default_min_ratio,
            default_max_ratio,
            default_ratio
        );
        {
            SplitElement.Height.Set(0f, 1f);
            SplitElement.Width.Set(0f, 1f);
        }
        BackPanel.Append(SplitElement);

        DescriptionPanel = new UIPanel();
        {
            DescriptionPanel.Width.Set(0f, 1f);
            DescriptionPanel.Height.Set(64, 0f);
            DescriptionPanel.VAlign = 1f;
            DescriptionPanel._backgroundTexture = AssetReferences.Assets.Images.UI.ConfigDescriptionPanel.Asset;
            DescriptionPanel.BackgroundColor = DescriptionPanelColor;
            DescriptionPanel.BorderColor = Color.Transparent;

            DescriptionText = new UIText(
                Mods.Daybreak.UI.DefaultConfigDescription.GetText()
            );
            {
                DescriptionText.Width.Set(0f, 1f);
                DescriptionText.Height.Set(0f, 1f);
                DescriptionText.IsWrapped = true;
            }
            DescriptionPanel.Append(DescriptionText);
        }
        SplitElement.RightElement.Append(DescriptionPanel);

        MetadataPanel = new UIPanel();
        {
            MetadataPanel.Width.Set(0f, 1f);
            MetadataPanel.MinWidth.Set(110f, 0f);
            MetadataPanel.Height.Set(64, 0f);
            MetadataPanel.VAlign = 1f;
            MetadataPanel._backgroundTexture = AssetReferences.Assets.Images.UI.ConfigDescriptionPanel.Asset;
            MetadataPanel.BackgroundColor = DescriptionPanelColor;
            MetadataPanel.BorderColor = Color.Transparent;

            MetadataText = new UIText(
                Mods.Daybreak.UI.DefaultMetadataDescription.GetText()
            );
            {
                MetadataText.Width.Set(0f, 1f);
                MetadataText.Height.Set(0f, 1f);
                MetadataText.IsWrapped = true;
                MetadataText.TextColor = Color.DarkGray;
            }
            MetadataPanel.Append(MetadataText);
        }
        SplitElement.LeftElement.Append(MetadataPanel);

        SearchBar = new SearchBar(Mods.Daybreak.UI.ConfigSearchHint.GetText());
        {
            SearchBar.MinWidth.Set(110f, 0f);
        }
        SplitElement.LeftElement.Append(SearchBar);

        var searchBarOffset = SearchBar.Height.Pixels + 6f;

        // It overflows by this amount of pixels vertically on the top and
        // bottom, for some reason.
        const float scrollbar_vertical_adjust = 6f;

        TabScrollbar = new UIScrollbar();
        {
            TabScrollbar.Height.Set(-64f - BackPanel.PaddingTop - searchBarOffset - (scrollbar_vertical_adjust * 2f), 1f);
            TabScrollbar.Top.Set(searchBarOffset + scrollbar_vertical_adjust, 0f);
            TabScrollbar.HAlign = 1f;
        }
        SplitElement.LeftElement.Append(TabScrollbar);

        // Panel tabs
        {
            var container = new UIElement();
            {
                container.Width.Set(-TabScrollbar.Width.Pixels - 4f, 1f);
                container.Height.Set(-64f - BackPanel.PaddingTop - searchBarOffset, 1f);
                container.Top.Set(searchBarOffset, 0f);
            }
            SplitElement.LeftElement.Append(container);

            Tabs = new TabList(Repository, TargetMod, TargetCategory, TargetEntry);
            {
                Tabs.Width.Set(0f, 1f);
                Tabs.Height.Set(0f, 1f);
                Tabs.HAlign = 1f;
                Tabs.OnCategorySelected += OnCategorySelected_UpdateConfigList;
                Tabs.OnModSelected += OnModSelected_UpdateConfigList;
                Tabs.SetScrollbar(TabScrollbar);
            }
            container.Append(Tabs);
        }

        ConfigScrollbar = new UIScrollbar();
        {
            ConfigScrollbar.Height.Set(-64f - BackPanel.PaddingTop - (scrollbar_vertical_adjust * 2f), 1f);
            ConfigScrollbar.Top.Set(scrollbar_vertical_adjust, 0f);
            ConfigScrollbar.HAlign = 1f;
        }
        SplitElement.RightElement.Append(ConfigScrollbar);

        // Panel config
        {
            var container = new UIElement();
            {
                container.Width.Set(-TabScrollbar.Width.Pixels - 4f, 1f);
                container.Height.Set(-64f - BackPanel.PaddingTop, 1f);
                container.Top.Set(0f, 0f);
                container.Left.Set(-TabScrollbar.Width.Pixels - 4f, 0f);
                container.HAlign = 1f;
            }
            SplitElement.RightElement.Append(container);

            Config = new ConfigList(Repository, TargetMod, TargetCategory, TargetEntry);
            {
                Config.Width.Set(0f, 1f);
                Config.Height.Set(0f, 1f);
                Config.HAlign = 1f;
                Config.SetScrollbar(ConfigScrollbar);
            }
            container.Append(Config);
        }

        HeaderPanel = new UITextPanel<LocalizedText>(
            Repository.DisplayName,
            textScale: 0.8f,
            large: true
        );
        {
            HeaderPanel.SetPadding(13f);
            HeaderPanel.Top.Set(-44f, 0f);
            HeaderPanel.HAlign = 0.5f;
            HeaderPanel.BackgroundColor = UICommon.DefaultUIBlue;
        }
        BackPanel.Append(HeaderPanel);

        BackButton = new UITextPanel<LocalizedText>(
            Language.GetText("UI.Back"),
            textScale: 0.7f,
            large: true
        );
        {
            BackButton.Width.Set(-8f, 0.5f);
            BackButton.Height.Set(50f, 0f);
            BackButton.Top.Set(-vertical_margin - 50, 0f);
            BackButton.VAlign = 1f;
            BackButton.HAlign = 0f;
            BackButton.OnLeftMouseDown += GoBackClick;
            BackButton.WithFadedMouseOver();
        }
        BaseElement.Append(BackButton);

        SaveButton = new UITextPanel<LocalizedText>(
            Language.GetText("tModLoader.ModConfigSaveConfig"),
            textScale: 0.7f,
            large: true
        );
        {
            SaveButton.Width.Set(-8f, 0.5f);
            SaveButton.Height.Set(50, 0f);
            SaveButton.Top.Set(-vertical_margin - 50, 0f);
            SaveButton.VAlign = 1f;
            SaveButton.HAlign = 1f;
            SaveButton.OnLeftMouseDown += GoBackClick;
            SaveButton.WithFadedMouseOver();
        }
        BaseElement.Append(SaveButton);

        return;

        void OnCategorySelected_UpdateConfigList(ConfigCategory? category)
        {
            Config.Mod = ConfigValue<Mod?>.Unset();
            Config.Category = category;
        }

        void OnModSelected_UpdateConfigList(ConfigValue<Mod?> mod)
        {
            Config.Category = null;
            Config.Mod = mod;
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        UILinkPointNavigator.Shortcuts.BackButtonCommand = 7;
    }

    private void ExitState()
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
    /*
    private void DescriptionMouseOver(UIMouseEvent evt, UIElement listeningElement)
    {
        // TODO
    }

    private void DescriptionMouseOut(UIMouseEvent evt, UIElement listeningElement)
    {
        DescriptionText?.SetText(Mods.Daybreak.UI.DefaultConfigDescription.GetText());
    }
    */

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

internal sealed class TabList : FadedList
{
    public event Action<ConfigCategory?>? OnCategorySelected;

    public event Action<ConfigValue<Mod?>>? OnModSelected;

    public ConfigCategory? Category
    {
        get;

        set
        {
            if (field is not null)
            {
                this[field.Handle.Mod]?.IsACategorySelected = false;
            }

            field = value;

            if (field is not null)
            {
                OpenCategory(field);
                Mod = ConfigValue<Mod?>.Unset();
            }
        }
    }

    public ConfigValue<Mod?> Mod
    {
        get;

        set
        {
            if (field.IsSet)
            {
                this[field.Value]?.IsHeaderSelected = false;
            }

            field = value;

            if (field.IsSet)
            {
                Category = null;
                OpenMod(field);
            }
        }
    }

    private ModGroup? this[Mod? mod]
    {
        get
        {
            var elem = _items.FirstOrDefault(m => m is ModGroup c && c.Mod == mod);

            return elem as ModGroup;
        }
    }

    public TabList(
        ConfigRepository repository,
        ConfigValue<Mod?> targetMod,
        ConfigCategory? targetCategory,
        IConfigEntry? targetEntry
    )
    {
        ListPadding = 6f;

        _innerList.MinWidth.Set(150f, 0f);

        // Extra padding at the start of the list to avoid the last item being
        // engulfed in the fade.
        var startPadElement = new UIElement();
        {
            startPadElement.Height.Set(4f, 0f);
        }
        Add(startPadElement);

        ManualSortMethod = _ => { };

        var categoriesByMod = new Dictionary<ConfigValue<Mod?>, List<ConfigCategory>>
        {
            [ConfigValue<Mod?>.Set(null)] = [],
            [ConfigValue<Mod?>.Set(ModContent.GetInstance<ModLoaderMod>())] = [],
        };

        foreach (var category in repository.Categories)
        {
            if (!categoriesByMod.TryGetValue(ConfigValue<Mod?>.Set(category.Handle.Mod), out var categories))
            {
                categoriesByMod[ConfigValue<Mod?>.Set(category.Handle.Mod)] = categories = [];
            }

            categories.Add(category);
        }

        foreach (var (mod, categories) in categoriesByMod)
        {
            var modGroup = new ModGroup(mod.Value, categories, targetCategory);
            {
                modGroup.OnCategorySelected += ModGroup_OnCategorySelected;
                modGroup.OnModSelected += ModGroup_OnModSelected;
            }
            Add(modGroup);
        }

        // Extra padding at the bottom of the list to avoid the last item being
        // engulfed in the fade.
        var endPadElement = new UIElement();
        {
            endPadElement.Height.Set(24f, 0f);
        }
        Add(endPadElement);

        var categoryToGoTo = targetCategory;
        if (targetEntry is not null)
        {
            if (targetCategory is null || !targetEntry.Categories.Contains(targetCategory))
            {
                categoryToGoTo = repository.GetCategory(targetEntry.MainCategory);
            }
        }

        if (categoryToGoTo is not null)
        {
            Category = categoryToGoTo;
        }
        else
        {
            Mod = targetMod;
        }
    }

    private void ModGroup_OnCategorySelected(ConfigCategory category)
    {
        if (Category == category)
        {
            return;
        }

        Category = category;
        OnCategorySelected?.Invoke(Category);
        SoundEngine.PlaySound(SoundID.MenuOpen);
    }

    private void ModGroup_OnModSelected(ConfigValue<Mod?> mod)
    {
        if (Mod == mod)
        {
            return;
        }

        Mod = mod;
        OnModSelected?.Invoke(Mod);
        SoundEngine.PlaySound(SoundID.MenuOpen);
    }

    public override void OnActivate()
    {
        OnCategorySelected?.Invoke(Category);
        OpenCategory(Category, true);
    }

    private void OpenCategory(ConfigCategory? category, bool scroll = false)
    {
        if (category is null)
        {
            return;
        }

        var elem = _items.FirstOrDefault(m => m is ModGroup g && g.Mod == category.Handle.Mod);

        if (elem is not ModGroup group)
        {
            return;
        }

        group.IsACategorySelected = true;

        group[category]?.Selected = true;

        if (scroll)
        {
            _scrollbar.ViewPosition = elem.Top.Pixels;
        }
    }

    private void OpenMod(ConfigValue<Mod?> mod, bool scroll = false)
    {
        if (!mod.IsSet)
        {
            return;
        }

        var elem = _items.FirstOrDefault(m => m is ModGroup g && g.Mod == mod.Value);

        if (elem is not ModGroup group)
        {
            return;
        }

        group.IsACategorySelected = false;
        group.IsHeaderSelected = true;

        if (scroll)
        {
            _scrollbar.ViewPosition = elem.Top.Pixels;
        }
    }

    private abstract class TextTab<T> : UIAutoScaleTextTextPanel<T>
    {
        protected TextTab(T text, Asset<Texture2D>? icon = null, bool forceIconPadding = false, bool iconLeft = false) : base(text)
        {
            BackgroundColor = Color.Transparent;
            BorderColor = Color.Transparent;

            Width.Set(0f, 1f);

            TextOriginX = 0f;

            SetPadding(0f);

            // Makes the text respond to padding.
            UseInnerDimensions = true;

            // Icon
            {
                if (icon is not null)
                {
                    if (iconLeft)
                    {
                        PaddingLeft += 30f;
                    }
                    else
                    {
                        PaddingRight += 30f;
                    }

                    var tabImage = new Icon();
                    {
                        tabImage.VAlign = 0.5f;
                        tabImage.HAlign = iconLeft ? 0f : 1f;
                        if (iconLeft)
                        {
                            tabImage.MarginLeft = -PaddingLeft;
                        }
                        else
                        {
                            tabImage.MarginRight = -PaddingRight;
                        }
                        tabImage.MarginTop = -2f;
                        tabImage.Width.Set(30f, 0f);
                        tabImage.Height.Set(30f, 0f);
                        tabImage.Texture = icon;
                    }
                    Append(tabImage);
                }
            }

            Recalculate();
        }

        public sealed override void Recalculate()
        {
            base.Recalculate();
        }

        protected sealed class Icon : UIElement
        {
            public float Rotation { get; set; }

            public Asset<Texture2D>? Texture { get; set; }

            protected override void DrawSelf(SpriteBatch spriteBatch)
            {
                base.DrawSelf(spriteBatch);

                if (Texture is null)
                {
                    return;
                }

                var dims = this.Dimensions;
                var position = dims.Center();
                var scale = dims.Size() / Texture.Value.Size();

                var origin = Texture.Value.Size() * 0.5f;

                spriteBatch.Draw(
                    Texture.Value,
                    position,
                    null,
                    Color.White,
                    Rotation,
                    origin,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }

    private sealed class ModGroup : TextTab<string>
    {
        private const float tab_height = 32f;

        public readonly Mod? Mod;

        public event Action<ConfigCategory>? OnCategorySelected;

        public event Action<ConfigValue<Mod?>>? OnModSelected;

        public bool IsHeaderSelected
        {
            get;

            set
            {
                field = value;

                if (value)
                {
                    foreach (var elem in list)
                    {
                        if (elem is CategoryTab tab)
                        {
                            tab.Selected = false;
                        }
                    }

                    IsACategorySelected = false;
                }
            }
        }

        public bool IsACategorySelected
        {
            get;

            set
            {
                field = value;

                if (value)
                {
                    openProgress = 12;
                    Open = true;
                    UpdateHeight();
                    return;
                }

                foreach (var elem in list)
                {
                    if (elem is CategoryTab tab)
                    {
                        tab.Selected = false;
                    }
                }
            }
        }

        public bool IsAtAllSelected => IsHeaderSelected || IsACategorySelected;

        public CategoryTab? this[ConfigCategory category]
        {
            get
            {
                var elem = list._items.FirstOrDefault(m => m is CategoryTab c && c.Category == category);

                return elem as CategoryTab;
            }
        }

        public bool Open { get; private set; }

        private bool hoveringHeader;

        private int hoverProgress;
        private int selectProgress;
        private int openProgress;

        private readonly UIHorizontalSeparator dimDivider;
        private readonly UIHorizontalSeparator highlightDivider;
        private readonly UIHorizontalSeparator selectDivider;
        private readonly Icon dropdownIcon;
        private readonly UIElement listContainer;
        private readonly UIList list;

        public ModGroup(Mod? mod, IEnumerable<ConfigCategory> categories, ConfigCategory? targetCategory) : base(mod?.DisplayName ?? "Terraria", GetModSmallIcon(mod))
        {
            Mod = mod;

            Width = StyleDimension.Fill;
            MinHeight.Set(tab_height, 0f);

            HAlign = 1f;

            PaddingLeft += 12f;

            dropdownIcon = new Icon();
            {
                dropdownIcon.VAlign = 0.5f;
                dropdownIcon.MarginLeft = -PaddingLeft;
                dropdownIcon.MarginTop = 2f;
                dropdownIcon.Width.Set(12f, 0f);
                dropdownIcon.Height.Set(12f, 0f);
                dropdownIcon.Texture = AssetReferences.Assets.Images.UI.Dropdown.Asset;
                dropdownIcon.OnLeftClick += (_, _) =>
                {
                    Open = !Open;
                    SoundEngine.PlaySound(Open ? SoundID.MenuOpen : SoundID.MenuClose);
                };
            }
            Append(dropdownIcon);

            // Dividers
            {
                var dividerContainer = new UIElement();
                {
                    dividerContainer.MarginLeft = -PaddingLeft;
                    dividerContainer.MarginRight = -PaddingRight;

                    dividerContainer.Width.Set(PaddingLeft + PaddingRight, 1f);
                    dividerContainer.MaxWidth.Set(PaddingLeft + PaddingRight, 1f);

                    dividerContainer.Height.Set(2f, 0f);

                    dividerContainer.VAlign = 1f;
                }
                Append(dividerContainer);

                dimDivider = new UIHorizontalSeparator();
                {
                    dimDivider.Width = StyleDimension.Fill;
                    dimDivider.Height.Set(2f, 0f);
                    dimDivider.Color = new Color(85, 88, 159) * 0.5f;
                }
                dividerContainer.Append(dimDivider);

                highlightDivider = new UIHorizontalSeparator();
                {
                    highlightDivider.Width = StyleDimension.Empty;
                    highlightDivider.Height.Set(2f, 0f);
                    highlightDivider.Color = new Color(85, 88, 159) * 0.75f;
                }
                dividerContainer.Append(highlightDivider);

                selectDivider = new UIHorizontalSeparator();
                {
                    selectDivider.Width = StyleDimension.Empty;
                    selectDivider.Height.Set(2f, 0f);
                    selectDivider.Color = Main.OurFavoriteColor;
                }
                dividerContainer.Append(selectDivider);
            }

            listContainer = new UIElement();
            {
                const float list_margin = 0f;

                listContainer.MarginLeft = -PaddingLeft + list_margin;
                listContainer.MarginRight = -PaddingRight;

                listContainer.Top.Set(tab_height, 0f);

                listContainer.MinWidth.Set(PaddingLeft + PaddingRight - list_margin, 1f);

                listContainer.OverflowHidden = true;
            }
            Append(listContainer);

            list = [];
            {
                list.Width.Set(0f, 1f);
                list.Height.Set(0f, 1f);
                list.VAlign = 1f;
                list.ListPadding = 4f;

                var topPaddingElement = new UIElement();
                {
                    topPaddingElement.Height.Set(0f, 0f);
                }
                list.Add(topPaddingElement);

                foreach (var category in categories)
                {
                    var tab = new CategoryTab(category);
                    {
                        tab.Height.Set(28, 0f);

                        tab.OnLeftClick += OnClickTab;

                        if (category == targetCategory)
                        {
                            IsACategorySelected = true;
                            tab.Selected = true;
                        }
                    }
                    list.Add(tab);
                }

                // Add some extra padding to the list to help visually separate
                // the UI.
                var paddingElement = new UIElement();
                {
                    paddingElement.Height.Set(6f, 0f);
                }
                list.Add(paddingElement);
            }
            listContainer.Append(list);

            list.MinHeight.Set(list.GetTotalHeight(), 0f);

            return;

            void OnClickTab(UIMouseEvent evt, UIElement listeningElement)
            {
                if (listeningElement is not CategoryTab tab)
                {
                    return;
                }

                IsACategorySelected = true;
                tab.Selected = true;

                OnCategorySelected?.Invoke(tab.Category);
            }
        }

        private void DropdownIcon_OnLeftClick(UIMouseEvent evt, UIElement listeningElement)
        {
            throw new NotImplementedException();
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

            return mod.RequestAssetIfExists<Texture2D>("icon_small", out var iconSmall) ? iconSmall : null;
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            base.LeftClick(evt);

            if (IsHeaderSelected ||!hoveringHeader)
            {
                return;
            }

            SoundEngine.PlaySound(SoundID.MenuOpen);

            IsHeaderSelected = true;

            OnModSelected?.Invoke(ConfigValue<Mod?>.Set(Mod));
        }

        public override void LeftDoubleClick(UIMouseEvent evt)
        {
            base.LeftDoubleClick(evt);

            if ( /*IsAtAllSelected ||*/ !hoveringHeader)
            {
                return;
            }

            Open = !Open;

            SoundEngine.PlaySound(Open ? SoundID.MenuOpen : SoundID.MenuClose);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var mousePosition = UserInterface.ActiveInstance.MousePosition;

            var wasHoveringHeader = hoveringHeader;
            hoveringHeader = IsMouseHovering && !dropdownIcon.IsMouseHovering && this.Dimensions.Contains(mousePosition.ToPoint()) && mousePosition.Y < this.InnerDimensions.Bottom;

            if (!IsHeaderSelected && hoveringHeader && !wasHoveringHeader)
            {
                SoundEngine.PlaySound(in SoundID.MenuTick);
            }

            const int hover_frames = 8;
            {
                hoverProgress = MathHelper.Clamp(hoverProgress + (hoveringHeader || IsAtAllSelected).ToDirectionInt(), 0, hover_frames);
                selectProgress = MathHelper.Clamp(selectProgress + IsHeaderSelected.ToDirectionInt(), 0, hover_frames);

                highlightDivider.Width.Set(0f, Ease(hoverProgress / (float)hover_frames));
                selectDivider.Width.Set(0f, Ease(selectProgress / (float)hover_frames));
            }

            UpdateHeight();
        }

        // Separated because this needs to be immediately enforced in Selected
        // when it's explicitly set to be fully opened (otherwise it lags
        // behind a frame).
        private void UpdateHeight()
        {
            const int open_frames = 12;
            {
                openProgress = MathHelper.Clamp(openProgress + Open.ToDirectionInt(), 0, open_frames);

                var eased = Open ? Ease(openProgress / (float)open_frames) : EaseClose(openProgress / (float)open_frames);

                dropdownIcon.Rotation = MathHelper.PiOver2 * eased;

                var height = list.GetTotalHeight() * eased;

                listContainer.MinHeight.Set(height, 0f);
                listContainer.MaxHeight.Set(height, 0f);

                Height.Set(tab_height + height, 0f);

                PaddingBottom = height;
            }
        }

        private static float Ease(float x)
        {
            // return x < 0.5f ? 4 * x * x * x : 1 - MathF.Pow(-2 * x + 2, 3) / 2;
            return MathF.Sqrt(1 - MathF.Pow(x - 1, 2));
        }

        private static float EaseClose(float x)
        {
            return x * x;
        }

        private static float InverseEase(float x)
        {
            return 1f - Ease(1f - x);
        }
    }

    private sealed class CategoryTab : TextTab<LocalizedText>
    {
        private static Color SelectedColor => Color.White;

        private static Color HoveredColor => (Color.White * 0.95f) with { A = 255 };

        private static Color UnselectedColor => (Color.White * 0.75f) with { A = 255 };

        public bool Selected
        {
            get;

            set
            {
                field = value;

                if (value)
                {
                    TextTargetColor = TextColor = SelectedColor;
                }
                else
                {
                    TextTargetColor = UnselectedColor;
                }
            }
        }

        private Color TextTargetColor
        {
            get;

            set
            {
                field = value;
                targetColorLerp = 0;
                oldColor = TextColor;
            }
        }

        private Color oldColor;
        private int targetColorLerp;
        private int hoverProgress;
        private int selectProgress;

        private readonly UIHorizontalSeparator dimDivider;
        private readonly UIHorizontalSeparator highlightDivider;
        private readonly UIHorizontalSeparator selectDivider;

        public ConfigCategory Category { get; }

        public CategoryTab(ConfigCategory category) : base(category.DisplayName, icon: AssetReferences.Assets.Images.Configuration.ModIcon_Terraria.Asset, forceIconPadding: true, iconLeft: true)
        {
            Category = category;

            TextTargetColor = TextColor = UnselectedColor;

            var dividerContainer = new UIElement();
            {
                dividerContainer.MarginLeft = -PaddingLeft;
                dividerContainer.MarginRight = -PaddingRight;

                dividerContainer.Left.Set(30f, 0f);
                dividerContainer.Width.Set(PaddingLeft + PaddingRight - 30f, 1f);
                dividerContainer.MaxWidth.Set(PaddingLeft + PaddingRight, 1f);

                dividerContainer.Height.Set(2f, 0f);

                dividerContainer.VAlign = 1f;
            }
            Append(dividerContainer);

            dimDivider = new UIHorizontalSeparator();
            {
                dimDivider.Width = StyleDimension.Fill;
                dimDivider.Height.Set(2f, 0f);
                dimDivider.Color = new Color(85, 88, 159) * 0.5f;
            }
            dividerContainer.Append(dimDivider);

            highlightDivider = new UIHorizontalSeparator();
            {
                highlightDivider.Width = StyleDimension.Empty;
                highlightDivider.Height.Set(2f, 0f);
                highlightDivider.Color = new Color(85, 88, 159) * 0.75f;
            }
            dividerContainer.Append(highlightDivider);

            selectDivider = new UIHorizontalSeparator();
            {
                selectDivider.Width = StyleDimension.Empty;
                selectDivider.Height.Set(2f, 0f);
                selectDivider.Color = Main.OurFavoriteColor;
            }
            dividerContainer.Append(selectDivider);
        }

        public override void Update(GameTime gameTime)
        {
            const int lerp_frames = 6;
            {
                targetColorLerp++;

                if (targetColorLerp > lerp_frames)
                {
                    targetColorLerp = lerp_frames;
                }

                TextColor = Color.Lerp(oldColor, TextTargetColor, targetColorLerp / (float)lerp_frames);
            }

            const int hover_frames = 8;
            {
                hoverProgress = MathHelper.Clamp(hoverProgress + (IsMouseHovering || Selected).ToDirectionInt(), 0, hover_frames);
                selectProgress = MathHelper.Clamp(selectProgress + Selected.ToDirectionInt(), 0, hover_frames);

                highlightDivider.Width.Set(0f, Ease(hoverProgress / (float)hover_frames));
                selectDivider.Width.Set(0f, Ease(selectProgress / (float)hover_frames));
            }

            return;

            static float Ease(float x)
            {
                return 1 - MathF.Pow(1 - x, 5);
            }
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            if (Selected)
            {
                return;
            }

            SoundEngine.PlaySound(in SoundID.MenuTick);
            TextTargetColor = HoveredColor;
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);

            if (Selected)
            {
                return;
            }

            TextTargetColor = UnselectedColor;
        }
    }
}

internal sealed class ConfigList : FadedList
{
    public ConfigCategory? Category
    {
        get;

        set
        {
            Clear();

            AddCategoryElements(value, null);

            field = value;
        }
    }

    public ConfigValue<Mod?> Mod
    {
        get;

        set
        {
            Clear();

            AddModElements(value);

            field = value;
        }
    }

    public ConfigList(
        ConfigRepository repository,
        ConfigValue<Mod?> targetMod,
        ConfigCategory? targetCategory,
        IConfigEntry? targetEntry
    )
    {
        ListPadding = 6f;

        _innerList.MinWidth.Set(300f, 0f);

        ManualSortMethod = _ => { };

        // var entriesByCategory = new Dictionary<ConfigCategory, List<IConfigEntry>>();

        var categoryToOpen = targetCategory;
        if (targetEntry is not null)
        {
            if (targetCategory is null || !targetEntry.Categories.Contains(targetCategory))
            {
                categoryToOpen = repository.GetCategory(targetEntry.MainCategory);
            }
        }

        if (categoryToOpen is not null)
        {
            Category = categoryToOpen;
            AddCategoryElements(Category, targetEntry);
        }
        else
        {
            Mod = targetMod;
            AddModElements(Mod);
        }
    }

    private void AddModElements(ConfigValue<Mod?> mod)
    {
        if (!mod.IsSet)
        {
            return;
        }
    }

    private void AddCategoryElements(
        ConfigCategory? category,
        IConfigEntry? targetEntry
    )
    {
        if (category is null)
        {
            return;
        }

        // Extra padding at the start of the list to avoid the last item being
        // engulfed in the fade.
        var startPadElement = new UIElement();
        {
            startPadElement.Height.Set(4f, 0f);
        }
        Add(startPadElement);

        var repository = category.Handle.Repository;

        foreach (var entry in repository.Entries)
        {
            if (!entry.Categories.Contains(category))
            {
                continue;
            }

            var configElement = new ConfigElement(entry);
            {
                // TODO: Highlight effect for the target entry.
            }
            Add(configElement);
        }

        // Extra padding at the bottom of the list to avoid the last item being
        // engulfed in the fade.
        var endPadElement = new UIElement();
        {
            endPadElement.Height.Set(24f, 0f);
        }
        Add(endPadElement);
    }
}

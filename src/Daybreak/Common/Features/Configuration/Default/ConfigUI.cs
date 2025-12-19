using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
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

    public ConfigCategory? TargetCategory { get; protected set; }

    public IConfigEntry? TargetEntry { get; protected set; }

    protected UIElement? baseElement;
    protected UIPanel? backPanel;
    protected SplitDraggableElement? splitElement;
    protected UIPanel? descriptionPanel;
    protected UIText? descriptionText;
    protected UIPanel? metadataPanel;
    protected UIText? metadataText;
    protected TabList? tabs;
    protected UIScrollbar? tabScrollbar;
    protected UIScrollbar? configScrollbar;
    protected UITextPanel<LocalizedText>? headerPanel;
    protected UITextPanel<LocalizedText>? backButton;
    protected UITextPanel<LocalizedText>? saveButton;
    protected SearchBar? searchBar;

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

        const float default_min_ratio = 0f;
        const float default_max_ratio = 0.5f;
        const float default_ratio = 1f / 3f;

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

        splitElement = new SplitDraggableElement(default_min_ratio, default_max_ratio, default_ratio);
        {
            splitElement.Height.Set(0f, 1f);
            splitElement.Width.Set(0f, 1f);
        }
        backPanel.Append(splitElement);

        descriptionPanel = new UIPanel();
        {
            descriptionPanel.Width.Set(0f, 1f);
            descriptionPanel.Height.Set(64, 0f);
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
        splitElement.Right.Append(descriptionPanel);

        metadataPanel = new UIPanel();
        {
            metadataPanel.Width.Set(0f, 1f);
            metadataPanel.MinWidth.Set(110f, 0f);
            metadataPanel.Height.Set(64, 0f);
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
        splitElement.Left.Append(metadataPanel);

        searchBar = new(Language.GetText("UI.PlayerNameSlot"));
        {
            searchBar.MinWidth.Set(110f, 0f);
        }
        splitElement.Left.Append(searchBar);

        float searchBarOffset = searchBar.Height.Pixels + 6f;

        tabScrollbar = new UIScrollbar();
        {
            // It overflows by this amount of pixels vertically on the top and
            // bottom, for some reason.
            const float vertical_adjust = 6f;

            tabScrollbar.Height.Set(-64f - backPanel.PaddingTop - searchBarOffset - (vertical_adjust * 2f), 1f);
            tabScrollbar.Top.Set(searchBarOffset + vertical_adjust, 0f);
            tabScrollbar.HAlign = 1f;
        }
        splitElement.Left.Append(tabScrollbar);

        // Panel tabs
        {
            var container = new UIElement();
            {
                container.Width.Set(-tabScrollbar.Width.Pixels - 4f, 1f);
                container.Height.Set(-64f - backPanel.PaddingTop - searchBarOffset, 1f);
                container.Top.Set(searchBarOffset, 0f);
            }
            splitElement.Left.Append(container);

            tabs = new TabList(Repository, TargetCategory, TargetEntry);
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

internal class TabList : FadedList
{
    public event Action<ConfigCategory>? OnCategorySelected;

    public ConfigCategory Category
    {
        get;
        set
        {
            if (Category is not null)
            {
                this[Category.Handle.Mod]?.Selected = false;
            }

            OpenCategory(value);

            field = value;
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
        ConfigCategory? targetCategory,
        IConfigEntry? targetEntry
    )
    {
        ListPadding = 16f;

        _innerList.MinWidth.Set(150f, 0f);

        // Extra padding at the start of the list to avoid the last item being
        // engulfed in the fade.
        var startPadElement = new UIElement();
        {
            startPadElement.Height.Set(4f, 0f);
        }
        Add(startPadElement);

        ManualSortMethod = _ => { };

        var categoriesByMod = new Dictionary<ValueTuple<Mod?>, List<ConfigCategory>>
        {
            [new ValueTuple<Mod?>(null)] = [],
            [new ValueTuple<Mod?>(ModContent.GetInstance<ModLoaderMod>())] = [],
        };

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
            var modGroup = new ModGroup(mod.Item1, categories, targetCategory);
            {
                modGroup.OnCategorySelected += ModGroup_OnCategorySelected;
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

        ConfigCategory categoryToGoTo;
        if (targetEntry is not null)
        {
            if (targetCategory is not null && targetEntry.Categories.Contains(targetCategory))
            {
                categoryToGoTo = targetCategory;
            }
            else
            {
                categoryToGoTo = repository.GetCategory(targetEntry.MainCategory);
            }
        }
        else
        {
            categoryToGoTo = targetCategory ?? categoriesByMod.Values.First().First();
        }

        Category = categoryToGoTo;
    }

    private void ModGroup_OnCategorySelected(ConfigCategory category)
    {
        if (Category != category)
        {
            Category = category;
            OnCategorySelected?.Invoke(Category);
            SoundEngine.PlaySound(SoundID.MenuOpen);
        }
    }

    public override void OnActivate()
    {
        OpenCategory(Category, true);
    }

    public void OpenCategory(ConfigCategory category, bool scroll = false)
    {
        var elem = _items.FirstOrDefault(m => m is ModGroup group && group.Mod == category.Handle.Mod);

        if (elem is null || elem is not ModGroup group)
        {
            return;
        }

        group.Selected = true;

        group[category]?.Selected = true;

        if (scroll)
        {
            _scrollbar.ViewPosition = elem.Top.Pixels;
        }
    }

    protected abstract class TextTab<T> : UIAutoScaleTextTextPanel<T>
    {
        public TextTab(T text, Asset<Texture2D>? icon = null) : base(text, 1f, false)
        {
            BackgroundColor = Color.Transparent;
            BorderColor = Color.Transparent;

            Width.Set(0f, 1f);

            TextOriginX = 0f;

            SetPadding(0f);

            // Makes the text respond to padding.
            UseInnerDimensions = true;

            bool useIcon = icon is not null;

            // Icon
            {
                if (useIcon)
                {
                    PaddingRight += 30f;

                    var tabImage = new Icon();
                    {
                        tabImage.VAlign = 0.5f;
                        tabImage.HAlign = 1f;
                        tabImage.MarginRight = -PaddingRight;
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

    protected class ModGroup : TextTab<string>
    {
        private const float tab_height = 32f;

        public readonly Mod? Mod;

        public event Action<ConfigCategory>? OnCategorySelected;

        public bool Selected
        {
            get;
            set
            {
                field = value;

                if (value)
                {
                    openProgress = 12;
                    Open = true;
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

        public CategoryTab? this[ConfigCategory category]
        {
            get
            {
                var elem = list._items.FirstOrDefault(m => m is CategoryTab c && c.Category == category);

                return elem as CategoryTab;
            }
        }

        public bool Open { get; private set; }

        protected bool hoveringHeader;

        private int hoverProgress;
        private int openProgress;

        private readonly UIHorizontalSeparator dimDivider;
        private readonly UIHorizontalSeparator highlightDivider;

        protected readonly Icon dropdownIcon;

        protected readonly UIElement listContainer;
        protected readonly UIList list;

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
            }

            listContainer = new UIElement();
            {
                const float list_margin = 15f;

                listContainer.MarginLeft = -PaddingLeft;
                listContainer.MarginRight = -PaddingRight;

                listContainer.Left.Set(list_margin, 0f);
                listContainer.Top.Set(tab_height, 0f);

                listContainer.Width.Set(list_margin + PaddingLeft + PaddingRight, 1f);
                listContainer.MaxWidth.Set(list_margin + PaddingLeft + PaddingRight, 1f);

                listContainer.OverflowHidden = true;
            }
            Append(listContainer);

            list = [];
            {
                list.Width.Set(0f, 1f);
                list.Height.Set(0f, 1f);

                list.VAlign = 1f;

                list.ListPadding = 4f;

                foreach (var category in categories)
                {
                    var tab = new CategoryTab(category);
                    {
                        tab.Height.Set(28, 0f);

                        tab.OnLeftClick += OnClickTab;

                        if (category == targetCategory)
                        {
                            Selected = true;
                            tab.Selected = true;
                        }
                    }
                    list.Add(tab);
                }
            }
            listContainer.Append(list);

            list.MinHeight.Set(list.GetTotalHeight(), 0f);

            void OnClickTab(UIMouseEvent evt, UIElement listeningElement)
            {
                if (listeningElement is CategoryTab tab)
                {
                    Selected = true;
                    tab.Selected = true;

                    OnCategorySelected?.Invoke(tab.Category);
                }
            }
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

            if (mod.RequestAssetIfExists<Texture2D>("icon_small", out var iconSmall))
            {
                return iconSmall;
            }

            return null;
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);

            if (!Selected && hoveringHeader)
            {
                Open = !Open;

                SoundEngine.PlaySound(Open ? SoundID.MenuOpen : SoundID.MenuClose);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var mousePosition = UserInterface.ActiveInstance.MousePosition;

            bool wasHoveringHeader = hoveringHeader;
            hoveringHeader = IsMouseHovering && this.Dimensions.Contains(mousePosition.ToPoint()) && mousePosition.Y < this.InnerDimensions.Bottom;

            if (!Selected && hoveringHeader && !wasHoveringHeader)
            {
                SoundEngine.PlaySound(in SoundID.MenuTick);
            }

            const int hover_frames = 8;
            {
                hoverProgress = MathHelper.Clamp(hoverProgress + (hoveringHeader || Selected).ToDirectionInt(), 0, hover_frames);

                highlightDivider.Width.Set(0f, Ease(hoverProgress / (float)hover_frames));
            }

            const int open_frames = 12;
            {
                openProgress = MathHelper.Clamp(openProgress + Open.ToDirectionInt(), 0, open_frames);

                float eased = Ease(openProgress / (float)open_frames);

                dropdownIcon.Rotation = MathHelper.PiOver2 * eased;

                float height = list.GetTotalHeight() * eased;

                listContainer.MinHeight.Set(height, 0f);
                listContainer.MaxHeight.Set(height, 0f);

                Height.Set(tab_height + height, 0f);

                PaddingBottom = height;
            }

            static float Ease(float x)
            {
                // return x < 0.5f ? 4 * x * x * x : 1 - MathF.Pow(-2 * x + 2, 3) / 2;
                return 1 - MathF.Pow(1 - x, 5);
            }
        }
    }

    protected class CategoryTab : TextTab<LocalizedText>
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

        public ConfigCategory Category;

        public CategoryTab(ConfigCategory category) : base(category.DisplayName)
        {
            Category = category;

            TextTargetColor = TextColor = UnselectedColor;

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

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Daybreak.Common.Features.Achievements;
using Daybreak.Core.Hooks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Daybreak.Content.UI;

internal sealed class AchievementsMenu : UIState, ILoadable
{
    private UIList? achievementsList;
    private List<AchievementListItem> achievementElements = [];
    private List<UIToggleImage> categoryButtons = [];
    private UIElement? backPanel;
    private UIElement? outerContainer;

    public void InitializePage()
    {
        RemoveAllChildren();
        categoryButtons.Clear();
        achievementElements.Clear();
        achievementsList = null;
        const bool flag = true;
        var num = flag.ToInt() * 100;
        var uIElement = new UIElement();
        uIElement.Width.Set(0f, 0.8f);
        uIElement.MaxWidth.Set(800f + num, 0f);
        uIElement.MinWidth.Set(600f + num, 0f);
        uIElement.Top.Set(220f, 0f);
        uIElement.Height.Set(-220f, 1f);
        uIElement.HAlign = 0.5f;
        outerContainer = uIElement;
        Append(uIElement);
        var uIPanel = new UIPanel();
        uIPanel.Width.Set(0f, 1f);
        uIPanel.Height.Set(-110f, 1f);
        uIPanel.BackgroundColor = new Color(33, 43, 79) * 0.8f;
        uIPanel.PaddingTop = 0f;
        uIElement.Append(uIPanel);
        achievementsList = [];
        achievementsList.Width.Set(-25f, 1f);
        achievementsList.Height.Set(-50f, 1f);
        achievementsList.Top.Set(50f, 0f);
        achievementsList.ListPadding = 5f;
        uIPanel.Append(achievementsList);
        var uITextPanel = new UITextPanel<LocalizedText>(Language.GetText("UI.Achievements"), large: true)
        {
            HAlign = 0.5f,
        };
        uITextPanel.Top.Set(-33f, 0f);
        uITextPanel.SetPadding(13f);
        uITextPanel.BackgroundColor = new Color(73, 94, 171);
        uIElement.Append(uITextPanel);
        var uITextPanel2 = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, large: true);
        uITextPanel2.Width.Set(-10f, 0.5f);
        uITextPanel2.Height.Set(50f, 0f);
        uITextPanel2.VAlign = 1f;
        uITextPanel2.HAlign = 0.5f;
        uITextPanel2.Top.Set(-45f, 0f);
        uITextPanel2.OnMouseOver += FadedMouseOver;
        uITextPanel2.OnMouseOut += FadedMouseOut;
        uITextPanel2.OnLeftClick += GoBackClick;
        uIElement.Append(uITextPanel2);
        backPanel = uITextPanel2;
        var list = AchievementImpl.ACHIEVEMENTS;
        foreach (var ach in list)
        {
            var item = new AchievementListItem(ach, flag);
            achievementsList.Add(item);
            achievementElements.Add(item);
        }

        var uIScrollbar = new UIScrollbar();
        uIScrollbar.SetView(100f, 1000f);
        uIScrollbar.Height.Set(-50f, 1f);
        uIScrollbar.Top.Set(50f, 0f);
        uIScrollbar.HAlign = 1f;
        uIPanel.Append(uIScrollbar);
        achievementsList.SetScrollbar(uIScrollbar);
        var uIElement2 = new UIElement();
        uIElement2.Width.Set(0f, 1f);
        uIElement2.Height.Set(32f, 0f);
        uIElement2.Top.Set(10f, 0f);
        for (var i = 0; i < AchievementImpl.CATEGORIES.Count; i++)
        {
            var texture = AchievementImpl.CATEGORIES[i].GetIcon(out var frame);
            var uIToggleImage = new UIToggleImage(texture, 32, 32, new Point(frame.X, frame.Y), new Point(frame.X, frame.Y + 34));
            uIToggleImage.Left.Set(i * 36 + 8, 0f);
            uIToggleImage.SetState(value: true);
            uIToggleImage.OnLeftClick += FilterList;
            categoryButtons.Add(uIToggleImage);
            uIElement2.Append(uIToggleImage);
        }

        uIPanel.Append(uIElement2);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        for (var i = 0; i < categoryButtons.Count; i++)
        {
            if (!categoryButtons[i].IsMouseHovering)
            {
                continue;
            }

            var text = AchievementImpl.CATEGORIES[i].DisplayName.Value;
            var x = FontAssets.MouseText.Value.MeasureString(text).X;
            var vector = new Vector2(Main.mouseX, Main.mouseY) + new Vector2(16f);
            {
                if (vector.Y > (Main.screenHeight - 30))
                {
                    vector.Y = Main.screenHeight - 30;
                }

                if (vector.X > Main.screenWidth - x)
                {
                    vector.X = Main.screenWidth - 460;
                }
            }

            Utils.DrawBorderStringFourWay(spriteBatch, FontAssets.MouseText.Value, text, vector.X, vector.Y, new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), Color.Black, Vector2.Zero);
            break;
        }

        SetupGamepadPoints();
    }

    public void GotoAchievement(Achievement achievement)
    {
        achievementsList?.Goto(x => x is AchievementListItem item && item.Achievement == achievement);
    }

    private void GoBackClick(UIMouseEvent e, UIElement element)
    {
        Main.menuMode = 0;
        IngameFancyUI.Close();
    }

    private void FadedMouseOver(UIMouseEvent e, UIElement element)
    {
        SoundEngine.PlaySound(SoundID.MenuTick);
        ((UIPanel)e.Target).BackgroundColor = new Color(73, 94, 171);
        ((UIPanel)e.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
    }

    private void FadedMouseOut(UIMouseEvent e, UIElement element)
    {
        ((UIPanel)e.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
        ((UIPanel)e.Target).BorderColor = Color.Black;
    }

    private void FilterList(UIMouseEvent evt, UIElement listeningElement)
    {
        Debug.Assert(achievementsList is not null);

        SoundEngine.PlaySound(SoundID.MenuTick);

        achievementsList.Clear();
        foreach (var achievementElement in achievementElements)
        {
            var categories = achievementElement.Achievement.GetCategories();
            var anyCategoryOn = categories.Any(category => categoryButtons[category.Id].IsOn);
            if (anyCategoryOn)
            {
                achievementsList.Add(achievementElement);
            }
        }

        Recalculate();
    }

    public override void OnActivate()
    {
        base.OnActivate();

        InitializePage();

        Debug.Assert(outerContainer is not null);
        Debug.Assert(achievementsList is not null);

        if (Main.gameMenu)
        {
            outerContainer.Top.Set(220f, 0f);
            outerContainer.Height.Set(-220f, 1f);
        }
        else
        {
            outerContainer.Top.Set(120f, 0f);
            outerContainer.Height.Set(-120f, 1f);
        }

        achievementsList.UpdateOrder();

        if (PlayerInput.UsingGamepadUI)
        {
            UILinkPointNavigator.ChangePoint(3002);
        }
    }

    private void SetupGamepadPoints()
    {
        Debug.Assert(backPanel is not null);
        Debug.Assert(outerContainer is not null);

        const int minimum_id = 3000;

        UILinkPointNavigator.Shortcuts.BackButtonCommand = 3;
        UILinkPointNavigator.SetPosition(minimum_id, backPanel.GetInnerDimensions().ToRectangle().Center.ToVector2());
        UILinkPointNavigator.SetPosition(minimum_id + 1, outerContainer.GetInnerDimensions().ToRectangle().Center.ToVector2());
        var id = minimum_id;

        var uILinkPoint = UILinkPointNavigator.Points[id];
        {
            uILinkPoint.Unlink();
            uILinkPoint.Up = id + 1;
            id++;
        }

        var uILinkPoint2 = UILinkPointNavigator.Points[id];
        {
            uILinkPoint2.Unlink();
            uILinkPoint2.Up = id + 1;
            uILinkPoint2.Down = id - 1;
        }

        for (var i = 0; i < categoryButtons.Count; i++)
        {
            id = UILinkPointNavigator.Shortcuts.FANCYUI_HIGHEST_INDEX = id + 1;
            UILinkPointNavigator.SetPosition(id, categoryButtons[i].GetInnerDimensions().ToRectangle().Center.ToVector2());
            var uILinkPoint3 = UILinkPointNavigator.Points[id];
            {
                uILinkPoint3.Unlink();
                uILinkPoint3.Left = i == 0 ? -3 : id - 1;
                uILinkPoint3.Right = i == categoryButtons.Count - 1 ? -4 : id + 1;
                uILinkPoint3.Down = minimum_id;
            }
        }
    }

    public void Load(Mod mod) { }

    public void Unload() { }
}
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.InfoIcons;

/// <summary>
///     The basic definition of an informative icon that may be rendered.
/// </summary>
[Autoload(Side = ModSide.Client)]
public abstract class InfoIcon : ModTexturedType, ILocalizedModType
{
    private sealed class BetterImageButton : UIElement
    {
        public float VisibilityActive { get; set; } = 1f;

        public float VisibilityInactive { get; set; } = 0.4f;

        public Asset<Texture2D> Asset { get; }

        public BetterImageButton(Asset<Texture2D> asset, float targetHeight, float targetWidth)
        {
            Asset = asset;
            Height.Set(targetHeight, 0f);
            Width.Set(targetWidth, 0f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var dims = this.Dimensions;

            spriteBatch.Draw(
                new DrawParameters(Asset)
                {
                    Destination = dims,
                    Color = Color.White * (IsMouseHovering ? VisibilityActive : VisibilityInactive),
                }
            );
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            SoundEngine.PlaySound(12);
        }
    }

    /// <inheritdoc />
    public virtual string LocalizationCategory => nameof(InfoIcon);

    /// <summary>
    ///     The description of the icon to show on hover.
    /// </summary>
    public virtual LocalizedText Description => this.GetLocalization(nameof(Description));

    /// <summary>
    ///     The sorting priority of this icon, relative to other icons.  This
    ///     value is clamped to a [0, 1] range.
    /// </summary>
    public virtual float Priority => 0.5f;

    /// <inheritdoc />
    protected sealed override void InitTemplateInstance()
    {
        base.InitTemplateInstance();
    }

    /// <inheritdoc />
    public sealed override void SetupContent()
    {
        base.SetupContent();

        _ = Description;

        SetStaticDefaults();
    }

    /// <summary>
    ///     Initializes the element to render this icon in.
    /// </summary>
    public virtual UIElement CreateElement()
    {
        return new BetterImageButton(ModContent.Request<Texture2D>(Texture), 20f, 20f)
        {
            VAlign = 0.5f,
        };
    }

    /// <summary>
    ///     Invoked when your icon element is being hovered.
    /// </summary>
    public virtual void OnHover()
    {
        UICommon.TooltipMouseText(Description.Value);
    }
}

/// <summary>
///     An icon rendered on a player select UI item.
/// </summary>
[Autoload(Side = ModSide.Client)]
public abstract class PlayerIcon : InfoIcon
{
    /// <inheritdoc />
    protected sealed override void Register()
    {
        InfoIcons.Register(this);
        // ModTypeLookup<PlayerIcon>.Register(this);
    }

    /// <summary>
    ///     Whether this icon is visible.
    /// </summary>
    public abstract bool IsVisible(PlayerFileData playerFile);
}

/// <summary>
///     An icon rendered on a world select UI item.
/// </summary>
[Autoload(Side = ModSide.Client)]
public abstract class WorldIcon : InfoIcon
{
    /// <inheritdoc />
    protected sealed override void Register()
    {
        InfoIcons.Register(this);
        // ModTypeLookup<WorldIcon>.Register(this);
    }

    /// <summary>
    ///     Whether this icon is visible.
    /// </summary>
    public abstract bool IsVisible(WorldFileData worldFile);
}

internal sealed class S : PlayerIcon
{
    [OnLoad]
    private static void Load()
    {
        ModContent.GetInstance<ModImpl>().AddContent(new S());
        ModContent.GetInstance<ModImpl>().AddContent(new S());
        ModContent.GetInstance<ModImpl>().AddContent(new S());
    }

    public override string Texture => AssetReferences.Assets.Images.UI.InfoIconTEST.KEY;

    public override bool IsVisible(PlayerFileData worldFile) => true;
}

[Autoload(Side = ModSide.Client)]
internal static class InfoIcons
{
    private sealed class ScrollableIconPanel : UIList
    {
        private float horizontalViewPosition;
        private float innerListWidth;

        public ScrollableIconPanel(IEnumerable<InfoIcon> icons)
        {
            foreach (var icon in icons)
            {
                if (!ModContent.RequestIfExists<Texture2D>(icon.Texture, out var texture))
                {
                    continue;
                }

                texture.Wait();

                var element = icon.CreateElement();
                {
                    element.OnDraw += e =>
                    {
                        if (e.IsMouseHovering)
                        {
                            icon.OnHover();
                        }
                    };
                }
                Add(element);
            }
        }

        public override void RecalculateChildren()
        {
            // Manually recalculate child elements.
            foreach (var element in Elements)
            {
                var tempParent = new UIElement
                {
                    _innerDimensions = _innerDimensions,
                };

                element.Parent = tempParent;
                element.Recalculate();
                element.Parent = this;
            }

            var totalWidth = 0f;
            foreach (var item in _items)
            {
                var padding = (_items.Count == 1) ? 0f : ListPadding;

                // item.Top.Set((this.Dimensions.Height - item.Dimensions.Height) / 2f, 0f);
                item.Left.Set(totalWidth, 0f);
                item.Recalculate();

                totalWidth += item.GetOuterDimensions().Width + padding;
            }

            innerListWidth = totalWidth;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var dims = this.Dimensions;

            PaddingLeft = 5f;
            PaddingRight = 5f;

            if (innerListWidth > dims.Width)
            {
                _innerList.Left.Set(-horizontalViewPosition, 0f);

                const float button_padding = 16f;

                PaddingLeft = button_padding;
                PaddingRight = button_padding;
            }

            Recalculate();

            DrawPanel(spriteBatch, dims.TopLeft(), MathF.Min(innerListWidth + 5, dims.Width));

            if (innerListWidth <= dims.Width)
            {
                return;
            }

            const float scroll_amount = 6f;

            AssetReferences.Assets.Images.UI.InfoIcon_Left.Asset.Wait();
            AssetReferences.Assets.Images.UI.InfoIcon_Left_Hover.Asset.Wait();

            var leftPos = new Vector2(
                dims.Left + 2f,
                dims.Top + dims.Height / 2f
            );

            var leftHitbox = AssetReferences.Assets.Images.UI.InfoIcon_Left.Asset.Frame();
            {
                leftHitbox.X = (int)leftPos.X;
                leftHitbox.Y = (int)leftPos.Y - AssetReferences.Assets.Images.UI.InfoIcon_Left.Asset.Height() / 2;
            }

            var leftHovered = leftHitbox.Contains(Main.MouseScreen.ToPoint());
            var leftAsset = leftHovered
                ? AssetReferences.Assets.Images.UI.InfoIcon_Left_Hover.Asset
                : AssetReferences.Assets.Images.UI.InfoIcon_Left.Asset;

            spriteBatch.Draw(
                new DrawParameters(leftAsset)
                {
                    Position = leftPos,
                    Origin = Vector2.UnitY * leftAsset.Height() / 2f,
                }
            );

            AssetReferences.Assets.Images.UI.InfoIcon_Right.Asset.Wait();
            AssetReferences.Assets.Images.UI.InfoIcon_Right_Hover.Asset.Wait();

            var rightPos = new Vector2(
                dims.Right - AssetReferences.Assets.Images.UI.InfoIcon_Right.Asset.Width() - 2f,
                dims.Top + dims.Height / 2f
            );

            var rightHitbox = AssetReferences.Assets.Images.UI.InfoIcon_Left.Asset.Frame();
            {
                rightHitbox.X = (int)rightPos.X;
                rightHitbox.Y = (int)rightPos.Y - AssetReferences.Assets.Images.UI.InfoIcon_Left.Asset.Height() / 2;
            }

            var rightHovered = rightHitbox.Contains(Main.MouseScreen.ToPoint());
            var rightAsset = rightHovered
                ? AssetReferences.Assets.Images.UI.InfoIcon_Right_Hover.Asset
                : AssetReferences.Assets.Images.UI.InfoIcon_Right.Asset;

            spriteBatch.Draw(
                new DrawParameters(rightAsset)
                {
                    Position = rightPos,
                    Origin = Vector2.UnitY * rightAsset.Height() / 2f,
                }
            );

            if (!Main.mouseLeft)
            {
                return;
            }

            if (leftHovered)
            {
                ScrollList(-scroll_amount);
            }
            else if (rightHovered)
            {
                ScrollList(scroll_amount);
            }

            return;

            void ScrollList(float amount)
            {
                horizontalViewPosition = Math.Clamp(horizontalViewPosition + amount, 0f, innerListWidth - this.Dimensions.Width + 5f);
            }
        }

        private static void DrawPanel(SpriteBatch spriteBatch, Vector2 position, float width)
        {
            var panelTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/InnerPanelBackground");

            spriteBatch.Draw(
                panelTexture.Value,
                position,
                new Rectangle(0, 0, 8, panelTexture.Height()),
                Color.White
            );
            spriteBatch.Draw(
                panelTexture.Value,
                new Vector2(position.X + 8f, position.Y),
                new Rectangle(8, 0, 8, panelTexture.Height()),
                Color.White,
                0f,
                Vector2.Zero,
                new Vector2((width - 16f) / 8f, 1f),
                SpriteEffects.None,
                0f
            );
            spriteBatch.Draw(
                panelTexture.Value,
                new Vector2(position.X + width - 8f, position.Y),
                new Rectangle(16, 0, 8, panelTexture.Height()),
                Color.White
            );
        }

#region List Edit
        [OnLoad]
        private static void Load()
        {
            IL_UIElement.Recalculate += IL_UIElement_Recalculate;
        }

        // Maybe make a generic horizontal list element if this is the amount of effort I put into UI.
        private static void IL_UIElement_Recalculate(ILContext il)
        {
            var c = new ILCursor(il);

            ILLabel? jumpUiListCheckTarget = null;
            c.GotoNext(
                MoveType.Before,
                i => i.MatchLdarg0(),
                i => i.MatchCall<UIElement>($"get_{nameof(Parent)}"),
                i => i.MatchIsinst<UIList>(),
                i => i.MatchBrfalse(out jumpUiListCheckTarget)
            );
            Debug.Assert(jumpUiListCheckTarget is not null);

            c.EmitLdarg0();
            c.EmitLdloca(0);
            c.EmitDelegate(
                (UIElement elem, ref CalculatedStyle parentDimensions) =>
                {
                    if (elem is not ScrollableIconPanel)
                    {
                        return false;
                    }

                    // parentDimensions.Width = float.MaxValue;
                    return true;
                }
            );

            c.EmitBrtrue(jumpUiListCheckTarget);
        }
#endregion
    }

    private static List<PlayerIcon> playerIcons = [];
    private static List<WorldIcon> worldIcons = [];

    public static void Register(PlayerIcon playerIcon)
    {
        playerIcons.Add(playerIcon);
    }

    public static void Register(WorldIcon worldIcon)
    {
        worldIcons.Add(worldIcon);
    }

    [ModSystemHooks.PostSetupContent]
    private static void SortIcons()
    {
        playerIcons = playerIcons.OrderBy(x => Math.Clamp(x.Priority, 0f, 1f)).ToList();
        worldIcons = worldIcons.OrderBy(x => Math.Clamp(x.Priority, 0f, 1f)).ToList();
    }

    [OnLoad]
    private static void ApplyHooks()
    {
        // This system is derivative of Luminance's, so we can't load it too
        // early.
        if (
            ModLoader.TryGetMod("Luminance", out var luminance)
         && luminance.Version <= new Version(1, 0, 13)
        )
        {
            return;
        }

        IL_UIWorldListItem.ctor += Ctor_WorldInfoIconPanel;
        On_UIWorldListItem.DrawSelf += DrawSelf_WorldInfoAdjustListForText;

        IL_UICharacterListItem.ctor += Ctor_PlayerInfoIconPanel;
        On_UICharacterListItem.DrawSelf += DrawSelf_PlayerInfoAdjustListForText;
    }

    private static void Ctor_WorldInfoIconPanel(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(
            MoveType.After,
            i => i.MatchLdfld<UIWorldListItem>(nameof(UIWorldListItem._deleteButtonLabel)),
            i => i.MatchCall<UIElement>(nameof(UIElement.Append))
        );

        c.EmitLdarg0();
        c.EmitLdloc0();

        c.EmitDelegate(
            (UIWorldListItem elem, float totalWidth) =>
            {
                /*
                elem.RemoveChild(elem._buttonLabel);
                elem.RemoveChild(elem._deleteButtonLabel);
                */

                var listContainer = new UIElement();
                {
                    listContainer.MarginLeft = totalWidth;
                    listContainer.MarginRight = 30f;

                    listContainer.VAlign = 1f;

                    listContainer.Width.Set(-totalWidth - elem._deleteButton.Width.Pixels - 6f, 1f);
                    listContainer.Height.Set(27f, 0f);
                    listContainer.Top.Set(2f, 0f);
                }
                elem.Append(listContainer);

                var icons = worldIcons.Where(x => x.IsVisible(elem.Data)).ToArray();
                var iconList = new ScrollableIconPanel(icons.Concat(icons).Concat(icons));
                {
                    iconList.Width.Set(0f, 1f);
                    iconList.Height.Set(0f, 1f);
                }
                listContainer.Append(iconList);
            }
        );
    }

    private static void DrawSelf_WorldInfoAdjustListForText(
        On_UIWorldListItem.orig_DrawSelf orig,
        UIWorldListItem self,
        SpriteBatch spriteBatch
    )
    {
        orig(self, spriteBatch);

        if (
            self.Children.FirstOrDefault(x => x.Children.Any(y => y is ScrollableIconPanel)) is not { } container
         || container.Children.FirstOrDefault(x => x is ScrollableIconPanel) is not ScrollableIconPanel panel
        )
        {
            return;
        }

        panel.Left.Set(0f, 0f);
        panel.Width.Set(0f, 1f);

        if (self._buttonLabel.Text != string.Empty)
        {
            var size = FontAssets.MouseText.Value.MeasureString(self._buttonLabel.Text);
            {
                size.X += 6f;
            }

            panel.Left.Set(size.X, 0f);
            panel.Width.Set(-size.X, 1f);
        }
        else if (self._deleteButtonLabel.Text != string.Empty)
        {
            var size = FontAssets.MouseText.Value.MeasureString(self._deleteButtonLabel.Text);
            {
                size.X += 6f;
            }

            panel.Left.Set(0f, 0f);
            panel.Width.Set(-size.X, 1f);
        }

        self.Recalculate();
    }

    private static void Ctor_PlayerInfoIconPanel(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(
            MoveType.After,
            i => i.MatchLdfld<UICharacterListItem>(nameof(UICharacterListItem._deleteButtonLabel)),
            i => i.MatchCall<UIElement>(nameof(UIElement.Append))
        );

        c.EmitLdarg0();
        c.EmitLdloc0();

        c.EmitDelegate(
            (UICharacterListItem elem, float totalWidth) =>
            {
                /*
                elem.RemoveChild(elem._buttonLabel);
                elem.RemoveChild(elem._deleteButtonLabel);
                */

                var listContainer = new UIElement();
                {
                    listContainer.MarginLeft = totalWidth;
                    listContainer.MarginRight = 30f;

                    listContainer.VAlign = 1f;

                    listContainer.Width.Set(-totalWidth - elem._deleteButton.Width.Pixels - 6f, 1f);
                    listContainer.Height.Set(27f, 0f);
                    listContainer.Top.Set(2f, 0f);
                }
                elem.Append(listContainer);

                var icons = playerIcons.Where(x => x.IsVisible(elem.Data)).ToArray();
                var iconList = new ScrollableIconPanel(icons.Concat(icons).Concat(icons).Concat(icons).Concat(icons));
                {
                    iconList.Width.Set(0f, 1f);
                    iconList.Height.Set(0f, 1f);
                }
                listContainer.Append(iconList);
            }
        );
    }

    private static void DrawSelf_PlayerInfoAdjustListForText(
        On_UICharacterListItem.orig_DrawSelf orig,
        UICharacterListItem self,
        SpriteBatch spriteBatch
    )
    {
        orig(self, spriteBatch);

        if (
            self.Children.FirstOrDefault(x => x.Children.Any(y => y is ScrollableIconPanel)) is not { } container
         || container.Children.FirstOrDefault(x => x is ScrollableIconPanel) is not ScrollableIconPanel panel
        )
        {
            return;
        }

        panel.Left.Set(0f, 0f);
        panel.Width.Set(0f, 1f);

        if (self._buttonLabel.Text != string.Empty)
        {
            var size = FontAssets.MouseText.Value.MeasureString(self._buttonLabel.Text);
            {
                size.X += 6f;
            }

            panel.Left.Set(size.X, 0f);
            panel.Width.Set(-size.X, 1f);
        }
        else if (self._deleteButtonLabel.Text != string.Empty)
        {
            var size = FontAssets.MouseText.Value.MeasureString(self._deleteButtonLabel.Text);
            {
                size.X += 6f;
            }

            panel.Left.Set(0f, 0f);
            panel.Width.Set(-size.X, 1f);
        }

        self.Recalculate();
    }
}

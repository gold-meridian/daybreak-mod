using System;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Content.UI;

internal class SplitDraggableElement : UIElement
{
    private const float divider_width = 6f;
    private static readonly Color base_divider_color = new(85, 88, 159);

    public float MinRatio { get; set; }

    public float MaxRatio { get; set; }

    public float Ratio { get; set; }

    public UIElement LeftElement { get; }

    public UIElement RightElement { get; }

    private readonly UIElement dividerContainer;
    private bool isDragging;
    private static bool showCursor;

    public SplitDraggableElement()
    {
        const float horizontal_padding = divider_width * 0.5f + 2f;

        LeftElement = new UIElement();
        {
            LeftElement.PaddingRight += horizontal_padding;

            LeftElement.Height.Set(0f, 1f);
            LeftElement.Width.Set(0, Ratio);
            LeftElement.OverflowHidden = true;
        }
        Append(LeftElement);

        RightElement = new UIElement();
        {
            RightElement.PaddingLeft += horizontal_padding;

            RightElement.Height.Set(0f, 1f);
            RightElement.Width.Set(0, 1f - Ratio);
            RightElement.HAlign = 1f;
            RightElement.OverflowHidden = true;
        }
        Append(RightElement);

        // Divider
        {
            // Use a container element to adjust the spacing of the divider and
            // allow inputs on it.
            dividerContainer = new UIElement();
            {
                dividerContainer.Width.Set(divider_width, 0f);
                dividerContainer.Height.Set(0f, 1f);

                dividerContainer.Left.Set(-divider_width * 0.5f, Ratio);

                dividerContainer.OnMouseOver += HoverDivider;
                dividerContainer.OnLeftMouseDown += GrabDivider;
                dividerContainer.OnLeftMouseUp += ReleaseDivider;
            }
            Append(dividerContainer);

            var divider = new UIVerticalSeparator();
            {
                divider.Height.Set(0f, 1f);

                divider.HAlign = 0.5f;

                divider.Color = base_divider_color;
            }
            dividerContainer.Append(divider);
        }

        return;

        void HoverDivider(UIMouseEvent evt, UIElement listeningElement)
        {
            if (!isDragging)
            {
                SoundEngine.PlaySound(in SoundID.MenuTick);
            }
        }

        void GrabDivider(UIMouseEvent evt, UIElement listeningElement)
        {
            isDragging = true;
        }

        void ReleaseDivider(UIMouseEvent evt, UIElement listeningElement)
        {
            isDragging = false;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (!isDragging)
        {
            return;
        }

        var dims = this.Dimensions;
        var mousePosition = UserInterface.ActiveInstance.MousePosition;
        var mouseRatio = (mousePosition.X - dims.X) / dims.Width;

        var min = MathHelper.Max(LeftElement.MinWidth.GetValue(dims.Width) / dims.Width, MinRatio);
        var max = MathHelper.Min(1f - RightElement.MinWidth.GetValue(dims.Width) / dims.Width, MaxRatio);

        var oldRatio = Ratio;
        Ratio = MathHelper.Clamp(mouseRatio, min, max);

        if (Math.Abs(Ratio - oldRatio) <= 0.001f)
        {
            return;
        }

        dividerContainer.Left.Set(-divider_width * 0.5f, Ratio);

        LeftElement.Width.Set(0, Ratio);
        RightElement.Width.Set(0, 1f - Ratio);

        Recalculate();
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        showCursor |= dividerContainer.IsMouseHovering || isDragging;
    }

#region Cursor Edit
    [OnLoad]
    private static void ApplyHooks()
    {
        IL_Main.DrawMenu += DrawMenu_DrawHorizontalDragCursor;
    }

    private static void DrawMenu_DrawHorizontalDragCursor(ILContext il)
    {
        var c = new ILCursor(il);

        var jumpDrawCursorTarget = c.DefineLabel();

        c.GotoNext(MoveType.Before, x => x.MatchCall<Main>(nameof(Main.DrawThickCursor)));

        c.EmitDelegate(
            () =>
            {
                if (!showCursor)
                {
                    return false;
                }

                var texture = Assets.Images.UI.HorizontalDragCursor.Asset.Value;
                var position = new Vector2(Main.mouseX, Main.mouseY);
                var origin = texture.Size() * 0.5f;

                Main.spriteBatch.Draw(
                    new DrawParameters(texture)
                    {
                        Position = position,
                        Origin = origin,
                        Scale = new Vector2(Main.cursorScale),
                    }
                );

                showCursor = false;
                return true;
            }
        );

        c.EmitBrtrue(jumpDrawCursorTarget);

        c.GotoNext(MoveType.After, x => x.MatchCall<Main>(nameof(Main.DrawCursor)));

        c.MarkLabel(jumpDrawCursorTarget);
    }

    // Only works in-game?
    /*
    private static void DrawInterface_36_Cursor_DrawHorizontalDragCursor(ILContext il)
    {
        var c = new ILCursor(il);

        var jumpRetTarget = c.DefineLabel();

        c.GotoNext(MoveType.After,
            i => i.MatchCallvirt<SpriteBatch>(nameof(SpriteBatch.Begin)));

        c.EmitDelegate(
            () =>
            {
                if (!showCursor)
                {
                    return false;
                }

                Texture2D texture = AssetReferences.Assets.Images.UI.HorizontalDragCursor.Asset.Value;

                var position = new Vector2(Main.mouseX, Main.mouseY);

                var origin = texture.Size() * 0.5f;

                Main.spriteBatch.Draw(
                    texture,
                    position,
                    null,
                    Color.White,
                    0f,
                    origin,
                    Main.cursorScale,
                    SpriteEffects.None,
                    0f);

                showCursor = false;

                return true;
            }
        );

        c.EmitBrfalse(jumpRetTarget);

        c.EmitRet();

        c.MarkLabel(jumpRetTarget);
    }
    */
#endregion
}

using Daybreak.Common.Features.Hooks;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Common.UI;

public class SplitDraggableElement : UIElement
{
    protected const float divider_width = 6f;

    protected static Color baseDividerColor { get; } = new Color(85, 88, 159);

    public float MinRatio { get; protected set; }

    public float MaxRatio { get; protected set; }

    public float Ratio { get; protected set; }

    public readonly UIElement Left;
    public readonly UIElement Right;

    protected readonly UIElement dividerContainer;

    protected bool isDragging;

    protected static bool showCursor;

    public SplitDraggableElement(float minRatio, float maxRatio, float ratio)
    {
        MinRatio = minRatio;
        MaxRatio = maxRatio;

        Ratio = ratio;

        float horizontalPadding = (divider_width * 0.5f) + 2f;

        Left = new UIElement();
        {
            Left.PaddingRight += horizontalPadding;

            Left.Height.Set(0f, 1f);
            Left.Width.Set(0, Ratio);
            Left.OverflowHidden = true;
        }
        Append(Left);

        Right = new UIElement();
        {
            Right.PaddingLeft += horizontalPadding;

            Right.Height.Set(0f, 1f);
            Right.Width.Set(0, 1f - Ratio);
            Right.HAlign = 1f;
            Right.OverflowHidden = true;
        }
        Append(Right);

        // Divider
        {
            // Use a container element to adjust the spacing of the divider and allow inputs on it.
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

                divider.Color = baseDividerColor;
            }
            dividerContainer.Append(divider);
        }

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

        Vector2 mousePosition = UserInterface.ActiveInstance.MousePosition;

        float mouseRatio = (mousePosition.X - dims.X) / dims.Width;

        float min = MathHelper.Max(Left.MinWidth.GetValue(dims.Width) / dims.Width, MinRatio);
        float max = MathHelper.Min(1f - (Right.MinWidth.GetValue(dims.Width) / dims.Width), MaxRatio);

        float oldRatio = Ratio;
        Ratio = MathHelper.Clamp(mouseRatio, min, max);

        if (Ratio != oldRatio)
        {
            dividerContainer.Left.Set(-divider_width * 0.5f, Ratio);

            Left.Width.Set(0, Ratio);
            Right.Width.Set(0, 1f - Ratio);

            Recalculate();
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        showCursor |= dividerContainer.IsMouseHovering || isDragging;
    }

#region Cursor Edit

    [OnLoad]
    private static void Load()
    {
        // IL_Main.DrawInterface_36_Cursor += DrawInterface_36_Cursor_DrawHorizontalDragCursor;
        IL_Main.DrawMenu += DrawMenu_DrawHorizontalDragCursor;
    }

    private static void DrawMenu_DrawHorizontalDragCursor(ILContext il)
    {
        var c = new ILCursor(il);

        var jumpDrawCursorTarget = c.DefineLabel();

        c.GotoNext(MoveType.Before,
            i => i.MatchLdcI4(0),
            i => i.MatchCall<Main>(nameof(Main.DrawThickCursor)));

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

        c.EmitBrtrue(jumpDrawCursorTarget);

        c.GotoNext(MoveType.After,
            i => i.MatchLdcI4(0),
            i => i.MatchCall<Main>(nameof(Main.DrawCursor)));

        c.MarkLabel(jumpDrawCursorTarget);
    }

    /*
     * Only works ingame ??
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

using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
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

    public SplitDraggableElement(float minRatio, float maxRatio, float ratio)
    {
        MinRatio = minRatio;
        MaxRatio = maxRatio;

        Ratio = ratio;

        const float horizontal_padding = 4f;

        Left = new UIElement();
        {
            Left.PaddingRight += horizontal_padding;

            Left.Height.Set(0f, 1f);
            Left.Width.Set(0, Ratio);
            Left.OverflowHidden = true;
        }
        Append(Left);

        Right = new UIElement();
        {
            Right.PaddingLeft += horizontal_padding;

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
}

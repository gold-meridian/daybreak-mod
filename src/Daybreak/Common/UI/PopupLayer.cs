using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Common.UI;

public sealed class PopupLayer : UIElement
{
    private UIElement? popup;

    public PopupLayer()
    {
        Width.Set(0f, 1f);
        Height.Set(0f, 1f);

        IgnoresMouseInteraction = true;
    }

    public override void LeftClick(UIMouseEvent evt)
    {
        base.LeftClick(evt);

        if (evt.Target != this)
        {
            return;
        }

        SoundEngine.PlaySound(SoundID.MenuClose);

        IgnoresMouseInteraction = true;

        RemoveAllChildren();
    }

    public override void ScrollWheel(UIScrollWheelEvent evt)
    {
        base.ScrollWheel(evt);

        if (evt.Target != this)
        {
            return;
        }

        SoundEngine.PlaySound(SoundID.MenuClose);

        IgnoresMouseInteraction = true;

        RemoveAllChildren();
    }

    public void AppendPopup(UIElement element, Vector2 position)
    {
        IgnoresMouseInteraction = false;

        RemoveAllChildren();

        element.Activate();
        {
            var dims = this.InnerDimensions;

            position = Vector2.Clamp(
                position,
                dims.TopLeft(),
                dims.BottomRight() - element.Dimensions.Size()
            );

            position.X -= PaddingLeft;
            position.Y -= PaddingTop;

            element.Left.Set(position.X, 0f);
            element.Top.Set(position.Y, 0f);
        }
        Append(element);

        element.Recalculate();

        popup = element;
    }

    public override void Recalculate()
    {
        base.Recalculate();
    }
}

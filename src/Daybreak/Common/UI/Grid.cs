using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Daybreak.Common.UI;

// Inherits from UIList to replicate a quirk of UIElement.Recalculate
// where the height of the parent element is treated as float.MaxValue:
// if (Parent != null && Parent is UIList)
// {
//     parentDimensions.Height = float.MaxValue;
// }
public class Grid : UIList
{
    public int Columns { get; set; } = 1;

    /// <inheritdoc/>
    public override void RecalculateChildren()
    {
        // base.RecalculateChildren()
        foreach (UIElement element in Elements)
        {
            element.Recalculate();
        }

        float totalHeight = 0f;
        float height = 0f;

        float width = this.Dimensions.Width / (float)Columns;

        width -= ListPadding;
        width += ListPadding / Columns;

        for (int i = 0; i < _items.Count; i++)
        {
            if (i >= 1 && i % Columns == 0)
            {
                totalHeight += height + ListPadding;
                height = 0f;
            }

            _items[i].Top.Set(totalHeight, 0f);

            _items[i].Left.Set(i % Columns * (width + ListPadding), 0f);
            _items[i].Width.Set(width, 0f);

            _items[i].Recalculate();

            height = MathF.Max(height, _items[i].OuterDimensions.Height);
        }

        totalHeight += height + ListPadding;

        _innerListHeight = totalHeight;
    }
}

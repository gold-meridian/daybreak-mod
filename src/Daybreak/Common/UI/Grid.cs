using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Daybreak.Common.UI;

public class Grid : UIList
{
    private static readonly MethodInfo recalculate_children_method = typeof(UIElement).GetMethod(nameof(UIElement.RecalculateChildren), BindingFlags.Public | BindingFlags.Instance)!;

    public int Columns { get; set; } = 1;

    /// <inheritdoc/>
    public override void RecalculateChildren()
    {
        recalculate_children_method.Invoke(this, null);

        float totalHeight = 0f;
        float height = 0f;

        float width = this.Dimensions.Width / (float)Columns;

        width += ListPadding / Columns;

        for (int i = 0; i < _items.Count; i++)
        {
            if (i >= 1 && i % Columns == 0)
            {
                totalHeight += _items[i].OuterDimensions.Height + ListPadding;
                height = 0f;
            }

            _items[i].Top.Set(totalHeight, 0f);

            _items[i].Left.Set(i % Columns * width, 0f);
            _items[i].Width.Set(width, 0f);

            _items[i].Recalculate();

            height = MathF.Max(height, _items[i].OuterDimensions.Height);
        }

        _innerListHeight = totalHeight;
    }
}

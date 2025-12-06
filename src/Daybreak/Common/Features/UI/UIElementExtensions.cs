using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Daybreak.Common.Features.UI;

[PublicAPI]
public static class UIElementExtensions
{
    extension(UIElement element)
    {
        public Rectangle Dimensions => element.GetDimensions().ToRectangle();

        public Rectangle InnerDimensions => element.GetInnerDimensions().ToRectangle();

        public Rectangle DimensionsFromParent
        {
            get
            {
                UIElement parent = element.Parent;

                if (parent is null)
                    return element.Dimensions;

                return element.GetDimensionsBasedOnParentDimensions(parent.GetInnerDimensions()).ToRectangle();
            }
        }
    }
}

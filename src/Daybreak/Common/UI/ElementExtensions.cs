using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Daybreak.Common.UI;

/// <summary>
///     Extensions for <see cref="UIElement"/>s.
/// </summary>
public static class ElementExtensions
{
    extension(UIElement element)
    {
        /// <summary>
        ///     The <paramref name="element"/>'s dimensions as a
        ///     <see cref="Rectangle"/>.
        /// </summary>
        public Rectangle Dimensions => element.GetDimensions().ToRectangle();

        /// <summary>
        ///     The <paramref name="element"/>'s inner dimensions as a
        ///     <see cref="Rectangle"/>.
        /// </summary>
        public Rectangle InnerDimensions => element.GetInnerDimensions().ToRectangle();

        /// <summary>
        ///     Attempts to get the dimensions of this
        ///     <paramref name="element"/> based on the dimensions of a parent
        ///     element.
        ///     <br />
        ///     If the element has no parent, <see cref="Dimensions"/> is
        ///     returned directly.
        /// </summary>
        public Rectangle ParentRelativeDimensions => element.Parent is not { } parent
            ? element.Dimensions
            : element.GetDimensionsBasedOnParentDimensions(parent.GetInnerDimensions()).ToRectangle();
    }
}

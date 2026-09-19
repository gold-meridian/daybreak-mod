using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Terraria.UI;

namespace Daybreak.UI.V1;

partial class Extensions
{
    extension(StyleDimension)
    {
        /// <summary>
        ///     Adds to both values of this <see cref="StyleDimension"/>.
        /// </summary>
        public static StyleDimension operator +(StyleDimension a, StyleDimension b) => new(a.Pixels + b.Pixels, a.Percent + b.Percent);

        /// <summary>
        ///     Subtracts from both values of this <see cref="StyleDimension"/>.
        /// </summary>
        public static StyleDimension operator -(StyleDimension a, StyleDimension b) => new(a.Pixels - b.Pixels, a.Percent - b.Percent);

        /// <inheritdoc cref="Extensions.op_Addition(StyleDimension, StyleDimension)" />
        public static StyleDimension operator +(StyleDimension a, (float pixels, float percent) b) => new(a.Pixels + b.pixels, a.Percent + b.percent);

        /// <inheritdoc cref="Extensions.op_Subtraction(StyleDimension, StyleDimension)" />
        public static StyleDimension operator -(StyleDimension a, (float pixels, float percent) b) => new(a.Pixels - b.pixels, a.Percent - b.percent);
    }

    extension(ref StyleDimension styleDimension)
    {
        /// <summary>
        ///     Adds to both values of this <see cref="StyleDimension"/>.<br/>
        ///     Does not return a value.
        /// </summary>
        public void Add(StyleDimension b) => styleDimension += b;

        /// <inheritdoc cref="Extensions.Add(ref Terraria.UI.StyleDimension,Terraria.UI.StyleDimension)"/>
        public void Add(float pixels, float percent) => styleDimension += new StyleDimension(pixels, percent);

        /// <summary>
        ///     Subtracts from both values of this <see cref="StyleDimension"/>.<br/>
        ///     Does not return a value.
        /// </summary>
        public void Sub(StyleDimension b) => styleDimension -= b;

        /// <inheritdoc cref="Extensions.Sub(ref Terraria.UI.StyleDimension,Terraria.UI.StyleDimension)"/>
        public void Sub(float pixels, float percent) => styleDimension -= new StyleDimension(pixels, percent);
    }

    extension(UIElement element)
    {
        /// <summary>
        ///     The <paramref name="element"/>'s dimensions as a
        ///     <see cref="Rectangle"/>.<br></br>
        ///     <inheritdoc cref="UIElement.GetDimensions"/>
        /// </summary>
        public Rectangle Dimensions => element.GetDimensions().ToRectangle();

        /// <summary>
        ///     The <paramref name="element"/>'s inner dimensions as a
        ///     <see cref="Rectangle"/>.<br></br>
        ///     <inheritdoc cref="UIElement.GetInnerDimensions"/>
        /// </summary>
        public Rectangle InnerDimensions => element.GetInnerDimensions().ToRectangle();

        /// <summary>
        ///     The <paramref name="element"/>'s outer dimensions as a
        ///     <see cref="Rectangle"/>.<br></br>
        ///     <inheritdoc cref="UIElement.GetOuterDimensions"/>
        /// </summary>
        public Rectangle OuterDimensions => element.GetOuterDimensions().ToRectangle();

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

        /// <summary>
        ///     The <paramref name="element"/>'s top, height, and vertical margin values combined.
        /// </summary>
        public StyleDimension Bottom => element.Top + element.Height + (element.MarginTop + element.MarginBottom, 0);

        /// <summary>
        ///     The <paramref name="element"/>'s left, width, and horizontal margin values combined.
        /// </summary>
        public StyleDimension Right => element.Left + element.Width + (element.MarginLeft + element.MarginRight, 0);
    }
}

using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;

namespace Daybreak.UI.V1;
// ReSharper disable ValueParameterNotUsed

partial class Extensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public readonly record struct EventWrapper<T>(Action<T> AddEvent)
        where T : Delegate
    {
        public static EventWrapper<T> operator +(EventWrapper<T> @this, T evt) => @this += evt;

        public void operator +=(T evt)
        {
            AddEvent(evt);
        }
    }

    public delegate void MouseEvent<in TElement>(UIMouseEvent evt, TElement listeningElement) where TElement : UIElement;

    public delegate void ScrollWheelEvent<in TElement>(UIScrollWheelEvent evt, TElement listeningElement) where TElement : UIElement;

    public delegate void ElementEvent<in TElement>(TElement affectedElement) where TElement : UIElement;

    public delegate void DrawEvent<in TElement>(TElement affectedElement, SpriteBatch sb) where TElement : UIElement;

    // TODO: Perhaps find a way of doing this without a layer of indirection?
    extension<TElement>(TElement element)
        where TElement : UIElement
    {
        public EventWrapper<MouseEvent<TElement>> OnLeftMouseDownExt { get => new(evt => element.OnLeftMouseDown += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnLeftMouseUpExt { get => new(evt => element.OnLeftMouseUp += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnLeftClickExt { get => new(evt => element.OnLeftClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnLeftDoubleClickExt { get => new(evt => element.OnLeftDoubleClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }

        public EventWrapper<MouseEvent<TElement>> OnRightMouseDownExt { get => new(evt => element.OnRightMouseDown += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnRightMouseUpExt { get => new(evt => element.OnRightMouseUp += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnRightClickExt { get => new(evt => element.OnRightClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnRightDoubleClickExt { get => new(evt => element.OnRightDoubleClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }

        /// <inheritdoc cref="UIElement.OnMouseOver"/>
        public EventWrapper<MouseEvent<TElement>> OnMouseOverExt { get => new(evt => element.OnMouseOver += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }

        /// <inheritdoc cref="UIElement.OnMouseOut"/>
        public EventWrapper<MouseEvent<TElement>> OnMouseOutExt { get => new(evt => element.OnMouseOut += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }

        public EventWrapper<ScrollWheelEvent<TElement>> OnScrollWheelExt { get => new(evt => element.OnScrollWheel += (sEvt, e) => evt(sEvt, (TElement)e)); set { } }

        public EventWrapper<ElementEvent<TElement>> OnUpdateExt { get => new(evt => element.OnUpdate += e => evt((TElement)e)); set { } }

        /// <inheritdoc cref="UIElement.OnDraw"/>
        public EventWrapper<DrawEvent<TElement>> OnDrawExt { get => new(evt => element.OnDraw += (e, sb) => evt((TElement)e, sb)); set { } }

        public EventWrapper<MouseEvent<TElement>> OnMiddleMouseDownExt { get => new(evt => element.OnMiddleMouseDown += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnMiddleMouseUpExt { get => new(evt => element.OnMiddleMouseUp += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnMiddleClickExt { get => new(evt => element.OnMiddleClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnMiddleDoubleClickExt { get => new(evt => element.OnMiddleDoubleClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }

        public EventWrapper<MouseEvent<TElement>> OnXButton1MouseDownExt { get => new(evt => element.OnXButton1MouseDown += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnXButton1MouseUpExt { get => new(evt => element.OnXButton1MouseUp += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnXButton1ClickExt { get => new(evt => element.OnXButton1Click += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnXButton1DoubleClickExt { get => new(evt => element.OnXButton1DoubleClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }

        public EventWrapper<MouseEvent<TElement>> OnXButton2MouseDownExt { get => new(evt => element.OnXButton2MouseDown += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnXButton2MouseUpExt { get => new(evt => element.OnXButton2MouseUp += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnXButton2ClickExt { get => new(evt => element.OnXButton2Click += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
        public EventWrapper<MouseEvent<TElement>> OnXButton2DoubleClickExt { get => new(evt => element.OnXButton2DoubleClick += (mEvt, e) => evt(mEvt, (TElement)e)); set { } }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}

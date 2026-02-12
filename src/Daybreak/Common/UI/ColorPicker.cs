using Daybreak.Common.Rendering;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Common.UI;

public class ColorPicker : UIElement
{
    public event Action<ColorPicker>? OnChanged;

    public Color Color
    {
        get
        {
            var col = HSVToColor(new Vector3(Square.Hue, Square.PickerPosition.X, 1 - Square.PickerPosition.Y));

            if (AlphaSlider is not null)
            {
                col.A = (byte)(AlphaSlider.Ratio * byte.MaxValue);
            }

            return HSVToColor(new Vector3(Square.Hue, Square.PickerPosition.X, 1 - Square.PickerPosition.Y)) ;
        }
        set
        {
            var hsv = ColorToHSV(value);

            HueSlider.Ratio = hsv.X;

            Square.Hue = hsv.X;

            Square.PickerPosition = new(hsv.Y, 1 - hsv.Z);

            AlphaSlider?.Ratio = (float)value.A / byte.MaxValue;
        }
    }

    protected Slider HueSlider;

    protected Slider? AlphaSlider;

    protected HSVSquare Square;

    public ColorPicker(bool showAlpha = false)
    {
        float sliderMargin = 0f;

        Width.Set(0f, 1f);

        Height.Set(0f, 1f);

        HueSlider = new Slider();
        {
            HueSlider.VAlign = 1f;

            HueSlider.InnerTexture = AssetReferences.Assets.Images.UI.ColorPickerHue.Asset;

            HueSlider.OnChanged += OnChanged_UpdateHue;

            sliderMargin += 20;
        }
        Append(HueSlider);

        if (showAlpha)
        {
            HueSlider.MarginBottom += sliderMargin;

            AlphaSlider = new Slider();
            {
                AlphaSlider.VAlign = 1f;

                sliderMargin += 20;
            }
            Append(AlphaSlider);
        }

        Square = new HSVSquare();
        {
            Square.HAlign = 1f;

            Square.Width.Set(0, 1f);
            Square.Height.Set(-sliderMargin, 1f);

            Square.OnChanged += (_) => OnChanged?.Invoke(this);
        }
        Append(Square);

        return;

        void OnChanged_UpdateHue(Slider obj)
        {
            Square?.Hue = obj.Ratio;

            OnChanged?.Invoke(this);
        }
    }

    protected static Color HSVToColor(Vector3 hsv)
    {
        int hue = (int)(hsv.X * 360f);

        float num2 = hsv.Y * hsv.Z;
        float num3 = num2 * (1f - MathF.Abs(hue / 60f % 2f - 1f));
        float num4 = hsv.Z - num2;

        return hue switch
        {
            < 60 => new(num4 + num2, num4 + num3, num4),
            < 120 => new(num4 + num3, num4 + num2, num4),
            < 180 => new(num4, num4 + num2, num4 + num3),
            < 240 => new(num4, num4 + num3, num4 + num2),
            < 300 => new(num4 + num3, num4, num4 + num2),
            _ => new(num4 + num2, num4, num4 + num3)
        };
    }

    protected static Vector3 ColorToHSV(Color color)
    {
        float max = MathF.Max(color.R, MathF.Max(color.G, color.B)) / 255f;
        float min = MathF.Min(color.R, MathF.Min(color.G, color.B)) / 255f;

        float hue = Main.rgbToHsl(color).X;
        float sat = (max == 0) ? 0f : 1f - (1f * min / max);
        float val = max;

        return new(hue, sat, val);
    }

    protected sealed class HSVSquare : UIElement
    {
        private static readonly Color OUTLINE_COLOR = new(215, 215, 215);

        public event Action<HSVSquare>? OnChanged;

        public float Hue { get; set; }

        /// <summary>
        /// Normalized position of the picker used for deriving saturation and value.
        /// </summary>
        public Vector2 PickerPosition { get; set; }

        protected bool IsHeld;

        public HSVSquare()
        {
            Width.Set(0, 1f);
            Height.Set(0, 1f);

            SetPadding(4);
        }

        public override void LeftMouseDown(UIMouseEvent evt)
        {
            base.LeftMouseDown(evt);

            if (evt.Target == this)
            {
                IsHeld = true;
            }
        }

        public override void LeftMouseUp(UIMouseEvent evt)
        {
            base.LeftMouseUp(evt);

            IsHeld = false;
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            IsMouseHovering &= !Main.mouseLeft || IsHeld;

            if (!IsMouseHovering || IsHeld)
            {
                return;
            }

            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsHeld)
            {
                return;
            }

            Rectangle dims = this.InnerDimensions;

            dims.Inflate(-4, -4);

            var oldPickerPosition = PickerPosition;

            Vector2 position = Vector2.Clamp(Main.MouseScreen, dims.TopLeft(), dims.TopLeft() + dims.Size());

            PickerPosition = (position - dims.TopLeft()) / dims.Size();

            if (oldPickerPosition != PickerPosition)
            {
                OnChanged?.Invoke(this);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            Texture2D value = AssetReferences.Assets.Images.UI.ColorPickerValue.Asset.Value;
            Texture2D saturation = AssetReferences.Assets.Images.UI.ColorPickerSaturation.Asset.Value;

            Texture2D picker = AssetReferences.Assets.Images.UI.ColorPickerDot.Asset.Value;

            Rectangle dims = this.Dimensions;

            spriteBatch.Draw(
                TextureAssets.MagicPixel.Value,
                dims,
                Color.Black
            );

            dims.Inflate(-2, -2);

            Color outline =
                IsHeld || IsMouseHovering
                ? Main.OurFavoriteColor
                : OUTLINE_COLOR;

            spriteBatch.Draw(
                TextureAssets.MagicPixel.Value,
                dims,
                outline
            );

            dims.Inflate(-2, -2);

            spriteBatch.Draw(value, dims, Color.White);
            spriteBatch.Draw(saturation, dims, HSVToColor(new Vector3(Hue, 1f, 1f)));

            Vector2 pickerOrigin = picker.Size() * 0.5f;

            Vector2 position = PickerPosition * dims.Size();
            position += dims.TopLeft();

            position = Utils.Round(position);

            spriteBatch.Draw(new DrawParameters(picker)
                {
                    Position = position,
                    Color = Color.White,
                    Origin = pickerOrigin
                }
            );
        }
    }
}

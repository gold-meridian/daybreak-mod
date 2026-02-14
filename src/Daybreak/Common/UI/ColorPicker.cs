using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Rendering;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameInput;
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

            if (Alpha is not null)
            {
                col.A = (byte)(Alpha.Ratio * byte.MaxValue);
            }

            return col;
        }
        set
        {
            var hsv = ColorToHSV(value);

            Hue.Ratio = hsv.X;

            Square.Hue = hsv.X;

            Square.PickerPosition = new(hsv.Y, 1 - hsv.Z);

            Alpha?.Ratio = (float)value.A / byte.MaxValue;

            Alpha?.InnerColor = value;
        }
    }

    protected Slider Hue;

    protected AlphaSlider? Alpha;

    protected HSVSquare Square;

    public ColorPicker(bool showAlpha = false)
    {
        float sliderMargin = 0f;

        Width.Set(0f, 1f);

        Height.Set(0f, 1f);

        Hue = new Slider();
        {
            Hue.VAlign = 1f;

            Hue.InnerTexture = AssetReferences.Assets.Images.UI.ColorPickerHue.Asset;

            Hue.OnChanged += OnChanged_UpdateHue;

            sliderMargin += 20;
        }
        Append(Hue);

        if (showAlpha)
        {
            Hue.MarginBottom += sliderMargin;

            Alpha = new AlphaSlider();
            {
                Alpha.VAlign = 1f;

                Alpha.OnChanged += (_) => OnChanged?.Invoke(this);

                sliderMargin += 20;
            }
            Append(Alpha);
        }

        Square = new HSVSquare();
        {
            Square.HAlign = 1f;

            Square.Width.Set(0, 1f);
            Square.Height.Set(-sliderMargin, 1f);

            Square.OnChanged += OnChanged_UpdateColor;
        }
        Append(Square);

        return;

        void OnChanged_UpdateHue(Slider obj)
        {
            Square?.Hue = obj.Ratio;

            Alpha?.InnerColor = Color;

            OnChanged?.Invoke(this);
        }

        void OnChanged_UpdateColor(HSVSquare obj)
        {
            Alpha?.InnerColor = Color;

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

    protected sealed class AlphaSlider : Slider
    {
        public AlphaSlider() : base()
        {
            InnerTexture = AssetReferences.Assets.Images.UI.PreMultipliedGradient.Asset;
        }

        protected override void DrawSliderInner(SpriteBatch spriteBatch, Rectangle dims, Color color)
        {
            spriteBatch.Draw(TextureAssets.MagicPixel.Value, dims, Color.White);

            color.A = byte.MaxValue;

            base.DrawSliderInner(spriteBatch, dims, color);
        }
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

        /// <summary>
        /// Used over IsMouseHovering to determine if the mouse is hovering the slider and NOT draging another.
        /// </summary>
        public bool Hovering { get; private set; }

        public bool IsHeld;

#region Mouse Movement Edit

        private const float slow_cursor_speed = 0.15f;

        private static bool SlowCursor;

        [OnLoad]
        private static void Load()
        {
            IL_PlayerInput.MouseInput += DoUpdate_HandleInput_SlowCursor;
        }

        private static void DoUpdate_HandleInput_SlowCursor(ILContext il)
        {
            var c = new ILCursor(il);

            c.GotoNext(MoveType.After,
                i => i.MatchCall(typeof(Mouse), nameof(Mouse.GetState)),
                i => i.MatchStsfld<PlayerInput>(nameof(PlayerInput.MouseInfo))
            );

            c.EmitDelegate(
                () =>
                {
                    if (SlowCursor)
                    {
                        Mouse.SetPosition(
                            PlayerInput.MouseInfoOld.X + (int)((PlayerInput.MouseInfo.X - PlayerInput.MouseInfoOld.X) * slow_cursor_speed),
                            PlayerInput.MouseInfoOld.Y + (int)((PlayerInput.MouseInfo.Y - PlayerInput.MouseInfoOld.Y) * slow_cursor_speed)
                        );

                        PlayerInput.MouseInfo = Mouse.GetState();
                    }

                    SlowCursor = false;
                }
            );
        }

#endregion

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

            Hovering = IsMouseHovering && (!Main.mouseLeft || IsHeld);

            if (!Hovering || IsHeld)
            {
                return;
            }

            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);

            Hovering = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!IsHeld)
            {
                if (!Hovering && IsMouseHovering && !Main.mouseLeft)
                {
                    Hovering = true;
                    SoundEngine.PlaySound(SoundID.MenuTick);
                }

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

            if (PlayerInput.Triggers.Current.SmartCursor || Main.keyState.IsKeyDown(Keys.LeftShift))
            {
                SlowCursor = true;
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
                Hovering || IsHeld
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

using Daybreak.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Content.UI;

public class Slider : UIElement
{
    public Asset<Texture2D> InnerTexture { get; set; }

    public Asset<Texture2D> BlipTexture { get; set; }

    public Color InnerColor { get; set; }

    public float Ratio { get; set; }

    public event Action<Slider>? OnChanged;

    protected bool IsHeld;

    public Slider()
    {
        Width.Set(0, 1f);
        Height.Set(16, 0f);

        InnerColor = Color.Gray;

        InnerTexture = Assets.Images.UI.Gradient.Asset;
        BlipTexture = TextureAssets.ColorSlider;
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

        Rectangle dims = this.Dimensions;

        if (IsHeld)
        {
            float oldRatio = Ratio;

            float num = Main.mouseX - dims.X;
            Ratio = Math.Clamp(num / dims.Width, 0f, 1f);

            if (oldRatio != Ratio)
            {
                OnChanged?.Invoke(this);
            }
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        Rectangle dims = this.Dimensions;

        Texture2D slider = Assets.Images.UI.Slider.Asset.Value;
        Texture2D sliderOutline = Assets.Images.UI.SliderHighlight.Asset.Value;

        DrawBar(slider, Color.White);

        if (IsHeld || IsMouseHovering)
        {
            DrawBar(sliderOutline, Main.OurFavoriteColor);
        }

        dims.Inflate(-4, -4);
        spriteBatch.Draw(InnerTexture.Value, dims, InnerColor);

        Texture2D blip = BlipTexture.Value;

        Vector2 blipOrigin = blip.Size() * .5f;
        Vector2 blipPosition = new(dims.X + Ratio * dims.Width, dims.Center.Y);

        spriteBatch.Draw(blip, blipPosition, null, Color.White, 0f, blipOrigin, 1f, 0, 0f);

        return;

        void DrawBar(Texture2D texture, Color color)
        {
            spriteBatch.Draw(texture, new Rectangle(dims.X, dims.Y, 6, dims.Height), new(0, 0, 6, texture.Height), color);
            spriteBatch.Draw(texture, new Rectangle(dims.X + 6, dims.Y, dims.Width - 12, dims.Height), new(6, 0, 2, texture.Height), color);
            spriteBatch.Draw(texture, new Rectangle(dims.X + dims.Width - 6, dims.Y, 6, dims.Height), new(8, 0, 6, texture.Height), color);
        }
    }
}

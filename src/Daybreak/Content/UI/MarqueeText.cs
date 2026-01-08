using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Content.UI;

// TODO: Clean-up to match InputField
internal sealed class MarqueeText<T> : UIElement
{
    private readonly T text;

    public string Text
    {
        get
        {
            return text?.ToString() ?? string.Empty;
        }
    }

    public float TextAlignX { get; set; } = 0f;

    public float TextScale { get; set; }

    public Color TextColor { get; set; }

    public bool Large { get; set; }

    public float ScrollSpeed { get; set; }

    private float scroll;
    private int scrollDirection = 1;

    public MarqueeText(T text, float scale = 1f, bool large = false)
    {
        this.text = text;

        TextScale = scale;

        Large = large;

        ScrollSpeed = 1f;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        DynamicSpriteFont font = Large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;

        Vector2 textSize = font.MeasureString(Text) * new Vector2(TextScale);

        if (textSize.X >= this.Dimensions.Width)
        {
            const float scroll_increment = 3f;

            scroll += scroll_increment * ScrollSpeed * scrollDirection;

            if (scroll >= textSize.X)
            {
                scrollDirection = -1;
            }
            else if (scroll <= 0f)
            {
                scrollDirection = 1;
            }
        }
        else
        {
            scroll = textSize.X * TextAlignX;
            scrollDirection = 1;
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        spriteBatch.End(out var ss);

        var oldScissor = spriteBatch.GraphicsDevice.ScissorRectangle;
        spriteBatch.GraphicsDevice.ScissorRectangle = GetClippingRectangle(spriteBatch);

        var dims = this.Dimensions;

        spriteBatch.Begin(ss with { RasterizerState = OverflowHiddenRasterizerState });
        {
            var font = FontAssets.MouseText.Value;
            var position = new Vector2(dims.X + dims.Width * TextAlignX + 2f, dims.Y + dims.Height * 0.5f + 4);
            var textSize = font.MeasureString(Text);
            var origin = new Vector2(textSize.X * TextAlignX, textSize.Y * 0.5f);

            if (textSize.X >= dims.Width)
            {
                var offset = scroll * TextScale;

                var width = Math.Sign(offset) <= 0
                    ? (dims.Width * TextAlignX)
                    : (dims.Width * (1f - TextAlignX));

                offset = Utils.Remap(Math.Abs(offset), width, textSize.X, 0f, textSize.X - width) * Math.Sign(offset);
                {
                    position.X -= offset;
                }
            }

            ChatManager.DrawColorCodedStringWithShadow(
                spriteBatch,
                font,
                Text,
                position,
                Color.White,
                0f,
                origin,
                new Vector2(TextScale)
            );
        }
        spriteBatch.End();

        spriteBatch.GraphicsDevice.ScissorRectangle = oldScissor;

        spriteBatch.Begin(in ss);
    }
}

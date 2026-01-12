using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;
using static System.Net.Mime.MediaTypeNames;

namespace Daybreak.Content.UI;

// TODO: Clean-up to match InputField
internal class MarqueeText<T> : UIElement
{
    private T text;

    public string Text
    {
        get
        {
            return text?.ToString() ?? string.Empty;
        }
    }

    public float TextAlignX { get; set; } = 0f;

    public float MaxTextScale { get; set; }

    public Color TextColor { get; set; }

    public bool Large { get; set; }

    public float ScrollSpeed { get; set; } = 1f;

    private float textScale;

    private float scroll;

    private int scrollTimer;

    private int scrollDirection = 1;

    public MarqueeText(T text, float scale = 1f, bool large = false)
    {
        this.text = text;

        MaxTextScale = scale;

        Large = large;
    }

    public override void Recalculate()
    {
        base.Recalculate();

        SetText(text);
    }

    public void SetText(T text)
    {
        this.text = text;

        DynamicSpriteFont font = Large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;

        Vector2 textSize = font.MeasureString(Text) * new Vector2(MaxTextScale);

        var dims = this.InnerDimensions;

        textScale = MathHelper.Min(dims.Height / textSize.Y, MaxTextScale);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        DynamicSpriteFont font = Large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;

        Vector2 textSize = font.MeasureString(Text) * new Vector2(MaxTextScale);

        var dims = this.InnerDimensions;

        if (textSize.X >= dims.Width)
        {
            const float scroll_increment = 1f;

            const int scroll_delay = 40;

            // Each half of the text seperated by the alignment.
            var left =
                (textSize.X * TextAlignX) -
                (dims.Width * TextAlignX);

            var right =
                (textSize.X * (1f - TextAlignX)) -
                (dims.Width * (1f - TextAlignX));

            scrollTimer--;

            if (scrollTimer > 0)
            {
                return;
            }

            scroll += scroll_increment * ScrollSpeed * scrollDirection;

            if (scroll >= right)
            {
                scroll = right;
                scrollTimer = scroll_delay;
                scrollDirection = -1;
            }
            else if (scroll <= -left)
            {
                scroll = -left;
                scrollTimer = scroll_delay;
                scrollDirection = 1;
            }
        }
        else
        {
            scroll = 0;
            scrollTimer = 0;
            scrollDirection = 1;
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        spriteBatch.End(out var ss);

        var oldScissor = spriteBatch.GraphicsDevice.ScissorRectangle;
        spriteBatch.GraphicsDevice.ScissorRectangle = GetClippingRectangle(spriteBatch);

        var dims = this.InnerDimensions;

        spriteBatch.Begin(ss with { RasterizerState = OverflowHiddenRasterizerState });
        {
            var font = FontAssets.MouseText.Value;
            var position = new Vector2(dims.X + dims.Width * TextAlignX + 2f, dims.Y + dims.Height * 0.5f + 4);
            var textSize = font.MeasureString(Text);
            var origin = new Vector2(textSize.X * TextAlignX, textSize.Y * 0.5f);

            if (textSize.X >= dims.Width)
            {
                var offset = scroll * textScale;

                position.X -= offset;
            }

            ChatManager.DrawColorCodedStringWithShadow(
                spriteBatch,
                font,
                Text,
                position,
                Color.White,
                0f,
                origin,
                new Vector2(textScale)
            );
        }
        spriteBatch.End();

        spriteBatch.GraphicsDevice.ScissorRectangle = oldScissor;

        spriteBatch.Begin(in ss);
    }
}

using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.UI;

public class MarqueeText<T> : UIElement
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

    public float TextAlignY { get; set; } = 0f;

    public float MaxTextScale { get; set; }

    public Color TextColor { get; set; }

    public bool Large { get; set; }

    public float ScrollSpeed { get; set; } = 1f;

    public bool OnlyScrollOnHover { get; set; } = true;

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

        const float margin = 15f;

        DynamicSpriteFont font = Large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;

        Vector2 textSize = ChatManager.GetStringSize(font, Text, new Vector2(textScale));
        textSize.X += margin * textScale;

        var dims = this.InnerDimensions;

        bool shouldScroll = textSize.X >= dims.Width;

        if (OnlyScrollOnHover)
        {
            shouldScroll &= IsMouseHovering;
        }

        if (shouldScroll)
        {
            const float scroll_increment = 1.5f;

            const int scroll_delay = 30;

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
            var position = new Vector2(dims.X + dims.Width * TextAlignX + 2f, dims.Y + dims.Height * TextAlignY + 4);
            var textSize = ChatManager.GetStringSize(font, Text, Vector2.One);
            var origin = new Vector2(textSize.X * TextAlignX, textSize.Y * TextAlignY);

            if (textSize.X >= dims.Width)
            {
                var offset = scroll * textScale;

                position.X -= offset;
            }

            // Chat tags don't correctly account for origin nor scale/rotation.
            position -= origin;

            ChatManager.DrawColorCodedStringWithShadow(
                spriteBatch,
                font,
                Text,
                position,
                Color.White,
                0f,
                Vector2.Zero,
                new Vector2(textScale)
            );
        }
        spriteBatch.End();

        spriteBatch.GraphicsDevice.ScissorRectangle = oldScissor;

        spriteBatch.Begin(in ss);
    }
}

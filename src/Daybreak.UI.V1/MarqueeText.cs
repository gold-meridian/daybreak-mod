using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.UI;
using Terraria.UI.Chat;
using Daybreak.Rendering.V1;
using Terraria.GameContent.UI.Elements;

namespace Daybreak.UI.V1;

/// <summary>
///     Identical to <see cref="UIText"/>, with generic features of <see cref="UITextPanel{T}"/>;
///     but scrolls text horizontally when overflowing.
/// </summary>
public class MarqueeText<T> : UIElement
{
    private T text;

    public string Text => text?.ToString() ?? string.Empty;

    /// <summary>
    ///     The horizontal origin of the drawn text in the range 0-1.
    /// </summary>
    public float TextAlignX { get; set; } = 0f;

    /// <summary>
    ///     The vertical origin of the drawn text in the range 0-1.
    /// </summary>
    public float TextAlignY { get; set; } = 0f;

    /// <summary>
    ///     Max scale the text should be drawn with. May draw at lower scales
    ///     depending on the height of this element.
    /// </summary>
    public float MaxTextScale { get; set; }

    /// <summary>
    ///     Color of the drawn text.
    /// </summary>
    public Color TextColor { get; set; }

    /// <summary>
    ///     Outline color of the drawn text.
    /// </summary>
    public Color TextShadowColor { get; set; }

    /// <summary>
    ///     If <see cref="FontAssets.DeathText"/>
    /// </summary>
    public bool Large { get; set; }

    /// <summary>
    ///     Speed of the overflow text scroll in pixels per frame.
    /// </summary>
    public float ScrollSpeed { get; set; } = 1.5f;

    /// <summary>
    ///     If the text should only scroll if the element is being hovered.
    /// </summary>
    public bool OnlyScrollOnHover { get; set; } = true;

    private float textScale;

    private float scroll;

    private int scrollTimer;

    private int scrollDirection = 1;

    /// <inheritdoc/>
    public MarqueeText(T text, float scale = 1f, bool large = false)
    {
        this.text = text;

        MaxTextScale = scale;

        Large = large;

        TextColor = Color.White;

        PaddingLeft = 4f;
        PaddingRight = 4f;
    }

    /// <inheritdoc/>
    public override void Recalculate()
    {
        base.Recalculate();

        SetText(text);
    }

    public void SetText(T text, float scale = -1f, bool large = false)
    {
        this.text = text;

        if (scale >= 0f)
        {
            MaxTextScale = scale;
        }

        Large = large;

        var font = Large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;

        var textSize = font.MeasureString(Text) * new Vector2(MaxTextScale);

        var dims = this.InnerDimensions;

        textScale = MathHelper.Min(dims.Height / textSize.Y, MaxTextScale);
    }

    /// <inheritdoc/>
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var font = Large ? FontAssets.DeathText.Value : FontAssets.MouseText.Value;

        var textSize = ChatManager.GetStringSize(font, Text, new Vector2(textScale));

        var dims = this.InnerDimensions;

        var shouldScroll = textSize.X >= dims.Width;

        if (OnlyScrollOnHover)
        {
            shouldScroll &= IsMouseHovering;
        }

        if (shouldScroll)
        {
            const int scroll_delay = 30;

            // Each half of the text separated by the alignment
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

            scroll += ScrollSpeed * scrollDirection;

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

    /// <inheritdoc/>
    protected override void DrawSelf(SpriteBatch sb)
    {
        base.DrawSelf(sb);

        sb.End(out var ss);

        var oldScissor = sb.GraphicsDevice.ScissorRectangle;
        sb.GraphicsDevice.ScissorRectangle = GetClippingRectangle(sb);

        var dims = this.InnerDimensions;

        sb.Begin(ss with { RasterizerState = OverflowHiddenRasterizerState });
        {
            var font = FontAssets.MouseText.Value;
            var position = new Vector2(dims.X + (dims.Width * TextAlignX), dims.Y + (dims.Height * TextAlignY) + 4);

            var textSize = ChatManager.GetStringSize(font, Text, Vector2.One);

            var origin = new Vector2(textSize.X * TextAlignX, textSize.Y * TextAlignY);

            if (textSize.X * textScale >= dims.Width)
            {
                var offset = scroll;

                position.X -= offset;
            }

            // Chat tags don't correctly account for origin nor scale/rotation
            // TODO: See if this is still needed.
            position -= origin * textScale;

            ChatManager.DrawColorCodedStringWithShadow(
                sb,
                font,
                Text,
                position,
                TextColor,
                TextShadowColor,
                0f,
                Vector2.Zero,
                new Vector2(textScale)
            );
        }
        sb.End();

        sb.GraphicsDevice.ScissorRectangle = oldScissor;

        sb.Begin(in ss);
    }
}

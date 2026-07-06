using System;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Daybreak.Rendering;

/// <summary>
///     Represents a point in a rectangle.
/// </summary>
public readonly struct Origin
{
    private enum OriginAnchor : byte
    {
        None = 0,
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        Center,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
    }

    private static readonly Vector2[] origins =
    [
        new(0f, 0f),     // TopLeft
        new(0.5f, 0f),   // TopCenter
        new(1f, 0f),     // TopRight
        new(0f, 0.5f),   // MiddleLeft
        new(0.5f, 0.5f), // Center
        new(1f, 0.5f),   // MiddleRight
        new(0f, 1f),     // BottomLeft
        new(0.5f, 1f),   // BottomCenter
        new(1f, 1f),     // BottomRight
    ];

    private readonly Vector2? origin;
    private readonly OriginAnchor anchor;
    private readonly Func<Vector2, Vector2> originProvider = DefaultProvider;

    /// <summary>
    ///     The top-left corner, i.e. <see cref="Vector2.Zero"/>.
    /// </summary>
    public static Origin TopLeft { get; } = new(OriginAnchor.TopLeft);

    /// <summary>
    ///     The horizontal center of the top edge.
    /// </summary>
    public static Origin TopCenter { get; } = new(OriginAnchor.TopCenter);

    /// <summary>
    ///     The top-right corner.
    /// </summary>
    public static Origin TopRight { get; } = new(OriginAnchor.TopRight);

    /// <summary>
    ///     The vertical center of the left edge.
    /// </summary>
    public static Origin MiddleLeft { get; } = new(OriginAnchor.MiddleLeft);

    /// <summary>
    ///     The absolute center.
    /// </summary>
    public static Origin Center { get; } = new(OriginAnchor.Center);

    /// <summary>
    ///     The vertical center of the right edge.
    /// </summary>
    public static Origin MiddleRight { get; } = new(OriginAnchor.MiddleRight);

    /// <summary>
    ///     The bottom-left corner.
    /// </summary>
    public static Origin BottomLeft { get; } = new(OriginAnchor.BottomLeft);

    /// <summary>
    ///     The horizontal center of the bottom edge.
    /// </summary>
    public static Origin BottomCenter { get; } = new(OriginAnchor.BottomCenter);

    /// <summary>
    ///     The bottom-right corner.
    /// </summary>
    public static Origin BottomRight { get; } = new(OriginAnchor.BottomRight);

    /// <summary>
    ///     Constructs an origin from a definite point.
    /// </summary>
    public Origin(Vector2 origin)
    {
        this.origin = origin;
    }

    /// <summary>
    ///     Constructs an origin from a provider function.
    /// </summary>
    public Origin(Func<Vector2, Vector2> originProvider)
    {
        this.originProvider = originProvider;
    }

    private Origin(OriginAnchor anchor)
    {
        this.anchor = anchor;
    }

    /// <summary>
    ///     Resolves an origin point for the given rectangle (represented as
    ///     <paramref name="size"/>).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector2 GetOrigin(Vector2 size)
    {
        if (origin is not null)
        {
            return origin.Value;
        }

        if (anchor != OriginAnchor.None)
        {
            return size * origins[(int)anchor - 1];
        }

        return originProvider(size);
    }

    /// <summary>
    ///     Automatically constructs an origin from a definite point.
    /// </summary>
    public static implicit operator Origin(Vector2 origin)
    {
        return new Origin(origin);
    }

    /// <summary>
    ///     Automatically constructs an origin from a provider function.
    /// </summary>
    public static implicit operator Origin(Func<Vector2, Vector2> originProvider)
    {
        return new Origin(originProvider);
    }

    private static Vector2 DefaultProvider(Vector2 size)
    {
        return size;
    }
}

/// <summary>
///     Extensions for <see cref="Origin"/>.
/// </summary>
public static class OriginExtensions
{
    extension(SpriteBatch sb)
    {
#region Draw
        /// <inheritdoc cref="SpriteBatch.Draw(Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float)"/>
        public void Draw(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Origin origin,
            float scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            var size = sourceRectangle?.Size() ?? texture.Size();

            sb.Draw(
                texture,
                position,
                sourceRectangle,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth
            );
        }

        /// <inheritdoc cref="SpriteBatch.Draw(Texture2D, Vector2, Rectangle?, Color, float, Vector2, Vector2, SpriteEffects, float)"/>
        public void Draw(
            Texture2D texture,
            Vector2 position,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Origin origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            var size = sourceRectangle?.Size() ?? texture.Size();

            sb.Draw(
                texture,
                position,
                sourceRectangle,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth
            );
        }

        /// <inheritdoc cref="SpriteBatch.Draw(Texture2D, Rectangle, Rectangle?, Color, float, Vector2, SpriteEffects, float)"/>
        public void Draw(
            Texture2D texture,
            Rectangle destinationRectangle,
            Rectangle? sourceRectangle,
            Color color,
            float rotation,
            Origin origin,
            SpriteEffects effects,
            float layerDepth
        )
        {
            var size = sourceRectangle?.Size() ?? texture.Size();

            sb.Draw(
                texture,
                destinationRectangle,
                sourceRectangle,
                color,
                rotation,
                origin.GetOrigin(size),
                effects,
                layerDepth
            );
        }
#endregion

#region DrawString
        /// <inheritdoc cref="SpriteBatch.DrawString(SpriteFont, StringBuilder, Vector2, Color, float, Vector2, float, SpriteEffects, float)"/>
        public void DrawString(
            SpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color,
            float rotation,
            Origin origin,
            float scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            var size = spriteFont.MeasureString(text);

            sb.DrawString(
                spriteFont,
                text,
                position,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth
            );
        }

        /// <inheritdoc cref="SpriteBatch.DrawString(SpriteFont, StringBuilder, Vector2, Color, float, Vector2, Vector2, SpriteEffects, float)"/>
        public void DrawString(
            SpriteFont spriteFont,
            StringBuilder text,
            Vector2 position,
            Color color,
            float rotation,
            Origin origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            var size = spriteFont.MeasureString(text);

            sb.DrawString(
                spriteFont,
                text,
                position,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth
            );
        }

        /// <inheritdoc cref="SpriteBatch.DrawString(SpriteFont, string, Vector2, Color, float, Vector2, float, SpriteEffects, float)"/>
        public void DrawString(
            SpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Origin origin,
            float scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            var size = spriteFont.MeasureString(text);

            sb.DrawString(
                spriteFont,
                text,
                position,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth
            );
        }

        /// <inheritdoc cref="SpriteBatch.DrawString(SpriteFont, string, Vector2, Color, float, Vector2, Vector2, SpriteEffects, float)"/>
        public void DrawString(
            SpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Origin origin,
            Vector2 scale,
            SpriteEffects effects,
            float layerDepth
        )
        {
            var size = spriteFont.MeasureString(text);

            sb.DrawString(
                spriteFont,
                text,
                position,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth
            );
        }
#endregion
    }
}

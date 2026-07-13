using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI.Chat;

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
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public static class OriginExtensions
{
    extension(SpriteBatch sb)
    {
#region Draw
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

#region ReLogic DrawString
        public void DrawString(
            DynamicSpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Origin origin,
            float scale,
            SpriteEffects effects,
            float layerDepth,
            Vector2[]? charOffsets = null,
            Color[]? charColors = null
        )
        {
            var size = spriteFont.MeasureString(text);

            DynamicSpriteFontExtensionMethods.DrawString(
                sb,
                spriteFont,
                text,
                position,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth,
                charOffsets,
                charColors
            );
        }

        public void DrawString(
            DynamicSpriteFont spriteFont,
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
            var realText = text.ToString();
            var size = spriteFont.MeasureString(realText);

            DynamicSpriteFontExtensionMethods.DrawString(
                sb,
                spriteFont,
                realText,
                position,
                color,
                rotation,
                origin.GetOrigin(size),
                scale,
                effects,
                layerDepth
            );
        }

        public void DrawString(
            DynamicSpriteFont spriteFont,
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

            DynamicSpriteFontExtensionMethods.DrawString(
                sb,
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

        public void DrawString(
            DynamicSpriteFont spriteFont,
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
            var realText = text.ToString();
            var size = spriteFont.MeasureString(realText);

            DynamicSpriteFontExtensionMethods.DrawString(
                sb,
                spriteFont,
                realText,
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

#region ChatManager DrawColorCodedString
    extension(ChatManager)
    {
        public static void DrawColorCodedStringShadow(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            IEnumerable<TextSnippet> snippets,
            Vector2 position,
            Color shadowColor,
            float rotation,
            Origin origin,
            Vector2 scale,
            float maxWidth = -1f,
            float spread = 2f
        )
        {
            snippets = snippets.ToArray();

            var size = ChatManager.GetStringSize(font, snippets, scale, maxWidth);

            ChatManager.DrawColorCodedStringShadow(
                spriteBatch,
                font,
                snippets,
                position,
                shadowColor,
                rotation,
                origin.GetOrigin(size),
                scale,
                maxWidth,
                spread
            );
        }

        public static void DrawColorCodedStringShadow(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            List<PositionedSnippet> snippets,
            Vector2 position,
            Color shadowColor,
            float rotation,
            Origin origin,
            Vector2 scale,
            float spread = 2f
        )
        {
            var size = ChatManager.GetStringSize(snippets);

            ChatManager.DrawColorCodedStringShadow(
                spriteBatch,
                font,
                snippets,
                position,
                shadowColor,
                rotation,
                origin.GetOrigin(size),
                scale,
                spread
            );
        }

        public static void DrawColorCodedString(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            IEnumerable<TextSnippet> snippets,
            Vector2 position,
            Color baseColor,
            float rotation,
            Origin origin,
            Vector2 scale,
            out int hoveredSnippet,
            float maxWidth = -1f,
            bool ignoreColors = false
        )
        {
            snippets = snippets.ToArray();

            var size = ChatManager.GetStringSize(font, snippets, scale, maxWidth);

            ChatManager.DrawColorCodedString(
                spriteBatch,
                font,
                snippets,
                position,
                rotation,
                origin.GetOrigin(size),
                scale,
                out hoveredSnippet,
                maxWidth
            );
        }

        public static void DrawColorCodedString(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            IEnumerable<TextSnippet> snippets,
            Vector2 position,
            float rotation,
            Origin origin,
            Vector2 scale,
            out int hoveredSnippet,
            float maxWidth = -1f
        )
        {
            snippets = snippets.ToArray();

            var size = ChatManager.GetStringSize(font, snippets, scale, maxWidth);

            ChatManager.DrawColorCodedString(
                spriteBatch,
                font,
                snippets,
                position,
                rotation,
                origin.GetOrigin(size),
                scale,
                out hoveredSnippet,
                maxWidth
            );
        }

        public static void DrawColorCodedString(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            IEnumerable<PositionedSnippet> snippets,
            Vector2 position,
            float rotation,
            Origin origin,
            Vector2 scale,
            out int hoveredSnippet,
            Color? colorOverride = null
        )
        {
            snippets = snippets.ToArray();

            var size = ChatManager.GetStringSize(snippets);

            ChatManager.DrawColorCodedString(
                spriteBatch,
                font,
                snippets,
                position,
                rotation,
                origin.GetOrigin(size),
                scale,
                out hoveredSnippet,
                colorOverride
            );
        }

        public static void DrawColorCodedStringWithShadow(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            TextSnippet[] snippets,
            Vector2 position,
            float rotation,
            Origin origin,
            Vector2 baseScale,
            out int hoveredSnippet,
            float maxWidth = -1f,
            float spread = 2f
        )
        {
            var size = ChatManager.GetStringSize(font, snippets, baseScale, maxWidth);

            ChatManager.DrawColorCodedStringWithShadow(
                spriteBatch,
                font,
                snippets,
                position,
                rotation,
                origin.GetOrigin(size),
                baseScale,
                out hoveredSnippet,
                maxWidth,
                spread
            );
        }

        public static void DrawColorCodedStringWithShadow(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            TextSnippet[] snippets,
            Vector2 position,
            Color color,
            float rotation,
            Origin origin,
            Vector2 baseScale,
            out int hoveredSnippet,
            float maxWidth = -1f,
            float spread = 2f
        )
        {
            var size = ChatManager.GetStringSize(font, snippets, baseScale, maxWidth);

            ChatManager.DrawColorCodedStringWithShadow(
                spriteBatch,
                font,
                snippets,
                position,
                color,
                rotation,
                origin.GetOrigin(size),
                baseScale,
                out hoveredSnippet,
                maxWidth,
                spread
            );
        }

        public static void DrawColorCodedStringShadow(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            string text,
            Vector2 position,
            Color baseColor,
            float rotation,
            Origin origin,
            Vector2 baseScale,
            float maxWidth = -1f,
            float spread = 2f,
            bool useRawStringSize = false
        )
        {
            var size = useRawStringSize
                ? font.MeasureString(text)
                : ChatManager.GetStringSize(font, text, baseScale, maxWidth);

            ChatManager.DrawColorCodedStringShadow(
                spriteBatch,
                font,
                text,
                position,
                baseColor,
                rotation,
                origin.GetOrigin(size),
                baseScale,
                maxWidth,
                spread
            );
        }

        public static Vector2 DrawColorCodedString(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            string text,
            Vector2 position,
            Color baseColor,
            float rotation,
            Origin origin,
            Vector2 baseScale,
            float maxWidth = -1f,
            bool ignoreColors = false,
            bool useRawStringSize = false
        )
        {
            var size = useRawStringSize
                ? font.MeasureString(text)
                : ChatManager.GetStringSize(font, text, baseScale, maxWidth);

            return ChatManager.DrawColorCodedString(
                spriteBatch,
                font,
                text,
                position,
                baseColor,
                rotation,
                origin.GetOrigin(size),
                baseScale,
                maxWidth,
                ignoreColors
            );
        }

        public static void DrawColorCodedStringWithShadow(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            string text,
            Vector2 position,
            Color baseColor,
            float rotation,
            Origin origin,
            Vector2 scale,
            float maxWidth = -1f,
            float spread = 2f,
            bool useRawStringSize = false
        )
        {
            var size = useRawStringSize
                ? font.MeasureString(text)
                : ChatManager.GetStringSize(font, text, scale, maxWidth);

            ChatManager.DrawColorCodedStringWithShadow(
                spriteBatch,
                font,
                text,
                position,
                baseColor,
                rotation,
                origin.GetOrigin(size),
                scale,
                maxWidth,
                spread
            );
        }
    }
#endregion
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

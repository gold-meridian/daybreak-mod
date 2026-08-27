using System;
using Daybreak.Mathematics.V1;
using Daybreak.Rendering.V1;
using Daybreak.Resources.V1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace Daybreak.ChatTags.V1;

/// <summary>
///     A tag that provides common text formatting utilities.
/// </summary>
public sealed class FormattingTag : ChatTag
{
    private readonly record struct Options(
        bool Bold,
        bool Italic,
        bool Underline,
        bool Strikethrough
    )
    {
        public static Options Parse(ReadOnlySpan<char> text)
        {
            var bold = false;
            var italic = false;
            var underline = false;
            var strikethrough = false;
            foreach (var opt in text)
            {
                switch (opt)
                {
                    case 'b':
                    {
                        bold = true;
                        break;
                    }

                    case 'i':
                    {
                        italic = true;
                        break;
                    }

                    case 'u':
                    {
                        underline = true;
                        break;
                    }

                    case 's':
                    {
                        strikethrough = true;
                        break;
                    }
                }
            }

            return new Options(
                bold,
                italic,
                underline,
                strikethrough
            );
        }
    }

    private sealed class Snippet : TextSnippet, IUniqueDrawString
    {
        private readonly Options options;

        public Snippet(Options options, string text = "") : base(text)
        {
            this.options = options;
        }

        public Snippet(Options options, string text, Color color) : base(text, color)
        {
            this.options = options;
        }

        public bool UniqueDrawString(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            bool justCheckingSize,
            out Vector2 size
        )
        {
            var textSize = font.MeasureString(text) * scale;
            {
                size = textSize;
            }

            if (options.Underline)
            {
                DrawLine(new Vector2(0, textSize.Y * 0.6f));
            }

            if (options.Strikethrough)
            {
                DrawLine(new Vector2(0, textSize.Y * 0.35f));
            }

            if (options.Italic)
            {
                DrawItalicText();
            }
            else
            {
                spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0f);

                if (options.Bold)
                {
                    DrawBoldOutline();
                }
            }

            return true;

            void DrawBoldOutline()
            {
                const int directions = 4;
                const float distance = 0.25f;

                for (var i = 0; i < directions; i++)
                {
                    var offset = new Vector2(distance, 0).RotatedBy(MathF.Tau * ((float)i / directions));

                    offset *= scale;

                    spriteBatch.DrawString(font, text, position + offset, color, rotation, origin, scale, SpriteEffects.None, 0f);
                }
            }

            void DrawItalicText()
            {
                const float skew_angle = -17f;

                var angle = Angle.FromDegrees(skew_angle);

                spriteBatch.End(out var snapshot);

                // Skew should base based on the bottom of the characters.
                var offset = (textSize.Y * 0.6f) - origin.Y;

                /*
                 * 1, tan(a), 0, 0,
                 * 0, 1,      0, 0,
                 * 0, 0,      1, 0,
                 * 0, 0,      0, 1
                 */
                var skew = Matrix.Identity;
                skew.M21 += MathF.Tan(angle.Radians);

                var matrix =
                    Matrix.CreateTranslation(new Vector3(-position, 0f)) *
                    Matrix.CreateRotationZ(-rotation) *
                    Matrix.CreateTranslation(0f, -offset, 0f) *
                    skew *
                    Matrix.CreateTranslation(0f, offset, 0f) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(new Vector3(position, 0f)) *
                    snapshot.TransformMatrix;

                spriteBatch.Begin(snapshot with { TransformMatrix = matrix });

                spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0f);

                if (options.Bold)
                {
                    DrawBoldOutline();
                }

                spriteBatch.Restart(snapshot);
            }

            void DrawLine(Vector2 offset)
            {
                const float size_ratio = 0.1f;

                /*
                var texture = Assets.Images.Formatting.RoundedLine.Asset;
                {
                    texture.Wait();
                }
                */

                const int texture_height = 9;
                var texture = TextureAssets.MagicPixel.ImmediateValue;

                var size = textSize.Y * size_ratio;

                var edgeSize = (int)(3 * scale.X);
                var height = (int)size;

                var matrix =
                    Matrix.CreateTranslation(new Vector3(-origin * scale, 0f)) *
                    Matrix.CreateRotationZ(rotation);

                var leftPosition = Vector2.Transform(offset, matrix) + position;

                var leftDest = new Rectangle(
                    (int)leftPosition.X,
                    (int)leftPosition.Y,
                    edgeSize,
                    height
                );
                var leftSource = new Rectangle(0, 0, 3, texture_height);

                var middlePosition = Vector2.Transform(new Vector2(edgeSize, 0) + offset, matrix) + position;

                var middleDest = new Rectangle(
                    (int)middlePosition.X,
                    (int)middlePosition.Y,
                    (int)textSize.X - (edgeSize * 2),
                    height
                );
                var middleSource = new Rectangle(3, 0, 1, texture_height);

                var rightPosition = Vector2.Transform(new Vector2(textSize.X - edgeSize, 0) + offset, matrix) + position;

                var rightDest = new Rectangle(
                    (int)rightPosition.X,
                    (int)rightPosition.Y,
                    edgeSize,
                    height
                );
                var rightSource = new Rectangle(4, 0, 3, texture_height);

                spriteBatch.Draw(texture, leftDest, leftSource, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, middleDest, middleSource, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture, rightDest, rightSource, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        public override Color GetVisibleColor()
        {
            return Color;
        }
    }

    private static string Prefix => ModuleLoader.OwningMod is "Daybreak" or null ? "" : ModuleLoader.OwningMod + '_';

    /// <inheritdoc />
    public override string TagName { get; } = $"{Prefix}dbf";

    /// <inheritdoc />
    public override TextSnippet Parse(string text, Color baseColor = new(), string? options = null)
    {
        if (string.IsNullOrEmpty(options))
        {
            return new TextSnippet(text, baseColor);
        }

        var formatting = Options.Parse(options);
        return new Snippet(formatting, text, baseColor);
    }
}

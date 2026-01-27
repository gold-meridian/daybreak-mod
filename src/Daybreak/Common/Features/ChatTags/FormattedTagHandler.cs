using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Mathematics;
using Daybreak.Common.Rendering;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Graphics;
using System;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.ChatTags;

internal sealed class FormattedTagHandler : ILoadableTagHandler<FormattedTagHandler>
{
    private record struct FormattingOptions(bool Bold, bool Italic, bool Underline, bool Strikethrough)
    {
        public static FormattingOptions Parse(string text)
        {
            var arr = text.Split(',');

            var options = new FormattingOptions();

            foreach (var opt in arr)
            {
                switch (opt[0])
                {
                    case 'b':
                    {
                        options.Bold = true;
                        break;
                    }
                    case 'i':
                    {
                        options.Italic = true;
                        break;
                    }
                    case 'u':
                    {
                        options.Underline = true;
                        break;
                    }
                    case 's':
                    {
                        options.Strikethrough = true;
                        break;
                    }
                }
            }

            return options;
        }
    }

    [OnLoad]
    private static void Load()
    {
        IL_ChatManager.DrawColorCodedString_SpriteBatch_DynamicSpriteFont_TextSnippetArray_Vector2_Color_float_Vector2_Vector2_refInt32_float_bool += DrawColorCodedStringShadow_FormattedSnippets;
    }

    private static void DrawColorCodedStringShadow_FormattedSnippets(ILContext il)
    {
        var c = new ILCursor(il);

        ILLabel skipDrawStringTarget = c.DefineLabel();
        ILLabel skipFormattedTarget = c.DefineLabel();

        c.GotoNext(
            MoveType.Before,
            i => i.MatchCall(typeof(DynamicSpriteFontExtensionMethods), nameof(DynamicSpriteFontExtensionMethods.DrawString))
        );

        int textSnippetIndex = -1;

        c.FindPrev(
            out _,
            i => i.MatchLdloc(out textSnippetIndex),
            i => i.MatchLdfld<TextSnippet>(nameof(TextSnippet.Scale))
        );

        c.EmitLdloc(textSnippetIndex);
        c.EmitDelegate(static (TextSnippet snippet) => snippet is Snippet);

        c.EmitBrtrue(skipDrawStringTarget);

        c.GotoNext(
            MoveType.After,
            i => i.MatchCall(typeof(DynamicSpriteFontExtensionMethods), nameof(DynamicSpriteFontExtensionMethods.DrawString))
        );

        c.EmitBr(skipFormattedTarget);

        c.MarkLabel(skipDrawStringTarget);

        c.EmitLdloc(textSnippetIndex);

        c.EmitDelegate(
            static (SpriteBatch spriteBatch,
            DynamicSpriteFont spriteFont,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale,
            SpriteEffects _,
            float _,
            TextSnippet snippet
            ) =>
            {
                if (snippet is not Snippet format)
                {
                    return;
                }

                format.DrawString(spriteBatch, spriteFont, text, position, color, rotation, origin, scale);
            }
        );

        c.MarkLabel(skipFormattedTarget);
    }

    private sealed class Snippet : TextSnippet
    {
        private readonly FormattingOptions options;

        public Snippet(FormattingOptions options, string text = "") : base(text)
        {
            this.options = options;
        }

        public Snippet(FormattingOptions options, string text, Color color, float scale = 1f) : base(text, color, scale)
        {
            this.options = options;
        }

        public void DrawString(
            SpriteBatch spriteBatch,
            DynamicSpriteFont font,
            string text,
            Vector2 position,
            Color color,
            float rotation,
            Vector2 origin,
            Vector2 scale
        )
        {
            var textSize = font.MeasureString(text) * scale;

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

            if (options.Underline)
            {
                DrawLine(new(0, textSize.Y * 0.6f));
            }

            if (options.Strikethrough)
            {
                DrawLine(new(0, textSize.Y * 0.3f));
            }

            return;

            void DrawBoldOutline()
            {
                const int directions = 4;

                const float distance = 0.25f;

                for (int i = 0; i < directions; i++)
                {
                    Vector2 offset = new Vector2(distance, 0).RotatedBy(MathF.Tau * ((float)i / directions));

                    offset *= scale;

                    spriteBatch.DrawString(font, text, position + offset, color, rotation, origin, scale, SpriteEffects.None, 0f);
                }
            }

            void DrawItalicText()
            {
                spriteBatch.End(out var snapshot);

                const float skew_angle = -17f;

                const float x_offset = 5f;

                position.X += (skew_angle * 0.5f + x_offset) * scale.X;

                var angle = Angle.FromDegrees(skew_angle) * scale.X;

                var translation = new Vector3(position.X - origin.X, position.Y + font.MeasureString(text).Y - origin.Y, 0);

                /*
                 * 1,      0, 0, 0,
                 * tan(a), 1, 0, 0,
                 * 0,      0, 1, 0,
                 * 0,      0, 0, 1
                 */
                var skew = Matrix.Identity;
                skew.M21 = MathF.Tan(angle.Radians);

                var matrix = Matrix.CreateTranslation(-translation) * skew * Matrix.CreateTranslation(translation);

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

                AssetReferences.Assets.Images.Formatting.RoundedLine.Asset.Wait();

                var texture = AssetReferences.Assets.Images.Formatting.RoundedLine.Asset;

                float size = textSize.Y * size_ratio;

                int edgeSize = (int)(3 * scale.X);
                int height = (int)size;

                Matrix matrix =
                    Matrix.CreateTranslation(new(-position, 0)) *
                    Matrix.CreateTranslation((0f - origin.X) * textSize.X, (0f - origin.Y) * textSize.Y, 0f) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(new(position, 0));

                var leftPosition = Vector2.Transform(position + offset, matrix);

                var leftDest = new Rectangle(
                    (int)leftPosition.X,
                    (int)leftPosition.Y,
                    edgeSize,
                    height
                );
                var leftSource = new Rectangle(0, 0, 3, texture.Height());

                var middlePosition = Vector2.Transform(new Vector2(position.X + edgeSize, position.Y) + offset, matrix);

                var middleDest = new Rectangle(
                    (int)middlePosition.X,
                    (int)middlePosition.Y,
                    (int)textSize.X -(edgeSize * 2),
                    height
                );
                var middleSource = new Rectangle(3, 0, 1, texture.Height());

                var rightPosition = Vector2.Transform(new Vector2(position.X + textSize.X - edgeSize, position.Y) + offset, matrix);

                var rightDest = new Rectangle(
                    (int)rightPosition.X,
                    (int)rightPosition.Y,
                    edgeSize,
                    height
                );
                var rightSource = new Rectangle(4, 0, 3, texture.Height());

                spriteBatch.Draw(texture.Value, leftDest, leftSource, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture.Value, middleDest, middleSource, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
                spriteBatch.Draw(texture.Value, rightDest, rightSource, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }
        }

        public override Color GetVisibleColor()
        {
            return Color;
        }
    }

    public string[] TagNames { get; } = ["dbf"];

    TextSnippet ITagHandler.Parse(string text, Color baseColor, string? options)
    {
        if (string.IsNullOrEmpty(options))
        {
            return new TextSnippet(text, baseColor);
        }

        var formatting = FormattingOptions.Parse(options);

        return new Snippet(formatting, text, baseColor);
    }
}

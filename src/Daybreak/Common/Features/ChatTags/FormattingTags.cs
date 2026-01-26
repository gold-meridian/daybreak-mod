using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Mathematics;
using Daybreak.Common.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Graphics;
using System;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.ChatTags;

public abstract class FormattedSnippet : TextSnippet
{
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
        c.EmitDelegate(static (TextSnippet snippet) => snippet is FormattedSnippet);

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
                if (snippet is not FormattedSnippet format)
                {
                    return;
                }

                format.DrawString(spriteBatch, spriteFont, text, position, color, rotation, origin, scale);
            }
        );

        c.MarkLabel(skipFormattedTarget);

        MonoModHooks.DumpIL(ModContent.GetInstance<ModImpl>(), il);
    }

    public FormattedSnippet(string text = "") : base(text)
    { }

    public FormattedSnippet(string text, Color color, float scale = 1f) : base(text, color, scale)
    { }

    public abstract void DrawString(
        SpriteBatch spriteBatch,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale
    );

    public override Color GetVisibleColor()
    {
        return Color;
    }
}

internal sealed class ItalicTagHandler : ILoadableTagHandler<ItalicTagHandler>
{
    private sealed class Snippet : FormattedSnippet
    {
        public Snippet(string text = "") : base(text)
        { }

        public Snippet(string text, Color color, float scale = 1f) : base(text, color, scale)
        { }

        public override void DrawString(
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
            const float skew_angle = -25f;

            const float x_offset = 5f;

            position.X += (skew_angle * 0.5f + x_offset) * scale.X;

            var angle = Angle.FromDegrees(skew_angle);

            spriteBatch.End(out var snapshot);

            var translation = new Vector3(position.X - origin.X, (position.Y + font.MeasureString(text).Y) - origin.Y, 0);

            /*
             * 1,      0, 0, 0,
             * tan(a), 1, 0, 0,
             * 0,      0, 1, 0,
             * 0,      0, 0, 1
             */
            var skew = Matrix.Identity;
            skew.M21 = MathF.Tan(angle.Radians);

            var matrix = Matrix.CreateTranslation(-translation) * skew * Matrix.CreateTranslation(translation);

            spriteBatch.Begin(snapshot with { TransformMatrix = matrix * snapshot.TransformMatrix });

            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0f);

            spriteBatch.Restart(snapshot);
        }
    }

    public string[] TagNames { get; } = ["dbi"];

    TextSnippet ITagHandler.Parse(string text, Color baseColor, string? options)
    {
        return new Snippet(text, baseColor);
    }
}

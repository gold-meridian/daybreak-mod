using System.Text.RegularExpressions;
using Daybreak.Hooks.V1;
using Daybreak.MonoMod.V1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Graphics;
using Terraria.UI.Chat;

namespace Daybreak.ChatTags.V1;

/// <summary>
///     Provides a hook to <see cref="TextSnippet"/>s that mirrors the
///     functionality of <see cref="TextSnippet.UniqueDraw"/> with the
///     parameters necessary for string drawing.
///     <br />
///     <br />
///     Due to restrctions in how snippets are laid out, overriding
///     <see cref="TextSnippet.UniqueDraw"/> is still necessary if your unique
///     string rendering is not identical to the normal string size.
///     <br />
///     To achieve this, override <see cref="TextSnippet.UniqueDraw"/> and set
///     <c>size</c> as you normally would, but only return
///     <see langword="true"/> if <c>justCheckingSize</c> is
///     <see langword="true"/>.
/// </summary>
public interface IUniqueDrawString
{
    /// <summary>
    ///     Like <see cref="TextSnippet.UniqueDraw"/>, but with text rendering
    ///     information.
    /// </summary>
    bool UniqueDrawString(
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
    );
}

internal static partial class TextSnippets
{
    private static readonly Regex tag_format = TagFormatRegex();

    [OnLoad]
    private static void ApplyHooks()
    {
        IL_ChatManager.ParseMessage += OverrideVanillaRegex;
        IL_ChatManager.MayNeedParsing += OverrideVanillaRegex;
        IL_ChatManager.DrawColorCodedString_SpriteBatch_DynamicSpriteFont_IEnumerable1_Vector2_float_Vector2_Vector2_refInt32_Nullable1 += DrawColorCodedStringWithUniqueDrawString;
    }

    private static void OverrideVanillaRegex(ILContext il)
    {
        var c = new ILCursor(il);

        while (c.TryGotoNext(MoveType.After, x => x.MatchLdsfld(typeof(ChatManager.Regexes), nameof(ChatManager.Regexes.Format))))
        {
            c.EmitPop();
            c.EmitStaticDelegateUnsafe(() => tag_format);
        }
    }

    private static void DrawColorCodedStringWithUniqueDrawString(ILContext il)
    {
        var c = new ILCursor(il);

        var snippetIdx = VariableIndex.Invalid;
        c.GotoNext(x => x.MatchLdfld<PositionedSnippet>(nameof(PositionedSnippet.Snippet)));
        c.GotoNext(x => x.MatchStloc(out snippetIdx));

        var colorIdx = VariableIndex.Invalid;
        c.GotoNext(x => x.MatchLdarga(8)); // colorOverride
        c.GotoNext(x => x.MatchStloc(out colorIdx));

        var posIdx = VariableIndex.Invalid;
        c.GotoNext(x => x.MatchCallvirt<TextSnippet>(nameof(TextSnippet.UniqueDraw)));
        c.GotoPrev(x => x.MatchLdarg0());
        c.GotoNext(x => x.MatchLdloc(out posIdx));

        c.GotoNext(MoveType.After, x => x.MatchCallvirt<TextSnippet>(nameof(TextSnippet.UniqueDraw)));

        c.EmitLdloc((int)snippetIdx); // snippet
        c.EmitLdarg0();               // sb
        c.EmitLdarg1();               // font
        c.EmitLdloc((int)posIdx);     // pos
        c.EmitLdloc((int)colorIdx);   // color
        c.EmitLdarg(4);               // rotation
        c.EmitLdarg(5);               // origin
        c.EmitLdarg(6);               // scale
        c.EmitDelegate(
            (TextSnippet snippet, SpriteBatch sb, DynamicSpriteFont font, Vector2 pos, Color color, float rotation, Vector2 origin, Vector2 scale) =>
            {
                if (snippet is not IUniqueDrawString dsSnippet)
                {
                    return false;
                }

                return dsSnippet.UniqueDrawString(
                    sb,
                    font,
                    snippet.Text,
                    pos,
                    color,
                    rotation,
                    origin,
                    scale,
                    justCheckingSize: false,
                    size: out _
                );
            }
        );
        c.EmitOr();
    }

    // Changes to vanilla:
    // - made '_' a valid character (for mod separation),
    // - made max valid character count unlimited (from 10).
    [GeneratedRegex(@"(?<!\\)\[(?<tag>[a-zA-Z_]+)(\/(?<options>[^:]+))?:(?<text>.+?)(?<!\\)\]")]
    private static partial Regex TagFormatRegex();
}

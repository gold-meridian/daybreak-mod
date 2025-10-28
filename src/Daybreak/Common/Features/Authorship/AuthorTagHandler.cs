using System.Diagnostics.CodeAnalysis;
using Daybreak.Common.Features.ChatTags;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.Authorship;

internal sealed class AuthorTagHandler : ILoadableTagHandler<AuthorTagHandler>
{
    private sealed class Snippet(AuthorTag tag) : TextSnippet
    {
        public override bool UniqueDraw(
            bool justCheckingString,
            out Vector2 size,
            SpriteBatch spriteBatch,
            Vector2 position = default,
            Color color = default,
            float scale = 1
        )
        {
            if (!justCheckingString && color is { R: > 0, G: > 0, B: > 0 })
            {
                tag.DrawIcon(spriteBatch, position);
            }

            size = new Vector2(26f);
            return true;
        }

        public override float GetStringLength(DynamicSpriteFont font)
        {
            return 26f;
        }

        public override Color GetVisibleColor()
        {
            return Color.White;
        }
    }

    public string[] TagNames { get; } = ["nsa"];

    TextSnippet ITagHandler.Parse(string text, Color baseColor, string? options)
    {
        if (!TrySplitName(text, out var modName, out var tagName))
        {
            return new TextSnippet(text + "1");
        }

        if (!ModLoader.TryGetMod(modName, out var mod))
        {
            return new TextSnippet(text + "2");
        }

        if (!mod.TryFind<AuthorTag>(tagName, out var tag))
        {
            return new TextSnippet(text + "3");
        }

        return new Snippet(tag);

        static bool TrySplitName(
            string name,
            [NotNullWhen(returnValue: true)] out string? domain,
            [NotNullWhen(returnValue: true)] out string? subName
        )
        {
            var length = name.IndexOfAny(ModContent.nameSplitters);
            if (length < 0)
            {
                domain = null;
                subName = null;
                return false;
            }

            domain = name[..length];
            subName = name[(length + 1)..];
            return true;
        }
    }
}

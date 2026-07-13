using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Daybreak.ChatTags;

/// <summary>
///     An autoloaded chat tag.  Chat tags provide <see cref="TextSnippet"/>
///     parsed from a tag: <c>[name/opts:text]</c> (or, optionally,
///     <c>[name:text]</c>).
/// </summary>
public abstract class ChatTag : ModType, ITagHandler
{
    /// <summary>
    ///     The primary name of this chat tag.
    /// </summary>
    public abstract string TagName { get; }

    /// <summary>
    ///     Additional names for the tag.
    /// </summary>
    public virtual string[] AliasNames { get; } = [];

    /// <inheritdoc />
    protected sealed override void Register()
    {
        ChatTags.Register(this, TagName, AliasNames);
    }

    /// <inheritdoc />
    public sealed override void SetupContent()
    {
        base.SetupContent();

        SetStaticDefaults();
    }

    /// <summary>
    ///     Parses the text into the accompanying <see cref="TextSnippet"/>.
    /// </summary>
    /// <param name="text">The text to parse.  The body of the tag.</param>
    /// <param name="baseColor">The base color to render with.</param>
    /// <param name="options">
    ///     Any options that are included after the tag name.
    /// </param>
    /// <returns>
    ///     The parsed text snippet.
    /// </returns>
    public abstract TextSnippet Parse(
        string text,
        Color baseColor = new(),
        string? options = null
    );
}

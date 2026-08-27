using System.Text;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Authorship;

/// <summary>
///     Utilities for dealing with authorship text.
/// </summary>
public static class AuthorText
{
    /// <summary>
    ///     Creates a formatted tooltip containing the authors in a mod.
    /// </summary>
    public static string GetAuthorTooltip(Mod mod, string? headerText)
    {
        var sb = new StringBuilder();

        // sb.AppendLine();

        if (headerText is not null)
        {
            sb.AppendLine(headerText);
        }
        
        foreach (var authorTag in mod.GetContent<AuthorTag>())
        {
            sb.Append($"[nsa:{authorTag.FullName}]");
            sb.Append(' ');
            sb.AppendLine(authorTag.DisplayName.Value);
        }

        return sb.ToString();
    }
}

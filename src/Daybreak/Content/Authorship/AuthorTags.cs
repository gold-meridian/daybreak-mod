using System;
using Daybreak.Common.Features.Authorship;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Daybreak.Content.Authorship;

/// <summary>
///     Not to be used by API consumers.
/// </summary>
public abstract class DaybreakAuthorTag : AuthorTag
{
    private const string suffix = "Tag";

    /// <inheritdoc />
    public override string Name => base.Name.EndsWith(suffix) ? base.Name[..^suffix.Length] : base.Name;

    /// <inheritdoc />
    public override string Texture => string.Join('/', Assets.Images.AuthorTags.Tomat.KEY.Split('/')[..^1]) + '/' + Name;
}

/// <summary>
///     Not to be used by API consumers.
/// </summary>
public class TomatTag : DaybreakAuthorTag;

/// <summary>
///     Not to be used by API consumers.
/// </summary>
public class BlockarozTag : DaybreakAuthorTag
{
    /// <inheritdoc />
    public override void DrawIcon(SpriteBatch sb, Vector2 position)
    {
        base.DrawIcon(sb, position);

        sb.Draw(
            Assets.Images.AuthorTags.Blockaroz_Overlay.Asset.Value,
            new Rectangle((int)position.X, (int)position.Y - 2, 26, 26),
            Color.Red with { A = 200 } * (0.4f + MathF.Sin(Main.GlobalTimeWrappedHourly * MathHelper.TwoPi * 0.1f) * 0.3f)
        );
    }
}

/// <summary>
///     Not to be used by API consumers.
/// </summary>
public class DreitoneTag : DaybreakAuthorTag;

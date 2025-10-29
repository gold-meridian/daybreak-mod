using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Authorship;

/// <summary>
///     An author tag to be automatically loaded and displayed in the Mods List
///     UI.
/// </summary>
public abstract class AuthorTag : ModTexturedType, ILocalizedModType
{
    /// <summary>
    ///     The author name.
    /// </summary>
    public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);

    /// <inheritdoc />
    public string LocalizationCategory => "AuthorTags";

    /// <summary>
    ///     Renders the icon.
    /// </summary>
    /// <param name="sb">The <see cref="SpriteBatch"/>.</param>
    /// <param name="position">The position to render at.</param>
    public virtual void DrawIcon(SpriteBatch sb, Vector2 position)
    {
        if (!ModContent.RequestIfExists<Texture2D>(Texture, out var icon))
        {
            return;
        }

        sb.Draw(
            icon.Value,
            new Rectangle((int)position.X, (int)position.Y - 2, 26, 26),
            Color.White
        );
    }

#region Sealed ModType boilerplate
    /// <inheritdoc />
    protected sealed override void Register()
    {
        ModTypeLookup<AuthorTag>.Register(this);
    }

    /// <inheritdoc />
    protected sealed override void InitTemplateInstance()
    {
        base.InitTemplateInstance();
    }

    /// <inheritdoc />
    public sealed override void SetupContent()
    {
        base.SetupContent();

        _ = DisplayName;

        SetStaticDefaults();
    }
#endregion
}

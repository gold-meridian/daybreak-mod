using Daybreak.ChatTags.V1;
using Terraria.ModLoader;

namespace Daybreak;

/// <summary>
///     The <see cref="Mod" /> implementation for DAYBREAK.
/// </summary>
partial class ModImpl : IHasCustomAuthorMessage
{
    /// <inheritdoc />
    public ModImpl()
    {
        // Handled by the asset generator.
        MusicAutoloadingEnabled = false;
    }

    string IHasCustomAuthorMessage.GetAuthorText() => AuthorText.GetAuthorTooltip(this, null);
}

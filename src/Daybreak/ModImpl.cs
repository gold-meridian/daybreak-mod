using Terraria.ModLoader;

namespace Daybreak;

/// <summary>
///     The <see cref="Mod" /> implementation for DAYBREAK.
/// </summary>
partial class ModImpl
{
    /// <inheritdoc />
    public ModImpl()
    {
        // Handled by the asset generator.
        MusicAutoloadingEnabled = false;
    }
}

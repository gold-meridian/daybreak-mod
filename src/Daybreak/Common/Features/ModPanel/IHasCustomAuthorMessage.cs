using Terraria.ModLoader;

namespace Daybreak.Common.Features.ModPanel;

/// <summary>
///     When implemented by a <see cref="Mod" />, allows the mod to modify their
///     Author text in the Mod Panel.
/// </summary>
/// <remarks>
///     For more advanced use cases, directly implement
///     <see cref="ModPanelStyle"/> and change the text there.
/// </remarks>
public interface IHasCustomAuthorMessage
{
    /// <summary>
    ///     Returns the author text to display.
    /// </summary>
    string GetAuthorText();
}

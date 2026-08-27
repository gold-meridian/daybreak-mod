namespace Daybreak.Common.Features.Rarities;

/// <summary>
///     The context behind which the rarity is being rendered.
/// </summary>
/// <param name="Ui">Whether this is rendering in a UI.</param>
/// <param name="DrawKind">The known routine in which this is being rendered.</param>
public readonly record struct RarityDrawContext(bool Ui, RarityDrawContext.Kind DrawKind)
{
    /// <summary>
    ///     The sub-categorization of the rarity rendering.
    /// </summary>
    public enum Kind : byte
    {
        /// <summary>
        ///     As part of an item tooltip.
        /// </summary>
        ItemTooltip,

        /// <summary>
        ///     As part of the mouse hover text (such as over an item in the world).
        /// </summary>
        MouseText,

        /// <summary>
        ///     As part of the popup text generated when an item is picked up,
        ///     reforged, etc.
        /// </summary>
        PopupText,
    }
}

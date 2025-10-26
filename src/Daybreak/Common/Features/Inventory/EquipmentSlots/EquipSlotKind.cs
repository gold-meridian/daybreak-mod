namespace Daybreak.Common.Features.Inventory;

/// <summary>
///     The context behind an equipment slot request.
/// </summary>
public enum EquipSlotKind
{
    /// <summary>
    ///     The actual item to be used.
    /// </summary>
    Functional,

    /// <summary>
    ///     The dye item to apply visually.
    /// </summary>
    Dye,
}

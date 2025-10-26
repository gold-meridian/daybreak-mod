namespace Daybreak.Common.Features.Inventory;

partial class EquipSlot
{
    /// <summary>
    ///     The base type for an order position.
    /// </summary>
    public abstract record Position;

    /// <summary>
    ///     Default ordering.
    /// </summary>
    public sealed record Default : Position;

    /// <summary>
    ///     Placed before a slot.
    /// </summary>
    public sealed record Before(EquipSlot BeforeSlot) : Position;

    /// <summary>
    ///     Placed after a slot.
    /// </summary>
    public sealed record After(EquipSlot AfterSlot) : Position;
}

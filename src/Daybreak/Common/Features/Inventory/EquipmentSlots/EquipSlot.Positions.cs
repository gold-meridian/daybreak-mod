using System.Collections.Generic;

namespace Daybreak.Common.Features.Inventory;

partial class EquipSlot
{
    /// <summary>
    ///     The base type for an order position.
    /// </summary>
    public abstract class Position
    {
        /// <summary>
        ///     Adds this <paramref name="slot"/> to <paramref name="slots"/>.
        /// </summary>
        public abstract void AddSorted(EquipSlot slot, List<EquipSlot> slots);
    }

    /// <summary>
    ///     Default ordering.
    /// </summary>
    public sealed class Default : Position
    {
        /// <inheritdoc />
        public override void AddSorted(EquipSlot slot, List<EquipSlot> slots)
        {
            slots.Add(slot);
        }
    }

    /// <summary>
    ///     Placed before a slot.
    /// </summary>
    public sealed class Before(EquipSlot beforeSlot) : Position
    {
        /// <inheritdoc />
        public override void AddSorted(EquipSlot slot, List<EquipSlot> slots)
        {
            var index = slots.IndexOf(beforeSlot);
            if (index == -1)
            {
                slots.Add(slot);
            }
            else
            {
                slots.Insert(index, slot);
            }
        }
    }

    /// <summary>
    ///     Placed after a slot.
    /// </summary>
    public sealed class After(EquipSlot afterSlot) : Position
    {
        /// <inheritdoc />
        public override void AddSorted(EquipSlot slot, List<EquipSlot> slots)
        {
            var index = slots.IndexOf(afterSlot);
            if (index == -1)
            {
                slots.Add(slot);
            }
            else
            {
                slots.Insert(index + 1, slot);
            }
        }
    }
}

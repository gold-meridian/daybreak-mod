using Daybreak.Common.Features.ItemSlots;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Inventory;

/// <summary>
///     
/// </summary>
[UsedImplicitly(
    ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    ImplicitUseTargetFlags.WithInheritors
)]
public abstract partial class EquipSlot : ModType
{
    /// <summary>
    ///     Controls how this equip slot is ordered relative to other slots.
    /// </summary>
    public virtual Position OrderPosition { get; private set; } = new Default();

#region Sealed ModType boilerplate
    /// <inheritdoc />
    protected sealed override void Register()
    {
        EquipSlotLoader.Register(this);
        ModTypeLookup<EquipSlot>.Register(this);
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

        SetStaticDefaults();
    }
#endregion

    /// <summary>
    ///     Gets a reference to the item within this slot.
    /// </summary>
    /// <param name="kind">The slot kind.</param>
    public abstract ref Item GetItem(EquipSlotKind kind);

    /// <summary>
    ///     The <see cref="ItemSlotContext"/>.
    /// </summary>
    /// <param name="kind">The slot kind.</param>
    public abstract int GetContext(EquipSlotKind kind);

    /// <summary>
    ///     Whether this slot can be toggled.
    /// </summary>
    /// <param name="kind">The slot kind.</param>
    public virtual bool CanBeToggled(EquipSlotKind kind)
    {
        return false;
    }

    /// <summary>
    ///     Handle when the toggle button is pressed.
    /// </summary>
    /// <param name="toggleButton">The toggle button texture.</param>
    /// <param name="toggleRect">The hitbox of the button.</param>
    /// <param name="mouseLoc">The location of the mouse.</param>
    /// <param name="hoverText">The hover text to display.</param>
    /// <param name="toggleHovered">Whether the button is being hovered.</param>
    /// <param name="kind">The slot kind.</param>
    public virtual void HandleToggle(
        ref Texture2D toggleButton,
        Rectangle toggleRect,
        Point mouseLoc,
        ref string? hoverText,
        ref bool toggleHovered,
        EquipSlotKind kind
    ) { }

    /// <summary>
    ///     Draw the toggle button.
    /// </summary>
    /// <param name="hoverText">The hover text to display.</param>
    /// <param name="toggleButton">The toggle button texture.</param>
    /// <param name="toggleRect">The hitbox of the button.</param>
    /// <param name="kind">The slot kind.</param>
    public virtual void DrawToggle(
        string? hoverText,
        Texture2D toggleButton,
        Rectangle toggleRect,
        EquipSlotKind kind
    ) { }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace Daybreak.Common.Features.ItemSlots;

/// <summary>
///     Base definition for a modded <see cref="ItemSlot"/> context with custom
///     logic.
/// </summary>
public abstract class ItemSlotContext : ModType
{
    /// <summary>
    ///     The numeric ID of your item slot, continued at
    ///     <see cref="ItemSlot.Context.Count"/>.
    /// </summary>
    public int Type { get; internal set; }

#region Sealed ModType boilerplate
    /// <inheritdoc />
    protected sealed override void Register()
    {
        ItemSlotLoader.Register(this);
        ModTypeLookup<ItemSlotContext>.Register(this);
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

    // Hooks which are ran in situations where an ItemSlot context is not
    // directly applied, such as when iterating over all contexts to make a
    // decision.
#region Context-independent hooks
    public virtual bool TryHandleSwap(ref Item item, int incomingContext, Player player)
    {
        return false;
    }
#endregion
    
    // Hooks which are ran in situations where an ItemSlot context is directly
    // applied, meaning the caller intends to run the behavior for a known
    // ItemSlot context.  These hooks are specifically for implementing behavior
    // around known ItemSlot events.
#region Context-dependent implementation hooks
    public virtual bool PreLeftClick(Item item, ref int context)
    {
        return true;
    }

    public virtual void PostLeftClick(Item item, int context) { }

    public virtual bool PreRightClick(Item item, ref int context)
    {
        return true;
    }

    public virtual void PostRightClick(Item item, int context) { }

    public virtual int? PrePickItemMovementAction(Item item, ref int context, Item checkItem)
    {
        return null;
    }

    public virtual void PostPickItemMovementAction(Item item, int context, Item checkItem) { }

    public virtual string? PreGetOverrideInstructions(Item item, ref int context)
    {
        return null;
    }

    public virtual void PostGetOverrideInstructions(Item item, int context) { }

    public virtual bool PreSwapVanityEquip(Item item, ref int context, Player player)
    {
        return true;
    }

    public virtual void PostSwapVanityEquip(Item item, int context, Player player) { }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Item item, ref int context, Vector2 position, Color lightColor)
    {
        return true;
    }

    public virtual void PostDraw(SpriteBatch spriteBatch, Item item, int context, Vector2 position, Color lightColor) { }

    public virtual bool PreMouseHover(Item item, ref int context)
    {
        return true;
    }

    public virtual void PostMouseHover(Item item, int context) { }

    public virtual bool PreSwapEquip(Item item, ref int context)
    {
        return true;
    }

    public virtual void PostSwapEquip(Item item, int context) { }

    public virtual bool PreOverrideHover(Item item, ref int context)
    {
        return true;
    }

    public virtual void PostOverrideHover(Item item, int context) { }
#endregion

    // Hooks which are ran in situations where an ItemSlot context is directly
    // applied, meaning the caller intends to run the behavior for a known
    // ItemSlot context.  These hooks are specifically for injecting within
    // defined behavior and running additional code or tweaking inputs without
    // assuming completely new behavior.
#region Context-dependent modification hooks
    public virtual bool ModifyIcon(
        SpriteBatch spriteBatch,
        ref Texture2D texture,
        ref Vector2 position,
        ref Rectangle? sourceRectangle,
        ref Color color,
        ref float rotation,
        ref Vector2 origin,
        ref float scale,
        ref SpriteEffects effects
    )
    {
        return true;
    }
#endregion
}

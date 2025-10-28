using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.Achievements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;

namespace Daybreak.Common.Features.ItemSlots;

/// <summary>
///     Loads <see cref="ItemSlotContext" />s and manages their implementation.
/// </summary>
public sealed class ItemSlotLoader : ModSystem
{
    /// <summary>
    ///     Loaded item slots by index.
    /// </summary>
    private static readonly List<ItemSlotContext> item_slots = [];

    /// <summary>
    ///     The original context at a given instance.
    /// </summary>
    [ThreadStatic]
    private static int? originalContext;

    /// <summary>
    ///     The total number of loaded item slot contexts, including both
    ///     vanilla and modded.
    /// </summary>
    public static int Count { get; private set; } = ItemSlot.Context.Count;

    internal static void Register(ItemSlotContext itemSlotContext)
    {
        itemSlotContext.Type = Count++;
        item_slots.Add(itemSlotContext);
    }

    /// <summary>
    ///     Attempts to get the item slot for a given type (context).
    /// </summary>
    /// <param name="type">The item slot context.</param>
    public static ItemSlotContext? GetItemSlot(int type)
    {
        if (type < ItemSlot.Context.Count || type >= Count)
        {
            return null;
        }

        return item_slots[type - ItemSlot.Context.Count];
    }

    private static ItemSlotContext? GetItemSlot(int? context)
    {
        return context.HasValue ? GetItemSlot(context.Value) : null;
    }

    //       Handle(Item[] inv, int context = 0, int slot = 0)
    // DONE: OverrideHover(Item[] inv, int context = 0, int slot = 0)
    //       OverrideLeftClick(Item[] inv, int context = 0, int slot = 0)
    //       IsAccessoryContext(int context)
    // DONE: LeftClick(Item[] inv, int context = 0, int slot = 0)
    //       LeftClick_SellOrTrash(Item[] inv, int context, int slot)
    //       SellOrTrash(Item[] inv, int context, int slot)
    // DONE: GetOverrideInstructions(Item[] inv, int context, int slot)
    // DONE: PickItemMovementAction(Item[] inv, int context, int slot, Item checkItem)
    // DONE: RightClick(Item[] inv, int context = 0, int slot = 0)
    //       PickupItemIntoMouse(Item[] inv, int context, int slot, Player player)
    // DONE: SwapVanityEquip(Item[] inv, int context, int slot, Player player)
    //       TryPickupDyeToCursor(int context, Item[] inv, int slot, Player player)
    // DONE: Draw(SpriteBatch spriteBatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor = default(Color))
    //       GetColorByLoadout(int slot, int context)
    //       TryGetSlotColor(int loadoutIndex, int context, out Color color)
    //       DrawItemIcon(Item item, int context, SpriteBatch spriteBatch, Vector2 screenPositionForItemCenter, float scale, float sizeLimit, Color environmentColor)
    // DONE: MouseHover(Item[] inv, int context = 0, int slot = 0)
    // DONE: SwapEquip(Item[] inv, int context, int slot)
    //       Equippable(Item[] inv, int context, int slot)
    //       TryEnteringFastUseMode(Item[] inv, int context, int slot, Player player, ref string s)
    //       TryEnteringBuildingMode(Item[] inv, int context, int slot, Player player, ref string s)

    /// <inheritdoc />
    public override void ResizeArrays()
    {
        base.ResizeArrays();

        Array.Resize(ref ItemSlot.canFavoriteAt, Count);
        Array.Resize(ref ItemSlot.canShareAt, Count);
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        On_ItemSlot.OverrideHover_ItemArray_int_int += OverrideHover;
        On_ItemSlot.LeftClick_ItemArray_int_int += LeftClick;
        On_ItemSlot.RightClick_ItemArray_int_int += RightClick;
        On_ItemSlot.GetOverrideInstructions += GetOverrideInstructions;
        On_ItemSlot.PickItemMovementAction += PickItemMovementAction;
        On_ItemSlot.SwapVanityEquip += SwapVanityEquip;
        On_ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += Draw;
        On_ItemSlot.MouseHover_ItemArray_int_int += MouseHover;
        On_ItemSlot.SwapEquip_ItemArray_int_int += SwapEquip;

        IL_ItemSlot.Draw_SpriteBatch_ItemArray_int_int_Vector2_Color += ModifyItemSlotIcon;
    }

    private static void ModifyItemSlotIcon(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(x => x.MatchCallvirt<AccessorySlotLoader>(nameof(AccessorySlotLoader.DrawSlotTexture)));
        c.GotoNext(MoveType.Before, x => x.MatchCallvirt<SpriteBatch>(nameof(SpriteBatch.Draw)));

        c.Remove();

        c.EmitDelegate(
            (
                SpriteBatch self,
                Texture2D texture,
                Vector2 position,
                Rectangle? sourceRectangle,
                Color color,
                float rotation,
                Vector2 origin,
                float scale,
                SpriteEffects effects,
                float layerDepth
            ) =>
            {
                if (GetItemSlot(originalContext) is not { } itemSlot)
                {
                    self.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
                    return;
                }

                if (itemSlot.ModifyIcon(self, ref texture, ref position, ref sourceRectangle, ref color, ref rotation, ref origin, ref scale, ref effects))
                {
                    self.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
                }
            }
        );
    }

    private static void LeftClick(On_ItemSlot.orig_LeftClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
    {
        try
        {
            originalContext = context;

            if (GetItemSlot(context) is not { } itemSlot)
            {
                orig(inv, context, slot);
                return;
            }

            if (!itemSlot.PreLeftClick(inv[slot], ref context))
            {
                return;
            }

            orig(inv, context, slot);
            itemSlot.PostLeftClick(inv[slot], context);
        }
        finally
        {
            originalContext = null;
        }
    }

    private static void RightClick(On_ItemSlot.orig_RightClick_ItemArray_int_int orig, Item[] inv, int context, int slot)
    {
        try
        {
            originalContext = context;

            if (GetItemSlot(context) is not { } itemSlot)
            {
                orig(inv, context, slot);
                return;
            }

            if (!itemSlot.PreRightClick(inv[slot], ref context))
            {
                return;
            }

            orig(inv, context, slot);
            itemSlot.PostRightClick(inv[slot], context);
        }
        finally
        {
            originalContext = null;
        }
    }

    private static int PickItemMovementAction(On_ItemSlot.orig_PickItemMovementAction orig, Item[] inv, int context, int slot, Item checkItem)
    {
        context = originalContext ?? context;

        if (GetItemSlot(context) is not { } itemSlot)
        {
            return orig(inv, context, slot, checkItem);
        }

        var result = itemSlot.PrePickItemMovementAction(inv[slot], ref context, checkItem);
        if (result.HasValue)
        {
            return result.Value;
        }

        result = orig(inv, context, slot, checkItem);
        itemSlot.PostPickItemMovementAction(inv[slot], context, checkItem);
        return result.Value;
    }

    private static string GetOverrideInstructions(On_ItemSlot.orig_GetOverrideInstructions orig, Item[] inv, int context, int slot)
    {
        if (GetItemSlot(context) is not { } itemSlot)
        {
            return orig(inv, context, slot);
        }

        var result = itemSlot.PreGetOverrideInstructions(inv[slot], ref context);
        if (result is not null)
        {
            return result;
        }

        result = orig(inv, context, slot);
        itemSlot.PostGetOverrideInstructions(inv[slot], context);
        return result;
    }

    private static void SwapVanityEquip(On_ItemSlot.orig_SwapVanityEquip orig, Item[] inv, int context, int slot, Player player)
    {
        if (GetItemSlot(context) is not { } itemSlot)
        {
            orig(inv, context, slot, player);
            return;
        }

        if (!itemSlot.PreSwapVanityEquip(inv[slot], ref context, player))
        {
            return;
        }

        orig(inv, context, slot, player);
        itemSlot.PostSwapVanityEquip(inv[slot], context, player);
    }

    private static void Draw(On_ItemSlot.orig_Draw_SpriteBatch_ItemArray_int_int_Vector2_Color orig, SpriteBatch spriteBatch, Item[] inv, int context, int slot, Vector2 position, Color lightColor)
    {
        try
        {
            originalContext = context;

            var itemSlot = GetItemSlot(context);
            if (itemSlot is null)
            {
                orig(spriteBatch, inv, context, slot, position, lightColor);
                return;
            }

            if (!itemSlot.PreDraw(spriteBatch, inv[slot], ref context, position, lightColor))
            {
                return;
            }

            orig(spriteBatch, inv, context, slot, position, lightColor);
            itemSlot.PostDraw(spriteBatch, inv[slot], context, position, lightColor);
        }
        finally
        {
            originalContext = null;
        }
    }

    private static void MouseHover(On_ItemSlot.orig_MouseHover_ItemArray_int_int orig, Item[] inv, int context, int slot)
    {
        if (GetItemSlot(context) is not { } itemSlot)
        {
            orig(inv, context, slot);
            return;
        }

        if (!itemSlot.PreMouseHover(inv[slot], ref context))
        {
            return;
        }

        orig(inv, context, slot);
        itemSlot.PostMouseHover(inv[slot], context);
    }

    private static void SwapEquip(On_ItemSlot.orig_SwapEquip_ItemArray_int_int orig, Item[] inv, int context, int slot)
    {
        if (GetItemSlot(context) is not { } itemSlot)
        {
            SwapEquipWithOurAdditionalHook(inv, context, slot);
            return;
        }

        if (!itemSlot.PreSwapEquip(inv[slot], ref context))
        {
            return;
        }

        SwapEquipWithOurAdditionalHook(inv, context, slot);
        itemSlot.PostSwapEquip(inv[slot], context);

        return;

        static void SwapEquipWithOurAdditionalHook(Item[] inv, int context, int slot)
        {
            var player = Main.LocalPlayer;
            if (ItemSlot.isEquipLocked(inv[slot].type) || inv[slot].IsAir)
            {
                return;
            }

            var success = false;
            if (inv[slot].dye > 0)
            {
                inv[slot] = ItemSlot.DyeSwap(inv[slot], out success);
                if (success)
                {
                    Main.EquipPageSelected = 0;
                    AchievementsHelper.HandleOnEquip(player, inv[slot], 12);
                }
            }
            else if (Main.projHook[inv[slot].shoot])
            {
                inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 4, out success);
                if (success)
                {
                    Main.EquipPageSelected = 2;
                    AchievementsHelper.HandleOnEquip(player, inv[slot], 16);
                }
            }
            else if (inv[slot].mountType != -1 && !MountID.Sets.Cart[inv[slot].mountType])
            {
                inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 3, out success);
                if (success)
                {
                    Main.EquipPageSelected = 2;
                    AchievementsHelper.HandleOnEquip(player, inv[slot], 17);
                }
            }
            else if (inv[slot].mountType != -1 && MountID.Sets.Cart[inv[slot].mountType])
            {
                inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 2, out success);
                if (success)
                {
                    Main.EquipPageSelected = 2;
                }
            }
            else if (inv[slot].buffType > 0 && Main.lightPet[inv[slot].buffType])
            {
                inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 1, out success);
                if (success)
                {
                    Main.EquipPageSelected = 2;
                }
            }
            else if (inv[slot].buffType > 0 && Main.vanityPet[inv[slot].buffType])
            {
                inv[slot] = ItemSlot.EquipSwap(inv[slot], player.miscEquips, 0, out success);
                if (success)
                {
                    Main.EquipPageSelected = 2;
                }
            }
            else
            {
                foreach (var customSlot in item_slots)
                {
                    if (!customSlot.TryHandleSwap(ref inv[slot], context, player))
                    {
                        continue;
                    }

                    success = true;
                    break;
                }

                if (!success)
                {
                    var item = inv[slot];
                    inv[slot] = ItemSlot.ArmorSwap(inv[slot], out success);
                    if (success)
                    {
                        Main.EquipPageSelected = 0;
                        AchievementsHelper.HandleOnEquip(player, item, (item.accessory ? 10 : 8) * Math.Sign(context));
                    }
                }
            }

            Recipe.FindRecipes();

            if (context == 3 && Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.SyncChestItem, -1, -1, null, player.chest, slot);
            }
        }
    }

    private static void OverrideHover(On_ItemSlot.orig_OverrideHover_ItemArray_int_int orig, Item[] inv, int context, int slot)
    {
        if (GetItemSlot(context) is not { } itemSlot)
        {
            orig(inv, context, slot);
            return;
        }

        if (!itemSlot.PreOverrideHover(inv[slot], ref context))
        {
            return;
        }

        orig(inv, context, slot);
        itemSlot.PostOverrideHover(inv[slot], context);
    }
}

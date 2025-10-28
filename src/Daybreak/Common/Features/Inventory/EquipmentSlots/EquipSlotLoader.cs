using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Gamepad;

namespace Daybreak.Common.Features.Inventory;

/// <summary>
///     Loads miscellaneous equip slots.
/// </summary>
public sealed class EquipSlotLoader : ModSystem
{
    private static readonly List<EquipSlot> slots;
    private static EquipSlot[] orderedSlots = [];

    /// <summary>
    ///     The Pet slot.
    /// </summary>
    public static EquipSlot Pet { get; } = new PetSlot();

    /// <summary>
    ///     The Light Pet slot.
    /// </summary>
    public static EquipSlot LightPet { get; } = new LightPetSlot();

    /// <summary>
    ///     The Minecart slot.
    /// </summary>
    public static EquipSlot Minecart { get; } = new MinecartSlot();

    /// <summary>
    ///     The Mount slot.
    /// </summary>
    public static EquipSlot Mount { get; } = new MountSlot();

    /// <summary>
    ///     The Hook slot.
    /// </summary>
    public static EquipSlot Hook { get; } = new HookSlot();

    static EquipSlotLoader()
    {
        slots =
        [
            Pet,
            LightPet,
            Minecart,
            Mount,
            Hook,
        ];
    }

    internal static void Register(EquipSlot slot)
    {
        slots.Add(slot);
    }

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        IL_Main.DrawInventory += DrawInventory_ReplaceVanillaMiscSlotDrawing;
    }

    /// <inheritdoc />
    public override void ResizeArrays()
    {
        base.ResizeArrays();

        // var positions = slots.ToDictionary(x => x, x => x.OrderPosition);

        var sort = new TopoSort<EquipSlot>(
            slots,
            x => x.OrderPosition is EquipSlot.After after ? [after.AfterSlot] : [],
            x => x.OrderPosition is EquipSlot.Before before ? [before.BeforeSlot] : []
        );

        orderedSlots = sort.Sort().ToArray();
    }

    private static void DrawInventory_ReplaceVanillaMiscSlotDrawing(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(
            x => x.MatchLdsfld<Main>(nameof(Main.EquipPage)),
            x => x.MatchLdcI4((int)EquipmentPageId.Misc)
        );

        c.GotoNext(MoveType.Before, x => x.MatchLdloca(out _));
        c.EmitDelegate(ManuallyDrawMiscSlots);

        var pos = c.Index;

        // Find the label needed to escape the if-else chain by going to the
        // next, shorter clause.
        c.GotoNext(
            x => x.MatchLdsfld<Main>(nameof(Main.EquipPage)),
            x => x.MatchLdcI4(1)
        );

        ILLabel? label = null;
        c.GotoNext(x => x.MatchBr(out label));
        {
            Debug.Assert(label is not null);
        }

        c.Index = pos;

        c.EmitBr(label);
    }

    // Modified from Main::DrawInventory.
    private static void ManuallyDrawMiscSlots()
    {
        var mouseLoc = new Point(Main.mouseX, Main.mouseY);

        var backPanelSize = new Rectangle(0, 0, (int)(TextureAssets.InventoryBack.Width() * Main.inventoryScale), (int)(TextureAssets.InventoryBack.Height() * Main.inventoryScale));

        var xPos = Main.screenWidth - 92;
        var yPos = Main.mH + 174;

        for (var i = 0; i < 2; i++)
        {
            var slotKind = (EquipSlotKind)i;

            backPanelSize.X = xPos + i * -47;

            for (var slot = 0; slot < orderedSlots.Length; slot++)
            {
                var equipSlot = orderedSlots[slot];
                var context = equipSlot.GetContext(slotKind);
                var canBeToggled = equipSlot.CanBeToggled(slotKind);

                ref var item = ref equipSlot.GetItem(slotKind);

                // Logic moved to EquipSlot.
                /*if (i == 1)
                {
                    context = equipSlot.GetVanityContext();
                    canBeToggled = false;
                }*/

                backPanelSize.Y = yPos + slot * 47;
                var toggleButton = TextureAssets.InventoryTickOn.Value;
                var toggleRect = new Rectangle(backPanelSize.Left + 34, backPanelSize.Top - 2, toggleButton.Width, toggleButton.Height);
                var toggleHovered = false;

                var hoverText = default(string);
                if (canBeToggled)
                {
                    equipSlot.HandleToggle(ref toggleButton, toggleRect, mouseLoc, ref hoverText, ref toggleHovered, slotKind);
                }

                if (backPanelSize.Contains(mouseLoc) && !toggleHovered && !PlayerInput.IgnoreMouseInterface)
                {
                    Main.LocalPlayer.mouseInterface = true;
                    Main.armorHide = true;
                    ItemSlot.Handle(ref item, context);
                }

                ItemSlot.Draw(Main.spriteBatch, ref item, context, backPanelSize.TopLeft());

                if (canBeToggled)
                {
                    equipSlot.DrawToggle(hoverText, toggleButton, toggleRect, slotKind);
                }
            }
        }

        yPos += 47 * orderedSlots.Length + 12;
        xPos += 8;

        var buffsDrawn = 0;
        var buffsPerColumn = 3;

        const int offset_or_smth = 260;

        if (Main.screenHeight > 630 + offset_or_smth * (Main.mapStyle == 1).ToInt())
        {
            buffsPerColumn++;
        }

        if (Main.screenHeight > 680 + offset_or_smth * (Main.mapStyle == 1).ToInt())
        {
            buffsPerColumn++;
        }

        if (Main.screenHeight > 730 + offset_or_smth * (Main.mapStyle == 1).ToInt())
        {
            buffsPerColumn++;
        }

        const int buff_size = 46;

        var hoveredBuff = -1;
        for (var i = 0; i < Player.maxBuffs; i++)
        {
            if (Main.LocalPlayer.buffType[i] == 0)
            {
                continue;
            }

            var xOff = buffsDrawn / buffsPerColumn;
            var yOff = buffsDrawn % buffsPerColumn;
            var buffDrawPos = new Point(xPos + xOff * -buff_size, yPos + yOff * buff_size);
            hoveredBuff = Main.DrawBuffIcon(hoveredBuff, i, buffDrawPos.X, buffDrawPos.Y);
            UILinkPointNavigator.SetPosition(9000 + buffsDrawn, new Vector2(buffDrawPos.X + 30, buffDrawPos.Y + 30));
            buffsDrawn++;

            if (Main.buffAlpha[i] < 0.65f)
            {
                Main.buffAlpha[i] = 0.65f;
            }
        }

        UILinkPointNavigator.Shortcuts.BUFFS_DRAWN = buffsDrawn;
        UILinkPointNavigator.Shortcuts.BUFFS_PER_COLUMN = buffsPerColumn;

        if (hoveredBuff < 0)
        {
            return;
        }

        var buff = Main.LocalPlayer.buffType[hoveredBuff];
        if (buff <= 0)
        {
            return;
        }

        var buffName = Lang.GetBuffName(buff);
        var buffTooltip = Main.GetBuffTooltip(Main.LocalPlayer, buff);
        if (buff == 147)
        {
            Main.bannerMouseOver = true;
        }

        /*
        if (meleeBuff[num34])
            MouseTextHackZoom(buffName, -10, 0, buffTooltip);
        else
            MouseTextHackZoom(buffName, buffTooltip);
        */
        var rare = 0;
        if (Main.meleeBuff[buff])
        {
            rare = -10;
        }

        BuffLoader.ModifyBuffText(buff, ref buffName, ref buffTooltip, ref rare);
        Main.instance.MouseTextHackZoom(buffName, rare, diff: 0, buffTooltip);
    }
}

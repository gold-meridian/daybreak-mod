using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Daybreak.Common.Features.Inventory;

// Implementation for well-defined Vanilla equip slots.

[Autoload(false)]
internal abstract class VanillaEquipSlot : EquipSlot
{
    protected virtual bool IsEffectHidden => false;

    public override void HandleToggle(ref Texture2D toggleButton, Rectangle toggleRect, Point mouseLoc, ref string? hoverText, ref bool toggleHovered, EquipSlotKind kind)
    {
        if (kind != EquipSlotKind.Functional)
        {
            return;
        }

        if (IsEffectHidden)
        {
            toggleButton = TextureAssets.InventoryTickOff.Value;
        }

        if (!toggleRect.Contains(mouseLoc) || PlayerInput.IgnoreMouseInterface)
        {
            return;
        }

        Main.LocalPlayer.mouseInterface = true;
        toggleHovered = true;
        if (Main.mouseLeft && Main.mouseLeftRelease)
        {
            OnToggle();

            Main.mouseLeftRelease = false;
            SoundEngine.PlaySound(SoundID.MenuTick);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.SyncPlayer, -1, -1, null, Main.myPlayer);
            }
        }

        var inter = !IsEffectHidden ? 1 : 2;
        hoverText = Lang.inter[58 + inter].Value;
    }

    public override void DrawToggle(string? hoverText, Texture2D toggleButton, Rectangle toggleRect, EquipSlotKind kind)
    {
        if (kind != EquipSlotKind.Functional)
        {
            return;
        }

        Main.spriteBatch.Draw(toggleButton, toggleRect.TopLeft(), Color.White * 0.7f);
        if (hoverText is null)
        {
            return;
        }

        Main.HoverItem = new Item();
        Main.hoverItemName = hoverText;
    }

    protected virtual void OnToggle() { }
}

internal sealed class PetSlot : VanillaEquipSlot
{
    protected override bool IsEffectHidden => Main.LocalPlayer.hideMisc[0];

    public override int GetContext(EquipSlotKind kind)
    {
        return kind switch
        {
            EquipSlotKind.Functional => ItemSlot.Context.EquipPet,
            EquipSlotKind.Dye => ItemSlot.Context.EquipMiscDye,
            _ => throw new ArgumentOutOfRangeException(nameof(kind)),
        };
    }

    public override bool CanBeToggled(EquipSlotKind kind)
    {
        return kind == EquipSlotKind.Functional;
    }

    public override ref Item GetItem(EquipSlotKind kind)
    {
        switch (kind)
        {
            case EquipSlotKind.Functional:
                return ref Main.LocalPlayer.miscEquips[0];

            case EquipSlotKind.Dye:
                return ref Main.LocalPlayer.miscDyes[0];

            default:
                throw new ArgumentOutOfRangeException(nameof(kind));
        }
    }

    protected override void OnToggle()
    {
        base.OnToggle();

        Main.LocalPlayer.TogglePet();
    }
}

internal sealed class LightPetSlot : VanillaEquipSlot
{
    public override Position OrderPosition => new After(EquipSlotLoader.Pet);

    protected override bool IsEffectHidden => Main.LocalPlayer.hideMisc[1];

    public override int GetContext(EquipSlotKind kind)
    {
        return kind switch
        {
            EquipSlotKind.Functional => ItemSlot.Context.EquipLight,
            EquipSlotKind.Dye => ItemSlot.Context.EquipMiscDye,
            _ => throw new ArgumentOutOfRangeException(nameof(kind)),
        };
    }

    public override bool CanBeToggled(EquipSlotKind kind)
    {
        return kind == EquipSlotKind.Functional;
    }

    public override ref Item GetItem(EquipSlotKind kind)
    {
        switch (kind)
        {
            case EquipSlotKind.Functional:
                return ref Main.LocalPlayer.miscEquips[1];

            case EquipSlotKind.Dye:
                return ref Main.LocalPlayer.miscDyes[1];

            default:
                throw new ArgumentOutOfRangeException(nameof(kind));
        }
    }

    protected override void OnToggle()
    {
        base.OnToggle();

        Main.LocalPlayer.ToggleLight();
    }
}

internal sealed class MinecartSlot : VanillaEquipSlot
{
    public override Position OrderPosition => new After(EquipSlotLoader.LightPet);

    public override int GetContext(EquipSlotKind kind)
    {
        return kind switch
        {
            EquipSlotKind.Functional => ItemSlot.Context.EquipMinecart,
            EquipSlotKind.Dye => ItemSlot.Context.EquipMiscDye,
            _ => throw new ArgumentOutOfRangeException(nameof(kind)),
        };
    }

    public override bool CanBeToggled(EquipSlotKind kind)
    {
        return kind == EquipSlotKind.Functional && Main.LocalPlayer.unlockedSuperCart;
    }

    public override ref Item GetItem(EquipSlotKind kind)
    {
        switch (kind)
        {
            case EquipSlotKind.Functional:
                return ref Main.LocalPlayer.miscEquips[2];

            case EquipSlotKind.Dye:
                return ref Main.LocalPlayer.miscDyes[2];

            default:
                throw new ArgumentOutOfRangeException(nameof(kind));
        }
    }

    public override void HandleToggle(ref Texture2D toggleButton, Rectangle toggleRect, Point mouseLoc, ref string? hoverText, ref bool toggleHovered, EquipSlotKind kind)
    {
        if (kind != EquipSlotKind.Functional)
        {
            return;
        }

        toggleButton = TextureAssets.Extra[ExtrasID.SuperCartEnabled].Value;
        if (!Main.LocalPlayer.enabledSuperCart)
        {
            toggleButton = TextureAssets.Extra[ExtrasID.SuperCartDisabled].Value;
        }

        toggleRect = new Rectangle(toggleRect.X + toggleRect.Width / 2, toggleRect.Y + toggleRect.Height / 2, toggleRect.Width, toggleRect.Height);
        toggleRect.Offset(-toggleRect.Width / 2, -toggleRect.Height / 2);
        if (!toggleRect.Contains(mouseLoc) || PlayerInput.IgnoreMouseInterface)
        {
            return;
        }

        Main.LocalPlayer.mouseInterface = true;
        toggleHovered = true;
        if (Main.mouseLeft && Main.mouseLeftRelease)
        {
            Main.LocalPlayer.enabledSuperCart = !Main.LocalPlayer.enabledSuperCart;
            Main.mouseLeftRelease = false;
            SoundEngine.PlaySound(SoundID.MenuTick);

            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendData(MessageID.SyncPlayer, -1, -1, null, Main.myPlayer);
            }
        }

        hoverText = Language.GetTextValue(Main.LocalPlayer.enabledSuperCart ? "GameUI.SuperCartEnabled" : "GameUI.SuperCartDisabled");
    }

    public override void DrawToggle(string? hoverText, Texture2D toggleButton, Rectangle toggleRect, EquipSlotKind kind)
    {
        if (kind != EquipSlotKind.Functional)
        {
            return;
        }

        Main.spriteBatch.Draw(toggleButton, toggleRect.TopLeft(), Color.White);

        if (hoverText is null)
        {
            return;
        }

        Main.HoverItem = new Item();
        Main.hoverItemName = hoverText;
    }
}

internal sealed class MountSlot : VanillaEquipSlot
{
    public override Position OrderPosition => new After(EquipSlotLoader.Minecart);

    public override int GetContext(EquipSlotKind kind)
    {
        return kind switch
        {
            EquipSlotKind.Functional => ItemSlot.Context.EquipMount,
            EquipSlotKind.Dye => ItemSlot.Context.EquipMiscDye,
            _ => throw new ArgumentOutOfRangeException(nameof(kind)),
        };
    }

    public override ref Item GetItem(EquipSlotKind kind)
    {
        switch (kind)
        {
            case EquipSlotKind.Functional:
                return ref Main.LocalPlayer.miscEquips[3];

            case EquipSlotKind.Dye:
                return ref Main.LocalPlayer.miscDyes[3];

            default:
                throw new ArgumentOutOfRangeException(nameof(kind));
        }
    }
}

internal sealed class HookSlot : VanillaEquipSlot
{
    public override Position OrderPosition => new After(EquipSlotLoader.Mount);

    public override int GetContext(EquipSlotKind kind)
    {
        return kind switch
        {
            EquipSlotKind.Functional => ItemSlot.Context.EquipGrapple,
            EquipSlotKind.Dye => ItemSlot.Context.EquipMiscDye,
            _ => throw new ArgumentOutOfRangeException(nameof(kind)),
        };
    }

    public override ref Item GetItem(EquipSlotKind kind)
    {
        switch (kind)
        {
            case EquipSlotKind.Functional:
                return ref Main.LocalPlayer.miscEquips[4];

            case EquipSlotKind.Dye:
                return ref Main.LocalPlayer.miscDyes[4];

            default:
                throw new ArgumentOutOfRangeException(nameof(kind));
        }
    }
}

namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalItem':
//     System.Void Terraria.ModLoader.GlobalItem::OnCreated(Terraria.Item,Terraria.DataStructures.ItemCreationContext)
//     System.Void Terraria.ModLoader.GlobalItem::OnSpawn(Terraria.Item,Terraria.DataStructures.IEntitySource)
//     System.Int32 Terraria.ModLoader.GlobalItem::ChoosePrefix(Terraria.Item,Terraria.Utilities.UnifiedRandom)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::PrefixChance(Terraria.Item,System.Int32,Terraria.Utilities.UnifiedRandom)
//     System.Boolean Terraria.ModLoader.GlobalItem::AllowPrefix(Terraria.Item,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanUseItem(Terraria.Item,Terraria.Player)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::CanAutoReuseItem(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UseStyle(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle)
//     System.Void Terraria.ModLoader.GlobalItem::HoldStyle(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle)
//     System.Void Terraria.ModLoader.GlobalItem::HoldItem(Terraria.Item,Terraria.Player)
//     System.Single Terraria.ModLoader.GlobalItem::UseTimeMultiplier(Terraria.Item,Terraria.Player)
//     System.Single Terraria.ModLoader.GlobalItem::UseAnimationMultiplier(Terraria.Item,Terraria.Player)
//     System.Single Terraria.ModLoader.GlobalItem::UseSpeedMultiplier(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::GetHealLife(Terraria.Item,Terraria.Player,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::GetHealMana(Terraria.Item,Terraria.Player,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyManaCost(Terraria.Item,Terraria.Player,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.GlobalItem::OnMissingMana(Terraria.Item,Terraria.Player,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::OnConsumeMana(Terraria.Item,Terraria.Player,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyWeaponDamage(Terraria.Item,Terraria.Player,Terraria.ModLoader.StatModifier&)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyResearchSorting(Terraria.Item,Terraria.ID.ContentSamples/CreativeHelper/ItemGroup&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::CanConsumeBait(Terraria.Player,Terraria.Item)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanResearch(Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::OnResearched(Terraria.Item,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyWeaponKnockback(Terraria.Item,Terraria.Player,Terraria.ModLoader.StatModifier&)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyWeaponCrit(Terraria.Item,Terraria.Player,System.Single&)
//     System.Boolean Terraria.ModLoader.GlobalItem::NeedsAmmo(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::PickAmmo(Terraria.Item,Terraria.Item,Terraria.Player,System.Int32&,System.Single&,Terraria.ModLoader.StatModifier&,System.Single&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::CanChooseAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::CanBeChosenAsAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanConsumeAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanBeConsumedAsAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::OnConsumeAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::OnConsumedAsAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanShoot(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyShootStats(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Vector2&,System.Int32&,System.Int32&,System.Single&)
//     System.Boolean Terraria.ModLoader.GlobalItem::Shoot(Terraria.Item,Terraria.Player,Terraria.DataStructures.EntitySource_ItemUse_WithAmmo,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Single)
//     System.Void Terraria.ModLoader.GlobalItem::UseItemHitbox(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle&,System.Boolean&)
//     System.Void Terraria.ModLoader.GlobalItem::MeleeEffects(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::CanCatchNPC(Terraria.Item,Terraria.NPC,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::OnCatchNPC(Terraria.Item,Terraria.NPC,Terraria.Player,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyItemScale(Terraria.Item,Terraria.Player,System.Single&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::CanHitNPC(Terraria.Item,Terraria.Player,Terraria.NPC)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::CanMeleeAttackCollideWithNPC(Terraria.Item,Microsoft.Xna.Framework.Rectangle,Terraria.Player,Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyHitNPC(Terraria.Item,Terraria.Player,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalItem::OnHitNPC(Terraria.Item,Terraria.Player,Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanHitPvp(Terraria.Item,Terraria.Player,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyHitPvp(Terraria.Item,Terraria.Player,Terraria.Player,Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.GlobalItem::OnHitPvp(Terraria.Item,Terraria.Player,Terraria.Player,Terraria.Player/HurtInfo)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalItem::UseItem(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UseAnimation(Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::ConsumeItem(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::OnConsumeItem(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UseItemFrame(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::HoldItemFrame(Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::AltFunctionUse(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateInventory(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateInfoAccessory(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateEquip(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateAccessory(Terraria.Item,Terraria.Player,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateVanity(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateVisibleAccessory(Terraria.Item,Terraria.Player,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateItemDye(Terraria.Item,Terraria.Player,System.Int32,System.Boolean)
//     System.String Terraria.ModLoader.GlobalItem::IsArmorSet(Terraria.Item,Terraria.Item,Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateArmorSet(Terraria.Player,System.String)
//     System.String Terraria.ModLoader.GlobalItem::IsVanitySet(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::PreUpdateVanitySet(Terraria.Player,System.String)
//     System.Void Terraria.ModLoader.GlobalItem::UpdateVanitySet(Terraria.Player,System.String)
//     System.Void Terraria.ModLoader.GlobalItem::ArmorSetShadows(Terraria.Player,System.String)
//     System.Void Terraria.ModLoader.GlobalItem::SetMatch(System.Int32,System.Int32,System.Boolean,System.Int32&,System.Boolean&)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanRightClick(Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::RightClick(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyItemLoot(Terraria.Item,Terraria.ModLoader.ItemLoot)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanStack(Terraria.Item,Terraria.Item)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanStackInWorld(Terraria.Item,Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::OnStack(Terraria.Item,Terraria.Item,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::SplitStack(Terraria.Item,Terraria.Item,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::ReforgePrice(Terraria.Item,System.Int32&,System.Boolean&)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanReforge(Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::PreReforge(Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::PostReforge(Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::DrawArmorColor(Terraria.ModLoader.EquipType,System.Int32,Terraria.Player,System.Single,Microsoft.Xna.Framework.Color&,System.Int32&,Microsoft.Xna.Framework.Color&)
//     System.Void Terraria.ModLoader.GlobalItem::ArmorArmGlowMask(System.Int32,Terraria.Player,System.Single,System.Int32&,Microsoft.Xna.Framework.Color&)
//     System.Void Terraria.ModLoader.GlobalItem::VerticalWingSpeeds(Terraria.Item,Terraria.Player,System.Single&,System.Single&,System.Single&,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.GlobalItem::HorizontalWingSpeeds(Terraria.Item,Terraria.Player,System.Single&,System.Single&)
//     System.Boolean Terraria.ModLoader.GlobalItem::WingUpdate(System.Int32,Terraria.Player,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::Update(Terraria.Item,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.GlobalItem::PostUpdate(Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::GrabRange(Terraria.Item,Terraria.Player,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalItem::GrabStyle(Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanPickup(Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::OnPickup(Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::ItemSpace(Terraria.Item,Terraria.Player)
//     System.Nullable`1<Microsoft.Xna.Framework.Color> Terraria.ModLoader.GlobalItem::GetAlpha(Terraria.Item,Microsoft.Xna.Framework.Color)
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawInWorld(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,System.Single&,System.Single&,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawInWorld(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,System.Single,System.Single,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawInInventory(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Vector2,System.Single)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawInInventory(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Vector2,System.Single)
//     System.Nullable`1<Microsoft.Xna.Framework.Vector2> Terraria.ModLoader.GlobalItem::HoldoutOffset(System.Int32)
//     System.Nullable`1<Microsoft.Xna.Framework.Vector2> Terraria.ModLoader.GlobalItem::HoldoutOrigin(System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanEquipAccessory(Terraria.Item,Terraria.Player,System.Int32,System.Boolean)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanAccessoryBeEquippedWith(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::ExtractinatorUse(System.Int32,System.Int32,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::CaughtFishStack(System.Int32,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalItem::IsAnglerQuestAvailable(System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::AnglerChat(System.Int32,System.String&,System.String&)
//     System.Void Terraria.ModLoader.GlobalItem::AddRecipes()
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawTooltip(Terraria.Item,System.Collections.ObjectModel.ReadOnlyCollection`1<Terraria.ModLoader.TooltipLine>,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawTooltip(Terraria.Item,System.Collections.ObjectModel.ReadOnlyCollection`1<Terraria.ModLoader.DrawableTooltipLine>)
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawTooltipLine(Terraria.Item,Terraria.ModLoader.DrawableTooltipLine,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawTooltipLine(Terraria.Item,Terraria.ModLoader.DrawableTooltipLine)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyTooltips(Terraria.Item,System.Collections.Generic.List`1<Terraria.ModLoader.TooltipLine>)
public static partial class GlobalItemHooks
{
    public sealed partial class OnCreated
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.DataStructures.ItemCreationContext context
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.DataStructures.ItemCreationContext context
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnCreated_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnCreated")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnCreated; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnSpawn
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.DataStructures.IEntitySource source
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.DataStructures.IEntitySource source
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnSpawn_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnSpawn")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnSpawn; use a flag to disable behavior.");
        }
    }

    public sealed partial class ChoosePrefix
    {
        public delegate int Original(
            Terraria.Item item,
            Terraria.Utilities.UnifiedRandom rand
        );

        public delegate int Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Utilities.UnifiedRandom rand
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ChoosePrefix_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ChoosePrefix")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ChoosePrefix; use a flag to disable behavior.");
        }
    }

    public sealed partial class PrefixChance
    {
        public delegate bool? Original(
            Terraria.Item item,
            int pre,
            Terraria.Utilities.UnifiedRandom rand
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            int pre,
            Terraria.Utilities.UnifiedRandom rand
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PrefixChance_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PrefixChance")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PrefixChance; use a flag to disable behavior.");
        }
    }

    public sealed partial class AllowPrefix
    {
        public delegate bool Original(
            Terraria.Item item,
            int pre
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            int pre
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_AllowPrefix_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::AllowPrefix")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::AllowPrefix; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanUseItem
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanUseItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanUseItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanUseItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanAutoReuseItem
    {
        public delegate bool? Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanAutoReuseItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanAutoReuseItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanAutoReuseItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseStyle
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseStyle_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseStyle")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseStyle; use a flag to disable behavior.");
        }
    }

    public sealed partial class HoldStyle
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_HoldStyle_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::HoldStyle")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::HoldStyle; use a flag to disable behavior.");
        }
    }

    public sealed partial class HoldItem
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_HoldItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::HoldItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::HoldItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseTimeMultiplier
    {
        public delegate float Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate float Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseTimeMultiplier_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseTimeMultiplier")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseTimeMultiplier; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseAnimationMultiplier
    {
        public delegate float Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate float Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseAnimationMultiplier_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseAnimationMultiplier")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseAnimationMultiplier; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseSpeedMultiplier
    {
        public delegate float Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate float Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseSpeedMultiplier_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseSpeedMultiplier")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseSpeedMultiplier; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetHealLife
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_GetHealLife_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::GetHealLife")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::GetHealLife; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetHealMana
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_GetHealMana_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::GetHealMana")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::GetHealMana; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyManaCost
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref float reduce,
            ref float mult
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float reduce,
            ref float mult
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyManaCost_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyManaCost")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyManaCost; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnMissingMana
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            int neededMana
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int neededMana
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnMissingMana_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnMissingMana")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnMissingMana; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnConsumeMana
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            int manaConsumed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int manaConsumed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnConsumeMana_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnConsumeMana")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnConsumeMana; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyWeaponDamage
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier damage
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier damage
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyWeaponDamage_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyWeaponDamage")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyWeaponDamage; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyResearchSorting
    {
        public delegate void Original(
            Terraria.Item item,
            ref Terraria.ID.ContentSamples.CreativeHelper.ItemGroup itemGroup
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref Terraria.ID.ContentSamples.CreativeHelper.ItemGroup itemGroup
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyResearchSorting_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyResearchSorting")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyResearchSorting; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanConsumeBait
    {
        public delegate bool? Original(
            Terraria.Player player,
            Terraria.Item bait
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            Terraria.Item bait
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanConsumeBait_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanConsumeBait")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanConsumeBait; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanResearch
    {
        public delegate bool Original(
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanResearch_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanResearch")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanResearch; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnResearched
    {
        public delegate void Original(
            Terraria.Item item,
            bool fullyResearched
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            bool fullyResearched
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnResearched_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnResearched")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnResearched; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyWeaponKnockback
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier knockback
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyWeaponKnockback_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyWeaponKnockback")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyWeaponKnockback; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyWeaponCrit
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref float crit
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float crit
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyWeaponCrit_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyWeaponCrit")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyWeaponCrit; use a flag to disable behavior.");
        }
    }

    public sealed partial class NeedsAmmo
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_NeedsAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::NeedsAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::NeedsAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class PickAmmo
    {
        public delegate void Original(
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player,
            ref int type,
            ref float speed,
            ref Terraria.ModLoader.StatModifier damage,
            ref float knockback
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player,
            ref int type,
            ref float speed,
            ref Terraria.ModLoader.StatModifier damage,
            ref float knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PickAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PickAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PickAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanChooseAmmo
    {
        public delegate bool? Original(
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanChooseAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanChooseAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanChooseAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeChosenAsAmmo
    {
        public delegate bool? Original(
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanBeChosenAsAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanBeChosenAsAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanBeChosenAsAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanConsumeAmmo
    {
        public delegate bool Original(
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanConsumeAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanConsumeAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanConsumeAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeConsumedAsAmmo
    {
        public delegate bool Original(
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanBeConsumedAsAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanBeConsumedAsAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanBeConsumedAsAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnConsumeAmmo
    {
        public delegate void Original(
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnConsumeAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnConsumeAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnConsumeAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnConsumedAsAmmo
    {
        public delegate void Original(
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnConsumedAsAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnConsumedAsAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnConsumedAsAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanShoot
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanShoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanShoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanShoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyShootStats
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyShootStats_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyShootStats")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyShootStats; use a flag to disable behavior.");
        }
    }

    public sealed partial class Shoot
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 velocity,
            int type,
            int damage,
            float knockback
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 velocity,
            int type,
            int damage,
            float knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_Shoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::Shoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::Shoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseItemHitbox
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref Microsoft.Xna.Framework.Rectangle hitbox,
            ref bool noHitbox
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Microsoft.Xna.Framework.Rectangle hitbox,
            ref bool noHitbox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseItemHitbox_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseItemHitbox")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseItemHitbox; use a flag to disable behavior.");
        }
    }

    public sealed partial class MeleeEffects
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle hitbox
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle hitbox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_MeleeEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::MeleeEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::MeleeEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanCatchNPC
    {
        public delegate bool? Original(
            Terraria.Item item,
            Terraria.NPC target,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.NPC target,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanCatchNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanCatchNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanCatchNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnCatchNPC
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.NPC npc,
            Terraria.Player player,
            bool failed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.NPC npc,
            Terraria.Player player,
            bool failed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnCatchNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnCatchNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnCatchNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyItemScale
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref float scale
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float scale
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyItemScale_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyItemScale")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyItemScale; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitNPC
    {
        public delegate bool? Original(
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanMeleeAttackCollideWithNPC
    {
        public delegate bool? Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle meleeAttackHitbox,
            Terraria.Player player,
            Terraria.NPC target
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle meleeAttackHitbox,
            Terraria.Player player,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanMeleeAttackCollideWithNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanMeleeAttackCollideWithNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanMeleeAttackCollideWithNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitNPC
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitPvp
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanHitPvp_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanHitPvp")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanHitPvp; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitPvp
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyHitPvp_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyHitPvp")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyHitPvp; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitPvp
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnHitPvp_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnHitPvp")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnHitPvp; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseItem
    {
        public delegate bool? Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseAnimation
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseAnimation_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseAnimation")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseAnimation; use a flag to disable behavior.");
        }
    }

    public sealed partial class ConsumeItem
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ConsumeItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ConsumeItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ConsumeItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnConsumeItem
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnConsumeItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnConsumeItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnConsumeItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseItemFrame
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UseItemFrame_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UseItemFrame")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UseItemFrame; use a flag to disable behavior.");
        }
    }

    public sealed partial class HoldItemFrame
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_HoldItemFrame_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::HoldItemFrame")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::HoldItemFrame; use a flag to disable behavior.");
        }
    }

    public sealed partial class AltFunctionUse
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_AltFunctionUse_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::AltFunctionUse")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::AltFunctionUse; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateInventory
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateInventory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateInventory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateInventory; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateInfoAccessory
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateInfoAccessory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateInfoAccessory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateInfoAccessory; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateEquip
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateEquip_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateEquip")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateEquip; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateAccessory
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateAccessory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateAccessory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateAccessory; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateVanity
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateVanity_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateVanity")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateVanity; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateVisibleAccessory
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateVisibleAccessory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateVisibleAccessory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateVisibleAccessory; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateItemDye
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            int dye,
            bool hideVisual
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int dye,
            bool hideVisual
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateItemDye_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateItemDye")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateItemDye; use a flag to disable behavior.");
        }
    }

    public sealed partial class IsArmorSet
    {
        public delegate string Original(
            Terraria.Item head,
            Terraria.Item body,
            Terraria.Item legs
        );

        public delegate string Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item head,
            Terraria.Item body,
            Terraria.Item legs
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_IsArmorSet_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::IsArmorSet")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::IsArmorSet; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateArmorSet
    {
        public delegate void Original(
            Terraria.Player player,
            string set
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateArmorSet_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateArmorSet")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateArmorSet; use a flag to disable behavior.");
        }
    }

    public sealed partial class IsVanitySet
    {
        public delegate string Original(
            int head,
            int body,
            int legs
        );

        public delegate string Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int head,
            int body,
            int legs
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_IsVanitySet_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::IsVanitySet")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::IsVanitySet; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateVanitySet
    {
        public delegate void Original(
            Terraria.Player player,
            string set
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PreUpdateVanitySet_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PreUpdateVanitySet")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PreUpdateVanitySet; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateVanitySet
    {
        public delegate void Original(
            Terraria.Player player,
            string set
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_UpdateVanitySet_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::UpdateVanitySet")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::UpdateVanitySet; use a flag to disable behavior.");
        }
    }

    public sealed partial class ArmorSetShadows
    {
        public delegate void Original(
            Terraria.Player player,
            string set
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ArmorSetShadows_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ArmorSetShadows")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ArmorSetShadows; use a flag to disable behavior.");
        }
    }

    public sealed partial class SetMatch
    {
        public delegate void Original(
            int armorSlot,
            int type,
            bool male,
            ref int equipSlot,
            ref bool robes
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int armorSlot,
            int type,
            bool male,
            ref int equipSlot,
            ref bool robes
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_SetMatch_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::SetMatch")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::SetMatch; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanRightClick
    {
        public delegate bool Original(
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanRightClick_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanRightClick")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanRightClick; use a flag to disable behavior.");
        }
    }

    public sealed partial class RightClick
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_RightClick_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::RightClick")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::RightClick; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyItemLoot
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.ModLoader.ItemLoot itemLoot
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.ItemLoot itemLoot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyItemLoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyItemLoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyItemLoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanStack
    {
        public delegate bool Original(
            Terraria.Item destination,
            Terraria.Item source
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanStack_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanStack")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanStack; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanStackInWorld
    {
        public delegate bool Original(
            Terraria.Item destination,
            Terraria.Item source
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanStackInWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanStackInWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanStackInWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnStack
    {
        public delegate void Original(
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnStack_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnStack")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnStack; use a flag to disable behavior.");
        }
    }

    public sealed partial class SplitStack
    {
        public delegate void Original(
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_SplitStack_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::SplitStack")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::SplitStack; use a flag to disable behavior.");
        }
    }

    public sealed partial class ReforgePrice
    {
        public delegate bool Original(
            Terraria.Item item,
            ref int reforgePrice,
            ref bool canApplyDiscount
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref int reforgePrice,
            ref bool canApplyDiscount
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ReforgePrice_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ReforgePrice")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ReforgePrice; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanReforge
    {
        public delegate bool Original(
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanReforge_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanReforge")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanReforge; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreReforge
    {
        public delegate void Original(
            Terraria.Item item
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PreReforge_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PreReforge")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PreReforge; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostReforge
    {
        public delegate void Original(
            Terraria.Item item
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PostReforge_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PostReforge")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PostReforge; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawArmorColor
    {
        public delegate void Original(
            Terraria.ModLoader.EquipType type,
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref Microsoft.Xna.Framework.Color color,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color glowMaskColor
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.ModLoader.EquipType type,
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref Microsoft.Xna.Framework.Color color,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color glowMaskColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_DrawArmorColor_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::DrawArmorColor")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::DrawArmorColor; use a flag to disable behavior.");
        }
    }

    public sealed partial class ArmorArmGlowMask
    {
        public delegate void Original(
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color color
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color color
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ArmorArmGlowMask_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ArmorArmGlowMask")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ArmorArmGlowMask; use a flag to disable behavior.");
        }
    }

    public sealed partial class VerticalWingSpeeds
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref float ascentWhenFalling,
            ref float ascentWhenRising,
            ref float maxCanAscendMultiplier,
            ref float maxAscentMultiplier,
            ref float constantAscend
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float ascentWhenFalling,
            ref float ascentWhenRising,
            ref float maxCanAscendMultiplier,
            ref float maxAscentMultiplier,
            ref float constantAscend
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_VerticalWingSpeeds_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::VerticalWingSpeeds")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::VerticalWingSpeeds; use a flag to disable behavior.");
        }
    }

    public sealed partial class HorizontalWingSpeeds
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref float speed,
            ref float acceleration
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float speed,
            ref float acceleration
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_HorizontalWingSpeeds_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::HorizontalWingSpeeds")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::HorizontalWingSpeeds; use a flag to disable behavior.");
        }
    }

    public sealed partial class WingUpdate
    {
        public delegate bool Original(
            int wings,
            Terraria.Player player,
            bool inUse
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int wings,
            Terraria.Player player,
            bool inUse
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_WingUpdate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::WingUpdate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::WingUpdate; use a flag to disable behavior.");
        }
    }

    public sealed partial class Update
    {
        public delegate void Original(
            Terraria.Item item,
            ref float gravity,
            ref float maxFallSpeed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref float gravity,
            ref float maxFallSpeed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_Update_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::Update")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::Update; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdate
    {
        public delegate void Original(
            Terraria.Item item
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PostUpdate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PostUpdate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PostUpdate; use a flag to disable behavior.");
        }
    }

    public sealed partial class GrabRange
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.Player player,
            ref int grabRange
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref int grabRange
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_GrabRange_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::GrabRange")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::GrabRange; use a flag to disable behavior.");
        }
    }

    public sealed partial class GrabStyle
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_GrabStyle_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::GrabStyle")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::GrabStyle; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanPickup
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanPickup_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanPickup")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanPickup; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnPickup
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_OnPickup_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::OnPickup")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::OnPickup; use a flag to disable behavior.");
        }
    }

    public sealed partial class ItemSpace
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ItemSpace_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ItemSpace")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ItemSpace; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetAlpha
    {
        public delegate Microsoft.Xna.Framework.Color? Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Color lightColor
        );

        public delegate Microsoft.Xna.Framework.Color? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Color lightColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_GetAlpha_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::GetAlpha")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::GetAlpha; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDrawInWorld
    {
        public delegate bool Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            ref float rotation,
            ref float scale,
            int whoAmI
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            ref float rotation,
            ref float scale,
            int whoAmI
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PreDrawInWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PreDrawInWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PreDrawInWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawInWorld
    {
        public delegate void Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            float rotation,
            float scale,
            int whoAmI
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            float rotation,
            float scale,
            int whoAmI
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PostDrawInWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PostDrawInWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PostDrawInWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDrawInInventory
    {
        public delegate bool Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Color drawColor,
            Microsoft.Xna.Framework.Color itemColor,
            Microsoft.Xna.Framework.Vector2 origin,
            float scale
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Color drawColor,
            Microsoft.Xna.Framework.Color itemColor,
            Microsoft.Xna.Framework.Vector2 origin,
            float scale
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PreDrawInInventory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PreDrawInInventory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PreDrawInInventory; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawInInventory
    {
        public delegate void Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Color drawColor,
            Microsoft.Xna.Framework.Color itemColor,
            Microsoft.Xna.Framework.Vector2 origin,
            float scale
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Color drawColor,
            Microsoft.Xna.Framework.Color itemColor,
            Microsoft.Xna.Framework.Vector2 origin,
            float scale
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PostDrawInInventory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PostDrawInInventory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PostDrawInInventory; use a flag to disable behavior.");
        }
    }

    public sealed partial class HoldoutOffset
    {
        public delegate Microsoft.Xna.Framework.Vector2? Original(
            int type
        );

        public delegate Microsoft.Xna.Framework.Vector2? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_HoldoutOffset_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::HoldoutOffset")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::HoldoutOffset; use a flag to disable behavior.");
        }
    }

    public sealed partial class HoldoutOrigin
    {
        public delegate Microsoft.Xna.Framework.Vector2? Original(
            int type
        );

        public delegate Microsoft.Xna.Framework.Vector2? Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_HoldoutOrigin_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::HoldoutOrigin")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::HoldoutOrigin; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanEquipAccessory
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player player,
            int slot,
            bool modded
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int slot,
            bool modded
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanEquipAccessory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanEquipAccessory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanEquipAccessory; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanAccessoryBeEquippedWith
    {
        public delegate bool Original(
            Terraria.Item equippedItem,
            Terraria.Item incomingItem,
            Terraria.Player player
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item equippedItem,
            Terraria.Item incomingItem,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CanAccessoryBeEquippedWith_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CanAccessoryBeEquippedWith")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CanAccessoryBeEquippedWith; use a flag to disable behavior.");
        }
    }

    public sealed partial class ExtractinatorUse
    {
        public delegate void Original(
            int extractType,
            int extractinatorBlockType,
            ref int resultType,
            ref int resultStack
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int extractType,
            int extractinatorBlockType,
            ref int resultType,
            ref int resultStack
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ExtractinatorUse_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ExtractinatorUse")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ExtractinatorUse; use a flag to disable behavior.");
        }
    }

    public sealed partial class CaughtFishStack
    {
        public delegate void Original(
            int type,
            ref int stack
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int type,
            ref int stack
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_CaughtFishStack_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::CaughtFishStack")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::CaughtFishStack; use a flag to disable behavior.");
        }
    }

    public sealed partial class IsAnglerQuestAvailable
    {
        public delegate bool Original(
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_IsAnglerQuestAvailable_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::IsAnglerQuestAvailable")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::IsAnglerQuestAvailable; use a flag to disable behavior.");
        }
    }

    public sealed partial class AnglerChat
    {
        public delegate void Original(
            int type,
            ref string chat,
            ref string catchLocation
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            int type,
            ref string chat,
            ref string catchLocation
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_AnglerChat_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::AnglerChat")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::AnglerChat; use a flag to disable behavior.");
        }
    }

    public sealed partial class AddRecipes
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_AddRecipes_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::AddRecipes")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::AddRecipes; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDrawTooltip
    {
        public delegate bool Original(
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.TooltipLine> lines,
            ref int x,
            ref int y
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.TooltipLine> lines,
            ref int x,
            ref int y
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PreDrawTooltip_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PreDrawTooltip")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PreDrawTooltip; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawTooltip
    {
        public delegate void Original(
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.DrawableTooltipLine> lines
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.DrawableTooltipLine> lines
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PostDrawTooltip_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PostDrawTooltip")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PostDrawTooltip; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDrawTooltipLine
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line,
            ref int yOffset
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line,
            ref int yOffset
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PreDrawTooltipLine_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PreDrawTooltipLine")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PreDrawTooltipLine; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawTooltipLine
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_PostDrawTooltipLine_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::PostDrawTooltipLine")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::PostDrawTooltipLine; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyTooltips
    {
        public delegate void Original(
            Terraria.Item item,
            System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalItem_ModifyTooltips_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalItem::ModifyTooltips")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalItem::ModifyTooltips; use a flag to disable behavior.");
        }
    }
}

public sealed partial class GlobalItem_OnCreated_Impl(GlobalItemHooks.OnCreated.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnCreated_Impl(hook);
    }

    public override void OnCreated(
        Terraria.Item item,
        Terraria.DataStructures.ItemCreationContext context
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.DataStructures.ItemCreationContext context_captured
            ) => base.OnCreated(
                item_captured,
                context_captured
            ),
            this,
            item,
            context
        );
    }
}

public sealed partial class GlobalItem_OnSpawn_Impl(GlobalItemHooks.OnSpawn.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnSpawn_Impl(hook);
    }

    public override void OnSpawn(
        Terraria.Item item,
        Terraria.DataStructures.IEntitySource source
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.DataStructures.IEntitySource source_captured
            ) => base.OnSpawn(
                item_captured,
                source_captured
            ),
            this,
            item,
            source
        );
    }
}

public sealed partial class GlobalItem_ChoosePrefix_Impl(GlobalItemHooks.ChoosePrefix.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ChoosePrefix_Impl(hook);
    }

    public override int ChoosePrefix(
        Terraria.Item item,
        Terraria.Utilities.UnifiedRandom rand
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Utilities.UnifiedRandom rand_captured
            ) => base.ChoosePrefix(
                item_captured,
                rand_captured
            ),
            this,
            item,
            rand
        );
    }
}

public sealed partial class GlobalItem_PrefixChance_Impl(GlobalItemHooks.PrefixChance.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PrefixChance_Impl(hook);
    }

    public override bool? PrefixChance(
        Terraria.Item item,
        int pre,
        Terraria.Utilities.UnifiedRandom rand
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                int pre_captured,
                Terraria.Utilities.UnifiedRandom rand_captured
            ) => base.PrefixChance(
                item_captured,
                pre_captured,
                rand_captured
            ),
            this,
            item,
            pre,
            rand
        );
    }
}

public sealed partial class GlobalItem_AllowPrefix_Impl(GlobalItemHooks.AllowPrefix.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_AllowPrefix_Impl(hook);
    }

    public override bool AllowPrefix(
        Terraria.Item item,
        int pre
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                int pre_captured
            ) => base.AllowPrefix(
                item_captured,
                pre_captured
            ),
            this,
            item,
            pre
        );
    }
}

public sealed partial class GlobalItem_CanUseItem_Impl(GlobalItemHooks.CanUseItem.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanUseItem_Impl(hook);
    }

    public override bool CanUseItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.CanUseItem(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_CanAutoReuseItem_Impl(GlobalItemHooks.CanAutoReuseItem.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanAutoReuseItem_Impl(hook);
    }

    public override bool? CanAutoReuseItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.CanAutoReuseItem(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UseStyle_Impl(GlobalItemHooks.UseStyle.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseStyle_Impl(hook);
    }

    public override void UseStyle(
        Terraria.Item item,
        Terraria.Player player,
        Microsoft.Xna.Framework.Rectangle heldItemFrame
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Microsoft.Xna.Framework.Rectangle heldItemFrame_captured
            ) => base.UseStyle(
                item_captured,
                player_captured,
                heldItemFrame_captured
            ),
            this,
            item,
            player,
            heldItemFrame
        );
    }
}

public sealed partial class GlobalItem_HoldStyle_Impl(GlobalItemHooks.HoldStyle.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_HoldStyle_Impl(hook);
    }

    public override void HoldStyle(
        Terraria.Item item,
        Terraria.Player player,
        Microsoft.Xna.Framework.Rectangle heldItemFrame
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Microsoft.Xna.Framework.Rectangle heldItemFrame_captured
            ) => base.HoldStyle(
                item_captured,
                player_captured,
                heldItemFrame_captured
            ),
            this,
            item,
            player,
            heldItemFrame
        );
    }
}

public sealed partial class GlobalItem_HoldItem_Impl(GlobalItemHooks.HoldItem.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_HoldItem_Impl(hook);
    }

    public override void HoldItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.HoldItem(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UseTimeMultiplier_Impl(GlobalItemHooks.UseTimeMultiplier.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseTimeMultiplier_Impl(hook);
    }

    public override float UseTimeMultiplier(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UseTimeMultiplier(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UseAnimationMultiplier_Impl(GlobalItemHooks.UseAnimationMultiplier.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseAnimationMultiplier_Impl(hook);
    }

    public override float UseAnimationMultiplier(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UseAnimationMultiplier(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UseSpeedMultiplier_Impl(GlobalItemHooks.UseSpeedMultiplier.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseSpeedMultiplier_Impl(hook);
    }

    public override float UseSpeedMultiplier(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UseSpeedMultiplier(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_GetHealLife_Impl(GlobalItemHooks.GetHealLife.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_GetHealLife_Impl(hook);
    }

    public override void GetHealLife(
        Terraria.Item item,
        Terraria.Player player,
        bool quickHeal,
        ref int healValue
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                bool quickHeal_captured,
                ref int healValue_captured
            ) => base.GetHealLife(
                item_captured,
                player_captured,
                quickHeal_captured,
                ref healValue_captured
            ),
            this,
            item,
            player,
            quickHeal,
            ref healValue
        );
    }
}

public sealed partial class GlobalItem_GetHealMana_Impl(GlobalItemHooks.GetHealMana.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_GetHealMana_Impl(hook);
    }

    public override void GetHealMana(
        Terraria.Item item,
        Terraria.Player player,
        bool quickHeal,
        ref int healValue
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                bool quickHeal_captured,
                ref int healValue_captured
            ) => base.GetHealMana(
                item_captured,
                player_captured,
                quickHeal_captured,
                ref healValue_captured
            ),
            this,
            item,
            player,
            quickHeal,
            ref healValue
        );
    }
}

public sealed partial class GlobalItem_ModifyManaCost_Impl(GlobalItemHooks.ModifyManaCost.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyManaCost_Impl(hook);
    }

    public override void ModifyManaCost(
        Terraria.Item item,
        Terraria.Player player,
        ref float reduce,
        ref float mult
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref float reduce_captured,
                ref float mult_captured
            ) => base.ModifyManaCost(
                item_captured,
                player_captured,
                ref reduce_captured,
                ref mult_captured
            ),
            this,
            item,
            player,
            ref reduce,
            ref mult
        );
    }
}

public sealed partial class GlobalItem_OnMissingMana_Impl(GlobalItemHooks.OnMissingMana.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnMissingMana_Impl(hook);
    }

    public override void OnMissingMana(
        Terraria.Item item,
        Terraria.Player player,
        int neededMana
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                int neededMana_captured
            ) => base.OnMissingMana(
                item_captured,
                player_captured,
                neededMana_captured
            ),
            this,
            item,
            player,
            neededMana
        );
    }
}

public sealed partial class GlobalItem_OnConsumeMana_Impl(GlobalItemHooks.OnConsumeMana.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnConsumeMana_Impl(hook);
    }

    public override void OnConsumeMana(
        Terraria.Item item,
        Terraria.Player player,
        int manaConsumed
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                int manaConsumed_captured
            ) => base.OnConsumeMana(
                item_captured,
                player_captured,
                manaConsumed_captured
            ),
            this,
            item,
            player,
            manaConsumed
        );
    }
}

public sealed partial class GlobalItem_ModifyWeaponDamage_Impl(GlobalItemHooks.ModifyWeaponDamage.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyWeaponDamage_Impl(hook);
    }

    public override void ModifyWeaponDamage(
        Terraria.Item item,
        Terraria.Player player,
        ref Terraria.ModLoader.StatModifier damage
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref Terraria.ModLoader.StatModifier damage_captured
            ) => base.ModifyWeaponDamage(
                item_captured,
                player_captured,
                ref damage_captured
            ),
            this,
            item,
            player,
            ref damage
        );
    }
}

public sealed partial class GlobalItem_ModifyResearchSorting_Impl(GlobalItemHooks.ModifyResearchSorting.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyResearchSorting_Impl(hook);
    }

    public override void ModifyResearchSorting(
        Terraria.Item item,
        ref Terraria.ID.ContentSamples.CreativeHelper.ItemGroup itemGroup
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                ref Terraria.ID.ContentSamples.CreativeHelper.ItemGroup itemGroup_captured
            ) => base.ModifyResearchSorting(
                item_captured,
                ref itemGroup_captured
            ),
            this,
            item,
            ref itemGroup
        );
    }
}

public sealed partial class GlobalItem_CanConsumeBait_Impl(GlobalItemHooks.CanConsumeBait.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanConsumeBait_Impl(hook);
    }

    public override bool? CanConsumeBait(
        Terraria.Player player,
        Terraria.Item bait
    )
    {
        return hook(
            (
                Terraria.Player player_captured,
                Terraria.Item bait_captured
            ) => base.CanConsumeBait(
                player_captured,
                bait_captured
            ),
            this,
            player,
            bait
        );
    }
}

public sealed partial class GlobalItem_CanResearch_Impl(GlobalItemHooks.CanResearch.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanResearch_Impl(hook);
    }

    public override bool CanResearch(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.CanResearch(
                item_captured
            ),
            this,
            item
        );
    }
}

public sealed partial class GlobalItem_OnResearched_Impl(GlobalItemHooks.OnResearched.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnResearched_Impl(hook);
    }

    public override void OnResearched(
        Terraria.Item item,
        bool fullyResearched
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                bool fullyResearched_captured
            ) => base.OnResearched(
                item_captured,
                fullyResearched_captured
            ),
            this,
            item,
            fullyResearched
        );
    }
}

public sealed partial class GlobalItem_ModifyWeaponKnockback_Impl(GlobalItemHooks.ModifyWeaponKnockback.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyWeaponKnockback_Impl(hook);
    }

    public override void ModifyWeaponKnockback(
        Terraria.Item item,
        Terraria.Player player,
        ref Terraria.ModLoader.StatModifier knockback
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref Terraria.ModLoader.StatModifier knockback_captured
            ) => base.ModifyWeaponKnockback(
                item_captured,
                player_captured,
                ref knockback_captured
            ),
            this,
            item,
            player,
            ref knockback
        );
    }
}

public sealed partial class GlobalItem_ModifyWeaponCrit_Impl(GlobalItemHooks.ModifyWeaponCrit.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyWeaponCrit_Impl(hook);
    }

    public override void ModifyWeaponCrit(
        Terraria.Item item,
        Terraria.Player player,
        ref float crit
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref float crit_captured
            ) => base.ModifyWeaponCrit(
                item_captured,
                player_captured,
                ref crit_captured
            ),
            this,
            item,
            player,
            ref crit
        );
    }
}

public sealed partial class GlobalItem_NeedsAmmo_Impl(GlobalItemHooks.NeedsAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_NeedsAmmo_Impl(hook);
    }

    public override bool NeedsAmmo(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.NeedsAmmo(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_PickAmmo_Impl(GlobalItemHooks.PickAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PickAmmo_Impl(hook);
    }

    public override void PickAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo,
        Terraria.Player player,
        ref int type,
        ref float speed,
        ref Terraria.ModLoader.StatModifier damage,
        ref float knockback
    )
    {
        hook(
            (
                Terraria.Item weapon_captured,
                Terraria.Item ammo_captured,
                Terraria.Player player_captured,
                ref int type_captured,
                ref float speed_captured,
                ref Terraria.ModLoader.StatModifier damage_captured,
                ref float knockback_captured
            ) => base.PickAmmo(
                weapon_captured,
                ammo_captured,
                player_captured,
                ref type_captured,
                ref speed_captured,
                ref damage_captured,
                ref knockback_captured
            ),
            this,
            weapon,
            ammo,
            player,
            ref type,
            ref speed,
            ref damage,
            ref knockback
        );
    }
}

public sealed partial class GlobalItem_CanChooseAmmo_Impl(GlobalItemHooks.CanChooseAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanChooseAmmo_Impl(hook);
    }

    public override bool? CanChooseAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item weapon_captured,
                Terraria.Item ammo_captured,
                Terraria.Player player_captured
            ) => base.CanChooseAmmo(
                weapon_captured,
                ammo_captured,
                player_captured
            ),
            this,
            weapon,
            ammo,
            player
        );
    }
}

public sealed partial class GlobalItem_CanBeChosenAsAmmo_Impl(GlobalItemHooks.CanBeChosenAsAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanBeChosenAsAmmo_Impl(hook);
    }

    public override bool? CanBeChosenAsAmmo(
        Terraria.Item ammo,
        Terraria.Item weapon,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item ammo_captured,
                Terraria.Item weapon_captured,
                Terraria.Player player_captured
            ) => base.CanBeChosenAsAmmo(
                ammo_captured,
                weapon_captured,
                player_captured
            ),
            this,
            ammo,
            weapon,
            player
        );
    }
}

public sealed partial class GlobalItem_CanConsumeAmmo_Impl(GlobalItemHooks.CanConsumeAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanConsumeAmmo_Impl(hook);
    }

    public override bool CanConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item weapon_captured,
                Terraria.Item ammo_captured,
                Terraria.Player player_captured
            ) => base.CanConsumeAmmo(
                weapon_captured,
                ammo_captured,
                player_captured
            ),
            this,
            weapon,
            ammo,
            player
        );
    }
}

public sealed partial class GlobalItem_CanBeConsumedAsAmmo_Impl(GlobalItemHooks.CanBeConsumedAsAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanBeConsumedAsAmmo_Impl(hook);
    }

    public override bool CanBeConsumedAsAmmo(
        Terraria.Item ammo,
        Terraria.Item weapon,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item ammo_captured,
                Terraria.Item weapon_captured,
                Terraria.Player player_captured
            ) => base.CanBeConsumedAsAmmo(
                ammo_captured,
                weapon_captured,
                player_captured
            ),
            this,
            ammo,
            weapon,
            player
        );
    }
}

public sealed partial class GlobalItem_OnConsumeAmmo_Impl(GlobalItemHooks.OnConsumeAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnConsumeAmmo_Impl(hook);
    }

    public override void OnConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item weapon_captured,
                Terraria.Item ammo_captured,
                Terraria.Player player_captured
            ) => base.OnConsumeAmmo(
                weapon_captured,
                ammo_captured,
                player_captured
            ),
            this,
            weapon,
            ammo,
            player
        );
    }
}

public sealed partial class GlobalItem_OnConsumedAsAmmo_Impl(GlobalItemHooks.OnConsumedAsAmmo.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnConsumedAsAmmo_Impl(hook);
    }

    public override void OnConsumedAsAmmo(
        Terraria.Item ammo,
        Terraria.Item weapon,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item ammo_captured,
                Terraria.Item weapon_captured,
                Terraria.Player player_captured
            ) => base.OnConsumedAsAmmo(
                ammo_captured,
                weapon_captured,
                player_captured
            ),
            this,
            ammo,
            weapon,
            player
        );
    }
}

public sealed partial class GlobalItem_CanShoot_Impl(GlobalItemHooks.CanShoot.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanShoot_Impl(hook);
    }

    public override bool CanShoot(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.CanShoot(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_ModifyShootStats_Impl(GlobalItemHooks.ModifyShootStats.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyShootStats_Impl(hook);
    }

    public override void ModifyShootStats(
        Terraria.Item item,
        Terraria.Player player,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Vector2 velocity,
        ref int type,
        ref int damage,
        ref float knockback
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref Microsoft.Xna.Framework.Vector2 position_captured,
                ref Microsoft.Xna.Framework.Vector2 velocity_captured,
                ref int type_captured,
                ref int damage_captured,
                ref float knockback_captured
            ) => base.ModifyShootStats(
                item_captured,
                player_captured,
                ref position_captured,
                ref velocity_captured,
                ref type_captured,
                ref damage_captured,
                ref knockback_captured
            ),
            this,
            item,
            player,
            ref position,
            ref velocity,
            ref type,
            ref damage,
            ref knockback
        );
    }
}

public sealed partial class GlobalItem_Shoot_Impl(GlobalItemHooks.Shoot.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_Shoot_Impl(hook);
    }

    public override bool Shoot(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Vector2 velocity,
        int type,
        int damage,
        float knockback
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Vector2 velocity_captured,
                int type_captured,
                int damage_captured,
                float knockback_captured
            ) => base.Shoot(
                item_captured,
                player_captured,
                source_captured,
                position_captured,
                velocity_captured,
                type_captured,
                damage_captured,
                knockback_captured
            ),
            this,
            item,
            player,
            source,
            position,
            velocity,
            type,
            damage,
            knockback
        );
    }
}

public sealed partial class GlobalItem_UseItemHitbox_Impl(GlobalItemHooks.UseItemHitbox.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseItemHitbox_Impl(hook);
    }

    public override void UseItemHitbox(
        Terraria.Item item,
        Terraria.Player player,
        ref Microsoft.Xna.Framework.Rectangle hitbox,
        ref bool noHitbox
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref Microsoft.Xna.Framework.Rectangle hitbox_captured,
                ref bool noHitbox_captured
            ) => base.UseItemHitbox(
                item_captured,
                player_captured,
                ref hitbox_captured,
                ref noHitbox_captured
            ),
            this,
            item,
            player,
            ref hitbox,
            ref noHitbox
        );
    }
}

public sealed partial class GlobalItem_MeleeEffects_Impl(GlobalItemHooks.MeleeEffects.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_MeleeEffects_Impl(hook);
    }

    public override void MeleeEffects(
        Terraria.Item item,
        Terraria.Player player,
        Microsoft.Xna.Framework.Rectangle hitbox
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Microsoft.Xna.Framework.Rectangle hitbox_captured
            ) => base.MeleeEffects(
                item_captured,
                player_captured,
                hitbox_captured
            ),
            this,
            item,
            player,
            hitbox
        );
    }
}

public sealed partial class GlobalItem_CanCatchNPC_Impl(GlobalItemHooks.CanCatchNPC.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanCatchNPC_Impl(hook);
    }

    public override bool? CanCatchNPC(
        Terraria.Item item,
        Terraria.NPC target,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.NPC target_captured,
                Terraria.Player player_captured
            ) => base.CanCatchNPC(
                item_captured,
                target_captured,
                player_captured
            ),
            this,
            item,
            target,
            player
        );
    }
}

public sealed partial class GlobalItem_OnCatchNPC_Impl(GlobalItemHooks.OnCatchNPC.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnCatchNPC_Impl(hook);
    }

    public override void OnCatchNPC(
        Terraria.Item item,
        Terraria.NPC npc,
        Terraria.Player player,
        bool failed
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.NPC npc_captured,
                Terraria.Player player_captured,
                bool failed_captured
            ) => base.OnCatchNPC(
                item_captured,
                npc_captured,
                player_captured,
                failed_captured
            ),
            this,
            item,
            npc,
            player,
            failed
        );
    }
}

public sealed partial class GlobalItem_ModifyItemScale_Impl(GlobalItemHooks.ModifyItemScale.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyItemScale_Impl(hook);
    }

    public override void ModifyItemScale(
        Terraria.Item item,
        Terraria.Player player,
        ref float scale
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref float scale_captured
            ) => base.ModifyItemScale(
                item_captured,
                player_captured,
                ref scale_captured
            ),
            this,
            item,
            player,
            ref scale
        );
    }
}

public sealed partial class GlobalItem_CanHitNPC_Impl(GlobalItemHooks.CanHitNPC.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanHitNPC_Impl(hook);
    }

    public override bool? CanHitNPC(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Terraria.NPC target_captured
            ) => base.CanHitNPC(
                item_captured,
                player_captured,
                target_captured
            ),
            this,
            item,
            player,
            target
        );
    }
}

public sealed partial class GlobalItem_CanMeleeAttackCollideWithNPC_Impl(GlobalItemHooks.CanMeleeAttackCollideWithNPC.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanMeleeAttackCollideWithNPC_Impl(hook);
    }

    public override bool? CanMeleeAttackCollideWithNPC(
        Terraria.Item item,
        Microsoft.Xna.Framework.Rectangle meleeAttackHitbox,
        Terraria.Player player,
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Rectangle meleeAttackHitbox_captured,
                Terraria.Player player_captured,
                Terraria.NPC target_captured
            ) => base.CanMeleeAttackCollideWithNPC(
                item_captured,
                meleeAttackHitbox_captured,
                player_captured,
                target_captured
            ),
            this,
            item,
            meleeAttackHitbox,
            player,
            target
        );
    }
}

public sealed partial class GlobalItem_ModifyHitNPC_Impl(GlobalItemHooks.ModifyHitNPC.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyHitNPC_Impl(hook);
    }

    public override void ModifyHitNPC(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Terraria.NPC target_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitNPC(
                item_captured,
                player_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            item,
            player,
            target,
            ref modifiers
        );
    }
}

public sealed partial class GlobalItem_OnHitNPC_Impl(GlobalItemHooks.OnHitNPC.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnHitNPC_Impl(hook);
    }

    public override void OnHitNPC(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Terraria.NPC target_captured,
                Terraria.NPC.HitInfo hit_captured,
                int damageDone_captured
            ) => base.OnHitNPC(
                item_captured,
                player_captured,
                target_captured,
                hit_captured,
                damageDone_captured
            ),
            this,
            item,
            player,
            target,
            hit,
            damageDone
        );
    }
}

public sealed partial class GlobalItem_CanHitPvp_Impl(GlobalItemHooks.CanHitPvp.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanHitPvp_Impl(hook);
    }

    public override bool CanHitPvp(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.Player target
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Terraria.Player target_captured
            ) => base.CanHitPvp(
                item_captured,
                player_captured,
                target_captured
            ),
            this,
            item,
            player,
            target
        );
    }
}

public sealed partial class GlobalItem_ModifyHitPvp_Impl(GlobalItemHooks.ModifyHitPvp.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyHitPvp_Impl(hook);
    }

    public override void ModifyHitPvp(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.Player target,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Terraria.Player target_captured,
                ref Terraria.Player.HurtModifiers modifiers_captured
            ) => base.ModifyHitPvp(
                item_captured,
                player_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            item,
            player,
            target,
            ref modifiers
        );
    }
}

public sealed partial class GlobalItem_OnHitPvp_Impl(GlobalItemHooks.OnHitPvp.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnHitPvp_Impl(hook);
    }

    public override void OnHitPvp(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.Player target,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                Terraria.Player target_captured,
                Terraria.Player.HurtInfo hurtInfo_captured
            ) => base.OnHitPvp(
                item_captured,
                player_captured,
                target_captured,
                hurtInfo_captured
            ),
            this,
            item,
            player,
            target,
            hurtInfo
        );
    }
}

public sealed partial class GlobalItem_UseItem_Impl(GlobalItemHooks.UseItem.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseItem_Impl(hook);
    }

    public override bool? UseItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UseItem(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UseAnimation_Impl(GlobalItemHooks.UseAnimation.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseAnimation_Impl(hook);
    }

    public override void UseAnimation(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UseAnimation(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_ConsumeItem_Impl(GlobalItemHooks.ConsumeItem.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ConsumeItem_Impl(hook);
    }

    public override bool ConsumeItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.ConsumeItem(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_OnConsumeItem_Impl(GlobalItemHooks.OnConsumeItem.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnConsumeItem_Impl(hook);
    }

    public override void OnConsumeItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.OnConsumeItem(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UseItemFrame_Impl(GlobalItemHooks.UseItemFrame.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UseItemFrame_Impl(hook);
    }

    public override void UseItemFrame(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UseItemFrame(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_HoldItemFrame_Impl(GlobalItemHooks.HoldItemFrame.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_HoldItemFrame_Impl(hook);
    }

    public override void HoldItemFrame(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.HoldItemFrame(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_AltFunctionUse_Impl(GlobalItemHooks.AltFunctionUse.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_AltFunctionUse_Impl(hook);
    }

    public override bool AltFunctionUse(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.AltFunctionUse(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UpdateInventory_Impl(GlobalItemHooks.UpdateInventory.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateInventory_Impl(hook);
    }

    public override void UpdateInventory(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UpdateInventory(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UpdateInfoAccessory_Impl(GlobalItemHooks.UpdateInfoAccessory.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateInfoAccessory_Impl(hook);
    }

    public override void UpdateInfoAccessory(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UpdateInfoAccessory(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UpdateEquip_Impl(GlobalItemHooks.UpdateEquip.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateEquip_Impl(hook);
    }

    public override void UpdateEquip(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UpdateEquip(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UpdateAccessory_Impl(GlobalItemHooks.UpdateAccessory.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateAccessory_Impl(hook);
    }

    public override void UpdateAccessory(
        Terraria.Item item,
        Terraria.Player player,
        bool hideVisual
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                bool hideVisual_captured
            ) => base.UpdateAccessory(
                item_captured,
                player_captured,
                hideVisual_captured
            ),
            this,
            item,
            player,
            hideVisual
        );
    }
}

public sealed partial class GlobalItem_UpdateVanity_Impl(GlobalItemHooks.UpdateVanity.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateVanity_Impl(hook);
    }

    public override void UpdateVanity(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.UpdateVanity(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_UpdateVisibleAccessory_Impl(GlobalItemHooks.UpdateVisibleAccessory.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateVisibleAccessory_Impl(hook);
    }

    public override void UpdateVisibleAccessory(
        Terraria.Item item,
        Terraria.Player player,
        bool hideVisual
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                bool hideVisual_captured
            ) => base.UpdateVisibleAccessory(
                item_captured,
                player_captured,
                hideVisual_captured
            ),
            this,
            item,
            player,
            hideVisual
        );
    }
}

public sealed partial class GlobalItem_UpdateItemDye_Impl(GlobalItemHooks.UpdateItemDye.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateItemDye_Impl(hook);
    }

    public override void UpdateItemDye(
        Terraria.Item item,
        Terraria.Player player,
        int dye,
        bool hideVisual
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                int dye_captured,
                bool hideVisual_captured
            ) => base.UpdateItemDye(
                item_captured,
                player_captured,
                dye_captured,
                hideVisual_captured
            ),
            this,
            item,
            player,
            dye,
            hideVisual
        );
    }
}

public sealed partial class GlobalItem_IsArmorSet_Impl(GlobalItemHooks.IsArmorSet.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_IsArmorSet_Impl(hook);
    }

    public override string IsArmorSet(
        Terraria.Item head,
        Terraria.Item body,
        Terraria.Item legs
    )
    {
        return hook(
            (
                Terraria.Item head_captured,
                Terraria.Item body_captured,
                Terraria.Item legs_captured
            ) => base.IsArmorSet(
                head_captured,
                body_captured,
                legs_captured
            ),
            this,
            head,
            body,
            legs
        );
    }
}

public sealed partial class GlobalItem_UpdateArmorSet_Impl(GlobalItemHooks.UpdateArmorSet.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateArmorSet_Impl(hook);
    }

    public override void UpdateArmorSet(
        Terraria.Player player,
        string set
    )
    {
        hook(
            (
                Terraria.Player player_captured,
                string set_captured
            ) => base.UpdateArmorSet(
                player_captured,
                set_captured
            ),
            this,
            player,
            set
        );
    }
}

public sealed partial class GlobalItem_IsVanitySet_Impl(GlobalItemHooks.IsVanitySet.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_IsVanitySet_Impl(hook);
    }

    public override string IsVanitySet(
        int head,
        int body,
        int legs
    )
    {
        return hook(
            (
                int head_captured,
                int body_captured,
                int legs_captured
            ) => base.IsVanitySet(
                head_captured,
                body_captured,
                legs_captured
            ),
            this,
            head,
            body,
            legs
        );
    }
}

public sealed partial class GlobalItem_PreUpdateVanitySet_Impl(GlobalItemHooks.PreUpdateVanitySet.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PreUpdateVanitySet_Impl(hook);
    }

    public override void PreUpdateVanitySet(
        Terraria.Player player,
        string set
    )
    {
        hook(
            (
                Terraria.Player player_captured,
                string set_captured
            ) => base.PreUpdateVanitySet(
                player_captured,
                set_captured
            ),
            this,
            player,
            set
        );
    }
}

public sealed partial class GlobalItem_UpdateVanitySet_Impl(GlobalItemHooks.UpdateVanitySet.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_UpdateVanitySet_Impl(hook);
    }

    public override void UpdateVanitySet(
        Terraria.Player player,
        string set
    )
    {
        hook(
            (
                Terraria.Player player_captured,
                string set_captured
            ) => base.UpdateVanitySet(
                player_captured,
                set_captured
            ),
            this,
            player,
            set
        );
    }
}

public sealed partial class GlobalItem_ArmorSetShadows_Impl(GlobalItemHooks.ArmorSetShadows.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ArmorSetShadows_Impl(hook);
    }

    public override void ArmorSetShadows(
        Terraria.Player player,
        string set
    )
    {
        hook(
            (
                Terraria.Player player_captured,
                string set_captured
            ) => base.ArmorSetShadows(
                player_captured,
                set_captured
            ),
            this,
            player,
            set
        );
    }
}

public sealed partial class GlobalItem_SetMatch_Impl(GlobalItemHooks.SetMatch.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_SetMatch_Impl(hook);
    }

    public override void SetMatch(
        int armorSlot,
        int type,
        bool male,
        ref int equipSlot,
        ref bool robes
    )
    {
        hook(
            (
                int armorSlot_captured,
                int type_captured,
                bool male_captured,
                ref int equipSlot_captured,
                ref bool robes_captured
            ) => base.SetMatch(
                armorSlot_captured,
                type_captured,
                male_captured,
                ref equipSlot_captured,
                ref robes_captured
            ),
            this,
            armorSlot,
            type,
            male,
            ref equipSlot,
            ref robes
        );
    }
}

public sealed partial class GlobalItem_CanRightClick_Impl(GlobalItemHooks.CanRightClick.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanRightClick_Impl(hook);
    }

    public override bool CanRightClick(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.CanRightClick(
                item_captured
            ),
            this,
            item
        );
    }
}

public sealed partial class GlobalItem_RightClick_Impl(GlobalItemHooks.RightClick.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_RightClick_Impl(hook);
    }

    public override void RightClick(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.RightClick(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_ModifyItemLoot_Impl(GlobalItemHooks.ModifyItemLoot.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyItemLoot_Impl(hook);
    }

    public override void ModifyItemLoot(
        Terraria.Item item,
        Terraria.ModLoader.ItemLoot itemLoot
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.ModLoader.ItemLoot itemLoot_captured
            ) => base.ModifyItemLoot(
                item_captured,
                itemLoot_captured
            ),
            this,
            item,
            itemLoot
        );
    }
}

public sealed partial class GlobalItem_CanStack_Impl(GlobalItemHooks.CanStack.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanStack_Impl(hook);
    }

    public override bool CanStack(
        Terraria.Item destination,
        Terraria.Item source
    )
    {
        return hook(
            (
                Terraria.Item destination_captured,
                Terraria.Item source_captured
            ) => base.CanStack(
                destination_captured,
                source_captured
            ),
            this,
            destination,
            source
        );
    }
}

public sealed partial class GlobalItem_CanStackInWorld_Impl(GlobalItemHooks.CanStackInWorld.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanStackInWorld_Impl(hook);
    }

    public override bool CanStackInWorld(
        Terraria.Item destination,
        Terraria.Item source
    )
    {
        return hook(
            (
                Terraria.Item destination_captured,
                Terraria.Item source_captured
            ) => base.CanStackInWorld(
                destination_captured,
                source_captured
            ),
            this,
            destination,
            source
        );
    }
}

public sealed partial class GlobalItem_OnStack_Impl(GlobalItemHooks.OnStack.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnStack_Impl(hook);
    }

    public override void OnStack(
        Terraria.Item destination,
        Terraria.Item source,
        int numToTransfer
    )
    {
        hook(
            (
                Terraria.Item destination_captured,
                Terraria.Item source_captured,
                int numToTransfer_captured
            ) => base.OnStack(
                destination_captured,
                source_captured,
                numToTransfer_captured
            ),
            this,
            destination,
            source,
            numToTransfer
        );
    }
}

public sealed partial class GlobalItem_SplitStack_Impl(GlobalItemHooks.SplitStack.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_SplitStack_Impl(hook);
    }

    public override void SplitStack(
        Terraria.Item destination,
        Terraria.Item source,
        int numToTransfer
    )
    {
        hook(
            (
                Terraria.Item destination_captured,
                Terraria.Item source_captured,
                int numToTransfer_captured
            ) => base.SplitStack(
                destination_captured,
                source_captured,
                numToTransfer_captured
            ),
            this,
            destination,
            source,
            numToTransfer
        );
    }
}

public sealed partial class GlobalItem_ReforgePrice_Impl(GlobalItemHooks.ReforgePrice.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ReforgePrice_Impl(hook);
    }

    public override bool ReforgePrice(
        Terraria.Item item,
        ref int reforgePrice,
        ref bool canApplyDiscount
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                ref int reforgePrice_captured,
                ref bool canApplyDiscount_captured
            ) => base.ReforgePrice(
                item_captured,
                ref reforgePrice_captured,
                ref canApplyDiscount_captured
            ),
            this,
            item,
            ref reforgePrice,
            ref canApplyDiscount
        );
    }
}

public sealed partial class GlobalItem_CanReforge_Impl(GlobalItemHooks.CanReforge.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanReforge_Impl(hook);
    }

    public override bool CanReforge(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.CanReforge(
                item_captured
            ),
            this,
            item
        );
    }
}

public sealed partial class GlobalItem_PreReforge_Impl(GlobalItemHooks.PreReforge.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PreReforge_Impl(hook);
    }

    public override void PreReforge(
        Terraria.Item item
    )
    {
        hook(
            (
                Terraria.Item item_captured
            ) => base.PreReforge(
                item_captured
            ),
            this,
            item
        );
    }
}

public sealed partial class GlobalItem_PostReforge_Impl(GlobalItemHooks.PostReforge.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PostReforge_Impl(hook);
    }

    public override void PostReforge(
        Terraria.Item item
    )
    {
        hook(
            (
                Terraria.Item item_captured
            ) => base.PostReforge(
                item_captured
            ),
            this,
            item
        );
    }
}

public sealed partial class GlobalItem_DrawArmorColor_Impl(GlobalItemHooks.DrawArmorColor.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_DrawArmorColor_Impl(hook);
    }

    public override void DrawArmorColor(
        Terraria.ModLoader.EquipType type,
        int slot,
        Terraria.Player drawPlayer,
        float shadow,
        ref Microsoft.Xna.Framework.Color color,
        ref int glowMask,
        ref Microsoft.Xna.Framework.Color glowMaskColor
    )
    {
        hook(
            (
                Terraria.ModLoader.EquipType type_captured,
                int slot_captured,
                Terraria.Player drawPlayer_captured,
                float shadow_captured,
                ref Microsoft.Xna.Framework.Color color_captured,
                ref int glowMask_captured,
                ref Microsoft.Xna.Framework.Color glowMaskColor_captured
            ) => base.DrawArmorColor(
                type_captured,
                slot_captured,
                drawPlayer_captured,
                shadow_captured,
                ref color_captured,
                ref glowMask_captured,
                ref glowMaskColor_captured
            ),
            this,
            type,
            slot,
            drawPlayer,
            shadow,
            ref color,
            ref glowMask,
            ref glowMaskColor
        );
    }
}

public sealed partial class GlobalItem_ArmorArmGlowMask_Impl(GlobalItemHooks.ArmorArmGlowMask.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ArmorArmGlowMask_Impl(hook);
    }

    public override void ArmorArmGlowMask(
        int slot,
        Terraria.Player drawPlayer,
        float shadow,
        ref int glowMask,
        ref Microsoft.Xna.Framework.Color color
    )
    {
        hook(
            (
                int slot_captured,
                Terraria.Player drawPlayer_captured,
                float shadow_captured,
                ref int glowMask_captured,
                ref Microsoft.Xna.Framework.Color color_captured
            ) => base.ArmorArmGlowMask(
                slot_captured,
                drawPlayer_captured,
                shadow_captured,
                ref glowMask_captured,
                ref color_captured
            ),
            this,
            slot,
            drawPlayer,
            shadow,
            ref glowMask,
            ref color
        );
    }
}

public sealed partial class GlobalItem_VerticalWingSpeeds_Impl(GlobalItemHooks.VerticalWingSpeeds.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_VerticalWingSpeeds_Impl(hook);
    }

    public override void VerticalWingSpeeds(
        Terraria.Item item,
        Terraria.Player player,
        ref float ascentWhenFalling,
        ref float ascentWhenRising,
        ref float maxCanAscendMultiplier,
        ref float maxAscentMultiplier,
        ref float constantAscend
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref float ascentWhenFalling_captured,
                ref float ascentWhenRising_captured,
                ref float maxCanAscendMultiplier_captured,
                ref float maxAscentMultiplier_captured,
                ref float constantAscend_captured
            ) => base.VerticalWingSpeeds(
                item_captured,
                player_captured,
                ref ascentWhenFalling_captured,
                ref ascentWhenRising_captured,
                ref maxCanAscendMultiplier_captured,
                ref maxAscentMultiplier_captured,
                ref constantAscend_captured
            ),
            this,
            item,
            player,
            ref ascentWhenFalling,
            ref ascentWhenRising,
            ref maxCanAscendMultiplier,
            ref maxAscentMultiplier,
            ref constantAscend
        );
    }
}

public sealed partial class GlobalItem_HorizontalWingSpeeds_Impl(GlobalItemHooks.HorizontalWingSpeeds.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_HorizontalWingSpeeds_Impl(hook);
    }

    public override void HorizontalWingSpeeds(
        Terraria.Item item,
        Terraria.Player player,
        ref float speed,
        ref float acceleration
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref float speed_captured,
                ref float acceleration_captured
            ) => base.HorizontalWingSpeeds(
                item_captured,
                player_captured,
                ref speed_captured,
                ref acceleration_captured
            ),
            this,
            item,
            player,
            ref speed,
            ref acceleration
        );
    }
}

public sealed partial class GlobalItem_WingUpdate_Impl(GlobalItemHooks.WingUpdate.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_WingUpdate_Impl(hook);
    }

    public override bool WingUpdate(
        int wings,
        Terraria.Player player,
        bool inUse
    )
    {
        return hook(
            (
                int wings_captured,
                Terraria.Player player_captured,
                bool inUse_captured
            ) => base.WingUpdate(
                wings_captured,
                player_captured,
                inUse_captured
            ),
            this,
            wings,
            player,
            inUse
        );
    }
}

public sealed partial class GlobalItem_Update_Impl(GlobalItemHooks.Update.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_Update_Impl(hook);
    }

    public override void Update(
        Terraria.Item item,
        ref float gravity,
        ref float maxFallSpeed
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                ref float gravity_captured,
                ref float maxFallSpeed_captured
            ) => base.Update(
                item_captured,
                ref gravity_captured,
                ref maxFallSpeed_captured
            ),
            this,
            item,
            ref gravity,
            ref maxFallSpeed
        );
    }
}

public sealed partial class GlobalItem_PostUpdate_Impl(GlobalItemHooks.PostUpdate.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PostUpdate_Impl(hook);
    }

    public override void PostUpdate(
        Terraria.Item item
    )
    {
        hook(
            (
                Terraria.Item item_captured
            ) => base.PostUpdate(
                item_captured
            ),
            this,
            item
        );
    }
}

public sealed partial class GlobalItem_GrabRange_Impl(GlobalItemHooks.GrabRange.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_GrabRange_Impl(hook);
    }

    public override void GrabRange(
        Terraria.Item item,
        Terraria.Player player,
        ref int grabRange
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                ref int grabRange_captured
            ) => base.GrabRange(
                item_captured,
                player_captured,
                ref grabRange_captured
            ),
            this,
            item,
            player,
            ref grabRange
        );
    }
}

public sealed partial class GlobalItem_GrabStyle_Impl(GlobalItemHooks.GrabStyle.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_GrabStyle_Impl(hook);
    }

    public override bool GrabStyle(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.GrabStyle(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_CanPickup_Impl(GlobalItemHooks.CanPickup.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanPickup_Impl(hook);
    }

    public override bool CanPickup(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.CanPickup(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_OnPickup_Impl(GlobalItemHooks.OnPickup.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_OnPickup_Impl(hook);
    }

    public override bool OnPickup(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.OnPickup(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_ItemSpace_Impl(GlobalItemHooks.ItemSpace.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ItemSpace_Impl(hook);
    }

    public override bool ItemSpace(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.ItemSpace(
                item_captured,
                player_captured
            ),
            this,
            item,
            player
        );
    }
}

public sealed partial class GlobalItem_GetAlpha_Impl(GlobalItemHooks.GetAlpha.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_GetAlpha_Impl(hook);
    }

    public override Microsoft.Xna.Framework.Color? GetAlpha(
        Terraria.Item item,
        Microsoft.Xna.Framework.Color lightColor
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Color lightColor_captured
            ) => base.GetAlpha(
                item_captured,
                lightColor_captured
            ),
            this,
            item,
            lightColor
        );
    }
}

public sealed partial class GlobalItem_PreDrawInWorld_Impl(GlobalItemHooks.PreDrawInWorld.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PreDrawInWorld_Impl(hook);
    }

    public override bool PreDrawInWorld(
        Terraria.Item item,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Color lightColor,
        Microsoft.Xna.Framework.Color alphaColor,
        ref float rotation,
        ref float scale,
        int whoAmI
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Color lightColor_captured,
                Microsoft.Xna.Framework.Color alphaColor_captured,
                ref float rotation_captured,
                ref float scale_captured,
                int whoAmI_captured
            ) => base.PreDrawInWorld(
                item_captured,
                spriteBatch_captured,
                lightColor_captured,
                alphaColor_captured,
                ref rotation_captured,
                ref scale_captured,
                whoAmI_captured
            ),
            this,
            item,
            spriteBatch,
            lightColor,
            alphaColor,
            ref rotation,
            ref scale,
            whoAmI
        );
    }
}

public sealed partial class GlobalItem_PostDrawInWorld_Impl(GlobalItemHooks.PostDrawInWorld.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PostDrawInWorld_Impl(hook);
    }

    public override void PostDrawInWorld(
        Terraria.Item item,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Color lightColor,
        Microsoft.Xna.Framework.Color alphaColor,
        float rotation,
        float scale,
        int whoAmI
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Color lightColor_captured,
                Microsoft.Xna.Framework.Color alphaColor_captured,
                float rotation_captured,
                float scale_captured,
                int whoAmI_captured
            ) => base.PostDrawInWorld(
                item_captured,
                spriteBatch_captured,
                lightColor_captured,
                alphaColor_captured,
                rotation_captured,
                scale_captured,
                whoAmI_captured
            ),
            this,
            item,
            spriteBatch,
            lightColor,
            alphaColor,
            rotation,
            scale,
            whoAmI
        );
    }
}

public sealed partial class GlobalItem_PreDrawInInventory_Impl(GlobalItemHooks.PreDrawInInventory.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PreDrawInInventory_Impl(hook);
    }

    public override bool PreDrawInInventory(
        Terraria.Item item,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Color drawColor,
        Microsoft.Xna.Framework.Color itemColor,
        Microsoft.Xna.Framework.Vector2 origin,
        float scale
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Rectangle frame_captured,
                Microsoft.Xna.Framework.Color drawColor_captured,
                Microsoft.Xna.Framework.Color itemColor_captured,
                Microsoft.Xna.Framework.Vector2 origin_captured,
                float scale_captured
            ) => base.PreDrawInInventory(
                item_captured,
                spriteBatch_captured,
                position_captured,
                frame_captured,
                drawColor_captured,
                itemColor_captured,
                origin_captured,
                scale_captured
            ),
            this,
            item,
            spriteBatch,
            position,
            frame,
            drawColor,
            itemColor,
            origin,
            scale
        );
    }
}

public sealed partial class GlobalItem_PostDrawInInventory_Impl(GlobalItemHooks.PostDrawInInventory.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PostDrawInInventory_Impl(hook);
    }

    public override void PostDrawInInventory(
        Terraria.Item item,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Color drawColor,
        Microsoft.Xna.Framework.Color itemColor,
        Microsoft.Xna.Framework.Vector2 origin,
        float scale
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Rectangle frame_captured,
                Microsoft.Xna.Framework.Color drawColor_captured,
                Microsoft.Xna.Framework.Color itemColor_captured,
                Microsoft.Xna.Framework.Vector2 origin_captured,
                float scale_captured
            ) => base.PostDrawInInventory(
                item_captured,
                spriteBatch_captured,
                position_captured,
                frame_captured,
                drawColor_captured,
                itemColor_captured,
                origin_captured,
                scale_captured
            ),
            this,
            item,
            spriteBatch,
            position,
            frame,
            drawColor,
            itemColor,
            origin,
            scale
        );
    }
}

public sealed partial class GlobalItem_HoldoutOffset_Impl(GlobalItemHooks.HoldoutOffset.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_HoldoutOffset_Impl(hook);
    }

    public override Microsoft.Xna.Framework.Vector2? HoldoutOffset(
        int type
    )
    {
        return hook(
            (
                int type_captured
            ) => base.HoldoutOffset(
                type_captured
            ),
            this,
            type
        );
    }
}

public sealed partial class GlobalItem_HoldoutOrigin_Impl(GlobalItemHooks.HoldoutOrigin.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_HoldoutOrigin_Impl(hook);
    }

    public override Microsoft.Xna.Framework.Vector2? HoldoutOrigin(
        int type
    )
    {
        return hook(
            (
                int type_captured
            ) => base.HoldoutOrigin(
                type_captured
            ),
            this,
            type
        );
    }
}

public sealed partial class GlobalItem_CanEquipAccessory_Impl(GlobalItemHooks.CanEquipAccessory.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanEquipAccessory_Impl(hook);
    }

    public override bool CanEquipAccessory(
        Terraria.Item item,
        Terraria.Player player,
        int slot,
        bool modded
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player player_captured,
                int slot_captured,
                bool modded_captured
            ) => base.CanEquipAccessory(
                item_captured,
                player_captured,
                slot_captured,
                modded_captured
            ),
            this,
            item,
            player,
            slot,
            modded
        );
    }
}

public sealed partial class GlobalItem_CanAccessoryBeEquippedWith_Impl(GlobalItemHooks.CanAccessoryBeEquippedWith.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CanAccessoryBeEquippedWith_Impl(hook);
    }

    public override bool CanAccessoryBeEquippedWith(
        Terraria.Item equippedItem,
        Terraria.Item incomingItem,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.Item equippedItem_captured,
                Terraria.Item incomingItem_captured,
                Terraria.Player player_captured
            ) => base.CanAccessoryBeEquippedWith(
                equippedItem_captured,
                incomingItem_captured,
                player_captured
            ),
            this,
            equippedItem,
            incomingItem,
            player
        );
    }
}

public sealed partial class GlobalItem_ExtractinatorUse_Impl(GlobalItemHooks.ExtractinatorUse.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ExtractinatorUse_Impl(hook);
    }

    public override void ExtractinatorUse(
        int extractType,
        int extractinatorBlockType,
        ref int resultType,
        ref int resultStack
    )
    {
        hook(
            (
                int extractType_captured,
                int extractinatorBlockType_captured,
                ref int resultType_captured,
                ref int resultStack_captured
            ) => base.ExtractinatorUse(
                extractType_captured,
                extractinatorBlockType_captured,
                ref resultType_captured,
                ref resultStack_captured
            ),
            this,
            extractType,
            extractinatorBlockType,
            ref resultType,
            ref resultStack
        );
    }
}

public sealed partial class GlobalItem_CaughtFishStack_Impl(GlobalItemHooks.CaughtFishStack.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_CaughtFishStack_Impl(hook);
    }

    public override void CaughtFishStack(
        int type,
        ref int stack
    )
    {
        hook(
            (
                int type_captured,
                ref int stack_captured
            ) => base.CaughtFishStack(
                type_captured,
                ref stack_captured
            ),
            this,
            type,
            ref stack
        );
    }
}

public sealed partial class GlobalItem_IsAnglerQuestAvailable_Impl(GlobalItemHooks.IsAnglerQuestAvailable.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_IsAnglerQuestAvailable_Impl(hook);
    }

    public override bool IsAnglerQuestAvailable(
        int type
    )
    {
        return hook(
            (
                int type_captured
            ) => base.IsAnglerQuestAvailable(
                type_captured
            ),
            this,
            type
        );
    }
}

public sealed partial class GlobalItem_AnglerChat_Impl(GlobalItemHooks.AnglerChat.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_AnglerChat_Impl(hook);
    }

    public override void AnglerChat(
        int type,
        ref string chat,
        ref string catchLocation
    )
    {
        hook(
            (
                int type_captured,
                ref string chat_captured,
                ref string catchLocation_captured
            ) => base.AnglerChat(
                type_captured,
                ref chat_captured,
                ref catchLocation_captured
            ),
            this,
            type,
            ref chat,
            ref catchLocation
        );
    }
}

public sealed partial class GlobalItem_AddRecipes_Impl(GlobalItemHooks.AddRecipes.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_AddRecipes_Impl(hook);
    }

    public override void AddRecipes()
    {
        hook(
            () => base.AddRecipes(),
            this
        );
    }
}

public sealed partial class GlobalItem_PreDrawTooltip_Impl(GlobalItemHooks.PreDrawTooltip.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PreDrawTooltip_Impl(hook);
    }

    public override bool PreDrawTooltip(
        Terraria.Item item,
        System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.TooltipLine> lines,
        ref int x,
        ref int y
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.TooltipLine> lines_captured,
                ref int x_captured,
                ref int y_captured
            ) => base.PreDrawTooltip(
                item_captured,
                lines_captured,
                ref x_captured,
                ref y_captured
            ),
            this,
            item,
            lines,
            ref x,
            ref y
        );
    }
}

public sealed partial class GlobalItem_PostDrawTooltip_Impl(GlobalItemHooks.PostDrawTooltip.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PostDrawTooltip_Impl(hook);
    }

    public override void PostDrawTooltip(
        Terraria.Item item,
        System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.DrawableTooltipLine> lines
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.DrawableTooltipLine> lines_captured
            ) => base.PostDrawTooltip(
                item_captured,
                lines_captured
            ),
            this,
            item,
            lines
        );
    }
}

public sealed partial class GlobalItem_PreDrawTooltipLine_Impl(GlobalItemHooks.PreDrawTooltipLine.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PreDrawTooltipLine_Impl(hook);
    }

    public override bool PreDrawTooltipLine(
        Terraria.Item item,
        Terraria.ModLoader.DrawableTooltipLine line,
        ref int yOffset
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.ModLoader.DrawableTooltipLine line_captured,
                ref int yOffset_captured
            ) => base.PreDrawTooltipLine(
                item_captured,
                line_captured,
                ref yOffset_captured
            ),
            this,
            item,
            line,
            ref yOffset
        );
    }
}

public sealed partial class GlobalItem_PostDrawTooltipLine_Impl(GlobalItemHooks.PostDrawTooltipLine.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_PostDrawTooltipLine_Impl(hook);
    }

    public override void PostDrawTooltipLine(
        Terraria.Item item,
        Terraria.ModLoader.DrawableTooltipLine line
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.ModLoader.DrawableTooltipLine line_captured
            ) => base.PostDrawTooltipLine(
                item_captured,
                line_captured
            ),
            this,
            item,
            line
        );
    }
}

public sealed partial class GlobalItem_ModifyTooltips_Impl(GlobalItemHooks.ModifyTooltips.Definition hook) : Terraria.ModLoader.GlobalItem
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalItem Clone(Terraria.Item? from, Terraria.Item to)
    {
        return new GlobalItem_ModifyTooltips_Impl(hook);
    }

    public override void ModifyTooltips(
        Terraria.Item item,
        System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips_captured
            ) => base.ModifyTooltips(
                item_captured,
                tooltips_captured
            ),
            this,
            item,
            tooltips
        );
    }
}
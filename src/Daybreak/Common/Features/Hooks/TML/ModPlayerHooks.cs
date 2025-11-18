namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.ModPlayer':
//     System.Void Terraria.ModLoader.ModPlayer::Initialize()
//     System.Void Terraria.ModLoader.ModPlayer::ResetEffects()
//     System.Void Terraria.ModLoader.ModPlayer::ResetInfoAccessories()
//     System.Void Terraria.ModLoader.ModPlayer::RefreshInfoAccessoriesFromTeamPlayers(Terraria.Player)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyMaxStats(Terraria.ModLoader.StatModifier&,Terraria.ModLoader.StatModifier&)
//     System.Void Terraria.ModLoader.ModPlayer::UpdateDead()
//     System.Void Terraria.ModLoader.ModPlayer::PreSaveCustomData()
//     System.Void Terraria.ModLoader.ModPlayer::PreSavePlayer()
//     System.Void Terraria.ModLoader.ModPlayer::PostSavePlayer()
//     System.Void Terraria.ModLoader.ModPlayer::UpdateBadLifeRegen()
//     System.Void Terraria.ModLoader.ModPlayer::UpdateLifeRegen()
//     System.Void Terraria.ModLoader.ModPlayer::NaturalLifeRegen(System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::UpdateAutopause()
//     System.Void Terraria.ModLoader.ModPlayer::PreUpdate()
//     System.Void Terraria.ModLoader.ModPlayer::ProcessTriggers(Terraria.GameInput.TriggersSet)
//     System.Void Terraria.ModLoader.ModPlayer::ArmorSetBonusActivated()
//     System.Void Terraria.ModLoader.ModPlayer::ArmorSetBonusHeld(System.Int32)
//     System.Void Terraria.ModLoader.ModPlayer::SetControls()
//     System.Void Terraria.ModLoader.ModPlayer::PreUpdateBuffs()
//     System.Void Terraria.ModLoader.ModPlayer::PostUpdateBuffs()
//     System.Void Terraria.ModLoader.ModPlayer::UpdateEquips()
//     System.Void Terraria.ModLoader.ModPlayer::PostUpdateEquips()
//     System.Void Terraria.ModLoader.ModPlayer::UpdateVisibleAccessories()
//     System.Void Terraria.ModLoader.ModPlayer::UpdateVisibleVanityAccessories()
//     System.Void Terraria.ModLoader.ModPlayer::UpdateDyes()
//     System.Void Terraria.ModLoader.ModPlayer::PostUpdateMiscEffects()
//     System.Void Terraria.ModLoader.ModPlayer::PostUpdateRunSpeeds()
//     System.Void Terraria.ModLoader.ModPlayer::PreUpdateMovement()
//     System.Void Terraria.ModLoader.ModPlayer::PostUpdate()
//     System.Void Terraria.ModLoader.ModPlayer::ModifyExtraJumpDurationMultiplier(Terraria.ModLoader.ExtraJump,System.Single&)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanStartExtraJump(Terraria.ModLoader.ExtraJump)
//     System.Void Terraria.ModLoader.ModPlayer::OnExtraJumpStarted(Terraria.ModLoader.ExtraJump,System.Boolean&)
//     System.Void Terraria.ModLoader.ModPlayer::OnExtraJumpEnded(Terraria.ModLoader.ExtraJump)
//     System.Void Terraria.ModLoader.ModPlayer::OnExtraJumpRefreshed(Terraria.ModLoader.ExtraJump)
//     System.Void Terraria.ModLoader.ModPlayer::ExtraJumpVisuals(Terraria.ModLoader.ExtraJump)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanShowExtraJumpVisuals(Terraria.ModLoader.ExtraJump)
//     System.Void Terraria.ModLoader.ModPlayer::OnExtraJumpCleared(Terraria.ModLoader.ExtraJump)
//     System.Void Terraria.ModLoader.ModPlayer::FrameEffects()
//     System.Boolean Terraria.ModLoader.ModPlayer::ImmuneTo(Terraria.DataStructures.PlayerDeathReason,System.Int32,System.Boolean)
//     System.Boolean Terraria.ModLoader.ModPlayer::FreeDodge(Terraria.Player/HurtInfo)
//     System.Boolean Terraria.ModLoader.ModPlayer::ConsumableDodge(Terraria.Player/HurtInfo)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHurt(Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHurt(Terraria.Player/HurtInfo)
//     System.Void Terraria.ModLoader.ModPlayer::PostHurt(Terraria.Player/HurtInfo)
//     System.Boolean Terraria.ModLoader.ModPlayer::PreKill(System.Double,System.Int32,System.Boolean,System.Boolean&,System.Boolean&,Terraria.DataStructures.PlayerDeathReason&)
//     System.Void Terraria.ModLoader.ModPlayer::Kill(System.Double,System.Int32,System.Boolean,Terraria.DataStructures.PlayerDeathReason)
//     System.Boolean Terraria.ModLoader.ModPlayer::PreModifyLuck(System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyLuck(System.Single&)
//     System.Boolean Terraria.ModLoader.ModPlayer::PreItemCheck()
//     System.Void Terraria.ModLoader.ModPlayer::PostItemCheck()
//     System.Single Terraria.ModLoader.ModPlayer::UseTimeMultiplier(Terraria.Item)
//     System.Single Terraria.ModLoader.ModPlayer::UseAnimationMultiplier(Terraria.Item)
//     System.Single Terraria.ModLoader.ModPlayer::UseSpeedMultiplier(Terraria.Item)
//     System.Void Terraria.ModLoader.ModPlayer::GetHealLife(Terraria.Item,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.ModPlayer::GetHealMana(Terraria.Item,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyManaCost(Terraria.Item,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::OnMissingMana(Terraria.Item,System.Int32)
//     System.Void Terraria.ModLoader.ModPlayer::OnConsumeMana(Terraria.Item,System.Int32)
//     System.Boolean Terraria.ModLoader.ModPlayer::ApplyPotionDelay(Terraria.Item,System.Int32)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyWeaponDamage(Terraria.Item,Terraria.ModLoader.StatModifier&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyWeaponKnockback(Terraria.Item,Terraria.ModLoader.StatModifier&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyWeaponCrit(Terraria.Item,System.Single&)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanConsumeAmmo(Terraria.Item,Terraria.Item)
//     System.Void Terraria.ModLoader.ModPlayer::OnConsumeAmmo(Terraria.Item,Terraria.Item)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanShoot(Terraria.Item)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyShootStats(Terraria.Item,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Vector2&,System.Int32&,System.Int32&,System.Single&)
//     System.Boolean Terraria.ModLoader.ModPlayer::Shoot(Terraria.Item,Terraria.DataStructures.EntitySource_ItemUse_WithAmmo,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Single)
//     System.Void Terraria.ModLoader.ModPlayer::MeleeEffects(Terraria.Item,Microsoft.Xna.Framework.Rectangle)
//     System.Void Terraria.ModLoader.ModPlayer::EmitEnchantmentVisualsAt(Terraria.Projectile,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.ModPlayer::CanCatchNPC(Terraria.NPC,Terraria.Item)
//     System.Void Terraria.ModLoader.ModPlayer::OnCatchNPC(Terraria.NPC,Terraria.Item,System.Boolean)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyItemScale(Terraria.Item,System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitAnything(System.Single,System.Single,Terraria.Entity)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanHitNPC(Terraria.NPC)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.ModPlayer::CanMeleeAttackCollideWithNPC(Terraria.Item,Microsoft.Xna.Framework.Rectangle,Terraria.NPC)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHitNPC(Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitNPC(Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.ModPlayer::CanHitNPCWithItem(Terraria.Item,Terraria.NPC)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHitNPCWithItem(Terraria.Item,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitNPCWithItem(Terraria.Item,Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.ModPlayer::CanHitNPCWithProj(Terraria.Projectile,Terraria.NPC)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHitNPCWithProj(Terraria.Projectile,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitNPCWithProj(Terraria.Projectile,Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanHitPvp(Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanHitPvpWithProj(Terraria.Projectile,Terraria.Player)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanBeHitByNPC(Terraria.NPC,System.Int32&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHitByNPC(Terraria.NPC,Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitByNPC(Terraria.NPC,Terraria.Player/HurtInfo)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanBeHitByProjectile(Terraria.Projectile)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHitByProjectile(Terraria.Projectile,Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitByProjectile(Terraria.Projectile,Terraria.Player/HurtInfo)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyFishingAttempt(Terraria.DataStructures.FishingAttempt&)
//     System.Void Terraria.ModLoader.ModPlayer::CatchFish(Terraria.DataStructures.FishingAttempt,System.Int32&,System.Int32&,Terraria.AdvancedPopupRequest&,Microsoft.Xna.Framework.Vector2&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyCaughtFish(Terraria.Item)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.ModPlayer::CanConsumeBait(Terraria.Item)
//     System.Void Terraria.ModLoader.ModPlayer::GetFishingLevel(Terraria.Item,Terraria.Item,System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::AnglerQuestReward(System.Single,System.Collections.Generic.List`1<Terraria.Item>)
//     System.Void Terraria.ModLoader.ModPlayer::GetDyeTraderReward(System.Collections.Generic.List`1<System.Int32>)
//     System.Void Terraria.ModLoader.ModPlayer::DrawEffects(Terraria.DataStructures.PlayerDrawSet,System.Single&,System.Single&,System.Single&,System.Single&,System.Boolean&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyDrawInfo(Terraria.DataStructures.PlayerDrawSet&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyDrawLayerOrdering(System.Collections.Generic.IDictionary`2<Terraria.ModLoader.PlayerDrawLayer,Terraria.ModLoader.PlayerDrawLayer/Position>)
//     System.Void Terraria.ModLoader.ModPlayer::HideDrawLayers(Terraria.DataStructures.PlayerDrawSet)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyScreenPosition()
//     System.Void Terraria.ModLoader.ModPlayer::ModifyZoom(System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::PlayerConnect()
//     System.Void Terraria.ModLoader.ModPlayer::PlayerDisconnect()
//     System.Void Terraria.ModLoader.ModPlayer::OnEnterWorld()
//     System.Void Terraria.ModLoader.ModPlayer::OnRespawn()
//     System.Boolean Terraria.ModLoader.ModPlayer::ShiftClickSlot(Terraria.Item[],System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.ModPlayer::HoverSlot(Terraria.Item[],System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.ModPlayer::PostSellItem(Terraria.NPC,Terraria.Item[],Terraria.Item)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanSellItem(Terraria.NPC,Terraria.Item[],Terraria.Item)
//     System.Void Terraria.ModLoader.ModPlayer::PostBuyItem(Terraria.NPC,Terraria.Item[],Terraria.Item)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanBuyItem(Terraria.NPC,Terraria.Item[],Terraria.Item)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanUseItem(Terraria.Item)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.ModPlayer::CanAutoReuseItem(Terraria.Item)
//     System.Boolean Terraria.ModLoader.ModPlayer::ModifyNurseHeal(Terraria.NPC,System.Int32&,System.Boolean&,System.String&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyNursePrice(Terraria.NPC,System.Int32,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.ModPlayer::PostNurseHeal(Terraria.NPC,System.Int32,System.Boolean,System.Int32)
//     System.Collections.Generic.IEnumerable`1<Terraria.Item> Terraria.ModLoader.ModPlayer::AddStartingItems(System.Boolean)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyStartingInventory(System.Collections.Generic.IReadOnlyDictionary`2<System.String,System.Collections.Generic.List`1<Terraria.Item>>,System.Boolean)
//     System.Collections.Generic.IEnumerable`1<Terraria.Item> Terraria.ModLoader.ModPlayer::AddMaterialsForCrafting(Terraria.ModLoader.ModPlayer/ItemConsumedCallback&)
//     System.Boolean Terraria.ModLoader.ModPlayer::OnPickup(Terraria.Item)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanBeTeleportedTo(Microsoft.Xna.Framework.Vector2,System.String)
//     System.Void Terraria.ModLoader.ModPlayer::OnEquipmentLoadoutSwitched(System.Int32,System.Int32)
public static partial class ModPlayerHooks
{
    public sealed partial class Initialize
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_Initialize_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::Initialize")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::Initialize; use a flag to disable behavior.");
        }
    }

    public sealed partial class ResetEffects
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ResetEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ResetEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ResetEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class ResetInfoAccessories
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ResetInfoAccessories_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ResetInfoAccessories")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ResetInfoAccessories; use a flag to disable behavior.");
        }
    }

    public sealed partial class RefreshInfoAccessoriesFromTeamPlayers
    {
        public delegate void Original(
            Terraria.Player otherPlayer
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player otherPlayer
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_RefreshInfoAccessoriesFromTeamPlayers_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::RefreshInfoAccessoriesFromTeamPlayers")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::RefreshInfoAccessoriesFromTeamPlayers; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyMaxStats
    {
        public delegate void Original(
            out Terraria.ModLoader.StatModifier health,
            out Terraria.ModLoader.StatModifier mana
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            out Terraria.ModLoader.StatModifier health,
            out Terraria.ModLoader.StatModifier mana
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyMaxStats_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyMaxStats")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyMaxStats; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateDead
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateDead_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateDead")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateDead; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreSaveCustomData
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreSaveCustomData_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreSaveCustomData")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreSaveCustomData; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreSavePlayer
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreSavePlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreSavePlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreSavePlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostSavePlayer
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostSavePlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostSavePlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostSavePlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateBadLifeRegen
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateBadLifeRegen_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateBadLifeRegen")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateBadLifeRegen; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateLifeRegen
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateLifeRegen_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateLifeRegen")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateLifeRegen; use a flag to disable behavior.");
        }
    }

    public sealed partial class NaturalLifeRegen
    {
        public delegate void Original(
            ref float regen
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            ref float regen
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_NaturalLifeRegen_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::NaturalLifeRegen")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::NaturalLifeRegen; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateAutopause
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateAutopause_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateAutopause")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateAutopause; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdate
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreUpdate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreUpdate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreUpdate; use a flag to disable behavior.");
        }
    }

    public sealed partial class ProcessTriggers
    {
        public delegate void Original(
            Terraria.GameInput.TriggersSet triggersSet
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.GameInput.TriggersSet triggersSet
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ProcessTriggers_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ProcessTriggers")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ProcessTriggers; use a flag to disable behavior.");
        }
    }

    public sealed partial class ArmorSetBonusActivated
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ArmorSetBonusActivated_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ArmorSetBonusActivated")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ArmorSetBonusActivated; use a flag to disable behavior.");
        }
    }

    public sealed partial class ArmorSetBonusHeld
    {
        public delegate void Original(
            int holdTime
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            int holdTime
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ArmorSetBonusHeld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ArmorSetBonusHeld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ArmorSetBonusHeld; use a flag to disable behavior.");
        }
    }

    public sealed partial class SetControls
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_SetControls_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::SetControls")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::SetControls; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateBuffs
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreUpdateBuffs_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreUpdateBuffs")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreUpdateBuffs; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateBuffs
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostUpdateBuffs_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostUpdateBuffs")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostUpdateBuffs; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateEquips
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateEquips_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateEquips")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateEquips; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateEquips
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostUpdateEquips_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostUpdateEquips")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostUpdateEquips; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateVisibleAccessories
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateVisibleAccessories_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateVisibleAccessories")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateVisibleAccessories; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateVisibleVanityAccessories
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateVisibleVanityAccessories_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateVisibleVanityAccessories")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateVisibleVanityAccessories; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateDyes
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UpdateDyes_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UpdateDyes")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UpdateDyes; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateMiscEffects
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostUpdateMiscEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostUpdateMiscEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostUpdateMiscEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateRunSpeeds
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostUpdateRunSpeeds_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostUpdateRunSpeeds")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostUpdateRunSpeeds; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateMovement
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreUpdateMovement_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreUpdateMovement")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreUpdateMovement; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdate
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostUpdate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostUpdate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostUpdate; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyExtraJumpDurationMultiplier
    {
        public delegate void Original(
            Terraria.ModLoader.ExtraJump jump,
            ref float duration
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump,
            ref float duration
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyExtraJumpDurationMultiplier_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyExtraJumpDurationMultiplier")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyExtraJumpDurationMultiplier; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanStartExtraJump
    {
        public delegate bool Original(
            Terraria.ModLoader.ExtraJump jump
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanStartExtraJump_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanStartExtraJump")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanStartExtraJump; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnExtraJumpStarted
    {
        public delegate void Original(
            Terraria.ModLoader.ExtraJump jump,
            ref bool playSound
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump,
            ref bool playSound
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnExtraJumpStarted_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnExtraJumpStarted")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnExtraJumpStarted; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnExtraJumpEnded
    {
        public delegate void Original(
            Terraria.ModLoader.ExtraJump jump
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnExtraJumpEnded_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnExtraJumpEnded")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnExtraJumpEnded; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnExtraJumpRefreshed
    {
        public delegate void Original(
            Terraria.ModLoader.ExtraJump jump
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnExtraJumpRefreshed_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnExtraJumpRefreshed")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnExtraJumpRefreshed; use a flag to disable behavior.");
        }
    }

    public sealed partial class ExtraJumpVisuals
    {
        public delegate void Original(
            Terraria.ModLoader.ExtraJump jump
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ExtraJumpVisuals_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ExtraJumpVisuals")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ExtraJumpVisuals; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanShowExtraJumpVisuals
    {
        public delegate bool Original(
            Terraria.ModLoader.ExtraJump jump
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanShowExtraJumpVisuals_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanShowExtraJumpVisuals")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanShowExtraJumpVisuals; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnExtraJumpCleared
    {
        public delegate void Original(
            Terraria.ModLoader.ExtraJump jump
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnExtraJumpCleared_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnExtraJumpCleared")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnExtraJumpCleared; use a flag to disable behavior.");
        }
    }

    public sealed partial class FrameEffects
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_FrameEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::FrameEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::FrameEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class ImmuneTo
    {
        public delegate bool Original(
            Terraria.DataStructures.PlayerDeathReason damageSource,
            int cooldownCounter,
            bool dodgeable
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDeathReason damageSource,
            int cooldownCounter,
            bool dodgeable
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ImmuneTo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ImmuneTo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ImmuneTo; use a flag to disable behavior.");
        }
    }

    public sealed partial class FreeDodge
    {
        public delegate bool Original(
            Terraria.Player.HurtInfo info
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_FreeDodge_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::FreeDodge")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::FreeDodge; use a flag to disable behavior.");
        }
    }

    public sealed partial class ConsumableDodge
    {
        public delegate bool Original(
            Terraria.Player.HurtInfo info
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ConsumableDodge_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ConsumableDodge")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ConsumableDodge; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHurt
    {
        public delegate void Original(
            ref Terraria.Player.HurtModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyHurt_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyHurt")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyHurt; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHurt
    {
        public delegate void Original(
            Terraria.Player.HurtInfo info
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnHurt_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnHurt")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnHurt; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostHurt
    {
        public delegate void Original(
            Terraria.Player.HurtInfo info
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostHurt_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostHurt")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostHurt; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreKill
    {
        public delegate bool Original(
            double damage,
            int hitDirection,
            bool pvp,
            ref bool playSound,
            ref bool genDust,
            ref Terraria.DataStructures.PlayerDeathReason damageSource
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            double damage,
            int hitDirection,
            bool pvp,
            ref bool playSound,
            ref bool genDust,
            ref Terraria.DataStructures.PlayerDeathReason damageSource
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreKill_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreKill")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreKill; use a flag to disable behavior.");
        }
    }

    public sealed partial class Kill
    {
        public delegate void Original(
            double damage,
            int hitDirection,
            bool pvp,
            Terraria.DataStructures.PlayerDeathReason damageSource
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            double damage,
            int hitDirection,
            bool pvp,
            Terraria.DataStructures.PlayerDeathReason damageSource
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_Kill_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::Kill")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::Kill; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreModifyLuck
    {
        public delegate bool Original(
            ref float luck
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            ref float luck
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreModifyLuck_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreModifyLuck")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreModifyLuck; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyLuck
    {
        public delegate void Original(
            ref float luck
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            ref float luck
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyLuck_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyLuck")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyLuck; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreItemCheck
    {
        public delegate bool Original();

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PreItemCheck_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PreItemCheck")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PreItemCheck; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostItemCheck
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostItemCheck_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostItemCheck")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostItemCheck; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseTimeMultiplier
    {
        public delegate float Original(
            Terraria.Item item
        );

        public delegate float Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UseTimeMultiplier_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UseTimeMultiplier")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UseTimeMultiplier; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseAnimationMultiplier
    {
        public delegate float Original(
            Terraria.Item item
        );

        public delegate float Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UseAnimationMultiplier_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UseAnimationMultiplier")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UseAnimationMultiplier; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseSpeedMultiplier
    {
        public delegate float Original(
            Terraria.Item item
        );

        public delegate float Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_UseSpeedMultiplier_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::UseSpeedMultiplier")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::UseSpeedMultiplier; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetHealLife
    {
        public delegate void Original(
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_GetHealLife_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::GetHealLife")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::GetHealLife; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetHealMana
    {
        public delegate void Original(
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_GetHealMana_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::GetHealMana")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::GetHealMana; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyManaCost
    {
        public delegate void Original(
            Terraria.Item item,
            ref float reduce,
            ref float mult
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float reduce,
            ref float mult
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyManaCost_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyManaCost")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyManaCost; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnMissingMana
    {
        public delegate void Original(
            Terraria.Item item,
            int neededMana
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            int neededMana
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnMissingMana_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnMissingMana")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnMissingMana; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnConsumeMana
    {
        public delegate void Original(
            Terraria.Item item,
            int manaConsumed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            int manaConsumed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnConsumeMana_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnConsumeMana")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnConsumeMana; use a flag to disable behavior.");
        }
    }

    public sealed partial class ApplyPotionDelay
    {
        public delegate bool Original(
            Terraria.Item item,
            int potionDelay
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            int potionDelay
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ApplyPotionDelay_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ApplyPotionDelay")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ApplyPotionDelay; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyWeaponDamage
    {
        public delegate void Original(
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier damage
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier damage
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyWeaponDamage_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyWeaponDamage")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyWeaponDamage; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyWeaponKnockback
    {
        public delegate void Original(
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier knockback
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyWeaponKnockback_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyWeaponKnockback")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyWeaponKnockback; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyWeaponCrit
    {
        public delegate void Original(
            Terraria.Item item,
            ref float crit
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float crit
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyWeaponCrit_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyWeaponCrit")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyWeaponCrit; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanConsumeAmmo
    {
        public delegate bool Original(
            Terraria.Item weapon,
            Terraria.Item ammo
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item weapon,
            Terraria.Item ammo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanConsumeAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanConsumeAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanConsumeAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnConsumeAmmo
    {
        public delegate void Original(
            Terraria.Item weapon,
            Terraria.Item ammo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item weapon,
            Terraria.Item ammo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnConsumeAmmo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnConsumeAmmo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnConsumeAmmo; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanShoot
    {
        public delegate bool Original(
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanShoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanShoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanShoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyShootStats
    {
        public delegate void Original(
            Terraria.Item item,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyShootStats_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyShootStats")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyShootStats; use a flag to disable behavior.");
        }
    }

    public sealed partial class Shoot
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 velocity,
            int type,
            int damage,
            float knockback
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 velocity,
            int type,
            int damage,
            float knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_Shoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::Shoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::Shoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class MeleeEffects
    {
        public delegate void Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle hitbox
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle hitbox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_MeleeEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::MeleeEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::MeleeEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class EmitEnchantmentVisualsAt
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 boxPosition,
            int boxWidth,
            int boxHeight
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 boxPosition,
            int boxWidth,
            int boxHeight
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_EmitEnchantmentVisualsAt_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::EmitEnchantmentVisualsAt")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::EmitEnchantmentVisualsAt; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanCatchNPC
    {
        public delegate bool? Original(
            Terraria.NPC target,
            Terraria.Item item
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanCatchNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanCatchNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanCatchNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnCatchNPC
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Item item,
            bool failed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            Terraria.Item item,
            bool failed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnCatchNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnCatchNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnCatchNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyItemScale
    {
        public delegate void Original(
            Terraria.Item item,
            ref float scale
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float scale
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyItemScale_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyItemScale")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyItemScale; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitAnything
    {
        public delegate void Original(
            float x,
            float y,
            Terraria.Entity victim
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            float x,
            float y,
            Terraria.Entity victim
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnHitAnything_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnHitAnything")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnHitAnything; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitNPC
    {
        public delegate bool Original(
            Terraria.NPC target
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanMeleeAttackCollideWithNPC
    {
        public delegate bool? Original(
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle meleeAttackHitbox,
            Terraria.NPC target
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle meleeAttackHitbox,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanMeleeAttackCollideWithNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanMeleeAttackCollideWithNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanMeleeAttackCollideWithNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitNPC
    {
        public delegate void Original(
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Original(
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitNPCWithItem
    {
        public delegate bool? Original(
            Terraria.Item item,
            Terraria.NPC target
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanHitNPCWithItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanHitNPCWithItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanHitNPCWithItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitNPCWithItem
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyHitNPCWithItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyHitNPCWithItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyHitNPCWithItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitNPCWithItem
    {
        public delegate void Original(
            Terraria.Item item,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnHitNPCWithItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnHitNPCWithItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnHitNPCWithItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitNPCWithProj
    {
        public delegate bool? Original(
            Terraria.Projectile proj,
            Terraria.NPC target
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanHitNPCWithProj_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanHitNPCWithProj")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanHitNPCWithProj; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitNPCWithProj
    {
        public delegate void Original(
            Terraria.Projectile proj,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyHitNPCWithProj_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyHitNPCWithProj")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyHitNPCWithProj; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitNPCWithProj
    {
        public delegate void Original(
            Terraria.Projectile proj,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnHitNPCWithProj_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnHitNPCWithProj")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnHitNPCWithProj; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitPvp
    {
        public delegate bool Original(
            Terraria.Item item,
            Terraria.Player target
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.Player target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanHitPvp_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanHitPvp")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanHitPvp; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitPvpWithProj
    {
        public delegate bool Original(
            Terraria.Projectile proj,
            Terraria.Player target
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.Player target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanHitPvpWithProj_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanHitPvpWithProj")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanHitPvpWithProj; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeHitByNPC
    {
        public delegate bool Original(
            Terraria.NPC npc,
            ref int cooldownSlot
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            ref int cooldownSlot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanBeHitByNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanBeHitByNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanBeHitByNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitByNPC
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyHitByNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyHitByNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyHitByNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitByNPC
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Player.HurtInfo hurtInfo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnHitByNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnHitByNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnHitByNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeHitByProjectile
    {
        public delegate bool Original(
            Terraria.Projectile proj
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanBeHitByProjectile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanBeHitByProjectile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanBeHitByProjectile; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitByProjectile
    {
        public delegate void Original(
            Terraria.Projectile proj,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyHitByProjectile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyHitByProjectile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyHitByProjectile; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitByProjectile
    {
        public delegate void Original(
            Terraria.Projectile proj,
            Terraria.Player.HurtInfo hurtInfo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnHitByProjectile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnHitByProjectile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnHitByProjectile; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyFishingAttempt
    {
        public delegate void Original(
            ref Terraria.DataStructures.FishingAttempt attempt
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.DataStructures.FishingAttempt attempt
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyFishingAttempt_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyFishingAttempt")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyFishingAttempt; use a flag to disable behavior.");
        }
    }

    public sealed partial class CatchFish
    {
        public delegate void Original(
            Terraria.DataStructures.FishingAttempt attempt,
            ref int itemDrop,
            ref int npcSpawn,
            ref Terraria.AdvancedPopupRequest sonar,
            ref Microsoft.Xna.Framework.Vector2 sonarPosition
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.FishingAttempt attempt,
            ref int itemDrop,
            ref int npcSpawn,
            ref Terraria.AdvancedPopupRequest sonar,
            ref Microsoft.Xna.Framework.Vector2 sonarPosition
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CatchFish_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CatchFish")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CatchFish; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyCaughtFish
    {
        public delegate void Original(
            Terraria.Item fish
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item fish
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyCaughtFish_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyCaughtFish")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyCaughtFish; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanConsumeBait
    {
        public delegate bool? Original(
            Terraria.Item bait
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item bait
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanConsumeBait_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanConsumeBait")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanConsumeBait; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetFishingLevel
    {
        public delegate void Original(
            Terraria.Item fishingRod,
            Terraria.Item bait,
            ref float fishingLevel
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item fishingRod,
            Terraria.Item bait,
            ref float fishingLevel
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_GetFishingLevel_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::GetFishingLevel")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::GetFishingLevel; use a flag to disable behavior.");
        }
    }

    public sealed partial class AnglerQuestReward
    {
        public delegate void Original(
            float rareMultiplier,
            System.Collections.Generic.List<Terraria.Item> rewardItems
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            float rareMultiplier,
            System.Collections.Generic.List<Terraria.Item> rewardItems
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_AnglerQuestReward_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::AnglerQuestReward")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::AnglerQuestReward; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetDyeTraderReward
    {
        public delegate void Original(
            System.Collections.Generic.List<int> rewardPool
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.List<int> rewardPool
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_GetDyeTraderReward_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::GetDyeTraderReward")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::GetDyeTraderReward; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawEffects
    {
        public delegate void Original(
            Terraria.DataStructures.PlayerDrawSet drawInfo,
            ref float r,
            ref float g,
            ref float b,
            ref float a,
            ref bool fullBright
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDrawSet drawInfo,
            ref float r,
            ref float g,
            ref float b,
            ref float a,
            ref bool fullBright
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_DrawEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::DrawEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::DrawEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyDrawInfo
    {
        public delegate void Original(
            ref Terraria.DataStructures.PlayerDrawSet drawInfo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.DataStructures.PlayerDrawSet drawInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyDrawInfo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyDrawInfo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyDrawInfo; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyDrawLayerOrdering
    {
        public delegate void Original(
            System.Collections.Generic.IDictionary<Terraria.ModLoader.PlayerDrawLayer, Terraria.ModLoader.PlayerDrawLayer.Position> positions
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.IDictionary<Terraria.ModLoader.PlayerDrawLayer, Terraria.ModLoader.PlayerDrawLayer.Position> positions
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyDrawLayerOrdering_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyDrawLayerOrdering")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyDrawLayerOrdering; use a flag to disable behavior.");
        }
    }

    public sealed partial class HideDrawLayers
    {
        public delegate void Original(
            Terraria.DataStructures.PlayerDrawSet drawInfo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDrawSet drawInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_HideDrawLayers_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::HideDrawLayers")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::HideDrawLayers; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyScreenPosition
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyScreenPosition_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyScreenPosition")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyScreenPosition; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyZoom
    {
        public delegate void Original(
            ref float zoom
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            ref float zoom
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyZoom_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyZoom")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyZoom; use a flag to disable behavior.");
        }
    }

    public sealed partial class PlayerConnect
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PlayerConnect_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PlayerConnect")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PlayerConnect; use a flag to disable behavior.");
        }
    }

    public sealed partial class PlayerDisconnect
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PlayerDisconnect_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PlayerDisconnect")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PlayerDisconnect; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnEnterWorld
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnEnterWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnEnterWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnEnterWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnRespawn
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnRespawn_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnRespawn")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnRespawn; use a flag to disable behavior.");
        }
    }

    public sealed partial class ShiftClickSlot
    {
        public delegate bool Original(
            Terraria.Item[] inventory,
            int context,
            int slot
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item[] inventory,
            int context,
            int slot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ShiftClickSlot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ShiftClickSlot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ShiftClickSlot; use a flag to disable behavior.");
        }
    }

    public sealed partial class HoverSlot
    {
        public delegate bool Original(
            Terraria.Item[] inventory,
            int context,
            int slot
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item[] inventory,
            int context,
            int slot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_HoverSlot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::HoverSlot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::HoverSlot; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostSellItem
    {
        public delegate void Original(
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostSellItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostSellItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostSellItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanSellItem
    {
        public delegate bool Original(
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanSellItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanSellItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanSellItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostBuyItem
    {
        public delegate void Original(
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostBuyItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostBuyItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostBuyItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBuyItem
    {
        public delegate bool Original(
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanBuyItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanBuyItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanBuyItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanUseItem
    {
        public delegate bool Original(
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanUseItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanUseItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanUseItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanAutoReuseItem
    {
        public delegate bool? Original(
            Terraria.Item item
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanAutoReuseItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanAutoReuseItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanAutoReuseItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyNurseHeal
    {
        public delegate bool Original(
            Terraria.NPC nurse,
            ref int health,
            ref bool removeDebuffs,
            ref string chatText
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            ref int health,
            ref bool removeDebuffs,
            ref string chatText
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyNurseHeal_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyNurseHeal")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyNurseHeal; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyNursePrice
    {
        public delegate void Original(
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            ref int price
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            ref int price
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyNursePrice_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyNursePrice")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyNursePrice; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostNurseHeal
    {
        public delegate void Original(
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            int price
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            int price
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_PostNurseHeal_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::PostNurseHeal")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::PostNurseHeal; use a flag to disable behavior.");
        }
    }

    public sealed partial class AddStartingItems
    {
        public delegate System.Collections.Generic.IEnumerable<Terraria.Item> Original(
            bool mediumCoreDeath
        );

        public delegate System.Collections.Generic.IEnumerable<Terraria.Item> Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            bool mediumCoreDeath
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_AddStartingItems_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::AddStartingItems")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::AddStartingItems; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyStartingInventory
    {
        public delegate void Original(
            System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<Terraria.Item>> itemsByMod,
            bool mediumCoreDeath
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<Terraria.Item>> itemsByMod,
            bool mediumCoreDeath
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_ModifyStartingInventory_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::ModifyStartingInventory")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::ModifyStartingInventory; use a flag to disable behavior.");
        }
    }

    public sealed partial class AddMaterialsForCrafting
    {
        public delegate System.Collections.Generic.IEnumerable<Terraria.Item> Original(
            out Terraria.ModLoader.ModPlayer.ItemConsumedCallback itemConsumedCallback
        );

        public delegate System.Collections.Generic.IEnumerable<Terraria.Item> Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            out Terraria.ModLoader.ModPlayer.ItemConsumedCallback itemConsumedCallback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_AddMaterialsForCrafting_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::AddMaterialsForCrafting")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::AddMaterialsForCrafting; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnPickup
    {
        public delegate bool Original(
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnPickup_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnPickup")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnPickup; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeTeleportedTo
    {
        public delegate bool Original(
            Microsoft.Xna.Framework.Vector2 teleportPosition,
            string context
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            Microsoft.Xna.Framework.Vector2 teleportPosition,
            string context
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_CanBeTeleportedTo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::CanBeTeleportedTo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::CanBeTeleportedTo; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnEquipmentLoadoutSwitched
    {
        public delegate void Original(
            int oldLoadoutIndex,
            int loadoutIndex
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModPlayer self,
            int oldLoadoutIndex,
            int loadoutIndex
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModPlayer_OnEquipmentLoadoutSwitched_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModPlayer::OnEquipmentLoadoutSwitched")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModPlayer::OnEquipmentLoadoutSwitched; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_Initialize_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.Initialize.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_Initialize_Impl(ModPlayerHooks.Initialize.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void Initialize()
    {
        hook(
            () => base.Initialize(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ResetEffects_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ResetEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ResetEffects_Impl(ModPlayerHooks.ResetEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ResetEffects()
    {
        hook(
            () => base.ResetEffects(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ResetInfoAccessories_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ResetInfoAccessories.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ResetInfoAccessories_Impl(ModPlayerHooks.ResetInfoAccessories.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ResetInfoAccessories()
    {
        hook(
            () => base.ResetInfoAccessories(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_RefreshInfoAccessoriesFromTeamPlayers_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.RefreshInfoAccessoriesFromTeamPlayers.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_RefreshInfoAccessoriesFromTeamPlayers_Impl(ModPlayerHooks.RefreshInfoAccessoriesFromTeamPlayers.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void RefreshInfoAccessoriesFromTeamPlayers(
        Terraria.Player otherPlayer
    )
    {
        hook(
            (
                Terraria.Player otherPlayer_captured
            ) => base.RefreshInfoAccessoriesFromTeamPlayers(
                otherPlayer_captured
            ),
            this,
            otherPlayer
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyMaxStats_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyMaxStats.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyMaxStats_Impl(ModPlayerHooks.ModifyMaxStats.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyMaxStats(
        out Terraria.ModLoader.StatModifier health,
        out Terraria.ModLoader.StatModifier mana
    )
    {
        hook(
            (
                out Terraria.ModLoader.StatModifier health_captured,
                out Terraria.ModLoader.StatModifier mana_captured
            ) => base.ModifyMaxStats(
                out health_captured,
                out mana_captured
            ),
            this,
            out health,
            out mana
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateDead_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateDead.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateDead_Impl(ModPlayerHooks.UpdateDead.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateDead()
    {
        hook(
            () => base.UpdateDead(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreSaveCustomData_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreSaveCustomData.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreSaveCustomData_Impl(ModPlayerHooks.PreSaveCustomData.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreSaveCustomData()
    {
        hook(
            () => base.PreSaveCustomData(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreSavePlayer_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreSavePlayer.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreSavePlayer_Impl(ModPlayerHooks.PreSavePlayer.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreSavePlayer()
    {
        hook(
            () => base.PreSavePlayer(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostSavePlayer_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostSavePlayer.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostSavePlayer_Impl(ModPlayerHooks.PostSavePlayer.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostSavePlayer()
    {
        hook(
            () => base.PostSavePlayer(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateBadLifeRegen_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateBadLifeRegen.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateBadLifeRegen_Impl(ModPlayerHooks.UpdateBadLifeRegen.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateBadLifeRegen()
    {
        hook(
            () => base.UpdateBadLifeRegen(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateLifeRegen_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateLifeRegen.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateLifeRegen_Impl(ModPlayerHooks.UpdateLifeRegen.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateLifeRegen()
    {
        hook(
            () => base.UpdateLifeRegen(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_NaturalLifeRegen_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.NaturalLifeRegen.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_NaturalLifeRegen_Impl(ModPlayerHooks.NaturalLifeRegen.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void NaturalLifeRegen(
        ref float regen
    )
    {
        hook(
            (
                ref float regen_captured
            ) => base.NaturalLifeRegen(
                ref regen_captured
            ),
            this,
            ref regen
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateAutopause_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateAutopause.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateAutopause_Impl(ModPlayerHooks.UpdateAutopause.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateAutopause()
    {
        hook(
            () => base.UpdateAutopause(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreUpdate_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreUpdate.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreUpdate_Impl(ModPlayerHooks.PreUpdate.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdate()
    {
        hook(
            () => base.PreUpdate(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ProcessTriggers_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ProcessTriggers.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ProcessTriggers_Impl(ModPlayerHooks.ProcessTriggers.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ProcessTriggers(
        Terraria.GameInput.TriggersSet triggersSet
    )
    {
        hook(
            (
                Terraria.GameInput.TriggersSet triggersSet_captured
            ) => base.ProcessTriggers(
                triggersSet_captured
            ),
            this,
            triggersSet
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ArmorSetBonusActivated_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ArmorSetBonusActivated.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ArmorSetBonusActivated_Impl(ModPlayerHooks.ArmorSetBonusActivated.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ArmorSetBonusActivated()
    {
        hook(
            () => base.ArmorSetBonusActivated(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ArmorSetBonusHeld_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ArmorSetBonusHeld.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ArmorSetBonusHeld_Impl(ModPlayerHooks.ArmorSetBonusHeld.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ArmorSetBonusHeld(
        int holdTime
    )
    {
        hook(
            (
                int holdTime_captured
            ) => base.ArmorSetBonusHeld(
                holdTime_captured
            ),
            this,
            holdTime
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_SetControls_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.SetControls.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_SetControls_Impl(ModPlayerHooks.SetControls.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void SetControls()
    {
        hook(
            () => base.SetControls(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreUpdateBuffs_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreUpdateBuffs.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreUpdateBuffs_Impl(ModPlayerHooks.PreUpdateBuffs.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateBuffs()
    {
        hook(
            () => base.PreUpdateBuffs(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostUpdateBuffs_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostUpdateBuffs.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostUpdateBuffs_Impl(ModPlayerHooks.PostUpdateBuffs.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateBuffs()
    {
        hook(
            () => base.PostUpdateBuffs(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateEquips_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateEquips.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateEquips_Impl(ModPlayerHooks.UpdateEquips.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateEquips()
    {
        hook(
            () => base.UpdateEquips(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostUpdateEquips_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostUpdateEquips.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostUpdateEquips_Impl(ModPlayerHooks.PostUpdateEquips.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateEquips()
    {
        hook(
            () => base.PostUpdateEquips(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateVisibleAccessories_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateVisibleAccessories.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateVisibleAccessories_Impl(ModPlayerHooks.UpdateVisibleAccessories.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateVisibleAccessories()
    {
        hook(
            () => base.UpdateVisibleAccessories(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateVisibleVanityAccessories_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateVisibleVanityAccessories.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateVisibleVanityAccessories_Impl(ModPlayerHooks.UpdateVisibleVanityAccessories.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateVisibleVanityAccessories()
    {
        hook(
            () => base.UpdateVisibleVanityAccessories(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UpdateDyes_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UpdateDyes.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UpdateDyes_Impl(ModPlayerHooks.UpdateDyes.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateDyes()
    {
        hook(
            () => base.UpdateDyes(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostUpdateMiscEffects_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostUpdateMiscEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostUpdateMiscEffects_Impl(ModPlayerHooks.PostUpdateMiscEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateMiscEffects()
    {
        hook(
            () => base.PostUpdateMiscEffects(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostUpdateRunSpeeds_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostUpdateRunSpeeds.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostUpdateRunSpeeds_Impl(ModPlayerHooks.PostUpdateRunSpeeds.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateRunSpeeds()
    {
        hook(
            () => base.PostUpdateRunSpeeds(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreUpdateMovement_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreUpdateMovement.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreUpdateMovement_Impl(ModPlayerHooks.PreUpdateMovement.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateMovement()
    {
        hook(
            () => base.PreUpdateMovement(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostUpdate_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostUpdate.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostUpdate_Impl(ModPlayerHooks.PostUpdate.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdate()
    {
        hook(
            () => base.PostUpdate(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyExtraJumpDurationMultiplier_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyExtraJumpDurationMultiplier.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyExtraJumpDurationMultiplier_Impl(ModPlayerHooks.ModifyExtraJumpDurationMultiplier.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyExtraJumpDurationMultiplier(
        Terraria.ModLoader.ExtraJump jump,
        ref float duration
    )
    {
        hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured,
                ref float duration_captured
            ) => base.ModifyExtraJumpDurationMultiplier(
                jump_captured,
                ref duration_captured
            ),
            this,
            jump,
            ref duration
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanStartExtraJump_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanStartExtraJump.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanStartExtraJump_Impl(ModPlayerHooks.CanStartExtraJump.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanStartExtraJump(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        return hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured
            ) => base.CanStartExtraJump(
                jump_captured
            ),
            this,
            jump
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnExtraJumpStarted_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnExtraJumpStarted.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnExtraJumpStarted_Impl(ModPlayerHooks.OnExtraJumpStarted.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnExtraJumpStarted(
        Terraria.ModLoader.ExtraJump jump,
        ref bool playSound
    )
    {
        hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured,
                ref bool playSound_captured
            ) => base.OnExtraJumpStarted(
                jump_captured,
                ref playSound_captured
            ),
            this,
            jump,
            ref playSound
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnExtraJumpEnded_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnExtraJumpEnded.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnExtraJumpEnded_Impl(ModPlayerHooks.OnExtraJumpEnded.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnExtraJumpEnded(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured
            ) => base.OnExtraJumpEnded(
                jump_captured
            ),
            this,
            jump
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnExtraJumpRefreshed_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnExtraJumpRefreshed.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnExtraJumpRefreshed_Impl(ModPlayerHooks.OnExtraJumpRefreshed.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnExtraJumpRefreshed(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured
            ) => base.OnExtraJumpRefreshed(
                jump_captured
            ),
            this,
            jump
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ExtraJumpVisuals_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ExtraJumpVisuals.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ExtraJumpVisuals_Impl(ModPlayerHooks.ExtraJumpVisuals.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ExtraJumpVisuals(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured
            ) => base.ExtraJumpVisuals(
                jump_captured
            ),
            this,
            jump
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanShowExtraJumpVisuals_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanShowExtraJumpVisuals.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanShowExtraJumpVisuals_Impl(ModPlayerHooks.CanShowExtraJumpVisuals.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanShowExtraJumpVisuals(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        return hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured
            ) => base.CanShowExtraJumpVisuals(
                jump_captured
            ),
            this,
            jump
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnExtraJumpCleared_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnExtraJumpCleared.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnExtraJumpCleared_Impl(ModPlayerHooks.OnExtraJumpCleared.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnExtraJumpCleared(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        hook(
            (
                Terraria.ModLoader.ExtraJump jump_captured
            ) => base.OnExtraJumpCleared(
                jump_captured
            ),
            this,
            jump
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_FrameEffects_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.FrameEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_FrameEffects_Impl(ModPlayerHooks.FrameEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void FrameEffects()
    {
        hook(
            () => base.FrameEffects(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ImmuneTo_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ImmuneTo.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ImmuneTo_Impl(ModPlayerHooks.ImmuneTo.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ImmuneTo(
        Terraria.DataStructures.PlayerDeathReason damageSource,
        int cooldownCounter,
        bool dodgeable
    )
    {
        return hook(
            (
                Terraria.DataStructures.PlayerDeathReason damageSource_captured,
                int cooldownCounter_captured,
                bool dodgeable_captured
            ) => base.ImmuneTo(
                damageSource_captured,
                cooldownCounter_captured,
                dodgeable_captured
            ),
            this,
            damageSource,
            cooldownCounter,
            dodgeable
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_FreeDodge_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.FreeDodge.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_FreeDodge_Impl(ModPlayerHooks.FreeDodge.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool FreeDodge(
        Terraria.Player.HurtInfo info
    )
    {
        return hook(
            (
                Terraria.Player.HurtInfo info_captured
            ) => base.FreeDodge(
                info_captured
            ),
            this,
            info
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ConsumableDodge_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ConsumableDodge.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ConsumableDodge_Impl(ModPlayerHooks.ConsumableDodge.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ConsumableDodge(
        Terraria.Player.HurtInfo info
    )
    {
        return hook(
            (
                Terraria.Player.HurtInfo info_captured
            ) => base.ConsumableDodge(
                info_captured
            ),
            this,
            info
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyHurt_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyHurt.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyHurt_Impl(ModPlayerHooks.ModifyHurt.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHurt(
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        hook(
            (
                ref Terraria.Player.HurtModifiers modifiers_captured
            ) => base.ModifyHurt(
                ref modifiers_captured
            ),
            this,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnHurt_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnHurt.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnHurt_Impl(ModPlayerHooks.OnHurt.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHurt(
        Terraria.Player.HurtInfo info
    )
    {
        hook(
            (
                Terraria.Player.HurtInfo info_captured
            ) => base.OnHurt(
                info_captured
            ),
            this,
            info
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostHurt_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostHurt.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostHurt_Impl(ModPlayerHooks.PostHurt.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostHurt(
        Terraria.Player.HurtInfo info
    )
    {
        hook(
            (
                Terraria.Player.HurtInfo info_captured
            ) => base.PostHurt(
                info_captured
            ),
            this,
            info
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreKill_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreKill.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreKill_Impl(ModPlayerHooks.PreKill.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreKill(
        double damage,
        int hitDirection,
        bool pvp,
        ref bool playSound,
        ref bool genDust,
        ref Terraria.DataStructures.PlayerDeathReason damageSource
    )
    {
        return hook(
            (
                double damage_captured,
                int hitDirection_captured,
                bool pvp_captured,
                ref bool playSound_captured,
                ref bool genDust_captured,
                ref Terraria.DataStructures.PlayerDeathReason damageSource_captured
            ) => base.PreKill(
                damage_captured,
                hitDirection_captured,
                pvp_captured,
                ref playSound_captured,
                ref genDust_captured,
                ref damageSource_captured
            ),
            this,
            damage,
            hitDirection,
            pvp,
            ref playSound,
            ref genDust,
            ref damageSource
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_Kill_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.Kill.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_Kill_Impl(ModPlayerHooks.Kill.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void Kill(
        double damage,
        int hitDirection,
        bool pvp,
        Terraria.DataStructures.PlayerDeathReason damageSource
    )
    {
        hook(
            (
                double damage_captured,
                int hitDirection_captured,
                bool pvp_captured,
                Terraria.DataStructures.PlayerDeathReason damageSource_captured
            ) => base.Kill(
                damage_captured,
                hitDirection_captured,
                pvp_captured,
                damageSource_captured
            ),
            this,
            damage,
            hitDirection,
            pvp,
            damageSource
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreModifyLuck_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreModifyLuck.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreModifyLuck_Impl(ModPlayerHooks.PreModifyLuck.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreModifyLuck(
        ref float luck
    )
    {
        return hook(
            (
                ref float luck_captured
            ) => base.PreModifyLuck(
                ref luck_captured
            ),
            this,
            ref luck
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyLuck_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyLuck.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyLuck_Impl(ModPlayerHooks.ModifyLuck.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyLuck(
        ref float luck
    )
    {
        hook(
            (
                ref float luck_captured
            ) => base.ModifyLuck(
                ref luck_captured
            ),
            this,
            ref luck
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PreItemCheck_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PreItemCheck.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PreItemCheck_Impl(ModPlayerHooks.PreItemCheck.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreItemCheck()
    {
        return hook(
            () => base.PreItemCheck(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostItemCheck_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostItemCheck.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostItemCheck_Impl(ModPlayerHooks.PostItemCheck.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostItemCheck()
    {
        hook(
            () => base.PostItemCheck(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UseTimeMultiplier_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UseTimeMultiplier.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UseTimeMultiplier_Impl(ModPlayerHooks.UseTimeMultiplier.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override float UseTimeMultiplier(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.UseTimeMultiplier(
                item_captured
            ),
            this,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UseAnimationMultiplier_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UseAnimationMultiplier.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UseAnimationMultiplier_Impl(ModPlayerHooks.UseAnimationMultiplier.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override float UseAnimationMultiplier(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.UseAnimationMultiplier(
                item_captured
            ),
            this,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_UseSpeedMultiplier_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.UseSpeedMultiplier.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_UseSpeedMultiplier_Impl(ModPlayerHooks.UseSpeedMultiplier.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override float UseSpeedMultiplier(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.UseSpeedMultiplier(
                item_captured
            ),
            this,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_GetHealLife_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.GetHealLife.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_GetHealLife_Impl(ModPlayerHooks.GetHealLife.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void GetHealLife(
        Terraria.Item item,
        bool quickHeal,
        ref int healValue
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                bool quickHeal_captured,
                ref int healValue_captured
            ) => base.GetHealLife(
                item_captured,
                quickHeal_captured,
                ref healValue_captured
            ),
            this,
            item,
            quickHeal,
            ref healValue
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_GetHealMana_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.GetHealMana.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_GetHealMana_Impl(ModPlayerHooks.GetHealMana.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void GetHealMana(
        Terraria.Item item,
        bool quickHeal,
        ref int healValue
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                bool quickHeal_captured,
                ref int healValue_captured
            ) => base.GetHealMana(
                item_captured,
                quickHeal_captured,
                ref healValue_captured
            ),
            this,
            item,
            quickHeal,
            ref healValue
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyManaCost_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyManaCost.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyManaCost_Impl(ModPlayerHooks.ModifyManaCost.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyManaCost(
        Terraria.Item item,
        ref float reduce,
        ref float mult
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                ref float reduce_captured,
                ref float mult_captured
            ) => base.ModifyManaCost(
                item_captured,
                ref reduce_captured,
                ref mult_captured
            ),
            this,
            item,
            ref reduce,
            ref mult
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnMissingMana_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnMissingMana.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnMissingMana_Impl(ModPlayerHooks.OnMissingMana.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnMissingMana(
        Terraria.Item item,
        int neededMana
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                int neededMana_captured
            ) => base.OnMissingMana(
                item_captured,
                neededMana_captured
            ),
            this,
            item,
            neededMana
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnConsumeMana_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnConsumeMana.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnConsumeMana_Impl(ModPlayerHooks.OnConsumeMana.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnConsumeMana(
        Terraria.Item item,
        int manaConsumed
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                int manaConsumed_captured
            ) => base.OnConsumeMana(
                item_captured,
                manaConsumed_captured
            ),
            this,
            item,
            manaConsumed
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ApplyPotionDelay_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ApplyPotionDelay.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ApplyPotionDelay_Impl(ModPlayerHooks.ApplyPotionDelay.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ApplyPotionDelay(
        Terraria.Item item,
        int potionDelay
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                int potionDelay_captured
            ) => base.ApplyPotionDelay(
                item_captured,
                potionDelay_captured
            ),
            this,
            item,
            potionDelay
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyWeaponDamage_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyWeaponDamage.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyWeaponDamage_Impl(ModPlayerHooks.ModifyWeaponDamage.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyWeaponDamage(
        Terraria.Item item,
        ref Terraria.ModLoader.StatModifier damage
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                ref Terraria.ModLoader.StatModifier damage_captured
            ) => base.ModifyWeaponDamage(
                item_captured,
                ref damage_captured
            ),
            this,
            item,
            ref damage
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyWeaponKnockback_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyWeaponKnockback.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyWeaponKnockback_Impl(ModPlayerHooks.ModifyWeaponKnockback.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyWeaponKnockback(
        Terraria.Item item,
        ref Terraria.ModLoader.StatModifier knockback
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                ref Terraria.ModLoader.StatModifier knockback_captured
            ) => base.ModifyWeaponKnockback(
                item_captured,
                ref knockback_captured
            ),
            this,
            item,
            ref knockback
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyWeaponCrit_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyWeaponCrit.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyWeaponCrit_Impl(ModPlayerHooks.ModifyWeaponCrit.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyWeaponCrit(
        Terraria.Item item,
        ref float crit
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                ref float crit_captured
            ) => base.ModifyWeaponCrit(
                item_captured,
                ref crit_captured
            ),
            this,
            item,
            ref crit
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanConsumeAmmo_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanConsumeAmmo.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanConsumeAmmo_Impl(ModPlayerHooks.CanConsumeAmmo.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo
    )
    {
        return hook(
            (
                Terraria.Item weapon_captured,
                Terraria.Item ammo_captured
            ) => base.CanConsumeAmmo(
                weapon_captured,
                ammo_captured
            ),
            this,
            weapon,
            ammo
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnConsumeAmmo_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnConsumeAmmo.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnConsumeAmmo_Impl(ModPlayerHooks.OnConsumeAmmo.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo
    )
    {
        hook(
            (
                Terraria.Item weapon_captured,
                Terraria.Item ammo_captured
            ) => base.OnConsumeAmmo(
                weapon_captured,
                ammo_captured
            ),
            this,
            weapon,
            ammo
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanShoot_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanShoot.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanShoot_Impl(ModPlayerHooks.CanShoot.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanShoot(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.CanShoot(
                item_captured
            ),
            this,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyShootStats_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyShootStats.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyShootStats_Impl(ModPlayerHooks.ModifyShootStats.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyShootStats(
        Terraria.Item item,
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
                ref Microsoft.Xna.Framework.Vector2 position_captured,
                ref Microsoft.Xna.Framework.Vector2 velocity_captured,
                ref int type_captured,
                ref int damage_captured,
                ref float knockback_captured
            ) => base.ModifyShootStats(
                item_captured,
                ref position_captured,
                ref velocity_captured,
                ref type_captured,
                ref damage_captured,
                ref knockback_captured
            ),
            this,
            item,
            ref position,
            ref velocity,
            ref type,
            ref damage,
            ref knockback
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_Shoot_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.Shoot.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_Shoot_Impl(ModPlayerHooks.Shoot.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool Shoot(
        Terraria.Item item,
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
                Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Vector2 velocity_captured,
                int type_captured,
                int damage_captured,
                float knockback_captured
            ) => base.Shoot(
                item_captured,
                source_captured,
                position_captured,
                velocity_captured,
                type_captured,
                damage_captured,
                knockback_captured
            ),
            this,
            item,
            source,
            position,
            velocity,
            type,
            damage,
            knockback
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_MeleeEffects_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.MeleeEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_MeleeEffects_Impl(ModPlayerHooks.MeleeEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void MeleeEffects(
        Terraria.Item item,
        Microsoft.Xna.Framework.Rectangle hitbox
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Rectangle hitbox_captured
            ) => base.MeleeEffects(
                item_captured,
                hitbox_captured
            ),
            this,
            item,
            hitbox
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_EmitEnchantmentVisualsAt_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.EmitEnchantmentVisualsAt.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_EmitEnchantmentVisualsAt_Impl(ModPlayerHooks.EmitEnchantmentVisualsAt.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void EmitEnchantmentVisualsAt(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Vector2 boxPosition,
        int boxWidth,
        int boxHeight
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Microsoft.Xna.Framework.Vector2 boxPosition_captured,
                int boxWidth_captured,
                int boxHeight_captured
            ) => base.EmitEnchantmentVisualsAt(
                projectile_captured,
                boxPosition_captured,
                boxWidth_captured,
                boxHeight_captured
            ),
            this,
            projectile,
            boxPosition,
            boxWidth,
            boxHeight
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanCatchNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanCatchNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanCatchNPC_Impl(ModPlayerHooks.CanCatchNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanCatchNPC(
        Terraria.NPC target,
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.NPC target_captured,
                Terraria.Item item_captured
            ) => base.CanCatchNPC(
                target_captured,
                item_captured
            ),
            this,
            target,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnCatchNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnCatchNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnCatchNPC_Impl(ModPlayerHooks.OnCatchNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnCatchNPC(
        Terraria.NPC npc,
        Terraria.Item item,
        bool failed
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Item item_captured,
                bool failed_captured
            ) => base.OnCatchNPC(
                npc_captured,
                item_captured,
                failed_captured
            ),
            this,
            npc,
            item,
            failed
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyItemScale_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyItemScale.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyItemScale_Impl(ModPlayerHooks.ModifyItemScale.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyItemScale(
        Terraria.Item item,
        ref float scale
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                ref float scale_captured
            ) => base.ModifyItemScale(
                item_captured,
                ref scale_captured
            ),
            this,
            item,
            ref scale
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnHitAnything_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnHitAnything.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnHitAnything_Impl(ModPlayerHooks.OnHitAnything.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitAnything(
        float x,
        float y,
        Terraria.Entity victim
    )
    {
        hook(
            (
                float x_captured,
                float y_captured,
                Terraria.Entity victim_captured
            ) => base.OnHitAnything(
                x_captured,
                y_captured,
                victim_captured
            ),
            this,
            x,
            y,
            victim
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanHitNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanHitNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanHitNPC_Impl(ModPlayerHooks.CanHitNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanHitNPC(
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.NPC target_captured
            ) => base.CanHitNPC(
                target_captured
            ),
            this,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanMeleeAttackCollideWithNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanMeleeAttackCollideWithNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanMeleeAttackCollideWithNPC_Impl(ModPlayerHooks.CanMeleeAttackCollideWithNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanMeleeAttackCollideWithNPC(
        Terraria.Item item,
        Microsoft.Xna.Framework.Rectangle meleeAttackHitbox,
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Rectangle meleeAttackHitbox_captured,
                Terraria.NPC target_captured
            ) => base.CanMeleeAttackCollideWithNPC(
                item_captured,
                meleeAttackHitbox_captured,
                target_captured
            ),
            this,
            item,
            meleeAttackHitbox,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyHitNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyHitNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyHitNPC_Impl(ModPlayerHooks.ModifyHitNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHitNPC(
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.NPC target_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitNPC(
                target_captured,
                ref modifiers_captured
            ),
            this,
            target,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnHitNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnHitNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnHitNPC_Impl(ModPlayerHooks.OnHitNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitNPC(
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        hook(
            (
                Terraria.NPC target_captured,
                Terraria.NPC.HitInfo hit_captured,
                int damageDone_captured
            ) => base.OnHitNPC(
                target_captured,
                hit_captured,
                damageDone_captured
            ),
            this,
            target,
            hit,
            damageDone
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanHitNPCWithItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanHitNPCWithItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanHitNPCWithItem_Impl(ModPlayerHooks.CanHitNPCWithItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanHitNPCWithItem(
        Terraria.Item item,
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.NPC target_captured
            ) => base.CanHitNPCWithItem(
                item_captured,
                target_captured
            ),
            this,
            item,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyHitNPCWithItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyHitNPCWithItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyHitNPCWithItem_Impl(ModPlayerHooks.ModifyHitNPCWithItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHitNPCWithItem(
        Terraria.Item item,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.NPC target_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitNPCWithItem(
                item_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            item,
            target,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnHitNPCWithItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnHitNPCWithItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnHitNPCWithItem_Impl(ModPlayerHooks.OnHitNPCWithItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitNPCWithItem(
        Terraria.Item item,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        hook(
            (
                Terraria.Item item_captured,
                Terraria.NPC target_captured,
                Terraria.NPC.HitInfo hit_captured,
                int damageDone_captured
            ) => base.OnHitNPCWithItem(
                item_captured,
                target_captured,
                hit_captured,
                damageDone_captured
            ),
            this,
            item,
            target,
            hit,
            damageDone
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanHitNPCWithProj_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanHitNPCWithProj.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanHitNPCWithProj_Impl(ModPlayerHooks.CanHitNPCWithProj.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanHitNPCWithProj(
        Terraria.Projectile proj,
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.Projectile proj_captured,
                Terraria.NPC target_captured
            ) => base.CanHitNPCWithProj(
                proj_captured,
                target_captured
            ),
            this,
            proj,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyHitNPCWithProj_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyHitNPCWithProj.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyHitNPCWithProj_Impl(ModPlayerHooks.ModifyHitNPCWithProj.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHitNPCWithProj(
        Terraria.Projectile proj,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.Projectile proj_captured,
                Terraria.NPC target_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitNPCWithProj(
                proj_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            proj,
            target,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnHitNPCWithProj_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnHitNPCWithProj.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnHitNPCWithProj_Impl(ModPlayerHooks.OnHitNPCWithProj.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitNPCWithProj(
        Terraria.Projectile proj,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        hook(
            (
                Terraria.Projectile proj_captured,
                Terraria.NPC target_captured,
                Terraria.NPC.HitInfo hit_captured,
                int damageDone_captured
            ) => base.OnHitNPCWithProj(
                proj_captured,
                target_captured,
                hit_captured,
                damageDone_captured
            ),
            this,
            proj,
            target,
            hit,
            damageDone
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanHitPvp_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanHitPvp.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanHitPvp_Impl(ModPlayerHooks.CanHitPvp.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanHitPvp(
        Terraria.Item item,
        Terraria.Player target
    )
    {
        return hook(
            (
                Terraria.Item item_captured,
                Terraria.Player target_captured
            ) => base.CanHitPvp(
                item_captured,
                target_captured
            ),
            this,
            item,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanHitPvpWithProj_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanHitPvpWithProj.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanHitPvpWithProj_Impl(ModPlayerHooks.CanHitPvpWithProj.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanHitPvpWithProj(
        Terraria.Projectile proj,
        Terraria.Player target
    )
    {
        return hook(
            (
                Terraria.Projectile proj_captured,
                Terraria.Player target_captured
            ) => base.CanHitPvpWithProj(
                proj_captured,
                target_captured
            ),
            this,
            proj,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanBeHitByNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanBeHitByNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanBeHitByNPC_Impl(ModPlayerHooks.CanBeHitByNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanBeHitByNPC(
        Terraria.NPC npc,
        ref int cooldownSlot
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                ref int cooldownSlot_captured
            ) => base.CanBeHitByNPC(
                npc_captured,
                ref cooldownSlot_captured
            ),
            this,
            npc,
            ref cooldownSlot
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyHitByNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyHitByNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyHitByNPC_Impl(ModPlayerHooks.ModifyHitByNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHitByNPC(
        Terraria.NPC npc,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Terraria.Player.HurtModifiers modifiers_captured
            ) => base.ModifyHitByNPC(
                npc_captured,
                ref modifiers_captured
            ),
            this,
            npc,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnHitByNPC_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnHitByNPC.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnHitByNPC_Impl(ModPlayerHooks.OnHitByNPC.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitByNPC(
        Terraria.NPC npc,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player.HurtInfo hurtInfo_captured
            ) => base.OnHitByNPC(
                npc_captured,
                hurtInfo_captured
            ),
            this,
            npc,
            hurtInfo
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanBeHitByProjectile_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanBeHitByProjectile.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanBeHitByProjectile_Impl(ModPlayerHooks.CanBeHitByProjectile.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanBeHitByProjectile(
        Terraria.Projectile proj
    )
    {
        return hook(
            (
                Terraria.Projectile proj_captured
            ) => base.CanBeHitByProjectile(
                proj_captured
            ),
            this,
            proj
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyHitByProjectile_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyHitByProjectile.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyHitByProjectile_Impl(ModPlayerHooks.ModifyHitByProjectile.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHitByProjectile(
        Terraria.Projectile proj,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        hook(
            (
                Terraria.Projectile proj_captured,
                ref Terraria.Player.HurtModifiers modifiers_captured
            ) => base.ModifyHitByProjectile(
                proj_captured,
                ref modifiers_captured
            ),
            this,
            proj,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnHitByProjectile_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnHitByProjectile.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnHitByProjectile_Impl(ModPlayerHooks.OnHitByProjectile.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitByProjectile(
        Terraria.Projectile proj,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        hook(
            (
                Terraria.Projectile proj_captured,
                Terraria.Player.HurtInfo hurtInfo_captured
            ) => base.OnHitByProjectile(
                proj_captured,
                hurtInfo_captured
            ),
            this,
            proj,
            hurtInfo
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyFishingAttempt_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyFishingAttempt.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyFishingAttempt_Impl(ModPlayerHooks.ModifyFishingAttempt.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyFishingAttempt(
        ref Terraria.DataStructures.FishingAttempt attempt
    )
    {
        hook(
            (
                ref Terraria.DataStructures.FishingAttempt attempt_captured
            ) => base.ModifyFishingAttempt(
                ref attempt_captured
            ),
            this,
            ref attempt
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CatchFish_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CatchFish.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CatchFish_Impl(ModPlayerHooks.CatchFish.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void CatchFish(
        Terraria.DataStructures.FishingAttempt attempt,
        ref int itemDrop,
        ref int npcSpawn,
        ref Terraria.AdvancedPopupRequest sonar,
        ref Microsoft.Xna.Framework.Vector2 sonarPosition
    )
    {
        hook(
            (
                Terraria.DataStructures.FishingAttempt attempt_captured,
                ref int itemDrop_captured,
                ref int npcSpawn_captured,
                ref Terraria.AdvancedPopupRequest sonar_captured,
                ref Microsoft.Xna.Framework.Vector2 sonarPosition_captured
            ) => base.CatchFish(
                attempt_captured,
                ref itemDrop_captured,
                ref npcSpawn_captured,
                ref sonar_captured,
                ref sonarPosition_captured
            ),
            this,
            attempt,
            ref itemDrop,
            ref npcSpawn,
            ref sonar,
            ref sonarPosition
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyCaughtFish_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyCaughtFish.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyCaughtFish_Impl(ModPlayerHooks.ModifyCaughtFish.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyCaughtFish(
        Terraria.Item fish
    )
    {
        hook(
            (
                Terraria.Item fish_captured
            ) => base.ModifyCaughtFish(
                fish_captured
            ),
            this,
            fish
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanConsumeBait_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanConsumeBait.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanConsumeBait_Impl(ModPlayerHooks.CanConsumeBait.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanConsumeBait(
        Terraria.Item bait
    )
    {
        return hook(
            (
                Terraria.Item bait_captured
            ) => base.CanConsumeBait(
                bait_captured
            ),
            this,
            bait
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_GetFishingLevel_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.GetFishingLevel.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_GetFishingLevel_Impl(ModPlayerHooks.GetFishingLevel.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void GetFishingLevel(
        Terraria.Item fishingRod,
        Terraria.Item bait,
        ref float fishingLevel
    )
    {
        hook(
            (
                Terraria.Item fishingRod_captured,
                Terraria.Item bait_captured,
                ref float fishingLevel_captured
            ) => base.GetFishingLevel(
                fishingRod_captured,
                bait_captured,
                ref fishingLevel_captured
            ),
            this,
            fishingRod,
            bait,
            ref fishingLevel
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_AnglerQuestReward_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.AnglerQuestReward.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_AnglerQuestReward_Impl(ModPlayerHooks.AnglerQuestReward.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void AnglerQuestReward(
        float rareMultiplier,
        System.Collections.Generic.List<Terraria.Item> rewardItems
    )
    {
        hook(
            (
                float rareMultiplier_captured,
                System.Collections.Generic.List<Terraria.Item> rewardItems_captured
            ) => base.AnglerQuestReward(
                rareMultiplier_captured,
                rewardItems_captured
            ),
            this,
            rareMultiplier,
            rewardItems
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_GetDyeTraderReward_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.GetDyeTraderReward.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_GetDyeTraderReward_Impl(ModPlayerHooks.GetDyeTraderReward.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void GetDyeTraderReward(
        System.Collections.Generic.List<int> rewardPool
    )
    {
        hook(
            (
                System.Collections.Generic.List<int> rewardPool_captured
            ) => base.GetDyeTraderReward(
                rewardPool_captured
            ),
            this,
            rewardPool
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_DrawEffects_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.DrawEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_DrawEffects_Impl(ModPlayerHooks.DrawEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void DrawEffects(
        Terraria.DataStructures.PlayerDrawSet drawInfo,
        ref float r,
        ref float g,
        ref float b,
        ref float a,
        ref bool fullBright
    )
    {
        hook(
            (
                Terraria.DataStructures.PlayerDrawSet drawInfo_captured,
                ref float r_captured,
                ref float g_captured,
                ref float b_captured,
                ref float a_captured,
                ref bool fullBright_captured
            ) => base.DrawEffects(
                drawInfo_captured,
                ref r_captured,
                ref g_captured,
                ref b_captured,
                ref a_captured,
                ref fullBright_captured
            ),
            this,
            drawInfo,
            ref r,
            ref g,
            ref b,
            ref a,
            ref fullBright
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyDrawInfo_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyDrawInfo.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyDrawInfo_Impl(ModPlayerHooks.ModifyDrawInfo.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyDrawInfo(
        ref Terraria.DataStructures.PlayerDrawSet drawInfo
    )
    {
        hook(
            (
                ref Terraria.DataStructures.PlayerDrawSet drawInfo_captured
            ) => base.ModifyDrawInfo(
                ref drawInfo_captured
            ),
            this,
            ref drawInfo
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyDrawLayerOrdering_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyDrawLayerOrdering.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyDrawLayerOrdering_Impl(ModPlayerHooks.ModifyDrawLayerOrdering.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyDrawLayerOrdering(
        System.Collections.Generic.IDictionary<Terraria.ModLoader.PlayerDrawLayer, Terraria.ModLoader.PlayerDrawLayer.Position> positions
    )
    {
        hook(
            (
                System.Collections.Generic.IDictionary<Terraria.ModLoader.PlayerDrawLayer, Terraria.ModLoader.PlayerDrawLayer.Position> positions_captured
            ) => base.ModifyDrawLayerOrdering(
                positions_captured
            ),
            this,
            positions
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_HideDrawLayers_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.HideDrawLayers.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_HideDrawLayers_Impl(ModPlayerHooks.HideDrawLayers.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void HideDrawLayers(
        Terraria.DataStructures.PlayerDrawSet drawInfo
    )
    {
        hook(
            (
                Terraria.DataStructures.PlayerDrawSet drawInfo_captured
            ) => base.HideDrawLayers(
                drawInfo_captured
            ),
            this,
            drawInfo
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyScreenPosition_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyScreenPosition.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyScreenPosition_Impl(ModPlayerHooks.ModifyScreenPosition.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyScreenPosition()
    {
        hook(
            () => base.ModifyScreenPosition(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyZoom_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyZoom.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyZoom_Impl(ModPlayerHooks.ModifyZoom.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyZoom(
        ref float zoom
    )
    {
        hook(
            (
                ref float zoom_captured
            ) => base.ModifyZoom(
                ref zoom_captured
            ),
            this,
            ref zoom
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PlayerConnect_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PlayerConnect.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PlayerConnect_Impl(ModPlayerHooks.PlayerConnect.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PlayerConnect()
    {
        hook(
            () => base.PlayerConnect(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PlayerDisconnect_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PlayerDisconnect.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PlayerDisconnect_Impl(ModPlayerHooks.PlayerDisconnect.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PlayerDisconnect()
    {
        hook(
            () => base.PlayerDisconnect(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnEnterWorld_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnEnterWorld.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnEnterWorld_Impl(ModPlayerHooks.OnEnterWorld.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnEnterWorld()
    {
        hook(
            () => base.OnEnterWorld(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnRespawn_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnRespawn.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnRespawn_Impl(ModPlayerHooks.OnRespawn.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnRespawn()
    {
        hook(
            () => base.OnRespawn(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ShiftClickSlot_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ShiftClickSlot.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ShiftClickSlot_Impl(ModPlayerHooks.ShiftClickSlot.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ShiftClickSlot(
        Terraria.Item[] inventory,
        int context,
        int slot
    )
    {
        return hook(
            (
                Terraria.Item[] inventory_captured,
                int context_captured,
                int slot_captured
            ) => base.ShiftClickSlot(
                inventory_captured,
                context_captured,
                slot_captured
            ),
            this,
            inventory,
            context,
            slot
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_HoverSlot_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.HoverSlot.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_HoverSlot_Impl(ModPlayerHooks.HoverSlot.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool HoverSlot(
        Terraria.Item[] inventory,
        int context,
        int slot
    )
    {
        return hook(
            (
                Terraria.Item[] inventory_captured,
                int context_captured,
                int slot_captured
            ) => base.HoverSlot(
                inventory_captured,
                context_captured,
                slot_captured
            ),
            this,
            inventory,
            context,
            slot
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostSellItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostSellItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostSellItem_Impl(ModPlayerHooks.PostSellItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostSellItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        hook(
            (
                Terraria.NPC vendor_captured,
                Terraria.Item[] shopInventory_captured,
                Terraria.Item item_captured
            ) => base.PostSellItem(
                vendor_captured,
                shopInventory_captured,
                item_captured
            ),
            this,
            vendor,
            shopInventory,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanSellItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanSellItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanSellItem_Impl(ModPlayerHooks.CanSellItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanSellItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.NPC vendor_captured,
                Terraria.Item[] shopInventory_captured,
                Terraria.Item item_captured
            ) => base.CanSellItem(
                vendor_captured,
                shopInventory_captured,
                item_captured
            ),
            this,
            vendor,
            shopInventory,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostBuyItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostBuyItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostBuyItem_Impl(ModPlayerHooks.PostBuyItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostBuyItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        hook(
            (
                Terraria.NPC vendor_captured,
                Terraria.Item[] shopInventory_captured,
                Terraria.Item item_captured
            ) => base.PostBuyItem(
                vendor_captured,
                shopInventory_captured,
                item_captured
            ),
            this,
            vendor,
            shopInventory,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanBuyItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanBuyItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanBuyItem_Impl(ModPlayerHooks.CanBuyItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanBuyItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.NPC vendor_captured,
                Terraria.Item[] shopInventory_captured,
                Terraria.Item item_captured
            ) => base.CanBuyItem(
                vendor_captured,
                shopInventory_captured,
                item_captured
            ),
            this,
            vendor,
            shopInventory,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanUseItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanUseItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanUseItem_Impl(ModPlayerHooks.CanUseItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanUseItem(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.CanUseItem(
                item_captured
            ),
            this,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanAutoReuseItem_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanAutoReuseItem.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanAutoReuseItem_Impl(ModPlayerHooks.CanAutoReuseItem.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanAutoReuseItem(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.CanAutoReuseItem(
                item_captured
            ),
            this,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyNurseHeal_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyNurseHeal.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyNurseHeal_Impl(ModPlayerHooks.ModifyNurseHeal.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ModifyNurseHeal(
        Terraria.NPC nurse,
        ref int health,
        ref bool removeDebuffs,
        ref string chatText
    )
    {
        return hook(
            (
                Terraria.NPC nurse_captured,
                ref int health_captured,
                ref bool removeDebuffs_captured,
                ref string chatText_captured
            ) => base.ModifyNurseHeal(
                nurse_captured,
                ref health_captured,
                ref removeDebuffs_captured,
                ref chatText_captured
            ),
            this,
            nurse,
            ref health,
            ref removeDebuffs,
            ref chatText
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyNursePrice_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyNursePrice.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyNursePrice_Impl(ModPlayerHooks.ModifyNursePrice.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyNursePrice(
        Terraria.NPC nurse,
        int health,
        bool removeDebuffs,
        ref int price
    )
    {
        hook(
            (
                Terraria.NPC nurse_captured,
                int health_captured,
                bool removeDebuffs_captured,
                ref int price_captured
            ) => base.ModifyNursePrice(
                nurse_captured,
                health_captured,
                removeDebuffs_captured,
                ref price_captured
            ),
            this,
            nurse,
            health,
            removeDebuffs,
            ref price
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_PostNurseHeal_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.PostNurseHeal.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_PostNurseHeal_Impl(ModPlayerHooks.PostNurseHeal.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostNurseHeal(
        Terraria.NPC nurse,
        int health,
        bool removeDebuffs,
        int price
    )
    {
        hook(
            (
                Terraria.NPC nurse_captured,
                int health_captured,
                bool removeDebuffs_captured,
                int price_captured
            ) => base.PostNurseHeal(
                nurse_captured,
                health_captured,
                removeDebuffs_captured,
                price_captured
            ),
            this,
            nurse,
            health,
            removeDebuffs,
            price
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_AddStartingItems_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.AddStartingItems.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_AddStartingItems_Impl(ModPlayerHooks.AddStartingItems.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override System.Collections.Generic.IEnumerable<Terraria.Item> AddStartingItems(
        bool mediumCoreDeath
    )
    {
        return hook(
            (
                bool mediumCoreDeath_captured
            ) => base.AddStartingItems(
                mediumCoreDeath_captured
            ),
            this,
            mediumCoreDeath
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_ModifyStartingInventory_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.ModifyStartingInventory.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_ModifyStartingInventory_Impl(ModPlayerHooks.ModifyStartingInventory.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyStartingInventory(
        System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<Terraria.Item>> itemsByMod,
        bool mediumCoreDeath
    )
    {
        hook(
            (
                System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<Terraria.Item>> itemsByMod_captured,
                bool mediumCoreDeath_captured
            ) => base.ModifyStartingInventory(
                itemsByMod_captured,
                mediumCoreDeath_captured
            ),
            this,
            itemsByMod,
            mediumCoreDeath
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_AddMaterialsForCrafting_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.AddMaterialsForCrafting.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_AddMaterialsForCrafting_Impl(ModPlayerHooks.AddMaterialsForCrafting.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override System.Collections.Generic.IEnumerable<Terraria.Item> AddMaterialsForCrafting(
        out Terraria.ModLoader.ModPlayer.ItemConsumedCallback itemConsumedCallback
    )
    {
        return hook(
            (
                out Terraria.ModLoader.ModPlayer.ItemConsumedCallback itemConsumedCallback_captured
            ) => base.AddMaterialsForCrafting(
                out itemConsumedCallback_captured
            ),
            this,
            out itemConsumedCallback
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnPickup_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnPickup.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnPickup_Impl(ModPlayerHooks.OnPickup.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool OnPickup(
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.Item item_captured
            ) => base.OnPickup(
                item_captured
            ),
            this,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_CanBeTeleportedTo_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.CanBeTeleportedTo.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_CanBeTeleportedTo_Impl(ModPlayerHooks.CanBeTeleportedTo.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanBeTeleportedTo(
        Microsoft.Xna.Framework.Vector2 teleportPosition,
        string context
    )
    {
        return hook(
            (
                Microsoft.Xna.Framework.Vector2 teleportPosition_captured,
                string context_captured
            ) => base.CanBeTeleportedTo(
                teleportPosition_captured,
                context_captured
            ),
            this,
            teleportPosition,
            context
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModPlayer_OnEquipmentLoadoutSwitched_Impl() : Terraria.ModLoader.ModPlayer
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModPlayerHooks.OnEquipmentLoadoutSwitched.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    protected override bool CloneNewInstances => true;

    public ModPlayer_OnEquipmentLoadoutSwitched_Impl(ModPlayerHooks.OnEquipmentLoadoutSwitched.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnEquipmentLoadoutSwitched(
        int oldLoadoutIndex,
        int loadoutIndex
    )
    {
        hook(
            (
                int oldLoadoutIndex_captured,
                int loadoutIndex_captured
            ) => base.OnEquipmentLoadoutSwitched(
                oldLoadoutIndex_captured,
                loadoutIndex_captured
            ),
            this,
            oldLoadoutIndex,
            loadoutIndex
        );
    }
}
namespace Daybreak.Common.Features.Hooks;

using System.Linq;

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
//     System.Void Terraria.ModLoader.ModPlayer::SaveData(Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.ModPlayer::LoadData(Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.ModPlayer::PreSavePlayer()
//     System.Void Terraria.ModLoader.ModPlayer::PostSavePlayer()
//     System.Void Terraria.ModLoader.ModPlayer::CopyClientState(Terraria.ModLoader.ModPlayer)
//     System.Void Terraria.ModLoader.ModPlayer::SyncPlayer(System.Int32,System.Int32,System.Boolean)
//     System.Void Terraria.ModLoader.ModPlayer::SendClientChanges(Terraria.ModLoader.ModPlayer)
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
//     System.Void Terraria.ModLoader.ModPlayer::GetHealLife(Terraria.Item,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.ModPlayer::GetHealMana(Terraria.Item,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyManaCost(Terraria.Item,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::OnMissingMana(Terraria.Item,System.Int32)
//     System.Void Terraria.ModLoader.ModPlayer::OnConsumeMana(Terraria.Item,System.Int32)
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
//     System.Void Terraria.ModLoader.ModPlayer::OnCatchNPC(Terraria.NPC,Terraria.Item,System.Boolean)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyItemScale(Terraria.Item,System.Single&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitAnything(System.Single,System.Single,Terraria.Entity)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanHitNPC(Terraria.NPC)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHitNPC(Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitNPC(Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyHitNPCWithItem(Terraria.Item,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.ModPlayer::OnHitNPCWithItem(Terraria.Item,Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
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
//     System.Boolean Terraria.ModLoader.ModPlayer::ModifyNurseHeal(Terraria.NPC,System.Int32&,System.Boolean&,System.String&)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyNursePrice(Terraria.NPC,System.Int32,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.ModPlayer::PostNurseHeal(Terraria.NPC,System.Int32,System.Boolean,System.Int32)
//     System.Void Terraria.ModLoader.ModPlayer::ModifyStartingInventory(System.Collections.Generic.IReadOnlyDictionary`2<System.String,System.Collections.Generic.List`1<Terraria.Item>>,System.Boolean)
//     System.Boolean Terraria.ModLoader.ModPlayer::OnPickup(Terraria.Item)
//     System.Boolean Terraria.ModLoader.ModPlayer::CanBeTeleportedTo(Microsoft.Xna.Framework.Vector2,System.String)
//     System.Void Terraria.ModLoader.ModPlayer::OnEquipmentLoadoutSwitched(System.Int32,System.Int32)
public static partial class ModPlayerHooks
{
    public sealed partial class Initialize
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ResetEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ResetInfoAccessories
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class RefreshInfoAccessoriesFromTeamPlayers
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player otherPlayer
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player otherPlayer
        )
        {
            Event?.Invoke(self, otherPlayer);
        }
    }

    public sealed partial class ModifyMaxStats
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            out Terraria.ModLoader.StatModifier health,
            out Terraria.ModLoader.StatModifier mana
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            out Terraria.ModLoader.StatModifier health,
            out Terraria.ModLoader.StatModifier mana
        )
        {
            health = default;
            mana = default;

            Event?.Invoke(self, out health, out mana);
        }
    }

    public sealed partial class UpdateDead
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreSaveCustomData
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class SaveData
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, tag);
        }
    }

    public sealed partial class LoadData
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, tag);
        }
    }

    public sealed partial class PreSavePlayer
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostSavePlayer
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class CopyClientState
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ModPlayer targetCopy
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ModPlayer targetCopy
        )
        {
            Event?.Invoke(self, targetCopy);
        }
    }

    public sealed partial class SyncPlayer
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            int toWho,
            int fromWho,
            bool newPlayer
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            int toWho,
            int fromWho,
            bool newPlayer
        )
        {
            Event?.Invoke(self, toWho, fromWho, newPlayer);
        }
    }

    public sealed partial class SendClientChanges
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ModPlayer clientPlayer
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ModPlayer clientPlayer
        )
        {
            Event?.Invoke(self, clientPlayer);
        }
    }

    public sealed partial class UpdateBadLifeRegen
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class UpdateLifeRegen
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class NaturalLifeRegen
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            ref float regen
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            ref float regen
        )
        {
            Event?.Invoke(self, ref regen);
        }
    }

    public sealed partial class UpdateAutopause
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdate
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ProcessTriggers
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.GameInput.TriggersSet triggersSet
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.GameInput.TriggersSet triggersSet
        )
        {
            Event?.Invoke(self, triggersSet);
        }
    }

    public sealed partial class ArmorSetBonusActivated
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ArmorSetBonusHeld
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            int holdTime
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            int holdTime
        )
        {
            Event?.Invoke(self, holdTime);
        }
    }

    public sealed partial class SetControls
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateBuffs
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateBuffs
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class UpdateEquips
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateEquips
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class UpdateVisibleAccessories
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class UpdateVisibleVanityAccessories
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class UpdateDyes
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateMiscEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateRunSpeeds
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateMovement
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdate
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyExtraJumpDurationMultiplier
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump,
            ref float duration
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump,
            ref float duration
        )
        {
            Event?.Invoke(self, jump, ref duration);
        }
    }

    public sealed partial class CanStartExtraJump
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, jump))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnExtraJumpStarted
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump,
            ref bool playSound
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump,
            ref bool playSound
        )
        {
            Event?.Invoke(self, jump, ref playSound);
        }
    }

    public sealed partial class OnExtraJumpEnded
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        )
        {
            Event?.Invoke(self, jump);
        }
    }

    public sealed partial class OnExtraJumpRefreshed
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        )
        {
            Event?.Invoke(self, jump);
        }
    }

    public sealed partial class ExtraJumpVisuals
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        )
        {
            Event?.Invoke(self, jump);
        }
    }

    public sealed partial class CanShowExtraJumpVisuals
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, jump))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnExtraJumpCleared
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.ModLoader.ExtraJump jump
        )
        {
            Event?.Invoke(self, jump);
        }
    }

    public sealed partial class FrameEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ImmuneTo
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDeathReason damageSource,
            int cooldownCounter,
            bool dodgeable
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDeathReason damageSource,
            int cooldownCounter,
            bool dodgeable
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, damageSource, cooldownCounter, dodgeable))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class FreeDodge
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, info))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class ConsumableDodge
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, info))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class ModifyHurt
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.Player.HurtModifiers modifiers
        )
        {
            Event?.Invoke(self, ref modifiers);
        }
    }

    public sealed partial class OnHurt
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        )
        {
            Event?.Invoke(self, info);
        }
    }

    public sealed partial class PostHurt
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Player.HurtInfo info
        )
        {
            Event?.Invoke(self, info);
        }
    }

    public sealed partial class PreKill
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            double damage,
            int hitDirection,
            bool pvp,
            ref bool playSound,
            ref bool genDust,
            ref Terraria.DataStructures.PlayerDeathReason damageSource
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            double damage,
            int hitDirection,
            bool pvp,
            ref bool playSound,
            ref bool genDust,
            ref Terraria.DataStructures.PlayerDeathReason damageSource
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, damage, hitDirection, pvp, ref playSound, ref genDust, ref damageSource);
            }

            return result;
        }
    }

    public sealed partial class Kill
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            double damage,
            int hitDirection,
            bool pvp,
            Terraria.DataStructures.PlayerDeathReason damageSource
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            double damage,
            int hitDirection,
            bool pvp,
            Terraria.DataStructures.PlayerDeathReason damageSource
        )
        {
            Event?.Invoke(self, damage, hitDirection, pvp, damageSource);
        }
    }

    public sealed partial class PreModifyLuck
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            ref float luck
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            ref float luck
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, ref luck);
            }

            return result;
        }
    }

    public sealed partial class ModifyLuck
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            ref float luck
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            ref float luck
        )
        {
            Event?.Invoke(self, ref luck);
        }
    }

    public sealed partial class PreItemCheck
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self);
            }

            return result;
        }
    }

    public sealed partial class PostItemCheck
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class GetHealLife
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        )
        {
            Event?.Invoke(self, item, quickHeal, ref healValue);
        }
    }

    public sealed partial class GetHealMana
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            bool quickHeal,
            ref int healValue
        )
        {
            Event?.Invoke(self, item, quickHeal, ref healValue);
        }
    }

    public sealed partial class ModifyManaCost
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float reduce,
            ref float mult
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float reduce,
            ref float mult
        )
        {
            Event?.Invoke(self, item, ref reduce, ref mult);
        }
    }

    public sealed partial class OnMissingMana
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            int neededMana
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            int neededMana
        )
        {
            Event?.Invoke(self, item, neededMana);
        }
    }

    public sealed partial class OnConsumeMana
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            int manaConsumed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            int manaConsumed
        )
        {
            Event?.Invoke(self, item, manaConsumed);
        }
    }

    public sealed partial class ModifyWeaponDamage
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier damage
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier damage
        )
        {
            Event?.Invoke(self, item, ref damage);
        }
    }

    public sealed partial class ModifyWeaponKnockback
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier knockback
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Terraria.ModLoader.StatModifier knockback
        )
        {
            Event?.Invoke(self, item, ref knockback);
        }
    }

    public sealed partial class ModifyWeaponCrit
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float crit
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float crit
        )
        {
            Event?.Invoke(self, item, ref crit);
        }
    }

    public sealed partial class CanConsumeAmmo
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item weapon,
            Terraria.Item ammo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item weapon,
            Terraria.Item ammo
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, weapon, ammo))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnConsumeAmmo
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item weapon,
            Terraria.Item ammo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item weapon,
            Terraria.Item ammo
        )
        {
            Event?.Invoke(self, weapon, ammo);
        }
    }

    public sealed partial class CanShoot
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item);
            }

            return result;
        }
    }

    public sealed partial class ModifyShootStats
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        )
        {
            Event?.Invoke(self, item, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }

    public sealed partial class Shoot
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 velocity,
            int type,
            int damage,
            float knockback
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Vector2 velocity,
            int type,
            int damage,
            float knockback
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, source, position, velocity, type, damage, knockback);
            }

            return result;
        }
    }

    public sealed partial class MeleeEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle hitbox
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle hitbox
        )
        {
            Event?.Invoke(self, item, hitbox);
        }
    }

    public sealed partial class EmitEnchantmentVisualsAt
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 boxPosition,
            int boxWidth,
            int boxHeight
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 boxPosition,
            int boxWidth,
            int boxHeight
        )
        {
            Event?.Invoke(self, projectile, boxPosition, boxWidth, boxHeight);
        }
    }

    public sealed partial class OnCatchNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            Terraria.Item item,
            bool failed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            Terraria.Item item,
            bool failed
        )
        {
            Event?.Invoke(self, npc, item, failed);
        }
    }

    public sealed partial class ModifyItemScale
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float scale
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            ref float scale
        )
        {
            Event?.Invoke(self, item, ref scale);
        }
    }

    public sealed partial class OnHitAnything
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            float x,
            float y,
            Terraria.Entity victim
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            float x,
            float y,
            Terraria.Entity victim
        )
        {
            Event?.Invoke(self, x, y, victim);
        }
    }

    public sealed partial class CanHitNPC
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, target))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyHitNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, target, ref modifiers);
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        )
        {
            Event?.Invoke(self, target, hit, damageDone);
        }
    }

    public sealed partial class ModifyHitNPCWithItem
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, item, target, ref modifiers);
        }
    }

    public sealed partial class OnHitNPCWithItem
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        )
        {
            Event?.Invoke(self, item, target, hit, damageDone);
        }
    }

    public sealed partial class ModifyHitNPCWithProj
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, proj, target, ref modifiers);
        }
    }

    public sealed partial class OnHitNPCWithProj
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        )
        {
            Event?.Invoke(self, proj, target, hit, damageDone);
        }
    }

    public sealed partial class CanHitPvp
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.Player target
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item,
            Terraria.Player target
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, target))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class CanHitPvpWithProj
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.Player target
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.Player target
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, proj, target))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class CanBeHitByNPC
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            ref int cooldownSlot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            ref int cooldownSlot
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, npc, ref cooldownSlot))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyHitByNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            ref Terraria.Player.HurtModifiers modifiers
        )
        {
            Event?.Invoke(self, npc, ref modifiers);
        }
    }

    public sealed partial class OnHitByNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC npc,
            Terraria.Player.HurtInfo hurtInfo
        )
        {
            Event?.Invoke(self, npc, hurtInfo);
        }
    }

    public sealed partial class CanBeHitByProjectile
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, proj))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyHitByProjectile
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            ref Terraria.Player.HurtModifiers modifiers
        )
        {
            Event?.Invoke(self, proj, ref modifiers);
        }
    }

    public sealed partial class OnHitByProjectile
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Projectile proj,
            Terraria.Player.HurtInfo hurtInfo
        )
        {
            Event?.Invoke(self, proj, hurtInfo);
        }
    }

    public sealed partial class ModifyFishingAttempt
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.DataStructures.FishingAttempt attempt
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.DataStructures.FishingAttempt attempt
        )
        {
            Event?.Invoke(self, ref attempt);
        }
    }

    public sealed partial class CatchFish
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.FishingAttempt attempt,
            ref int itemDrop,
            ref int npcSpawn,
            ref Terraria.AdvancedPopupRequest sonar,
            ref Microsoft.Xna.Framework.Vector2 sonarPosition
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.FishingAttempt attempt,
            ref int itemDrop,
            ref int npcSpawn,
            ref Terraria.AdvancedPopupRequest sonar,
            ref Microsoft.Xna.Framework.Vector2 sonarPosition
        )
        {
            Event?.Invoke(self, attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);
        }
    }

    public sealed partial class ModifyCaughtFish
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item fish
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item fish
        )
        {
            Event?.Invoke(self, fish);
        }
    }

    public sealed partial class GetFishingLevel
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item fishingRod,
            Terraria.Item bait,
            ref float fishingLevel
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item fishingRod,
            Terraria.Item bait,
            ref float fishingLevel
        )
        {
            Event?.Invoke(self, fishingRod, bait, ref fishingLevel);
        }
    }

    public sealed partial class AnglerQuestReward
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            float rareMultiplier,
            System.Collections.Generic.List<Terraria.Item> rewardItems
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            float rareMultiplier,
            System.Collections.Generic.List<Terraria.Item> rewardItems
        )
        {
            Event?.Invoke(self, rareMultiplier, rewardItems);
        }
    }

    public sealed partial class GetDyeTraderReward
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.List<int> rewardPool
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.List<int> rewardPool
        )
        {
            Event?.Invoke(self, rewardPool);
        }
    }

    public sealed partial class DrawEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDrawSet drawInfo,
            ref float r,
            ref float g,
            ref float b,
            ref float a,
            ref bool fullBright
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDrawSet drawInfo,
            ref float r,
            ref float g,
            ref float b,
            ref float a,
            ref bool fullBright
        )
        {
            Event?.Invoke(self, drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }
    }

    public sealed partial class ModifyDrawInfo
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.DataStructures.PlayerDrawSet drawInfo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            ref Terraria.DataStructures.PlayerDrawSet drawInfo
        )
        {
            Event?.Invoke(self, ref drawInfo);
        }
    }

    public sealed partial class ModifyDrawLayerOrdering
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.IDictionary<Terraria.ModLoader.PlayerDrawLayer, Terraria.ModLoader.PlayerDrawLayer.Position> positions
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.IDictionary<Terraria.ModLoader.PlayerDrawLayer, Terraria.ModLoader.PlayerDrawLayer.Position> positions
        )
        {
            Event?.Invoke(self, positions);
        }
    }

    public sealed partial class HideDrawLayers
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDrawSet drawInfo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.DataStructures.PlayerDrawSet drawInfo
        )
        {
            Event?.Invoke(self, drawInfo);
        }
    }

    public sealed partial class ModifyScreenPosition
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyZoom
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            ref float zoom
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            ref float zoom
        )
        {
            Event?.Invoke(self, ref zoom);
        }
    }

    public sealed partial class PlayerConnect
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PlayerDisconnect
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class OnEnterWorld
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class OnRespawn
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ShiftClickSlot
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item[] inventory,
            int context,
            int slot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item[] inventory,
            int context,
            int slot
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, inventory, context, slot))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class HoverSlot
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item[] inventory,
            int context,
            int slot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item[] inventory,
            int context,
            int slot
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, inventory, context, slot))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class PostSellItem
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        )
        {
            Event?.Invoke(self, vendor, shopInventory, item);
        }
    }

    public sealed partial class CanSellItem
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, vendor, shopInventory, item))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class PostBuyItem
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        )
        {
            Event?.Invoke(self, vendor, shopInventory, item);
        }
    }

    public sealed partial class CanBuyItem
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC vendor,
            Terraria.Item[] shopInventory,
            Terraria.Item item
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, vendor, shopInventory, item))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class CanUseItem
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyNurseHeal
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            ref int health,
            ref bool removeDebuffs,
            ref string chatText
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            ref int health,
            ref bool removeDebuffs,
            ref string chatText
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, nurse, ref health, ref removeDebuffs, ref chatText))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyNursePrice
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            ref int price
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            ref int price
        )
        {
            Event?.Invoke(self, nurse, health, removeDebuffs, ref price);
        }
    }

    public sealed partial class PostNurseHeal
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            int price
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.NPC nurse,
            int health,
            bool removeDebuffs,
            int price
        )
        {
            Event?.Invoke(self, nurse, health, removeDebuffs, price);
        }
    }

    public sealed partial class ModifyStartingInventory
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<Terraria.Item>> itemsByMod,
            bool mediumCoreDeath
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<Terraria.Item>> itemsByMod,
            bool mediumCoreDeath
        )
        {
            Event?.Invoke(self, itemsByMod, mediumCoreDeath);
        }
    }

    public sealed partial class OnPickup
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Terraria.Item item
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class CanBeTeleportedTo
    {
        public delegate bool Definition(
            Terraria.ModLoader.ModPlayer self,
            Microsoft.Xna.Framework.Vector2 teleportPosition,
            string context
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.ModPlayer self,
            Microsoft.Xna.Framework.Vector2 teleportPosition,
            string context
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, teleportPosition, context))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnEquipmentLoadoutSwitched
    {
        public delegate void Definition(
            Terraria.ModLoader.ModPlayer self,
            int oldLoadoutIndex,
            int loadoutIndex
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModPlayer self,
            int oldLoadoutIndex,
            int loadoutIndex
        )
        {
            Event?.Invoke(self, oldLoadoutIndex, loadoutIndex);
        }
    }
}

public sealed partial class ModPlayerImpl : Terraria.ModLoader.ModPlayer
{
    public override void Initialize()
    {
        if (!ModPlayerHooks.Initialize.GetInvocationList().Any())
        {
            base.Initialize();
            return;
        }

        ModPlayerHooks.Initialize.Invoke(
            this
        );
    }

    public override void ResetEffects()
    {
        if (!ModPlayerHooks.ResetEffects.GetInvocationList().Any())
        {
            base.ResetEffects();
            return;
        }

        ModPlayerHooks.ResetEffects.Invoke(
            this
        );
    }

    public override void ResetInfoAccessories()
    {
        if (!ModPlayerHooks.ResetInfoAccessories.GetInvocationList().Any())
        {
            base.ResetInfoAccessories();
            return;
        }

        ModPlayerHooks.ResetInfoAccessories.Invoke(
            this
        );
    }

    public override void RefreshInfoAccessoriesFromTeamPlayers(
        Terraria.Player otherPlayer
    )
    {
        if (!ModPlayerHooks.RefreshInfoAccessoriesFromTeamPlayers.GetInvocationList().Any())
        {
            base.RefreshInfoAccessoriesFromTeamPlayers(
                otherPlayer
            );
            return;
        }

        ModPlayerHooks.RefreshInfoAccessoriesFromTeamPlayers.Invoke(
            this,
            otherPlayer
        );
    }

    public override void ModifyMaxStats(
        out Terraria.ModLoader.StatModifier health,
        out Terraria.ModLoader.StatModifier mana
    )
    {
        if (!ModPlayerHooks.ModifyMaxStats.GetInvocationList().Any())
        {
            base.ModifyMaxStats(
                out health,
                out mana
            );
            return;
        }

        ModPlayerHooks.ModifyMaxStats.Invoke(
            this,
            out health,
            out mana
        );
    }

    public override void UpdateDead()
    {
        if (!ModPlayerHooks.UpdateDead.GetInvocationList().Any())
        {
            base.UpdateDead();
            return;
        }

        ModPlayerHooks.UpdateDead.Invoke(
            this
        );
    }

    public override void PreSaveCustomData()
    {
        if (!ModPlayerHooks.PreSaveCustomData.GetInvocationList().Any())
        {
            base.PreSaveCustomData();
            return;
        }

        ModPlayerHooks.PreSaveCustomData.Invoke(
            this
        );
    }

    public override void SaveData(
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!ModPlayerHooks.SaveData.GetInvocationList().Any())
        {
            base.SaveData(
                tag
            );
            return;
        }

        ModPlayerHooks.SaveData.Invoke(
            this,
            tag
        );
    }

    public override void LoadData(
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!ModPlayerHooks.LoadData.GetInvocationList().Any())
        {
            base.LoadData(
                tag
            );
            return;
        }

        ModPlayerHooks.LoadData.Invoke(
            this,
            tag
        );
    }

    public override void PreSavePlayer()
    {
        if (!ModPlayerHooks.PreSavePlayer.GetInvocationList().Any())
        {
            base.PreSavePlayer();
            return;
        }

        ModPlayerHooks.PreSavePlayer.Invoke(
            this
        );
    }

    public override void PostSavePlayer()
    {
        if (!ModPlayerHooks.PostSavePlayer.GetInvocationList().Any())
        {
            base.PostSavePlayer();
            return;
        }

        ModPlayerHooks.PostSavePlayer.Invoke(
            this
        );
    }

    public override void CopyClientState(
        Terraria.ModLoader.ModPlayer targetCopy
    )
    {
        if (!ModPlayerHooks.CopyClientState.GetInvocationList().Any())
        {
            base.CopyClientState(
                targetCopy
            );
            return;
        }

        ModPlayerHooks.CopyClientState.Invoke(
            this,
            targetCopy
        );
    }

    public override void SyncPlayer(
        int toWho,
        int fromWho,
        bool newPlayer
    )
    {
        if (!ModPlayerHooks.SyncPlayer.GetInvocationList().Any())
        {
            base.SyncPlayer(
                toWho,
                fromWho,
                newPlayer
            );
            return;
        }

        ModPlayerHooks.SyncPlayer.Invoke(
            this,
            toWho,
            fromWho,
            newPlayer
        );
    }

    public override void SendClientChanges(
        Terraria.ModLoader.ModPlayer clientPlayer
    )
    {
        if (!ModPlayerHooks.SendClientChanges.GetInvocationList().Any())
        {
            base.SendClientChanges(
                clientPlayer
            );
            return;
        }

        ModPlayerHooks.SendClientChanges.Invoke(
            this,
            clientPlayer
        );
    }

    public override void UpdateBadLifeRegen()
    {
        if (!ModPlayerHooks.UpdateBadLifeRegen.GetInvocationList().Any())
        {
            base.UpdateBadLifeRegen();
            return;
        }

        ModPlayerHooks.UpdateBadLifeRegen.Invoke(
            this
        );
    }

    public override void UpdateLifeRegen()
    {
        if (!ModPlayerHooks.UpdateLifeRegen.GetInvocationList().Any())
        {
            base.UpdateLifeRegen();
            return;
        }

        ModPlayerHooks.UpdateLifeRegen.Invoke(
            this
        );
    }

    public override void NaturalLifeRegen(
        ref float regen
    )
    {
        if (!ModPlayerHooks.NaturalLifeRegen.GetInvocationList().Any())
        {
            base.NaturalLifeRegen(
                ref regen
            );
            return;
        }

        ModPlayerHooks.NaturalLifeRegen.Invoke(
            this,
            ref regen
        );
    }

    public override void UpdateAutopause()
    {
        if (!ModPlayerHooks.UpdateAutopause.GetInvocationList().Any())
        {
            base.UpdateAutopause();
            return;
        }

        ModPlayerHooks.UpdateAutopause.Invoke(
            this
        );
    }

    public override void PreUpdate()
    {
        if (!ModPlayerHooks.PreUpdate.GetInvocationList().Any())
        {
            base.PreUpdate();
            return;
        }

        ModPlayerHooks.PreUpdate.Invoke(
            this
        );
    }

    public override void ProcessTriggers(
        Terraria.GameInput.TriggersSet triggersSet
    )
    {
        if (!ModPlayerHooks.ProcessTriggers.GetInvocationList().Any())
        {
            base.ProcessTriggers(
                triggersSet
            );
            return;
        }

        ModPlayerHooks.ProcessTriggers.Invoke(
            this,
            triggersSet
        );
    }

    public override void ArmorSetBonusActivated()
    {
        if (!ModPlayerHooks.ArmorSetBonusActivated.GetInvocationList().Any())
        {
            base.ArmorSetBonusActivated();
            return;
        }

        ModPlayerHooks.ArmorSetBonusActivated.Invoke(
            this
        );
    }

    public override void ArmorSetBonusHeld(
        int holdTime
    )
    {
        if (!ModPlayerHooks.ArmorSetBonusHeld.GetInvocationList().Any())
        {
            base.ArmorSetBonusHeld(
                holdTime
            );
            return;
        }

        ModPlayerHooks.ArmorSetBonusHeld.Invoke(
            this,
            holdTime
        );
    }

    public override void SetControls()
    {
        if (!ModPlayerHooks.SetControls.GetInvocationList().Any())
        {
            base.SetControls();
            return;
        }

        ModPlayerHooks.SetControls.Invoke(
            this
        );
    }

    public override void PreUpdateBuffs()
    {
        if (!ModPlayerHooks.PreUpdateBuffs.GetInvocationList().Any())
        {
            base.PreUpdateBuffs();
            return;
        }

        ModPlayerHooks.PreUpdateBuffs.Invoke(
            this
        );
    }

    public override void PostUpdateBuffs()
    {
        if (!ModPlayerHooks.PostUpdateBuffs.GetInvocationList().Any())
        {
            base.PostUpdateBuffs();
            return;
        }

        ModPlayerHooks.PostUpdateBuffs.Invoke(
            this
        );
    }

    public override void UpdateEquips()
    {
        if (!ModPlayerHooks.UpdateEquips.GetInvocationList().Any())
        {
            base.UpdateEquips();
            return;
        }

        ModPlayerHooks.UpdateEquips.Invoke(
            this
        );
    }

    public override void PostUpdateEquips()
    {
        if (!ModPlayerHooks.PostUpdateEquips.GetInvocationList().Any())
        {
            base.PostUpdateEquips();
            return;
        }

        ModPlayerHooks.PostUpdateEquips.Invoke(
            this
        );
    }

    public override void UpdateVisibleAccessories()
    {
        if (!ModPlayerHooks.UpdateVisibleAccessories.GetInvocationList().Any())
        {
            base.UpdateVisibleAccessories();
            return;
        }

        ModPlayerHooks.UpdateVisibleAccessories.Invoke(
            this
        );
    }

    public override void UpdateVisibleVanityAccessories()
    {
        if (!ModPlayerHooks.UpdateVisibleVanityAccessories.GetInvocationList().Any())
        {
            base.UpdateVisibleVanityAccessories();
            return;
        }

        ModPlayerHooks.UpdateVisibleVanityAccessories.Invoke(
            this
        );
    }

    public override void UpdateDyes()
    {
        if (!ModPlayerHooks.UpdateDyes.GetInvocationList().Any())
        {
            base.UpdateDyes();
            return;
        }

        ModPlayerHooks.UpdateDyes.Invoke(
            this
        );
    }

    public override void PostUpdateMiscEffects()
    {
        if (!ModPlayerHooks.PostUpdateMiscEffects.GetInvocationList().Any())
        {
            base.PostUpdateMiscEffects();
            return;
        }

        ModPlayerHooks.PostUpdateMiscEffects.Invoke(
            this
        );
    }

    public override void PostUpdateRunSpeeds()
    {
        if (!ModPlayerHooks.PostUpdateRunSpeeds.GetInvocationList().Any())
        {
            base.PostUpdateRunSpeeds();
            return;
        }

        ModPlayerHooks.PostUpdateRunSpeeds.Invoke(
            this
        );
    }

    public override void PreUpdateMovement()
    {
        if (!ModPlayerHooks.PreUpdateMovement.GetInvocationList().Any())
        {
            base.PreUpdateMovement();
            return;
        }

        ModPlayerHooks.PreUpdateMovement.Invoke(
            this
        );
    }

    public override void PostUpdate()
    {
        if (!ModPlayerHooks.PostUpdate.GetInvocationList().Any())
        {
            base.PostUpdate();
            return;
        }

        ModPlayerHooks.PostUpdate.Invoke(
            this
        );
    }

    public override void ModifyExtraJumpDurationMultiplier(
        Terraria.ModLoader.ExtraJump jump,
        ref float duration
    )
    {
        if (!ModPlayerHooks.ModifyExtraJumpDurationMultiplier.GetInvocationList().Any())
        {
            base.ModifyExtraJumpDurationMultiplier(
                jump,
                ref duration
            );
            return;
        }

        ModPlayerHooks.ModifyExtraJumpDurationMultiplier.Invoke(
            this,
            jump,
            ref duration
        );
    }

    public override bool CanStartExtraJump(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        if (!ModPlayerHooks.CanStartExtraJump.GetInvocationList().Any())
        {
            return base.CanStartExtraJump(
                jump
            );
        }

        return ModPlayerHooks.CanStartExtraJump.Invoke(
            this,
            jump
        );
    }

    public override void OnExtraJumpStarted(
        Terraria.ModLoader.ExtraJump jump,
        ref bool playSound
    )
    {
        if (!ModPlayerHooks.OnExtraJumpStarted.GetInvocationList().Any())
        {
            base.OnExtraJumpStarted(
                jump,
                ref playSound
            );
            return;
        }

        ModPlayerHooks.OnExtraJumpStarted.Invoke(
            this,
            jump,
            ref playSound
        );
    }

    public override void OnExtraJumpEnded(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        if (!ModPlayerHooks.OnExtraJumpEnded.GetInvocationList().Any())
        {
            base.OnExtraJumpEnded(
                jump
            );
            return;
        }

        ModPlayerHooks.OnExtraJumpEnded.Invoke(
            this,
            jump
        );
    }

    public override void OnExtraJumpRefreshed(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        if (!ModPlayerHooks.OnExtraJumpRefreshed.GetInvocationList().Any())
        {
            base.OnExtraJumpRefreshed(
                jump
            );
            return;
        }

        ModPlayerHooks.OnExtraJumpRefreshed.Invoke(
            this,
            jump
        );
    }

    public override void ExtraJumpVisuals(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        if (!ModPlayerHooks.ExtraJumpVisuals.GetInvocationList().Any())
        {
            base.ExtraJumpVisuals(
                jump
            );
            return;
        }

        ModPlayerHooks.ExtraJumpVisuals.Invoke(
            this,
            jump
        );
    }

    public override bool CanShowExtraJumpVisuals(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        if (!ModPlayerHooks.CanShowExtraJumpVisuals.GetInvocationList().Any())
        {
            return base.CanShowExtraJumpVisuals(
                jump
            );
        }

        return ModPlayerHooks.CanShowExtraJumpVisuals.Invoke(
            this,
            jump
        );
    }

    public override void OnExtraJumpCleared(
        Terraria.ModLoader.ExtraJump jump
    )
    {
        if (!ModPlayerHooks.OnExtraJumpCleared.GetInvocationList().Any())
        {
            base.OnExtraJumpCleared(
                jump
            );
            return;
        }

        ModPlayerHooks.OnExtraJumpCleared.Invoke(
            this,
            jump
        );
    }

    public override void FrameEffects()
    {
        if (!ModPlayerHooks.FrameEffects.GetInvocationList().Any())
        {
            base.FrameEffects();
            return;
        }

        ModPlayerHooks.FrameEffects.Invoke(
            this
        );
    }

    public override bool ImmuneTo(
        Terraria.DataStructures.PlayerDeathReason damageSource,
        int cooldownCounter,
        bool dodgeable
    )
    {
        if (!ModPlayerHooks.ImmuneTo.GetInvocationList().Any())
        {
            return base.ImmuneTo(
                damageSource,
                cooldownCounter,
                dodgeable
            );
        }

        return ModPlayerHooks.ImmuneTo.Invoke(
            this,
            damageSource,
            cooldownCounter,
            dodgeable
        );
    }

    public override bool FreeDodge(
        Terraria.Player.HurtInfo info
    )
    {
        if (!ModPlayerHooks.FreeDodge.GetInvocationList().Any())
        {
            return base.FreeDodge(
                info
            );
        }

        return ModPlayerHooks.FreeDodge.Invoke(
            this,
            info
        );
    }

    public override bool ConsumableDodge(
        Terraria.Player.HurtInfo info
    )
    {
        if (!ModPlayerHooks.ConsumableDodge.GetInvocationList().Any())
        {
            return base.ConsumableDodge(
                info
            );
        }

        return ModPlayerHooks.ConsumableDodge.Invoke(
            this,
            info
        );
    }

    public override void ModifyHurt(
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        if (!ModPlayerHooks.ModifyHurt.GetInvocationList().Any())
        {
            base.ModifyHurt(
                ref modifiers
            );
            return;
        }

        ModPlayerHooks.ModifyHurt.Invoke(
            this,
            ref modifiers
        );
    }

    public override void OnHurt(
        Terraria.Player.HurtInfo info
    )
    {
        if (!ModPlayerHooks.OnHurt.GetInvocationList().Any())
        {
            base.OnHurt(
                info
            );
            return;
        }

        ModPlayerHooks.OnHurt.Invoke(
            this,
            info
        );
    }

    public override void PostHurt(
        Terraria.Player.HurtInfo info
    )
    {
        if (!ModPlayerHooks.PostHurt.GetInvocationList().Any())
        {
            base.PostHurt(
                info
            );
            return;
        }

        ModPlayerHooks.PostHurt.Invoke(
            this,
            info
        );
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
        if (!ModPlayerHooks.PreKill.GetInvocationList().Any())
        {
            return base.PreKill(
                damage,
                hitDirection,
                pvp,
                ref playSound,
                ref genDust,
                ref damageSource
            );
        }

        return ModPlayerHooks.PreKill.Invoke(
            this,
            damage,
            hitDirection,
            pvp,
            ref playSound,
            ref genDust,
            ref damageSource
        );
    }

    public override void Kill(
        double damage,
        int hitDirection,
        bool pvp,
        Terraria.DataStructures.PlayerDeathReason damageSource
    )
    {
        if (!ModPlayerHooks.Kill.GetInvocationList().Any())
        {
            base.Kill(
                damage,
                hitDirection,
                pvp,
                damageSource
            );
            return;
        }

        ModPlayerHooks.Kill.Invoke(
            this,
            damage,
            hitDirection,
            pvp,
            damageSource
        );
    }

    public override bool PreModifyLuck(
        ref float luck
    )
    {
        if (!ModPlayerHooks.PreModifyLuck.GetInvocationList().Any())
        {
            return base.PreModifyLuck(
                ref luck
            );
        }

        return ModPlayerHooks.PreModifyLuck.Invoke(
            this,
            ref luck
        );
    }

    public override void ModifyLuck(
        ref float luck
    )
    {
        if (!ModPlayerHooks.ModifyLuck.GetInvocationList().Any())
        {
            base.ModifyLuck(
                ref luck
            );
            return;
        }

        ModPlayerHooks.ModifyLuck.Invoke(
            this,
            ref luck
        );
    }

    public override bool PreItemCheck()
    {
        if (!ModPlayerHooks.PreItemCheck.GetInvocationList().Any())
        {
            return base.PreItemCheck();
        }

        return ModPlayerHooks.PreItemCheck.Invoke(
            this
        );
    }

    public override void PostItemCheck()
    {
        if (!ModPlayerHooks.PostItemCheck.GetInvocationList().Any())
        {
            base.PostItemCheck();
            return;
        }

        ModPlayerHooks.PostItemCheck.Invoke(
            this
        );
    }

    public override void GetHealLife(
        Terraria.Item item,
        bool quickHeal,
        ref int healValue
    )
    {
        if (!ModPlayerHooks.GetHealLife.GetInvocationList().Any())
        {
            base.GetHealLife(
                item,
                quickHeal,
                ref healValue
            );
            return;
        }

        ModPlayerHooks.GetHealLife.Invoke(
            this,
            item,
            quickHeal,
            ref healValue
        );
    }

    public override void GetHealMana(
        Terraria.Item item,
        bool quickHeal,
        ref int healValue
    )
    {
        if (!ModPlayerHooks.GetHealMana.GetInvocationList().Any())
        {
            base.GetHealMana(
                item,
                quickHeal,
                ref healValue
            );
            return;
        }

        ModPlayerHooks.GetHealMana.Invoke(
            this,
            item,
            quickHeal,
            ref healValue
        );
    }

    public override void ModifyManaCost(
        Terraria.Item item,
        ref float reduce,
        ref float mult
    )
    {
        if (!ModPlayerHooks.ModifyManaCost.GetInvocationList().Any())
        {
            base.ModifyManaCost(
                item,
                ref reduce,
                ref mult
            );
            return;
        }

        ModPlayerHooks.ModifyManaCost.Invoke(
            this,
            item,
            ref reduce,
            ref mult
        );
    }

    public override void OnMissingMana(
        Terraria.Item item,
        int neededMana
    )
    {
        if (!ModPlayerHooks.OnMissingMana.GetInvocationList().Any())
        {
            base.OnMissingMana(
                item,
                neededMana
            );
            return;
        }

        ModPlayerHooks.OnMissingMana.Invoke(
            this,
            item,
            neededMana
        );
    }

    public override void OnConsumeMana(
        Terraria.Item item,
        int manaConsumed
    )
    {
        if (!ModPlayerHooks.OnConsumeMana.GetInvocationList().Any())
        {
            base.OnConsumeMana(
                item,
                manaConsumed
            );
            return;
        }

        ModPlayerHooks.OnConsumeMana.Invoke(
            this,
            item,
            manaConsumed
        );
    }

    public override void ModifyWeaponDamage(
        Terraria.Item item,
        ref Terraria.ModLoader.StatModifier damage
    )
    {
        if (!ModPlayerHooks.ModifyWeaponDamage.GetInvocationList().Any())
        {
            base.ModifyWeaponDamage(
                item,
                ref damage
            );
            return;
        }

        ModPlayerHooks.ModifyWeaponDamage.Invoke(
            this,
            item,
            ref damage
        );
    }

    public override void ModifyWeaponKnockback(
        Terraria.Item item,
        ref Terraria.ModLoader.StatModifier knockback
    )
    {
        if (!ModPlayerHooks.ModifyWeaponKnockback.GetInvocationList().Any())
        {
            base.ModifyWeaponKnockback(
                item,
                ref knockback
            );
            return;
        }

        ModPlayerHooks.ModifyWeaponKnockback.Invoke(
            this,
            item,
            ref knockback
        );
    }

    public override void ModifyWeaponCrit(
        Terraria.Item item,
        ref float crit
    )
    {
        if (!ModPlayerHooks.ModifyWeaponCrit.GetInvocationList().Any())
        {
            base.ModifyWeaponCrit(
                item,
                ref crit
            );
            return;
        }

        ModPlayerHooks.ModifyWeaponCrit.Invoke(
            this,
            item,
            ref crit
        );
    }

    public override bool CanConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo
    )
    {
        if (!ModPlayerHooks.CanConsumeAmmo.GetInvocationList().Any())
        {
            return base.CanConsumeAmmo(
                weapon,
                ammo
            );
        }

        return ModPlayerHooks.CanConsumeAmmo.Invoke(
            this,
            weapon,
            ammo
        );
    }

    public override void OnConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo
    )
    {
        if (!ModPlayerHooks.OnConsumeAmmo.GetInvocationList().Any())
        {
            base.OnConsumeAmmo(
                weapon,
                ammo
            );
            return;
        }

        ModPlayerHooks.OnConsumeAmmo.Invoke(
            this,
            weapon,
            ammo
        );
    }

    public override bool CanShoot(
        Terraria.Item item
    )
    {
        if (!ModPlayerHooks.CanShoot.GetInvocationList().Any())
        {
            return base.CanShoot(
                item
            );
        }

        return ModPlayerHooks.CanShoot.Invoke(
            this,
            item
        );
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
        if (!ModPlayerHooks.ModifyShootStats.GetInvocationList().Any())
        {
            base.ModifyShootStats(
                item,
                ref position,
                ref velocity,
                ref type,
                ref damage,
                ref knockback
            );
            return;
        }

        ModPlayerHooks.ModifyShootStats.Invoke(
            this,
            item,
            ref position,
            ref velocity,
            ref type,
            ref damage,
            ref knockback
        );
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
        if (!ModPlayerHooks.Shoot.GetInvocationList().Any())
        {
            return base.Shoot(
                item,
                source,
                position,
                velocity,
                type,
                damage,
                knockback
            );
        }

        return ModPlayerHooks.Shoot.Invoke(
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

    public override void MeleeEffects(
        Terraria.Item item,
        Microsoft.Xna.Framework.Rectangle hitbox
    )
    {
        if (!ModPlayerHooks.MeleeEffects.GetInvocationList().Any())
        {
            base.MeleeEffects(
                item,
                hitbox
            );
            return;
        }

        ModPlayerHooks.MeleeEffects.Invoke(
            this,
            item,
            hitbox
        );
    }

    public override void EmitEnchantmentVisualsAt(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Vector2 boxPosition,
        int boxWidth,
        int boxHeight
    )
    {
        if (!ModPlayerHooks.EmitEnchantmentVisualsAt.GetInvocationList().Any())
        {
            base.EmitEnchantmentVisualsAt(
                projectile,
                boxPosition,
                boxWidth,
                boxHeight
            );
            return;
        }

        ModPlayerHooks.EmitEnchantmentVisualsAt.Invoke(
            this,
            projectile,
            boxPosition,
            boxWidth,
            boxHeight
        );
    }

    public override void OnCatchNPC(
        Terraria.NPC npc,
        Terraria.Item item,
        bool failed
    )
    {
        if (!ModPlayerHooks.OnCatchNPC.GetInvocationList().Any())
        {
            base.OnCatchNPC(
                npc,
                item,
                failed
            );
            return;
        }

        ModPlayerHooks.OnCatchNPC.Invoke(
            this,
            npc,
            item,
            failed
        );
    }

    public override void ModifyItemScale(
        Terraria.Item item,
        ref float scale
    )
    {
        if (!ModPlayerHooks.ModifyItemScale.GetInvocationList().Any())
        {
            base.ModifyItemScale(
                item,
                ref scale
            );
            return;
        }

        ModPlayerHooks.ModifyItemScale.Invoke(
            this,
            item,
            ref scale
        );
    }

    public override void OnHitAnything(
        float x,
        float y,
        Terraria.Entity victim
    )
    {
        if (!ModPlayerHooks.OnHitAnything.GetInvocationList().Any())
        {
            base.OnHitAnything(
                x,
                y,
                victim
            );
            return;
        }

        ModPlayerHooks.OnHitAnything.Invoke(
            this,
            x,
            y,
            victim
        );
    }

    public override bool CanHitNPC(
        Terraria.NPC target
    )
    {
        if (!ModPlayerHooks.CanHitNPC.GetInvocationList().Any())
        {
            return base.CanHitNPC(
                target
            );
        }

        return ModPlayerHooks.CanHitNPC.Invoke(
            this,
            target
        );
    }

    public override void ModifyHitNPC(
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!ModPlayerHooks.ModifyHitNPC.GetInvocationList().Any())
        {
            base.ModifyHitNPC(
                target,
                ref modifiers
            );
            return;
        }

        ModPlayerHooks.ModifyHitNPC.Invoke(
            this,
            target,
            ref modifiers
        );
    }

    public override void OnHitNPC(
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        if (!ModPlayerHooks.OnHitNPC.GetInvocationList().Any())
        {
            base.OnHitNPC(
                target,
                hit,
                damageDone
            );
            return;
        }

        ModPlayerHooks.OnHitNPC.Invoke(
            this,
            target,
            hit,
            damageDone
        );
    }

    public override void ModifyHitNPCWithItem(
        Terraria.Item item,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!ModPlayerHooks.ModifyHitNPCWithItem.GetInvocationList().Any())
        {
            base.ModifyHitNPCWithItem(
                item,
                target,
                ref modifiers
            );
            return;
        }

        ModPlayerHooks.ModifyHitNPCWithItem.Invoke(
            this,
            item,
            target,
            ref modifiers
        );
    }

    public override void OnHitNPCWithItem(
        Terraria.Item item,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        if (!ModPlayerHooks.OnHitNPCWithItem.GetInvocationList().Any())
        {
            base.OnHitNPCWithItem(
                item,
                target,
                hit,
                damageDone
            );
            return;
        }

        ModPlayerHooks.OnHitNPCWithItem.Invoke(
            this,
            item,
            target,
            hit,
            damageDone
        );
    }

    public override void ModifyHitNPCWithProj(
        Terraria.Projectile proj,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!ModPlayerHooks.ModifyHitNPCWithProj.GetInvocationList().Any())
        {
            base.ModifyHitNPCWithProj(
                proj,
                target,
                ref modifiers
            );
            return;
        }

        ModPlayerHooks.ModifyHitNPCWithProj.Invoke(
            this,
            proj,
            target,
            ref modifiers
        );
    }

    public override void OnHitNPCWithProj(
        Terraria.Projectile proj,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        if (!ModPlayerHooks.OnHitNPCWithProj.GetInvocationList().Any())
        {
            base.OnHitNPCWithProj(
                proj,
                target,
                hit,
                damageDone
            );
            return;
        }

        ModPlayerHooks.OnHitNPCWithProj.Invoke(
            this,
            proj,
            target,
            hit,
            damageDone
        );
    }

    public override bool CanHitPvp(
        Terraria.Item item,
        Terraria.Player target
    )
    {
        if (!ModPlayerHooks.CanHitPvp.GetInvocationList().Any())
        {
            return base.CanHitPvp(
                item,
                target
            );
        }

        return ModPlayerHooks.CanHitPvp.Invoke(
            this,
            item,
            target
        );
    }

    public override bool CanHitPvpWithProj(
        Terraria.Projectile proj,
        Terraria.Player target
    )
    {
        if (!ModPlayerHooks.CanHitPvpWithProj.GetInvocationList().Any())
        {
            return base.CanHitPvpWithProj(
                proj,
                target
            );
        }

        return ModPlayerHooks.CanHitPvpWithProj.Invoke(
            this,
            proj,
            target
        );
    }

    public override bool CanBeHitByNPC(
        Terraria.NPC npc,
        ref int cooldownSlot
    )
    {
        if (!ModPlayerHooks.CanBeHitByNPC.GetInvocationList().Any())
        {
            return base.CanBeHitByNPC(
                npc,
                ref cooldownSlot
            );
        }

        return ModPlayerHooks.CanBeHitByNPC.Invoke(
            this,
            npc,
            ref cooldownSlot
        );
    }

    public override void ModifyHitByNPC(
        Terraria.NPC npc,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        if (!ModPlayerHooks.ModifyHitByNPC.GetInvocationList().Any())
        {
            base.ModifyHitByNPC(
                npc,
                ref modifiers
            );
            return;
        }

        ModPlayerHooks.ModifyHitByNPC.Invoke(
            this,
            npc,
            ref modifiers
        );
    }

    public override void OnHitByNPC(
        Terraria.NPC npc,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        if (!ModPlayerHooks.OnHitByNPC.GetInvocationList().Any())
        {
            base.OnHitByNPC(
                npc,
                hurtInfo
            );
            return;
        }

        ModPlayerHooks.OnHitByNPC.Invoke(
            this,
            npc,
            hurtInfo
        );
    }

    public override bool CanBeHitByProjectile(
        Terraria.Projectile proj
    )
    {
        if (!ModPlayerHooks.CanBeHitByProjectile.GetInvocationList().Any())
        {
            return base.CanBeHitByProjectile(
                proj
            );
        }

        return ModPlayerHooks.CanBeHitByProjectile.Invoke(
            this,
            proj
        );
    }

    public override void ModifyHitByProjectile(
        Terraria.Projectile proj,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        if (!ModPlayerHooks.ModifyHitByProjectile.GetInvocationList().Any())
        {
            base.ModifyHitByProjectile(
                proj,
                ref modifiers
            );
            return;
        }

        ModPlayerHooks.ModifyHitByProjectile.Invoke(
            this,
            proj,
            ref modifiers
        );
    }

    public override void OnHitByProjectile(
        Terraria.Projectile proj,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        if (!ModPlayerHooks.OnHitByProjectile.GetInvocationList().Any())
        {
            base.OnHitByProjectile(
                proj,
                hurtInfo
            );
            return;
        }

        ModPlayerHooks.OnHitByProjectile.Invoke(
            this,
            proj,
            hurtInfo
        );
    }

    public override void ModifyFishingAttempt(
        ref Terraria.DataStructures.FishingAttempt attempt
    )
    {
        if (!ModPlayerHooks.ModifyFishingAttempt.GetInvocationList().Any())
        {
            base.ModifyFishingAttempt(
                ref attempt
            );
            return;
        }

        ModPlayerHooks.ModifyFishingAttempt.Invoke(
            this,
            ref attempt
        );
    }

    public override void CatchFish(
        Terraria.DataStructures.FishingAttempt attempt,
        ref int itemDrop,
        ref int npcSpawn,
        ref Terraria.AdvancedPopupRequest sonar,
        ref Microsoft.Xna.Framework.Vector2 sonarPosition
    )
    {
        if (!ModPlayerHooks.CatchFish.GetInvocationList().Any())
        {
            base.CatchFish(
                attempt,
                ref itemDrop,
                ref npcSpawn,
                ref sonar,
                ref sonarPosition
            );
            return;
        }

        ModPlayerHooks.CatchFish.Invoke(
            this,
            attempt,
            ref itemDrop,
            ref npcSpawn,
            ref sonar,
            ref sonarPosition
        );
    }

    public override void ModifyCaughtFish(
        Terraria.Item fish
    )
    {
        if (!ModPlayerHooks.ModifyCaughtFish.GetInvocationList().Any())
        {
            base.ModifyCaughtFish(
                fish
            );
            return;
        }

        ModPlayerHooks.ModifyCaughtFish.Invoke(
            this,
            fish
        );
    }

    public override void GetFishingLevel(
        Terraria.Item fishingRod,
        Terraria.Item bait,
        ref float fishingLevel
    )
    {
        if (!ModPlayerHooks.GetFishingLevel.GetInvocationList().Any())
        {
            base.GetFishingLevel(
                fishingRod,
                bait,
                ref fishingLevel
            );
            return;
        }

        ModPlayerHooks.GetFishingLevel.Invoke(
            this,
            fishingRod,
            bait,
            ref fishingLevel
        );
    }

    public override void AnglerQuestReward(
        float rareMultiplier,
        System.Collections.Generic.List<Terraria.Item> rewardItems
    )
    {
        if (!ModPlayerHooks.AnglerQuestReward.GetInvocationList().Any())
        {
            base.AnglerQuestReward(
                rareMultiplier,
                rewardItems
            );
            return;
        }

        ModPlayerHooks.AnglerQuestReward.Invoke(
            this,
            rareMultiplier,
            rewardItems
        );
    }

    public override void GetDyeTraderReward(
        System.Collections.Generic.List<int> rewardPool
    )
    {
        if (!ModPlayerHooks.GetDyeTraderReward.GetInvocationList().Any())
        {
            base.GetDyeTraderReward(
                rewardPool
            );
            return;
        }

        ModPlayerHooks.GetDyeTraderReward.Invoke(
            this,
            rewardPool
        );
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
        if (!ModPlayerHooks.DrawEffects.GetInvocationList().Any())
        {
            base.DrawEffects(
                drawInfo,
                ref r,
                ref g,
                ref b,
                ref a,
                ref fullBright
            );
            return;
        }

        ModPlayerHooks.DrawEffects.Invoke(
            this,
            drawInfo,
            ref r,
            ref g,
            ref b,
            ref a,
            ref fullBright
        );
    }

    public override void ModifyDrawInfo(
        ref Terraria.DataStructures.PlayerDrawSet drawInfo
    )
    {
        if (!ModPlayerHooks.ModifyDrawInfo.GetInvocationList().Any())
        {
            base.ModifyDrawInfo(
                ref drawInfo
            );
            return;
        }

        ModPlayerHooks.ModifyDrawInfo.Invoke(
            this,
            ref drawInfo
        );
    }

    public override void ModifyDrawLayerOrdering(
        System.Collections.Generic.IDictionary<Terraria.ModLoader.PlayerDrawLayer, Terraria.ModLoader.PlayerDrawLayer.Position> positions
    )
    {
        if (!ModPlayerHooks.ModifyDrawLayerOrdering.GetInvocationList().Any())
        {
            base.ModifyDrawLayerOrdering(
                positions
            );
            return;
        }

        ModPlayerHooks.ModifyDrawLayerOrdering.Invoke(
            this,
            positions
        );
    }

    public override void HideDrawLayers(
        Terraria.DataStructures.PlayerDrawSet drawInfo
    )
    {
        if (!ModPlayerHooks.HideDrawLayers.GetInvocationList().Any())
        {
            base.HideDrawLayers(
                drawInfo
            );
            return;
        }

        ModPlayerHooks.HideDrawLayers.Invoke(
            this,
            drawInfo
        );
    }

    public override void ModifyScreenPosition()
    {
        if (!ModPlayerHooks.ModifyScreenPosition.GetInvocationList().Any())
        {
            base.ModifyScreenPosition();
            return;
        }

        ModPlayerHooks.ModifyScreenPosition.Invoke(
            this
        );
    }

    public override void ModifyZoom(
        ref float zoom
    )
    {
        if (!ModPlayerHooks.ModifyZoom.GetInvocationList().Any())
        {
            base.ModifyZoom(
                ref zoom
            );
            return;
        }

        ModPlayerHooks.ModifyZoom.Invoke(
            this,
            ref zoom
        );
    }

    public override void PlayerConnect()
    {
        if (!ModPlayerHooks.PlayerConnect.GetInvocationList().Any())
        {
            base.PlayerConnect();
            return;
        }

        ModPlayerHooks.PlayerConnect.Invoke(
            this
        );
    }

    public override void PlayerDisconnect()
    {
        if (!ModPlayerHooks.PlayerDisconnect.GetInvocationList().Any())
        {
            base.PlayerDisconnect();
            return;
        }

        ModPlayerHooks.PlayerDisconnect.Invoke(
            this
        );
    }

    public override void OnEnterWorld()
    {
        if (!ModPlayerHooks.OnEnterWorld.GetInvocationList().Any())
        {
            base.OnEnterWorld();
            return;
        }

        ModPlayerHooks.OnEnterWorld.Invoke(
            this
        );
    }

    public override void OnRespawn()
    {
        if (!ModPlayerHooks.OnRespawn.GetInvocationList().Any())
        {
            base.OnRespawn();
            return;
        }

        ModPlayerHooks.OnRespawn.Invoke(
            this
        );
    }

    public override bool ShiftClickSlot(
        Terraria.Item[] inventory,
        int context,
        int slot
    )
    {
        if (!ModPlayerHooks.ShiftClickSlot.GetInvocationList().Any())
        {
            return base.ShiftClickSlot(
                inventory,
                context,
                slot
            );
        }

        return ModPlayerHooks.ShiftClickSlot.Invoke(
            this,
            inventory,
            context,
            slot
        );
    }

    public override bool HoverSlot(
        Terraria.Item[] inventory,
        int context,
        int slot
    )
    {
        if (!ModPlayerHooks.HoverSlot.GetInvocationList().Any())
        {
            return base.HoverSlot(
                inventory,
                context,
                slot
            );
        }

        return ModPlayerHooks.HoverSlot.Invoke(
            this,
            inventory,
            context,
            slot
        );
    }

    public override void PostSellItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        if (!ModPlayerHooks.PostSellItem.GetInvocationList().Any())
        {
            base.PostSellItem(
                vendor,
                shopInventory,
                item
            );
            return;
        }

        ModPlayerHooks.PostSellItem.Invoke(
            this,
            vendor,
            shopInventory,
            item
        );
    }

    public override bool CanSellItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        if (!ModPlayerHooks.CanSellItem.GetInvocationList().Any())
        {
            return base.CanSellItem(
                vendor,
                shopInventory,
                item
            );
        }

        return ModPlayerHooks.CanSellItem.Invoke(
            this,
            vendor,
            shopInventory,
            item
        );
    }

    public override void PostBuyItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        if (!ModPlayerHooks.PostBuyItem.GetInvocationList().Any())
        {
            base.PostBuyItem(
                vendor,
                shopInventory,
                item
            );
            return;
        }

        ModPlayerHooks.PostBuyItem.Invoke(
            this,
            vendor,
            shopInventory,
            item
        );
    }

    public override bool CanBuyItem(
        Terraria.NPC vendor,
        Terraria.Item[] shopInventory,
        Terraria.Item item
    )
    {
        if (!ModPlayerHooks.CanBuyItem.GetInvocationList().Any())
        {
            return base.CanBuyItem(
                vendor,
                shopInventory,
                item
            );
        }

        return ModPlayerHooks.CanBuyItem.Invoke(
            this,
            vendor,
            shopInventory,
            item
        );
    }

    public override bool CanUseItem(
        Terraria.Item item
    )
    {
        if (!ModPlayerHooks.CanUseItem.GetInvocationList().Any())
        {
            return base.CanUseItem(
                item
            );
        }

        return ModPlayerHooks.CanUseItem.Invoke(
            this,
            item
        );
    }

    public override bool ModifyNurseHeal(
        Terraria.NPC nurse,
        ref int health,
        ref bool removeDebuffs,
        ref string chatText
    )
    {
        if (!ModPlayerHooks.ModifyNurseHeal.GetInvocationList().Any())
        {
            return base.ModifyNurseHeal(
                nurse,
                ref health,
                ref removeDebuffs,
                ref chatText
            );
        }

        return ModPlayerHooks.ModifyNurseHeal.Invoke(
            this,
            nurse,
            ref health,
            ref removeDebuffs,
            ref chatText
        );
    }

    public override void ModifyNursePrice(
        Terraria.NPC nurse,
        int health,
        bool removeDebuffs,
        ref int price
    )
    {
        if (!ModPlayerHooks.ModifyNursePrice.GetInvocationList().Any())
        {
            base.ModifyNursePrice(
                nurse,
                health,
                removeDebuffs,
                ref price
            );
            return;
        }

        ModPlayerHooks.ModifyNursePrice.Invoke(
            this,
            nurse,
            health,
            removeDebuffs,
            ref price
        );
    }

    public override void PostNurseHeal(
        Terraria.NPC nurse,
        int health,
        bool removeDebuffs,
        int price
    )
    {
        if (!ModPlayerHooks.PostNurseHeal.GetInvocationList().Any())
        {
            base.PostNurseHeal(
                nurse,
                health,
                removeDebuffs,
                price
            );
            return;
        }

        ModPlayerHooks.PostNurseHeal.Invoke(
            this,
            nurse,
            health,
            removeDebuffs,
            price
        );
    }

    public override void ModifyStartingInventory(
        System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.List<Terraria.Item>> itemsByMod,
        bool mediumCoreDeath
    )
    {
        if (!ModPlayerHooks.ModifyStartingInventory.GetInvocationList().Any())
        {
            base.ModifyStartingInventory(
                itemsByMod,
                mediumCoreDeath
            );
            return;
        }

        ModPlayerHooks.ModifyStartingInventory.Invoke(
            this,
            itemsByMod,
            mediumCoreDeath
        );
    }

    public override bool OnPickup(
        Terraria.Item item
    )
    {
        if (!ModPlayerHooks.OnPickup.GetInvocationList().Any())
        {
            return base.OnPickup(
                item
            );
        }

        return ModPlayerHooks.OnPickup.Invoke(
            this,
            item
        );
    }

    public override bool CanBeTeleportedTo(
        Microsoft.Xna.Framework.Vector2 teleportPosition,
        string context
    )
    {
        if (!ModPlayerHooks.CanBeTeleportedTo.GetInvocationList().Any())
        {
            return base.CanBeTeleportedTo(
                teleportPosition,
                context
            );
        }

        return ModPlayerHooks.CanBeTeleportedTo.Invoke(
            this,
            teleportPosition,
            context
        );
    }

    public override void OnEquipmentLoadoutSwitched(
        int oldLoadoutIndex,
        int loadoutIndex
    )
    {
        if (!ModPlayerHooks.OnEquipmentLoadoutSwitched.GetInvocationList().Any())
        {
            base.OnEquipmentLoadoutSwitched(
                oldLoadoutIndex,
                loadoutIndex
            );
            return;
        }

        ModPlayerHooks.OnEquipmentLoadoutSwitched.Invoke(
            this,
            oldLoadoutIndex,
            loadoutIndex
        );
    }
}

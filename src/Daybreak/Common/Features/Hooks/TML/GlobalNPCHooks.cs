namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalNPC':
//     System.Void Terraria.ModLoader.GlobalNPC::SetDefaultsFromNetId(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::OnSpawn(Terraria.NPC,Terraria.DataStructures.IEntitySource)
//     System.Void Terraria.ModLoader.GlobalNPC::ApplyDifficultyAndPlayerScaling(Terraria.NPC,System.Int32,System.Single,System.Single)
//     System.Void Terraria.ModLoader.GlobalNPC::SetBestiary(Terraria.NPC,Terraria.GameContent.Bestiary.BestiaryDatabase,Terraria.GameContent.Bestiary.BestiaryEntry)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyTypeName(Terraria.NPC,System.String&)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyHoverBoundingBox(Terraria.NPC,Microsoft.Xna.Framework.Rectangle&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreHoverInteract(Terraria.NPC,System.Boolean)
//     Terraria.GameContent.ITownNPCProfile Terraria.ModLoader.GlobalNPC::ModifyTownNPCProfile(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyNPCNameList(Terraria.NPC,System.Collections.Generic.List`1<System.String>)
//     System.Void Terraria.ModLoader.GlobalNPC::ResetEffects(Terraria.NPC)
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreAI(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::AI(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::PostAI(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::FindFrame(Terraria.NPC,System.Int32)
//     System.Void Terraria.ModLoader.GlobalNPC::HitEffect(Terraria.NPC,Terraria.NPC/HitInfo)
//     System.Void Terraria.ModLoader.GlobalNPC::UpdateLifeRegen(Terraria.NPC,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::CheckActive(Terraria.NPC)
//     System.Boolean Terraria.ModLoader.GlobalNPC::CheckDead(Terraria.NPC)
//     System.Boolean Terraria.ModLoader.GlobalNPC::SpecialOnKill(Terraria.NPC)
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreKill(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::OnKill(Terraria.NPC)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::CanFallThroughPlatforms(Terraria.NPC)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::CanBeCaughtBy(Terraria.NPC,Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalNPC::OnCaughtBy(Terraria.NPC,Terraria.Player,Terraria.Item,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyNPCLoot(Terraria.NPC,Terraria.ModLoader.NPCLoot)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyGlobalLoot(Terraria.ModLoader.GlobalLoot)
//     System.Boolean Terraria.ModLoader.GlobalNPC::CanHitPlayer(Terraria.NPC,Terraria.Player,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyHitPlayer(Terraria.NPC,Terraria.Player,Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::OnHitPlayer(Terraria.NPC,Terraria.Player,Terraria.Player/HurtInfo)
//     System.Boolean Terraria.ModLoader.GlobalNPC::CanHitNPC(Terraria.NPC,Terraria.NPC)
//     System.Boolean Terraria.ModLoader.GlobalNPC::CanBeHitByNPC(Terraria.NPC,Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyHitNPC(Terraria.NPC,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::OnHitNPC(Terraria.NPC,Terraria.NPC,Terraria.NPC/HitInfo)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::CanBeHitByItem(Terraria.NPC,Terraria.Player,Terraria.Item)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::CanCollideWithPlayerMeleeAttack(Terraria.NPC,Terraria.Player,Terraria.Item,Microsoft.Xna.Framework.Rectangle)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyHitByItem(Terraria.NPC,Terraria.Player,Terraria.Item,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::OnHitByItem(Terraria.NPC,Terraria.Player,Terraria.Item,Terraria.NPC/HitInfo,System.Int32)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::CanBeHitByProjectile(Terraria.NPC,Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyHitByProjectile(Terraria.NPC,Terraria.Projectile,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::OnHitByProjectile(Terraria.NPC,Terraria.Projectile,Terraria.NPC/HitInfo,System.Int32)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyIncomingHit(Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::BossHeadSlot(Terraria.NPC,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::BossHeadRotation(Terraria.NPC,System.Single&)
//     System.Void Terraria.ModLoader.GlobalNPC::BossHeadSpriteEffects(Terraria.NPC,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Nullable`1<Microsoft.Xna.Framework.Color> Terraria.ModLoader.GlobalNPC::GetAlpha(Terraria.NPC,Microsoft.Xna.Framework.Color)
//     System.Void Terraria.ModLoader.GlobalNPC::DrawEffects(Terraria.NPC,Microsoft.Xna.Framework.Color&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreDraw(Terraria.NPC,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color)
//     System.Void Terraria.ModLoader.GlobalNPC::PostDraw(Terraria.NPC,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color)
//     System.Void Terraria.ModLoader.GlobalNPC::DrawBehind(Terraria.NPC,System.Int32)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::DrawHealthBar(Terraria.NPC,System.Byte,System.Single&,Microsoft.Xna.Framework.Vector2&)
//     System.Void Terraria.ModLoader.GlobalNPC::EditSpawnRate(Terraria.Player,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::EditSpawnRange(Terraria.Player,System.Int32&,System.Int32&,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::EditSpawnPool(System.Collections.Generic.IDictionary`2<System.Int32,System.Single>,Terraria.ModLoader.NPCSpawnInfo)
//     System.Void Terraria.ModLoader.GlobalNPC::SpawnNPC(System.Int32,System.Int32,System.Int32)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::CanChat(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::GetChat(Terraria.NPC,System.String&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreChatButtonClicked(Terraria.NPC,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalNPC::OnChatButtonClicked(Terraria.NPC,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyShop(Terraria.ModLoader.NPCShop)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyActiveShop(Terraria.NPC,System.String,Terraria.Item[])
//     System.Void Terraria.ModLoader.GlobalNPC::SetupTravelShop(System.Int32[],System.Int32&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalNPC::CanGoToStatue(Terraria.NPC,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalNPC::OnGoToStatue(Terraria.NPC,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalNPC::BuffTownNPC(System.Single&,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::ModifyDeathMessage(Terraria.NPC,Terraria.Localization.NetworkText&,Microsoft.Xna.Framework.Color&)
//     System.Void Terraria.ModLoader.GlobalNPC::TownNPCAttackStrength(Terraria.NPC,System.Int32&,System.Single&)
//     System.Void Terraria.ModLoader.GlobalNPC::TownNPCAttackCooldown(Terraria.NPC,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::TownNPCAttackProj(Terraria.NPC,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::TownNPCAttackProjSpeed(Terraria.NPC,System.Single&,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.GlobalNPC::TownNPCAttackShoot(Terraria.NPC,System.Boolean&)
//     System.Void Terraria.ModLoader.GlobalNPC::TownNPCAttackMagic(Terraria.NPC,System.Single&)
//     System.Void Terraria.ModLoader.GlobalNPC::TownNPCAttackSwing(Terraria.NPC,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::DrawTownAttackGun(Terraria.NPC,Microsoft.Xna.Framework.Graphics.Texture2D&,Microsoft.Xna.Framework.Rectangle&,System.Single&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::DrawTownAttackSwing(Terraria.NPC,Microsoft.Xna.Framework.Graphics.Texture2D&,Microsoft.Xna.Framework.Rectangle&,System.Int32&,System.Single&,Microsoft.Xna.Framework.Vector2&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::ModifyCollisionData(Terraria.NPC,Microsoft.Xna.Framework.Rectangle,System.Int32&,Terraria.ModLoader.MultipliableFloat&,Microsoft.Xna.Framework.Rectangle&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::NeedSaving(Terraria.NPC)
//     System.Nullable`1<System.Int32> Terraria.ModLoader.GlobalNPC::PickEmote(Terraria.NPC,Terraria.Player,System.Collections.Generic.List`1<System.Int32>,Terraria.GameContent.UI.WorldUIAnchor)
//     System.Void Terraria.ModLoader.GlobalNPC::ChatBubblePosition(Terraria.NPC,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Void Terraria.ModLoader.GlobalNPC::PartyHatPosition(Terraria.NPC,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Void Terraria.ModLoader.GlobalNPC::EmoteBubblePosition(Terraria.NPC,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
public static partial class GlobalNPCHooks
{
    public sealed partial class SetDefaultsFromNetId
    {
        public delegate void Original(
            Terraria.NPC npc
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_SetDefaultsFromNetId_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::SetDefaultsFromNetId")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::SetDefaultsFromNetId; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnSpawn
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.DataStructures.IEntitySource source
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.DataStructures.IEntitySource source
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnSpawn_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnSpawn")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnSpawn; use a flag to disable behavior.");
        }
    }

    public sealed partial class ApplyDifficultyAndPlayerScaling
    {
        public delegate void Original(
            Terraria.NPC npc,
            int numPlayers,
            float balance,
            float bossAdjustment
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int numPlayers,
            float balance,
            float bossAdjustment
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ApplyDifficultyAndPlayerScaling_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ApplyDifficultyAndPlayerScaling")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ApplyDifficultyAndPlayerScaling; use a flag to disable behavior.");
        }
    }

    public sealed partial class SetBestiary
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.GameContent.Bestiary.BestiaryDatabase database,
            Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.GameContent.Bestiary.BestiaryDatabase database,
            Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_SetBestiary_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::SetBestiary")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::SetBestiary; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyTypeName
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref string typeName
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref string typeName
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyTypeName_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyTypeName")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyTypeName; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHoverBoundingBox
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Rectangle boundingBox
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Rectangle boundingBox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyHoverBoundingBox_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyHoverBoundingBox")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyHoverBoundingBox; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreHoverInteract
    {
        public delegate bool Original(
            Terraria.NPC npc,
            bool mouseIntersects
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool mouseIntersects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PreHoverInteract_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PreHoverInteract")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PreHoverInteract; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyTownNPCProfile
    {
        public delegate Terraria.GameContent.ITownNPCProfile Original(
            Terraria.NPC npc
        );

        public delegate Terraria.GameContent.ITownNPCProfile Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyTownNPCProfile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyTownNPCProfile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyTownNPCProfile; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyNPCNameList
    {
        public delegate void Original(
            Terraria.NPC npc,
            System.Collections.Generic.List<string> nameList
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            System.Collections.Generic.List<string> nameList
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyNPCNameList_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyNPCNameList")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyNPCNameList; use a flag to disable behavior.");
        }
    }

    public sealed partial class ResetEffects
    {
        public delegate void Original(
            Terraria.NPC npc
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ResetEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ResetEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ResetEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreAI
    {
        public delegate bool Original(
            Terraria.NPC npc
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PreAI_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PreAI")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PreAI; use a flag to disable behavior.");
        }
    }

    public sealed partial class AI
    {
        public delegate void Original(
            Terraria.NPC npc
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_AI_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::AI")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::AI; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostAI
    {
        public delegate void Original(
            Terraria.NPC npc
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PostAI_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PostAI")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PostAI; use a flag to disable behavior.");
        }
    }

    public sealed partial class FindFrame
    {
        public delegate void Original(
            Terraria.NPC npc,
            int frameHeight
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int frameHeight
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_FindFrame_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::FindFrame")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::FindFrame; use a flag to disable behavior.");
        }
    }

    public sealed partial class HitEffect
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.NPC.HitInfo hit
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC.HitInfo hit
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_HitEffect_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::HitEffect")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::HitEffect; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateLifeRegen
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref int damage
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int damage
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_UpdateLifeRegen_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::UpdateLifeRegen")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::UpdateLifeRegen; use a flag to disable behavior.");
        }
    }

    public sealed partial class CheckActive
    {
        public delegate bool Original(
            Terraria.NPC npc
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CheckActive_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CheckActive")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CheckActive; use a flag to disable behavior.");
        }
    }

    public sealed partial class CheckDead
    {
        public delegate bool Original(
            Terraria.NPC npc
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CheckDead_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CheckDead")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CheckDead; use a flag to disable behavior.");
        }
    }

    public sealed partial class SpecialOnKill
    {
        public delegate bool Original(
            Terraria.NPC npc
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_SpecialOnKill_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::SpecialOnKill")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::SpecialOnKill; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreKill
    {
        public delegate bool Original(
            Terraria.NPC npc
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PreKill_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PreKill")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PreKill; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnKill
    {
        public delegate void Original(
            Terraria.NPC npc
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnKill_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnKill")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnKill; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanFallThroughPlatforms
    {
        public delegate bool? Original(
            Terraria.NPC npc
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanFallThroughPlatforms_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanFallThroughPlatforms")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanFallThroughPlatforms; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeCaughtBy
    {
        public delegate bool? Original(
            Terraria.NPC npc,
            Terraria.Item item,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanBeCaughtBy_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanBeCaughtBy")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanBeCaughtBy; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnCaughtBy
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            bool failed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            bool failed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnCaughtBy_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnCaughtBy")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnCaughtBy; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyNPCLoot
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.ModLoader.NPCLoot npcLoot
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.ModLoader.NPCLoot npcLoot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyNPCLoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyNPCLoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyNPCLoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyGlobalLoot
    {
        public delegate void Original(
            Terraria.ModLoader.GlobalLoot globalLoot
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.ModLoader.GlobalLoot globalLoot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyGlobalLoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyGlobalLoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyGlobalLoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitPlayer
    {
        public delegate bool Original(
            Terraria.NPC npc,
            Terraria.Player target,
            ref int cooldownSlot
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            ref int cooldownSlot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanHitPlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanHitPlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanHitPlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitPlayer
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyHitPlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyHitPlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyHitPlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitPlayer
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnHitPlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnHitPlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnHitPlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitNPC
    {
        public delegate bool Original(
            Terraria.NPC npc,
            Terraria.NPC target
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeHitByNPC
    {
        public delegate bool Original(
            Terraria.NPC npc,
            Terraria.NPC attacker
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC attacker
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanBeHitByNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanBeHitByNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanBeHitByNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitNPC
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeHitByItem
    {
        public delegate bool? Original(
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanBeHitByItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanBeHitByItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanBeHitByItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanCollideWithPlayerMeleeAttack
    {
        public delegate bool? Original(
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle meleeAttackHitbox
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            Microsoft.Xna.Framework.Rectangle meleeAttackHitbox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanCollideWithPlayerMeleeAttack_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanCollideWithPlayerMeleeAttack")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanCollideWithPlayerMeleeAttack; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitByItem
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyHitByItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyHitByItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyHitByItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitByItem
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnHitByItem_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnHitByItem")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnHitByItem; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeHitByProjectile
    {
        public delegate bool? Original(
            Terraria.NPC npc,
            Terraria.Projectile projectile
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanBeHitByProjectile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanBeHitByProjectile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanBeHitByProjectile; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitByProjectile
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyHitByProjectile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyHitByProjectile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyHitByProjectile; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitByProjectile
    {
        public delegate void Original(
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnHitByProjectile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnHitByProjectile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnHitByProjectile; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyIncomingHit
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyIncomingHit_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyIncomingHit")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyIncomingHit; use a flag to disable behavior.");
        }
    }

    public sealed partial class BossHeadSlot
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref int index
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int index
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_BossHeadSlot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::BossHeadSlot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::BossHeadSlot; use a flag to disable behavior.");
        }
    }

    public sealed partial class BossHeadRotation
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref float rotation
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float rotation
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_BossHeadRotation_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::BossHeadRotation")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::BossHeadRotation; use a flag to disable behavior.");
        }
    }

    public sealed partial class BossHeadSpriteEffects
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_BossHeadSpriteEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::BossHeadSpriteEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::BossHeadSpriteEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetAlpha
    {
        public delegate Microsoft.Xna.Framework.Color? Original(
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Color drawColor
        );

        public delegate Microsoft.Xna.Framework.Color? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Color drawColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_GetAlpha_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::GetAlpha")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::GetAlpha; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawEffects
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Color drawColor
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Color drawColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_DrawEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::DrawEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::DrawEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Original(
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PreDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PreDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PreDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Original(
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PostDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PostDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PostDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawBehind
    {
        public delegate void Original(
            Terraria.NPC npc,
            int index
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int index
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_DrawBehind_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::DrawBehind")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::DrawBehind; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawHealthBar
    {
        public delegate bool? Original(
            Terraria.NPC npc,
            byte hbPosition,
            ref float scale,
            ref Microsoft.Xna.Framework.Vector2 position
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            byte hbPosition,
            ref float scale,
            ref Microsoft.Xna.Framework.Vector2 position
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_DrawHealthBar_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::DrawHealthBar")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::DrawHealthBar; use a flag to disable behavior.");
        }
    }

    public sealed partial class EditSpawnRate
    {
        public delegate void Original(
            Terraria.Player player,
            ref int spawnRate,
            ref int maxSpawns
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.Player player,
            ref int spawnRate,
            ref int maxSpawns
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_EditSpawnRate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::EditSpawnRate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::EditSpawnRate; use a flag to disable behavior.");
        }
    }

    public sealed partial class EditSpawnRange
    {
        public delegate void Original(
            Terraria.Player player,
            ref int spawnRangeX,
            ref int spawnRangeY,
            ref int safeRangeX,
            ref int safeRangeY
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.Player player,
            ref int spawnRangeX,
            ref int spawnRangeY,
            ref int safeRangeX,
            ref int safeRangeY
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_EditSpawnRange_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::EditSpawnRange")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::EditSpawnRange; use a flag to disable behavior.");
        }
    }

    public sealed partial class EditSpawnPool
    {
        public delegate void Original(
            System.Collections.Generic.IDictionary<int, float> pool,
            Terraria.ModLoader.NPCSpawnInfo spawnInfo
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            System.Collections.Generic.IDictionary<int, float> pool,
            Terraria.ModLoader.NPCSpawnInfo spawnInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_EditSpawnPool_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::EditSpawnPool")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::EditSpawnPool; use a flag to disable behavior.");
        }
    }

    public sealed partial class SpawnNPC
    {
        public delegate void Original(
            int npc,
            int tileX,
            int tileY
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            int npc,
            int tileX,
            int tileY
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_SpawnNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::SpawnNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::SpawnNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanChat
    {
        public delegate bool? Original(
            Terraria.NPC npc
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanChat_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanChat")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanChat; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetChat
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref string chat
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref string chat
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_GetChat_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::GetChat")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::GetChat; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreChatButtonClicked
    {
        public delegate bool Original(
            Terraria.NPC npc,
            bool firstButton
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool firstButton
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PreChatButtonClicked_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PreChatButtonClicked")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PreChatButtonClicked; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnChatButtonClicked
    {
        public delegate void Original(
            Terraria.NPC npc,
            bool firstButton
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool firstButton
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnChatButtonClicked_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnChatButtonClicked")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnChatButtonClicked; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyShop
    {
        public delegate void Original(
            Terraria.ModLoader.NPCShop shop
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.ModLoader.NPCShop shop
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyShop_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyShop")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyShop; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyActiveShop
    {
        public delegate void Original(
            Terraria.NPC npc,
            string shopName,
            Terraria.Item[] items
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            string shopName,
            Terraria.Item[] items
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyActiveShop_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyActiveShop")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyActiveShop; use a flag to disable behavior.");
        }
    }

    public sealed partial class SetupTravelShop
    {
        public delegate void Original(
            int[] shop,
            ref int nextSlot
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            int[] shop,
            ref int nextSlot
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_SetupTravelShop_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::SetupTravelShop")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::SetupTravelShop; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanGoToStatue
    {
        public delegate bool? Original(
            Terraria.NPC npc,
            bool toKingStatue
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool toKingStatue
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_CanGoToStatue_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::CanGoToStatue")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::CanGoToStatue; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnGoToStatue
    {
        public delegate void Original(
            Terraria.NPC npc,
            bool toKingStatue
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool toKingStatue
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_OnGoToStatue_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::OnGoToStatue")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::OnGoToStatue; use a flag to disable behavior.");
        }
    }

    public sealed partial class BuffTownNPC
    {
        public delegate void Original(
            ref float damageMult,
            ref int defense
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            ref float damageMult,
            ref int defense
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_BuffTownNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::BuffTownNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::BuffTownNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyDeathMessage
    {
        public delegate bool Original(
            Terraria.NPC npc,
            ref Terraria.Localization.NetworkText customText,
            ref Microsoft.Xna.Framework.Color color
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Terraria.Localization.NetworkText customText,
            ref Microsoft.Xna.Framework.Color color
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyDeathMessage_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyDeathMessage")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyDeathMessage; use a flag to disable behavior.");
        }
    }

    public sealed partial class TownNPCAttackStrength
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref int damage,
            ref float knockback
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int damage,
            ref float knockback
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_TownNPCAttackStrength_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::TownNPCAttackStrength")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::TownNPCAttackStrength; use a flag to disable behavior.");
        }
    }

    public sealed partial class TownNPCAttackCooldown
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref int cooldown,
            ref int randExtraCooldown
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int cooldown,
            ref int randExtraCooldown
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_TownNPCAttackCooldown_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::TownNPCAttackCooldown")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::TownNPCAttackCooldown; use a flag to disable behavior.");
        }
    }

    public sealed partial class TownNPCAttackProj
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref int projType,
            ref int attackDelay
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int projType,
            ref int attackDelay
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_TownNPCAttackProj_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::TownNPCAttackProj")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::TownNPCAttackProj; use a flag to disable behavior.");
        }
    }

    public sealed partial class TownNPCAttackProjSpeed
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref float multiplier,
            ref float gravityCorrection,
            ref float randomOffset
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float multiplier,
            ref float gravityCorrection,
            ref float randomOffset
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_TownNPCAttackProjSpeed_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::TownNPCAttackProjSpeed")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::TownNPCAttackProjSpeed; use a flag to disable behavior.");
        }
    }

    public sealed partial class TownNPCAttackShoot
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref bool inBetweenShots
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref bool inBetweenShots
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_TownNPCAttackShoot_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::TownNPCAttackShoot")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::TownNPCAttackShoot; use a flag to disable behavior.");
        }
    }

    public sealed partial class TownNPCAttackMagic
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref float auraLightMultiplier
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float auraLightMultiplier
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_TownNPCAttackMagic_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::TownNPCAttackMagic")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::TownNPCAttackMagic; use a flag to disable behavior.");
        }
    }

    public sealed partial class TownNPCAttackSwing
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref int itemWidth,
            ref int itemHeight
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int itemWidth,
            ref int itemHeight
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_TownNPCAttackSwing_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::TownNPCAttackSwing")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::TownNPCAttackSwing; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawTownAttackGun
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref float scale,
            ref int horizontalHoldoutOffset
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref float scale,
            ref int horizontalHoldoutOffset
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_DrawTownAttackGun_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::DrawTownAttackGun")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::DrawTownAttackGun; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawTownAttackSwing
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref int itemSize,
            ref float scale,
            ref Microsoft.Xna.Framework.Vector2 offset
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref int itemSize,
            ref float scale,
            ref Microsoft.Xna.Framework.Vector2 offset
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_DrawTownAttackSwing_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::DrawTownAttackSwing")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::DrawTownAttackSwing; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyCollisionData
    {
        public delegate bool Original(
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Rectangle victimHitbox,
            ref int immunityCooldownSlot,
            ref Terraria.ModLoader.MultipliableFloat damageMultiplier,
            ref Microsoft.Xna.Framework.Rectangle npcHitbox
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Rectangle victimHitbox,
            ref int immunityCooldownSlot,
            ref Terraria.ModLoader.MultipliableFloat damageMultiplier,
            ref Microsoft.Xna.Framework.Rectangle npcHitbox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ModifyCollisionData_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ModifyCollisionData")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ModifyCollisionData; use a flag to disable behavior.");
        }
    }

    public sealed partial class NeedSaving
    {
        public delegate bool Original(
            Terraria.NPC npc
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_NeedSaving_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::NeedSaving")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::NeedSaving; use a flag to disable behavior.");
        }
    }

    public sealed partial class PickEmote
    {
        public delegate int? Original(
            Terraria.NPC npc,
            Terraria.Player closestPlayer,
            System.Collections.Generic.List<int> emoteList,
            Terraria.GameContent.UI.WorldUIAnchor otherAnchor
        );

        public delegate int? Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player closestPlayer,
            System.Collections.Generic.List<int> emoteList,
            Terraria.GameContent.UI.WorldUIAnchor otherAnchor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PickEmote_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PickEmote")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PickEmote; use a flag to disable behavior.");
        }
    }

    public sealed partial class ChatBubblePosition
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_ChatBubblePosition_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::ChatBubblePosition")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::ChatBubblePosition; use a flag to disable behavior.");
        }
    }

    public sealed partial class PartyHatPosition
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_PartyHatPosition_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::PartyHatPosition")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::PartyHatPosition; use a flag to disable behavior.");
        }
    }

    public sealed partial class EmoteBubblePosition
    {
        public delegate void Original(
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalNPC_EmoteBubblePosition_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalNPC::EmoteBubblePosition")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalNPC::EmoteBubblePosition; use a flag to disable behavior.");
        }
    }
}

public sealed partial class GlobalNPC_SetDefaultsFromNetId_Impl(GlobalNPCHooks.SetDefaultsFromNetId.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_SetDefaultsFromNetId_Impl(hook);
    }

    public override void SetDefaultsFromNetId(
        Terraria.NPC npc
    )
    {
        hook(
            (
                Terraria.NPC npc_captured
            ) => base.SetDefaultsFromNetId(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_OnSpawn_Impl(GlobalNPCHooks.OnSpawn.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnSpawn_Impl(hook);
    }

    public override void OnSpawn(
        Terraria.NPC npc,
        Terraria.DataStructures.IEntitySource source
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.DataStructures.IEntitySource source_captured
            ) => base.OnSpawn(
                npc_captured,
                source_captured
            ),
            this,
            npc,
            source
        );
    }
}

public sealed partial class GlobalNPC_ApplyDifficultyAndPlayerScaling_Impl(GlobalNPCHooks.ApplyDifficultyAndPlayerScaling.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ApplyDifficultyAndPlayerScaling_Impl(hook);
    }

    public override void ApplyDifficultyAndPlayerScaling(
        Terraria.NPC npc,
        int numPlayers,
        float balance,
        float bossAdjustment
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                int numPlayers_captured,
                float balance_captured,
                float bossAdjustment_captured
            ) => base.ApplyDifficultyAndPlayerScaling(
                npc_captured,
                numPlayers_captured,
                balance_captured,
                bossAdjustment_captured
            ),
            this,
            npc,
            numPlayers,
            balance,
            bossAdjustment
        );
    }
}

public sealed partial class GlobalNPC_SetBestiary_Impl(GlobalNPCHooks.SetBestiary.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_SetBestiary_Impl(hook);
    }

    public override void SetBestiary(
        Terraria.NPC npc,
        Terraria.GameContent.Bestiary.BestiaryDatabase database,
        Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.GameContent.Bestiary.BestiaryDatabase database_captured,
                Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry_captured
            ) => base.SetBestiary(
                npc_captured,
                database_captured,
                bestiaryEntry_captured
            ),
            this,
            npc,
            database,
            bestiaryEntry
        );
    }
}

public sealed partial class GlobalNPC_ModifyTypeName_Impl(GlobalNPCHooks.ModifyTypeName.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyTypeName_Impl(hook);
    }

    public override void ModifyTypeName(
        Terraria.NPC npc,
        ref string typeName
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref string typeName_captured
            ) => base.ModifyTypeName(
                npc_captured,
                ref typeName_captured
            ),
            this,
            npc,
            ref typeName
        );
    }
}

public sealed partial class GlobalNPC_ModifyHoverBoundingBox_Impl(GlobalNPCHooks.ModifyHoverBoundingBox.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyHoverBoundingBox_Impl(hook);
    }

    public override void ModifyHoverBoundingBox(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Rectangle boundingBox
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Rectangle boundingBox_captured
            ) => base.ModifyHoverBoundingBox(
                npc_captured,
                ref boundingBox_captured
            ),
            this,
            npc,
            ref boundingBox
        );
    }
}

public sealed partial class GlobalNPC_PreHoverInteract_Impl(GlobalNPCHooks.PreHoverInteract.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PreHoverInteract_Impl(hook);
    }

    public override bool PreHoverInteract(
        Terraria.NPC npc,
        bool mouseIntersects
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                bool mouseIntersects_captured
            ) => base.PreHoverInteract(
                npc_captured,
                mouseIntersects_captured
            ),
            this,
            npc,
            mouseIntersects
        );
    }
}

public sealed partial class GlobalNPC_ModifyTownNPCProfile_Impl(GlobalNPCHooks.ModifyTownNPCProfile.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyTownNPCProfile_Impl(hook);
    }

    public override Terraria.GameContent.ITownNPCProfile ModifyTownNPCProfile(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.ModifyTownNPCProfile(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_ModifyNPCNameList_Impl(GlobalNPCHooks.ModifyNPCNameList.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyNPCNameList_Impl(hook);
    }

    public override void ModifyNPCNameList(
        Terraria.NPC npc,
        System.Collections.Generic.List<string> nameList
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                System.Collections.Generic.List<string> nameList_captured
            ) => base.ModifyNPCNameList(
                npc_captured,
                nameList_captured
            ),
            this,
            npc,
            nameList
        );
    }
}

public sealed partial class GlobalNPC_ResetEffects_Impl(GlobalNPCHooks.ResetEffects.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ResetEffects_Impl(hook);
    }

    public override void ResetEffects(
        Terraria.NPC npc
    )
    {
        hook(
            (
                Terraria.NPC npc_captured
            ) => base.ResetEffects(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_PreAI_Impl(GlobalNPCHooks.PreAI.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PreAI_Impl(hook);
    }

    public override bool PreAI(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.PreAI(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_AI_Impl(GlobalNPCHooks.AI.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_AI_Impl(hook);
    }

    public override void AI(
        Terraria.NPC npc
    )
    {
        hook(
            (
                Terraria.NPC npc_captured
            ) => base.AI(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_PostAI_Impl(GlobalNPCHooks.PostAI.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PostAI_Impl(hook);
    }

    public override void PostAI(
        Terraria.NPC npc
    )
    {
        hook(
            (
                Terraria.NPC npc_captured
            ) => base.PostAI(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_FindFrame_Impl(GlobalNPCHooks.FindFrame.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_FindFrame_Impl(hook);
    }

    public override void FindFrame(
        Terraria.NPC npc,
        int frameHeight
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                int frameHeight_captured
            ) => base.FindFrame(
                npc_captured,
                frameHeight_captured
            ),
            this,
            npc,
            frameHeight
        );
    }
}

public sealed partial class GlobalNPC_HitEffect_Impl(GlobalNPCHooks.HitEffect.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_HitEffect_Impl(hook);
    }

    public override void HitEffect(
        Terraria.NPC npc,
        Terraria.NPC.HitInfo hit
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.NPC.HitInfo hit_captured
            ) => base.HitEffect(
                npc_captured,
                hit_captured
            ),
            this,
            npc,
            hit
        );
    }
}

public sealed partial class GlobalNPC_UpdateLifeRegen_Impl(GlobalNPCHooks.UpdateLifeRegen.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_UpdateLifeRegen_Impl(hook);
    }

    public override void UpdateLifeRegen(
        Terraria.NPC npc,
        ref int damage
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref int damage_captured
            ) => base.UpdateLifeRegen(
                npc_captured,
                ref damage_captured
            ),
            this,
            npc,
            ref damage
        );
    }
}

public sealed partial class GlobalNPC_CheckActive_Impl(GlobalNPCHooks.CheckActive.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CheckActive_Impl(hook);
    }

    public override bool CheckActive(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.CheckActive(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_CheckDead_Impl(GlobalNPCHooks.CheckDead.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CheckDead_Impl(hook);
    }

    public override bool CheckDead(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.CheckDead(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_SpecialOnKill_Impl(GlobalNPCHooks.SpecialOnKill.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_SpecialOnKill_Impl(hook);
    }

    public override bool SpecialOnKill(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.SpecialOnKill(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_PreKill_Impl(GlobalNPCHooks.PreKill.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PreKill_Impl(hook);
    }

    public override bool PreKill(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.PreKill(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_OnKill_Impl(GlobalNPCHooks.OnKill.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnKill_Impl(hook);
    }

    public override void OnKill(
        Terraria.NPC npc
    )
    {
        hook(
            (
                Terraria.NPC npc_captured
            ) => base.OnKill(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_CanFallThroughPlatforms_Impl(GlobalNPCHooks.CanFallThroughPlatforms.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanFallThroughPlatforms_Impl(hook);
    }

    public override bool? CanFallThroughPlatforms(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.CanFallThroughPlatforms(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_CanBeCaughtBy_Impl(GlobalNPCHooks.CanBeCaughtBy.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanBeCaughtBy_Impl(hook);
    }

    public override bool? CanBeCaughtBy(
        Terraria.NPC npc,
        Terraria.Item item,
        Terraria.Player player
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Item item_captured,
                Terraria.Player player_captured
            ) => base.CanBeCaughtBy(
                npc_captured,
                item_captured,
                player_captured
            ),
            this,
            npc,
            item,
            player
        );
    }
}

public sealed partial class GlobalNPC_OnCaughtBy_Impl(GlobalNPCHooks.OnCaughtBy.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnCaughtBy_Impl(hook);
    }

    public override void OnCaughtBy(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item,
        bool failed
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player player_captured,
                Terraria.Item item_captured,
                bool failed_captured
            ) => base.OnCaughtBy(
                npc_captured,
                player_captured,
                item_captured,
                failed_captured
            ),
            this,
            npc,
            player,
            item,
            failed
        );
    }
}

public sealed partial class GlobalNPC_ModifyNPCLoot_Impl(GlobalNPCHooks.ModifyNPCLoot.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyNPCLoot_Impl(hook);
    }

    public override void ModifyNPCLoot(
        Terraria.NPC npc,
        Terraria.ModLoader.NPCLoot npcLoot
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.ModLoader.NPCLoot npcLoot_captured
            ) => base.ModifyNPCLoot(
                npc_captured,
                npcLoot_captured
            ),
            this,
            npc,
            npcLoot
        );
    }
}

public sealed partial class GlobalNPC_ModifyGlobalLoot_Impl(GlobalNPCHooks.ModifyGlobalLoot.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyGlobalLoot_Impl(hook);
    }

    public override void ModifyGlobalLoot(
        Terraria.ModLoader.GlobalLoot globalLoot
    )
    {
        hook(
            (
                Terraria.ModLoader.GlobalLoot globalLoot_captured
            ) => base.ModifyGlobalLoot(
                globalLoot_captured
            ),
            this,
            globalLoot
        );
    }
}

public sealed partial class GlobalNPC_CanHitPlayer_Impl(GlobalNPCHooks.CanHitPlayer.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanHitPlayer_Impl(hook);
    }

    public override bool CanHitPlayer(
        Terraria.NPC npc,
        Terraria.Player target,
        ref int cooldownSlot
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player target_captured,
                ref int cooldownSlot_captured
            ) => base.CanHitPlayer(
                npc_captured,
                target_captured,
                ref cooldownSlot_captured
            ),
            this,
            npc,
            target,
            ref cooldownSlot
        );
    }
}

public sealed partial class GlobalNPC_ModifyHitPlayer_Impl(GlobalNPCHooks.ModifyHitPlayer.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyHitPlayer_Impl(hook);
    }

    public override void ModifyHitPlayer(
        Terraria.NPC npc,
        Terraria.Player target,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player target_captured,
                ref Terraria.Player.HurtModifiers modifiers_captured
            ) => base.ModifyHitPlayer(
                npc_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            npc,
            target,
            ref modifiers
        );
    }
}

public sealed partial class GlobalNPC_OnHitPlayer_Impl(GlobalNPCHooks.OnHitPlayer.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnHitPlayer_Impl(hook);
    }

    public override void OnHitPlayer(
        Terraria.NPC npc,
        Terraria.Player target,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player target_captured,
                Terraria.Player.HurtInfo hurtInfo_captured
            ) => base.OnHitPlayer(
                npc_captured,
                target_captured,
                hurtInfo_captured
            ),
            this,
            npc,
            target,
            hurtInfo
        );
    }
}

public sealed partial class GlobalNPC_CanHitNPC_Impl(GlobalNPCHooks.CanHitNPC.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanHitNPC_Impl(hook);
    }

    public override bool CanHitNPC(
        Terraria.NPC npc,
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.NPC target_captured
            ) => base.CanHitNPC(
                npc_captured,
                target_captured
            ),
            this,
            npc,
            target
        );
    }
}

public sealed partial class GlobalNPC_CanBeHitByNPC_Impl(GlobalNPCHooks.CanBeHitByNPC.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanBeHitByNPC_Impl(hook);
    }

    public override bool CanBeHitByNPC(
        Terraria.NPC npc,
        Terraria.NPC attacker
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.NPC attacker_captured
            ) => base.CanBeHitByNPC(
                npc_captured,
                attacker_captured
            ),
            this,
            npc,
            attacker
        );
    }
}

public sealed partial class GlobalNPC_ModifyHitNPC_Impl(GlobalNPCHooks.ModifyHitNPC.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyHitNPC_Impl(hook);
    }

    public override void ModifyHitNPC(
        Terraria.NPC npc,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.NPC target_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitNPC(
                npc_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            npc,
            target,
            ref modifiers
        );
    }
}

public sealed partial class GlobalNPC_OnHitNPC_Impl(GlobalNPCHooks.OnHitNPC.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnHitNPC_Impl(hook);
    }

    public override void OnHitNPC(
        Terraria.NPC npc,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.NPC target_captured,
                Terraria.NPC.HitInfo hit_captured
            ) => base.OnHitNPC(
                npc_captured,
                target_captured,
                hit_captured
            ),
            this,
            npc,
            target,
            hit
        );
    }
}

public sealed partial class GlobalNPC_CanBeHitByItem_Impl(GlobalNPCHooks.CanBeHitByItem.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanBeHitByItem_Impl(hook);
    }

    public override bool? CanBeHitByItem(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player player_captured,
                Terraria.Item item_captured
            ) => base.CanBeHitByItem(
                npc_captured,
                player_captured,
                item_captured
            ),
            this,
            npc,
            player,
            item
        );
    }
}

public sealed partial class GlobalNPC_CanCollideWithPlayerMeleeAttack_Impl(GlobalNPCHooks.CanCollideWithPlayerMeleeAttack.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanCollideWithPlayerMeleeAttack_Impl(hook);
    }

    public override bool? CanCollideWithPlayerMeleeAttack(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item,
        Microsoft.Xna.Framework.Rectangle meleeAttackHitbox
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player player_captured,
                Terraria.Item item_captured,
                Microsoft.Xna.Framework.Rectangle meleeAttackHitbox_captured
            ) => base.CanCollideWithPlayerMeleeAttack(
                npc_captured,
                player_captured,
                item_captured,
                meleeAttackHitbox_captured
            ),
            this,
            npc,
            player,
            item,
            meleeAttackHitbox
        );
    }
}

public sealed partial class GlobalNPC_ModifyHitByItem_Impl(GlobalNPCHooks.ModifyHitByItem.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyHitByItem_Impl(hook);
    }

    public override void ModifyHitByItem(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player player_captured,
                Terraria.Item item_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitByItem(
                npc_captured,
                player_captured,
                item_captured,
                ref modifiers_captured
            ),
            this,
            npc,
            player,
            item,
            ref modifiers
        );
    }
}

public sealed partial class GlobalNPC_OnHitByItem_Impl(GlobalNPCHooks.OnHitByItem.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnHitByItem_Impl(hook);
    }

    public override void OnHitByItem(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player player_captured,
                Terraria.Item item_captured,
                Terraria.NPC.HitInfo hit_captured,
                int damageDone_captured
            ) => base.OnHitByItem(
                npc_captured,
                player_captured,
                item_captured,
                hit_captured,
                damageDone_captured
            ),
            this,
            npc,
            player,
            item,
            hit,
            damageDone
        );
    }
}

public sealed partial class GlobalNPC_CanBeHitByProjectile_Impl(GlobalNPCHooks.CanBeHitByProjectile.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanBeHitByProjectile_Impl(hook);
    }

    public override bool? CanBeHitByProjectile(
        Terraria.NPC npc,
        Terraria.Projectile projectile
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Projectile projectile_captured
            ) => base.CanBeHitByProjectile(
                npc_captured,
                projectile_captured
            ),
            this,
            npc,
            projectile
        );
    }
}

public sealed partial class GlobalNPC_ModifyHitByProjectile_Impl(GlobalNPCHooks.ModifyHitByProjectile.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyHitByProjectile_Impl(hook);
    }

    public override void ModifyHitByProjectile(
        Terraria.NPC npc,
        Terraria.Projectile projectile,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Projectile projectile_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitByProjectile(
                npc_captured,
                projectile_captured,
                ref modifiers_captured
            ),
            this,
            npc,
            projectile,
            ref modifiers
        );
    }
}

public sealed partial class GlobalNPC_OnHitByProjectile_Impl(GlobalNPCHooks.OnHitByProjectile.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnHitByProjectile_Impl(hook);
    }

    public override void OnHitByProjectile(
        Terraria.NPC npc,
        Terraria.Projectile projectile,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Projectile projectile_captured,
                Terraria.NPC.HitInfo hit_captured,
                int damageDone_captured
            ) => base.OnHitByProjectile(
                npc_captured,
                projectile_captured,
                hit_captured,
                damageDone_captured
            ),
            this,
            npc,
            projectile,
            hit,
            damageDone
        );
    }
}

public sealed partial class GlobalNPC_ModifyIncomingHit_Impl(GlobalNPCHooks.ModifyIncomingHit.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyIncomingHit_Impl(hook);
    }

    public override void ModifyIncomingHit(
        Terraria.NPC npc,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyIncomingHit(
                npc_captured,
                ref modifiers_captured
            ),
            this,
            npc,
            ref modifiers
        );
    }
}

public sealed partial class GlobalNPC_BossHeadSlot_Impl(GlobalNPCHooks.BossHeadSlot.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_BossHeadSlot_Impl(hook);
    }

    public override void BossHeadSlot(
        Terraria.NPC npc,
        ref int index
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref int index_captured
            ) => base.BossHeadSlot(
                npc_captured,
                ref index_captured
            ),
            this,
            npc,
            ref index
        );
    }
}

public sealed partial class GlobalNPC_BossHeadRotation_Impl(GlobalNPCHooks.BossHeadRotation.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_BossHeadRotation_Impl(hook);
    }

    public override void BossHeadRotation(
        Terraria.NPC npc,
        ref float rotation
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref float rotation_captured
            ) => base.BossHeadRotation(
                npc_captured,
                ref rotation_captured
            ),
            this,
            npc,
            ref rotation
        );
    }
}

public sealed partial class GlobalNPC_BossHeadSpriteEffects_Impl(GlobalNPCHooks.BossHeadSpriteEffects.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_BossHeadSpriteEffects_Impl(hook);
    }

    public override void BossHeadSpriteEffects(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.BossHeadSpriteEffects(
                npc_captured,
                ref spriteEffects_captured
            ),
            this,
            npc,
            ref spriteEffects
        );
    }
}

public sealed partial class GlobalNPC_GetAlpha_Impl(GlobalNPCHooks.GetAlpha.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_GetAlpha_Impl(hook);
    }

    public override Microsoft.Xna.Framework.Color? GetAlpha(
        Terraria.NPC npc,
        Microsoft.Xna.Framework.Color drawColor
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Microsoft.Xna.Framework.Color drawColor_captured
            ) => base.GetAlpha(
                npc_captured,
                drawColor_captured
            ),
            this,
            npc,
            drawColor
        );
    }
}

public sealed partial class GlobalNPC_DrawEffects_Impl(GlobalNPCHooks.DrawEffects.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_DrawEffects_Impl(hook);
    }

    public override void DrawEffects(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Color drawColor
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Color drawColor_captured
            ) => base.DrawEffects(
                npc_captured,
                ref drawColor_captured
            ),
            this,
            npc,
            ref drawColor
        );
    }
}

public sealed partial class GlobalNPC_PreDraw_Impl(GlobalNPCHooks.PreDraw.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PreDraw_Impl(hook);
    }

    public override bool PreDraw(
        Terraria.NPC npc,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Vector2 screenPos,
        Microsoft.Xna.Framework.Color drawColor
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Vector2 screenPos_captured,
                Microsoft.Xna.Framework.Color drawColor_captured
            ) => base.PreDraw(
                npc_captured,
                spriteBatch_captured,
                screenPos_captured,
                drawColor_captured
            ),
            this,
            npc,
            spriteBatch,
            screenPos,
            drawColor
        );
    }
}

public sealed partial class GlobalNPC_PostDraw_Impl(GlobalNPCHooks.PostDraw.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PostDraw_Impl(hook);
    }

    public override void PostDraw(
        Terraria.NPC npc,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Vector2 screenPos,
        Microsoft.Xna.Framework.Color drawColor
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Vector2 screenPos_captured,
                Microsoft.Xna.Framework.Color drawColor_captured
            ) => base.PostDraw(
                npc_captured,
                spriteBatch_captured,
                screenPos_captured,
                drawColor_captured
            ),
            this,
            npc,
            spriteBatch,
            screenPos,
            drawColor
        );
    }
}

public sealed partial class GlobalNPC_DrawBehind_Impl(GlobalNPCHooks.DrawBehind.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_DrawBehind_Impl(hook);
    }

    public override void DrawBehind(
        Terraria.NPC npc,
        int index
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                int index_captured
            ) => base.DrawBehind(
                npc_captured,
                index_captured
            ),
            this,
            npc,
            index
        );
    }
}

public sealed partial class GlobalNPC_DrawHealthBar_Impl(GlobalNPCHooks.DrawHealthBar.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_DrawHealthBar_Impl(hook);
    }

    public override bool? DrawHealthBar(
        Terraria.NPC npc,
        byte hbPosition,
        ref float scale,
        ref Microsoft.Xna.Framework.Vector2 position
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                byte hbPosition_captured,
                ref float scale_captured,
                ref Microsoft.Xna.Framework.Vector2 position_captured
            ) => base.DrawHealthBar(
                npc_captured,
                hbPosition_captured,
                ref scale_captured,
                ref position_captured
            ),
            this,
            npc,
            hbPosition,
            ref scale,
            ref position
        );
    }
}

public sealed partial class GlobalNPC_EditSpawnRate_Impl(GlobalNPCHooks.EditSpawnRate.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_EditSpawnRate_Impl(hook);
    }

    public override void EditSpawnRate(
        Terraria.Player player,
        ref int spawnRate,
        ref int maxSpawns
    )
    {
        hook(
            (
                Terraria.Player player_captured,
                ref int spawnRate_captured,
                ref int maxSpawns_captured
            ) => base.EditSpawnRate(
                player_captured,
                ref spawnRate_captured,
                ref maxSpawns_captured
            ),
            this,
            player,
            ref spawnRate,
            ref maxSpawns
        );
    }
}

public sealed partial class GlobalNPC_EditSpawnRange_Impl(GlobalNPCHooks.EditSpawnRange.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_EditSpawnRange_Impl(hook);
    }

    public override void EditSpawnRange(
        Terraria.Player player,
        ref int spawnRangeX,
        ref int spawnRangeY,
        ref int safeRangeX,
        ref int safeRangeY
    )
    {
        hook(
            (
                Terraria.Player player_captured,
                ref int spawnRangeX_captured,
                ref int spawnRangeY_captured,
                ref int safeRangeX_captured,
                ref int safeRangeY_captured
            ) => base.EditSpawnRange(
                player_captured,
                ref spawnRangeX_captured,
                ref spawnRangeY_captured,
                ref safeRangeX_captured,
                ref safeRangeY_captured
            ),
            this,
            player,
            ref spawnRangeX,
            ref spawnRangeY,
            ref safeRangeX,
            ref safeRangeY
        );
    }
}

public sealed partial class GlobalNPC_EditSpawnPool_Impl(GlobalNPCHooks.EditSpawnPool.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_EditSpawnPool_Impl(hook);
    }

    public override void EditSpawnPool(
        System.Collections.Generic.IDictionary<int, float> pool,
        Terraria.ModLoader.NPCSpawnInfo spawnInfo
    )
    {
        hook(
            (
                System.Collections.Generic.IDictionary<int, float> pool_captured,
                Terraria.ModLoader.NPCSpawnInfo spawnInfo_captured
            ) => base.EditSpawnPool(
                pool_captured,
                spawnInfo_captured
            ),
            this,
            pool,
            spawnInfo
        );
    }
}

public sealed partial class GlobalNPC_SpawnNPC_Impl(GlobalNPCHooks.SpawnNPC.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_SpawnNPC_Impl(hook);
    }

    public override void SpawnNPC(
        int npc,
        int tileX,
        int tileY
    )
    {
        hook(
            (
                int npc_captured,
                int tileX_captured,
                int tileY_captured
            ) => base.SpawnNPC(
                npc_captured,
                tileX_captured,
                tileY_captured
            ),
            this,
            npc,
            tileX,
            tileY
        );
    }
}

public sealed partial class GlobalNPC_CanChat_Impl(GlobalNPCHooks.CanChat.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanChat_Impl(hook);
    }

    public override bool? CanChat(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.CanChat(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_GetChat_Impl(GlobalNPCHooks.GetChat.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_GetChat_Impl(hook);
    }

    public override void GetChat(
        Terraria.NPC npc,
        ref string chat
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref string chat_captured
            ) => base.GetChat(
                npc_captured,
                ref chat_captured
            ),
            this,
            npc,
            ref chat
        );
    }
}

public sealed partial class GlobalNPC_PreChatButtonClicked_Impl(GlobalNPCHooks.PreChatButtonClicked.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PreChatButtonClicked_Impl(hook);
    }

    public override bool PreChatButtonClicked(
        Terraria.NPC npc,
        bool firstButton
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                bool firstButton_captured
            ) => base.PreChatButtonClicked(
                npc_captured,
                firstButton_captured
            ),
            this,
            npc,
            firstButton
        );
    }
}

public sealed partial class GlobalNPC_OnChatButtonClicked_Impl(GlobalNPCHooks.OnChatButtonClicked.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnChatButtonClicked_Impl(hook);
    }

    public override void OnChatButtonClicked(
        Terraria.NPC npc,
        bool firstButton
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                bool firstButton_captured
            ) => base.OnChatButtonClicked(
                npc_captured,
                firstButton_captured
            ),
            this,
            npc,
            firstButton
        );
    }
}

public sealed partial class GlobalNPC_ModifyShop_Impl(GlobalNPCHooks.ModifyShop.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyShop_Impl(hook);
    }

    public override void ModifyShop(
        Terraria.ModLoader.NPCShop shop
    )
    {
        hook(
            (
                Terraria.ModLoader.NPCShop shop_captured
            ) => base.ModifyShop(
                shop_captured
            ),
            this,
            shop
        );
    }
}

public sealed partial class GlobalNPC_ModifyActiveShop_Impl(GlobalNPCHooks.ModifyActiveShop.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyActiveShop_Impl(hook);
    }

    public override void ModifyActiveShop(
        Terraria.NPC npc,
        string shopName,
        Terraria.Item[] items
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                string shopName_captured,
                Terraria.Item[] items_captured
            ) => base.ModifyActiveShop(
                npc_captured,
                shopName_captured,
                items_captured
            ),
            this,
            npc,
            shopName,
            items
        );
    }
}

public sealed partial class GlobalNPC_SetupTravelShop_Impl(GlobalNPCHooks.SetupTravelShop.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_SetupTravelShop_Impl(hook);
    }

    public override void SetupTravelShop(
        int[] shop,
        ref int nextSlot
    )
    {
        hook(
            (
                int[] shop_captured,
                ref int nextSlot_captured
            ) => base.SetupTravelShop(
                shop_captured,
                ref nextSlot_captured
            ),
            this,
            shop,
            ref nextSlot
        );
    }
}

public sealed partial class GlobalNPC_CanGoToStatue_Impl(GlobalNPCHooks.CanGoToStatue.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_CanGoToStatue_Impl(hook);
    }

    public override bool? CanGoToStatue(
        Terraria.NPC npc,
        bool toKingStatue
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                bool toKingStatue_captured
            ) => base.CanGoToStatue(
                npc_captured,
                toKingStatue_captured
            ),
            this,
            npc,
            toKingStatue
        );
    }
}

public sealed partial class GlobalNPC_OnGoToStatue_Impl(GlobalNPCHooks.OnGoToStatue.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_OnGoToStatue_Impl(hook);
    }

    public override void OnGoToStatue(
        Terraria.NPC npc,
        bool toKingStatue
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                bool toKingStatue_captured
            ) => base.OnGoToStatue(
                npc_captured,
                toKingStatue_captured
            ),
            this,
            npc,
            toKingStatue
        );
    }
}

public sealed partial class GlobalNPC_BuffTownNPC_Impl(GlobalNPCHooks.BuffTownNPC.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_BuffTownNPC_Impl(hook);
    }

    public override void BuffTownNPC(
        ref float damageMult,
        ref int defense
    )
    {
        hook(
            (
                ref float damageMult_captured,
                ref int defense_captured
            ) => base.BuffTownNPC(
                ref damageMult_captured,
                ref defense_captured
            ),
            this,
            ref damageMult,
            ref defense
        );
    }
}

public sealed partial class GlobalNPC_ModifyDeathMessage_Impl(GlobalNPCHooks.ModifyDeathMessage.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyDeathMessage_Impl(hook);
    }

    public override bool ModifyDeathMessage(
        Terraria.NPC npc,
        ref Terraria.Localization.NetworkText customText,
        ref Microsoft.Xna.Framework.Color color
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                ref Terraria.Localization.NetworkText customText_captured,
                ref Microsoft.Xna.Framework.Color color_captured
            ) => base.ModifyDeathMessage(
                npc_captured,
                ref customText_captured,
                ref color_captured
            ),
            this,
            npc,
            ref customText,
            ref color
        );
    }
}

public sealed partial class GlobalNPC_TownNPCAttackStrength_Impl(GlobalNPCHooks.TownNPCAttackStrength.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_TownNPCAttackStrength_Impl(hook);
    }

    public override void TownNPCAttackStrength(
        Terraria.NPC npc,
        ref int damage,
        ref float knockback
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref int damage_captured,
                ref float knockback_captured
            ) => base.TownNPCAttackStrength(
                npc_captured,
                ref damage_captured,
                ref knockback_captured
            ),
            this,
            npc,
            ref damage,
            ref knockback
        );
    }
}

public sealed partial class GlobalNPC_TownNPCAttackCooldown_Impl(GlobalNPCHooks.TownNPCAttackCooldown.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_TownNPCAttackCooldown_Impl(hook);
    }

    public override void TownNPCAttackCooldown(
        Terraria.NPC npc,
        ref int cooldown,
        ref int randExtraCooldown
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref int cooldown_captured,
                ref int randExtraCooldown_captured
            ) => base.TownNPCAttackCooldown(
                npc_captured,
                ref cooldown_captured,
                ref randExtraCooldown_captured
            ),
            this,
            npc,
            ref cooldown,
            ref randExtraCooldown
        );
    }
}

public sealed partial class GlobalNPC_TownNPCAttackProj_Impl(GlobalNPCHooks.TownNPCAttackProj.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_TownNPCAttackProj_Impl(hook);
    }

    public override void TownNPCAttackProj(
        Terraria.NPC npc,
        ref int projType,
        ref int attackDelay
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref int projType_captured,
                ref int attackDelay_captured
            ) => base.TownNPCAttackProj(
                npc_captured,
                ref projType_captured,
                ref attackDelay_captured
            ),
            this,
            npc,
            ref projType,
            ref attackDelay
        );
    }
}

public sealed partial class GlobalNPC_TownNPCAttackProjSpeed_Impl(GlobalNPCHooks.TownNPCAttackProjSpeed.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_TownNPCAttackProjSpeed_Impl(hook);
    }

    public override void TownNPCAttackProjSpeed(
        Terraria.NPC npc,
        ref float multiplier,
        ref float gravityCorrection,
        ref float randomOffset
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref float multiplier_captured,
                ref float gravityCorrection_captured,
                ref float randomOffset_captured
            ) => base.TownNPCAttackProjSpeed(
                npc_captured,
                ref multiplier_captured,
                ref gravityCorrection_captured,
                ref randomOffset_captured
            ),
            this,
            npc,
            ref multiplier,
            ref gravityCorrection,
            ref randomOffset
        );
    }
}

public sealed partial class GlobalNPC_TownNPCAttackShoot_Impl(GlobalNPCHooks.TownNPCAttackShoot.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_TownNPCAttackShoot_Impl(hook);
    }

    public override void TownNPCAttackShoot(
        Terraria.NPC npc,
        ref bool inBetweenShots
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref bool inBetweenShots_captured
            ) => base.TownNPCAttackShoot(
                npc_captured,
                ref inBetweenShots_captured
            ),
            this,
            npc,
            ref inBetweenShots
        );
    }
}

public sealed partial class GlobalNPC_TownNPCAttackMagic_Impl(GlobalNPCHooks.TownNPCAttackMagic.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_TownNPCAttackMagic_Impl(hook);
    }

    public override void TownNPCAttackMagic(
        Terraria.NPC npc,
        ref float auraLightMultiplier
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref float auraLightMultiplier_captured
            ) => base.TownNPCAttackMagic(
                npc_captured,
                ref auraLightMultiplier_captured
            ),
            this,
            npc,
            ref auraLightMultiplier
        );
    }
}

public sealed partial class GlobalNPC_TownNPCAttackSwing_Impl(GlobalNPCHooks.TownNPCAttackSwing.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_TownNPCAttackSwing_Impl(hook);
    }

    public override void TownNPCAttackSwing(
        Terraria.NPC npc,
        ref int itemWidth,
        ref int itemHeight
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref int itemWidth_captured,
                ref int itemHeight_captured
            ) => base.TownNPCAttackSwing(
                npc_captured,
                ref itemWidth_captured,
                ref itemHeight_captured
            ),
            this,
            npc,
            ref itemWidth,
            ref itemHeight
        );
    }
}

public sealed partial class GlobalNPC_DrawTownAttackGun_Impl(GlobalNPCHooks.DrawTownAttackGun.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_DrawTownAttackGun_Impl(hook);
    }

    public override void DrawTownAttackGun(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Graphics.Texture2D item,
        ref Microsoft.Xna.Framework.Rectangle itemFrame,
        ref float scale,
        ref int horizontalHoldoutOffset
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Graphics.Texture2D item_captured,
                ref Microsoft.Xna.Framework.Rectangle itemFrame_captured,
                ref float scale_captured,
                ref int horizontalHoldoutOffset_captured
            ) => base.DrawTownAttackGun(
                npc_captured,
                ref item_captured,
                ref itemFrame_captured,
                ref scale_captured,
                ref horizontalHoldoutOffset_captured
            ),
            this,
            npc,
            ref item,
            ref itemFrame,
            ref scale,
            ref horizontalHoldoutOffset
        );
    }
}

public sealed partial class GlobalNPC_DrawTownAttackSwing_Impl(GlobalNPCHooks.DrawTownAttackSwing.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_DrawTownAttackSwing_Impl(hook);
    }

    public override void DrawTownAttackSwing(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Graphics.Texture2D item,
        ref Microsoft.Xna.Framework.Rectangle itemFrame,
        ref int itemSize,
        ref float scale,
        ref Microsoft.Xna.Framework.Vector2 offset
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Graphics.Texture2D item_captured,
                ref Microsoft.Xna.Framework.Rectangle itemFrame_captured,
                ref int itemSize_captured,
                ref float scale_captured,
                ref Microsoft.Xna.Framework.Vector2 offset_captured
            ) => base.DrawTownAttackSwing(
                npc_captured,
                ref item_captured,
                ref itemFrame_captured,
                ref itemSize_captured,
                ref scale_captured,
                ref offset_captured
            ),
            this,
            npc,
            ref item,
            ref itemFrame,
            ref itemSize,
            ref scale,
            ref offset
        );
    }
}

public sealed partial class GlobalNPC_ModifyCollisionData_Impl(GlobalNPCHooks.ModifyCollisionData.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ModifyCollisionData_Impl(hook);
    }

    public override bool ModifyCollisionData(
        Terraria.NPC npc,
        Microsoft.Xna.Framework.Rectangle victimHitbox,
        ref int immunityCooldownSlot,
        ref Terraria.ModLoader.MultipliableFloat damageMultiplier,
        ref Microsoft.Xna.Framework.Rectangle npcHitbox
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Microsoft.Xna.Framework.Rectangle victimHitbox_captured,
                ref int immunityCooldownSlot_captured,
                ref Terraria.ModLoader.MultipliableFloat damageMultiplier_captured,
                ref Microsoft.Xna.Framework.Rectangle npcHitbox_captured
            ) => base.ModifyCollisionData(
                npc_captured,
                victimHitbox_captured,
                ref immunityCooldownSlot_captured,
                ref damageMultiplier_captured,
                ref npcHitbox_captured
            ),
            this,
            npc,
            victimHitbox,
            ref immunityCooldownSlot,
            ref damageMultiplier,
            ref npcHitbox
        );
    }
}

public sealed partial class GlobalNPC_NeedSaving_Impl(GlobalNPCHooks.NeedSaving.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_NeedSaving_Impl(hook);
    }

    public override bool NeedSaving(
        Terraria.NPC npc
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured
            ) => base.NeedSaving(
                npc_captured
            ),
            this,
            npc
        );
    }
}

public sealed partial class GlobalNPC_PickEmote_Impl(GlobalNPCHooks.PickEmote.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PickEmote_Impl(hook);
    }

    public override int? PickEmote(
        Terraria.NPC npc,
        Terraria.Player closestPlayer,
        System.Collections.Generic.List<int> emoteList,
        Terraria.GameContent.UI.WorldUIAnchor otherAnchor
    )
    {
        return hook(
            (
                Terraria.NPC npc_captured,
                Terraria.Player closestPlayer_captured,
                System.Collections.Generic.List<int> emoteList_captured,
                Terraria.GameContent.UI.WorldUIAnchor otherAnchor_captured
            ) => base.PickEmote(
                npc_captured,
                closestPlayer_captured,
                emoteList_captured,
                otherAnchor_captured
            ),
            this,
            npc,
            closestPlayer,
            emoteList,
            otherAnchor
        );
    }
}

public sealed partial class GlobalNPC_ChatBubblePosition_Impl(GlobalNPCHooks.ChatBubblePosition.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_ChatBubblePosition_Impl(hook);
    }

    public override void ChatBubblePosition(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Vector2 position_captured,
                ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.ChatBubblePosition(
                npc_captured,
                ref position_captured,
                ref spriteEffects_captured
            ),
            this,
            npc,
            ref position,
            ref spriteEffects
        );
    }
}

public sealed partial class GlobalNPC_PartyHatPosition_Impl(GlobalNPCHooks.PartyHatPosition.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_PartyHatPosition_Impl(hook);
    }

    public override void PartyHatPosition(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Vector2 position_captured,
                ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.PartyHatPosition(
                npc_captured,
                ref position_captured,
                ref spriteEffects_captured
            ),
            this,
            npc,
            ref position,
            ref spriteEffects
        );
    }
}

public sealed partial class GlobalNPC_EmoteBubblePosition_Impl(GlobalNPCHooks.EmoteBubblePosition.Definition hook) : Terraria.ModLoader.GlobalNPC
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public override Terraria.ModLoader.GlobalNPC Clone(Terraria.NPC? from, Terraria.NPC to)
    {
        return new GlobalNPC_EmoteBubblePosition_Impl(hook);
    }

    public override void EmoteBubblePosition(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        hook(
            (
                Terraria.NPC npc_captured,
                ref Microsoft.Xna.Framework.Vector2 position_captured,
                ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.EmoteBubblePosition(
                npc_captured,
                ref position_captured,
                ref spriteEffects_captured
            ),
            this,
            npc,
            ref position,
            ref spriteEffects
        );
    }
}
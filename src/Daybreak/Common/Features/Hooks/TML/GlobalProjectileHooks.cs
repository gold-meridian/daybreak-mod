namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalProjectile':
//     System.Void Terraria.ModLoader.GlobalProjectile::OnSpawn(Terraria.Projectile,Terraria.DataStructures.IEntitySource)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::PreAI(Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalProjectile::AI(Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalProjectile::PostAI(Terraria.Projectile)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::ShouldUpdatePosition(Terraria.Projectile)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::TileCollideStyle(Terraria.Projectile,System.Int32&,System.Int32&,System.Boolean&,Microsoft.Xna.Framework.Vector2&)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::OnTileCollide(Terraria.Projectile,Microsoft.Xna.Framework.Vector2)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::PreKill(Terraria.Projectile,System.Int32)
//     System.Void Terraria.ModLoader.GlobalProjectile::OnKill(Terraria.Projectile,System.Int32)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalProjectile::CanCutTiles(Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalProjectile::CutTiles(Terraria.Projectile)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalProjectile::CanDamage(Terraria.Projectile)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::MinionContactDamage(Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalProjectile::ModifyDamageHitbox(Terraria.Projectile,Microsoft.Xna.Framework.Rectangle&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalProjectile::CanHitNPC(Terraria.Projectile,Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalProjectile::ModifyHitNPC(Terraria.Projectile,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalProjectile::OnHitNPC(Terraria.Projectile,Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::CanHitPvp(Terraria.Projectile,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::CanHitPlayer(Terraria.Projectile,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalProjectile::ModifyHitPlayer(Terraria.Projectile,Terraria.Player,Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.GlobalProjectile::OnHitPlayer(Terraria.Projectile,Terraria.Player,Terraria.Player/HurtInfo)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalProjectile::Colliding(Terraria.Projectile,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Rectangle)
//     System.Nullable`1<Microsoft.Xna.Framework.Color> Terraria.ModLoader.GlobalProjectile::GetAlpha(Terraria.Projectile,Microsoft.Xna.Framework.Color)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::PreDrawExtras(Terraria.Projectile)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::PreDraw(Terraria.Projectile,Microsoft.Xna.Framework.Color&)
//     System.Void Terraria.ModLoader.GlobalProjectile::PostDraw(Terraria.Projectile,Microsoft.Xna.Framework.Color)
//     System.Void Terraria.ModLoader.GlobalProjectile::DrawBehind(Terraria.Projectile,System.Int32,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalProjectile::CanUseGrapple(System.Int32,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalProjectile::UseGrapple(Terraria.Player,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalProjectile::NumGrappleHooks(Terraria.Projectile,Terraria.Player,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalProjectile::GrappleRetreatSpeed(Terraria.Projectile,Terraria.Player,System.Single&)
//     System.Void Terraria.ModLoader.GlobalProjectile::GrapplePullSpeed(Terraria.Projectile,Terraria.Player,System.Single&)
//     System.Void Terraria.ModLoader.GlobalProjectile::GrappleTargetPoint(Terraria.Projectile,Terraria.Player,System.Single&,System.Single&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalProjectile::GrappleCanLatchOnTo(Terraria.Projectile,Terraria.Player,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalProjectile::PrepareBombToBlow(Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalProjectile::EmitEnchantmentVisualsAt(Terraria.Projectile,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)
public static partial class GlobalProjectileHooks
{
    public sealed partial class OnSpawn
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.DataStructures.IEntitySource source
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.DataStructures.IEntitySource source
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_OnSpawn_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::OnSpawn")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::OnSpawn; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreAI
    {
        public delegate bool Original(
            Terraria.Projectile projectile
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_PreAI_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::PreAI")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::PreAI; use a flag to disable behavior.");
        }
    }

    public sealed partial class AI
    {
        public delegate void Original(
            Terraria.Projectile projectile
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_AI_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::AI")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::AI; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostAI
    {
        public delegate void Original(
            Terraria.Projectile projectile
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_PostAI_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::PostAI")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::PostAI; use a flag to disable behavior.");
        }
    }

    public sealed partial class ShouldUpdatePosition
    {
        public delegate bool Original(
            Terraria.Projectile projectile
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_ShouldUpdatePosition_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::ShouldUpdatePosition")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::ShouldUpdatePosition; use a flag to disable behavior.");
        }
    }

    public sealed partial class TileCollideStyle
    {
        public delegate bool Original(
            Terraria.Projectile projectile,
            ref int width,
            ref int height,
            ref bool fallThrough,
            ref Microsoft.Xna.Framework.Vector2 hitboxCenterFrac
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref int width,
            ref int height,
            ref bool fallThrough,
            ref Microsoft.Xna.Framework.Vector2 hitboxCenterFrac
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_TileCollideStyle_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::TileCollideStyle")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::TileCollideStyle; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnTileCollide
    {
        public delegate bool Original(
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 oldVelocity
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 oldVelocity
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_OnTileCollide_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::OnTileCollide")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::OnTileCollide; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreKill
    {
        public delegate bool Original(
            Terraria.Projectile projectile,
            int timeLeft
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int timeLeft
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_PreKill_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::PreKill")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::PreKill; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnKill
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            int timeLeft
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int timeLeft
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_OnKill_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::OnKill")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::OnKill; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanCutTiles
    {
        public delegate bool? Original(
            Terraria.Projectile projectile
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_CanCutTiles_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::CanCutTiles")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::CanCutTiles; use a flag to disable behavior.");
        }
    }

    public sealed partial class CutTiles
    {
        public delegate void Original(
            Terraria.Projectile projectile
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_CutTiles_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::CutTiles")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::CutTiles; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanDamage
    {
        public delegate bool? Original(
            Terraria.Projectile projectile
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_CanDamage_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::CanDamage")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::CanDamage; use a flag to disable behavior.");
        }
    }

    public sealed partial class MinionContactDamage
    {
        public delegate bool Original(
            Terraria.Projectile projectile
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_MinionContactDamage_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::MinionContactDamage")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::MinionContactDamage; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyDamageHitbox
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Rectangle hitbox
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Rectangle hitbox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_ModifyDamageHitbox_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::ModifyDamageHitbox")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::ModifyDamageHitbox; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitNPC
    {
        public delegate bool? Original(
            Terraria.Projectile projectile,
            Terraria.NPC target
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.NPC target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_CanHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::CanHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::CanHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitNPC
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_ModifyHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::ModifyHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::ModifyHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_OnHitNPC_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::OnHitNPC")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::OnHitNPC; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitPvp
    {
        public delegate bool Original(
            Terraria.Projectile projectile,
            Terraria.Player target
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_CanHitPvp_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::CanHitPvp")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::CanHitPvp; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanHitPlayer
    {
        public delegate bool Original(
            Terraria.Projectile projectile,
            Terraria.Player target
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_CanHitPlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::CanHitPlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::CanHitPlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHitPlayer
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_ModifyHitPlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::ModifyHitPlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::ModifyHitPlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnHitPlayer
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.Player target,
            Terraria.Player.HurtInfo info
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_OnHitPlayer_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::OnHitPlayer")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::OnHitPlayer; use a flag to disable behavior.");
        }
    }

    public sealed partial class Colliding
    {
        public delegate bool? Original(
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Rectangle projHitbox,
            Microsoft.Xna.Framework.Rectangle targetHitbox
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Rectangle projHitbox,
            Microsoft.Xna.Framework.Rectangle targetHitbox
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_Colliding_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::Colliding")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::Colliding; use a flag to disable behavior.");
        }
    }

    public sealed partial class GetAlpha
    {
        public delegate Microsoft.Xna.Framework.Color? Original(
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Color lightColor
        );

        public delegate Microsoft.Xna.Framework.Color? Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Color lightColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_GetAlpha_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::GetAlpha")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::GetAlpha; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDrawExtras
    {
        public delegate bool Original(
            Terraria.Projectile projectile
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_PreDrawExtras_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::PreDrawExtras")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::PreDrawExtras; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Original(
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Color lightColor
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Color lightColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_PreDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::PreDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::PreDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Color lightColor
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Color lightColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_PostDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::PostDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::PostDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawBehind
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            int index,
            System.Collections.Generic.List<int> behindNPCsAndTiles,
            System.Collections.Generic.List<int> behindNPCs,
            System.Collections.Generic.List<int> behindProjectiles,
            System.Collections.Generic.List<int> overPlayers,
            System.Collections.Generic.List<int> overWiresUI
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int index,
            System.Collections.Generic.List<int> behindNPCsAndTiles,
            System.Collections.Generic.List<int> behindNPCs,
            System.Collections.Generic.List<int> behindProjectiles,
            System.Collections.Generic.List<int> overPlayers,
            System.Collections.Generic.List<int> overWiresUI
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_DrawBehind_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::DrawBehind")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::DrawBehind; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanUseGrapple
    {
        public delegate bool? Original(
            int type,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            int type,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_CanUseGrapple_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::CanUseGrapple")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::CanUseGrapple; use a flag to disable behavior.");
        }
    }

    public sealed partial class UseGrapple
    {
        public delegate void Original(
            Terraria.Player player,
            ref int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Player player,
            ref int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_UseGrapple_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::UseGrapple")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::UseGrapple; use a flag to disable behavior.");
        }
    }

    public sealed partial class NumGrappleHooks
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref int numHooks
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref int numHooks
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_NumGrappleHooks_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::NumGrappleHooks")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::NumGrappleHooks; use a flag to disable behavior.");
        }
    }

    public sealed partial class GrappleRetreatSpeed
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_GrappleRetreatSpeed_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::GrappleRetreatSpeed")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::GrappleRetreatSpeed; use a flag to disable behavior.");
        }
    }

    public sealed partial class GrapplePullSpeed
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_GrapplePullSpeed_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::GrapplePullSpeed")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::GrapplePullSpeed; use a flag to disable behavior.");
        }
    }

    public sealed partial class GrappleTargetPoint
    {
        public delegate void Original(
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float grappleX,
            ref float grappleY
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float grappleX,
            ref float grappleY
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_GrappleTargetPoint_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::GrappleTargetPoint")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::GrappleTargetPoint; use a flag to disable behavior.");
        }
    }

    public sealed partial class GrappleCanLatchOnTo
    {
        public delegate bool? Original(
            Terraria.Projectile projectile,
            Terraria.Player player,
            int x,
            int y
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            int x,
            int y
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_GrappleCanLatchOnTo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::GrappleCanLatchOnTo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::GrappleCanLatchOnTo; use a flag to disable behavior.");
        }
    }

    public sealed partial class PrepareBombToBlow
    {
        public delegate void Original(
            Terraria.Projectile projectile
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_PrepareBombToBlow_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::PrepareBombToBlow")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::PrepareBombToBlow; use a flag to disable behavior.");
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
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 boxPosition,
            int boxWidth,
            int boxHeight
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalProjectile_EmitEnchantmentVisualsAt_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalProjectile::EmitEnchantmentVisualsAt")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalProjectile::EmitEnchantmentVisualsAt; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_OnSpawn_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.OnSpawn.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_OnSpawn_Impl(GlobalProjectileHooks.OnSpawn.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnSpawn(
        Terraria.Projectile projectile,
        Terraria.DataStructures.IEntitySource source
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.DataStructures.IEntitySource source_captured
            ) => base.OnSpawn(
                projectile_captured,
                source_captured
            ),
            this,
            projectile,
            source
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_PreAI_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.PreAI.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_PreAI_Impl(GlobalProjectileHooks.PreAI.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreAI(
        Terraria.Projectile projectile
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.PreAI(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_AI_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.AI.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_AI_Impl(GlobalProjectileHooks.AI.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void AI(
        Terraria.Projectile projectile
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.AI(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_PostAI_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.PostAI.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_PostAI_Impl(GlobalProjectileHooks.PostAI.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostAI(
        Terraria.Projectile projectile
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.PostAI(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_ShouldUpdatePosition_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.ShouldUpdatePosition.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_ShouldUpdatePosition_Impl(GlobalProjectileHooks.ShouldUpdatePosition.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ShouldUpdatePosition(
        Terraria.Projectile projectile
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.ShouldUpdatePosition(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_TileCollideStyle_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.TileCollideStyle.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_TileCollideStyle_Impl(GlobalProjectileHooks.TileCollideStyle.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool TileCollideStyle(
        Terraria.Projectile projectile,
        ref int width,
        ref int height,
        ref bool fallThrough,
        ref Microsoft.Xna.Framework.Vector2 hitboxCenterFrac
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                ref int width_captured,
                ref int height_captured,
                ref bool fallThrough_captured,
                ref Microsoft.Xna.Framework.Vector2 hitboxCenterFrac_captured
            ) => base.TileCollideStyle(
                projectile_captured,
                ref width_captured,
                ref height_captured,
                ref fallThrough_captured,
                ref hitboxCenterFrac_captured
            ),
            this,
            projectile,
            ref width,
            ref height,
            ref fallThrough,
            ref hitboxCenterFrac
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_OnTileCollide_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.OnTileCollide.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_OnTileCollide_Impl(GlobalProjectileHooks.OnTileCollide.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool OnTileCollide(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Vector2 oldVelocity
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                Microsoft.Xna.Framework.Vector2 oldVelocity_captured
            ) => base.OnTileCollide(
                projectile_captured,
                oldVelocity_captured
            ),
            this,
            projectile,
            oldVelocity
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_PreKill_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.PreKill.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_PreKill_Impl(GlobalProjectileHooks.PreKill.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreKill(
        Terraria.Projectile projectile,
        int timeLeft
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                int timeLeft_captured
            ) => base.PreKill(
                projectile_captured,
                timeLeft_captured
            ),
            this,
            projectile,
            timeLeft
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_OnKill_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.OnKill.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_OnKill_Impl(GlobalProjectileHooks.OnKill.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnKill(
        Terraria.Projectile projectile,
        int timeLeft
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                int timeLeft_captured
            ) => base.OnKill(
                projectile_captured,
                timeLeft_captured
            ),
            this,
            projectile,
            timeLeft
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_CanCutTiles_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.CanCutTiles.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_CanCutTiles_Impl(GlobalProjectileHooks.CanCutTiles.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanCutTiles(
        Terraria.Projectile projectile
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.CanCutTiles(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_CutTiles_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.CutTiles.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_CutTiles_Impl(GlobalProjectileHooks.CutTiles.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void CutTiles(
        Terraria.Projectile projectile
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.CutTiles(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_CanDamage_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.CanDamage.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_CanDamage_Impl(GlobalProjectileHooks.CanDamage.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanDamage(
        Terraria.Projectile projectile
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.CanDamage(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_MinionContactDamage_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.MinionContactDamage.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_MinionContactDamage_Impl(GlobalProjectileHooks.MinionContactDamage.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool MinionContactDamage(
        Terraria.Projectile projectile
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.MinionContactDamage(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_ModifyDamageHitbox_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.ModifyDamageHitbox.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_ModifyDamageHitbox_Impl(GlobalProjectileHooks.ModifyDamageHitbox.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyDamageHitbox(
        Terraria.Projectile projectile,
        ref Microsoft.Xna.Framework.Rectangle hitbox
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                ref Microsoft.Xna.Framework.Rectangle hitbox_captured
            ) => base.ModifyDamageHitbox(
                projectile_captured,
                ref hitbox_captured
            ),
            this,
            projectile,
            ref hitbox
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_CanHitNPC_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.CanHitNPC.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_CanHitNPC_Impl(GlobalProjectileHooks.CanHitNPC.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanHitNPC(
        Terraria.Projectile projectile,
        Terraria.NPC target
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.NPC target_captured
            ) => base.CanHitNPC(
                projectile_captured,
                target_captured
            ),
            this,
            projectile,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_ModifyHitNPC_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.ModifyHitNPC.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_ModifyHitNPC_Impl(GlobalProjectileHooks.ModifyHitNPC.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHitNPC(
        Terraria.Projectile projectile,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.NPC target_captured,
                ref Terraria.NPC.HitModifiers modifiers_captured
            ) => base.ModifyHitNPC(
                projectile_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            projectile,
            target,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_OnHitNPC_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.OnHitNPC.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_OnHitNPC_Impl(GlobalProjectileHooks.OnHitNPC.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitNPC(
        Terraria.Projectile projectile,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.NPC target_captured,
                Terraria.NPC.HitInfo hit_captured,
                int damageDone_captured
            ) => base.OnHitNPC(
                projectile_captured,
                target_captured,
                hit_captured,
                damageDone_captured
            ),
            this,
            projectile,
            target,
            hit,
            damageDone
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_CanHitPvp_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.CanHitPvp.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_CanHitPvp_Impl(GlobalProjectileHooks.CanHitPvp.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanHitPvp(
        Terraria.Projectile projectile,
        Terraria.Player target
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player target_captured
            ) => base.CanHitPvp(
                projectile_captured,
                target_captured
            ),
            this,
            projectile,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_CanHitPlayer_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.CanHitPlayer.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_CanHitPlayer_Impl(GlobalProjectileHooks.CanHitPlayer.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanHitPlayer(
        Terraria.Projectile projectile,
        Terraria.Player target
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player target_captured
            ) => base.CanHitPlayer(
                projectile_captured,
                target_captured
            ),
            this,
            projectile,
            target
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_ModifyHitPlayer_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.ModifyHitPlayer.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_ModifyHitPlayer_Impl(GlobalProjectileHooks.ModifyHitPlayer.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHitPlayer(
        Terraria.Projectile projectile,
        Terraria.Player target,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player target_captured,
                ref Terraria.Player.HurtModifiers modifiers_captured
            ) => base.ModifyHitPlayer(
                projectile_captured,
                target_captured,
                ref modifiers_captured
            ),
            this,
            projectile,
            target,
            ref modifiers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_OnHitPlayer_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.OnHitPlayer.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_OnHitPlayer_Impl(GlobalProjectileHooks.OnHitPlayer.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnHitPlayer(
        Terraria.Projectile projectile,
        Terraria.Player target,
        Terraria.Player.HurtInfo info
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player target_captured,
                Terraria.Player.HurtInfo info_captured
            ) => base.OnHitPlayer(
                projectile_captured,
                target_captured,
                info_captured
            ),
            this,
            projectile,
            target,
            info
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_Colliding_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.Colliding.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_Colliding_Impl(GlobalProjectileHooks.Colliding.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? Colliding(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Rectangle projHitbox,
        Microsoft.Xna.Framework.Rectangle targetHitbox
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                Microsoft.Xna.Framework.Rectangle projHitbox_captured,
                Microsoft.Xna.Framework.Rectangle targetHitbox_captured
            ) => base.Colliding(
                projectile_captured,
                projHitbox_captured,
                targetHitbox_captured
            ),
            this,
            projectile,
            projHitbox,
            targetHitbox
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_GetAlpha_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.GetAlpha.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_GetAlpha_Impl(GlobalProjectileHooks.GetAlpha.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override Microsoft.Xna.Framework.Color? GetAlpha(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Color lightColor
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                Microsoft.Xna.Framework.Color lightColor_captured
            ) => base.GetAlpha(
                projectile_captured,
                lightColor_captured
            ),
            this,
            projectile,
            lightColor
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_PreDrawExtras_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.PreDrawExtras.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_PreDrawExtras_Impl(GlobalProjectileHooks.PreDrawExtras.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDrawExtras(
        Terraria.Projectile projectile
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.PreDrawExtras(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_PreDraw_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.PreDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_PreDraw_Impl(GlobalProjectileHooks.PreDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDraw(
        Terraria.Projectile projectile,
        ref Microsoft.Xna.Framework.Color lightColor
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                ref Microsoft.Xna.Framework.Color lightColor_captured
            ) => base.PreDraw(
                projectile_captured,
                ref lightColor_captured
            ),
            this,
            projectile,
            ref lightColor
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_PostDraw_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.PostDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_PostDraw_Impl(GlobalProjectileHooks.PostDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDraw(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Color lightColor
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Microsoft.Xna.Framework.Color lightColor_captured
            ) => base.PostDraw(
                projectile_captured,
                lightColor_captured
            ),
            this,
            projectile,
            lightColor
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_DrawBehind_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.DrawBehind.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_DrawBehind_Impl(GlobalProjectileHooks.DrawBehind.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void DrawBehind(
        Terraria.Projectile projectile,
        int index,
        System.Collections.Generic.List<int> behindNPCsAndTiles,
        System.Collections.Generic.List<int> behindNPCs,
        System.Collections.Generic.List<int> behindProjectiles,
        System.Collections.Generic.List<int> overPlayers,
        System.Collections.Generic.List<int> overWiresUI
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                int index_captured,
                System.Collections.Generic.List<int> behindNPCsAndTiles_captured,
                System.Collections.Generic.List<int> behindNPCs_captured,
                System.Collections.Generic.List<int> behindProjectiles_captured,
                System.Collections.Generic.List<int> overPlayers_captured,
                System.Collections.Generic.List<int> overWiresUI_captured
            ) => base.DrawBehind(
                projectile_captured,
                index_captured,
                behindNPCsAndTiles_captured,
                behindNPCs_captured,
                behindProjectiles_captured,
                overPlayers_captured,
                overWiresUI_captured
            ),
            this,
            projectile,
            index,
            behindNPCsAndTiles,
            behindNPCs,
            behindProjectiles,
            overPlayers,
            overWiresUI
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_CanUseGrapple_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.CanUseGrapple.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_CanUseGrapple_Impl(GlobalProjectileHooks.CanUseGrapple.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? CanUseGrapple(
        int type,
        Terraria.Player player
    )
    {
        return hook(
            (
                int type_captured,
                Terraria.Player player_captured
            ) => base.CanUseGrapple(
                type_captured,
                player_captured
            ),
            this,
            type,
            player
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_UseGrapple_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.UseGrapple.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_UseGrapple_Impl(GlobalProjectileHooks.UseGrapple.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UseGrapple(
        Terraria.Player player,
        ref int type
    )
    {
        hook(
            (
                Terraria.Player player_captured,
                ref int type_captured
            ) => base.UseGrapple(
                player_captured,
                ref type_captured
            ),
            this,
            player,
            ref type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_NumGrappleHooks_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.NumGrappleHooks.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_NumGrappleHooks_Impl(GlobalProjectileHooks.NumGrappleHooks.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void NumGrappleHooks(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref int numHooks
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player player_captured,
                ref int numHooks_captured
            ) => base.NumGrappleHooks(
                projectile_captured,
                player_captured,
                ref numHooks_captured
            ),
            this,
            projectile,
            player,
            ref numHooks
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_GrappleRetreatSpeed_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.GrappleRetreatSpeed.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_GrappleRetreatSpeed_Impl(GlobalProjectileHooks.GrappleRetreatSpeed.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void GrappleRetreatSpeed(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref float speed
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player player_captured,
                ref float speed_captured
            ) => base.GrappleRetreatSpeed(
                projectile_captured,
                player_captured,
                ref speed_captured
            ),
            this,
            projectile,
            player,
            ref speed
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_GrapplePullSpeed_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.GrapplePullSpeed.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_GrapplePullSpeed_Impl(GlobalProjectileHooks.GrapplePullSpeed.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void GrapplePullSpeed(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref float speed
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player player_captured,
                ref float speed_captured
            ) => base.GrapplePullSpeed(
                projectile_captured,
                player_captured,
                ref speed_captured
            ),
            this,
            projectile,
            player,
            ref speed
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_GrappleTargetPoint_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.GrappleTargetPoint.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_GrappleTargetPoint_Impl(GlobalProjectileHooks.GrappleTargetPoint.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void GrappleTargetPoint(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref float grappleX,
        ref float grappleY
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player player_captured,
                ref float grappleX_captured,
                ref float grappleY_captured
            ) => base.GrappleTargetPoint(
                projectile_captured,
                player_captured,
                ref grappleX_captured,
                ref grappleY_captured
            ),
            this,
            projectile,
            player,
            ref grappleX,
            ref grappleY
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_GrappleCanLatchOnTo_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.GrappleCanLatchOnTo.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_GrappleCanLatchOnTo_Impl(GlobalProjectileHooks.GrappleCanLatchOnTo.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? GrappleCanLatchOnTo(
        Terraria.Projectile projectile,
        Terraria.Player player,
        int x,
        int y
    )
    {
        return hook(
            (
                Terraria.Projectile projectile_captured,
                Terraria.Player player_captured,
                int x_captured,
                int y_captured
            ) => base.GrappleCanLatchOnTo(
                projectile_captured,
                player_captured,
                x_captured,
                y_captured
            ),
            this,
            projectile,
            player,
            x,
            y
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_PrepareBombToBlow_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.PrepareBombToBlow.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_PrepareBombToBlow_Impl(GlobalProjectileHooks.PrepareBombToBlow.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PrepareBombToBlow(
        Terraria.Projectile projectile
    )
    {
        hook(
            (
                Terraria.Projectile projectile_captured
            ) => base.PrepareBombToBlow(
                projectile_captured
            ),
            this,
            projectile
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalProjectile_EmitEnchantmentVisualsAt_Impl : Terraria.ModLoader.GlobalProjectile
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalProjectileHooks.EmitEnchantmentVisualsAt.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalProjectile_EmitEnchantmentVisualsAt_Impl(GlobalProjectileHooks.EmitEnchantmentVisualsAt.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
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
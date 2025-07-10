namespace Daybreak.Common.Features.Hooks;

using System.Linq;

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
//     System.Void Terraria.ModLoader.GlobalProjectile::SendExtraAI(Terraria.Projectile,Terraria.ModLoader.IO.BitWriter,System.IO.BinaryWriter)
//     System.Void Terraria.ModLoader.GlobalProjectile::ReceiveExtraAI(Terraria.Projectile,Terraria.ModLoader.IO.BitReader,System.IO.BinaryReader)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::ShouldUpdatePosition(Terraria.Projectile)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::TileCollideStyle(Terraria.Projectile,System.Int32&,System.Int32&,System.Boolean&,Microsoft.Xna.Framework.Vector2&)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::OnTileCollide(Terraria.Projectile,Microsoft.Xna.Framework.Vector2)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::PreKill(Terraria.Projectile,System.Int32)
//     System.Void Terraria.ModLoader.GlobalProjectile::OnKill(Terraria.Projectile,System.Int32)
//     System.Void Terraria.ModLoader.GlobalProjectile::CutTiles(Terraria.Projectile)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::MinionContactDamage(Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalProjectile::ModifyDamageHitbox(Terraria.Projectile,Microsoft.Xna.Framework.Rectangle&)
//     System.Void Terraria.ModLoader.GlobalProjectile::ModifyHitNPC(Terraria.Projectile,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalProjectile::OnHitNPC(Terraria.Projectile,Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Void Terraria.ModLoader.GlobalProjectile::ModifyHitPlayer(Terraria.Projectile,Terraria.Player,Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.GlobalProjectile::OnHitPlayer(Terraria.Projectile,Terraria.Player,Terraria.Player/HurtInfo)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::PreDrawExtras(Terraria.Projectile)
//     System.Boolean Terraria.ModLoader.GlobalProjectile::PreDraw(Terraria.Projectile,Microsoft.Xna.Framework.Color&)
//     System.Void Terraria.ModLoader.GlobalProjectile::PostDraw(Terraria.Projectile,Microsoft.Xna.Framework.Color)
//     System.Void Terraria.ModLoader.GlobalProjectile::DrawBehind(Terraria.Projectile,System.Int32,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>,System.Collections.Generic.List`1<System.Int32>)
//     System.Void Terraria.ModLoader.GlobalProjectile::UseGrapple(Terraria.Player,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalProjectile::NumGrappleHooks(Terraria.Projectile,Terraria.Player,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalProjectile::GrappleRetreatSpeed(Terraria.Projectile,Terraria.Player,System.Single&)
//     System.Void Terraria.ModLoader.GlobalProjectile::GrapplePullSpeed(Terraria.Projectile,Terraria.Player,System.Single&)
//     System.Void Terraria.ModLoader.GlobalProjectile::GrappleTargetPoint(Terraria.Projectile,Terraria.Player,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.GlobalProjectile::PrepareBombToBlow(Terraria.Projectile)
//     System.Void Terraria.ModLoader.GlobalProjectile::EmitEnchantmentVisualsAt(Terraria.Projectile,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32)
public static partial class GlobalProjectileHooks
{
    public sealed partial class OnSpawn
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.DataStructures.IEntitySource source
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.DataStructures.IEntitySource source
        )
        {
            Event?.Invoke(self, projectile, source);
        }
    }

    public sealed partial class PreAI
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, projectile);
            }

            return result;
        }
    }

    public sealed partial class AI
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            Event?.Invoke(self, projectile);
        }
    }

    public sealed partial class PostAI
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            Event?.Invoke(self, projectile);
        }
    }

    public sealed partial class SendExtraAI
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.ModLoader.IO.BitWriter bitWriter,
            System.IO.BinaryWriter binaryWriter
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.ModLoader.IO.BitWriter bitWriter,
            System.IO.BinaryWriter binaryWriter
        )
        {
            Event?.Invoke(self, projectile, bitWriter, binaryWriter);
        }
    }

    public sealed partial class ReceiveExtraAI
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.ModLoader.IO.BitReader bitReader,
            System.IO.BinaryReader binaryReader
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.ModLoader.IO.BitReader bitReader,
            System.IO.BinaryReader binaryReader
        )
        {
            Event?.Invoke(self, projectile, bitReader, binaryReader);
        }
    }

    public sealed partial class ShouldUpdatePosition
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, projectile))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class TileCollideStyle
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref int width,
            ref int height,
            ref bool fallThrough,
            ref Microsoft.Xna.Framework.Vector2 hitboxCenterFrac
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref int width,
            ref int height,
            ref bool fallThrough,
            ref Microsoft.Xna.Framework.Vector2 hitboxCenterFrac
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, projectile, ref width, ref height, ref fallThrough, ref hitboxCenterFrac))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnTileCollide
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 oldVelocity
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 oldVelocity
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, projectile, oldVelocity);
            }

            return result;
        }
    }

    public sealed partial class PreKill
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int timeLeft
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int timeLeft
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, projectile, timeLeft);
            }

            return result;
        }
    }

    public sealed partial class OnKill
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int timeLeft
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int timeLeft
        )
        {
            Event?.Invoke(self, projectile, timeLeft);
        }
    }

    public sealed partial class CutTiles
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            Event?.Invoke(self, projectile);
        }
    }

    public sealed partial class MinionContactDamage
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, projectile))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class ModifyDamageHitbox
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Rectangle hitbox
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Rectangle hitbox
        )
        {
            Event?.Invoke(self, projectile, ref hitbox);
        }
    }

    public sealed partial class ModifyHitNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, projectile, target, ref modifiers);
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
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
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        )
        {
            Event?.Invoke(self, projectile, target, hit, damageDone);
        }
    }

    public sealed partial class ModifyHitPlayer
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        )
        {
            Event?.Invoke(self, projectile, target, ref modifiers);
        }
    }

    public sealed partial class OnHitPlayer
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target,
            Terraria.Player.HurtInfo info
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player target,
            Terraria.Player.HurtInfo info
        )
        {
            Event?.Invoke(self, projectile, target, info);
        }
    }

    public sealed partial class PreDrawExtras
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, projectile);
            }

            return result;
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Color lightColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            ref Microsoft.Xna.Framework.Color lightColor
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, projectile, ref lightColor);
            }

            return result;
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Color lightColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Color lightColor
        )
        {
            Event?.Invoke(self, projectile, lightColor);
        }
    }

    public sealed partial class DrawBehind
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int index,
            System.Collections.Generic.List<int> behindNPCsAndTiles,
            System.Collections.Generic.List<int> behindNPCs,
            System.Collections.Generic.List<int> behindProjectiles,
            System.Collections.Generic.List<int> overPlayers,
            System.Collections.Generic.List<int> overWiresUI
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            int index,
            System.Collections.Generic.List<int> behindNPCsAndTiles,
            System.Collections.Generic.List<int> behindNPCs,
            System.Collections.Generic.List<int> behindProjectiles,
            System.Collections.Generic.List<int> overPlayers,
            System.Collections.Generic.List<int> overWiresUI
        )
        {
            Event?.Invoke(self, projectile, index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
        }
    }

    public sealed partial class UseGrapple
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Player player,
            ref int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Player player,
            ref int type
        )
        {
            Event?.Invoke(self, player, ref type);
        }
    }

    public sealed partial class NumGrappleHooks
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref int numHooks
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref int numHooks
        )
        {
            Event?.Invoke(self, projectile, player, ref numHooks);
        }
    }

    public sealed partial class GrappleRetreatSpeed
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        )
        {
            Event?.Invoke(self, projectile, player, ref speed);
        }
    }

    public sealed partial class GrapplePullSpeed
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float speed
        )
        {
            Event?.Invoke(self, projectile, player, ref speed);
        }
    }

    public sealed partial class GrappleTargetPoint
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float grappleX,
            ref float grappleY
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Terraria.Player player,
            ref float grappleX,
            ref float grappleY
        )
        {
            Event?.Invoke(self, projectile, player, ref grappleX, ref grappleY);
        }
    }

    public sealed partial class PrepareBombToBlow
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile
        )
        {
            Event?.Invoke(self, projectile);
        }
    }

    public sealed partial class EmitEnchantmentVisualsAt
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalProjectile self,
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
            Terraria.ModLoader.GlobalProjectile self,
            Terraria.Projectile projectile,
            Microsoft.Xna.Framework.Vector2 boxPosition,
            int boxWidth,
            int boxHeight
        )
        {
            Event?.Invoke(self, projectile, boxPosition, boxWidth, boxHeight);
        }
    }
}

public sealed partial class GlobalProjectileImpl : Terraria.ModLoader.GlobalProjectile
{
    public override void OnSpawn(
        Terraria.Projectile projectile,
        Terraria.DataStructures.IEntitySource source
    )
    {
        if (!GlobalProjectileHooks.OnSpawn.GetInvocationList().Any())
        {
            base.OnSpawn(
                projectile,
                source
            );
            return;
        }

        GlobalProjectileHooks.OnSpawn.Invoke(
            this,
            projectile,
            source
        );
    }

    public override bool PreAI(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.PreAI.GetInvocationList().Any())
        {
            return base.PreAI(
                projectile
            );
        }

        return GlobalProjectileHooks.PreAI.Invoke(
            this,
            projectile
        );
    }

    public override void AI(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.AI.GetInvocationList().Any())
        {
            base.AI(
                projectile
            );
            return;
        }

        GlobalProjectileHooks.AI.Invoke(
            this,
            projectile
        );
    }

    public override void PostAI(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.PostAI.GetInvocationList().Any())
        {
            base.PostAI(
                projectile
            );
            return;
        }

        GlobalProjectileHooks.PostAI.Invoke(
            this,
            projectile
        );
    }

    public override void SendExtraAI(
        Terraria.Projectile projectile,
        Terraria.ModLoader.IO.BitWriter bitWriter,
        System.IO.BinaryWriter binaryWriter
    )
    {
        if (!GlobalProjectileHooks.SendExtraAI.GetInvocationList().Any())
        {
            base.SendExtraAI(
                projectile,
                bitWriter,
                binaryWriter
            );
            return;
        }

        GlobalProjectileHooks.SendExtraAI.Invoke(
            this,
            projectile,
            bitWriter,
            binaryWriter
        );
    }

    public override void ReceiveExtraAI(
        Terraria.Projectile projectile,
        Terraria.ModLoader.IO.BitReader bitReader,
        System.IO.BinaryReader binaryReader
    )
    {
        if (!GlobalProjectileHooks.ReceiveExtraAI.GetInvocationList().Any())
        {
            base.ReceiveExtraAI(
                projectile,
                bitReader,
                binaryReader
            );
            return;
        }

        GlobalProjectileHooks.ReceiveExtraAI.Invoke(
            this,
            projectile,
            bitReader,
            binaryReader
        );
    }

    public override bool ShouldUpdatePosition(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.ShouldUpdatePosition.GetInvocationList().Any())
        {
            return base.ShouldUpdatePosition(
                projectile
            );
        }

        return GlobalProjectileHooks.ShouldUpdatePosition.Invoke(
            this,
            projectile
        );
    }

    public override bool TileCollideStyle(
        Terraria.Projectile projectile,
        ref int width,
        ref int height,
        ref bool fallThrough,
        ref Microsoft.Xna.Framework.Vector2 hitboxCenterFrac
    )
    {
        if (!GlobalProjectileHooks.TileCollideStyle.GetInvocationList().Any())
        {
            return base.TileCollideStyle(
                projectile,
                ref width,
                ref height,
                ref fallThrough,
                ref hitboxCenterFrac
            );
        }

        return GlobalProjectileHooks.TileCollideStyle.Invoke(
            this,
            projectile,
            ref width,
            ref height,
            ref fallThrough,
            ref hitboxCenterFrac
        );
    }

    public override bool OnTileCollide(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Vector2 oldVelocity
    )
    {
        if (!GlobalProjectileHooks.OnTileCollide.GetInvocationList().Any())
        {
            return base.OnTileCollide(
                projectile,
                oldVelocity
            );
        }

        return GlobalProjectileHooks.OnTileCollide.Invoke(
            this,
            projectile,
            oldVelocity
        );
    }

    public override bool PreKill(
        Terraria.Projectile projectile,
        int timeLeft
    )
    {
        if (!GlobalProjectileHooks.PreKill.GetInvocationList().Any())
        {
            return base.PreKill(
                projectile,
                timeLeft
            );
        }

        return GlobalProjectileHooks.PreKill.Invoke(
            this,
            projectile,
            timeLeft
        );
    }

    public override void OnKill(
        Terraria.Projectile projectile,
        int timeLeft
    )
    {
        if (!GlobalProjectileHooks.OnKill.GetInvocationList().Any())
        {
            base.OnKill(
                projectile,
                timeLeft
            );
            return;
        }

        GlobalProjectileHooks.OnKill.Invoke(
            this,
            projectile,
            timeLeft
        );
    }

    public override void CutTiles(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.CutTiles.GetInvocationList().Any())
        {
            base.CutTiles(
                projectile
            );
            return;
        }

        GlobalProjectileHooks.CutTiles.Invoke(
            this,
            projectile
        );
    }

    public override bool MinionContactDamage(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.MinionContactDamage.GetInvocationList().Any())
        {
            return base.MinionContactDamage(
                projectile
            );
        }

        return GlobalProjectileHooks.MinionContactDamage.Invoke(
            this,
            projectile
        );
    }

    public override void ModifyDamageHitbox(
        Terraria.Projectile projectile,
        ref Microsoft.Xna.Framework.Rectangle hitbox
    )
    {
        if (!GlobalProjectileHooks.ModifyDamageHitbox.GetInvocationList().Any())
        {
            base.ModifyDamageHitbox(
                projectile,
                ref hitbox
            );
            return;
        }

        GlobalProjectileHooks.ModifyDamageHitbox.Invoke(
            this,
            projectile,
            ref hitbox
        );
    }

    public override void ModifyHitNPC(
        Terraria.Projectile projectile,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!GlobalProjectileHooks.ModifyHitNPC.GetInvocationList().Any())
        {
            base.ModifyHitNPC(
                projectile,
                target,
                ref modifiers
            );
            return;
        }

        GlobalProjectileHooks.ModifyHitNPC.Invoke(
            this,
            projectile,
            target,
            ref modifiers
        );
    }

    public override void OnHitNPC(
        Terraria.Projectile projectile,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        if (!GlobalProjectileHooks.OnHitNPC.GetInvocationList().Any())
        {
            base.OnHitNPC(
                projectile,
                target,
                hit,
                damageDone
            );
            return;
        }

        GlobalProjectileHooks.OnHitNPC.Invoke(
            this,
            projectile,
            target,
            hit,
            damageDone
        );
    }

    public override void ModifyHitPlayer(
        Terraria.Projectile projectile,
        Terraria.Player target,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        if (!GlobalProjectileHooks.ModifyHitPlayer.GetInvocationList().Any())
        {
            base.ModifyHitPlayer(
                projectile,
                target,
                ref modifiers
            );
            return;
        }

        GlobalProjectileHooks.ModifyHitPlayer.Invoke(
            this,
            projectile,
            target,
            ref modifiers
        );
    }

    public override void OnHitPlayer(
        Terraria.Projectile projectile,
        Terraria.Player target,
        Terraria.Player.HurtInfo info
    )
    {
        if (!GlobalProjectileHooks.OnHitPlayer.GetInvocationList().Any())
        {
            base.OnHitPlayer(
                projectile,
                target,
                info
            );
            return;
        }

        GlobalProjectileHooks.OnHitPlayer.Invoke(
            this,
            projectile,
            target,
            info
        );
    }

    public override bool PreDrawExtras(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.PreDrawExtras.GetInvocationList().Any())
        {
            return base.PreDrawExtras(
                projectile
            );
        }

        return GlobalProjectileHooks.PreDrawExtras.Invoke(
            this,
            projectile
        );
    }

    public override bool PreDraw(
        Terraria.Projectile projectile,
        ref Microsoft.Xna.Framework.Color lightColor
    )
    {
        if (!GlobalProjectileHooks.PreDraw.GetInvocationList().Any())
        {
            return base.PreDraw(
                projectile,
                ref lightColor
            );
        }

        return GlobalProjectileHooks.PreDraw.Invoke(
            this,
            projectile,
            ref lightColor
        );
    }

    public override void PostDraw(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Color lightColor
    )
    {
        if (!GlobalProjectileHooks.PostDraw.GetInvocationList().Any())
        {
            base.PostDraw(
                projectile,
                lightColor
            );
            return;
        }

        GlobalProjectileHooks.PostDraw.Invoke(
            this,
            projectile,
            lightColor
        );
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
        if (!GlobalProjectileHooks.DrawBehind.GetInvocationList().Any())
        {
            base.DrawBehind(
                projectile,
                index,
                behindNPCsAndTiles,
                behindNPCs,
                behindProjectiles,
                overPlayers,
                overWiresUI
            );
            return;
        }

        GlobalProjectileHooks.DrawBehind.Invoke(
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

    public override void UseGrapple(
        Terraria.Player player,
        ref int type
    )
    {
        if (!GlobalProjectileHooks.UseGrapple.GetInvocationList().Any())
        {
            base.UseGrapple(
                player,
                ref type
            );
            return;
        }

        GlobalProjectileHooks.UseGrapple.Invoke(
            this,
            player,
            ref type
        );
    }

    public override void NumGrappleHooks(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref int numHooks
    )
    {
        if (!GlobalProjectileHooks.NumGrappleHooks.GetInvocationList().Any())
        {
            base.NumGrappleHooks(
                projectile,
                player,
                ref numHooks
            );
            return;
        }

        GlobalProjectileHooks.NumGrappleHooks.Invoke(
            this,
            projectile,
            player,
            ref numHooks
        );
    }

    public override void GrappleRetreatSpeed(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref float speed
    )
    {
        if (!GlobalProjectileHooks.GrappleRetreatSpeed.GetInvocationList().Any())
        {
            base.GrappleRetreatSpeed(
                projectile,
                player,
                ref speed
            );
            return;
        }

        GlobalProjectileHooks.GrappleRetreatSpeed.Invoke(
            this,
            projectile,
            player,
            ref speed
        );
    }

    public override void GrapplePullSpeed(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref float speed
    )
    {
        if (!GlobalProjectileHooks.GrapplePullSpeed.GetInvocationList().Any())
        {
            base.GrapplePullSpeed(
                projectile,
                player,
                ref speed
            );
            return;
        }

        GlobalProjectileHooks.GrapplePullSpeed.Invoke(
            this,
            projectile,
            player,
            ref speed
        );
    }

    public override void GrappleTargetPoint(
        Terraria.Projectile projectile,
        Terraria.Player player,
        ref float grappleX,
        ref float grappleY
    )
    {
        if (!GlobalProjectileHooks.GrappleTargetPoint.GetInvocationList().Any())
        {
            base.GrappleTargetPoint(
                projectile,
                player,
                ref grappleX,
                ref grappleY
            );
            return;
        }

        GlobalProjectileHooks.GrappleTargetPoint.Invoke(
            this,
            projectile,
            player,
            ref grappleX,
            ref grappleY
        );
    }

    public override void PrepareBombToBlow(
        Terraria.Projectile projectile
    )
    {
        if (!GlobalProjectileHooks.PrepareBombToBlow.GetInvocationList().Any())
        {
            base.PrepareBombToBlow(
                projectile
            );
            return;
        }

        GlobalProjectileHooks.PrepareBombToBlow.Invoke(
            this,
            projectile
        );
    }

    public override void EmitEnchantmentVisualsAt(
        Terraria.Projectile projectile,
        Microsoft.Xna.Framework.Vector2 boxPosition,
        int boxWidth,
        int boxHeight
    )
    {
        if (!GlobalProjectileHooks.EmitEnchantmentVisualsAt.GetInvocationList().Any())
        {
            base.EmitEnchantmentVisualsAt(
                projectile,
                boxPosition,
                boxWidth,
                boxHeight
            );
            return;
        }

        GlobalProjectileHooks.EmitEnchantmentVisualsAt.Invoke(
            this,
            projectile,
            boxPosition,
            boxWidth,
            boxHeight
        );
    }
}

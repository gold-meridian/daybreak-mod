namespace Daybreak.Common.Features.Hooks;

using System.Linq;

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
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreKill(Terraria.NPC)
//     System.Void Terraria.ModLoader.GlobalNPC::OnKill(Terraria.NPC)
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
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyHitByItem(Terraria.NPC,Terraria.Player,Terraria.Item,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::OnHitByItem(Terraria.NPC,Terraria.Player,Terraria.Item,Terraria.NPC/HitInfo,System.Int32)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyHitByProjectile(Terraria.NPC,Terraria.Projectile,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::OnHitByProjectile(Terraria.NPC,Terraria.Projectile,Terraria.NPC/HitInfo,System.Int32)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyIncomingHit(Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalNPC::BossHeadSlot(Terraria.NPC,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::BossHeadRotation(Terraria.NPC,System.Single&)
//     System.Void Terraria.ModLoader.GlobalNPC::BossHeadSpriteEffects(Terraria.NPC,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Void Terraria.ModLoader.GlobalNPC::DrawEffects(Terraria.NPC,Microsoft.Xna.Framework.Color&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreDraw(Terraria.NPC,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color)
//     System.Void Terraria.ModLoader.GlobalNPC::PostDraw(Terraria.NPC,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color)
//     System.Void Terraria.ModLoader.GlobalNPC::DrawBehind(Terraria.NPC,System.Int32)
//     System.Void Terraria.ModLoader.GlobalNPC::EditSpawnRate(Terraria.Player,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::EditSpawnRange(Terraria.Player,System.Int32&,System.Int32&,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalNPC::EditSpawnPool(System.Collections.Generic.IDictionary`2<System.Int32,System.Single>,Terraria.ModLoader.NPCSpawnInfo)
//     System.Void Terraria.ModLoader.GlobalNPC::SpawnNPC(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalNPC::GetChat(Terraria.NPC,System.String&)
//     System.Boolean Terraria.ModLoader.GlobalNPC::PreChatButtonClicked(Terraria.NPC,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalNPC::OnChatButtonClicked(Terraria.NPC,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyShop(Terraria.ModLoader.NPCShop)
//     System.Void Terraria.ModLoader.GlobalNPC::ModifyActiveShop(Terraria.NPC,System.String,Terraria.Item[])
//     System.Void Terraria.ModLoader.GlobalNPC::SetupTravelShop(System.Int32[],System.Int32&)
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
//     System.Void Terraria.ModLoader.GlobalNPC::SaveData(Terraria.NPC,Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.GlobalNPC::LoadData(Terraria.NPC,Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.GlobalNPC::ChatBubblePosition(Terraria.NPC,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Void Terraria.ModLoader.GlobalNPC::PartyHatPosition(Terraria.NPC,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Void Terraria.ModLoader.GlobalNPC::EmoteBubblePosition(Terraria.NPC,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
public static partial class GlobalNPCHooks
{
    public sealed partial class SetDefaultsFromNetId
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            Event?.Invoke(self, npc);
        }
    }

    public sealed partial class OnSpawn
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.DataStructures.IEntitySource source
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.DataStructures.IEntitySource source
        )
        {
            Event?.Invoke(self, npc, source);
        }
    }

    public sealed partial class ApplyDifficultyAndPlayerScaling
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int numPlayers,
            float balance,
            float bossAdjustment
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int numPlayers,
            float balance,
            float bossAdjustment
        )
        {
            Event?.Invoke(self, npc, numPlayers, balance, bossAdjustment);
        }
    }

    public sealed partial class SetBestiary
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.GameContent.Bestiary.BestiaryDatabase database,
            Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.GameContent.Bestiary.BestiaryDatabase database,
            Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry
        )
        {
            Event?.Invoke(self, npc, database, bestiaryEntry);
        }
    }

    public sealed partial class ModifyTypeName
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref string typeName
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref string typeName
        )
        {
            Event?.Invoke(self, npc, ref typeName);
        }
    }

    public sealed partial class ModifyHoverBoundingBox
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Rectangle boundingBox
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Rectangle boundingBox
        )
        {
            Event?.Invoke(self, npc, ref boundingBox);
        }
    }

    public sealed partial class ModifyNPCNameList
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            System.Collections.Generic.List<string> nameList
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            System.Collections.Generic.List<string> nameList
        )
        {
            Event?.Invoke(self, npc, nameList);
        }
    }

    public sealed partial class ResetEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            Event?.Invoke(self, npc);
        }
    }

    public sealed partial class PreAI
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, npc);
            }

            return result;
        }
    }

    public sealed partial class AI
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            Event?.Invoke(self, npc);
        }
    }

    public sealed partial class PostAI
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            Event?.Invoke(self, npc);
        }
    }

    public sealed partial class FindFrame
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int frameHeight
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int frameHeight
        )
        {
            Event?.Invoke(self, npc, frameHeight);
        }
    }

    public sealed partial class HitEffect
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC.HitInfo hit
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC.HitInfo hit
        )
        {
            Event?.Invoke(self, npc, hit);
        }
    }

    public sealed partial class UpdateLifeRegen
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int damage
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int damage
        )
        {
            Event?.Invoke(self, npc, ref damage);
        }
    }

    public sealed partial class CheckActive
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, npc))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class CheckDead
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, npc);
            }

            return result;
        }
    }

    public sealed partial class PreKill
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, npc);
            }

            return result;
        }
    }

    public sealed partial class OnKill
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            Event?.Invoke(self, npc);
        }
    }

    public sealed partial class OnCaughtBy
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            bool failed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            bool failed
        )
        {
            Event?.Invoke(self, npc, player, item, failed);
        }
    }

    public sealed partial class ModifyNPCLoot
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.ModLoader.NPCLoot npcLoot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.ModLoader.NPCLoot npcLoot
        )
        {
            Event?.Invoke(self, npc, npcLoot);
        }
    }

    public sealed partial class ModifyGlobalLoot
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.ModLoader.GlobalLoot globalLoot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.ModLoader.GlobalLoot globalLoot
        )
        {
            Event?.Invoke(self, globalLoot);
        }
    }

    public sealed partial class CanHitPlayer
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            ref int cooldownSlot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            ref int cooldownSlot
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, npc, target, ref cooldownSlot))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyHitPlayer
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        )
        {
            Event?.Invoke(self, npc, target, ref modifiers);
        }
    }

    public sealed partial class OnHitPlayer
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        )
        {
            Event?.Invoke(self, npc, target, hurtInfo);
        }
    }

    public sealed partial class CanHitNPC
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, npc, target))
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
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC attacker
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC attacker
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, npc, attacker))
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
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, npc, target, ref modifiers);
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit
        )
        {
            Event?.Invoke(self, npc, target, hit);
        }
    }

    public sealed partial class ModifyHitByItem
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, npc, player, item, ref modifiers);
        }
    }

    public sealed partial class OnHitByItem
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Player player,
            Terraria.Item item,
            Terraria.NPC.HitInfo hit,
            int damageDone
        )
        {
            Event?.Invoke(self, npc, player, item, hit, damageDone);
        }
    }

    public sealed partial class ModifyHitByProjectile
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, npc, projectile, ref modifiers);
        }
    }

    public sealed partial class OnHitByProjectile
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            Terraria.NPC.HitInfo hit,
            int damageDone
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.Projectile projectile,
            Terraria.NPC.HitInfo hit,
            int damageDone
        )
        {
            Event?.Invoke(self, npc, projectile, hit, damageDone);
        }
    }

    public sealed partial class ModifyIncomingHit
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, npc, ref modifiers);
        }
    }

    public sealed partial class BossHeadSlot
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int index
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int index
        )
        {
            Event?.Invoke(self, npc, ref index);
        }
    }

    public sealed partial class BossHeadRotation
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float rotation
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float rotation
        )
        {
            Event?.Invoke(self, npc, ref rotation);
        }
    }

    public sealed partial class BossHeadSpriteEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        )
        {
            Event?.Invoke(self, npc, ref spriteEffects);
        }
    }

    public sealed partial class DrawEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Color drawColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Color drawColor
        )
        {
            Event?.Invoke(self, npc, ref drawColor);
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, npc, spriteBatch, screenPos, drawColor);
            }

            return result;
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Vector2 screenPos,
            Microsoft.Xna.Framework.Color drawColor
        )
        {
            Event?.Invoke(self, npc, spriteBatch, screenPos, drawColor);
        }
    }

    public sealed partial class DrawBehind
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int index
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            int index
        )
        {
            Event?.Invoke(self, npc, index);
        }
    }

    public sealed partial class EditSpawnRate
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.Player player,
            ref int spawnRate,
            ref int maxSpawns
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.Player player,
            ref int spawnRate,
            ref int maxSpawns
        )
        {
            Event?.Invoke(self, player, ref spawnRate, ref maxSpawns);
        }
    }

    public sealed partial class EditSpawnRange
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.Player player,
            ref int spawnRangeX,
            ref int spawnRangeY,
            ref int safeRangeX,
            ref int safeRangeY
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.Player player,
            ref int spawnRangeX,
            ref int spawnRangeY,
            ref int safeRangeX,
            ref int safeRangeY
        )
        {
            Event?.Invoke(self, player, ref spawnRangeX, ref spawnRangeY, ref safeRangeX, ref safeRangeY);
        }
    }

    public sealed partial class EditSpawnPool
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            System.Collections.Generic.IDictionary<int, float> pool,
            Terraria.ModLoader.NPCSpawnInfo spawnInfo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            System.Collections.Generic.IDictionary<int, float> pool,
            Terraria.ModLoader.NPCSpawnInfo spawnInfo
        )
        {
            Event?.Invoke(self, pool, spawnInfo);
        }
    }

    public sealed partial class SpawnNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            int npc,
            int tileX,
            int tileY
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            int npc,
            int tileX,
            int tileY
        )
        {
            Event?.Invoke(self, npc, tileX, tileY);
        }
    }

    public sealed partial class GetChat
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref string chat
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref string chat
        )
        {
            Event?.Invoke(self, npc, ref chat);
        }
    }

    public sealed partial class PreChatButtonClicked
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool firstButton
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool firstButton
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, npc, firstButton);
            }

            return result;
        }
    }

    public sealed partial class OnChatButtonClicked
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool firstButton
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool firstButton
        )
        {
            Event?.Invoke(self, npc, firstButton);
        }
    }

    public sealed partial class ModifyShop
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.ModLoader.NPCShop shop
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.ModLoader.NPCShop shop
        )
        {
            Event?.Invoke(self, shop);
        }
    }

    public sealed partial class ModifyActiveShop
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            string shopName,
            Terraria.Item[] items
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            string shopName,
            Terraria.Item[] items
        )
        {
            Event?.Invoke(self, npc, shopName, items);
        }
    }

    public sealed partial class SetupTravelShop
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            int[] shop,
            ref int nextSlot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            int[] shop,
            ref int nextSlot
        )
        {
            Event?.Invoke(self, shop, ref nextSlot);
        }
    }

    public sealed partial class OnGoToStatue
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool toKingStatue
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            bool toKingStatue
        )
        {
            Event?.Invoke(self, npc, toKingStatue);
        }
    }

    public sealed partial class BuffTownNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            ref float damageMult,
            ref int defense
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            ref float damageMult,
            ref int defense
        )
        {
            Event?.Invoke(self, ref damageMult, ref defense);
        }
    }

    public sealed partial class ModifyDeathMessage
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Terraria.Localization.NetworkText customText,
            ref Microsoft.Xna.Framework.Color color
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Terraria.Localization.NetworkText customText,
            ref Microsoft.Xna.Framework.Color color
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, npc, ref customText, ref color);
            }

            return result;
        }
    }

    public sealed partial class TownNPCAttackStrength
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int damage,
            ref float knockback
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int damage,
            ref float knockback
        )
        {
            Event?.Invoke(self, npc, ref damage, ref knockback);
        }
    }

    public sealed partial class TownNPCAttackCooldown
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int cooldown,
            ref int randExtraCooldown
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int cooldown,
            ref int randExtraCooldown
        )
        {
            Event?.Invoke(self, npc, ref cooldown, ref randExtraCooldown);
        }
    }

    public sealed partial class TownNPCAttackProj
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int projType,
            ref int attackDelay
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int projType,
            ref int attackDelay
        )
        {
            Event?.Invoke(self, npc, ref projType, ref attackDelay);
        }
    }

    public sealed partial class TownNPCAttackProjSpeed
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float multiplier,
            ref float gravityCorrection,
            ref float randomOffset
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float multiplier,
            ref float gravityCorrection,
            ref float randomOffset
        )
        {
            Event?.Invoke(self, npc, ref multiplier, ref gravityCorrection, ref randomOffset);
        }
    }

    public sealed partial class TownNPCAttackShoot
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref bool inBetweenShots
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref bool inBetweenShots
        )
        {
            Event?.Invoke(self, npc, ref inBetweenShots);
        }
    }

    public sealed partial class TownNPCAttackMagic
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float auraLightMultiplier
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref float auraLightMultiplier
        )
        {
            Event?.Invoke(self, npc, ref auraLightMultiplier);
        }
    }

    public sealed partial class TownNPCAttackSwing
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int itemWidth,
            ref int itemHeight
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref int itemWidth,
            ref int itemHeight
        )
        {
            Event?.Invoke(self, npc, ref itemWidth, ref itemHeight);
        }
    }

    public sealed partial class DrawTownAttackGun
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref float scale,
            ref int horizontalHoldoutOffset
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref float scale,
            ref int horizontalHoldoutOffset
        )
        {
            Event?.Invoke(self, npc, ref item, ref itemFrame, ref scale, ref horizontalHoldoutOffset);
        }
    }

    public sealed partial class DrawTownAttackSwing
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref int itemSize,
            ref float scale,
            ref Microsoft.Xna.Framework.Vector2 offset
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Graphics.Texture2D item,
            ref Microsoft.Xna.Framework.Rectangle itemFrame,
            ref int itemSize,
            ref float scale,
            ref Microsoft.Xna.Framework.Vector2 offset
        )
        {
            Event?.Invoke(self, npc, ref item, ref itemFrame, ref itemSize, ref scale, ref offset);
        }
    }

    public sealed partial class ModifyCollisionData
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Rectangle victimHitbox,
            ref int immunityCooldownSlot,
            ref Terraria.ModLoader.MultipliableFloat damageMultiplier,
            ref Microsoft.Xna.Framework.Rectangle npcHitbox
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Microsoft.Xna.Framework.Rectangle victimHitbox,
            ref int immunityCooldownSlot,
            ref Terraria.ModLoader.MultipliableFloat damageMultiplier,
            ref Microsoft.Xna.Framework.Rectangle npcHitbox
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, npc, victimHitbox, ref immunityCooldownSlot, ref damageMultiplier, ref npcHitbox);
            }

            return result;
        }
    }

    public sealed partial class NeedSaving
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc
        )
        {
            var result = false;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result |= handler.Invoke(self, npc);
            }

            return result;
        }
    }

    public sealed partial class SaveData
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, npc, tag);
        }
    }

    public sealed partial class LoadData
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, npc, tag);
        }
    }

    public sealed partial class ChatBubblePosition
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        )
        {
            Event?.Invoke(self, npc, ref position, ref spriteEffects);
        }
    }

    public sealed partial class PartyHatPosition
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        )
        {
            Event?.Invoke(self, npc, ref position, ref spriteEffects);
        }
    }

    public sealed partial class EmoteBubblePosition
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalNPC self,
            Terraria.NPC npc,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        )
        {
            Event?.Invoke(self, npc, ref position, ref spriteEffects);
        }
    }
}

public sealed partial class GlobalNPCImpl : Terraria.ModLoader.GlobalNPC
{
    public override void SetDefaultsFromNetId(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.SetDefaultsFromNetId.GetInvocationList().Any())
        {
            base.SetDefaultsFromNetId(
                npc
            );
            return;
        }

        GlobalNPCHooks.SetDefaultsFromNetId.Invoke(
            this,
            npc
        );
    }

    public override void OnSpawn(
        Terraria.NPC npc,
        Terraria.DataStructures.IEntitySource source
    )
    {
        if (!GlobalNPCHooks.OnSpawn.GetInvocationList().Any())
        {
            base.OnSpawn(
                npc,
                source
            );
            return;
        }

        GlobalNPCHooks.OnSpawn.Invoke(
            this,
            npc,
            source
        );
    }

    public override void ApplyDifficultyAndPlayerScaling(
        Terraria.NPC npc,
        int numPlayers,
        float balance,
        float bossAdjustment
    )
    {
        if (!GlobalNPCHooks.ApplyDifficultyAndPlayerScaling.GetInvocationList().Any())
        {
            base.ApplyDifficultyAndPlayerScaling(
                npc,
                numPlayers,
                balance,
                bossAdjustment
            );
            return;
        }

        GlobalNPCHooks.ApplyDifficultyAndPlayerScaling.Invoke(
            this,
            npc,
            numPlayers,
            balance,
            bossAdjustment
        );
    }

    public override void SetBestiary(
        Terraria.NPC npc,
        Terraria.GameContent.Bestiary.BestiaryDatabase database,
        Terraria.GameContent.Bestiary.BestiaryEntry bestiaryEntry
    )
    {
        if (!GlobalNPCHooks.SetBestiary.GetInvocationList().Any())
        {
            base.SetBestiary(
                npc,
                database,
                bestiaryEntry
            );
            return;
        }

        GlobalNPCHooks.SetBestiary.Invoke(
            this,
            npc,
            database,
            bestiaryEntry
        );
    }

    public override void ModifyTypeName(
        Terraria.NPC npc,
        ref string typeName
    )
    {
        if (!GlobalNPCHooks.ModifyTypeName.GetInvocationList().Any())
        {
            base.ModifyTypeName(
                npc,
                ref typeName
            );
            return;
        }

        GlobalNPCHooks.ModifyTypeName.Invoke(
            this,
            npc,
            ref typeName
        );
    }

    public override void ModifyHoverBoundingBox(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Rectangle boundingBox
    )
    {
        if (!GlobalNPCHooks.ModifyHoverBoundingBox.GetInvocationList().Any())
        {
            base.ModifyHoverBoundingBox(
                npc,
                ref boundingBox
            );
            return;
        }

        GlobalNPCHooks.ModifyHoverBoundingBox.Invoke(
            this,
            npc,
            ref boundingBox
        );
    }

    public override void ModifyNPCNameList(
        Terraria.NPC npc,
        System.Collections.Generic.List<string> nameList
    )
    {
        if (!GlobalNPCHooks.ModifyNPCNameList.GetInvocationList().Any())
        {
            base.ModifyNPCNameList(
                npc,
                nameList
            );
            return;
        }

        GlobalNPCHooks.ModifyNPCNameList.Invoke(
            this,
            npc,
            nameList
        );
    }

    public override void ResetEffects(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.ResetEffects.GetInvocationList().Any())
        {
            base.ResetEffects(
                npc
            );
            return;
        }

        GlobalNPCHooks.ResetEffects.Invoke(
            this,
            npc
        );
    }

    public override bool PreAI(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.PreAI.GetInvocationList().Any())
        {
            return base.PreAI(
                npc
            );
        }

        return GlobalNPCHooks.PreAI.Invoke(
            this,
            npc
        );
    }

    public override void AI(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.AI.GetInvocationList().Any())
        {
            base.AI(
                npc
            );
            return;
        }

        GlobalNPCHooks.AI.Invoke(
            this,
            npc
        );
    }

    public override void PostAI(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.PostAI.GetInvocationList().Any())
        {
            base.PostAI(
                npc
            );
            return;
        }

        GlobalNPCHooks.PostAI.Invoke(
            this,
            npc
        );
    }

    public override void FindFrame(
        Terraria.NPC npc,
        int frameHeight
    )
    {
        if (!GlobalNPCHooks.FindFrame.GetInvocationList().Any())
        {
            base.FindFrame(
                npc,
                frameHeight
            );
            return;
        }

        GlobalNPCHooks.FindFrame.Invoke(
            this,
            npc,
            frameHeight
        );
    }

    public override void HitEffect(
        Terraria.NPC npc,
        Terraria.NPC.HitInfo hit
    )
    {
        if (!GlobalNPCHooks.HitEffect.GetInvocationList().Any())
        {
            base.HitEffect(
                npc,
                hit
            );
            return;
        }

        GlobalNPCHooks.HitEffect.Invoke(
            this,
            npc,
            hit
        );
    }

    public override void UpdateLifeRegen(
        Terraria.NPC npc,
        ref int damage
    )
    {
        if (!GlobalNPCHooks.UpdateLifeRegen.GetInvocationList().Any())
        {
            base.UpdateLifeRegen(
                npc,
                ref damage
            );
            return;
        }

        GlobalNPCHooks.UpdateLifeRegen.Invoke(
            this,
            npc,
            ref damage
        );
    }

    public override bool CheckActive(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.CheckActive.GetInvocationList().Any())
        {
            return base.CheckActive(
                npc
            );
        }

        return GlobalNPCHooks.CheckActive.Invoke(
            this,
            npc
        );
    }

    public override bool CheckDead(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.CheckDead.GetInvocationList().Any())
        {
            return base.CheckDead(
                npc
            );
        }

        return GlobalNPCHooks.CheckDead.Invoke(
            this,
            npc
        );
    }

    public override bool PreKill(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.PreKill.GetInvocationList().Any())
        {
            return base.PreKill(
                npc
            );
        }

        return GlobalNPCHooks.PreKill.Invoke(
            this,
            npc
        );
    }

    public override void OnKill(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.OnKill.GetInvocationList().Any())
        {
            base.OnKill(
                npc
            );
            return;
        }

        GlobalNPCHooks.OnKill.Invoke(
            this,
            npc
        );
    }

    public override void OnCaughtBy(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item,
        bool failed
    )
    {
        if (!GlobalNPCHooks.OnCaughtBy.GetInvocationList().Any())
        {
            base.OnCaughtBy(
                npc,
                player,
                item,
                failed
            );
            return;
        }

        GlobalNPCHooks.OnCaughtBy.Invoke(
            this,
            npc,
            player,
            item,
            failed
        );
    }

    public override void ModifyNPCLoot(
        Terraria.NPC npc,
        Terraria.ModLoader.NPCLoot npcLoot
    )
    {
        if (!GlobalNPCHooks.ModifyNPCLoot.GetInvocationList().Any())
        {
            base.ModifyNPCLoot(
                npc,
                npcLoot
            );
            return;
        }

        GlobalNPCHooks.ModifyNPCLoot.Invoke(
            this,
            npc,
            npcLoot
        );
    }

    public override void ModifyGlobalLoot(
        Terraria.ModLoader.GlobalLoot globalLoot
    )
    {
        if (!GlobalNPCHooks.ModifyGlobalLoot.GetInvocationList().Any())
        {
            base.ModifyGlobalLoot(
                globalLoot
            );
            return;
        }

        GlobalNPCHooks.ModifyGlobalLoot.Invoke(
            this,
            globalLoot
        );
    }

    public override bool CanHitPlayer(
        Terraria.NPC npc,
        Terraria.Player target,
        ref int cooldownSlot
    )
    {
        if (!GlobalNPCHooks.CanHitPlayer.GetInvocationList().Any())
        {
            return base.CanHitPlayer(
                npc,
                target,
                ref cooldownSlot
            );
        }

        return GlobalNPCHooks.CanHitPlayer.Invoke(
            this,
            npc,
            target,
            ref cooldownSlot
        );
    }

    public override void ModifyHitPlayer(
        Terraria.NPC npc,
        Terraria.Player target,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        if (!GlobalNPCHooks.ModifyHitPlayer.GetInvocationList().Any())
        {
            base.ModifyHitPlayer(
                npc,
                target,
                ref modifiers
            );
            return;
        }

        GlobalNPCHooks.ModifyHitPlayer.Invoke(
            this,
            npc,
            target,
            ref modifiers
        );
    }

    public override void OnHitPlayer(
        Terraria.NPC npc,
        Terraria.Player target,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        if (!GlobalNPCHooks.OnHitPlayer.GetInvocationList().Any())
        {
            base.OnHitPlayer(
                npc,
                target,
                hurtInfo
            );
            return;
        }

        GlobalNPCHooks.OnHitPlayer.Invoke(
            this,
            npc,
            target,
            hurtInfo
        );
    }

    public override bool CanHitNPC(
        Terraria.NPC npc,
        Terraria.NPC target
    )
    {
        if (!GlobalNPCHooks.CanHitNPC.GetInvocationList().Any())
        {
            return base.CanHitNPC(
                npc,
                target
            );
        }

        return GlobalNPCHooks.CanHitNPC.Invoke(
            this,
            npc,
            target
        );
    }

    public override bool CanBeHitByNPC(
        Terraria.NPC npc,
        Terraria.NPC attacker
    )
    {
        if (!GlobalNPCHooks.CanBeHitByNPC.GetInvocationList().Any())
        {
            return base.CanBeHitByNPC(
                npc,
                attacker
            );
        }

        return GlobalNPCHooks.CanBeHitByNPC.Invoke(
            this,
            npc,
            attacker
        );
    }

    public override void ModifyHitNPC(
        Terraria.NPC npc,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!GlobalNPCHooks.ModifyHitNPC.GetInvocationList().Any())
        {
            base.ModifyHitNPC(
                npc,
                target,
                ref modifiers
            );
            return;
        }

        GlobalNPCHooks.ModifyHitNPC.Invoke(
            this,
            npc,
            target,
            ref modifiers
        );
    }

    public override void OnHitNPC(
        Terraria.NPC npc,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit
    )
    {
        if (!GlobalNPCHooks.OnHitNPC.GetInvocationList().Any())
        {
            base.OnHitNPC(
                npc,
                target,
                hit
            );
            return;
        }

        GlobalNPCHooks.OnHitNPC.Invoke(
            this,
            npc,
            target,
            hit
        );
    }

    public override void ModifyHitByItem(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!GlobalNPCHooks.ModifyHitByItem.GetInvocationList().Any())
        {
            base.ModifyHitByItem(
                npc,
                player,
                item,
                ref modifiers
            );
            return;
        }

        GlobalNPCHooks.ModifyHitByItem.Invoke(
            this,
            npc,
            player,
            item,
            ref modifiers
        );
    }

    public override void OnHitByItem(
        Terraria.NPC npc,
        Terraria.Player player,
        Terraria.Item item,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        if (!GlobalNPCHooks.OnHitByItem.GetInvocationList().Any())
        {
            base.OnHitByItem(
                npc,
                player,
                item,
                hit,
                damageDone
            );
            return;
        }

        GlobalNPCHooks.OnHitByItem.Invoke(
            this,
            npc,
            player,
            item,
            hit,
            damageDone
        );
    }

    public override void ModifyHitByProjectile(
        Terraria.NPC npc,
        Terraria.Projectile projectile,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!GlobalNPCHooks.ModifyHitByProjectile.GetInvocationList().Any())
        {
            base.ModifyHitByProjectile(
                npc,
                projectile,
                ref modifiers
            );
            return;
        }

        GlobalNPCHooks.ModifyHitByProjectile.Invoke(
            this,
            npc,
            projectile,
            ref modifiers
        );
    }

    public override void OnHitByProjectile(
        Terraria.NPC npc,
        Terraria.Projectile projectile,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        if (!GlobalNPCHooks.OnHitByProjectile.GetInvocationList().Any())
        {
            base.OnHitByProjectile(
                npc,
                projectile,
                hit,
                damageDone
            );
            return;
        }

        GlobalNPCHooks.OnHitByProjectile.Invoke(
            this,
            npc,
            projectile,
            hit,
            damageDone
        );
    }

    public override void ModifyIncomingHit(
        Terraria.NPC npc,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!GlobalNPCHooks.ModifyIncomingHit.GetInvocationList().Any())
        {
            base.ModifyIncomingHit(
                npc,
                ref modifiers
            );
            return;
        }

        GlobalNPCHooks.ModifyIncomingHit.Invoke(
            this,
            npc,
            ref modifiers
        );
    }

    public override void BossHeadSlot(
        Terraria.NPC npc,
        ref int index
    )
    {
        if (!GlobalNPCHooks.BossHeadSlot.GetInvocationList().Any())
        {
            base.BossHeadSlot(
                npc,
                ref index
            );
            return;
        }

        GlobalNPCHooks.BossHeadSlot.Invoke(
            this,
            npc,
            ref index
        );
    }

    public override void BossHeadRotation(
        Terraria.NPC npc,
        ref float rotation
    )
    {
        if (!GlobalNPCHooks.BossHeadRotation.GetInvocationList().Any())
        {
            base.BossHeadRotation(
                npc,
                ref rotation
            );
            return;
        }

        GlobalNPCHooks.BossHeadRotation.Invoke(
            this,
            npc,
            ref rotation
        );
    }

    public override void BossHeadSpriteEffects(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        if (!GlobalNPCHooks.BossHeadSpriteEffects.GetInvocationList().Any())
        {
            base.BossHeadSpriteEffects(
                npc,
                ref spriteEffects
            );
            return;
        }

        GlobalNPCHooks.BossHeadSpriteEffects.Invoke(
            this,
            npc,
            ref spriteEffects
        );
    }

    public override void DrawEffects(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Color drawColor
    )
    {
        if (!GlobalNPCHooks.DrawEffects.GetInvocationList().Any())
        {
            base.DrawEffects(
                npc,
                ref drawColor
            );
            return;
        }

        GlobalNPCHooks.DrawEffects.Invoke(
            this,
            npc,
            ref drawColor
        );
    }

    public override bool PreDraw(
        Terraria.NPC npc,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Vector2 screenPos,
        Microsoft.Xna.Framework.Color drawColor
    )
    {
        if (!GlobalNPCHooks.PreDraw.GetInvocationList().Any())
        {
            return base.PreDraw(
                npc,
                spriteBatch,
                screenPos,
                drawColor
            );
        }

        return GlobalNPCHooks.PreDraw.Invoke(
            this,
            npc,
            spriteBatch,
            screenPos,
            drawColor
        );
    }

    public override void PostDraw(
        Terraria.NPC npc,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Vector2 screenPos,
        Microsoft.Xna.Framework.Color drawColor
    )
    {
        if (!GlobalNPCHooks.PostDraw.GetInvocationList().Any())
        {
            base.PostDraw(
                npc,
                spriteBatch,
                screenPos,
                drawColor
            );
            return;
        }

        GlobalNPCHooks.PostDraw.Invoke(
            this,
            npc,
            spriteBatch,
            screenPos,
            drawColor
        );
    }

    public override void DrawBehind(
        Terraria.NPC npc,
        int index
    )
    {
        if (!GlobalNPCHooks.DrawBehind.GetInvocationList().Any())
        {
            base.DrawBehind(
                npc,
                index
            );
            return;
        }

        GlobalNPCHooks.DrawBehind.Invoke(
            this,
            npc,
            index
        );
    }

    public override void EditSpawnRate(
        Terraria.Player player,
        ref int spawnRate,
        ref int maxSpawns
    )
    {
        if (!GlobalNPCHooks.EditSpawnRate.GetInvocationList().Any())
        {
            base.EditSpawnRate(
                player,
                ref spawnRate,
                ref maxSpawns
            );
            return;
        }

        GlobalNPCHooks.EditSpawnRate.Invoke(
            this,
            player,
            ref spawnRate,
            ref maxSpawns
        );
    }

    public override void EditSpawnRange(
        Terraria.Player player,
        ref int spawnRangeX,
        ref int spawnRangeY,
        ref int safeRangeX,
        ref int safeRangeY
    )
    {
        if (!GlobalNPCHooks.EditSpawnRange.GetInvocationList().Any())
        {
            base.EditSpawnRange(
                player,
                ref spawnRangeX,
                ref spawnRangeY,
                ref safeRangeX,
                ref safeRangeY
            );
            return;
        }

        GlobalNPCHooks.EditSpawnRange.Invoke(
            this,
            player,
            ref spawnRangeX,
            ref spawnRangeY,
            ref safeRangeX,
            ref safeRangeY
        );
    }

    public override void EditSpawnPool(
        System.Collections.Generic.IDictionary<int, float> pool,
        Terraria.ModLoader.NPCSpawnInfo spawnInfo
    )
    {
        if (!GlobalNPCHooks.EditSpawnPool.GetInvocationList().Any())
        {
            base.EditSpawnPool(
                pool,
                spawnInfo
            );
            return;
        }

        GlobalNPCHooks.EditSpawnPool.Invoke(
            this,
            pool,
            spawnInfo
        );
    }

    public override void SpawnNPC(
        int npc,
        int tileX,
        int tileY
    )
    {
        if (!GlobalNPCHooks.SpawnNPC.GetInvocationList().Any())
        {
            base.SpawnNPC(
                npc,
                tileX,
                tileY
            );
            return;
        }

        GlobalNPCHooks.SpawnNPC.Invoke(
            this,
            npc,
            tileX,
            tileY
        );
    }

    public override void GetChat(
        Terraria.NPC npc,
        ref string chat
    )
    {
        if (!GlobalNPCHooks.GetChat.GetInvocationList().Any())
        {
            base.GetChat(
                npc,
                ref chat
            );
            return;
        }

        GlobalNPCHooks.GetChat.Invoke(
            this,
            npc,
            ref chat
        );
    }

    public override bool PreChatButtonClicked(
        Terraria.NPC npc,
        bool firstButton
    )
    {
        if (!GlobalNPCHooks.PreChatButtonClicked.GetInvocationList().Any())
        {
            return base.PreChatButtonClicked(
                npc,
                firstButton
            );
        }

        return GlobalNPCHooks.PreChatButtonClicked.Invoke(
            this,
            npc,
            firstButton
        );
    }

    public override void OnChatButtonClicked(
        Terraria.NPC npc,
        bool firstButton
    )
    {
        if (!GlobalNPCHooks.OnChatButtonClicked.GetInvocationList().Any())
        {
            base.OnChatButtonClicked(
                npc,
                firstButton
            );
            return;
        }

        GlobalNPCHooks.OnChatButtonClicked.Invoke(
            this,
            npc,
            firstButton
        );
    }

    public override void ModifyShop(
        Terraria.ModLoader.NPCShop shop
    )
    {
        if (!GlobalNPCHooks.ModifyShop.GetInvocationList().Any())
        {
            base.ModifyShop(
                shop
            );
            return;
        }

        GlobalNPCHooks.ModifyShop.Invoke(
            this,
            shop
        );
    }

    public override void ModifyActiveShop(
        Terraria.NPC npc,
        string shopName,
        Terraria.Item[] items
    )
    {
        if (!GlobalNPCHooks.ModifyActiveShop.GetInvocationList().Any())
        {
            base.ModifyActiveShop(
                npc,
                shopName,
                items
            );
            return;
        }

        GlobalNPCHooks.ModifyActiveShop.Invoke(
            this,
            npc,
            shopName,
            items
        );
    }

    public override void SetupTravelShop(
        int[] shop,
        ref int nextSlot
    )
    {
        if (!GlobalNPCHooks.SetupTravelShop.GetInvocationList().Any())
        {
            base.SetupTravelShop(
                shop,
                ref nextSlot
            );
            return;
        }

        GlobalNPCHooks.SetupTravelShop.Invoke(
            this,
            shop,
            ref nextSlot
        );
    }

    public override void OnGoToStatue(
        Terraria.NPC npc,
        bool toKingStatue
    )
    {
        if (!GlobalNPCHooks.OnGoToStatue.GetInvocationList().Any())
        {
            base.OnGoToStatue(
                npc,
                toKingStatue
            );
            return;
        }

        GlobalNPCHooks.OnGoToStatue.Invoke(
            this,
            npc,
            toKingStatue
        );
    }

    public override void BuffTownNPC(
        ref float damageMult,
        ref int defense
    )
    {
        if (!GlobalNPCHooks.BuffTownNPC.GetInvocationList().Any())
        {
            base.BuffTownNPC(
                ref damageMult,
                ref defense
            );
            return;
        }

        GlobalNPCHooks.BuffTownNPC.Invoke(
            this,
            ref damageMult,
            ref defense
        );
    }

    public override bool ModifyDeathMessage(
        Terraria.NPC npc,
        ref Terraria.Localization.NetworkText customText,
        ref Microsoft.Xna.Framework.Color color
    )
    {
        if (!GlobalNPCHooks.ModifyDeathMessage.GetInvocationList().Any())
        {
            return base.ModifyDeathMessage(
                npc,
                ref customText,
                ref color
            );
        }

        return GlobalNPCHooks.ModifyDeathMessage.Invoke(
            this,
            npc,
            ref customText,
            ref color
        );
    }

    public override void TownNPCAttackStrength(
        Terraria.NPC npc,
        ref int damage,
        ref float knockback
    )
    {
        if (!GlobalNPCHooks.TownNPCAttackStrength.GetInvocationList().Any())
        {
            base.TownNPCAttackStrength(
                npc,
                ref damage,
                ref knockback
            );
            return;
        }

        GlobalNPCHooks.TownNPCAttackStrength.Invoke(
            this,
            npc,
            ref damage,
            ref knockback
        );
    }

    public override void TownNPCAttackCooldown(
        Terraria.NPC npc,
        ref int cooldown,
        ref int randExtraCooldown
    )
    {
        if (!GlobalNPCHooks.TownNPCAttackCooldown.GetInvocationList().Any())
        {
            base.TownNPCAttackCooldown(
                npc,
                ref cooldown,
                ref randExtraCooldown
            );
            return;
        }

        GlobalNPCHooks.TownNPCAttackCooldown.Invoke(
            this,
            npc,
            ref cooldown,
            ref randExtraCooldown
        );
    }

    public override void TownNPCAttackProj(
        Terraria.NPC npc,
        ref int projType,
        ref int attackDelay
    )
    {
        if (!GlobalNPCHooks.TownNPCAttackProj.GetInvocationList().Any())
        {
            base.TownNPCAttackProj(
                npc,
                ref projType,
                ref attackDelay
            );
            return;
        }

        GlobalNPCHooks.TownNPCAttackProj.Invoke(
            this,
            npc,
            ref projType,
            ref attackDelay
        );
    }

    public override void TownNPCAttackProjSpeed(
        Terraria.NPC npc,
        ref float multiplier,
        ref float gravityCorrection,
        ref float randomOffset
    )
    {
        if (!GlobalNPCHooks.TownNPCAttackProjSpeed.GetInvocationList().Any())
        {
            base.TownNPCAttackProjSpeed(
                npc,
                ref multiplier,
                ref gravityCorrection,
                ref randomOffset
            );
            return;
        }

        GlobalNPCHooks.TownNPCAttackProjSpeed.Invoke(
            this,
            npc,
            ref multiplier,
            ref gravityCorrection,
            ref randomOffset
        );
    }

    public override void TownNPCAttackShoot(
        Terraria.NPC npc,
        ref bool inBetweenShots
    )
    {
        if (!GlobalNPCHooks.TownNPCAttackShoot.GetInvocationList().Any())
        {
            base.TownNPCAttackShoot(
                npc,
                ref inBetweenShots
            );
            return;
        }

        GlobalNPCHooks.TownNPCAttackShoot.Invoke(
            this,
            npc,
            ref inBetweenShots
        );
    }

    public override void TownNPCAttackMagic(
        Terraria.NPC npc,
        ref float auraLightMultiplier
    )
    {
        if (!GlobalNPCHooks.TownNPCAttackMagic.GetInvocationList().Any())
        {
            base.TownNPCAttackMagic(
                npc,
                ref auraLightMultiplier
            );
            return;
        }

        GlobalNPCHooks.TownNPCAttackMagic.Invoke(
            this,
            npc,
            ref auraLightMultiplier
        );
    }

    public override void TownNPCAttackSwing(
        Terraria.NPC npc,
        ref int itemWidth,
        ref int itemHeight
    )
    {
        if (!GlobalNPCHooks.TownNPCAttackSwing.GetInvocationList().Any())
        {
            base.TownNPCAttackSwing(
                npc,
                ref itemWidth,
                ref itemHeight
            );
            return;
        }

        GlobalNPCHooks.TownNPCAttackSwing.Invoke(
            this,
            npc,
            ref itemWidth,
            ref itemHeight
        );
    }

    public override void DrawTownAttackGun(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Graphics.Texture2D item,
        ref Microsoft.Xna.Framework.Rectangle itemFrame,
        ref float scale,
        ref int horizontalHoldoutOffset
    )
    {
        if (!GlobalNPCHooks.DrawTownAttackGun.GetInvocationList().Any())
        {
            base.DrawTownAttackGun(
                npc,
                ref item,
                ref itemFrame,
                ref scale,
                ref horizontalHoldoutOffset
            );
            return;
        }

        GlobalNPCHooks.DrawTownAttackGun.Invoke(
            this,
            npc,
            ref item,
            ref itemFrame,
            ref scale,
            ref horizontalHoldoutOffset
        );
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
        if (!GlobalNPCHooks.DrawTownAttackSwing.GetInvocationList().Any())
        {
            base.DrawTownAttackSwing(
                npc,
                ref item,
                ref itemFrame,
                ref itemSize,
                ref scale,
                ref offset
            );
            return;
        }

        GlobalNPCHooks.DrawTownAttackSwing.Invoke(
            this,
            npc,
            ref item,
            ref itemFrame,
            ref itemSize,
            ref scale,
            ref offset
        );
    }

    public override bool ModifyCollisionData(
        Terraria.NPC npc,
        Microsoft.Xna.Framework.Rectangle victimHitbox,
        ref int immunityCooldownSlot,
        ref Terraria.ModLoader.MultipliableFloat damageMultiplier,
        ref Microsoft.Xna.Framework.Rectangle npcHitbox
    )
    {
        if (!GlobalNPCHooks.ModifyCollisionData.GetInvocationList().Any())
        {
            return base.ModifyCollisionData(
                npc,
                victimHitbox,
                ref immunityCooldownSlot,
                ref damageMultiplier,
                ref npcHitbox
            );
        }

        return GlobalNPCHooks.ModifyCollisionData.Invoke(
            this,
            npc,
            victimHitbox,
            ref immunityCooldownSlot,
            ref damageMultiplier,
            ref npcHitbox
        );
    }

    public override bool NeedSaving(
        Terraria.NPC npc
    )
    {
        if (!GlobalNPCHooks.NeedSaving.GetInvocationList().Any())
        {
            return base.NeedSaving(
                npc
            );
        }

        return GlobalNPCHooks.NeedSaving.Invoke(
            this,
            npc
        );
    }

    public override void SaveData(
        Terraria.NPC npc,
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!GlobalNPCHooks.SaveData.GetInvocationList().Any())
        {
            base.SaveData(
                npc,
                tag
            );
            return;
        }

        GlobalNPCHooks.SaveData.Invoke(
            this,
            npc,
            tag
        );
    }

    public override void LoadData(
        Terraria.NPC npc,
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!GlobalNPCHooks.LoadData.GetInvocationList().Any())
        {
            base.LoadData(
                npc,
                tag
            );
            return;
        }

        GlobalNPCHooks.LoadData.Invoke(
            this,
            npc,
            tag
        );
    }

    public override void ChatBubblePosition(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        if (!GlobalNPCHooks.ChatBubblePosition.GetInvocationList().Any())
        {
            base.ChatBubblePosition(
                npc,
                ref position,
                ref spriteEffects
            );
            return;
        }

        GlobalNPCHooks.ChatBubblePosition.Invoke(
            this,
            npc,
            ref position,
            ref spriteEffects
        );
    }

    public override void PartyHatPosition(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        if (!GlobalNPCHooks.PartyHatPosition.GetInvocationList().Any())
        {
            base.PartyHatPosition(
                npc,
                ref position,
                ref spriteEffects
            );
            return;
        }

        GlobalNPCHooks.PartyHatPosition.Invoke(
            this,
            npc,
            ref position,
            ref spriteEffects
        );
    }

    public override void EmoteBubblePosition(
        Terraria.NPC npc,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        if (!GlobalNPCHooks.EmoteBubblePosition.GetInvocationList().Any())
        {
            base.EmoteBubblePosition(
                npc,
                ref position,
                ref spriteEffects
            );
            return;
        }

        GlobalNPCHooks.EmoteBubblePosition.Invoke(
            this,
            npc,
            ref position,
            ref spriteEffects
        );
    }
}

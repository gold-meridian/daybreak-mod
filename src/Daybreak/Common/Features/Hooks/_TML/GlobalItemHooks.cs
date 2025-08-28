namespace Daybreak.Common.Features.Hooks;

using System.Linq;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalItem':
//     System.Void Terraria.ModLoader.GlobalItem::OnCreated(Terraria.Item,Terraria.DataStructures.ItemCreationContext)
//     System.Void Terraria.ModLoader.GlobalItem::OnSpawn(Terraria.Item,Terraria.DataStructures.IEntitySource)
//     System.Boolean Terraria.ModLoader.GlobalItem::AllowPrefix(Terraria.Item,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanUseItem(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::UseStyle(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle)
//     System.Void Terraria.ModLoader.GlobalItem::HoldStyle(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle)
//     System.Void Terraria.ModLoader.GlobalItem::HoldItem(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::GetHealLife(Terraria.Item,Terraria.Player,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::GetHealMana(Terraria.Item,Terraria.Player,System.Boolean,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyManaCost(Terraria.Item,Terraria.Player,System.Single&,System.Single&)
//     System.Void Terraria.ModLoader.GlobalItem::OnMissingMana(Terraria.Item,Terraria.Player,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::OnConsumeMana(Terraria.Item,Terraria.Player,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyWeaponDamage(Terraria.Item,Terraria.Player,Terraria.ModLoader.StatModifier&)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyResearchSorting(Terraria.Item,Terraria.ID.ContentSamples/CreativeHelper/ItemGroup&)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanResearch(Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalItem::OnResearched(Terraria.Item,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyWeaponKnockback(Terraria.Item,Terraria.Player,Terraria.ModLoader.StatModifier&)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyWeaponCrit(Terraria.Item,Terraria.Player,System.Single&)
//     System.Boolean Terraria.ModLoader.GlobalItem::NeedsAmmo(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::PickAmmo(Terraria.Item,Terraria.Item,Terraria.Player,System.Int32&,System.Single&,Terraria.ModLoader.StatModifier&,System.Single&)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanConsumeAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::OnConsumeAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::OnConsumedAsAmmo(Terraria.Item,Terraria.Item,Terraria.Player)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanShoot(Terraria.Item,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyShootStats(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Vector2&,System.Int32&,System.Int32&,System.Single&)
//     System.Boolean Terraria.ModLoader.GlobalItem::Shoot(Terraria.Item,Terraria.Player,Terraria.DataStructures.EntitySource_ItemUse_WithAmmo,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Vector2,System.Int32,System.Int32,System.Single)
//     System.Void Terraria.ModLoader.GlobalItem::UseItemHitbox(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle&,System.Boolean&)
//     System.Void Terraria.ModLoader.GlobalItem::MeleeEffects(Terraria.Item,Terraria.Player,Microsoft.Xna.Framework.Rectangle)
//     System.Void Terraria.ModLoader.GlobalItem::OnCatchNPC(Terraria.Item,Terraria.NPC,Terraria.Player,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyItemScale(Terraria.Item,Terraria.Player,System.Single&)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyHitNPC(Terraria.Item,Terraria.Player,Terraria.NPC,Terraria.NPC/HitModifiers&)
//     System.Void Terraria.ModLoader.GlobalItem::OnHitNPC(Terraria.Item,Terraria.Player,Terraria.NPC,Terraria.NPC/HitInfo,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanHitPvp(Terraria.Item,Terraria.Player,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyHitPvp(Terraria.Item,Terraria.Player,Terraria.Player,Terraria.Player/HurtModifiers&)
//     System.Void Terraria.ModLoader.GlobalItem::OnHitPvp(Terraria.Item,Terraria.Player,Terraria.Player,Terraria.Player/HurtInfo)
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
//     System.Void Terraria.ModLoader.GlobalItem::UpdateArmorSet(Terraria.Player,System.String)
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
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawInWorld(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,System.Single&,System.Single&,System.Int32)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawInWorld(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,System.Single,System.Single,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawInInventory(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Vector2,System.Single)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawInInventory(Terraria.Item,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Color,Microsoft.Xna.Framework.Vector2,System.Single)
//     System.Boolean Terraria.ModLoader.GlobalItem::CanEquipAccessory(Terraria.Item,Terraria.Player,System.Int32,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalItem::ExtractinatorUse(System.Int32,System.Int32,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::CaughtFishStack(System.Int32,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::AnglerChat(System.Int32,System.String&,System.String&)
//     System.Void Terraria.ModLoader.GlobalItem::AddRecipes()
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawTooltip(Terraria.Item,System.Collections.ObjectModel.ReadOnlyCollection`1<Terraria.ModLoader.TooltipLine>,System.Int32&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawTooltip(Terraria.Item,System.Collections.ObjectModel.ReadOnlyCollection`1<Terraria.ModLoader.DrawableTooltipLine>)
//     System.Boolean Terraria.ModLoader.GlobalItem::PreDrawTooltipLine(Terraria.Item,Terraria.ModLoader.DrawableTooltipLine,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalItem::PostDrawTooltipLine(Terraria.Item,Terraria.ModLoader.DrawableTooltipLine)
//     System.Void Terraria.ModLoader.GlobalItem::ModifyTooltips(Terraria.Item,System.Collections.Generic.List`1<Terraria.ModLoader.TooltipLine>)
//     System.Void Terraria.ModLoader.GlobalItem::SaveData(Terraria.Item,Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.GlobalItem::LoadData(Terraria.Item,Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.GlobalItem::NetSend(Terraria.Item,System.IO.BinaryWriter)
//     System.Void Terraria.ModLoader.GlobalItem::NetReceive(Terraria.Item,System.IO.BinaryReader)
public static partial class GlobalItemHooks
{
    public sealed partial class OnCreated
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.DataStructures.ItemCreationContext context
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.DataStructures.ItemCreationContext context
        )
        {
            Event?.Invoke(self, item, context);
        }
    }

    public sealed partial class OnSpawn
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.DataStructures.IEntitySource source
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.DataStructures.IEntitySource source
        )
        {
            Event?.Invoke(self, item, source);
        }
    }

    public sealed partial class AllowPrefix
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            int pre
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            int pre
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, pre);
            }

            return result;
        }
    }

    public sealed partial class CanUseItem
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class UseStyle
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        )
        {
            Event?.Invoke(self, item, player, heldItemFrame);
        }
    }

    public sealed partial class HoldStyle
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle heldItemFrame
        )
        {
            Event?.Invoke(self, item, player, heldItemFrame);
        }
    }

    public sealed partial class HoldItem
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class GetHealLife
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        )
        {
            Event?.Invoke(self, item, player, quickHeal, ref healValue);
        }
    }

    public sealed partial class GetHealMana
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool quickHeal,
            ref int healValue
        )
        {
            Event?.Invoke(self, item, player, quickHeal, ref healValue);
        }
    }

    public sealed partial class ModifyManaCost
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float reduce,
            ref float mult
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float reduce,
            ref float mult
        )
        {
            Event?.Invoke(self, item, player, ref reduce, ref mult);
        }
    }

    public sealed partial class OnMissingMana
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int neededMana
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int neededMana
        )
        {
            Event?.Invoke(self, item, player, neededMana);
        }
    }

    public sealed partial class OnConsumeMana
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int manaConsumed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int manaConsumed
        )
        {
            Event?.Invoke(self, item, player, manaConsumed);
        }
    }

    public sealed partial class ModifyWeaponDamage
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier damage
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier damage
        )
        {
            Event?.Invoke(self, item, player, ref damage);
        }
    }

    public sealed partial class ModifyResearchSorting
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref Terraria.ID.ContentSamples.CreativeHelper.ItemGroup itemGroup
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref Terraria.ID.ContentSamples.CreativeHelper.ItemGroup itemGroup
        )
        {
            Event?.Invoke(self, item, ref itemGroup);
        }
    }

    public sealed partial class CanResearch
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
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

    public sealed partial class OnResearched
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            bool fullyResearched
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            bool fullyResearched
        )
        {
            Event?.Invoke(self, item, fullyResearched);
        }
    }

    public sealed partial class ModifyWeaponKnockback
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier knockback
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Terraria.ModLoader.StatModifier knockback
        )
        {
            Event?.Invoke(self, item, player, ref knockback);
        }
    }

    public sealed partial class ModifyWeaponCrit
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float crit
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float crit
        )
        {
            Event?.Invoke(self, item, player, ref crit);
        }
    }

    public sealed partial class NeedsAmmo
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class PickAmmo
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player,
            ref int type,
            ref float speed,
            ref Terraria.ModLoader.StatModifier damage,
            ref float knockback
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player,
            ref int type,
            ref float speed,
            ref Terraria.ModLoader.StatModifier damage,
            ref float knockback
        )
        {
            Event?.Invoke(self, weapon, ammo, player, ref type, ref speed, ref damage, ref knockback);
        }
    }

    public sealed partial class CanConsumeAmmo
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, weapon, ammo, player))
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
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item weapon,
            Terraria.Item ammo,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, weapon, ammo, player);
        }
    }

    public sealed partial class OnConsumedAsAmmo
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item ammo,
            Terraria.Item weapon,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, ammo, weapon, player);
        }
    }

    public sealed partial class CanShoot
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyShootStats
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
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
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Vector2 velocity,
            ref int type,
            ref int damage,
            ref float knockback
        )
        {
            Event?.Invoke(self, item, player, ref position, ref velocity, ref type, ref damage, ref knockback);
        }
    }

    public sealed partial class Shoot
    {
        public delegate bool Definition(
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

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
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
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, player, source, position, velocity, type, damage, knockback);
            }

            return result;
        }
    }

    public sealed partial class UseItemHitbox
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Microsoft.Xna.Framework.Rectangle hitbox,
            ref bool noHitbox
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref Microsoft.Xna.Framework.Rectangle hitbox,
            ref bool noHitbox
        )
        {
            Event?.Invoke(self, item, player, ref hitbox, ref noHitbox);
        }
    }

    public sealed partial class MeleeEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle hitbox
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Microsoft.Xna.Framework.Rectangle hitbox
        )
        {
            Event?.Invoke(self, item, player, hitbox);
        }
    }

    public sealed partial class OnCatchNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.NPC npc,
            Terraria.Player player,
            bool failed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.NPC npc,
            Terraria.Player player,
            bool failed
        )
        {
            Event?.Invoke(self, item, npc, player, failed);
        }
    }

    public sealed partial class ModifyItemScale
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float scale
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float scale
        )
        {
            Event?.Invoke(self, item, player, ref scale);
        }
    }

    public sealed partial class ModifyHitNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target,
            ref Terraria.NPC.HitModifiers modifiers
        )
        {
            Event?.Invoke(self, item, player, target, ref modifiers);
        }
    }

    public sealed partial class OnHitNPC
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
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
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.NPC target,
            Terraria.NPC.HitInfo hit,
            int damageDone
        )
        {
            Event?.Invoke(self, item, player, target, hit, damageDone);
        }
    }

    public sealed partial class CanHitPvp
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player, target))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ModifyHitPvp
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            ref Terraria.Player.HurtModifiers modifiers
        )
        {
            Event?.Invoke(self, item, player, target, ref modifiers);
        }
    }

    public sealed partial class OnHitPvp
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            Terraria.Player target,
            Terraria.Player.HurtInfo hurtInfo
        )
        {
            Event?.Invoke(self, item, player, target, hurtInfo);
        }
    }

    public sealed partial class UseAnimation
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class ConsumeItem
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnConsumeItem
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class UseItemFrame
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class HoldItemFrame
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class AltFunctionUse
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, item, player))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class UpdateInventory
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class UpdateInfoAccessory
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class UpdateEquip
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class UpdateAccessory
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        )
        {
            Event?.Invoke(self, item, player, hideVisual);
        }
    }

    public sealed partial class UpdateVanity
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class UpdateVisibleAccessory
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            bool hideVisual
        )
        {
            Event?.Invoke(self, item, player, hideVisual);
        }
    }

    public sealed partial class UpdateItemDye
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int dye,
            bool hideVisual
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int dye,
            bool hideVisual
        )
        {
            Event?.Invoke(self, item, player, dye, hideVisual);
        }
    }

    public sealed partial class UpdateArmorSet
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        )
        {
            Event?.Invoke(self, player, set);
        }
    }

    public sealed partial class PreUpdateVanitySet
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        )
        {
            Event?.Invoke(self, player, set);
        }
    }

    public sealed partial class UpdateVanitySet
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        )
        {
            Event?.Invoke(self, player, set);
        }
    }

    public sealed partial class ArmorSetShadows
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Player player,
            string set
        )
        {
            Event?.Invoke(self, player, set);
        }
    }

    public sealed partial class SetMatch
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            int armorSlot,
            int type,
            bool male,
            ref int equipSlot,
            ref bool robes
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            int armorSlot,
            int type,
            bool male,
            ref int equipSlot,
            ref bool robes
        )
        {
            Event?.Invoke(self, armorSlot, type, male, ref equipSlot, ref robes);
        }
    }

    public sealed partial class CanRightClick
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, item))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class RightClick
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, item, player);
        }
    }

    public sealed partial class ModifyItemLoot
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.ItemLoot itemLoot
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.ItemLoot itemLoot
        )
        {
            Event?.Invoke(self, item, itemLoot);
        }
    }

    public sealed partial class CanStack
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, destination, source))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class CanStackInWorld
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, destination, source))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnStack
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        )
        {
            Event?.Invoke(self, destination, source, numToTransfer);
        }
    }

    public sealed partial class SplitStack
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item destination,
            Terraria.Item source,
            int numToTransfer
        )
        {
            Event?.Invoke(self, destination, source, numToTransfer);
        }
    }

    public sealed partial class ReforgePrice
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref int reforgePrice,
            ref bool canApplyDiscount
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref int reforgePrice,
            ref bool canApplyDiscount
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, ref reforgePrice, ref canApplyDiscount);
            }

            return result;
        }
    }

    public sealed partial class CanReforge
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
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

    public sealed partial class PreReforge
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        )
        {
            Event?.Invoke(self, item);
        }
    }

    public sealed partial class PostReforge
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        )
        {
            Event?.Invoke(self, item);
        }
    }

    public sealed partial class DrawArmorColor
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.ModLoader.EquipType type,
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref Microsoft.Xna.Framework.Color color,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color glowMaskColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.ModLoader.EquipType type,
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref Microsoft.Xna.Framework.Color color,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color glowMaskColor
        )
        {
            Event?.Invoke(self, type, slot, drawPlayer, shadow, ref color, ref glowMask, ref glowMaskColor);
        }
    }

    public sealed partial class ArmorArmGlowMask
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color color
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            int slot,
            Terraria.Player drawPlayer,
            float shadow,
            ref int glowMask,
            ref Microsoft.Xna.Framework.Color color
        )
        {
            Event?.Invoke(self, slot, drawPlayer, shadow, ref glowMask, ref color);
        }
    }

    public sealed partial class VerticalWingSpeeds
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float ascentWhenFalling,
            ref float ascentWhenRising,
            ref float maxCanAscendMultiplier,
            ref float maxAscentMultiplier,
            ref float constantAscend
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float ascentWhenFalling,
            ref float ascentWhenRising,
            ref float maxCanAscendMultiplier,
            ref float maxAscentMultiplier,
            ref float constantAscend
        )
        {
            Event?.Invoke(self, item, player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
        }
    }

    public sealed partial class HorizontalWingSpeeds
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float speed,
            ref float acceleration
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref float speed,
            ref float acceleration
        )
        {
            Event?.Invoke(self, item, player, ref speed, ref acceleration);
        }
    }

    public sealed partial class WingUpdate
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            int wings,
            Terraria.Player player,
            bool inUse
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            int wings,
            Terraria.Player player,
            bool inUse
        )
        {
            var result = false;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result |= handler.Invoke(self, wings, player, inUse);
            }

            return result;
        }
    }

    public sealed partial class Update
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref float gravity,
            ref float maxFallSpeed
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            ref float gravity,
            ref float maxFallSpeed
        )
        {
            Event?.Invoke(self, item, ref gravity, ref maxFallSpeed);
        }
    }

    public sealed partial class PostUpdate
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item
        )
        {
            Event?.Invoke(self, item);
        }
    }

    public sealed partial class GrabRange
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref int grabRange
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            ref int grabRange
        )
        {
            Event?.Invoke(self, item, player, ref grabRange);
        }
    }

    public sealed partial class GrabStyle
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, item, player))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class CanPickup
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class OnPickup
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ItemSpace
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, item, player))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class PreDrawInWorld
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            ref float rotation,
            ref float scale,
            int whoAmI
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            ref float rotation,
            ref float scale,
            int whoAmI
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
            }

            return result;
        }
    }

    public sealed partial class PostDrawInWorld
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            float rotation,
            float scale,
            int whoAmI
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor,
            Microsoft.Xna.Framework.Color alphaColor,
            float rotation,
            float scale,
            int whoAmI
        )
        {
            Event?.Invoke(self, item, spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI);
        }
    }

    public sealed partial class PreDrawInInventory
    {
        public delegate bool Definition(
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

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
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
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
            }

            return result;
        }
    }

    public sealed partial class PostDrawInInventory
    {
        public delegate void Definition(
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

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
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
            Event?.Invoke(self, item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }
    }

    public sealed partial class CanEquipAccessory
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int slot,
            bool modded
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.Player player,
            int slot,
            bool modded
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, item, player, slot, modded))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ExtractinatorUse
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            int extractType,
            int extractinatorBlockType,
            ref int resultType,
            ref int resultStack
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            int extractType,
            int extractinatorBlockType,
            ref int resultType,
            ref int resultStack
        )
        {
            Event?.Invoke(self, extractType, extractinatorBlockType, ref resultType, ref resultStack);
        }
    }

    public sealed partial class CaughtFishStack
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            int type,
            ref int stack
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            int type,
            ref int stack
        )
        {
            Event?.Invoke(self, type, ref stack);
        }
    }

    public sealed partial class AnglerChat
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            int type,
            ref string chat,
            ref string catchLocation
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            int type,
            ref string chat,
            ref string catchLocation
        )
        {
            Event?.Invoke(self, type, ref chat, ref catchLocation);
        }
    }

    public sealed partial class AddRecipes
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreDrawTooltip
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.TooltipLine> lines,
            ref int x,
            ref int y
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.TooltipLine> lines,
            ref int x,
            ref int y
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, lines, ref x, ref y);
            }

            return result;
        }
    }

    public sealed partial class PostDrawTooltip
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.DrawableTooltipLine> lines
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.DrawableTooltipLine> lines
        )
        {
            Event?.Invoke(self, item, lines);
        }
    }

    public sealed partial class PreDrawTooltipLine
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line,
            ref int yOffset
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line,
            ref int yOffset
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, item, line, ref yOffset);
            }

            return result;
        }
    }

    public sealed partial class PostDrawTooltipLine
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.DrawableTooltipLine line
        )
        {
            Event?.Invoke(self, item, line);
        }
    }

    public sealed partial class ModifyTooltips
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips
        )
        {
            Event?.Invoke(self, item, tooltips);
        }
    }

    public sealed partial class SaveData
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, item, tag);
        }
    }

    public sealed partial class LoadData
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, item, tag);
        }
    }

    public sealed partial class NetSend
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.IO.BinaryWriter writer
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.IO.BinaryWriter writer
        )
        {
            Event?.Invoke(self, item, writer);
        }
    }

    public sealed partial class NetReceive
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.IO.BinaryReader reader
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalItem self,
            Terraria.Item item,
            System.IO.BinaryReader reader
        )
        {
            Event?.Invoke(self, item, reader);
        }
    }
}

public sealed partial class GlobalItemImpl : Terraria.ModLoader.GlobalItem
{
    public override void OnCreated(
        Terraria.Item item,
        Terraria.DataStructures.ItemCreationContext context
    )
    {
        if (!GlobalItemHooks.OnCreated.GetInvocationList().Any())
        {
            base.OnCreated(
                item,
                context
            );
            return;
        }

        GlobalItemHooks.OnCreated.Invoke(
            this,
            item,
            context
        );
    }

    public override void OnSpawn(
        Terraria.Item item,
        Terraria.DataStructures.IEntitySource source
    )
    {
        if (!GlobalItemHooks.OnSpawn.GetInvocationList().Any())
        {
            base.OnSpawn(
                item,
                source
            );
            return;
        }

        GlobalItemHooks.OnSpawn.Invoke(
            this,
            item,
            source
        );
    }

    public override bool AllowPrefix(
        Terraria.Item item,
        int pre
    )
    {
        if (!GlobalItemHooks.AllowPrefix.GetInvocationList().Any())
        {
            return base.AllowPrefix(
                item,
                pre
            );
        }

        return GlobalItemHooks.AllowPrefix.Invoke(
            this,
            item,
            pre
        );
    }

    public override bool CanUseItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.CanUseItem.GetInvocationList().Any())
        {
            return base.CanUseItem(
                item,
                player
            );
        }

        return GlobalItemHooks.CanUseItem.Invoke(
            this,
            item,
            player
        );
    }

    public override void UseStyle(
        Terraria.Item item,
        Terraria.Player player,
        Microsoft.Xna.Framework.Rectangle heldItemFrame
    )
    {
        if (!GlobalItemHooks.UseStyle.GetInvocationList().Any())
        {
            base.UseStyle(
                item,
                player,
                heldItemFrame
            );
            return;
        }

        GlobalItemHooks.UseStyle.Invoke(
            this,
            item,
            player,
            heldItemFrame
        );
    }

    public override void HoldStyle(
        Terraria.Item item,
        Terraria.Player player,
        Microsoft.Xna.Framework.Rectangle heldItemFrame
    )
    {
        if (!GlobalItemHooks.HoldStyle.GetInvocationList().Any())
        {
            base.HoldStyle(
                item,
                player,
                heldItemFrame
            );
            return;
        }

        GlobalItemHooks.HoldStyle.Invoke(
            this,
            item,
            player,
            heldItemFrame
        );
    }

    public override void HoldItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.HoldItem.GetInvocationList().Any())
        {
            base.HoldItem(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.HoldItem.Invoke(
            this,
            item,
            player
        );
    }

    public override void GetHealLife(
        Terraria.Item item,
        Terraria.Player player,
        bool quickHeal,
        ref int healValue
    )
    {
        if (!GlobalItemHooks.GetHealLife.GetInvocationList().Any())
        {
            base.GetHealLife(
                item,
                player,
                quickHeal,
                ref healValue
            );
            return;
        }

        GlobalItemHooks.GetHealLife.Invoke(
            this,
            item,
            player,
            quickHeal,
            ref healValue
        );
    }

    public override void GetHealMana(
        Terraria.Item item,
        Terraria.Player player,
        bool quickHeal,
        ref int healValue
    )
    {
        if (!GlobalItemHooks.GetHealMana.GetInvocationList().Any())
        {
            base.GetHealMana(
                item,
                player,
                quickHeal,
                ref healValue
            );
            return;
        }

        GlobalItemHooks.GetHealMana.Invoke(
            this,
            item,
            player,
            quickHeal,
            ref healValue
        );
    }

    public override void ModifyManaCost(
        Terraria.Item item,
        Terraria.Player player,
        ref float reduce,
        ref float mult
    )
    {
        if (!GlobalItemHooks.ModifyManaCost.GetInvocationList().Any())
        {
            base.ModifyManaCost(
                item,
                player,
                ref reduce,
                ref mult
            );
            return;
        }

        GlobalItemHooks.ModifyManaCost.Invoke(
            this,
            item,
            player,
            ref reduce,
            ref mult
        );
    }

    public override void OnMissingMana(
        Terraria.Item item,
        Terraria.Player player,
        int neededMana
    )
    {
        if (!GlobalItemHooks.OnMissingMana.GetInvocationList().Any())
        {
            base.OnMissingMana(
                item,
                player,
                neededMana
            );
            return;
        }

        GlobalItemHooks.OnMissingMana.Invoke(
            this,
            item,
            player,
            neededMana
        );
    }

    public override void OnConsumeMana(
        Terraria.Item item,
        Terraria.Player player,
        int manaConsumed
    )
    {
        if (!GlobalItemHooks.OnConsumeMana.GetInvocationList().Any())
        {
            base.OnConsumeMana(
                item,
                player,
                manaConsumed
            );
            return;
        }

        GlobalItemHooks.OnConsumeMana.Invoke(
            this,
            item,
            player,
            manaConsumed
        );
    }

    public override void ModifyWeaponDamage(
        Terraria.Item item,
        Terraria.Player player,
        ref Terraria.ModLoader.StatModifier damage
    )
    {
        if (!GlobalItemHooks.ModifyWeaponDamage.GetInvocationList().Any())
        {
            base.ModifyWeaponDamage(
                item,
                player,
                ref damage
            );
            return;
        }

        GlobalItemHooks.ModifyWeaponDamage.Invoke(
            this,
            item,
            player,
            ref damage
        );
    }

    public override void ModifyResearchSorting(
        Terraria.Item item,
        ref Terraria.ID.ContentSamples.CreativeHelper.ItemGroup itemGroup
    )
    {
        if (!GlobalItemHooks.ModifyResearchSorting.GetInvocationList().Any())
        {
            base.ModifyResearchSorting(
                item,
                ref itemGroup
            );
            return;
        }

        GlobalItemHooks.ModifyResearchSorting.Invoke(
            this,
            item,
            ref itemGroup
        );
    }

    public override bool CanResearch(
        Terraria.Item item
    )
    {
        if (!GlobalItemHooks.CanResearch.GetInvocationList().Any())
        {
            return base.CanResearch(
                item
            );
        }

        return GlobalItemHooks.CanResearch.Invoke(
            this,
            item
        );
    }

    public override void OnResearched(
        Terraria.Item item,
        bool fullyResearched
    )
    {
        if (!GlobalItemHooks.OnResearched.GetInvocationList().Any())
        {
            base.OnResearched(
                item,
                fullyResearched
            );
            return;
        }

        GlobalItemHooks.OnResearched.Invoke(
            this,
            item,
            fullyResearched
        );
    }

    public override void ModifyWeaponKnockback(
        Terraria.Item item,
        Terraria.Player player,
        ref Terraria.ModLoader.StatModifier knockback
    )
    {
        if (!GlobalItemHooks.ModifyWeaponKnockback.GetInvocationList().Any())
        {
            base.ModifyWeaponKnockback(
                item,
                player,
                ref knockback
            );
            return;
        }

        GlobalItemHooks.ModifyWeaponKnockback.Invoke(
            this,
            item,
            player,
            ref knockback
        );
    }

    public override void ModifyWeaponCrit(
        Terraria.Item item,
        Terraria.Player player,
        ref float crit
    )
    {
        if (!GlobalItemHooks.ModifyWeaponCrit.GetInvocationList().Any())
        {
            base.ModifyWeaponCrit(
                item,
                player,
                ref crit
            );
            return;
        }

        GlobalItemHooks.ModifyWeaponCrit.Invoke(
            this,
            item,
            player,
            ref crit
        );
    }

    public override bool NeedsAmmo(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.NeedsAmmo.GetInvocationList().Any())
        {
            return base.NeedsAmmo(
                item,
                player
            );
        }

        return GlobalItemHooks.NeedsAmmo.Invoke(
            this,
            item,
            player
        );
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
        if (!GlobalItemHooks.PickAmmo.GetInvocationList().Any())
        {
            base.PickAmmo(
                weapon,
                ammo,
                player,
                ref type,
                ref speed,
                ref damage,
                ref knockback
            );
            return;
        }

        GlobalItemHooks.PickAmmo.Invoke(
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

    public override bool CanConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.CanConsumeAmmo.GetInvocationList().Any())
        {
            return base.CanConsumeAmmo(
                weapon,
                ammo,
                player
            );
        }

        return GlobalItemHooks.CanConsumeAmmo.Invoke(
            this,
            weapon,
            ammo,
            player
        );
    }

    public override void OnConsumeAmmo(
        Terraria.Item weapon,
        Terraria.Item ammo,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.OnConsumeAmmo.GetInvocationList().Any())
        {
            base.OnConsumeAmmo(
                weapon,
                ammo,
                player
            );
            return;
        }

        GlobalItemHooks.OnConsumeAmmo.Invoke(
            this,
            weapon,
            ammo,
            player
        );
    }

    public override void OnConsumedAsAmmo(
        Terraria.Item ammo,
        Terraria.Item weapon,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.OnConsumedAsAmmo.GetInvocationList().Any())
        {
            base.OnConsumedAsAmmo(
                ammo,
                weapon,
                player
            );
            return;
        }

        GlobalItemHooks.OnConsumedAsAmmo.Invoke(
            this,
            ammo,
            weapon,
            player
        );
    }

    public override bool CanShoot(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.CanShoot.GetInvocationList().Any())
        {
            return base.CanShoot(
                item,
                player
            );
        }

        return GlobalItemHooks.CanShoot.Invoke(
            this,
            item,
            player
        );
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
        if (!GlobalItemHooks.ModifyShootStats.GetInvocationList().Any())
        {
            base.ModifyShootStats(
                item,
                player,
                ref position,
                ref velocity,
                ref type,
                ref damage,
                ref knockback
            );
            return;
        }

        GlobalItemHooks.ModifyShootStats.Invoke(
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
        if (!GlobalItemHooks.Shoot.GetInvocationList().Any())
        {
            return base.Shoot(
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

        return GlobalItemHooks.Shoot.Invoke(
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

    public override void UseItemHitbox(
        Terraria.Item item,
        Terraria.Player player,
        ref Microsoft.Xna.Framework.Rectangle hitbox,
        ref bool noHitbox
    )
    {
        if (!GlobalItemHooks.UseItemHitbox.GetInvocationList().Any())
        {
            base.UseItemHitbox(
                item,
                player,
                ref hitbox,
                ref noHitbox
            );
            return;
        }

        GlobalItemHooks.UseItemHitbox.Invoke(
            this,
            item,
            player,
            ref hitbox,
            ref noHitbox
        );
    }

    public override void MeleeEffects(
        Terraria.Item item,
        Terraria.Player player,
        Microsoft.Xna.Framework.Rectangle hitbox
    )
    {
        if (!GlobalItemHooks.MeleeEffects.GetInvocationList().Any())
        {
            base.MeleeEffects(
                item,
                player,
                hitbox
            );
            return;
        }

        GlobalItemHooks.MeleeEffects.Invoke(
            this,
            item,
            player,
            hitbox
        );
    }

    public override void OnCatchNPC(
        Terraria.Item item,
        Terraria.NPC npc,
        Terraria.Player player,
        bool failed
    )
    {
        if (!GlobalItemHooks.OnCatchNPC.GetInvocationList().Any())
        {
            base.OnCatchNPC(
                item,
                npc,
                player,
                failed
            );
            return;
        }

        GlobalItemHooks.OnCatchNPC.Invoke(
            this,
            item,
            npc,
            player,
            failed
        );
    }

    public override void ModifyItemScale(
        Terraria.Item item,
        Terraria.Player player,
        ref float scale
    )
    {
        if (!GlobalItemHooks.ModifyItemScale.GetInvocationList().Any())
        {
            base.ModifyItemScale(
                item,
                player,
                ref scale
            );
            return;
        }

        GlobalItemHooks.ModifyItemScale.Invoke(
            this,
            item,
            player,
            ref scale
        );
    }

    public override void ModifyHitNPC(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.NPC target,
        ref Terraria.NPC.HitModifiers modifiers
    )
    {
        if (!GlobalItemHooks.ModifyHitNPC.GetInvocationList().Any())
        {
            base.ModifyHitNPC(
                item,
                player,
                target,
                ref modifiers
            );
            return;
        }

        GlobalItemHooks.ModifyHitNPC.Invoke(
            this,
            item,
            player,
            target,
            ref modifiers
        );
    }

    public override void OnHitNPC(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.NPC target,
        Terraria.NPC.HitInfo hit,
        int damageDone
    )
    {
        if (!GlobalItemHooks.OnHitNPC.GetInvocationList().Any())
        {
            base.OnHitNPC(
                item,
                player,
                target,
                hit,
                damageDone
            );
            return;
        }

        GlobalItemHooks.OnHitNPC.Invoke(
            this,
            item,
            player,
            target,
            hit,
            damageDone
        );
    }

    public override bool CanHitPvp(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.Player target
    )
    {
        if (!GlobalItemHooks.CanHitPvp.GetInvocationList().Any())
        {
            return base.CanHitPvp(
                item,
                player,
                target
            );
        }

        return GlobalItemHooks.CanHitPvp.Invoke(
            this,
            item,
            player,
            target
        );
    }

    public override void ModifyHitPvp(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.Player target,
        ref Terraria.Player.HurtModifiers modifiers
    )
    {
        if (!GlobalItemHooks.ModifyHitPvp.GetInvocationList().Any())
        {
            base.ModifyHitPvp(
                item,
                player,
                target,
                ref modifiers
            );
            return;
        }

        GlobalItemHooks.ModifyHitPvp.Invoke(
            this,
            item,
            player,
            target,
            ref modifiers
        );
    }

    public override void OnHitPvp(
        Terraria.Item item,
        Terraria.Player player,
        Terraria.Player target,
        Terraria.Player.HurtInfo hurtInfo
    )
    {
        if (!GlobalItemHooks.OnHitPvp.GetInvocationList().Any())
        {
            base.OnHitPvp(
                item,
                player,
                target,
                hurtInfo
            );
            return;
        }

        GlobalItemHooks.OnHitPvp.Invoke(
            this,
            item,
            player,
            target,
            hurtInfo
        );
    }

    public override void UseAnimation(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.UseAnimation.GetInvocationList().Any())
        {
            base.UseAnimation(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.UseAnimation.Invoke(
            this,
            item,
            player
        );
    }

    public override bool ConsumeItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.ConsumeItem.GetInvocationList().Any())
        {
            return base.ConsumeItem(
                item,
                player
            );
        }

        return GlobalItemHooks.ConsumeItem.Invoke(
            this,
            item,
            player
        );
    }

    public override void OnConsumeItem(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.OnConsumeItem.GetInvocationList().Any())
        {
            base.OnConsumeItem(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.OnConsumeItem.Invoke(
            this,
            item,
            player
        );
    }

    public override void UseItemFrame(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.UseItemFrame.GetInvocationList().Any())
        {
            base.UseItemFrame(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.UseItemFrame.Invoke(
            this,
            item,
            player
        );
    }

    public override void HoldItemFrame(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.HoldItemFrame.GetInvocationList().Any())
        {
            base.HoldItemFrame(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.HoldItemFrame.Invoke(
            this,
            item,
            player
        );
    }

    public override bool AltFunctionUse(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.AltFunctionUse.GetInvocationList().Any())
        {
            return base.AltFunctionUse(
                item,
                player
            );
        }

        return GlobalItemHooks.AltFunctionUse.Invoke(
            this,
            item,
            player
        );
    }

    public override void UpdateInventory(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.UpdateInventory.GetInvocationList().Any())
        {
            base.UpdateInventory(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.UpdateInventory.Invoke(
            this,
            item,
            player
        );
    }

    public override void UpdateInfoAccessory(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.UpdateInfoAccessory.GetInvocationList().Any())
        {
            base.UpdateInfoAccessory(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.UpdateInfoAccessory.Invoke(
            this,
            item,
            player
        );
    }

    public override void UpdateEquip(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.UpdateEquip.GetInvocationList().Any())
        {
            base.UpdateEquip(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.UpdateEquip.Invoke(
            this,
            item,
            player
        );
    }

    public override void UpdateAccessory(
        Terraria.Item item,
        Terraria.Player player,
        bool hideVisual
    )
    {
        if (!GlobalItemHooks.UpdateAccessory.GetInvocationList().Any())
        {
            base.UpdateAccessory(
                item,
                player,
                hideVisual
            );
            return;
        }

        GlobalItemHooks.UpdateAccessory.Invoke(
            this,
            item,
            player,
            hideVisual
        );
    }

    public override void UpdateVanity(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.UpdateVanity.GetInvocationList().Any())
        {
            base.UpdateVanity(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.UpdateVanity.Invoke(
            this,
            item,
            player
        );
    }

    public override void UpdateVisibleAccessory(
        Terraria.Item item,
        Terraria.Player player,
        bool hideVisual
    )
    {
        if (!GlobalItemHooks.UpdateVisibleAccessory.GetInvocationList().Any())
        {
            base.UpdateVisibleAccessory(
                item,
                player,
                hideVisual
            );
            return;
        }

        GlobalItemHooks.UpdateVisibleAccessory.Invoke(
            this,
            item,
            player,
            hideVisual
        );
    }

    public override void UpdateItemDye(
        Terraria.Item item,
        Terraria.Player player,
        int dye,
        bool hideVisual
    )
    {
        if (!GlobalItemHooks.UpdateItemDye.GetInvocationList().Any())
        {
            base.UpdateItemDye(
                item,
                player,
                dye,
                hideVisual
            );
            return;
        }

        GlobalItemHooks.UpdateItemDye.Invoke(
            this,
            item,
            player,
            dye,
            hideVisual
        );
    }

    public override void UpdateArmorSet(
        Terraria.Player player,
        string set
    )
    {
        if (!GlobalItemHooks.UpdateArmorSet.GetInvocationList().Any())
        {
            base.UpdateArmorSet(
                player,
                set
            );
            return;
        }

        GlobalItemHooks.UpdateArmorSet.Invoke(
            this,
            player,
            set
        );
    }

    public override void PreUpdateVanitySet(
        Terraria.Player player,
        string set
    )
    {
        if (!GlobalItemHooks.PreUpdateVanitySet.GetInvocationList().Any())
        {
            base.PreUpdateVanitySet(
                player,
                set
            );
            return;
        }

        GlobalItemHooks.PreUpdateVanitySet.Invoke(
            this,
            player,
            set
        );
    }

    public override void UpdateVanitySet(
        Terraria.Player player,
        string set
    )
    {
        if (!GlobalItemHooks.UpdateVanitySet.GetInvocationList().Any())
        {
            base.UpdateVanitySet(
                player,
                set
            );
            return;
        }

        GlobalItemHooks.UpdateVanitySet.Invoke(
            this,
            player,
            set
        );
    }

    public override void ArmorSetShadows(
        Terraria.Player player,
        string set
    )
    {
        if (!GlobalItemHooks.ArmorSetShadows.GetInvocationList().Any())
        {
            base.ArmorSetShadows(
                player,
                set
            );
            return;
        }

        GlobalItemHooks.ArmorSetShadows.Invoke(
            this,
            player,
            set
        );
    }

    public override void SetMatch(
        int armorSlot,
        int type,
        bool male,
        ref int equipSlot,
        ref bool robes
    )
    {
        if (!GlobalItemHooks.SetMatch.GetInvocationList().Any())
        {
            base.SetMatch(
                armorSlot,
                type,
                male,
                ref equipSlot,
                ref robes
            );
            return;
        }

        GlobalItemHooks.SetMatch.Invoke(
            this,
            armorSlot,
            type,
            male,
            ref equipSlot,
            ref robes
        );
    }

    public override bool CanRightClick(
        Terraria.Item item
    )
    {
        if (!GlobalItemHooks.CanRightClick.GetInvocationList().Any())
        {
            return base.CanRightClick(
                item
            );
        }

        return GlobalItemHooks.CanRightClick.Invoke(
            this,
            item
        );
    }

    public override void RightClick(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.RightClick.GetInvocationList().Any())
        {
            base.RightClick(
                item,
                player
            );
            return;
        }

        GlobalItemHooks.RightClick.Invoke(
            this,
            item,
            player
        );
    }

    public override void ModifyItemLoot(
        Terraria.Item item,
        Terraria.ModLoader.ItemLoot itemLoot
    )
    {
        if (!GlobalItemHooks.ModifyItemLoot.GetInvocationList().Any())
        {
            base.ModifyItemLoot(
                item,
                itemLoot
            );
            return;
        }

        GlobalItemHooks.ModifyItemLoot.Invoke(
            this,
            item,
            itemLoot
        );
    }

    public override bool CanStack(
        Terraria.Item destination,
        Terraria.Item source
    )
    {
        if (!GlobalItemHooks.CanStack.GetInvocationList().Any())
        {
            return base.CanStack(
                destination,
                source
            );
        }

        return GlobalItemHooks.CanStack.Invoke(
            this,
            destination,
            source
        );
    }

    public override bool CanStackInWorld(
        Terraria.Item destination,
        Terraria.Item source
    )
    {
        if (!GlobalItemHooks.CanStackInWorld.GetInvocationList().Any())
        {
            return base.CanStackInWorld(
                destination,
                source
            );
        }

        return GlobalItemHooks.CanStackInWorld.Invoke(
            this,
            destination,
            source
        );
    }

    public override void OnStack(
        Terraria.Item destination,
        Terraria.Item source,
        int numToTransfer
    )
    {
        if (!GlobalItemHooks.OnStack.GetInvocationList().Any())
        {
            base.OnStack(
                destination,
                source,
                numToTransfer
            );
            return;
        }

        GlobalItemHooks.OnStack.Invoke(
            this,
            destination,
            source,
            numToTransfer
        );
    }

    public override void SplitStack(
        Terraria.Item destination,
        Terraria.Item source,
        int numToTransfer
    )
    {
        if (!GlobalItemHooks.SplitStack.GetInvocationList().Any())
        {
            base.SplitStack(
                destination,
                source,
                numToTransfer
            );
            return;
        }

        GlobalItemHooks.SplitStack.Invoke(
            this,
            destination,
            source,
            numToTransfer
        );
    }

    public override bool ReforgePrice(
        Terraria.Item item,
        ref int reforgePrice,
        ref bool canApplyDiscount
    )
    {
        if (!GlobalItemHooks.ReforgePrice.GetInvocationList().Any())
        {
            return base.ReforgePrice(
                item,
                ref reforgePrice,
                ref canApplyDiscount
            );
        }

        return GlobalItemHooks.ReforgePrice.Invoke(
            this,
            item,
            ref reforgePrice,
            ref canApplyDiscount
        );
    }

    public override bool CanReforge(
        Terraria.Item item
    )
    {
        if (!GlobalItemHooks.CanReforge.GetInvocationList().Any())
        {
            return base.CanReforge(
                item
            );
        }

        return GlobalItemHooks.CanReforge.Invoke(
            this,
            item
        );
    }

    public override void PreReforge(
        Terraria.Item item
    )
    {
        if (!GlobalItemHooks.PreReforge.GetInvocationList().Any())
        {
            base.PreReforge(
                item
            );
            return;
        }

        GlobalItemHooks.PreReforge.Invoke(
            this,
            item
        );
    }

    public override void PostReforge(
        Terraria.Item item
    )
    {
        if (!GlobalItemHooks.PostReforge.GetInvocationList().Any())
        {
            base.PostReforge(
                item
            );
            return;
        }

        GlobalItemHooks.PostReforge.Invoke(
            this,
            item
        );
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
        if (!GlobalItemHooks.DrawArmorColor.GetInvocationList().Any())
        {
            base.DrawArmorColor(
                type,
                slot,
                drawPlayer,
                shadow,
                ref color,
                ref glowMask,
                ref glowMaskColor
            );
            return;
        }

        GlobalItemHooks.DrawArmorColor.Invoke(
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

    public override void ArmorArmGlowMask(
        int slot,
        Terraria.Player drawPlayer,
        float shadow,
        ref int glowMask,
        ref Microsoft.Xna.Framework.Color color
    )
    {
        if (!GlobalItemHooks.ArmorArmGlowMask.GetInvocationList().Any())
        {
            base.ArmorArmGlowMask(
                slot,
                drawPlayer,
                shadow,
                ref glowMask,
                ref color
            );
            return;
        }

        GlobalItemHooks.ArmorArmGlowMask.Invoke(
            this,
            slot,
            drawPlayer,
            shadow,
            ref glowMask,
            ref color
        );
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
        if (!GlobalItemHooks.VerticalWingSpeeds.GetInvocationList().Any())
        {
            base.VerticalWingSpeeds(
                item,
                player,
                ref ascentWhenFalling,
                ref ascentWhenRising,
                ref maxCanAscendMultiplier,
                ref maxAscentMultiplier,
                ref constantAscend
            );
            return;
        }

        GlobalItemHooks.VerticalWingSpeeds.Invoke(
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

    public override void HorizontalWingSpeeds(
        Terraria.Item item,
        Terraria.Player player,
        ref float speed,
        ref float acceleration
    )
    {
        if (!GlobalItemHooks.HorizontalWingSpeeds.GetInvocationList().Any())
        {
            base.HorizontalWingSpeeds(
                item,
                player,
                ref speed,
                ref acceleration
            );
            return;
        }

        GlobalItemHooks.HorizontalWingSpeeds.Invoke(
            this,
            item,
            player,
            ref speed,
            ref acceleration
        );
    }

    public override bool WingUpdate(
        int wings,
        Terraria.Player player,
        bool inUse
    )
    {
        if (!GlobalItemHooks.WingUpdate.GetInvocationList().Any())
        {
            return base.WingUpdate(
                wings,
                player,
                inUse
            );
        }

        return GlobalItemHooks.WingUpdate.Invoke(
            this,
            wings,
            player,
            inUse
        );
    }

    public override void Update(
        Terraria.Item item,
        ref float gravity,
        ref float maxFallSpeed
    )
    {
        if (!GlobalItemHooks.Update.GetInvocationList().Any())
        {
            base.Update(
                item,
                ref gravity,
                ref maxFallSpeed
            );
            return;
        }

        GlobalItemHooks.Update.Invoke(
            this,
            item,
            ref gravity,
            ref maxFallSpeed
        );
    }

    public override void PostUpdate(
        Terraria.Item item
    )
    {
        if (!GlobalItemHooks.PostUpdate.GetInvocationList().Any())
        {
            base.PostUpdate(
                item
            );
            return;
        }

        GlobalItemHooks.PostUpdate.Invoke(
            this,
            item
        );
    }

    public override void GrabRange(
        Terraria.Item item,
        Terraria.Player player,
        ref int grabRange
    )
    {
        if (!GlobalItemHooks.GrabRange.GetInvocationList().Any())
        {
            base.GrabRange(
                item,
                player,
                ref grabRange
            );
            return;
        }

        GlobalItemHooks.GrabRange.Invoke(
            this,
            item,
            player,
            ref grabRange
        );
    }

    public override bool GrabStyle(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.GrabStyle.GetInvocationList().Any())
        {
            return base.GrabStyle(
                item,
                player
            );
        }

        return GlobalItemHooks.GrabStyle.Invoke(
            this,
            item,
            player
        );
    }

    public override bool CanPickup(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.CanPickup.GetInvocationList().Any())
        {
            return base.CanPickup(
                item,
                player
            );
        }

        return GlobalItemHooks.CanPickup.Invoke(
            this,
            item,
            player
        );
    }

    public override bool OnPickup(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.OnPickup.GetInvocationList().Any())
        {
            return base.OnPickup(
                item,
                player
            );
        }

        return GlobalItemHooks.OnPickup.Invoke(
            this,
            item,
            player
        );
    }

    public override bool ItemSpace(
        Terraria.Item item,
        Terraria.Player player
    )
    {
        if (!GlobalItemHooks.ItemSpace.GetInvocationList().Any())
        {
            return base.ItemSpace(
                item,
                player
            );
        }

        return GlobalItemHooks.ItemSpace.Invoke(
            this,
            item,
            player
        );
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
        if (!GlobalItemHooks.PreDrawInWorld.GetInvocationList().Any())
        {
            return base.PreDrawInWorld(
                item,
                spriteBatch,
                lightColor,
                alphaColor,
                ref rotation,
                ref scale,
                whoAmI
            );
        }

        return GlobalItemHooks.PreDrawInWorld.Invoke(
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
        if (!GlobalItemHooks.PostDrawInWorld.GetInvocationList().Any())
        {
            base.PostDrawInWorld(
                item,
                spriteBatch,
                lightColor,
                alphaColor,
                rotation,
                scale,
                whoAmI
            );
            return;
        }

        GlobalItemHooks.PostDrawInWorld.Invoke(
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
        if (!GlobalItemHooks.PreDrawInInventory.GetInvocationList().Any())
        {
            return base.PreDrawInInventory(
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

        return GlobalItemHooks.PreDrawInInventory.Invoke(
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
        if (!GlobalItemHooks.PostDrawInInventory.GetInvocationList().Any())
        {
            base.PostDrawInInventory(
                item,
                spriteBatch,
                position,
                frame,
                drawColor,
                itemColor,
                origin,
                scale
            );
            return;
        }

        GlobalItemHooks.PostDrawInInventory.Invoke(
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

    public override bool CanEquipAccessory(
        Terraria.Item item,
        Terraria.Player player,
        int slot,
        bool modded
    )
    {
        if (!GlobalItemHooks.CanEquipAccessory.GetInvocationList().Any())
        {
            return base.CanEquipAccessory(
                item,
                player,
                slot,
                modded
            );
        }

        return GlobalItemHooks.CanEquipAccessory.Invoke(
            this,
            item,
            player,
            slot,
            modded
        );
    }

    public override void ExtractinatorUse(
        int extractType,
        int extractinatorBlockType,
        ref int resultType,
        ref int resultStack
    )
    {
        if (!GlobalItemHooks.ExtractinatorUse.GetInvocationList().Any())
        {
            base.ExtractinatorUse(
                extractType,
                extractinatorBlockType,
                ref resultType,
                ref resultStack
            );
            return;
        }

        GlobalItemHooks.ExtractinatorUse.Invoke(
            this,
            extractType,
            extractinatorBlockType,
            ref resultType,
            ref resultStack
        );
    }

    public override void CaughtFishStack(
        int type,
        ref int stack
    )
    {
        if (!GlobalItemHooks.CaughtFishStack.GetInvocationList().Any())
        {
            base.CaughtFishStack(
                type,
                ref stack
            );
            return;
        }

        GlobalItemHooks.CaughtFishStack.Invoke(
            this,
            type,
            ref stack
        );
    }

    public override void AnglerChat(
        int type,
        ref string chat,
        ref string catchLocation
    )
    {
        if (!GlobalItemHooks.AnglerChat.GetInvocationList().Any())
        {
            base.AnglerChat(
                type,
                ref chat,
                ref catchLocation
            );
            return;
        }

        GlobalItemHooks.AnglerChat.Invoke(
            this,
            type,
            ref chat,
            ref catchLocation
        );
    }

    public override void AddRecipes()
    {
        if (!GlobalItemHooks.AddRecipes.GetInvocationList().Any())
        {
            base.AddRecipes();
            return;
        }

        GlobalItemHooks.AddRecipes.Invoke(
            this
        );
    }

    public override bool PreDrawTooltip(
        Terraria.Item item,
        System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.TooltipLine> lines,
        ref int x,
        ref int y
    )
    {
        if (!GlobalItemHooks.PreDrawTooltip.GetInvocationList().Any())
        {
            return base.PreDrawTooltip(
                item,
                lines,
                ref x,
                ref y
            );
        }

        return GlobalItemHooks.PreDrawTooltip.Invoke(
            this,
            item,
            lines,
            ref x,
            ref y
        );
    }

    public override void PostDrawTooltip(
        Terraria.Item item,
        System.Collections.ObjectModel.ReadOnlyCollection<Terraria.ModLoader.DrawableTooltipLine> lines
    )
    {
        if (!GlobalItemHooks.PostDrawTooltip.GetInvocationList().Any())
        {
            base.PostDrawTooltip(
                item,
                lines
            );
            return;
        }

        GlobalItemHooks.PostDrawTooltip.Invoke(
            this,
            item,
            lines
        );
    }

    public override bool PreDrawTooltipLine(
        Terraria.Item item,
        Terraria.ModLoader.DrawableTooltipLine line,
        ref int yOffset
    )
    {
        if (!GlobalItemHooks.PreDrawTooltipLine.GetInvocationList().Any())
        {
            return base.PreDrawTooltipLine(
                item,
                line,
                ref yOffset
            );
        }

        return GlobalItemHooks.PreDrawTooltipLine.Invoke(
            this,
            item,
            line,
            ref yOffset
        );
    }

    public override void PostDrawTooltipLine(
        Terraria.Item item,
        Terraria.ModLoader.DrawableTooltipLine line
    )
    {
        if (!GlobalItemHooks.PostDrawTooltipLine.GetInvocationList().Any())
        {
            base.PostDrawTooltipLine(
                item,
                line
            );
            return;
        }

        GlobalItemHooks.PostDrawTooltipLine.Invoke(
            this,
            item,
            line
        );
    }

    public override void ModifyTooltips(
        Terraria.Item item,
        System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> tooltips
    )
    {
        if (!GlobalItemHooks.ModifyTooltips.GetInvocationList().Any())
        {
            base.ModifyTooltips(
                item,
                tooltips
            );
            return;
        }

        GlobalItemHooks.ModifyTooltips.Invoke(
            this,
            item,
            tooltips
        );
    }

    public override void SaveData(
        Terraria.Item item,
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!GlobalItemHooks.SaveData.GetInvocationList().Any())
        {
            base.SaveData(
                item,
                tag
            );
            return;
        }

        GlobalItemHooks.SaveData.Invoke(
            this,
            item,
            tag
        );
    }

    public override void LoadData(
        Terraria.Item item,
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!GlobalItemHooks.LoadData.GetInvocationList().Any())
        {
            base.LoadData(
                item,
                tag
            );
            return;
        }

        GlobalItemHooks.LoadData.Invoke(
            this,
            item,
            tag
        );
    }

    public override void NetSend(
        Terraria.Item item,
        System.IO.BinaryWriter writer
    )
    {
        if (!GlobalItemHooks.NetSend.GetInvocationList().Any())
        {
            base.NetSend(
                item,
                writer
            );
            return;
        }

        GlobalItemHooks.NetSend.Invoke(
            this,
            item,
            writer
        );
    }

    public override void NetReceive(
        Terraria.Item item,
        System.IO.BinaryReader reader
    )
    {
        if (!GlobalItemHooks.NetReceive.GetInvocationList().Any())
        {
            base.NetReceive(
                item,
                reader
            );
            return;
        }

        GlobalItemHooks.NetReceive.Invoke(
            this,
            item,
            reader
        );
    }
}

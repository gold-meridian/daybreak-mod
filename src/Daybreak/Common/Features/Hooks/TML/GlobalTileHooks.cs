namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalTile':
//     System.Void Terraria.ModLoader.GlobalTile::DropCritterChance(System.Int32,System.Int32,System.Int32,System.Int32&,System.Int32&,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalTile::CanDrop(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::Drop(System.Int32,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalTile::CanKillTile(System.Int32,System.Int32,System.Int32,System.Boolean&)
//     System.Void Terraria.ModLoader.GlobalTile::KillTile(System.Int32,System.Int32,System.Int32,System.Boolean&,System.Boolean&,System.Boolean&)
//     System.Void Terraria.ModLoader.GlobalTile::NearbyEffects(System.Int32,System.Int32,System.Int32,System.Boolean)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalTile::IsTileDangerous(System.Int32,System.Int32,System.Int32,Terraria.Player)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalTile::IsTileBiomeSightable(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Color&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalTile::IsTileSpelunkable(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::SetSpriteEffects(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Void Terraria.ModLoader.GlobalTile::AnimateTile()
//     System.Void Terraria.ModLoader.GlobalTile::DrawEffects(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.DataStructures.TileDrawInfo&)
//     System.Void Terraria.ModLoader.GlobalTile::EmitParticles(System.Int32,System.Int32,Terraria.Tile,System.UInt16,System.Int16,System.Int16,Microsoft.Xna.Framework.Color,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalTile::SpecialDraw(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)
//     System.Boolean Terraria.ModLoader.GlobalTile::PreDrawPlacementPreview(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Rectangle&,Microsoft.Xna.Framework.Vector2&,Microsoft.Xna.Framework.Color&,System.Boolean,Microsoft.Xna.Framework.Graphics.SpriteEffects&)
//     System.Void Terraria.ModLoader.GlobalTile::PostDrawPlacementPreview(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Color,System.Boolean,Microsoft.Xna.Framework.Graphics.SpriteEffects)
//     System.Boolean Terraria.ModLoader.GlobalTile::TileFrame(System.Int32,System.Int32,System.Int32,System.Boolean&,System.Boolean&)
//     System.Int32[] Terraria.ModLoader.GlobalTile::AdjTiles(System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::RightClick(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::MouseOver(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::MouseOverFar(System.Int32,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalTile::AutoSelect(System.Int32,System.Int32,System.Int32,Terraria.Item)
//     System.Boolean Terraria.ModLoader.GlobalTile::PreHitWire(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::HitWire(System.Int32,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalTile::Slope(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::FloorVisuals(System.Int32,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalTile::ChangeWaterfallStyle(System.Int32,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalTile::CanReplace(System.Int32,System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::ReplaceTile(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::PostSetupTileMerge()
//     System.Void Terraria.ModLoader.GlobalTile::PreShakeTree(System.Int32,System.Int32,Terraria.Enums.TreeTypes)
//     System.Boolean Terraria.ModLoader.GlobalTile::ShakeTree(System.Int32,System.Int32,Terraria.Enums.TreeTypes)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::KillSound(System.Int32,System.Int32,System.Int32,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalBlockType::NumDust(System.Int32,System.Int32,System.Int32,System.Boolean,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::CreateDust(System.Int32,System.Int32,System.Int32,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::CanPlace(System.Int32,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::CanExplode(System.Int32,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::PreDraw(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)
//     System.Void Terraria.ModLoader.GlobalBlockType::PostDraw(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)
//     System.Void Terraria.ModLoader.GlobalBlockType::RandomUpdate(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalBlockType::PlaceInWorld(System.Int32,System.Int32,System.Int32,Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalBlockType::ModifyLight(System.Int32,System.Int32,System.Int32,System.Single&,System.Single&,System.Single&)
public static partial class GlobalTileHooks
{
    public sealed partial class DropCritterChance
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            ref int wormChance,
            ref int grassHopperChance,
            ref int jungleGrubChance
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref int wormChance,
            ref int grassHopperChance,
            ref int jungleGrubChance
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_DropCritterChance_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::DropCritterChance")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::DropCritterChance; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanDrop
    {
        public delegate bool Original(
            int i,
            int j,
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_CanDrop_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::CanDrop")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::CanDrop; use a flag to disable behavior.");
        }
    }

    public sealed partial class Drop
    {
        public delegate void Original(
            int i,
            int j,
            int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_Drop_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::Drop")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::Drop; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanKillTile
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            ref bool blockDamaged
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool blockDamaged
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_CanKillTile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::CanKillTile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::CanKillTile; use a flag to disable behavior.");
        }
    }

    public sealed partial class KillTile
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            ref bool fail,
            ref bool effectOnly,
            ref bool noItem
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool fail,
            ref bool effectOnly,
            ref bool noItem
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_KillTile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::KillTile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::KillTile; use a flag to disable behavior.");
        }
    }

    public sealed partial class NearbyEffects
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            bool closer
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            bool closer
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_NearbyEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::NearbyEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::NearbyEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class IsTileDangerous
    {
        public delegate bool? Original(
            int i,
            int j,
            int type,
            Terraria.Player player
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_IsTileDangerous_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::IsTileDangerous")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::IsTileDangerous; use a flag to disable behavior.");
        }
    }

    public sealed partial class IsTileBiomeSightable
    {
        public delegate bool? Original(
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Color sightColor
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Color sightColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_IsTileBiomeSightable_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::IsTileBiomeSightable")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::IsTileBiomeSightable; use a flag to disable behavior.");
        }
    }

    public sealed partial class IsTileSpelunkable
    {
        public delegate bool? Original(
            int i,
            int j,
            int type
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_IsTileSpelunkable_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::IsTileSpelunkable")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::IsTileSpelunkable; use a flag to disable behavior.");
        }
    }

    public sealed partial class SetSpriteEffects
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_SetSpriteEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::SetSpriteEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::SetSpriteEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class AnimateTile
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_AnimateTile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::AnimateTile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::AnimateTile; use a flag to disable behavior.");
        }
    }

    public sealed partial class DrawEffects
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            ref Terraria.DataStructures.TileDrawInfo drawData
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            ref Terraria.DataStructures.TileDrawInfo drawData
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_DrawEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::DrawEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::DrawEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class EmitParticles
    {
        public delegate void Original(
            int i,
            int j,
            Terraria.Tile tileCache,
            ushort typeCache,
            short tileFrameX,
            short tileFrameY,
            Microsoft.Xna.Framework.Color tileLight,
            bool visible
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            Terraria.Tile tileCache,
            ushort typeCache,
            short tileFrameX,
            short tileFrameY,
            Microsoft.Xna.Framework.Color tileLight,
            bool visible
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_EmitParticles_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::EmitParticles")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::EmitParticles; use a flag to disable behavior.");
        }
    }

    public sealed partial class SpecialDraw
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_SpecialDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::SpecialDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::SpecialDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDrawPlacementPreview
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            ref Microsoft.Xna.Framework.Rectangle frame,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Color color,
            bool validPlacement,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            ref Microsoft.Xna.Framework.Rectangle frame,
            ref Microsoft.Xna.Framework.Vector2 position,
            ref Microsoft.Xna.Framework.Color color,
            bool validPlacement,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PreDrawPlacementPreview_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PreDrawPlacementPreview")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PreDrawPlacementPreview; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawPlacementPreview
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Color color,
            bool validPlacement,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Color color,
            bool validPlacement,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PostDrawPlacementPreview_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PostDrawPlacementPreview")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PostDrawPlacementPreview; use a flag to disable behavior.");
        }
    }

    public sealed partial class TileFrame
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            ref bool resetFrame,
            ref bool noBreak
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool resetFrame,
            ref bool noBreak
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_TileFrame_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::TileFrame")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::TileFrame; use a flag to disable behavior.");
        }
    }

    public sealed partial class AdjTiles
    {
        public delegate int[] Original(
            int type
        );

        public delegate int[] Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_AdjTiles_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::AdjTiles")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::AdjTiles; use a flag to disable behavior.");
        }
    }

    public sealed partial class RightClick
    {
        public delegate void Original(
            int i,
            int j,
            int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_RightClick_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::RightClick")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::RightClick; use a flag to disable behavior.");
        }
    }

    public sealed partial class MouseOver
    {
        public delegate void Original(
            int i,
            int j,
            int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_MouseOver_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::MouseOver")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::MouseOver; use a flag to disable behavior.");
        }
    }

    public sealed partial class MouseOverFar
    {
        public delegate void Original(
            int i,
            int j,
            int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_MouseOverFar_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::MouseOverFar")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::MouseOverFar; use a flag to disable behavior.");
        }
    }

    public sealed partial class AutoSelect
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            Terraria.Item item
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_AutoSelect_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::AutoSelect")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::AutoSelect; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreHitWire
    {
        public delegate bool Original(
            int i,
            int j,
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PreHitWire_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PreHitWire")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PreHitWire; use a flag to disable behavior.");
        }
    }

    public sealed partial class HitWire
    {
        public delegate void Original(
            int i,
            int j,
            int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_HitWire_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::HitWire")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::HitWire; use a flag to disable behavior.");
        }
    }

    public sealed partial class Slope
    {
        public delegate bool Original(
            int i,
            int j,
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_Slope_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::Slope")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::Slope; use a flag to disable behavior.");
        }
    }

    public sealed partial class FloorVisuals
    {
        public delegate void Original(
            int type,
            Terraria.Player player
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int type,
            Terraria.Player player
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_FloorVisuals_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::FloorVisuals")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::FloorVisuals; use a flag to disable behavior.");
        }
    }

    public sealed partial class ChangeWaterfallStyle
    {
        public delegate void Original(
            int type,
            ref int style
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int type,
            ref int style
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_ChangeWaterfallStyle_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::ChangeWaterfallStyle")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::ChangeWaterfallStyle; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanReplace
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            int tileTypeBeingPlaced
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            int tileTypeBeingPlaced
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_CanReplace_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::CanReplace")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::CanReplace; use a flag to disable behavior.");
        }
    }

    public sealed partial class ReplaceTile
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            int targetType,
            int targetStyle
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            int targetType,
            int targetStyle
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_ReplaceTile_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::ReplaceTile")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::ReplaceTile; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostSetupTileMerge
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PostSetupTileMerge_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PostSetupTileMerge")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PostSetupTileMerge; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreShakeTree
    {
        public delegate void Original(
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PreShakeTree_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PreShakeTree")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PreShakeTree; use a flag to disable behavior.");
        }
    }

    public sealed partial class ShakeTree
    {
        public delegate bool Original(
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalTile self,
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_ShakeTree_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::ShakeTree")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::ShakeTree; use a flag to disable behavior.");
        }
    }

    public sealed partial class KillSound
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            bool fail
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            bool fail
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_KillSound_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::KillSound")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::KillSound; use a flag to disable behavior.");
        }
    }

    public sealed partial class NumDust
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            bool fail,
            ref int num
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            bool fail,
            ref int num
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_NumDust_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::NumDust")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::NumDust; use a flag to disable behavior.");
        }
    }

    public sealed partial class CreateDust
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            ref int dustType
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            ref int dustType
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_CreateDust_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::CreateDust")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::CreateDust; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanPlace
    {
        public delegate bool Original(
            int i,
            int j,
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_CanPlace_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::CanPlace")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::CanPlace; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanExplode
    {
        public delegate bool Original(
            int i,
            int j,
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_CanExplode_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::CanExplode")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::CanExplode; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PreDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PreDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PreDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PostDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PostDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PostDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class RandomUpdate
    {
        public delegate void Original(
            int i,
            int j,
            int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_RandomUpdate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::RandomUpdate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::RandomUpdate; use a flag to disable behavior.");
        }
    }

    public sealed partial class PlaceInWorld
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            Terraria.Item item
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_PlaceInWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::PlaceInWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::PlaceInWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyLight
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            ref float r,
            ref float g,
            ref float b
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            ref float r,
            ref float g,
            ref float b
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalTile_ModifyLight_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalTile::ModifyLight")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalTile::ModifyLight; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_DropCritterChance_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.DropCritterChance.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_DropCritterChance_Impl(GlobalTileHooks.DropCritterChance.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void DropCritterChance(
        int i,
        int j,
        int type,
        ref int wormChance,
        ref int grassHopperChance,
        ref int jungleGrubChance
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref int wormChance_captured,
                ref int grassHopperChance_captured,
                ref int jungleGrubChance_captured
            ) => base.DropCritterChance(
                i_captured,
                j_captured,
                type_captured,
                ref wormChance_captured,
                ref grassHopperChance_captured,
                ref jungleGrubChance_captured
            ),
            this,
            i,
            j,
            type,
            ref wormChance,
            ref grassHopperChance,
            ref jungleGrubChance
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_CanDrop_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.CanDrop.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_CanDrop_Impl(GlobalTileHooks.CanDrop.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanDrop(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.CanDrop(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_Drop_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.Drop.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_Drop_Impl(GlobalTileHooks.Drop.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void Drop(
        int i,
        int j,
        int type
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.Drop(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_CanKillTile_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.CanKillTile.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_CanKillTile_Impl(GlobalTileHooks.CanKillTile.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanKillTile(
        int i,
        int j,
        int type,
        ref bool blockDamaged
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref bool blockDamaged_captured
            ) => base.CanKillTile(
                i_captured,
                j_captured,
                type_captured,
                ref blockDamaged_captured
            ),
            this,
            i,
            j,
            type,
            ref blockDamaged
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_KillTile_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.KillTile.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_KillTile_Impl(GlobalTileHooks.KillTile.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void KillTile(
        int i,
        int j,
        int type,
        ref bool fail,
        ref bool effectOnly,
        ref bool noItem
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref bool fail_captured,
                ref bool effectOnly_captured,
                ref bool noItem_captured
            ) => base.KillTile(
                i_captured,
                j_captured,
                type_captured,
                ref fail_captured,
                ref effectOnly_captured,
                ref noItem_captured
            ),
            this,
            i,
            j,
            type,
            ref fail,
            ref effectOnly,
            ref noItem
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_NearbyEffects_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.NearbyEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_NearbyEffects_Impl(GlobalTileHooks.NearbyEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void NearbyEffects(
        int i,
        int j,
        int type,
        bool closer
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                bool closer_captured
            ) => base.NearbyEffects(
                i_captured,
                j_captured,
                type_captured,
                closer_captured
            ),
            this,
            i,
            j,
            type,
            closer
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_IsTileDangerous_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.IsTileDangerous.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_IsTileDangerous_Impl(GlobalTileHooks.IsTileDangerous.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? IsTileDangerous(
        int i,
        int j,
        int type,
        Terraria.Player player
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Terraria.Player player_captured
            ) => base.IsTileDangerous(
                i_captured,
                j_captured,
                type_captured,
                player_captured
            ),
            this,
            i,
            j,
            type,
            player
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_IsTileBiomeSightable_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.IsTileBiomeSightable.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_IsTileBiomeSightable_Impl(GlobalTileHooks.IsTileBiomeSightable.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? IsTileBiomeSightable(
        int i,
        int j,
        int type,
        ref Microsoft.Xna.Framework.Color sightColor
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref Microsoft.Xna.Framework.Color sightColor_captured
            ) => base.IsTileBiomeSightable(
                i_captured,
                j_captured,
                type_captured,
                ref sightColor_captured
            ),
            this,
            i,
            j,
            type,
            ref sightColor
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_IsTileSpelunkable_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.IsTileSpelunkable.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_IsTileSpelunkable_Impl(GlobalTileHooks.IsTileSpelunkable.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? IsTileSpelunkable(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.IsTileSpelunkable(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_SetSpriteEffects_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.SetSpriteEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_SetSpriteEffects_Impl(GlobalTileHooks.SetSpriteEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void SetSpriteEffects(
        int i,
        int j,
        int type,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.SetSpriteEffects(
                i_captured,
                j_captured,
                type_captured,
                ref spriteEffects_captured
            ),
            this,
            i,
            j,
            type,
            ref spriteEffects
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_AnimateTile_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.AnimateTile.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_AnimateTile_Impl(GlobalTileHooks.AnimateTile.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void AnimateTile()
    {
        hook(
            () => base.AnimateTile(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_DrawEffects_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.DrawEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_DrawEffects_Impl(GlobalTileHooks.DrawEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void DrawEffects(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        ref Terraria.DataStructures.TileDrawInfo drawData
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                ref Terraria.DataStructures.TileDrawInfo drawData_captured
            ) => base.DrawEffects(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured,
                ref drawData_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch,
            ref drawData
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_EmitParticles_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.EmitParticles.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_EmitParticles_Impl(GlobalTileHooks.EmitParticles.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void EmitParticles(
        int i,
        int j,
        Terraria.Tile tileCache,
        ushort typeCache,
        short tileFrameX,
        short tileFrameY,
        Microsoft.Xna.Framework.Color tileLight,
        bool visible
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                Terraria.Tile tileCache_captured,
                ushort typeCache_captured,
                short tileFrameX_captured,
                short tileFrameY_captured,
                Microsoft.Xna.Framework.Color tileLight_captured,
                bool visible_captured
            ) => base.EmitParticles(
                i_captured,
                j_captured,
                tileCache_captured,
                typeCache_captured,
                tileFrameX_captured,
                tileFrameY_captured,
                tileLight_captured,
                visible_captured
            ),
            this,
            i,
            j,
            tileCache,
            typeCache,
            tileFrameX,
            tileFrameY,
            tileLight,
            visible
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_SpecialDraw_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.SpecialDraw.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_SpecialDraw_Impl(GlobalTileHooks.SpecialDraw.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void SpecialDraw(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured
            ) => base.SpecialDraw(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PreDrawPlacementPreview_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PreDrawPlacementPreview.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PreDrawPlacementPreview_Impl(GlobalTileHooks.PreDrawPlacementPreview.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDrawPlacementPreview(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        ref Microsoft.Xna.Framework.Rectangle frame,
        ref Microsoft.Xna.Framework.Vector2 position,
        ref Microsoft.Xna.Framework.Color color,
        bool validPlacement,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                ref Microsoft.Xna.Framework.Rectangle frame_captured,
                ref Microsoft.Xna.Framework.Vector2 position_captured,
                ref Microsoft.Xna.Framework.Color color_captured,
                bool validPlacement_captured,
                ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.PreDrawPlacementPreview(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured,
                ref frame_captured,
                ref position_captured,
                ref color_captured,
                validPlacement_captured,
                ref spriteEffects_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch,
            ref frame,
            ref position,
            ref color,
            validPlacement,
            ref spriteEffects
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PostDrawPlacementPreview_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PostDrawPlacementPreview.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PostDrawPlacementPreview_Impl(GlobalTileHooks.PostDrawPlacementPreview.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDrawPlacementPreview(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Color color,
        bool validPlacement,
        Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Rectangle frame_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Color color_captured,
                bool validPlacement_captured,
                Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.PostDrawPlacementPreview(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured,
                frame_captured,
                position_captured,
                color_captured,
                validPlacement_captured,
                spriteEffects_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch,
            frame,
            position,
            color,
            validPlacement,
            spriteEffects
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_TileFrame_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.TileFrame.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_TileFrame_Impl(GlobalTileHooks.TileFrame.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool TileFrame(
        int i,
        int j,
        int type,
        ref bool resetFrame,
        ref bool noBreak
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref bool resetFrame_captured,
                ref bool noBreak_captured
            ) => base.TileFrame(
                i_captured,
                j_captured,
                type_captured,
                ref resetFrame_captured,
                ref noBreak_captured
            ),
            this,
            i,
            j,
            type,
            ref resetFrame,
            ref noBreak
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_AdjTiles_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.AdjTiles.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_AdjTiles_Impl(GlobalTileHooks.AdjTiles.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override int[] AdjTiles(
        int type
    )
    {
        return hook(
            (
                int type_captured
            ) => base.AdjTiles(
                type_captured
            ),
            this,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_RightClick_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.RightClick.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_RightClick_Impl(GlobalTileHooks.RightClick.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void RightClick(
        int i,
        int j,
        int type
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.RightClick(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_MouseOver_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.MouseOver.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_MouseOver_Impl(GlobalTileHooks.MouseOver.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void MouseOver(
        int i,
        int j,
        int type
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.MouseOver(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_MouseOverFar_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.MouseOverFar.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_MouseOverFar_Impl(GlobalTileHooks.MouseOverFar.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void MouseOverFar(
        int i,
        int j,
        int type
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.MouseOverFar(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_AutoSelect_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.AutoSelect.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_AutoSelect_Impl(GlobalTileHooks.AutoSelect.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool AutoSelect(
        int i,
        int j,
        int type,
        Terraria.Item item
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Terraria.Item item_captured
            ) => base.AutoSelect(
                i_captured,
                j_captured,
                type_captured,
                item_captured
            ),
            this,
            i,
            j,
            type,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PreHitWire_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PreHitWire.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PreHitWire_Impl(GlobalTileHooks.PreHitWire.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreHitWire(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.PreHitWire(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_HitWire_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.HitWire.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_HitWire_Impl(GlobalTileHooks.HitWire.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void HitWire(
        int i,
        int j,
        int type
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.HitWire(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_Slope_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.Slope.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_Slope_Impl(GlobalTileHooks.Slope.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool Slope(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.Slope(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_FloorVisuals_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.FloorVisuals.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_FloorVisuals_Impl(GlobalTileHooks.FloorVisuals.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void FloorVisuals(
        int type,
        Terraria.Player player
    )
    {
        hook(
            (
                int type_captured,
                Terraria.Player player_captured
            ) => base.FloorVisuals(
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
public sealed partial class GlobalTile_ChangeWaterfallStyle_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.ChangeWaterfallStyle.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_ChangeWaterfallStyle_Impl(GlobalTileHooks.ChangeWaterfallStyle.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ChangeWaterfallStyle(
        int type,
        ref int style
    )
    {
        hook(
            (
                int type_captured,
                ref int style_captured
            ) => base.ChangeWaterfallStyle(
                type_captured,
                ref style_captured
            ),
            this,
            type,
            ref style
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_CanReplace_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.CanReplace.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_CanReplace_Impl(GlobalTileHooks.CanReplace.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanReplace(
        int i,
        int j,
        int type,
        int tileTypeBeingPlaced
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                int tileTypeBeingPlaced_captured
            ) => base.CanReplace(
                i_captured,
                j_captured,
                type_captured,
                tileTypeBeingPlaced_captured
            ),
            this,
            i,
            j,
            type,
            tileTypeBeingPlaced
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_ReplaceTile_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.ReplaceTile.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_ReplaceTile_Impl(GlobalTileHooks.ReplaceTile.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ReplaceTile(
        int i,
        int j,
        int type,
        int targetType,
        int targetStyle
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                int targetType_captured,
                int targetStyle_captured
            ) => base.ReplaceTile(
                i_captured,
                j_captured,
                type_captured,
                targetType_captured,
                targetStyle_captured
            ),
            this,
            i,
            j,
            type,
            targetType,
            targetStyle
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PostSetupTileMerge_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PostSetupTileMerge.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PostSetupTileMerge_Impl(GlobalTileHooks.PostSetupTileMerge.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostSetupTileMerge()
    {
        hook(
            () => base.PostSetupTileMerge(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PreShakeTree_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PreShakeTree.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PreShakeTree_Impl(GlobalTileHooks.PreShakeTree.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreShakeTree(
        int x,
        int y,
        Terraria.Enums.TreeTypes treeType
    )
    {
        hook(
            (
                int x_captured,
                int y_captured,
                Terraria.Enums.TreeTypes treeType_captured
            ) => base.PreShakeTree(
                x_captured,
                y_captured,
                treeType_captured
            ),
            this,
            x,
            y,
            treeType
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_ShakeTree_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.ShakeTree.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_ShakeTree_Impl(GlobalTileHooks.ShakeTree.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ShakeTree(
        int x,
        int y,
        Terraria.Enums.TreeTypes treeType
    )
    {
        return hook(
            (
                int x_captured,
                int y_captured,
                Terraria.Enums.TreeTypes treeType_captured
            ) => base.ShakeTree(
                x_captured,
                y_captured,
                treeType_captured
            ),
            this,
            x,
            y,
            treeType
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_KillSound_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.KillSound.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_KillSound_Impl(GlobalTileHooks.KillSound.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool KillSound(
        int i,
        int j,
        int type,
        bool fail
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                bool fail_captured
            ) => base.KillSound(
                i_captured,
                j_captured,
                type_captured,
                fail_captured
            ),
            this,
            i,
            j,
            type,
            fail
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_NumDust_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.NumDust.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_NumDust_Impl(GlobalTileHooks.NumDust.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void NumDust(
        int i,
        int j,
        int type,
        bool fail,
        ref int num
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                bool fail_captured,
                ref int num_captured
            ) => base.NumDust(
                i_captured,
                j_captured,
                type_captured,
                fail_captured,
                ref num_captured
            ),
            this,
            i,
            j,
            type,
            fail,
            ref num
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_CreateDust_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.CreateDust.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_CreateDust_Impl(GlobalTileHooks.CreateDust.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CreateDust(
        int i,
        int j,
        int type,
        ref int dustType
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref int dustType_captured
            ) => base.CreateDust(
                i_captured,
                j_captured,
                type_captured,
                ref dustType_captured
            ),
            this,
            i,
            j,
            type,
            ref dustType
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_CanPlace_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.CanPlace.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_CanPlace_Impl(GlobalTileHooks.CanPlace.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanPlace(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.CanPlace(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_CanExplode_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.CanExplode.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_CanExplode_Impl(GlobalTileHooks.CanExplode.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanExplode(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.CanExplode(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PreDraw_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PreDraw.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PreDraw_Impl(GlobalTileHooks.PreDraw.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDraw(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured
            ) => base.PreDraw(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PostDraw_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PostDraw.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PostDraw_Impl(GlobalTileHooks.PostDraw.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDraw(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured
            ) => base.PostDraw(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_RandomUpdate_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.RandomUpdate.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_RandomUpdate_Impl(GlobalTileHooks.RandomUpdate.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void RandomUpdate(
        int i,
        int j,
        int type
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.RandomUpdate(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_PlaceInWorld_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.PlaceInWorld.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_PlaceInWorld_Impl(GlobalTileHooks.PlaceInWorld.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PlaceInWorld(
        int i,
        int j,
        int type,
        Terraria.Item item
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Terraria.Item item_captured
            ) => base.PlaceInWorld(
                i_captured,
                j_captured,
                type_captured,
                item_captured
            ),
            this,
            i,
            j,
            type,
            item
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalTile_ModifyLight_Impl() : Terraria.ModLoader.GlobalTile
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private GlobalTileHooks.ModifyLight.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public GlobalTile_ModifyLight_Impl(GlobalTileHooks.ModifyLight.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyLight(
        int i,
        int j,
        int type,
        ref float r,
        ref float g,
        ref float b
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref float r_captured,
                ref float g_captured,
                ref float b_captured
            ) => base.ModifyLight(
                i_captured,
                j_captured,
                type_captured,
                ref r_captured,
                ref g_captured,
                ref b_captured
            ),
            this,
            i,
            j,
            type,
            ref r,
            ref g,
            ref b
        );
    }
}
namespace Daybreak.Common.Features.Hooks;

using System.Linq;

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
//     System.Boolean Terraria.ModLoader.GlobalTile::PreHitWire(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::HitWire(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::FloorVisuals(System.Int32,Terraria.Player)
//     System.Void Terraria.ModLoader.GlobalTile::ChangeWaterfallStyle(System.Int32,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalTile::CanReplace(System.Int32,System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::ReplaceTile(System.Int32,System.Int32,System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalTile::PostSetupTileMerge()
//     System.Void Terraria.ModLoader.GlobalTile::PreShakeTree(System.Int32,System.Int32,Terraria.Enums.TreeTypes)
//     System.Boolean Terraria.ModLoader.GlobalTile::ShakeTree(System.Int32,System.Int32,Terraria.Enums.TreeTypes)
public static partial class GlobalTileHooks
{
    public sealed partial class DropCritterChance
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref int wormChance,
            ref int grassHopperChance,
            ref int jungleGrubChance
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref int wormChance,
            ref int grassHopperChance,
            ref int jungleGrubChance
        )
        {
            Event?.Invoke(self, i, j, type, ref wormChance, ref grassHopperChance, ref jungleGrubChance);
        }
    }

    public sealed partial class CanDrop
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, i, j, type);
            }

            return result;
        }
    }

    public sealed partial class Drop
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            Event?.Invoke(self, i, j, type);
        }
    }

    public sealed partial class CanKillTile
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool blockDamaged
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool blockDamaged
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, i, j, type, ref blockDamaged))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class KillTile
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool fail,
            ref bool effectOnly,
            ref bool noItem
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool fail,
            ref bool effectOnly,
            ref bool noItem
        )
        {
            Event?.Invoke(self, i, j, type, ref fail, ref effectOnly, ref noItem);
        }
    }

    public sealed partial class NearbyEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            bool closer
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            bool closer
        )
        {
            Event?.Invoke(self, i, j, type, closer);
        }
    }

    public sealed partial class IsTileDangerous
    {
        public delegate bool? Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool? Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Terraria.Player player
        )
        {
            var result = default(bool?);
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                var newValue = handler.Invoke(self, i, j, type, player);
                if (newValue.HasValue)
                {
                    result &= newValue;
                }
            }

            return result;
        }
    }

    public sealed partial class IsTileBiomeSightable
    {
        public delegate bool? Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Color sightColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool? Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Color sightColor
        )
        {
            var result = default(bool?);
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                var newValue = handler.Invoke(self, i, j, type, ref sightColor);
                if (newValue.HasValue)
                {
                    result &= newValue;
                }
            }

            return result;
        }
    }

    public sealed partial class IsTileSpelunkable
    {
        public delegate bool? Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool? Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            var result = default(bool?);
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                var newValue = handler.Invoke(self, i, j, type);
                if (newValue.HasValue)
                {
                    result &= newValue;
                }
            }

            return result;
        }
    }

    public sealed partial class SetSpriteEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        )
        {
            Event?.Invoke(self, i, j, type, ref spriteEffects);
        }
    }

    public sealed partial class AnimateTile
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class DrawEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            ref Terraria.DataStructures.TileDrawInfo drawData
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            ref Terraria.DataStructures.TileDrawInfo drawData
        )
        {
            Event?.Invoke(self, i, j, type, spriteBatch, ref drawData);
        }
    }

    public sealed partial class EmitParticles
    {
        public delegate void Definition(
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

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
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
            Event?.Invoke(self, i, j, tileCache, typeCache, tileFrameX, tileFrameY, tileLight, visible);
        }
    }

    public sealed partial class SpecialDraw
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        )
        {
            Event?.Invoke(self, i, j, type, spriteBatch);
        }
    }

    public sealed partial class PreDrawPlacementPreview
    {
        public delegate bool Definition(
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

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
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
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, i, j, type, spriteBatch, ref frame, ref position, ref color, validPlacement, ref spriteEffects);
            }

            return result;
        }
    }

    public sealed partial class PostDrawPlacementPreview
    {
        public delegate void Definition(
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

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
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
        )
        {
            Event?.Invoke(self, i, j, type, spriteBatch, frame, position, color, validPlacement, spriteEffects);
        }
    }

    public sealed partial class TileFrame
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool resetFrame,
            ref bool noBreak
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            ref bool resetFrame,
            ref bool noBreak
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, i, j, type, ref resetFrame, ref noBreak);
            }

            return result;
        }
    }

    public sealed partial class AdjTiles
    {
        public delegate int[] Definition(
            Terraria.ModLoader.GlobalTile self,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static int[] Invoke(
            Terraria.ModLoader.GlobalTile self,
            int type
        )
        {
            var result = new System.Collections.Generic.List<int>();
            if (Event == null)
            {
                return result.ToArray();
            }

            foreach (var handler in GetInvocationList())
            {
                var newValue = handler.Invoke(self, type);
                if (newValue != null)
                {
                    result.AddRange(newValue);
                }
            }

            return result.ToArray();
        }
    }

    public sealed partial class RightClick
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            Event?.Invoke(self, i, j, type);
        }
    }

    public sealed partial class MouseOver
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            Event?.Invoke(self, i, j, type);
        }
    }

    public sealed partial class MouseOverFar
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            Event?.Invoke(self, i, j, type);
        }
    }

    public sealed partial class PreHitWire
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, i, j, type))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class HitWire
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type
        )
        {
            Event?.Invoke(self, i, j, type);
        }
    }

    public sealed partial class FloorVisuals
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int type,
            Terraria.Player player
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int type,
            Terraria.Player player
        )
        {
            Event?.Invoke(self, type, player);
        }
    }

    public sealed partial class ChangeWaterfallStyle
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int type,
            ref int style
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int type,
            ref int style
        )
        {
            Event?.Invoke(self, type, ref style);
        }
    }

    public sealed partial class CanReplace
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            int tileTypeBeingPlaced
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            int tileTypeBeingPlaced
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, i, j, type, tileTypeBeingPlaced))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class ReplaceTile
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            int targetType,
            int targetStyle
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int i,
            int j,
            int type,
            int targetType,
            int targetStyle
        )
        {
            Event?.Invoke(self, i, j, type, targetType, targetStyle);
        }
    }

    public sealed partial class PostSetupTileMerge
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreShakeTree
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalTile self,
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalTile self,
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        )
        {
            Event?.Invoke(self, x, y, treeType);
        }
    }

    public sealed partial class ShakeTree
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalTile self,
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalTile self,
            int x,
            int y,
            Terraria.Enums.TreeTypes treeType
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, x, y, treeType))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

public sealed partial class GlobalTileImpl : Terraria.ModLoader.GlobalTile
{
    public override void DropCritterChance(
        int i,
        int j,
        int type,
        ref int wormChance,
        ref int grassHopperChance,
        ref int jungleGrubChance
    )
    {
        if (!GlobalTileHooks.DropCritterChance.GetInvocationList().Any())
        {
            base.DropCritterChance(
                i,
                j,
                type,
                ref wormChance,
                ref grassHopperChance,
                ref jungleGrubChance
            );
            return;
        }

        GlobalTileHooks.DropCritterChance.Invoke(
            this,
            i,
            j,
            type,
            ref wormChance,
            ref grassHopperChance,
            ref jungleGrubChance
        );
    }

    public override bool CanDrop(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.CanDrop.GetInvocationList().Any())
        {
            return base.CanDrop(
                i,
                j,
                type
            );
        }

        return GlobalTileHooks.CanDrop.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override void Drop(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.Drop.GetInvocationList().Any())
        {
            base.Drop(
                i,
                j,
                type
            );
            return;
        }

        GlobalTileHooks.Drop.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override bool CanKillTile(
        int i,
        int j,
        int type,
        ref bool blockDamaged
    )
    {
        if (!GlobalTileHooks.CanKillTile.GetInvocationList().Any())
        {
            return base.CanKillTile(
                i,
                j,
                type,
                ref blockDamaged
            );
        }

        return GlobalTileHooks.CanKillTile.Invoke(
            this,
            i,
            j,
            type,
            ref blockDamaged
        );
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
        if (!GlobalTileHooks.KillTile.GetInvocationList().Any())
        {
            base.KillTile(
                i,
                j,
                type,
                ref fail,
                ref effectOnly,
                ref noItem
            );
            return;
        }

        GlobalTileHooks.KillTile.Invoke(
            this,
            i,
            j,
            type,
            ref fail,
            ref effectOnly,
            ref noItem
        );
    }

    public override void NearbyEffects(
        int i,
        int j,
        int type,
        bool closer
    )
    {
        if (!GlobalTileHooks.NearbyEffects.GetInvocationList().Any())
        {
            base.NearbyEffects(
                i,
                j,
                type,
                closer
            );
            return;
        }

        GlobalTileHooks.NearbyEffects.Invoke(
            this,
            i,
            j,
            type,
            closer
        );
    }

    public override bool? IsTileDangerous(
        int i,
        int j,
        int type,
        Terraria.Player player
    )
    {
        if (!GlobalTileHooks.IsTileDangerous.GetInvocationList().Any())
        {
            return base.IsTileDangerous(
                i,
                j,
                type,
                player
            );
        }

        return GlobalTileHooks.IsTileDangerous.Invoke(
            this,
            i,
            j,
            type,
            player
        );
    }

    public override bool? IsTileBiomeSightable(
        int i,
        int j,
        int type,
        ref Microsoft.Xna.Framework.Color sightColor
    )
    {
        if (!GlobalTileHooks.IsTileBiomeSightable.GetInvocationList().Any())
        {
            return base.IsTileBiomeSightable(
                i,
                j,
                type,
                ref sightColor
            );
        }

        return GlobalTileHooks.IsTileBiomeSightable.Invoke(
            this,
            i,
            j,
            type,
            ref sightColor
        );
    }

    public override bool? IsTileSpelunkable(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.IsTileSpelunkable.GetInvocationList().Any())
        {
            return base.IsTileSpelunkable(
                i,
                j,
                type
            );
        }

        return GlobalTileHooks.IsTileSpelunkable.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override void SetSpriteEffects(
        int i,
        int j,
        int type,
        ref Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        if (!GlobalTileHooks.SetSpriteEffects.GetInvocationList().Any())
        {
            base.SetSpriteEffects(
                i,
                j,
                type,
                ref spriteEffects
            );
            return;
        }

        GlobalTileHooks.SetSpriteEffects.Invoke(
            this,
            i,
            j,
            type,
            ref spriteEffects
        );
    }

    public override void AnimateTile()
    {
        if (!GlobalTileHooks.AnimateTile.GetInvocationList().Any())
        {
            base.AnimateTile();
            return;
        }

        GlobalTileHooks.AnimateTile.Invoke(
            this
        );
    }

    public override void DrawEffects(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        ref Terraria.DataStructures.TileDrawInfo drawData
    )
    {
        if (!GlobalTileHooks.DrawEffects.GetInvocationList().Any())
        {
            base.DrawEffects(
                i,
                j,
                type,
                spriteBatch,
                ref drawData
            );
            return;
        }

        GlobalTileHooks.DrawEffects.Invoke(
            this,
            i,
            j,
            type,
            spriteBatch,
            ref drawData
        );
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
        if (!GlobalTileHooks.EmitParticles.GetInvocationList().Any())
        {
            base.EmitParticles(
                i,
                j,
                tileCache,
                typeCache,
                tileFrameX,
                tileFrameY,
                tileLight,
                visible
            );
            return;
        }

        GlobalTileHooks.EmitParticles.Invoke(
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

    public override void SpecialDraw(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        if (!GlobalTileHooks.SpecialDraw.GetInvocationList().Any())
        {
            base.SpecialDraw(
                i,
                j,
                type,
                spriteBatch
            );
            return;
        }

        GlobalTileHooks.SpecialDraw.Invoke(
            this,
            i,
            j,
            type,
            spriteBatch
        );
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
        if (!GlobalTileHooks.PreDrawPlacementPreview.GetInvocationList().Any())
        {
            return base.PreDrawPlacementPreview(
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

        return GlobalTileHooks.PreDrawPlacementPreview.Invoke(
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
        if (!GlobalTileHooks.PostDrawPlacementPreview.GetInvocationList().Any())
        {
            base.PostDrawPlacementPreview(
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
            return;
        }

        GlobalTileHooks.PostDrawPlacementPreview.Invoke(
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

    public override bool TileFrame(
        int i,
        int j,
        int type,
        ref bool resetFrame,
        ref bool noBreak
    )
    {
        if (!GlobalTileHooks.TileFrame.GetInvocationList().Any())
        {
            return base.TileFrame(
                i,
                j,
                type,
                ref resetFrame,
                ref noBreak
            );
        }

        return GlobalTileHooks.TileFrame.Invoke(
            this,
            i,
            j,
            type,
            ref resetFrame,
            ref noBreak
        );
    }

    public override int[] AdjTiles(
        int type
    )
    {
        if (!GlobalTileHooks.AdjTiles.GetInvocationList().Any())
        {
            return base.AdjTiles(
                type
            );
        }

        return GlobalTileHooks.AdjTiles.Invoke(
            this,
            type
        );
    }

    public override void RightClick(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.RightClick.GetInvocationList().Any())
        {
            base.RightClick(
                i,
                j,
                type
            );
            return;
        }

        GlobalTileHooks.RightClick.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override void MouseOver(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.MouseOver.GetInvocationList().Any())
        {
            base.MouseOver(
                i,
                j,
                type
            );
            return;
        }

        GlobalTileHooks.MouseOver.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override void MouseOverFar(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.MouseOverFar.GetInvocationList().Any())
        {
            base.MouseOverFar(
                i,
                j,
                type
            );
            return;
        }

        GlobalTileHooks.MouseOverFar.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override bool PreHitWire(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.PreHitWire.GetInvocationList().Any())
        {
            return base.PreHitWire(
                i,
                j,
                type
            );
        }

        return GlobalTileHooks.PreHitWire.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override void HitWire(
        int i,
        int j,
        int type
    )
    {
        if (!GlobalTileHooks.HitWire.GetInvocationList().Any())
        {
            base.HitWire(
                i,
                j,
                type
            );
            return;
        }

        GlobalTileHooks.HitWire.Invoke(
            this,
            i,
            j,
            type
        );
    }

    public override void FloorVisuals(
        int type,
        Terraria.Player player
    )
    {
        if (!GlobalTileHooks.FloorVisuals.GetInvocationList().Any())
        {
            base.FloorVisuals(
                type,
                player
            );
            return;
        }

        GlobalTileHooks.FloorVisuals.Invoke(
            this,
            type,
            player
        );
    }

    public override void ChangeWaterfallStyle(
        int type,
        ref int style
    )
    {
        if (!GlobalTileHooks.ChangeWaterfallStyle.GetInvocationList().Any())
        {
            base.ChangeWaterfallStyle(
                type,
                ref style
            );
            return;
        }

        GlobalTileHooks.ChangeWaterfallStyle.Invoke(
            this,
            type,
            ref style
        );
    }

    public override bool CanReplace(
        int i,
        int j,
        int type,
        int tileTypeBeingPlaced
    )
    {
        if (!GlobalTileHooks.CanReplace.GetInvocationList().Any())
        {
            return base.CanReplace(
                i,
                j,
                type,
                tileTypeBeingPlaced
            );
        }

        return GlobalTileHooks.CanReplace.Invoke(
            this,
            i,
            j,
            type,
            tileTypeBeingPlaced
        );
    }

    public override void ReplaceTile(
        int i,
        int j,
        int type,
        int targetType,
        int targetStyle
    )
    {
        if (!GlobalTileHooks.ReplaceTile.GetInvocationList().Any())
        {
            base.ReplaceTile(
                i,
                j,
                type,
                targetType,
                targetStyle
            );
            return;
        }

        GlobalTileHooks.ReplaceTile.Invoke(
            this,
            i,
            j,
            type,
            targetType,
            targetStyle
        );
    }

    public override void PostSetupTileMerge()
    {
        if (!GlobalTileHooks.PostSetupTileMerge.GetInvocationList().Any())
        {
            base.PostSetupTileMerge();
            return;
        }

        GlobalTileHooks.PostSetupTileMerge.Invoke(
            this
        );
    }

    public override void PreShakeTree(
        int x,
        int y,
        Terraria.Enums.TreeTypes treeType
    )
    {
        if (!GlobalTileHooks.PreShakeTree.GetInvocationList().Any())
        {
            base.PreShakeTree(
                x,
                y,
                treeType
            );
            return;
        }

        GlobalTileHooks.PreShakeTree.Invoke(
            this,
            x,
            y,
            treeType
        );
    }

    public override bool ShakeTree(
        int x,
        int y,
        Terraria.Enums.TreeTypes treeType
    )
    {
        if (!GlobalTileHooks.ShakeTree.GetInvocationList().Any())
        {
            return base.ShakeTree(
                x,
                y,
                treeType
            );
        }

        return GlobalTileHooks.ShakeTree.Invoke(
            this,
            x,
            y,
            treeType
        );
    }
}

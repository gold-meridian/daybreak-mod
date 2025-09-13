namespace Daybreak.Common.Features.Hooks;

using System.Linq;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.ModSystem':
//     System.Void Terraria.ModLoader.ModSystem::PostSetupContent()
//     System.Void Terraria.ModLoader.ModSystem::OnLocalizationsLoaded()
//     System.Void Terraria.ModLoader.ModSystem::AddRecipes()
//     System.Void Terraria.ModLoader.ModSystem::PostAddRecipes()
//     System.Void Terraria.ModLoader.ModSystem::PostSetupRecipes()
//     System.Void Terraria.ModLoader.ModSystem::AddRecipeGroups()
//     System.Void Terraria.ModLoader.ModSystem::OnWorldLoad()
//     System.Void Terraria.ModLoader.ModSystem::PostWorldLoad()
//     System.Void Terraria.ModLoader.ModSystem::OnWorldUnload()
//     System.Void Terraria.ModLoader.ModSystem::ClearWorld()
//     System.Void Terraria.ModLoader.ModSystem::ModifyScreenPosition()
//     System.Void Terraria.ModLoader.ModSystem::ModifyTransformMatrix(Terraria.Graphics.SpriteViewMatrix&)
//     System.Void Terraria.ModLoader.ModSystem::UpdateUI(Microsoft.Xna.Framework.GameTime)
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateEntities()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdatePlayers()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdatePlayers()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateNPCs()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateNPCs()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateGores()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateGores()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateProjectiles()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateProjectiles()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateItems()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateItems()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateDusts()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateDusts()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateTime()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateTime()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateWorld()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateWorld()
//     System.Void Terraria.ModLoader.ModSystem::PreUpdateInvasions()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateInvasions()
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateEverything()
//     System.Void Terraria.ModLoader.ModSystem::ModifyInterfaceLayers(System.Collections.Generic.List`1<Terraria.UI.GameInterfaceLayer>)
//     System.Void Terraria.ModLoader.ModSystem::ModifyGameTipVisibility(System.Collections.Generic.IReadOnlyList`1<Terraria.ModLoader.GameTipData>)
//     System.Void Terraria.ModLoader.ModSystem::PostDrawInterface(Microsoft.Xna.Framework.Graphics.SpriteBatch)
//     System.Void Terraria.ModLoader.ModSystem::PreDrawMapIconOverlay(System.Collections.Generic.IReadOnlyList`1<Terraria.Map.IMapLayer>,Terraria.Map.MapOverlayDrawContext)
//     System.Void Terraria.ModLoader.ModSystem::PostDrawFullscreenMap(System.String&)
//     System.Void Terraria.ModLoader.ModSystem::PostUpdateInput()
//     System.Void Terraria.ModLoader.ModSystem::PreSaveAndQuit()
//     System.Void Terraria.ModLoader.ModSystem::PostDrawTiles()
//     System.Void Terraria.ModLoader.ModSystem::ModifyTimeRate(System.Double&,System.Double&,System.Double&)
//     System.Void Terraria.ModLoader.ModSystem::SaveWorldData(Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.ModSystem::LoadWorldData(Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.ModSystem::SaveWorldHeader(Terraria.ModLoader.IO.TagCompound)
//     System.Void Terraria.ModLoader.ModSystem::PreWorldGen()
//     System.Void Terraria.ModLoader.ModSystem::ModifyWorldGenTasks(System.Collections.Generic.List`1<Terraria.WorldBuilding.GenPass>,System.Double&)
//     System.Void Terraria.ModLoader.ModSystem::PostWorldGen()
//     System.Void Terraria.ModLoader.ModSystem::ResetNearbyTileEffects()
//     System.Void Terraria.ModLoader.ModSystem::ModifyHardmodeTasks(System.Collections.Generic.List`1<Terraria.WorldBuilding.GenPass>)
//     System.Void Terraria.ModLoader.ModSystem::ModifySunLightColor(Microsoft.Xna.Framework.Color&,Microsoft.Xna.Framework.Color&)
//     System.Void Terraria.ModLoader.ModSystem::ModifyLightingBrightness(System.Single&)
//     System.Void Terraria.ModLoader.ModSystem::TileCountsAvailable(System.ReadOnlySpan`1<System.Int32>)
//     System.Void Terraria.ModLoader.ModSystem::ResizeArrays()
public static partial class ModSystemHooks
{
    public sealed partial class PostSetupContent
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class OnLocalizationsLoaded
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class AddRecipes
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostAddRecipes
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostSetupRecipes
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class AddRecipeGroups
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class OnWorldLoad
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostWorldLoad
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class OnWorldUnload
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ClearWorld
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyScreenPosition
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyTransformMatrix
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            ref Terraria.Graphics.SpriteViewMatrix Transform
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            ref Terraria.Graphics.SpriteViewMatrix Transform
        )
        {
            Event?.Invoke(self, ref Transform);
        }
    }

    public sealed partial class UpdateUI
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            Microsoft.Xna.Framework.GameTime gameTime
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            Microsoft.Xna.Framework.GameTime gameTime
        )
        {
            Event?.Invoke(self, gameTime);
        }
    }

    public sealed partial class PreUpdateEntities
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdatePlayers
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdatePlayers
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateNPCs
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateNPCs
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateGores
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateGores
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateProjectiles
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateProjectiles
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateItems
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateItems
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateDusts
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateDusts
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateTime
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateTime
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateWorld
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateWorld
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreUpdateInvasions
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateInvasions
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostUpdateEverything
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyInterfaceLayers
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.UI.GameInterfaceLayer> layers
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.UI.GameInterfaceLayer> layers
        )
        {
            Event?.Invoke(self, layers);
        }
    }

    public sealed partial class ModifyGameTipVisibility
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.IReadOnlyList<Terraria.ModLoader.GameTipData> gameTips
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.IReadOnlyList<Terraria.ModLoader.GameTipData> gameTips
        )
        {
            Event?.Invoke(self, gameTips);
        }
    }

    public sealed partial class PostDrawInterface
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        )
        {
            Event?.Invoke(self, spriteBatch);
        }
    }

    public sealed partial class PreDrawMapIconOverlay
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.IReadOnlyList<Terraria.Map.IMapLayer> layers,
            Terraria.Map.MapOverlayDrawContext mapOverlayDrawContext
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.IReadOnlyList<Terraria.Map.IMapLayer> layers,
            Terraria.Map.MapOverlayDrawContext mapOverlayDrawContext
        )
        {
            Event?.Invoke(self, layers, mapOverlayDrawContext);
        }
    }

    public sealed partial class PostDrawFullscreenMap
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            ref string mouseText
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            ref string mouseText
        )
        {
            Event?.Invoke(self, ref mouseText);
        }
    }

    public sealed partial class PostUpdateInput
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PreSaveAndQuit
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class PostDrawTiles
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyTimeRate
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            ref double timeRate,
            ref double tileUpdateRate,
            ref double eventUpdateRate
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            ref double timeRate,
            ref double tileUpdateRate,
            ref double eventUpdateRate
        )
        {
            Event?.Invoke(self, ref timeRate, ref tileUpdateRate, ref eventUpdateRate);
        }
    }

    public sealed partial class SaveWorldData
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, tag);
        }
    }

    public sealed partial class LoadWorldData
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, tag);
        }
    }

    public sealed partial class SaveWorldHeader
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            Terraria.ModLoader.IO.TagCompound tag
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            Terraria.ModLoader.IO.TagCompound tag
        )
        {
            Event?.Invoke(self, tag);
        }
    }

    public sealed partial class PreWorldGen
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyWorldGenTasks
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> tasks,
            ref double totalWeight
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> tasks,
            ref double totalWeight
        )
        {
            Event?.Invoke(self, tasks, ref totalWeight);
        }
    }

    public sealed partial class PostWorldGen
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ResetNearbyTileEffects
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }

    public sealed partial class ModifyHardmodeTasks
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> list
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> list
        )
        {
            Event?.Invoke(self, list);
        }
    }

    public sealed partial class ModifySunLightColor
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            ref Microsoft.Xna.Framework.Color tileColor,
            ref Microsoft.Xna.Framework.Color backgroundColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            ref Microsoft.Xna.Framework.Color tileColor,
            ref Microsoft.Xna.Framework.Color backgroundColor
        )
        {
            Event?.Invoke(self, ref tileColor, ref backgroundColor);
        }
    }

    public sealed partial class ModifyLightingBrightness
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            ref float scale
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            ref float scale
        )
        {
            Event?.Invoke(self, ref scale);
        }
    }

    public sealed partial class TileCountsAvailable
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self,
            System.ReadOnlySpan<int> tileCounts
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self,
            System.ReadOnlySpan<int> tileCounts
        )
        {
            Event?.Invoke(self, tileCounts);
        }
    }

    public sealed partial class ResizeArrays
    {
        public delegate void Definition(
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.ModSystem self
        )
        {
            Event?.Invoke(self);
        }
    }
}

public sealed partial class ModSystemImpl : Terraria.ModLoader.ModSystem
{
    public override void PostSetupContent()
    {
        if (!ModSystemHooks.PostSetupContent.GetInvocationList().Any())
        {
            base.PostSetupContent();
            return;
        }

        ModSystemHooks.PostSetupContent.Invoke(
            this
        );
    }

    public override void OnLocalizationsLoaded()
    {
        if (!ModSystemHooks.OnLocalizationsLoaded.GetInvocationList().Any())
        {
            base.OnLocalizationsLoaded();
            return;
        }

        ModSystemHooks.OnLocalizationsLoaded.Invoke(
            this
        );
    }

    public override void AddRecipes()
    {
        if (!ModSystemHooks.AddRecipes.GetInvocationList().Any())
        {
            base.AddRecipes();
            return;
        }

        ModSystemHooks.AddRecipes.Invoke(
            this
        );
    }

    public override void PostAddRecipes()
    {
        if (!ModSystemHooks.PostAddRecipes.GetInvocationList().Any())
        {
            base.PostAddRecipes();
            return;
        }

        ModSystemHooks.PostAddRecipes.Invoke(
            this
        );
    }

    public override void PostSetupRecipes()
    {
        if (!ModSystemHooks.PostSetupRecipes.GetInvocationList().Any())
        {
            base.PostSetupRecipes();
            return;
        }

        ModSystemHooks.PostSetupRecipes.Invoke(
            this
        );
    }

    public override void AddRecipeGroups()
    {
        if (!ModSystemHooks.AddRecipeGroups.GetInvocationList().Any())
        {
            base.AddRecipeGroups();
            return;
        }

        ModSystemHooks.AddRecipeGroups.Invoke(
            this
        );
    }

    public override void OnWorldLoad()
    {
        if (!ModSystemHooks.OnWorldLoad.GetInvocationList().Any())
        {
            base.OnWorldLoad();
            return;
        }

        ModSystemHooks.OnWorldLoad.Invoke(
            this
        );
    }

    public override void PostWorldLoad()
    {
        if (!ModSystemHooks.PostWorldLoad.GetInvocationList().Any())
        {
            base.PostWorldLoad();
            return;
        }

        ModSystemHooks.PostWorldLoad.Invoke(
            this
        );
    }

    public override void OnWorldUnload()
    {
        if (!ModSystemHooks.OnWorldUnload.GetInvocationList().Any())
        {
            base.OnWorldUnload();
            return;
        }

        ModSystemHooks.OnWorldUnload.Invoke(
            this
        );
    }

    public override void ClearWorld()
    {
        if (!ModSystemHooks.ClearWorld.GetInvocationList().Any())
        {
            base.ClearWorld();
            return;
        }

        ModSystemHooks.ClearWorld.Invoke(
            this
        );
    }

    public override void ModifyScreenPosition()
    {
        if (!ModSystemHooks.ModifyScreenPosition.GetInvocationList().Any())
        {
            base.ModifyScreenPosition();
            return;
        }

        ModSystemHooks.ModifyScreenPosition.Invoke(
            this
        );
    }

    public override void ModifyTransformMatrix(
        ref Terraria.Graphics.SpriteViewMatrix Transform
    )
    {
        if (!ModSystemHooks.ModifyTransformMatrix.GetInvocationList().Any())
        {
            base.ModifyTransformMatrix(
                ref Transform
            );
            return;
        }

        ModSystemHooks.ModifyTransformMatrix.Invoke(
            this,
            ref Transform
        );
    }

    public override void UpdateUI(
        Microsoft.Xna.Framework.GameTime gameTime
    )
    {
        if (!ModSystemHooks.UpdateUI.GetInvocationList().Any())
        {
            base.UpdateUI(
                gameTime
            );
            return;
        }

        ModSystemHooks.UpdateUI.Invoke(
            this,
            gameTime
        );
    }

    public override void PreUpdateEntities()
    {
        if (!ModSystemHooks.PreUpdateEntities.GetInvocationList().Any())
        {
            base.PreUpdateEntities();
            return;
        }

        ModSystemHooks.PreUpdateEntities.Invoke(
            this
        );
    }

    public override void PreUpdatePlayers()
    {
        if (!ModSystemHooks.PreUpdatePlayers.GetInvocationList().Any())
        {
            base.PreUpdatePlayers();
            return;
        }

        ModSystemHooks.PreUpdatePlayers.Invoke(
            this
        );
    }

    public override void PostUpdatePlayers()
    {
        if (!ModSystemHooks.PostUpdatePlayers.GetInvocationList().Any())
        {
            base.PostUpdatePlayers();
            return;
        }

        ModSystemHooks.PostUpdatePlayers.Invoke(
            this
        );
    }

    public override void PreUpdateNPCs()
    {
        if (!ModSystemHooks.PreUpdateNPCs.GetInvocationList().Any())
        {
            base.PreUpdateNPCs();
            return;
        }

        ModSystemHooks.PreUpdateNPCs.Invoke(
            this
        );
    }

    public override void PostUpdateNPCs()
    {
        if (!ModSystemHooks.PostUpdateNPCs.GetInvocationList().Any())
        {
            base.PostUpdateNPCs();
            return;
        }

        ModSystemHooks.PostUpdateNPCs.Invoke(
            this
        );
    }

    public override void PreUpdateGores()
    {
        if (!ModSystemHooks.PreUpdateGores.GetInvocationList().Any())
        {
            base.PreUpdateGores();
            return;
        }

        ModSystemHooks.PreUpdateGores.Invoke(
            this
        );
    }

    public override void PostUpdateGores()
    {
        if (!ModSystemHooks.PostUpdateGores.GetInvocationList().Any())
        {
            base.PostUpdateGores();
            return;
        }

        ModSystemHooks.PostUpdateGores.Invoke(
            this
        );
    }

    public override void PreUpdateProjectiles()
    {
        if (!ModSystemHooks.PreUpdateProjectiles.GetInvocationList().Any())
        {
            base.PreUpdateProjectiles();
            return;
        }

        ModSystemHooks.PreUpdateProjectiles.Invoke(
            this
        );
    }

    public override void PostUpdateProjectiles()
    {
        if (!ModSystemHooks.PostUpdateProjectiles.GetInvocationList().Any())
        {
            base.PostUpdateProjectiles();
            return;
        }

        ModSystemHooks.PostUpdateProjectiles.Invoke(
            this
        );
    }

    public override void PreUpdateItems()
    {
        if (!ModSystemHooks.PreUpdateItems.GetInvocationList().Any())
        {
            base.PreUpdateItems();
            return;
        }

        ModSystemHooks.PreUpdateItems.Invoke(
            this
        );
    }

    public override void PostUpdateItems()
    {
        if (!ModSystemHooks.PostUpdateItems.GetInvocationList().Any())
        {
            base.PostUpdateItems();
            return;
        }

        ModSystemHooks.PostUpdateItems.Invoke(
            this
        );
    }

    public override void PreUpdateDusts()
    {
        if (!ModSystemHooks.PreUpdateDusts.GetInvocationList().Any())
        {
            base.PreUpdateDusts();
            return;
        }

        ModSystemHooks.PreUpdateDusts.Invoke(
            this
        );
    }

    public override void PostUpdateDusts()
    {
        if (!ModSystemHooks.PostUpdateDusts.GetInvocationList().Any())
        {
            base.PostUpdateDusts();
            return;
        }

        ModSystemHooks.PostUpdateDusts.Invoke(
            this
        );
    }

    public override void PreUpdateTime()
    {
        if (!ModSystemHooks.PreUpdateTime.GetInvocationList().Any())
        {
            base.PreUpdateTime();
            return;
        }

        ModSystemHooks.PreUpdateTime.Invoke(
            this
        );
    }

    public override void PostUpdateTime()
    {
        if (!ModSystemHooks.PostUpdateTime.GetInvocationList().Any())
        {
            base.PostUpdateTime();
            return;
        }

        ModSystemHooks.PostUpdateTime.Invoke(
            this
        );
    }

    public override void PreUpdateWorld()
    {
        if (!ModSystemHooks.PreUpdateWorld.GetInvocationList().Any())
        {
            base.PreUpdateWorld();
            return;
        }

        ModSystemHooks.PreUpdateWorld.Invoke(
            this
        );
    }

    public override void PostUpdateWorld()
    {
        if (!ModSystemHooks.PostUpdateWorld.GetInvocationList().Any())
        {
            base.PostUpdateWorld();
            return;
        }

        ModSystemHooks.PostUpdateWorld.Invoke(
            this
        );
    }

    public override void PreUpdateInvasions()
    {
        if (!ModSystemHooks.PreUpdateInvasions.GetInvocationList().Any())
        {
            base.PreUpdateInvasions();
            return;
        }

        ModSystemHooks.PreUpdateInvasions.Invoke(
            this
        );
    }

    public override void PostUpdateInvasions()
    {
        if (!ModSystemHooks.PostUpdateInvasions.GetInvocationList().Any())
        {
            base.PostUpdateInvasions();
            return;
        }

        ModSystemHooks.PostUpdateInvasions.Invoke(
            this
        );
    }

    public override void PostUpdateEverything()
    {
        if (!ModSystemHooks.PostUpdateEverything.GetInvocationList().Any())
        {
            base.PostUpdateEverything();
            return;
        }

        ModSystemHooks.PostUpdateEverything.Invoke(
            this
        );
    }

    public override void ModifyInterfaceLayers(
        System.Collections.Generic.List<Terraria.UI.GameInterfaceLayer> layers
    )
    {
        if (!ModSystemHooks.ModifyInterfaceLayers.GetInvocationList().Any())
        {
            base.ModifyInterfaceLayers(
                layers
            );
            return;
        }

        ModSystemHooks.ModifyInterfaceLayers.Invoke(
            this,
            layers
        );
    }

    public override void ModifyGameTipVisibility(
        System.Collections.Generic.IReadOnlyList<Terraria.ModLoader.GameTipData> gameTips
    )
    {
        if (!ModSystemHooks.ModifyGameTipVisibility.GetInvocationList().Any())
        {
            base.ModifyGameTipVisibility(
                gameTips
            );
            return;
        }

        ModSystemHooks.ModifyGameTipVisibility.Invoke(
            this,
            gameTips
        );
    }

    public override void PostDrawInterface(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        if (!ModSystemHooks.PostDrawInterface.GetInvocationList().Any())
        {
            base.PostDrawInterface(
                spriteBatch
            );
            return;
        }

        ModSystemHooks.PostDrawInterface.Invoke(
            this,
            spriteBatch
        );
    }

    public override void PreDrawMapIconOverlay(
        System.Collections.Generic.IReadOnlyList<Terraria.Map.IMapLayer> layers,
        Terraria.Map.MapOverlayDrawContext mapOverlayDrawContext
    )
    {
        if (!ModSystemHooks.PreDrawMapIconOverlay.GetInvocationList().Any())
        {
            base.PreDrawMapIconOverlay(
                layers,
                mapOverlayDrawContext
            );
            return;
        }

        ModSystemHooks.PreDrawMapIconOverlay.Invoke(
            this,
            layers,
            mapOverlayDrawContext
        );
    }

    public override void PostDrawFullscreenMap(
        ref string mouseText
    )
    {
        if (!ModSystemHooks.PostDrawFullscreenMap.GetInvocationList().Any())
        {
            base.PostDrawFullscreenMap(
                ref mouseText
            );
            return;
        }

        ModSystemHooks.PostDrawFullscreenMap.Invoke(
            this,
            ref mouseText
        );
    }

    public override void PostUpdateInput()
    {
        if (!ModSystemHooks.PostUpdateInput.GetInvocationList().Any())
        {
            base.PostUpdateInput();
            return;
        }

        ModSystemHooks.PostUpdateInput.Invoke(
            this
        );
    }

    public override void PreSaveAndQuit()
    {
        if (!ModSystemHooks.PreSaveAndQuit.GetInvocationList().Any())
        {
            base.PreSaveAndQuit();
            return;
        }

        ModSystemHooks.PreSaveAndQuit.Invoke(
            this
        );
    }

    public override void PostDrawTiles()
    {
        if (!ModSystemHooks.PostDrawTiles.GetInvocationList().Any())
        {
            base.PostDrawTiles();
            return;
        }

        ModSystemHooks.PostDrawTiles.Invoke(
            this
        );
    }

    public override void ModifyTimeRate(
        ref double timeRate,
        ref double tileUpdateRate,
        ref double eventUpdateRate
    )
    {
        if (!ModSystemHooks.ModifyTimeRate.GetInvocationList().Any())
        {
            base.ModifyTimeRate(
                ref timeRate,
                ref tileUpdateRate,
                ref eventUpdateRate
            );
            return;
        }

        ModSystemHooks.ModifyTimeRate.Invoke(
            this,
            ref timeRate,
            ref tileUpdateRate,
            ref eventUpdateRate
        );
    }

    public override void SaveWorldData(
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!ModSystemHooks.SaveWorldData.GetInvocationList().Any())
        {
            base.SaveWorldData(
                tag
            );
            return;
        }

        ModSystemHooks.SaveWorldData.Invoke(
            this,
            tag
        );
    }

    public override void LoadWorldData(
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!ModSystemHooks.LoadWorldData.GetInvocationList().Any())
        {
            base.LoadWorldData(
                tag
            );
            return;
        }

        ModSystemHooks.LoadWorldData.Invoke(
            this,
            tag
        );
    }

    public override void SaveWorldHeader(
        Terraria.ModLoader.IO.TagCompound tag
    )
    {
        if (!ModSystemHooks.SaveWorldHeader.GetInvocationList().Any())
        {
            base.SaveWorldHeader(
                tag
            );
            return;
        }

        ModSystemHooks.SaveWorldHeader.Invoke(
            this,
            tag
        );
    }

    public override void PreWorldGen()
    {
        if (!ModSystemHooks.PreWorldGen.GetInvocationList().Any())
        {
            base.PreWorldGen();
            return;
        }

        ModSystemHooks.PreWorldGen.Invoke(
            this
        );
    }

    public override void ModifyWorldGenTasks(
        System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> tasks,
        ref double totalWeight
    )
    {
        if (!ModSystemHooks.ModifyWorldGenTasks.GetInvocationList().Any())
        {
            base.ModifyWorldGenTasks(
                tasks,
                ref totalWeight
            );
            return;
        }

        ModSystemHooks.ModifyWorldGenTasks.Invoke(
            this,
            tasks,
            ref totalWeight
        );
    }

    public override void PostWorldGen()
    {
        if (!ModSystemHooks.PostWorldGen.GetInvocationList().Any())
        {
            base.PostWorldGen();
            return;
        }

        ModSystemHooks.PostWorldGen.Invoke(
            this
        );
    }

    public override void ResetNearbyTileEffects()
    {
        if (!ModSystemHooks.ResetNearbyTileEffects.GetInvocationList().Any())
        {
            base.ResetNearbyTileEffects();
            return;
        }

        ModSystemHooks.ResetNearbyTileEffects.Invoke(
            this
        );
    }

    public override void ModifyHardmodeTasks(
        System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> list
    )
    {
        if (!ModSystemHooks.ModifyHardmodeTasks.GetInvocationList().Any())
        {
            base.ModifyHardmodeTasks(
                list
            );
            return;
        }

        ModSystemHooks.ModifyHardmodeTasks.Invoke(
            this,
            list
        );
    }

    public override void ModifySunLightColor(
        ref Microsoft.Xna.Framework.Color tileColor,
        ref Microsoft.Xna.Framework.Color backgroundColor
    )
    {
        if (!ModSystemHooks.ModifySunLightColor.GetInvocationList().Any())
        {
            base.ModifySunLightColor(
                ref tileColor,
                ref backgroundColor
            );
            return;
        }

        ModSystemHooks.ModifySunLightColor.Invoke(
            this,
            ref tileColor,
            ref backgroundColor
        );
    }

    public override void ModifyLightingBrightness(
        ref float scale
    )
    {
        if (!ModSystemHooks.ModifyLightingBrightness.GetInvocationList().Any())
        {
            base.ModifyLightingBrightness(
                ref scale
            );
            return;
        }

        ModSystemHooks.ModifyLightingBrightness.Invoke(
            this,
            ref scale
        );
    }

    public override void TileCountsAvailable(
        System.ReadOnlySpan<int> tileCounts
    )
    {
        if (!ModSystemHooks.TileCountsAvailable.GetInvocationList().Any())
        {
            base.TileCountsAvailable(
                tileCounts
            );
            return;
        }

        ModSystemHooks.TileCountsAvailable.Invoke(
            this,
            tileCounts
        );
    }

    public override void ResizeArrays()
    {
        if (!ModSystemHooks.ResizeArrays.GetInvocationList().Any())
        {
            base.ResizeArrays();
            return;
        }

        ModSystemHooks.ResizeArrays.Invoke(
            this
        );
    }
}

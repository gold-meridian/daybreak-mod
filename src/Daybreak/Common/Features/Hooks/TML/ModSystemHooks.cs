namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.ModSystem':
//     System.Void Terraria.ModLoader.ModSystem::SetupContent()
//     System.Void Terraria.ModLoader.ModSystem::OnModLoad()
//     System.Void Terraria.ModLoader.ModSystem::OnModUnload()
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
//     System.Boolean Terraria.ModLoader.ModSystem::CanWorldBePlayed(Terraria.IO.PlayerFileData,Terraria.IO.WorldFileData)
//     System.String Terraria.ModLoader.ModSystem::WorldCanBePlayedRejectionMessage(Terraria.IO.PlayerFileData,Terraria.IO.WorldFileData)
//     System.Boolean Terraria.ModLoader.ModSystem::HijackGetData(System.Byte&,System.IO.BinaryReader&,System.Int32)
//     System.Boolean Terraria.ModLoader.ModSystem::HijackSendData(System.Int32,System.Int32,System.Int32,System.Int32,Terraria.Localization.NetworkText,System.Int32,System.Single,System.Single,System.Single,System.Int32,System.Int32,System.Int32)
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
    public sealed partial class SetupContent
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_SetupContent_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::SetupContent")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::SetupContent; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnModLoad
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_OnModLoad_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::OnModLoad")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::OnModLoad; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnModUnload
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_OnModUnload_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::OnModUnload")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::OnModUnload; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostSetupContent
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostSetupContent_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostSetupContent")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostSetupContent; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnLocalizationsLoaded
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_OnLocalizationsLoaded_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::OnLocalizationsLoaded")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::OnLocalizationsLoaded; use a flag to disable behavior.");
        }
    }

    public sealed partial class AddRecipes
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_AddRecipes_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::AddRecipes")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::AddRecipes; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostAddRecipes
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostAddRecipes_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostAddRecipes")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostAddRecipes; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostSetupRecipes
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostSetupRecipes_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostSetupRecipes")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostSetupRecipes; use a flag to disable behavior.");
        }
    }

    public sealed partial class AddRecipeGroups
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_AddRecipeGroups_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::AddRecipeGroups")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::AddRecipeGroups; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnWorldLoad
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_OnWorldLoad_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::OnWorldLoad")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::OnWorldLoad; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostWorldLoad
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostWorldLoad_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostWorldLoad")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostWorldLoad; use a flag to disable behavior.");
        }
    }

    public sealed partial class OnWorldUnload
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_OnWorldUnload_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::OnWorldUnload")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::OnWorldUnload; use a flag to disable behavior.");
        }
    }

    public sealed partial class ClearWorld
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ClearWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ClearWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ClearWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyScreenPosition
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyScreenPosition_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyScreenPosition")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyScreenPosition; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyTransformMatrix
    {
        public delegate void Original(
            ref Terraria.Graphics.SpriteViewMatrix Transform
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            ref Terraria.Graphics.SpriteViewMatrix Transform
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyTransformMatrix_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyTransformMatrix")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyTransformMatrix; use a flag to disable behavior.");
        }
    }

    public sealed partial class UpdateUI
    {
        public delegate void Original(
            Microsoft.Xna.Framework.GameTime gameTime
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            Microsoft.Xna.Framework.GameTime gameTime
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_UpdateUI_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::UpdateUI")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::UpdateUI; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateEntities
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateEntities_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateEntities")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateEntities; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdatePlayers
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdatePlayers_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdatePlayers")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdatePlayers; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdatePlayers
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdatePlayers_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdatePlayers")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdatePlayers; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateNPCs
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateNPCs_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateNPCs")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateNPCs; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateNPCs
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateNPCs_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateNPCs")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateNPCs; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateGores
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateGores_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateGores")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateGores; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateGores
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateGores_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateGores")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateGores; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateProjectiles
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateProjectiles_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateProjectiles")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateProjectiles; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateProjectiles
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateProjectiles_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateProjectiles")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateProjectiles; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateItems
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateItems_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateItems")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateItems; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateItems
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateItems_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateItems")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateItems; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateDusts
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateDusts_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateDusts")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateDusts; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateDusts
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateDusts_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateDusts")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateDusts; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateTime
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateTime_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateTime")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateTime; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateTime
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateTime_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateTime")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateTime; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateWorld
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateWorld
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreUpdateInvasions
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreUpdateInvasions_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreUpdateInvasions")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreUpdateInvasions; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateInvasions
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateInvasions_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateInvasions")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateInvasions; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateEverything
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateEverything_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateEverything")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateEverything; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyInterfaceLayers
    {
        public delegate void Original(
            System.Collections.Generic.List<Terraria.UI.GameInterfaceLayer> layers
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.UI.GameInterfaceLayer> layers
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyInterfaceLayers_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyInterfaceLayers")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyInterfaceLayers; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyGameTipVisibility
    {
        public delegate void Original(
            System.Collections.Generic.IReadOnlyList<Terraria.ModLoader.GameTipData> gameTips
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.IReadOnlyList<Terraria.ModLoader.GameTipData> gameTips
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyGameTipVisibility_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyGameTipVisibility")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyGameTipVisibility; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawInterface
    {
        public delegate void Original(
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostDrawInterface_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostDrawInterface")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostDrawInterface; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDrawMapIconOverlay
    {
        public delegate void Original(
            System.Collections.Generic.IReadOnlyList<Terraria.Map.IMapLayer> layers,
            Terraria.Map.MapOverlayDrawContext mapOverlayDrawContext
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.IReadOnlyList<Terraria.Map.IMapLayer> layers,
            Terraria.Map.MapOverlayDrawContext mapOverlayDrawContext
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreDrawMapIconOverlay_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreDrawMapIconOverlay")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreDrawMapIconOverlay; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawFullscreenMap
    {
        public delegate void Original(
            ref string mouseText
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            ref string mouseText
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostDrawFullscreenMap_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostDrawFullscreenMap")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostDrawFullscreenMap; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostUpdateInput
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostUpdateInput_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostUpdateInput")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostUpdateInput; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreSaveAndQuit
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreSaveAndQuit_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreSaveAndQuit")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreSaveAndQuit; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDrawTiles
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostDrawTiles_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostDrawTiles")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostDrawTiles; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyTimeRate
    {
        public delegate void Original(
            ref double timeRate,
            ref double tileUpdateRate,
            ref double eventUpdateRate
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            ref double timeRate,
            ref double tileUpdateRate,
            ref double eventUpdateRate
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyTimeRate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyTimeRate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyTimeRate; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanWorldBePlayed
    {
        public delegate bool Original(
            Terraria.IO.PlayerFileData playerData,
            Terraria.IO.WorldFileData worldFileData
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            Terraria.IO.PlayerFileData playerData,
            Terraria.IO.WorldFileData worldFileData
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_CanWorldBePlayed_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::CanWorldBePlayed")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::CanWorldBePlayed; use a flag to disable behavior.");
        }
    }

    public sealed partial class WorldCanBePlayedRejectionMessage
    {
        public delegate string Original(
            Terraria.IO.PlayerFileData playerData,
            Terraria.IO.WorldFileData worldData
        );

        public delegate string Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            Terraria.IO.PlayerFileData playerData,
            Terraria.IO.WorldFileData worldData
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_WorldCanBePlayedRejectionMessage_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::WorldCanBePlayedRejectionMessage")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::WorldCanBePlayedRejectionMessage; use a flag to disable behavior.");
        }
    }

    public sealed partial class HijackGetData
    {
        public delegate bool Original(
            ref byte messageType,
            ref System.IO.BinaryReader reader,
            int playerNumber
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            ref byte messageType,
            ref System.IO.BinaryReader reader,
            int playerNumber
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_HijackGetData_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::HijackGetData")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::HijackGetData; use a flag to disable behavior.");
        }
    }

    public sealed partial class HijackSendData
    {
        public delegate bool Original(
            int whoAmI,
            int msgType,
            int remoteClient,
            int ignoreClient,
            Terraria.Localization.NetworkText text,
            int number,
            float number2,
            float number3,
            float number4,
            int number5,
            int number6,
            int number7
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            int whoAmI,
            int msgType,
            int remoteClient,
            int ignoreClient,
            Terraria.Localization.NetworkText text,
            int number,
            float number2,
            float number3,
            float number4,
            int number5,
            int number6,
            int number7
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_HijackSendData_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::HijackSendData")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::HijackSendData; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreWorldGen
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PreWorldGen_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PreWorldGen")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PreWorldGen; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyWorldGenTasks
    {
        public delegate void Original(
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> tasks,
            ref double totalWeight
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> tasks,
            ref double totalWeight
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyWorldGenTasks_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyWorldGenTasks")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyWorldGenTasks; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostWorldGen
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_PostWorldGen_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::PostWorldGen")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::PostWorldGen; use a flag to disable behavior.");
        }
    }

    public sealed partial class ResetNearbyTileEffects
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ResetNearbyTileEffects_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ResetNearbyTileEffects")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ResetNearbyTileEffects; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyHardmodeTasks
    {
        public delegate void Original(
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> list
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> list
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyHardmodeTasks_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyHardmodeTasks")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyHardmodeTasks; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifySunLightColor
    {
        public delegate void Original(
            ref Microsoft.Xna.Framework.Color tileColor,
            ref Microsoft.Xna.Framework.Color backgroundColor
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            ref Microsoft.Xna.Framework.Color tileColor,
            ref Microsoft.Xna.Framework.Color backgroundColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifySunLightColor_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifySunLightColor")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifySunLightColor; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyLightingBrightness
    {
        public delegate void Original(
            ref float scale
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            ref float scale
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ModifyLightingBrightness_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ModifyLightingBrightness")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ModifyLightingBrightness; use a flag to disable behavior.");
        }
    }

    public sealed partial class TileCountsAvailable
    {
        public delegate void Original(
            System.ReadOnlySpan<int> tileCounts
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self,
            System.ReadOnlySpan<int> tileCounts
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_TileCountsAvailable_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::TileCountsAvailable")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::TileCountsAvailable; use a flag to disable behavior.");
        }
    }

    public sealed partial class ResizeArrays
    {
        public delegate void Original();

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.ModSystem self
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new ModSystem_ResizeArrays_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: ModSystem::ResizeArrays")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: ModSystem::ResizeArrays; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_SetupContent_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.SetupContent.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_SetupContent_Impl(ModSystemHooks.SetupContent.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void SetupContent()
    {
        hook(
            () => base.SetupContent(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_OnModLoad_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.OnModLoad.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_OnModLoad_Impl(ModSystemHooks.OnModLoad.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnModLoad()
    {
        hook(
            () => base.OnModLoad(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_OnModUnload_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.OnModUnload.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_OnModUnload_Impl(ModSystemHooks.OnModUnload.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnModUnload()
    {
        hook(
            () => base.OnModUnload(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostSetupContent_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostSetupContent.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostSetupContent_Impl(ModSystemHooks.PostSetupContent.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostSetupContent()
    {
        hook(
            () => base.PostSetupContent(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_OnLocalizationsLoaded_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.OnLocalizationsLoaded.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_OnLocalizationsLoaded_Impl(ModSystemHooks.OnLocalizationsLoaded.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnLocalizationsLoaded()
    {
        hook(
            () => base.OnLocalizationsLoaded(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_AddRecipes_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.AddRecipes.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_AddRecipes_Impl(ModSystemHooks.AddRecipes.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void AddRecipes()
    {
        hook(
            () => base.AddRecipes(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostAddRecipes_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostAddRecipes.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostAddRecipes_Impl(ModSystemHooks.PostAddRecipes.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostAddRecipes()
    {
        hook(
            () => base.PostAddRecipes(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostSetupRecipes_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostSetupRecipes.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostSetupRecipes_Impl(ModSystemHooks.PostSetupRecipes.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostSetupRecipes()
    {
        hook(
            () => base.PostSetupRecipes(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_AddRecipeGroups_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.AddRecipeGroups.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_AddRecipeGroups_Impl(ModSystemHooks.AddRecipeGroups.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void AddRecipeGroups()
    {
        hook(
            () => base.AddRecipeGroups(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_OnWorldLoad_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.OnWorldLoad.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_OnWorldLoad_Impl(ModSystemHooks.OnWorldLoad.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnWorldLoad()
    {
        hook(
            () => base.OnWorldLoad(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostWorldLoad_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostWorldLoad.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostWorldLoad_Impl(ModSystemHooks.PostWorldLoad.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostWorldLoad()
    {
        hook(
            () => base.PostWorldLoad(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_OnWorldUnload_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.OnWorldUnload.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_OnWorldUnload_Impl(ModSystemHooks.OnWorldUnload.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnWorldUnload()
    {
        hook(
            () => base.OnWorldUnload(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ClearWorld_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ClearWorld.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ClearWorld_Impl(ModSystemHooks.ClearWorld.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ClearWorld()
    {
        hook(
            () => base.ClearWorld(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifyScreenPosition_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyScreenPosition.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyScreenPosition_Impl(ModSystemHooks.ModifyScreenPosition.Definition hook) : this()
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
public sealed partial class ModSystem_ModifyTransformMatrix_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyTransformMatrix.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyTransformMatrix_Impl(ModSystemHooks.ModifyTransformMatrix.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyTransformMatrix(
        ref Terraria.Graphics.SpriteViewMatrix Transform
    )
    {
        hook(
            (
                ref Terraria.Graphics.SpriteViewMatrix Transform_captured
            ) => base.ModifyTransformMatrix(
                ref Transform_captured
            ),
            this,
            ref Transform
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_UpdateUI_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.UpdateUI.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_UpdateUI_Impl(ModSystemHooks.UpdateUI.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void UpdateUI(
        Microsoft.Xna.Framework.GameTime gameTime
    )
    {
        hook(
            (
                Microsoft.Xna.Framework.GameTime gameTime_captured
            ) => base.UpdateUI(
                gameTime_captured
            ),
            this,
            gameTime
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateEntities_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateEntities.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateEntities_Impl(ModSystemHooks.PreUpdateEntities.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateEntities()
    {
        hook(
            () => base.PreUpdateEntities(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdatePlayers_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdatePlayers.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdatePlayers_Impl(ModSystemHooks.PreUpdatePlayers.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdatePlayers()
    {
        hook(
            () => base.PreUpdatePlayers(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdatePlayers_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdatePlayers.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdatePlayers_Impl(ModSystemHooks.PostUpdatePlayers.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdatePlayers()
    {
        hook(
            () => base.PostUpdatePlayers(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateNPCs_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateNPCs.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateNPCs_Impl(ModSystemHooks.PreUpdateNPCs.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateNPCs()
    {
        hook(
            () => base.PreUpdateNPCs(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateNPCs_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateNPCs.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateNPCs_Impl(ModSystemHooks.PostUpdateNPCs.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateNPCs()
    {
        hook(
            () => base.PostUpdateNPCs(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateGores_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateGores.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateGores_Impl(ModSystemHooks.PreUpdateGores.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateGores()
    {
        hook(
            () => base.PreUpdateGores(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateGores_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateGores.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateGores_Impl(ModSystemHooks.PostUpdateGores.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateGores()
    {
        hook(
            () => base.PostUpdateGores(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateProjectiles_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateProjectiles.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateProjectiles_Impl(ModSystemHooks.PreUpdateProjectiles.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateProjectiles()
    {
        hook(
            () => base.PreUpdateProjectiles(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateProjectiles_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateProjectiles.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateProjectiles_Impl(ModSystemHooks.PostUpdateProjectiles.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateProjectiles()
    {
        hook(
            () => base.PostUpdateProjectiles(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateItems_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateItems.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateItems_Impl(ModSystemHooks.PreUpdateItems.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateItems()
    {
        hook(
            () => base.PreUpdateItems(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateItems_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateItems.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateItems_Impl(ModSystemHooks.PostUpdateItems.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateItems()
    {
        hook(
            () => base.PostUpdateItems(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateDusts_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateDusts.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateDusts_Impl(ModSystemHooks.PreUpdateDusts.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateDusts()
    {
        hook(
            () => base.PreUpdateDusts(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateDusts_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateDusts.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateDusts_Impl(ModSystemHooks.PostUpdateDusts.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateDusts()
    {
        hook(
            () => base.PostUpdateDusts(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateTime_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateTime.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateTime_Impl(ModSystemHooks.PreUpdateTime.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateTime()
    {
        hook(
            () => base.PreUpdateTime(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateTime_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateTime.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateTime_Impl(ModSystemHooks.PostUpdateTime.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateTime()
    {
        hook(
            () => base.PostUpdateTime(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateWorld_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateWorld.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateWorld_Impl(ModSystemHooks.PreUpdateWorld.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateWorld()
    {
        hook(
            () => base.PreUpdateWorld(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateWorld_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateWorld.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateWorld_Impl(ModSystemHooks.PostUpdateWorld.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateWorld()
    {
        hook(
            () => base.PostUpdateWorld(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreUpdateInvasions_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreUpdateInvasions.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreUpdateInvasions_Impl(ModSystemHooks.PreUpdateInvasions.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreUpdateInvasions()
    {
        hook(
            () => base.PreUpdateInvasions(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateInvasions_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateInvasions.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateInvasions_Impl(ModSystemHooks.PostUpdateInvasions.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateInvasions()
    {
        hook(
            () => base.PostUpdateInvasions(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateEverything_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateEverything.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateEverything_Impl(ModSystemHooks.PostUpdateEverything.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateEverything()
    {
        hook(
            () => base.PostUpdateEverything(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifyInterfaceLayers_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyInterfaceLayers.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyInterfaceLayers_Impl(ModSystemHooks.ModifyInterfaceLayers.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyInterfaceLayers(
        System.Collections.Generic.List<Terraria.UI.GameInterfaceLayer> layers
    )
    {
        hook(
            (
                System.Collections.Generic.List<Terraria.UI.GameInterfaceLayer> layers_captured
            ) => base.ModifyInterfaceLayers(
                layers_captured
            ),
            this,
            layers
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifyGameTipVisibility_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyGameTipVisibility.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyGameTipVisibility_Impl(ModSystemHooks.ModifyGameTipVisibility.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyGameTipVisibility(
        System.Collections.Generic.IReadOnlyList<Terraria.ModLoader.GameTipData> gameTips
    )
    {
        hook(
            (
                System.Collections.Generic.IReadOnlyList<Terraria.ModLoader.GameTipData> gameTips_captured
            ) => base.ModifyGameTipVisibility(
                gameTips_captured
            ),
            this,
            gameTips
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostDrawInterface_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostDrawInterface.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostDrawInterface_Impl(ModSystemHooks.PostDrawInterface.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDrawInterface(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        hook(
            (
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured
            ) => base.PostDrawInterface(
                spriteBatch_captured
            ),
            this,
            spriteBatch
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreDrawMapIconOverlay_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreDrawMapIconOverlay.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreDrawMapIconOverlay_Impl(ModSystemHooks.PreDrawMapIconOverlay.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreDrawMapIconOverlay(
        System.Collections.Generic.IReadOnlyList<Terraria.Map.IMapLayer> layers,
        Terraria.Map.MapOverlayDrawContext mapOverlayDrawContext
    )
    {
        hook(
            (
                System.Collections.Generic.IReadOnlyList<Terraria.Map.IMapLayer> layers_captured,
                Terraria.Map.MapOverlayDrawContext mapOverlayDrawContext_captured
            ) => base.PreDrawMapIconOverlay(
                layers_captured,
                mapOverlayDrawContext_captured
            ),
            this,
            layers,
            mapOverlayDrawContext
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostDrawFullscreenMap_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostDrawFullscreenMap.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostDrawFullscreenMap_Impl(ModSystemHooks.PostDrawFullscreenMap.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDrawFullscreenMap(
        ref string mouseText
    )
    {
        hook(
            (
                ref string mouseText_captured
            ) => base.PostDrawFullscreenMap(
                ref mouseText_captured
            ),
            this,
            ref mouseText
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostUpdateInput_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostUpdateInput.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostUpdateInput_Impl(ModSystemHooks.PostUpdateInput.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostUpdateInput()
    {
        hook(
            () => base.PostUpdateInput(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreSaveAndQuit_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreSaveAndQuit.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreSaveAndQuit_Impl(ModSystemHooks.PreSaveAndQuit.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreSaveAndQuit()
    {
        hook(
            () => base.PreSaveAndQuit(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostDrawTiles_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostDrawTiles.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostDrawTiles_Impl(ModSystemHooks.PostDrawTiles.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDrawTiles()
    {
        hook(
            () => base.PostDrawTiles(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifyTimeRate_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyTimeRate.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyTimeRate_Impl(ModSystemHooks.ModifyTimeRate.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyTimeRate(
        ref double timeRate,
        ref double tileUpdateRate,
        ref double eventUpdateRate
    )
    {
        hook(
            (
                ref double timeRate_captured,
                ref double tileUpdateRate_captured,
                ref double eventUpdateRate_captured
            ) => base.ModifyTimeRate(
                ref timeRate_captured,
                ref tileUpdateRate_captured,
                ref eventUpdateRate_captured
            ),
            this,
            ref timeRate,
            ref tileUpdateRate,
            ref eventUpdateRate
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_CanWorldBePlayed_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.CanWorldBePlayed.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_CanWorldBePlayed_Impl(ModSystemHooks.CanWorldBePlayed.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool CanWorldBePlayed(
        Terraria.IO.PlayerFileData playerData,
        Terraria.IO.WorldFileData worldFileData
    )
    {
        return hook(
            (
                Terraria.IO.PlayerFileData playerData_captured,
                Terraria.IO.WorldFileData worldFileData_captured
            ) => base.CanWorldBePlayed(
                playerData_captured,
                worldFileData_captured
            ),
            this,
            playerData,
            worldFileData
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_WorldCanBePlayedRejectionMessage_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.WorldCanBePlayedRejectionMessage.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_WorldCanBePlayedRejectionMessage_Impl(ModSystemHooks.WorldCanBePlayedRejectionMessage.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override string WorldCanBePlayedRejectionMessage(
        Terraria.IO.PlayerFileData playerData,
        Terraria.IO.WorldFileData worldData
    )
    {
        return hook(
            (
                Terraria.IO.PlayerFileData playerData_captured,
                Terraria.IO.WorldFileData worldData_captured
            ) => base.WorldCanBePlayedRejectionMessage(
                playerData_captured,
                worldData_captured
            ),
            this,
            playerData,
            worldData
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_HijackGetData_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.HijackGetData.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_HijackGetData_Impl(ModSystemHooks.HijackGetData.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool HijackGetData(
        ref byte messageType,
        ref System.IO.BinaryReader reader,
        int playerNumber
    )
    {
        return hook(
            (
                ref byte messageType_captured,
                ref System.IO.BinaryReader reader_captured,
                int playerNumber_captured
            ) => base.HijackGetData(
                ref messageType_captured,
                ref reader_captured,
                playerNumber_captured
            ),
            this,
            ref messageType,
            ref reader,
            playerNumber
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_HijackSendData_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.HijackSendData.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_HijackSendData_Impl(ModSystemHooks.HijackSendData.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool HijackSendData(
        int whoAmI,
        int msgType,
        int remoteClient,
        int ignoreClient,
        Terraria.Localization.NetworkText text,
        int number,
        float number2,
        float number3,
        float number4,
        int number5,
        int number6,
        int number7
    )
    {
        return hook(
            (
                int whoAmI_captured,
                int msgType_captured,
                int remoteClient_captured,
                int ignoreClient_captured,
                Terraria.Localization.NetworkText text_captured,
                int number_captured,
                float number2_captured,
                float number3_captured,
                float number4_captured,
                int number5_captured,
                int number6_captured,
                int number7_captured
            ) => base.HijackSendData(
                whoAmI_captured,
                msgType_captured,
                remoteClient_captured,
                ignoreClient_captured,
                text_captured,
                number_captured,
                number2_captured,
                number3_captured,
                number4_captured,
                number5_captured,
                number6_captured,
                number7_captured
            ),
            this,
            whoAmI,
            msgType,
            remoteClient,
            ignoreClient,
            text,
            number,
            number2,
            number3,
            number4,
            number5,
            number6,
            number7
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PreWorldGen_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PreWorldGen.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PreWorldGen_Impl(ModSystemHooks.PreWorldGen.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PreWorldGen()
    {
        hook(
            () => base.PreWorldGen(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifyWorldGenTasks_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyWorldGenTasks.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyWorldGenTasks_Impl(ModSystemHooks.ModifyWorldGenTasks.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyWorldGenTasks(
        System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> tasks,
        ref double totalWeight
    )
    {
        hook(
            (
                System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> tasks_captured,
                ref double totalWeight_captured
            ) => base.ModifyWorldGenTasks(
                tasks_captured,
                ref totalWeight_captured
            ),
            this,
            tasks,
            ref totalWeight
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_PostWorldGen_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.PostWorldGen.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_PostWorldGen_Impl(ModSystemHooks.PostWorldGen.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostWorldGen()
    {
        hook(
            () => base.PostWorldGen(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ResetNearbyTileEffects_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ResetNearbyTileEffects.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ResetNearbyTileEffects_Impl(ModSystemHooks.ResetNearbyTileEffects.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ResetNearbyTileEffects()
    {
        hook(
            () => base.ResetNearbyTileEffects(),
            this
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifyHardmodeTasks_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyHardmodeTasks.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyHardmodeTasks_Impl(ModSystemHooks.ModifyHardmodeTasks.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyHardmodeTasks(
        System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> list
    )
    {
        hook(
            (
                System.Collections.Generic.List<Terraria.WorldBuilding.GenPass> list_captured
            ) => base.ModifyHardmodeTasks(
                list_captured
            ),
            this,
            list
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifySunLightColor_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifySunLightColor.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifySunLightColor_Impl(ModSystemHooks.ModifySunLightColor.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifySunLightColor(
        ref Microsoft.Xna.Framework.Color tileColor,
        ref Microsoft.Xna.Framework.Color backgroundColor
    )
    {
        hook(
            (
                ref Microsoft.Xna.Framework.Color tileColor_captured,
                ref Microsoft.Xna.Framework.Color backgroundColor_captured
            ) => base.ModifySunLightColor(
                ref tileColor_captured,
                ref backgroundColor_captured
            ),
            this,
            ref tileColor,
            ref backgroundColor
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ModifyLightingBrightness_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ModifyLightingBrightness.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ModifyLightingBrightness_Impl(ModSystemHooks.ModifyLightingBrightness.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyLightingBrightness(
        ref float scale
    )
    {
        hook(
            (
                ref float scale_captured
            ) => base.ModifyLightingBrightness(
                ref scale_captured
            ),
            this,
            ref scale
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_TileCountsAvailable_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.TileCountsAvailable.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_TileCountsAvailable_Impl(ModSystemHooks.TileCountsAvailable.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void TileCountsAvailable(
        System.ReadOnlySpan<int> tileCounts
    )
    {
        hook(
            (
                System.ReadOnlySpan<int> tileCounts_captured
            ) => base.TileCountsAvailable(
                tileCounts_captured
            ),
            this,
            tileCounts
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class ModSystem_ResizeArrays_Impl() : Terraria.ModLoader.ModSystem
{
    [Terraria.ModLoader.CloneByReference]
    private string namePrefix = string.Empty;

    [field: Terraria.ModLoader.CloneByReference]
    private ModSystemHooks.ResizeArrays.Definition hook;

    public override string Name => base.Name + '_' + namePrefix;

    public ModSystem_ResizeArrays_Impl(ModSystemHooks.ResizeArrays.Definition hook) : this()
    {
        this.hook = hook;
        namePrefix = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ResizeArrays()
    {
        hook(
            () => base.ResizeArrays(),
            this
        );
    }
}
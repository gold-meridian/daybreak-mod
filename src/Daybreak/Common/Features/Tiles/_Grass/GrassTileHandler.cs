using Daybreak.Common.Features.Hooks;

using Terraria.ModLoader;

namespace Daybreak.Common.Features.Tiles;

internal sealed class GrassTileHandler : GlobalTile
{
    [OnLoad]
    public static void ModifyVanilla()
    {
        /*IL_DelegateMethods.SpreadDirt += SpreadDirt_CountTilesConvertibleToDirtOnHit;
        IL_SmartCursorHelper.Step_AlchemySeeds += PermitHerbPlacementSmartCursor;
        IL_SmartCursorHelper.Step_PumpkinSeeds += PermitPumpkinSeedPlacementSmartCursor;
        IL_Liquid.DelWater += DelWater_KillGrass;
        IL_Player.PlaceThing_Tiles_PlaceIt_KillGrassForSolids += KillGrassForSolids;
        IL_Player.PlaceThing_Tiles_BlockPlacementForAssortedThings += MightBeForPlacingSeeds;
        On_Player.ItemCheck_IsValidDirtRodTarget += MakeValidDirtRodTarget;
        IL_Player.DoesPickTargetTransformOnKill += PickTransformsGrassOnKill;
        IL_WorldGen.PlaceAlch += PermitHerbPlacement;
        IL_WorldGen.PlantAlch += TODO;
        IL_WorldGen.CheckAlch += PermitContinuedHerbExistence;
        IL_WorldGen.Place2x2Style += PermitPumpkinSeedPlacement;
        IL_WorldGen.Check2x2Style += PermitPumpkinCheck;
        IL_WorldGen.PlaceSunflower += PermitSunflowerPlacement;
        IL_WorldGen.PlaceTile += HandlePlacementOfWeeds;
        // TODO: Support lily pad logic (PlaceLilyPad, CheckLilyPad)?
        // TODO: Special logic in PlaceTile? Covered by
        // ResetsHalfBrickPlacementAttempt?
        IL_WorldGen.KillTile += ActuallyTurnGrassToDirtOnHit;
        // IL_WorldGen.hardUpdateWorld += EnableSpreadInHardMode; // TODO: correct?
        // TODO: plantDye?
        // TODO: UpdateWorld_OvergroundTile
        // TODO: UpdateWorld_UndergroundTile
        IL_WorldGen.UpdateWorld_GrassGrowth += EnableGrassGrowth;
        // TODO: WorldGen.PlantCheck

        IL_NPC.SpawnNPC += SpawnCrittersOnGrassThatAllowsIs;

        On_TreePaintSystemData.GetTileSettings += UseDirtSettingsForGrassPaintingSettings;
        On_Player.DoBootsEffect_PlaceFlowersOnTile += PlaceGrassPlantsFromFlowerBoots;
        On_WorldGen.IsFitToPlaceFlowerIn += MakeFitToPlaceFlowerIn;*/
    }
}
namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalPylon':
//     System.Boolean Terraria.ModLoader.GlobalPylon::PreDrawMapIcon(Terraria.Map.MapOverlayDrawContext&,System.String&,Terraria.GameContent.TeleportPylonInfo&,System.Boolean&,Microsoft.Xna.Framework.Color&,System.Single&,System.Single&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalPylon::PreCanPlacePylon(System.Int32,System.Int32,System.Int32,Terraria.GameContent.TeleportPylonType)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalPylon::ValidTeleportCheck_PreNPCCount(Terraria.GameContent.TeleportPylonInfo,System.Int32&)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalPylon::ValidTeleportCheck_PreAnyDanger(Terraria.GameContent.TeleportPylonInfo)
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalPylon::ValidTeleportCheck_PreBiomeRequirements(Terraria.GameContent.TeleportPylonInfo,Terraria.SceneMetrics)
//     System.Void Terraria.ModLoader.GlobalPylon::PostValidTeleportCheck(Terraria.GameContent.TeleportPylonInfo,Terraria.GameContent.TeleportPylonInfo,System.Boolean&,System.Boolean&,System.String&)
public static partial class GlobalPylonHooks
{
    public sealed partial class PreDrawMapIcon
    {
        public delegate bool Original(
            ref Terraria.Map.MapOverlayDrawContext context,
            ref string mouseOverText,
            ref Terraria.GameContent.TeleportPylonInfo pylonInfo,
            ref bool isNearPylon,
            ref Microsoft.Xna.Framework.Color drawColor,
            ref float deselectedScale,
            ref float selectedScale
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalPylon self,
            ref Terraria.Map.MapOverlayDrawContext context,
            ref string mouseOverText,
            ref Terraria.GameContent.TeleportPylonInfo pylonInfo,
            ref bool isNearPylon,
            ref Microsoft.Xna.Framework.Color drawColor,
            ref float deselectedScale,
            ref float selectedScale
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalPylon_PreDrawMapIcon_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalPylon::PreDrawMapIcon")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalPylon::PreDrawMapIcon; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreCanPlacePylon
    {
        public delegate bool? Original(
            int x,
            int y,
            int tileType,
            Terraria.GameContent.TeleportPylonType pylonType
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalPylon self,
            int x,
            int y,
            int tileType,
            Terraria.GameContent.TeleportPylonType pylonType
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalPylon_PreCanPlacePylon_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalPylon::PreCanPlacePylon")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalPylon::PreCanPlacePylon; use a flag to disable behavior.");
        }
    }

    public sealed partial class ValidTeleportCheck_PreNPCCount
    {
        public delegate bool? Original(
            Terraria.GameContent.TeleportPylonInfo pylonInfo,
            ref int defaultNecessaryNPCCount
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalPylon self,
            Terraria.GameContent.TeleportPylonInfo pylonInfo,
            ref int defaultNecessaryNPCCount
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalPylon_ValidTeleportCheck_PreNPCCount_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalPylon::ValidTeleportCheck_PreNPCCount")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalPylon::ValidTeleportCheck_PreNPCCount; use a flag to disable behavior.");
        }
    }

    public sealed partial class ValidTeleportCheck_PreAnyDanger
    {
        public delegate bool? Original(
            Terraria.GameContent.TeleportPylonInfo pylonInfo
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalPylon self,
            Terraria.GameContent.TeleportPylonInfo pylonInfo
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalPylon_ValidTeleportCheck_PreAnyDanger_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalPylon::ValidTeleportCheck_PreAnyDanger")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalPylon::ValidTeleportCheck_PreAnyDanger; use a flag to disable behavior.");
        }
    }

    public sealed partial class ValidTeleportCheck_PreBiomeRequirements
    {
        public delegate bool? Original(
            Terraria.GameContent.TeleportPylonInfo pylonInfo,
            Terraria.SceneMetrics sceneData
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalPylon self,
            Terraria.GameContent.TeleportPylonInfo pylonInfo,
            Terraria.SceneMetrics sceneData
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalPylon_ValidTeleportCheck_PreBiomeRequirements_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalPylon::ValidTeleportCheck_PreBiomeRequirements")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalPylon::ValidTeleportCheck_PreBiomeRequirements; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostValidTeleportCheck
    {
        public delegate void Original(
            Terraria.GameContent.TeleportPylonInfo destinationPylonInfo,
            Terraria.GameContent.TeleportPylonInfo nearbyPylonInfo,
            ref bool destinationPylonValid,
            ref bool validNearbyPylonFound,
            ref string errorKey
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalPylon self,
            Terraria.GameContent.TeleportPylonInfo destinationPylonInfo,
            Terraria.GameContent.TeleportPylonInfo nearbyPylonInfo,
            ref bool destinationPylonValid,
            ref bool validNearbyPylonFound,
            ref string errorKey
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalPylon_PostValidTeleportCheck_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalPylon::PostValidTeleportCheck")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalPylon::PostValidTeleportCheck; use a flag to disable behavior.");
        }
    }
}

public sealed partial class GlobalPylon_PreDrawMapIcon_Impl(GlobalPylonHooks.PreDrawMapIcon.Definition hook) : Terraria.ModLoader.GlobalPylon
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool PreDrawMapIcon(
        ref Terraria.Map.MapOverlayDrawContext context,
        ref string mouseOverText,
        ref Terraria.GameContent.TeleportPylonInfo pylonInfo,
        ref bool isNearPylon,
        ref Microsoft.Xna.Framework.Color drawColor,
        ref float deselectedScale,
        ref float selectedScale
    )
    {
        return hook(
            (
                ref Terraria.Map.MapOverlayDrawContext context_captured,
                ref string mouseOverText_captured,
                ref Terraria.GameContent.TeleportPylonInfo pylonInfo_captured,
                ref bool isNearPylon_captured,
                ref Microsoft.Xna.Framework.Color drawColor_captured,
                ref float deselectedScale_captured,
                ref float selectedScale_captured
            ) => base.PreDrawMapIcon(
                ref context_captured,
                ref mouseOverText_captured,
                ref pylonInfo_captured,
                ref isNearPylon_captured,
                ref drawColor_captured,
                ref deselectedScale_captured,
                ref selectedScale_captured
            ),
            this,
            ref context,
            ref mouseOverText,
            ref pylonInfo,
            ref isNearPylon,
            ref drawColor,
            ref deselectedScale,
            ref selectedScale
        );
    }
}

public sealed partial class GlobalPylon_PreCanPlacePylon_Impl(GlobalPylonHooks.PreCanPlacePylon.Definition hook) : Terraria.ModLoader.GlobalPylon
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool? PreCanPlacePylon(
        int x,
        int y,
        int tileType,
        Terraria.GameContent.TeleportPylonType pylonType
    )
    {
        return hook(
            (
                int x_captured,
                int y_captured,
                int tileType_captured,
                Terraria.GameContent.TeleportPylonType pylonType_captured
            ) => base.PreCanPlacePylon(
                x_captured,
                y_captured,
                tileType_captured,
                pylonType_captured
            ),
            this,
            x,
            y,
            tileType,
            pylonType
        );
    }
}

public sealed partial class GlobalPylon_ValidTeleportCheck_PreNPCCount_Impl(GlobalPylonHooks.ValidTeleportCheck_PreNPCCount.Definition hook) : Terraria.ModLoader.GlobalPylon
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool? ValidTeleportCheck_PreNPCCount(
        Terraria.GameContent.TeleportPylonInfo pylonInfo,
        ref int defaultNecessaryNPCCount
    )
    {
        return hook(
            (
                Terraria.GameContent.TeleportPylonInfo pylonInfo_captured,
                ref int defaultNecessaryNPCCount_captured
            ) => base.ValidTeleportCheck_PreNPCCount(
                pylonInfo_captured,
                ref defaultNecessaryNPCCount_captured
            ),
            this,
            pylonInfo,
            ref defaultNecessaryNPCCount
        );
    }
}

public sealed partial class GlobalPylon_ValidTeleportCheck_PreAnyDanger_Impl(GlobalPylonHooks.ValidTeleportCheck_PreAnyDanger.Definition hook) : Terraria.ModLoader.GlobalPylon
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool? ValidTeleportCheck_PreAnyDanger(
        Terraria.GameContent.TeleportPylonInfo pylonInfo
    )
    {
        return hook(
            (
                Terraria.GameContent.TeleportPylonInfo pylonInfo_captured
            ) => base.ValidTeleportCheck_PreAnyDanger(
                pylonInfo_captured
            ),
            this,
            pylonInfo
        );
    }
}

public sealed partial class GlobalPylon_ValidTeleportCheck_PreBiomeRequirements_Impl(GlobalPylonHooks.ValidTeleportCheck_PreBiomeRequirements.Definition hook) : Terraria.ModLoader.GlobalPylon
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool? ValidTeleportCheck_PreBiomeRequirements(
        Terraria.GameContent.TeleportPylonInfo pylonInfo,
        Terraria.SceneMetrics sceneData
    )
    {
        return hook(
            (
                Terraria.GameContent.TeleportPylonInfo pylonInfo_captured,
                Terraria.SceneMetrics sceneData_captured
            ) => base.ValidTeleportCheck_PreBiomeRequirements(
                pylonInfo_captured,
                sceneData_captured
            ),
            this,
            pylonInfo,
            sceneData
        );
    }
}

public sealed partial class GlobalPylon_PostValidTeleportCheck_Impl(GlobalPylonHooks.PostValidTeleportCheck.Definition hook) : Terraria.ModLoader.GlobalPylon
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override void PostValidTeleportCheck(
        Terraria.GameContent.TeleportPylonInfo destinationPylonInfo,
        Terraria.GameContent.TeleportPylonInfo nearbyPylonInfo,
        ref bool destinationPylonValid,
        ref bool validNearbyPylonFound,
        ref string errorKey
    )
    {
        hook(
            (
                Terraria.GameContent.TeleportPylonInfo destinationPylonInfo_captured,
                Terraria.GameContent.TeleportPylonInfo nearbyPylonInfo_captured,
                ref bool destinationPylonValid_captured,
                ref bool validNearbyPylonFound_captured,
                ref string errorKey_captured
            ) => base.PostValidTeleportCheck(
                destinationPylonInfo_captured,
                nearbyPylonInfo_captured,
                ref destinationPylonValid_captured,
                ref validNearbyPylonFound_captured,
                ref errorKey_captured
            ),
            this,
            destinationPylonInfo,
            nearbyPylonInfo,
            ref destinationPylonValid,
            ref validNearbyPylonFound,
            ref errorKey
        );
    }
}
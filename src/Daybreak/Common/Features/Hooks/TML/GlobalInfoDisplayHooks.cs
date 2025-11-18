namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalInfoDisplay':
//     System.Nullable`1<System.Boolean> Terraria.ModLoader.GlobalInfoDisplay::Active(Terraria.ModLoader.InfoDisplay)
//     System.Void Terraria.ModLoader.GlobalInfoDisplay::ModifyDisplayParameters(Terraria.ModLoader.InfoDisplay,System.String&,System.String&,Microsoft.Xna.Framework.Color&,Microsoft.Xna.Framework.Color&)
public static partial class GlobalInfoDisplayHooks
{
    public sealed partial class Active
    {
        public delegate bool? Original(
            Terraria.ModLoader.InfoDisplay currentDisplay
        );

        public delegate bool? Definition(
            Original orig,
            Terraria.ModLoader.GlobalInfoDisplay self,
            Terraria.ModLoader.InfoDisplay currentDisplay
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalInfoDisplay_Active_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalInfoDisplay::Active")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalInfoDisplay::Active; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyDisplayParameters
    {
        public delegate void Original(
            Terraria.ModLoader.InfoDisplay currentDisplay,
            ref string displayValue,
            ref string displayName,
            ref Microsoft.Xna.Framework.Color displayColor,
            ref Microsoft.Xna.Framework.Color displayShadowColor
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalInfoDisplay self,
            Terraria.ModLoader.InfoDisplay currentDisplay,
            ref string displayValue,
            ref string displayName,
            ref Microsoft.Xna.Framework.Color displayColor,
            ref Microsoft.Xna.Framework.Color displayShadowColor
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalInfoDisplay_ModifyDisplayParameters_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalInfoDisplay::ModifyDisplayParameters")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalInfoDisplay::ModifyDisplayParameters; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalInfoDisplay_Active_Impl : Terraria.ModLoader.GlobalInfoDisplay
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalInfoDisplayHooks.Active.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalInfoDisplay_Active_Impl(GlobalInfoDisplayHooks.Active.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool? Active(
        Terraria.ModLoader.InfoDisplay currentDisplay
    )
    {
        return hook(
            (
                Terraria.ModLoader.InfoDisplay currentDisplay_captured
            ) => base.Active(
                currentDisplay_captured
            ),
            this,
            currentDisplay
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalInfoDisplay_ModifyDisplayParameters_Impl : Terraria.ModLoader.GlobalInfoDisplay
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalInfoDisplayHooks.ModifyDisplayParameters.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalInfoDisplay_ModifyDisplayParameters_Impl(GlobalInfoDisplayHooks.ModifyDisplayParameters.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyDisplayParameters(
        Terraria.ModLoader.InfoDisplay currentDisplay,
        ref string displayValue,
        ref string displayName,
        ref Microsoft.Xna.Framework.Color displayColor,
        ref Microsoft.Xna.Framework.Color displayShadowColor
    )
    {
        hook(
            (
                Terraria.ModLoader.InfoDisplay currentDisplay_captured,
                ref string displayValue_captured,
                ref string displayName_captured,
                ref Microsoft.Xna.Framework.Color displayColor_captured,
                ref Microsoft.Xna.Framework.Color displayShadowColor_captured
            ) => base.ModifyDisplayParameters(
                currentDisplay_captured,
                ref displayValue_captured,
                ref displayName_captured,
                ref displayColor_captured,
                ref displayShadowColor_captured
            ),
            this,
            currentDisplay,
            ref displayValue,
            ref displayName,
            ref displayColor,
            ref displayShadowColor
        );
    }
}
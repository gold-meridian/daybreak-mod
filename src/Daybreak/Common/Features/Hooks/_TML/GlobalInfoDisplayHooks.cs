namespace Daybreak.Common.Features.Hooks;

using System.Linq;

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
        public delegate bool? Definition(
            Terraria.ModLoader.GlobalInfoDisplay self,
            Terraria.ModLoader.InfoDisplay currentDisplay
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool? Invoke(
            Terraria.ModLoader.GlobalInfoDisplay self,
            Terraria.ModLoader.InfoDisplay currentDisplay
        )
        {
            var result = default(bool?);
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                var newValue = handler.Invoke(self, currentDisplay);
                if (newValue.HasValue)
                {
                    result &= newValue;
                }
            }

            return result;
        }
    }

    public sealed partial class ModifyDisplayParameters
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalInfoDisplay self,
            Terraria.ModLoader.InfoDisplay currentDisplay,
            ref string displayValue,
            ref string displayName,
            ref Microsoft.Xna.Framework.Color displayColor,
            ref Microsoft.Xna.Framework.Color displayShadowColor
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalInfoDisplay self,
            Terraria.ModLoader.InfoDisplay currentDisplay,
            ref string displayValue,
            ref string displayName,
            ref Microsoft.Xna.Framework.Color displayColor,
            ref Microsoft.Xna.Framework.Color displayShadowColor
        )
        {
            Event?.Invoke(self, currentDisplay, ref displayValue, ref displayName, ref displayColor, ref displayShadowColor);
        }
    }
}

public sealed partial class GlobalInfoDisplayImpl : Terraria.ModLoader.GlobalInfoDisplay
{
    public override bool? Active(
        Terraria.ModLoader.InfoDisplay currentDisplay
    )
    {
        if (!GlobalInfoDisplayHooks.Active.GetInvocationList().Any())
        {
            return base.Active(
                currentDisplay
            );
        }

        return GlobalInfoDisplayHooks.Active.Invoke(
            this,
            currentDisplay
        );
    }

    public override void ModifyDisplayParameters(
        Terraria.ModLoader.InfoDisplay currentDisplay,
        ref string displayValue,
        ref string displayName,
        ref Microsoft.Xna.Framework.Color displayColor,
        ref Microsoft.Xna.Framework.Color displayShadowColor
    )
    {
        if (!GlobalInfoDisplayHooks.ModifyDisplayParameters.GetInvocationList().Any())
        {
            base.ModifyDisplayParameters(
                currentDisplay,
                ref displayValue,
                ref displayName,
                ref displayColor,
                ref displayShadowColor
            );
            return;
        }

        GlobalInfoDisplayHooks.ModifyDisplayParameters.Invoke(
            this,
            currentDisplay,
            ref displayValue,
            ref displayName,
            ref displayColor,
            ref displayShadowColor
        );
    }
}

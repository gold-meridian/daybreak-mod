using System.Diagnostics;
using System.Reflection;
using Daybreak.Common.Features.Hooks;
using Mono.Cecil;
using MonoMod.Cil;
using Terraria.ModLoader;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;

namespace Daybreak.Common.Features.ModPanel;

/// <summary>
///     When implemented by a <see cref="Mod" />, allows the mod to modify their
///     Mod Configuration menu button.
/// </summary>
public interface IHasCustomModConfigButton
{
    /// <summary>
    ///     Creates the <see cref="UIButton{T}" /> for the mod entry.
    ///     <br />
    ///     This may be either the button for when mods are available, or for
    ///     when they aren't.
    /// </summary>
    /// <param name="button">The incoming button.</param>
    /// <returns>The button to use.</returns>
    UIButton<string> CreateModConfigButton(UIButton<string> button);

    [OnLoad]
    private static void Hook()
    {
        MonoModHooks.Modify(
            typeof(UIModConfigList).GetMethod(nameof(UIModConfigList.PopulateMods), BindingFlags.NonPublic | BindingFlags.Instance),
            PopulateMods_HookUiButtonCreation
        );
    }

    private static void PopulateMods_HookUiButtonCreation(ILContext il)
    {
        var c = new ILCursor(il);

        var modLoc = -1;
        var ldMod = default(FieldReference);
        c.GotoNext(MoveType.After, x => x.MatchNewobj<UIButton<string>>());
        c.GotoPrev(x => x.MatchLdloc(out modLoc));
        c.GotoNext(x => x.MatchLdfld(out ldMod));
        Debug.Assert(modLoc != -1);
        Debug.Assert(ldMod is not null);

        c.Index = 0;

        while (c.TryGotoNext(MoveType.After, x => x.MatchNewobj<UIButton<string>>()))
        {
            c.EmitLdloc(modLoc);
            c.EmitLdfld(ldMod);
            c.EmitDelegate(
                (UIButton<string> button, Mod mod) =>
                {
                    if (mod is IHasCustomModConfigButton customButton)
                    {
                        return customButton.CreateModConfigButton(button);
                    }

                    return button;
                }
            );
        }
    }
}

using System.Collections.Generic;

namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     Handles rendering the user interface with various effects.
/// </summary>
public static class UserInterfaceModifier
{
    /// <summary>
    ///     All active modifiers to apply to the interface.
    /// </summary>
    public static IEnumerable<IUserInterfaceModifier> Modifiers => modifiers;

    /// <summary>
    ///     Whether the user interface should be captured this frame to apply
    ///     rendering modifiers.
    /// </summary>
    public static bool ShouldCaptureThisFrame => modifiers.Count > 0;

    private static readonly List<IUserInterfaceModifier> modifiers = [];

    /// <summary>
    ///     Adds a new user interface modifier to apply.
    /// </summary>
    /// <param name="modifier">The user interface modifier.</param>
    public static void Add(IUserInterfaceModifier modifier)
    {
        modifiers.Add(modifier);
    }

    /// <summary>
    ///     Applies the currently active modifiers to <paramref name="uiInfo"/>.
    /// </summary>
    /// <param name="uiInfo">The UI rendering info.</param>
    public static void ApplyTo(ref UserInterfaceInfo uiInfo)
    {
        ClearFinishedModifiers();

        foreach (var modifier in Modifiers)
        {
            modifier.Update(ref uiInfo);
        }
    }

    private static void ClearFinishedModifiers()
    {
        for (var i = modifiers.Count - 1; i >= 0; i--)
        {
            if (modifiers[i].Finished)
            {
                modifiers.RemoveAt(i);
            }
        }
    }

#region Common modifiers
    /// <summary>
    ///     A basic fade modifier for the UI which may stack with other fades.
    /// </summary>
    /// <param name="lengthInTicks">
    ///     The length to fade out for, in ticks.  If <c>0</c>, hides it instantly.
    /// </param>
    /// <remarks>
    ///     Be sure to call <see cref="HideUserInterfaceModifier.Show"/> to
    ///     reveal the UI after.
    /// </remarks>
    public static HideUserInterfaceModifier Hide(int lengthInTicks)
    {
        return new HideUserInterfaceModifier(lengthInTicks);
    }
#endregion
}

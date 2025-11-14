using System;
using System.Collections.Generic;
using System.Linq;

namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     Handles rendering the user interface with various effects.
/// </summary>
public static class UserInterfaceModifier
{
    /// <summary>
    ///     All active modifiers to apply to the interface.
    /// </summary>
    public static IEnumerable<IUserInterfaceModifier> Modifiers => singleton_modifiers.Where(x => !x.Finished).Concat(modifiers);

    /// <summary>
    ///     Whether the user interface should be captured this frame to apply
    ///     rendering modifiers.
    /// </summary>
    public static bool ShouldCaptureThisFrame => Modifiers.Any();

    private static readonly List<IUserInterfaceModifier> singleton_modifiers =
    [
        new HideUserInterfaceModifier(),
    ];

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
    ///     Attempts to hide the user interface.  This function is special and
    ///     should be called every frame with the desired delta per frame.  The
    ///     greatest delta will be used.
    ///     <br />
    ///     If this function does not get called for a frame, it will attempt to
    ///     start showing the UI again, completing in half a second (30 ticks)
    ///     if it was completely hidden.
    /// </summary>
    /// <param name="deltaAlpha">The amount to subtract.</param>
    public static void Hide(float deltaAlpha)
    {
        HideUserInterfaceModifier.DeltaAlpha = MathF.Max(HideUserInterfaceModifier.DeltaAlpha, deltaAlpha);
    }
#endregion
}

namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     A modifier applied to render target containing the user interface.
/// </summary>
public interface IUserInterfaceModifier
{
    /// <summary>
    ///     Whether this modifier is finished applying and may be removed.
    /// </summary>
    bool Finished { get; }

    /// <summary>
    ///     Updates this modifier's state and the UI rendering info for the
    ///     frame.
    /// </summary>
    /// <param name="uiInfo">The UI rendering info.</param>
    void Update(ref UserInterfaceInfo uiInfo);
}

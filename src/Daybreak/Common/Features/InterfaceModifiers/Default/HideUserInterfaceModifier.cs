namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     A basic fade modifier for the UI which may stack with other fades.
/// </summary>
/// <param name="lengthInTicks">
///     The length to fade out for, in ticks.  If <c>0</c>, hides it instantly.
/// </param>
public sealed class HideUserInterfaceModifier(int lengthInTicks = 0) : IUserInterfaceModifier
{
    /// <inheritdoc />
    public bool Finished => fadeIn && Progress >= 1f;

    private int fadeLength = lengthInTicks;
    private int fadeTicks;
    private bool fadeIn;

    private float Progress =>
        fadeLength == 0 ? 1f : fadeTicks / (float)fadeLength;

    /// <inheritdoc />
    public void Update(ref UserInterfaceInfo uiInfo)
    {
        fadeTicks++;

        if (fadeIn)
        {
            uiInfo.Color *= 1f - Progress;
        }
        else
        {
            uiInfo.Color *= Progress;
        }
    }

    /// <summary>
    ///     Shows the UI again.
    /// </summary>
    /// <param name="lengthInTicks">
    ///     The length to fade in for, in ticks.  If <c>-1</c>, uses the same
    ///     length it took to fade out,  If <c>0</c>, shows it instantly.
    /// </param>
    public void Show(int lengthInTicks = -1)
    {
        if (lengthInTicks > -1)
        {
            fadeLength = lengthInTicks;
        }

        fadeTicks = 0;
        fadeIn = true;
    }
}

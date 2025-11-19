namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     A special modifier which can fade the UI in and out.
/// </summary>
internal sealed class HideUserInterfaceModifier : IUserInterfaceModifier
{
    public bool Finished => DeltaAlpha <= 0f && alpha >= 1f;

    public static float DeltaAlpha { get; set; }

    private static float alpha = 1f;

    private const float show_rate = 1f / 30f;

    public void Update(ref UserInterfaceInfo uiInfo)
    {
        // If there's a delta, subtract it, otherwise start trying to show the
        // UI.  Always set to zero after since Hide should be getting called
        // every frame.
        if (DeltaAlpha > 0f)
        {
            alpha -= DeltaAlpha;
        }
        else
        {
            alpha += show_rate;
        }

        if (alpha <= 0f)
        {
            alpha = 0f;
        }

        DeltaAlpha = 0f;

        uiInfo.Color *= alpha;
    }
}

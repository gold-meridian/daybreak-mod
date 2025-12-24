using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     Settings for a <see cref="INoise2d{TSelf,TSettings}"/> function.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
public interface INoise2dSettings<out TSelf>
{
    /// <summary>
    ///     The seed to use when sampling.
    /// </summary>
    int Seed { get; set; }

    /// <summary>
    ///     Initializes an instance of the settings with its default
    ///     configuration.
    /// </summary>
    static abstract TSelf DefaultSettings();
}

/// <summary>
///     A 2-dimensional noise function.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
/// <typeparam name="TSettings">The settings that may configure a sample.</typeparam>
public interface INoise2d<TSelf, in TSettings>
    where TSelf : INoise2d<TSelf, TSettings>
    where TSettings : INoise2dSettings<TSettings>
{
    /// <inheritdoc cref="Sample(Vector2, TSettings)"/>
    static virtual float Sample(Vector2 p)
    {
        return TSelf.Sample(p, TSettings.DefaultSettings());
    }

    /// <summary>
    ///     Samples the noise function at the point <paramref name="p"/>,
    ///     returning a scalar value.
    ///     <br />
    ///     This is a statically-provided 
    /// </summary>
    /// <param name="p">The point to sample at.</param>
    /// <param name="settings">The settings to use for this sample.</param>
    /// <returns>The scalar value resulting from the sample.</returns>
    static abstract float Sample(
        Vector2 p,
        TSettings settings
    );
}

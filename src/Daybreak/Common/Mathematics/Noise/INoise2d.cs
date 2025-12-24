using Microsoft.Xna.Framework;

namespace Daybreak.Common.Mathematics;

/// <summary>
///     A 2-dimensional noise function.  Instances denote settings for the
///     sampler.
/// </summary>
/// <typeparam name="TSelf">The type that implements this interface.</typeparam>
public interface INoise2d<TSelf>
    where TSelf : unmanaged, INoise2d<TSelf>
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

    /// <inheritdoc cref="Sample(Vector2, TSelf)"/>
    static virtual float Sample(Vector2 p)
    {
        return TSelf.Sample(p, TSelf.DefaultSettings());
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
        TSelf settings
    );
}

using Terraria.DataStructures;

namespace Daybreak.Common.Rendering;

/// <summary>
///     A type-safe wrapper over a packed player shader integer.
/// </summary>
/// <param name="LocalIndex">The local index of the shader.</param>
/// <param name="ShaderType">The shader kind.</param>
public readonly record struct PlayerShader(
    int LocalIndex,
    PlayerDrawHelper.ShaderConfiguration ShaderType
)
{
    /// <summary>
    ///     The packed value.
    /// </summary>
    public int PackedValue => LocalIndex + (int)ShaderType * 1000;

    /// <summary>
    ///     Creates a new wrapper over a packed value.
    /// </summary>
    /// <param name="packed">The packed value.</param>
    /// <returns>The desconstructed wrapper.</returns>
    public static PlayerShader FromPacked(int packed)
    {
        return new PlayerShader(
            packed % 1000,
            (PlayerDrawHelper.ShaderConfiguration)(packed / 1000)
        );
    }
}

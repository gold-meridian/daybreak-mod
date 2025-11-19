using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     Determines how to render the UI render target for a frame.
/// </summary>
/// <param name="OriginalPosition">
///     The original position of the UI buffer.  The origin is always
///     <c>(0,0)</c> (the top-left), so this value is generally always
///     <see cref="Vector2.Zero"/>.
/// </param>
/// <param name="OriginalTexture">
///     The buffer containing the original UI render.
/// </param>
public record struct UserInterfaceInfo(
    Vector2 OriginalPosition,
    Texture2D OriginalTexture
)
{
    /// <summary>
    ///     The position to render the UI at.
    /// </summary>
    public Vector2 Position { get; set; } = OriginalPosition;

    /// <summary>
    ///     The current texture to render as the UI.
    /// </summary>
    public Texture2D Texture { get; set; } = OriginalTexture;

    /// <summary>
    ///     The center of the image to be rendered, with the
    ///     <see cref="Position"/> offset.
    /// </summary>
    public Vector2 Center => Texture.Size() / 2f + Position;

    /// <summary>
    ///     The original center of the original image to be rendered.
    /// </summary>
    public Vector2 OriginalCenter => OriginalTexture.Size() / 2f + OriginalPosition;

    /// <summary>
    ///     Whether the inventory button should open the settings menu instead
    ///     of the inventory.  Useful for cinematic modifiers such as fades
    ///     which need to give the player a degree of control still.
    /// </summary>
    public bool InventoryButtonOpensSettings { get; set; }

    /// <summary>
    ///     The color to render the texture with.
    /// </summary>
    public Color Color { get; set; } = Color.White;

    /// <summary>
    ///     The rotation to render the texture with.
    /// </summary>
    public float Rotation { get; set; } = 0f;

    /// <summary>
    ///     The scale to render the texture with.
    /// </summary>
    public Vector2 Scale { get; set; } = Vector2.One;

    /// <summary>
    ///     The sprite effects to render the texture with.
    /// </summary>
    public SpriteEffects SpriteEffects { get; set; } = SpriteEffects.None;
}

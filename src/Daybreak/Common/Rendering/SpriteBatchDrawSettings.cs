using System;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Rendering;

/// <summary>
///     A set of parameters for a <see cref="SpriteBatch"/> drawing call.
/// </summary>
public struct DrawSettings()
{
    /// <summary>
    ///     The <see cref="Texture2D"/> for this draw.
    /// </summary>
    public required Texture2D Texture;
    
    /// <summary>
    ///     The position for this draw.
    /// </summary>
    public Vector2 Position;
    
    /// <summary>
    ///     The source rectangle for this draw.
    /// </summary>
    public Rectangle? SourceRectangle = null;
    
    /// <summary>
    ///     The color for this draw.
    /// </summary>
    public Color Color = Color.White;
    
    /// <summary>
    ///     The rotation for this draw.
    /// </summary>
    public float Rotation = 0f;
    
    /// <summary>
    ///     The origin for this draw.
    /// </summary>
    public Vector2 Origin = Vector2.Zero;
    
    /// <summary>
    ///     The texture for this draw.
    /// </summary>
    public Vector2 Scale = Vector2.One;
    
    /// <summary>
    ///     The <see cref="SpriteEffects"/> for this draw.
    /// </summary>
    public SpriteEffects Effects = SpriteEffects.None;
    
    /// <summary>
    ///     The layer depth for this draw.
    /// </summary>
    public float LayerDepth = 0f;
}

/// <summary>
///     Extensions to <see cref="SpriteBatch" /> using
///     <see cref="DrawSettings" /> instances.
/// </summary>
public static class SpriteBatchDrawSettingsExtensions
{
    /// <param name="sb">The <see cref="SpriteBatch" />.</param>
    extension(SpriteBatch sb)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        public void Draw(DrawSettings settings)
        {
            sb.Draw(
                settings.Texture,
                settings.Position,
                settings.SourceRectangle,
                settings.Color,
                settings.Rotation,
                settings.Origin,
                settings.Scale,
                settings.Effects,
                settings.LayerDepth
            );
        }
    }
}
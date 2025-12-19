using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Common.Rendering;

/// <summary>
///     A set of parameters for a <see cref="SpriteBatch"/> drawing call.
/// </summary>
public struct DrawSettings()
{
    /// <summary>
    ///     The <see cref="Texture2D"/> for this draw.
    /// </summary>
    public Texture2D Texture;
    
    /// <summary>
    ///     The position for this draw.
    /// </summary>
    public Vector2 Position;
    
    /// <summary>
    ///     The source rectangle for this draw.
    /// </summary>
    public Rectangle? SourceRectangle = null;
    
    /// <summary>
    ///     Gets / sets the absolute dimensions of this draw.
    ///     Updating this automatically derives the canonical scale.
    /// </summary>
    public Vector2 Size
    {
        get
        {
            float sw = SourceRectangle?.Width ?? Texture.Width;
            float sh = SourceRectangle?.Height ?? Texture.Height;
            return new Vector2(sw * Scale.X, sh * Scale.Y);
        }
        set
        {
            float sw = SourceRectangle?.Width ?? Texture.Width;
            float sh = SourceRectangle?.Height ?? Texture.Height;
            Scale = new Vector2(value.X / sw, value.Y / sh);
        }
    }
    
    /// <summary>
    ///     The destination rectangle for this draw.
    /// </summary>
    public Rectangle Destination
    {
        get => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
        set
        {
            Position = new Vector2(value.X, value.Y);
            Size = new Vector2(value.Width, value.Height);
        }
    }
    
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
    
    public DrawSettings(Texture2D texture) : this()
    {
        Texture = texture;
        SourceRectangle = null;
        Position = Vector2.Zero;
        Scale = Vector2.One;
        Color = Color.White;
        Rotation = 0f;
        Origin = Vector2.Zero;
        Effects = SpriteEffects.None;
        LayerDepth = 0f;
    }
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(DrawSettings settings) {
            float texW = settings.Texture.Width;
            float texH = settings.Texture.Height;

            float srcX = settings.SourceRectangle?.X ?? 0f;
            float srcY = settings.SourceRectangle?.Y ?? 0f;
            float srcW = settings.SourceRectangle?.Width ?? texW;
            float srcH = settings.SourceRectangle?.Height ?? texH;

            float dstW = srcW * settings.Scale.X;
            float dstH = srcH * settings.Scale.Y;

            sb.PushSprite(
                settings.Texture,
                srcX / texW,
                srcY / texH,
                srcW / texW,
                srcH / texH,
                settings.Position.X,
                settings.Position.Y,
                dstW,
                dstH,
                settings.Color,
                settings.Origin.X / srcW,
                settings.Origin.Y / srcH,
                (float)Math.Sin(settings.Rotation),
                (float)Math.Cos(settings.Rotation),
                settings.LayerDepth,
                (byte)((int)settings.Effects)
            );
        }
    }
}
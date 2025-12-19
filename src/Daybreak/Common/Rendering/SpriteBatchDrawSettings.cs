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
    ///     The destination rectangle for this draw.
    /// </summary>
    public Rectangle? DestinationRectangle = null;
    
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
    
    public DrawSettings(Texture2D texture) : this() {
        Texture = texture;
        Position = Vector2.Zero;
        SourceRectangle = null;
        DestinationRectangle = null;
        Color = Color.White;
        Rotation = 0f;
        Origin = Vector2.Zero;
        Scale = Vector2.One;
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
        public void Draw(DrawSettings settings)
        {
            float invW = 1f / settings.Texture.Width;
            float invH = 1f / settings.Texture.Height;

            float sX, sY, sW, sH;
            float srcW, srcH;

            if (settings.SourceRectangle.HasValue) {
                var src = settings.SourceRectangle.Value;
                sX = src.X * invW;
                sY = src.Y * invH;
                sW = src.Width * invW;
                sH = src.Height * invH;
                srcW = src.Width;
                srcH = src.Height;
            }
            else {
                sX = 0.0f;
                sY = 0.0f;
                sW = 1.0f;
                sH = 1.0f;
                srcW = settings.Texture.Width;
                srcH = settings.Texture.Height;
            }

            float dX, dY, dW, dH;
            if (settings.DestinationRectangle.HasValue) {
                var dest = settings.DestinationRectangle.Value;
                dX = dest.X;
                dY = dest.Y;
                dW = dest.Width;
                dH = dest.Height;
            }
            else {
                dX = settings.Position.X;
                dY = settings.Position.Y;
                dW = srcW * settings.Scale.X;
                dH = srcH * settings.Scale.Y;
            }

            float oX = settings.Origin.X / srcW;
            float oY = settings.Origin.Y / srcH;

            sb.PushSprite(
                settings.Texture,
                sX, sY, sW, sH,
                dX, dY, dW, dH,
                settings.Color,
                oX,
                oY,
                (float)Math.Sin(settings.Rotation),
                (float)Math.Cos(settings.Rotation),
                settings.LayerDepth,
                (byte)(settings.Effects)
            );
        }
    }
}

internal class Test : ModSystem
{
    public override void PostDrawTiles()
    {
        var spriteBatch = Main.spriteBatch;

        using (var scope = spriteBatch.Scope()) {
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                Main.DefaultSamplerState,
                DepthStencilState.None,
                Main.Rasterizer,
                null
            );

            var texture = AssetReferences.Assets.Images.UI.LockableSettingsToggle.Asset.Value;

            spriteBatch.Draw(new (texture) {
                Position = Main.MouseScreen,
                SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height - 11),
                Rotation = (float)Math.Sin(Main.timeForVisualEffects * 0.05f * MathF.PI)
            });
            
            spriteBatch.Draw(new (texture) {
                DestinationRectangle = new Rectangle(
                    (int)Main.MouseScreen.X, 
                    (int)Main.MouseScreen.Y, 
                    100, 
                    150
                ),
                Color = Color.Red * 0.5f,
                Rotation = (float)Math.Sin(Main.timeForVisualEffects * 0.01f * MathF.PI)
            });
        }
    }
}
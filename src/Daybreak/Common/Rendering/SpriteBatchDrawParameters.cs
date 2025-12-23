using System;
using System.Runtime.CompilerServices;
using Daybreak.Common.Mathematics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Daybreak.Common.Rendering;

/// <summary>
///     Encapsulates the full set of parameters for a single
///     <see cref="SpriteBatch.Draw(Microsoft.Xna.Framework.Graphics.Texture2D, Microsoft.Xna.Framework.Rectangle, Microsoft.Xna.Framework.Rectangle?, Microsoft.Xna.Framework.Color, float, Microsoft.Xna.Framework.Vector2, Microsoft.Xna.Framework.Graphics.SpriteEffects, float)"/>
///     call.
///     <br />
///     <br />
///     This struct acts as a lightweight, immutable value object describing how
///     a textured quad should be submitted to the <see cref="SpriteBatch"/>,
///     independent of the actual draw call.  This promotes reuse in basic
///     parameters and allows for more eloquent expression of values without
///     specifying redundant variables.
/// </summary>
/// <remarks>
///     Several properties are derived from others:
///     <list type="bullet">
///         <item>
///             <description>
///                 <see cref="Size"/> is computed from the <see cref="Scale"/>
///                 and the source texture (or <see cref="Source"/>, if
///                 provided).
///             </description>
///         </item>
///         <item>
///             <description>
///                 Setting <see cref="Size"/> updates <see cref="Scale"/> to
///                 preserve the requested absolute dimensions.
///             </description>
///         </item>
///         <item>
///             <description>
///                 <see cref="Destination"/> is a convenience wrapper over
///                 <see cref="Position"/> and <see cref="Size"/>, additionally
///                 performing integer truncation.
///             </description>
///         </item>
///     </list>
///     <br />
///     This type is intended to be cheap to copy and suitable for transient
///     construction during draw submission.
/// </remarks>
public readonly struct DrawParameters
{
    /// <summary>
    ///     The texture to render.
    /// </summary>
    public Texture2D Texture =>
        field
     ?? asset?.Value
     ?? throw new InvalidOperationException("Attempted to get the Texture of DrawParameters, but no texture was provided?");

    /// <summary>
    ///     The world- or screen-space position at which the quad will be
    ///     rendered.
    ///     <br />
    ///     Interpreted as the top-left corner prior to origin, rotation, and
    ///     scale being applied.
    /// </summary>
    public Vector2 Position { get; init; } = Vector2.Zero;

    /// <summary>
    ///     An optional sub-region of the <see cref="Texture"/> to sample from.
    ///     <br />
    ///     When used, this essentially crops the texture to the specified
    ///     region and treats that cropped area as if it were the entire
    ///     texture.
    ///     <br />
    ///     <br />
    ///     When <see langword="null"/>, the entire texture is used (default).
    /// </summary>
    public Rectangle? Source { get; init; } = null;

    /// <summary>
    ///     Gets or sets the absolute, on-screen size of the rendered quad.
    ///     <br />
    ///     <br />
    ///     Reading this value derives it from the <see cref="Scale"/> and the
    ///     source dimensions of the texture.  Writing this value updates
    ///     <see cref="Scale"/> so that the requested size is preserved.
    /// </summary>
    public Vector2 Size
    {
        get
        {
            float sw = Source?.Width ?? Texture.Width;
            float sh = Source?.Height ?? Texture.Height;
            return new Vector2(sw * Scale.X, sh * Scale.Y);
        }

        init
        {
            float sw = Source?.Width ?? Texture.Width;
            float sh = Source?.Height ?? Texture.Height;
            Scale = new Vector2(value.X / sw, value.Y / sh);
        }
    }

    /// <summary>
    ///     Gets or sets the destination rectangle of the draw in integer
    ///     coordinates.
    ///     <br />
    ///     <br />
    ///     Setting this updates <see cref="Position"/> and <see cref="Size"/>.
    ///     Reading this returns the integer-truncated values of
    ///     <see cref="Position"/> and <see cref="Size"/>.
    /// </summary>
    public Rectangle Destination
    {
        get => new((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        init
        {
            Position = new Vector2(value.X, value.Y);
            Size = new Vector2(value.Width, value.Height);
        }
    }

    /// <summary>
    ///     The color tint applied to the texture at render time.
    ///     <br />
    ///     <see cref="Color.White"/> results in no tinting.
    /// </summary>
    public Color Color { get; init; } = Color.White;

    /// <summary>
    ///     The angle, in radians, used to rotate the texture around the
    ///     <see cref="Origin"/>.
    /// </summary>
    public Angle Rotation { get; init; } = Angle.Zero;

    /// <summary>
    ///     The origin point, in texture-space coordinates, about which rotation
    ///     and scaling and performed.
    /// </summary>
    public Vector2 Origin { get; init; } = Vector2.Zero;

    /// <summary>
    ///     The scale factor applied to the source texture.
    ///     <br />
    ///     <br />
    ///     This value is considered the canonical representation of scale;
    ///     other size-related properties derive from it.
    /// </summary>
    public Vector2 Scale { get; init; } = Vector2.One;

    /// <summary>
    ///     Additional, built-in effects provided by <see cref="BasicEffect"/>.
    ///     <br />
    ///     Includes options for mirroring and flipping the texture during
    ///     rendering.
    /// </summary>
    public SpriteEffects Effects { get; init; } = SpriteEffects.None;

    /// <summary>
    ///     The depth value used for draw ordering when the
    ///     <see cref="SpriteBatch"/> is configured to or sprites by depth.
    /// </summary>
    /// <remarks>
    ///     This is seldom-used in Terraria rendering!
    /// </remarks>
    public float LayerDepth { get; init; } = 0f;

    private readonly Asset<Texture2D>? asset;

    /// <summary>
    ///     Initializes a new instance of <see cref="DrawParameters"/> which
    ///     will use the given <paramref name="texture"/>.
    /// </summary>
    public DrawParameters(Texture2D texture)
    {
        Texture = texture;
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="DrawParameters"/> which
    ///     will use the given <paramref name="asset"/>.  The value of the
    ///     <paramref name="asset"/> is resolved upon request from the
    ///     <see cref="Texture"/> property.
    /// </summary>
    public DrawParameters(Asset<Texture2D> asset)
    {
        this.asset = asset;
    }

    /// <summary>
    ///     Truncates the <see cref="Position"/> and <see cref="Size"/> to
    ///     render at integer coordinates by setting <see cref="Destination"/>
    ///     to itself.
    /// </summary>
    public DrawParameters Truncate()
    {
        return this with { Destination = Destination };
    }
}

/// <summary>
///     Extensions to <see cref="SpriteBatch"/> using <see cref="DrawParameters"/>
///     instances.
/// </summary>
public static class SpriteBatchDrawSettingsExtensions
{
    /// <param name="sb">The <see cref="SpriteBatch"/>.</param>
    extension(SpriteBatch sb)
    {
        /// <summary>
        ///     Pushes the <paramref name="parameters"/> to the
        ///     <paramref name="sb"/> for rendering.
        /// </summary>
        /// <param name="parameters">
        ///     The parameters determining how a quad is rendered.
        /// </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Draw(in DrawParameters parameters)
        {
            var tex = parameters.Texture;
            {
                ArgumentNullException.ThrowIfNull(tex);
            }

            sb.CheckBegin(nameof(Draw));

            var texW = (float)tex.Width;
            var texH = (float)tex.Height;

            var srcX = parameters.Source?.X ?? 0f;
            var srcY = parameters.Source?.Y ?? 0f;
            var srcW = parameters.Source?.Width ?? texW;
            var srcH = parameters.Source?.Height ?? texH;

            var dstW = srcW * parameters.Scale.X;
            var dstH = srcH * parameters.Scale.Y;

            var (sin, cos) = parameters.Rotation.SinCos();

            sb.PushSprite(
                tex,
                srcX / texW,
                srcY / texH,
                srcW / texW,
                srcH / texH,
                parameters.Position.X,
                parameters.Position.Y,
                dstW,
                dstH,
                parameters.Color,
                parameters.Origin.X / srcW,
                parameters.Origin.Y / srcH,
                sin,
                cos,
                parameters.LayerDepth,
                (byte)parameters.Effects
            );
        }
    }
}

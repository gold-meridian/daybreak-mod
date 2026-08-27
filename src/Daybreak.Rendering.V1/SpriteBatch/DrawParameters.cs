using System.Runtime.CompilerServices;
using Daybreak.Mathematics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;

namespace Daybreak.Rendering;

/// <summary>
///     Encapsulates the full set of parameters for a single
///     <see cref="SpriteBatch"/> <c>Draw</c> call.
///     <br />
///     <br />
///     This structure acts as a lightweight object describing how a textured
///     quad should be submitted to the <see cref="SpriteBatch"/>, independent
///     of the actual draw call.  This promotes reuse of basic parameters and
///     allows for more eloquent expression of values without specifying
///     redundant variables.
/// </summary>
public struct DrawParameters
{
    internal readonly ref struct Resolved
    {
        public readonly float SrcX;
        public readonly float SrcY;
        public readonly float SrcW;
        public readonly float SrcH;
        public readonly Vector2 Scale;
        public readonly Vector2 Origin;

        public Rectangle Source => new((int)SrcX, (int)SrcY, (int)SrcW, (int)SrcH);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Resolved(scoped in DrawParameters parameters)
        {
            SrcX = parameters.Source?.X ?? 0f;
            SrcY = parameters.Source?.Y ?? 0f;
            (SrcW, SrcH) = parameters.SourceDimensions;

            Scale = parameters.sizeOverride is { } size
                ? new Vector2(size.X / SrcW, size.Y / SrcH)
                : parameters.rawScale;

            Origin = parameters.Origin.GetOrigin(new Vector2(SrcW, SrcH));
        }
    }

    /// <summary>
    ///     The texture to render.
    /// </summary>
    public Texture2D Texture { get; }

    /// <summary>
    ///     The world- or screen-space position at which the quad will be
    ///     rendered.
    ///     <br />
    ///     Interpreted as the top-left corner prior to origin, rotation, and
    ///     scale being applied.
    /// </summary>
    public Vector2 Position { get; set; } = Vector2.Zero;

    /// <summary>
    ///     An optional subregion of the <see cref="Texture"/> to sample from.
    ///     <br />
    ///     When used, this essentially crops the texture to the specified
    ///     region and treats that cropped area as if it were the entire
    ///     texture.
    ///     <br />
    ///     <br />
    ///     When <see langword="null"/>, the entire texture is used (default).
    /// </summary>
    public Rectangle? Source { get; set; } = null;

    /// <summary>
    ///     The scale factor applied to the source texture.
    /// </summary>
    public Vector2 Scale
    {
        readonly get
        {
            if (sizeOverride is { } size)
            {
                var (sw, sh) = SourceDimensions;
                return new Vector2(size.X / sw, size.Y / sh);
            }

            return rawScale;
        }

        set
        {
            rawScale = value;
            sizeOverride = null;
        }
    }

    private Vector2 rawScale = Vector2.One;

    /// <summary>
    ///     Gets or sets the absolute, on-screen size of the rendered quad.
    /// </summary>
    public Vector2 Size
    {
        readonly get
        {
            if (sizeOverride is { } size)
            {
                return size;
            }

            var (sw, sh) = SourceDimensions;
            return new Vector2(sw * rawScale.X, sh * rawScale.Y);
        }

        set => sizeOverride = value;
    }

    private Vector2? sizeOverride;

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
        readonly get
        {
            var size = Size;
            return new Rectangle((int)Position.X, (int)Position.Y, (int)size.X, (int)size.Y);
        }

        set
        {
            Position = new Vector2(value.X, value.Y);
            sizeOverride = new Vector2(value.Width, value.Height);
        }
    }

    /// <summary>
    ///     The color tint applied to the texture at render time.
    ///     <br />
    ///     <see cref="Color.White"/> results in no tinting.
    /// </summary>
    public Color Color { get; set; } = Color.White;

    /// <summary>
    ///     The angle, in radians, used to rotate the texture around the
    ///     <see cref="Origin"/>.
    /// </summary>
    public Angle Rotation { get; set; } = Angle.Zero;

    /// <summary>
    ///     The origin point.  This may be an explicit point in
    ///     source-texture-space pixel coordinates, an anchor point evaluated
    ///     during resolution, and an arbitrary function. 
    /// </summary>
    public Origin Origin { get; set; } = Origin.TopLeft;

    /// <summary>
    ///     Additional, built-in effects provided by <see cref="BasicEffect"/>.
    ///     <br />
    ///     Includes options for mirroring and flipping the texture during
    ///     rendering.
    /// </summary>
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    /// <summary>
    ///     The depth value used for draw ordering when the
    ///     <see cref="SpriteBatch"/> is configured to or sprites by depth.
    /// </summary>
    /// <remarks>
    ///     This is seldom used in Terraria rendering!
    /// </remarks>
    public float LayerDepth { get; set; } = 0f;

    private readonly (float Width, float Height) SourceDimensions =>
        (Source?.Width ?? Texture.Width, Source?.Height ?? Texture.Height);

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
        Texture = asset.Value;
    }

    /// <summary>
    ///     Truncates the <see cref="Position"/> and <see cref="Size"/> to
    ///     render at integer coordinates by setting <see cref="Destination"/>
    ///     to itself.
    /// </summary>
    public readonly DrawParameters Truncate()
    {
        return this with { Destination = Destination };
    }

    /// <summary>
    ///     Sets a new origin while keeping the resolved position the same.
    /// </summary>
    public readonly DrawParameters WithOrigin(Origin newOrigin)
    {
        var r = Resolve();
        var newOriginPos = newOrigin.GetOrigin(new Vector2(r.SrcW, r.SrcH));

        var result = this;
        {
            result.Position += (newOriginPos - r.Origin) * r.Scale;
            result.Origin = newOrigin;
        }

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal readonly Resolved Resolve()
    {
        return new Resolved(in this);
    }

    /// <summary>
    ///     Converts this set of parameters to a <see cref="DrawData"/> instance
    ///     to be used in appropriate APIs.
    /// </summary>
    public readonly DrawData ToDrawData()
    {
        var r = Resolve();

        return new DrawData(
            Texture,
            Position,
            r.Source,
            Color,
            Rotation.Radians,
            r.Origin,
            r.Scale,
            Effects,
            LayerDepth
        );
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
            sb.CheckBegin(nameof(Draw));

            var r = parameters.Resolve();
            var (sin, cos) = parameters.Rotation.SinCos();
            var texW = (float)parameters.Texture.Width;
            var texH = (float)parameters.Texture.Height;

            sb.PushSprite(
                parameters.Texture,
                r.SrcX / texW,
                r.SrcY / texH,
                r.SrcW / texW,
                r.SrcH / texH,
                parameters.Position.X,
                parameters.Position.Y,
                r.SrcW * r.Scale.X,
                r.SrcH * r.Scale.Y,
                parameters.Color,
                r.Origin.X / r.SrcW,
                r.Origin.Y / r.SrcH,
                sin,
                cos,
                parameters.LayerDepth,
                (byte)parameters.Effects
            );
        }
    }

    extension(Main)
    {
        /// <inheritdoc cref="Main.EntitySpriteDraw(DrawData)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EntitySpriteDraw(DrawParameters parameters)
        {
            Main.EntitySpriteDraw(parameters.ToDrawData());
        }
    }
}

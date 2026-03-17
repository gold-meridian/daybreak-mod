using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Drawing;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Daybreak.Common.Features.Tiles;

/// <summary>
///     Represents an instance of a tile whose
///     contents are rendered as a shader post-processing
///     step at the end of standard tile renders.
/// </summary>
public abstract class ShaderMaskedTile : ModTile
{
    /// <summary>
    ///     The set of points on the tile grid
    ///     that should be rendered.
    /// </summary>
    public HashSet<Point> RenderPointsCache
    {
        get;
    } = [];

    /// <summary>
    ///     Whether any of this kind of tile are being
    ///     rendered this frame or not.
    /// </summary>
    public bool Active => RenderPointsCache.Count >= 1;

    /// <summary>
    ///     The mask render target that contains
    ///     the contents of all tiles of this kind
    ///     on-screen.
    /// </summary>
    public RenderTargetLease? Mask
    {
        get;
        private set;
    }

    private void RenderIntoMaskTarget()
    {
        Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Matrix.Identity);

        foreach (Point p in RenderPointsCache)
            RenderIntoMask(p);

        Main.spriteBatch.End();
    }

    private void RenderShadedMaskTargetResults()
    {
        if (Mask is null)
            return;

        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

        float yOffset = TileObjectData.GetTileData(Type, 0)?.DrawYOffset ?? 0f;
        ApplyShader();
        Main.spriteBatch.Draw(Mask.Target, Vector2.UnitY * yOffset, Color.White);

        Main.spriteBatch.End();
    }

    /// <summary>
    ///     Applies optional tile-independent effects before
    ///     tiles get rendered into the mask target.
    /// </summary>
    protected virtual void PreMaskTargetRender()
    { }

    /// <summary>
    ///     Renders a masked tile instance
    ///     into the mask target.
    /// </summary>
    /// <remarks>
    ///     This is distinct from the typical Draw
    ///     hooks, and exists solely to dictate what
    ///     is presented by the shader processing
    ///     the mask target.
    ///     
    ///     <br></br>
    /// 
    ///     You may still use PreDraw and PostDraw
    ///     to perform standard tile rendering tasks.
    /// </remarks>
    protected abstract void RenderIntoMask(Point p);

    /// <summary>
    ///     Applies the post-processing shader to
    ///     the mask target for use at the time of rendering.
    /// </summary>
    protected abstract void ApplyShader();

    /// <inheritdoc />
    public override void DrawEffects(int x, int y, SpriteBatch spriteBatch, ref TileDrawInfo drawData) =>
        Main.instance.TilesRenderer.AddSpecialPoint(x, y, TileDrawing.TileCounterType.CustomSolid);

    /// <inheritdoc />
    public override void SpecialDraw(int x, int y, SpriteBatch spriteBatch) =>
        RenderPointsCache.Add(new Point(x, y));

    [ModSystemHooks.PostDrawTiles]
    internal static void RenderMaskedTiles()
    {
        foreach (ShaderMaskedTile tiles in ModContent.GetContent<ShaderMaskedTile>())
        {
            if (!tiles.Active)
                continue;

            tiles.Mask ??= ScreenspaceTargetPool.Shared.Rent(Main.instance.GraphicsDevice);

            tiles.PreMaskTargetRender();
            using (tiles.Mask.Scope(clearColor: Color.Transparent))
            {
                tiles.RenderIntoMaskTarget();
            }

            tiles.RenderShadedMaskTargetResults();
            tiles.RenderPointsCache.Clear();
        }
    }
}

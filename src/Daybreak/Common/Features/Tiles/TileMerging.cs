using Daybreak.Common.Features.Hooks;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Tiles;

// Adapted from 'https://github.com/GabeHasWon/SpiritReforged/blob/scarabdate/Common/TileCommon/TileMerging/TileMerger.cs' with permission from math2, and GabeHasWon.
public static class TileMerging
{
    private static readonly Dictionary<int, Asset<Texture2D>> textureOverlayByType = [];

    private static Dictionary<int, HashSet<int>> MergesWith { get; } = [];

    private static readonly Point[] offsets =
    [
        new(-1, -1), new(18, 0), new(18, 36), new(54, 0),
        new(0, 18), new(0, 0), new(0, 36), new(90, 0),
        new(36, 18), new(36, 0), new(36, 36), new(90, 18),
        new(54, 18), new(72, 0), new(72, 18), new(18, 18),
    ];

    private static readonly Point[] corner_offsets =
    [
        new(-1, -1), new(18, 90), new(18, 72), new(36, 54),
        new(0, 90), new(0, 54), new(18, 108), new(54, 90),
        new(0, 72), new(0, 108), new(18, 54), new(54, 72),
        new(54, 54), new(36, 90), new(36, 72), new(36, 108),
    ];

    private static bool[] UsesCornerMergeFrames { get; set; } = [];

    private static Mod Mod => ModContent.GetInstance<ModImpl>();

    [ModSystemHooks.ResizeArrays]
    private static void ResizeArrays()
    {
        UsesCornerMergeFrames = CreateSet(nameof(UsesCornerMergeFrames), false);

        return;

        static T[] CreateSet<T>(string name, T defaultState)
        {
            return TileID.Sets.Factory.CreateNamedSet(Mod, name)
                         .RegisterCustomSet(defaultState);
        }
    }

    /// <summary>
    /// Adds custom merge frame overlays for supplied <paramref name="targetTypes"/>;
    /// uses a texture format different to that of vanilla dirt merge frames.
    /// <br/>
    /// Additionally, merge overlays will use the paint of the neighboring
    /// tiles over the paint of the center tile.
    /// </summary>
    public static void AddCustomMerge(int type, Asset<Texture2D> texture, params int[] targetTypes)
    {
        textureOverlayByType[type] = texture;

        TileID.Sets.ChecksForMerge[type] = true;

        foreach (int targetType in targetTypes)
        {
            MergesWith.TryAdd(targetType, []);
            MergesWith[targetType].Add(type);

            Main.tileMerge[type][targetType] = true;
            Main.tileMerge[targetType][type] = true;

            TileID.Sets.ChecksForMerge[targetType] = true;
        }
    }

    /// <inheritdoc cref="AddCustomMerge(int, Asset{Texture2D}, int[])"/>
    public static void AddCustomMerge(int type, bool useCorners, Asset<Texture2D> texture, params int[] targetTypes)
    {
        UsesCornerMergeFrames[type] = useCorners;

        AddCustomMerge(type, texture, targetTypes);
    }

    #region Drawing
    [GlobalTileHooks.PostDraw]
    private static void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        if (MergesWith.TryGetValue(type, out var merges) && merges.Count > 0)
        {
            DrawMerge(spriteBatch, i, j, merges);
        }
    }

    private static void DrawMerge(SpriteBatch spriteBatch, int i, int j, params IEnumerable<int> types)
    {
        const int full_frame_width = 108;

        Tile tile = Framing.GetTileSafely(i, j);

        Color color = Lighting.GetColor(i, j);

        int frameNumber = tile.Get<TileWallWireStateData>().TileFrameNumber;

        Vector2 zero =
            Main.drawToScreen
          ? Vector2.Zero
          : new Vector2(Main.offScreenRange);

        Vector2 position = new Vector2(i * 16, j * 16) - Main.screenPosition + zero;

        foreach (int type in types)
        {
            (int mask, int paint) = GetMergeData(i, j, type);

            if (UsesCornerMergeFrames[type])
            {
                DrawCorners(type, mask);
            }

            if (mask <= 0 ||
                !textureOverlayByType.TryGetValue(type, out var asset))
            {
                continue;
            }

            Color finalColor = color;

            Texture2D? texture = null;

            if (paint > PaintID.None && !TryGetPaintTexture(type, paint, asset, out texture))
            {
                finalColor = finalColor.MultiplyRGBA(WorldGen.paintColor(paint));
            }

            texture ??= asset.Value;

            Point p = offsets[mask];

            var source = new Rectangle(p.X + (frameNumber * full_frame_width), p.Y, 16, 16);

            spriteBatch.Draw(texture, position, source, finalColor);
        }

        return;

        void DrawCorners(int type, int edgeMask)
        {
            (int mask, int paint) = GetMergeCornerData(i, j, type, edgeMask);

            if (mask <= 0 ||
                !textureOverlayByType.TryGetValue(type, out var asset))
            {
                return;
            }

            Color finalColor = color;

            Texture2D? texture = null;

            if (paint > PaintID.None && !TryGetPaintTexture(type, paint, asset, out texture))
            {
                finalColor = finalColor.MultiplyRGBA(WorldGen.paintColor(paint));
            }

            texture ??= asset.Value;

            Point p = corner_offsets[mask];

            var source = new Rectangle(p.X + (frameNumber * full_frame_width), p.Y, 16, 16);

            spriteBatch.Draw(texture, position, source, finalColor);
        }
    }

    private static (int mask, int shaderIndex) GetMergeData(int i, int j, int type)
    {
        var center = Framing.GetTileSafely(i, j);

        var mask = 0;
        var shaderIndex = 0;

        // Check for each tile merging with its neighbor;
        // tiles should only merge if their slope state allows it.

        var down = Framing.GetTileSafely(i, j + 1);
        if (center.Slope.Down && down.Slope.Up && !down.IsHalfBlock)
        {
            Check(down, 2);
        }

        // Half tiles should only merge with the tile below them.
        if (center.IsHalfBlock)
        {
            return (mask, shaderIndex);
        }

        var up = Framing.GetTileSafely(i, j - 1);
        if (center.Slope.Up && up.Slope.Down)
        {
            Check(up, 1);
        }

        var left = Framing.GetTileSafely(i - 1, j);
        if (center.Slope.Left && left.Slope.Right && !left.IsHalfBlock)
        {
            Check(left, 4);
        }

        var right = Framing.GetTileSafely(i + 1, j);
        if (center.Slope.Right && right.Slope.Left && !right.IsHalfBlock)
        {
            Check(right, 8);
        }

        return (mask, shaderIndex);

        void Check(Tile tile, int bit)
        {
            if (tile.TileType != type)
            {
                return;
            }

            mask |= bit;

            if (shaderIndex == 0)
            {
                shaderIndex = tile.TileColor;
            }
        }
    }

    private static (int mask, int shaderIndex) GetMergeCornerData(int i, int j, int type, int edgeMask)
    {
        var center = Framing.GetTileSafely(i, j);

        var mask = 0;
        var shaderIndex = 0;

        var downLeft = Framing.GetTileSafely(i - 1, j + 1);
        if (center.Slope.DownLeft && downLeft.Slope.UpRight && !downLeft.IsHalfBlock
         && (edgeMask & 2) == 0 && (edgeMask & 4) == 0)
        {
            Check(downLeft, 2);
        }

        var downRight = Framing.GetTileSafely(i + 1, j + 1);
        if (center.Slope.DownRight && downLeft.Slope.UpLeft && !downLeft.IsHalfBlock
         && (edgeMask & 2) == 0 && (edgeMask & 8) == 0)
        {
            Check(downRight, 8);
        }

        if (center.IsHalfBlock)
        {
            return (mask, shaderIndex);
        }

        var upLeft = Framing.GetTileSafely(i - 1, j - 1);
        if (center.Slope.UpLeft && upLeft.Slope.DownRight
         && (edgeMask & 1) == 0 && (edgeMask & 4) == 0)
        {
            Check(upLeft, 1);
        }

        var upRight = Framing.GetTileSafely(i + 1, j - 1);
        if (center.Slope.UpRight && upRight.Slope.DownLeft
         && (edgeMask & 1) == 0 && (edgeMask & 8) == 0)
        {
            Check(upRight, 4);
        }

        return (mask, shaderIndex);

        void Check(Tile tile, int bit)
        {
            if (tile.TileType != type)
            {
                return;
            }

            mask |= bit;

            if (shaderIndex == 0)
            {
                shaderIndex = tile.TileColor;
            }
        }
    }
    #endregion

    #region Paint
    private static readonly Dictionary<TileMergingVariantKey, TileMergingRenderTargetHolder> paintCache = [];

    private readonly record struct TileMergingVariantKey(int Type, int PaintColor);

    private class TileMergingRenderTargetHolder(TileMergingVariantKey key, Asset<Texture2D> asset, int copySettingsFrom = -1) : TilePaintSystemV2.ARenderTargetHolder
    {
        private TileMergingVariantKey Key { get; } = key;

        private TreePaintingSettings PaintSettings { get; } = TreePaintSystemData.GetTileSettings(copySettingsFrom, 0);

        private Asset<Texture2D> Texture { get; } = asset;

        public override void Prepare()
        {
            Texture.Wait();

            PrepareTextureIfNecessary(Texture.Value);
        }

        public override void PrepareShader()
        {
            PrepareShader(Key.PaintColor, PaintSettings);
        }
    }

    private static bool TryGetPaintTexture(
        int type,
        int paintColor,
        Asset<Texture2D> asset,
        [NotNullWhen(true)] out Texture2D? texture
    )
    {
        texture = null;

        TileMergingVariantKey key = new(type, paintColor);

        if (paintCache.TryGetValue(key, out TileMergingRenderTargetHolder? holder) &&
            holder.IsReady)
        {
            texture = holder.Target;

            return true;
        }

        var newHolder = new TileMergingRenderTargetHolder(key, asset);

        paintCache[key] = newHolder;

        Main.instance.TilePaintSystem._requests.Add(newHolder);

        return false;
    }
    #endregion
}

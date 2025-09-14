using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.IDs;

/// <summary>
///     Provides Tile ID sets.
/// </summary>
public sealed class DaybreakTileSets : ModSystem
{
    public static int?[] OtherTileDrawDataToCopy { get; private set; } = [];

    public override void ResizeArrays()
    {
        base.ResizeArrays();

        OtherTileDrawDataToCopy = CreateSet<int?>(nameof(OtherTileDrawDataToCopy), null);

        return;

        T[] CreateSet<T>(string name, T defaultState)
        {
            return TileID.Sets.Factory.CreateNamedSet(Mod, name)
                         .RegisterCustomSet(defaultState);
        }
    }

    public override void Load()
    {
        base.Load();

        On_TileDrawing.GetTileDrawData += GetTileDrawData_CopyDataWhenRequested;
    }

    private static void GetTileDrawData_CopyDataWhenRequested(
        On_TileDrawing.orig_GetTileDrawData orig,
        TileDrawing self,
        int x,
        int y,
        Tile tileCache,
        ushort typeCache,
        ref short tileFrameX,
        ref short tileFrameY,
        out int tileWidth,
        out int tileHeight,
        out int tileTop,
        out int halfBrickHeight,
        out int addFrX,
        out int addFrY,
        out SpriteEffects tileSpriteEffect,
        out Texture2D glowTexture,
        out Rectangle glowSourceRect,
        out Color glowColor
    )
    {
        var copyType = OtherTileDrawDataToCopy[typeCache];
        if (copyType.HasValue)
        {
            typeCache = (ushort)copyType.Value;
        }
        
        orig(
            self,
            x,
            y,
            tileCache,
            typeCache,
            ref tileFrameX,
            ref tileFrameY,
            out tileWidth,
            out tileHeight,
            out tileTop,
            out halfBrickHeight,
            out addFrX,
            out addFrY,
            out tileSpriteEffect,
            out glowTexture,
            out glowSourceRect,
            out glowColor
        );
    }
}
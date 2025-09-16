using Daybreak.Common.Features.Hooks;
using Daybreak.Common.IDs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Tiles;

/// <summary>
///     Behaves like a Terraria cattail.
/// </summary>
public interface ICattail
{
    /// <summary>
    ///     Checks validity of the tile and kills it if it isn't valid.
    /// </summary>
    void CheckCattail(int x, int y);

    /// <summary>
    ///     Grows a cattail at the specified coordinates.
    /// </summary>
    void GrowCattail(int x, int y);

    /// <summary>
    ///     Climbs to the bottom of a cattail and returns the height.
    /// </summary>
    int ClimbCattail(int originX, int originY);
}

/// <summary>
///     Cattail implementation.
/// </summary>
public abstract class CattailTile : ModTile, ICattail
{
    /// <inheritdoc />
    public override void SetStaticDefaults()
    {
        base.SetStaticDefaults();

        TileID.Sets.TileCutIgnore.Regrowth[Type] = true;

        Main.tileCut[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileLighted[Type] = true;
        Main.tileNoFail[Type] = true;

        DaybreakTileSets.OtherTileDrawDataToCopy[Type] = TileID.Cattail;
    }

    /// <inheritdoc />
    public override void RandomUpdate(int i, int j)
    {
        base.RandomUpdate(i, j);

        CheckCattail(i, j);
        if (!Main.tile[i, j].HasTile || !WorldGen.genRand.NextBool(8))
        {
            return;
        }

        GrowCattail(i, j);
        CheckCattail(i, j);
    }

    /// <inheritdoc />
    public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
    {
        CheckCattail(i, j);
        return false;
    }

    /// <inheritdoc />
    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        if (Main.tile[i, j].frameX / 18 <= 4)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileGrass);
        }

        return false;
    }

    /// <inheritdoc />
    public virtual void GrowCattail(int x, int y)
    {
        if (Main.netMode == NetmodeID.MultiplayerClient)
        {
            return;
        }

        var num = y;
        while (Main.tile[x, num].liquid > 0 && num > 50)
        {
            num--;
        }

        num++;
        int i;
        for (i = num; (!Main.tile[x, i].active() || !Main.tileSolid[Main.tile[x, i].type] || Main.tileSolidTop[Main.tile[x, i].type]) && i < Main.maxTilesY - 50; i++) { }

        num = i - 1;
        while (Main.tile[x, num].active() && Main.tile[x, num].type == Type)
        {
            num--;
        }

        num++;
        if (Main.tile[x, num].frameX == 90 && Main.tile[x, num - 1].active() && Main.tileCut[Main.tile[x, num - 1].type])
        {
            WorldGen.KillTile(x, num - 1);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 0, x, num - 1);
            }
        }

        if (Main.tile[x, num - 1].active())
        {
            return;
        }

        if (Main.tile[x, num].frameX == 0)
        {
            Main.tile[x, num].frameX = 18;
            WorldGen.SquareTileFrame(x, num);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendTileSquare(-1, x, num);
            }
        }
        else if (Main.tile[x, num].frameX == 18)
        {
            Main.tile[x, num].frameX = (short)(18 * WorldGen.genRand.Next(2, 5));
            Main.tile[x, num - 1].active(active: true);
            Main.tile[x, num - 1].type = Type;
            Main.tile[x, num - 1].frameX = 90;
            Main.tile[x, num - 1].frameY = Main.tile[x, num].frameY;
            Main.tile[x, num - 1].halfBrick(halfBrick: false);
            Main.tile[x, num - 1].slope(0);
            Main.tile[x, num - 1].CopyPaintAndCoating(Main.tile[x, num]);
            WorldGen.SquareTileFrame(x, num);
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendTileSquare(-1, x, num);
            }
        }
        else if (Main.tile[x, num].frameX == 90)
        {
            if (Main.tile[x, num - 1].liquid == 0)
            {
                if (!Main.tile[x, num - 2].active() && (Main.tile[x, num].liquid > 0 || Main.tile[x, num + 1].liquid > 0 || Main.tile[x, num + 2].liquid > 0) && WorldGen.genRand.NextBool(3))
                {
                    Main.tile[x, num].frameX = 108;
                    Main.tile[x, num - 1].active(active: true);
                    Main.tile[x, num - 1].type = Type;
                    Main.tile[x, num - 1].frameX = 90;
                    Main.tile[x, num - 1].frameY = Main.tile[x, num].frameY;
                    Main.tile[x, num - 1].halfBrick(halfBrick: false);
                    Main.tile[x, num - 1].slope(0);
                    Main.tile[x, num - 1].CopyPaintAndCoating(Main.tile[x, num]);
                    WorldGen.SquareTileFrame(x, num);
                }
                else
                {
                    var num2 = WorldGen.genRand.Next(3);
                    Main.tile[x, num].frameX = (short)(126 + num2 * 18);
                    Main.tile[x, num - 1].active(active: true);
                    Main.tile[x, num - 1].type = Type;
                    Main.tile[x, num - 1].frameX = (short)(180 + num2 * 18);
                    Main.tile[x, num - 1].frameY = Main.tile[x, num].frameY;
                    Main.tile[x, num - 1].halfBrick(halfBrick: false);
                    Main.tile[x, num - 1].slope(0);
                    Main.tile[x, num - 1].CopyPaintAndCoating(Main.tile[x, num]);
                    WorldGen.SquareTileFrame(x, num);
                }
            }
            else
            {
                Main.tile[x, num].frameX = 108;
                Main.tile[x, num - 1].active(active: true);
                Main.tile[x, num - 1].type = Type;
                Main.tile[x, num - 1].frameX = 90;
                Main.tile[x, num - 1].frameY = Main.tile[x, num].frameY;
                Main.tile[x, num - 1].halfBrick(halfBrick: false);
                Main.tile[x, num - 1].slope(0);
                Main.tile[x, num - 1].CopyPaintAndCoating(Main.tile[x, num]);
                WorldGen.SquareTileFrame(x, num);
            }
        }

        WorldGen.SquareTileFrame(x, num - 1, resetFrame: false);
        if (Main.netMode == NetmodeID.Server)
        {
            NetMessage.SendTileSquare(-1, x, num - 1, 1, 2);
        }
    }

    /// <inheritdoc />
    public virtual int ClimbCattail(int originX, int originY)
    {
        var num = 0;
        var num2 = originY;
        while (num2 > 10)
        {
            var tile = Main.tile[originX, num2];
            if (!tile.active() || tile.type != Type)
            {
                break;
            }

            if (tile.frameX >= 180)
            {
                num++;
                break;
            }

            num2--;
            num++;
        }

        return num;
    }

    /// <inheritdoc />
    public abstract void CheckCattail(int x, int y);
}

internal static partial class WaterFoliageHandler
{
    [OnLoad]
    public static void Load_Cattails()
    {
        On_TileDrawing.DrawMultiTileGrassInWind += DrawMultiTileGrassInWind_ClimbModdedCattails;
    }

    private static void DrawMultiTileGrassInWind_ClimbModdedCattails(
        On_TileDrawing.orig_DrawMultiTileGrassInWind orig,
        TileDrawing self,
        Vector2 screenPosition,
        Vector2 offSet,
        int topLeftX,
        int topLeftY,
        int sizeX,
        int sizeY
    )
    {
        var tile = Main.tile[topLeftX, topLeftY];
        if (TileLoader.GetTile(tile.TileType) is ICattail cattail)
        {
            sizeX = 1;
            sizeY = cattail.ClimbCattail(topLeftX, topLeftY);
            topLeftY -= sizeY - 1;
        }

        orig(self, screenPosition, offSet, topLeftX, topLeftY, sizeX, sizeY);
    }
}
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.PotLoot;

internal sealed class PotLootImpl : ModSystem
{
    public override void Load()
    {
        base.Load();

        // Vanilla logic is completely rewritten here in favor of directly
        // implementing everything Nightshade needs.
        On_WorldGen.CheckPot += CheckPot_EncodeVanillaAndModdedStyles;
    }

    private static void CheckPot_EncodeVanillaAndModdedStyles(On_WorldGen.orig_CheckPot orig, int i, int j, int type)
    {
        if (WorldGen.destroyObject)
        {
            return;
        }

        var startX = 0;
        var startY = j;
        for (startX += Main.tile[i, j].frameX / 18; startX > 1; startX -= 2) { }

        startX *= -1;
        startX += i;

        var frameY = Main.tile[i, j].frameY / 18;
        var style = 0;
        while (frameY > 1)
        {
            frameY -= 2;
            style++;
        }

        startY -= frameY;

        var dontHandleBreak = false;
        for (var x = startX; x < startX + 2; x++)
        {
            for (var y = startY; y < startY + 2; y++)
            {
                int frameXOffset;
                for (frameXOffset = Main.tile[x, y].frameX / 18; frameXOffset > 1; frameXOffset -= 2) { }

                if (!Main.tile[x, y].active() || Main.tile[x, y].type != type || frameXOffset != x - startX || Main.tile[x, y].frameY != ((y - startY) * 18) + (style * 36))
                {
                    dontHandleBreak = true;
                }
            }

            if (!WorldGen.SolidTile2(x, startY + 2))
            {
                dontHandleBreak = true;
            }
        }

        if (!dontHandleBreak)
        {
            return;
        }

        WorldGen.destroyObject = true;

        if (!PotBehavior.TryGetPotBehavior(type, out var pot))
        {
            return;
        }

        var ctx = new PotBreakContext(i, j, style);

        pot.PlayBreakSound(ctx);

        var drop = TileLoader.Drop(i, j, type);
        for (var m = startX; m < startX + 2; m++)
        {
            for (var n = startY; n < startY + 2; n++)
            {
                if (Main.tile[m, n].type == type && Main.tile[m, n].active())
                {
                    WorldGen.KillTile(m, n);
                }
            }
        }

        using (new Item.DisableNewItemMethod(!drop))
        {
            pot.SpawnGore(ctx);

            if (Main.netMode != NetmodeID.MultiplayerClient && pot.ShouldTryForLoot(ctx))
            {
                SpawnThingsFromPot_HandleVanillaAndModdedStyles(i, j, startX, startY, pot, style);
            }
        }

        WorldGen.destroyObject = false;
    }

    private static void SpawnThingsFromPot_HandleVanillaAndModdedStyles(
        int i,
        int j,
        int x2,
        int y2,
        PotBehavior potBehavior,
        int style
    )
    {
        if (WorldGen.gen)
        {
            return;
        }

        var aboveRockLayer = j < Main.rockLayer;
        var aboveUnderworldLayer = j < Main.UnderworldLayer;

        if (Main.remixWorld)
        {
            aboveRockLayer = j > Main.rockLayer && j < Main.UnderworldLayer;
            aboveUnderworldLayer = j > Main.worldSurface && j < Main.rockLayer;
        }

        var ctx = new PotLootContext(i, j, x2, y2, style, aboveRockLayer, aboveUnderworldLayer);

        var coinMult = potBehavior.GetInitialCoinMult(ctx);
        {
            coinMult = ((coinMult * 2f) + 1f) / 3f;
        }

        var ctxWithCoinValue = new PotLootContextWithCoinMult(
            ctx.X,
            ctx.Y,
            ctx.X2,
            ctx.Y2,
            ctx.Style,
            ctx.AboveRockLayer,
            ctx.AboveUnderworldLayer,
            coinMult
        );

        if (potBehavior.ShouldSpawnCoinPortal(ctxWithCoinValue))
        {
            potBehavior.SpawnCoinPortal(ctxWithCoinValue);
            return;
        }

        if (potBehavior.ShouldSpawnGoldenKey(ctxWithCoinValue))
        {
            potBehavior.SpawnGoldenKey(ctxWithCoinValue);
            return;
        }

        if (potBehavior.ShouldSpawnLitBomb(ctxWithCoinValue))
        {
            potBehavior.SpawnLitBomb(ctxWithCoinValue);
            return;
        }

        if (potBehavior.ShouldSpawnDontDigUpStar(ctxWithCoinValue))
        {
            potBehavior.SpawnDontDigUpStar(ctxWithCoinValue);
            return;
        }

        if (potBehavior.ShouldSpawnDontDigUpRope(ctxWithCoinValue))
        {
            potBehavior.SpawnDontDigUpRope(ctxWithCoinValue);
            return;
        }

        if (potBehavior.ShouldSpawnPotion(ctxWithCoinValue))
        {
            var potions = potBehavior.GetPotions(ctxWithCoinValue);
            potBehavior.SpawnPotions(ctxWithCoinValue, potions);
            return;
        }

        if (potBehavior.ShouldSpawnWormholePotion(ctxWithCoinValue))
        {
            potBehavior.SpawnWormholePotion(ctxWithCoinValue);
            return;
        }

        // Named after the `S` notation on the wiki:
        // https://terraria.wiki.gg/wiki/Pot
        var sChoice = Main.rand.Next(7);
        if (Main.expertMode)
        {
            sChoice--;
        }

        const int torch_threshold = 20;

        var player = Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)];

        var torchCount = 0;
        for (var invSlot = 0; invSlot < 50; invSlot++)
        {
            var item = player.inventory[invSlot];

            if (item.createTile <= -1 || item.IsAir || !TileID.Sets.Torch[item.createTile])
            {
                continue;
            }

            torchCount += item.stack;

            if (torchCount >= torch_threshold)
            {
                break;
            }
        }

        if (sChoice == 0 && player.statLife < player.statLifeMax2)
        {
            potBehavior.SpawnHearts(ctxWithCoinValue);
            return;
        }

        var secondChanceAtTorches = torchCount < torch_threshold;
        if (sChoice == 1 || (sChoice == 0 && secondChanceAtTorches))
        {
            potBehavior.SpawnTorches(ctxWithCoinValue, player);
            return;
        }

        switch (sChoice)
        {
            case 2:
            {
                potBehavior.SpawnAmmo(ctxWithCoinValue);
                return;
            }

            case 3:
            {
                potBehavior.SpawnHealingPotions(ctxWithCoinValue);
                return;
            }

            case 4:
                potBehavior.SpawnUtilityItems(ctxWithCoinValue);
                return;
        }

        if (sChoice is 4 or 5 && j < Main.UnderworldLayer && !Main.hardMode)
        {
            potBehavior.SpawnRopes(ctxWithCoinValue);
            return;
        }

        float coinAmount = 200 + WorldGen.genRand.Next(-100, 101);

        if (j < Main.worldSurface)
        {
            coinAmount *= 0.5f;
        }
        else if (aboveRockLayer)
        {
            coinAmount *= 0.75f;
        }
        else if (j > Main.maxTilesY - 250)
        {
            coinAmount *= 1.25f;
        }

        coinAmount *= 1f + (Main.rand.Next(-20, 21) * 0.01f);

        if (Main.rand.NextBool(4))
        {
            coinAmount *= 1f + (Main.rand.Next(5, 11) * 0.01f);
        }

        if (Main.rand.NextBool(8))
        {
            coinAmount *= 1f + (Main.rand.Next(10, 21) * 0.01f);
        }

        if (Main.rand.NextBool(12))
        {
            coinAmount *= 1f + (Main.rand.Next(20, 41) * 0.01f);
        }

        if (Main.rand.NextBool(16))
        {
            coinAmount *= 1f + (Main.rand.Next(40, 81) * 0.01f);
        }

        if (Main.rand.NextBool(20))
        {
            coinAmount *= 1f + (Main.rand.Next(50, 101) * 0.01f);
        }

        if (Main.expertMode)
        {
            coinAmount *= 2.5f;
        }

        if (Main.expertMode && Main.rand.NextBool(2))
        {
            coinAmount *= 1.25f;
        }

        if (Main.expertMode && Main.rand.NextBool(3))
        {
            coinAmount *= 1.5f;
        }

        if (Main.expertMode && Main.rand.NextBool(4))
        {
            coinAmount *= 1.75f;
        }

        coinAmount *= coinMult;

        if (NPC.downedBoss1)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedBoss2)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedBoss3)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedMechBoss1)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedMechBoss2)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedMechBoss3)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedPlantBoss)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedQueenBee)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedGolemBoss)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedPirates)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedGoblins)
        {
            coinAmount *= 1.1f;
        }

        if (NPC.downedFrost)
        {
            coinAmount *= 1.1f;
        }

        // TODO: Maybe a final hook around here to modify the coin amount.

        while ((int)coinAmount > 0)
        {
            switch (coinAmount)
            {
                case > 1000000f:
                {
                    var platCount = (int)(coinAmount / 1000000f);
                    if (platCount > 50 && Main.rand.NextBool(2))
                    {
                        platCount /= Main.rand.Next(3) + 1;
                    }

                    if (Main.rand.NextBool(2))
                    {
                        platCount /= Main.rand.Next(3) + 1;
                    }

                    coinAmount -= 1000000 * platCount;
                    Item.NewItem(
                        WorldGen.GetItemSource_FromTileBreak(i, j),
                        i * 16,
                        j * 16,
                        16,
                        16,
                        74,
                        platCount
                    );
                    continue;
                }

                case > 10000f:
                {
                    var goldCount = (int)(coinAmount / 10000f);
                    if (goldCount > 50 && Main.rand.NextBool(2))
                    {
                        goldCount /= Main.rand.Next(3) + 1;
                    }

                    if (Main.rand.NextBool(2))
                    {
                        goldCount /= Main.rand.Next(3) + 1;
                    }

                    coinAmount -= 10000 * goldCount;
                    Item.NewItem(
                        WorldGen.GetItemSource_FromTileBreak(i, j),
                        i * 16,
                        j * 16,
                        16,
                        16,
                        73,
                        goldCount
                    );
                    continue;
                }

                case > 100f:
                {
                    var silverCount = (int)(coinAmount / 100f);
                    if (silverCount > 50 && Main.rand.NextBool(2))
                    {
                        silverCount /= Main.rand.Next(3) + 1;
                    }

                    if (Main.rand.NextBool(2))
                    {
                        silverCount /= Main.rand.Next(3) + 1;
                    }

                    coinAmount -= 100 * silverCount;
                    Item.NewItem(
                        WorldGen.GetItemSource_FromTileBreak(i, j),
                        i * 16,
                        j * 16,
                        16,
                        16,
                        72,
                        silverCount
                    );
                    continue;
                }
            }

            var copperCount = (int)coinAmount;
            if (copperCount > 50 && Main.rand.NextBool(2))
            {
                copperCount /= Main.rand.Next(3) + 1;
            }

            if (Main.rand.NextBool(2))
            {
                copperCount /= Main.rand.Next(4) + 1;
            }

            if (copperCount < 1)
            {
                copperCount = 1;
            }

            coinAmount -= copperCount;
            Item.NewItem(
                WorldGen.GetItemSource_FromTileBreak(i, j),
                i * 16,
                j * 16,
                16,
                16,
                71,
                copperCount
            );
        }
    }
}

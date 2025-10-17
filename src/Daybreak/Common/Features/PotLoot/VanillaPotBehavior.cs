using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

namespace Daybreak.Common.Features.PotLoot;

/// <summary>
///     Provides implementations for vanilla pot types.
/// </summary>
internal sealed class VanillaPotBehavior(bool echo) : PotBehavior
{
    internal override void PlayBreakSound(PotBreakContext ctx)
    {
        base.PlayBreakSound(ctx);

        var i = ctx.X;
        var j = ctx.Y;

        switch (ctx.Style)
        {
            case >= 7 and <= 9:
                SoundEngine.PlaySound(SoundID.Grass, i * 16, j * 16);
                break;

            case >= 16 and <= 24:
                SoundEngine.PlaySound(LegacySoundIDs.NPCKilled, i * 16, j * 16);
                break;

            default:
                SoundEngine.PlaySound(SoundID.Shatter, i * 16, j * 16);
                break;
        }
    }

    public override void SpawnGore(PotBreakContext ctx)
    {
        var i = ctx.X;
        var j = ctx.Y;
        var style = ctx.Style;

        switch (style)
        {
            case 0:
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 51);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 52);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 53);
                break;

            case 1:
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 166);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 167);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 168);
                break;

            case 2:
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 169);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 170);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 171);
                break;

            case 3:
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 172);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 173);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 174);
                break;

            case 4:
            case 5:
            case 6:
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 197);
                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 198);
                break;

            default:
                if (style is >= 7 and <= 9)
                {
                    Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 199);
                    Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 200);
                }
                else if (style is >= 10 and <= 12)
                {
                    Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 201);
                    Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 202);
                }
                else if (style is >= 13 and <= 15)
                {
                    Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 203);
                    Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 204);
                }
                else
                {
                    if (style is >= 16 and <= 18 or >= 19 and <= 21 or >= 22 and <= 24)
                    {
                        break;
                    }

                    switch (style)
                    {
                        case >= 25 and <= 27:
                            Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), WorldGen.genRand.Next(217, 220));
                            Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), WorldGen.genRand.Next(217, 220));
                            break;

                        case >= 28 and <= 30:
                            Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), WorldGen.genRand.Next(315, 317));
                            Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), WorldGen.genRand.Next(315, 317));
                            break;

                        case >= 31 and <= 33:
                        {
                            var num6 = WorldGen.genRand.Next(2, 5);
                            for (var num7 = 0; num7 < num6; num7++)
                            {
                                Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 698 + WorldGen.genRand.Next(6));
                            }

                            break;
                        }

                        case >= 34 and <= 36:
                            Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 1122);
                            Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 1123);
                            Gore.NewGore(new Vector2(i * 16, j * 16), default(Vector2), 1124);
                            break;
                    }
                }

                break;
        }
    }

    public override bool ShouldTryForLoot(PotBreakContext ctx)
    {
        return !echo;
    }

    public override float GetInitialCoinMult(PotLootContext ctx)
    {
        var multiplier = 1f;

        switch (ctx.Style)
        {
            case 4:
            case 5:
            case 6:
                multiplier = 1.25f;
                break;

            default:
                switch (ctx.Style)
                {
                    case >= 7 and <= 9:
                        multiplier = 1.75f;
                        break;

                    case >= 10 and <= 12:
                        multiplier = 1.9f;
                        break;

                    case >= 13 and <= 15:
                        multiplier = 2.1f;
                        break;

                    case >= 16 and <= 18:
                        multiplier = 1.6f;
                        break;

                    case >= 19 and <= 21:
                        multiplier = 3.5f;
                        break;

                    case >= 22 and <= 24:
                        multiplier = 1.6f;
                        break;

                    case >= 25 and <= 27:
                        multiplier = 10f;
                        break;

                    case >= 28 and <= 30:
                    {
                        if (Main.hardMode)
                        {
                            multiplier = 4f;
                        }

                        break;
                    }

                    case >= 31 and <= 33:
                        multiplier = 2f;
                        break;

                    case >= 34 and <= 36:
                        multiplier = 1.25f;
                        break;
                }

                break;

            case 0:
            case 1:
            case 2:
            case 3:
                break;
        }

        return multiplier;
    }

    protected override void ModifyTorchType(
        PotLootContextWithCoinMult ctx,
        Player player,
        ref int torchType,
        ref int glowstickType,
        ref int itemStack
    )
    {
        var style = ctx.Style;

        if (player.ZoneHallow)
        {
            itemStack += Main.rand.Next(2, 7);
            torchType = 4387;
        }
        else if (style is >= 22 and <= 24 || player.ZoneCrimson)
        {
            itemStack += Main.rand.Next(2, 7);
            torchType = 4386;
        }
        else if (style is >= 16 and <= 18 || player.ZoneCorrupt)
        {
            itemStack += Main.rand.Next(2, 7);
            torchType = 4385;
        }
        else if (style is >= 7 and <= 9)
        {
            itemStack += Main.rand.Next(2, 7);
            itemStack = (int)(itemStack * 1.5f);
            torchType = 4388;
        }
        else if (style is >= 4 and <= 6)
        {
            torchType = 974;
            glowstickType = 286;
        }
        else if (style is >= 34 and <= 36)
        {
            itemStack += Main.rand.Next(2, 7);
            torchType = 4383;
        }
        else if (player.ZoneGlowshroom)
        {
            itemStack += Main.rand.Next(2, 7);
            torchType = 5293;
        }
    }

    protected override bool TryGetUtilityItem(
        PotLootContextWithCoinMult ctx,
        out int utilityType,
        out int utilityStack
    )
    {
        var isUndergroundDesertPot = ctx.Style is >= 34 and <= 36;
        if (!isUndergroundDesertPot && !ctx.AboveUnderworldLayer)
        {
            utilityType = 0;
            utilityStack = 0;
            return false;
        }

        utilityType = 166;
        if (isUndergroundDesertPot)
        {
            utilityType = 4423;
        }

        utilityStack = Main.rand.Next(4) + 1;
        if (Main.expertMode)
        {
            utilityStack += Main.rand.Next(4);
        }

        return true;
    }
}

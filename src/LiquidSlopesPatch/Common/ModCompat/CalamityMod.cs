using System;
using System.Reflection;
using CalamityMod.ILEditing;
using CalamityMod.Waters;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace LiquidSlopesPatch.Common.ModCompat;

[ExtendsFromMod("CalamityMod")]
internal sealed class CalamityMod : ModSystem
{
    [NoJIT]
    private static class CalamityModPreSso
    {
        public static void Load()
        {
            var ilc = typeof(ILChanges);

            MonoModHooks.Add(ilc.GetMethod(nameof(ILChanges.ChangeWaterQuadColors), BindingFlags.NonPublic | BindingFlags.Static), ChangeWaterQuadColors);
        }

        private static void ChangeWaterQuadColors(ILContext il)
        {
            // no-op

            var cursor = new ILCursor(il);

            if (!cursor.TryGotoNext(c => c.MatchLdfld<LiquidRenderer>("_liquidTextures")))
            {
                ILChanges.LogFailure("Custom Lava Drawing", "Could not locate the liquid texture array load.");
                return;
            }

            // Move to the end of the get_Value() call and then use the resulting texture to check if a new one should replace it.
            // Adding to the index directly would seem like a simple, direct way of achieving this since the operation is incredibly light, but
            // it also unsafe due to the potential for NOP operations to appear.
            if (!cursor.TryGotoNext(MoveType.After, c => c.MatchCallvirt(ILChanges.textureGetValueMethod)))
            {
                ILChanges.LogFailure("Custom Lava Drawing", "Could not locate the liquid texture Value call.");
                return;
            }

            cursor.EmitDelegate<Func<Texture2D, Texture2D>>(initialTexture => ILChanges.SelectLavaTexture(initialTexture, LiquidTileType.Waterflow));

            if (!cursor.TryGotoNext(MoveType.After, c => c.MatchLdloc(11)))
            {
                ILChanges.LogFailure("Custom Lava Drawing", "Could not locate the liquid light color.");
                return;
            }

            // Pass the texture in so that the method can ensure it is not messing around with non-lava textures.
            cursor.Emit(OpCodes.Ldarg_0);
            cursor.Emit(OpCodes.Ldfld, typeof(LiquidRenderer).GetField("_liquidTextures"));
            cursor.Emit(OpCodes.Ldloc, 8);
            cursor.Emit(OpCodes.Ldelem_Ref);
            cursor.Emit(OpCodes.Ldloc, 8);
            cursor.Emit(OpCodes.Ldloc, 9);
            cursor.Emit(OpCodes.Ldloc, 10);

            // Caching these values can save a LOT of overhead at runtime.
            ModWaterStyle sunkenSeaWater = ModContent.GetInstance<SunkenSeaWater>();
            ModWaterStyle sulphuricWater = ModContent.GetInstance<SulphuricWater>();
            ModWaterStyle sulphuricDepthsWater = ModContent.GetInstance<SulphuricDepthsWater>();
            ModWaterStyle upperAbyssWater = ModContent.GetInstance<UpperAbyssWater>();
            ModWaterStyle middleAbyssWater = ModContent.GetInstance<MiddleAbyssWater>();
            ModWaterStyle voidWater = ModContent.GetInstance<VoidWater>();

            cursor.EmitDelegate<Func<VertexColors, Texture2D, int, int, int, VertexColors>>(
                (initialColor, initialTexture, liquidType, x, y) =>
                {
                    // Don't bother changing the color if the cached drawing style is null.
                    if (ILChanges.cachedLavaStyle != default(object))
                    {
                        initialColor = ILChanges.SelectLavaQuadColor(initialTexture, ref initialColor, liquidType == 1);
                    }

                    if (liquidType == sunkenSeaWater.Slot ||
                        liquidType == sulphuricWater.Slot ||
                        liquidType == sulphuricDepthsWater.Slot ||
                        liquidType == upperAbyssWater.Slot ||
                        liquidType == middleAbyssWater.Slot ||
                        liquidType == voidWater.Slot)
                    {
                        ILChanges.SelectSulphuricWaterColor(x, y, ref initialColor);
                    }

                    // Apply any extra color conditions.
                    // TODO: unsued?
                    // initialColor = ILChanges.ExtraColorChangeConditions?.Invoke(initialColor, liquidType, new(x, y)) ?? initialColor;

                    return initialColor;
                }
            );
        }
    }
    /*[NoJIT]
    private static class CalamityModPostSso
    {
        public static void Load()
        {
            var ilc = typeof(ILChanges);

            // MonoModHooks.Add(ilc.GetMethod(LavaBubbleReplacer, BindingFlags.NonPublic | BindingFlags.Static), LavaBubbleReplacer);
        }
    }*/

    public override void Load()
    {
        base.Load();

        if (ModLoader.GetMod("CalamityMod").Version >= new Version(2, 1))
        {
            // CalamityModPostSso.Load();
        }
        else
        {
            CalamityModPreSso.Load();
        }
    }
}

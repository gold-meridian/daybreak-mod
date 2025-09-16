using System.Reflection;

using MonoMod.Cil;

using Spooky.Content.Biomes;
using Spooky.Content.Tiles.Water;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LiquidSlopesPatch.Common.ModCompat;

[ExtendsFromMod("Spooky")]
internal sealed class SpookyMod : ModSystem
{
    public override void Load()
    {
        base.Load();

        var tpb = typeof(TarPitsBiome);

        MonoModHooks.Add(tpb.GetMethod(nameof(TarPitsBiome.WaterOpacityChanger), BindingFlags.NonPublic | BindingFlags.Instance), WaterOpacityChanger_FixStructTypes);
    }

    private static void WaterOpacityChanger_FixStructTypes(TarPitsBiome self, ILContext il)
    {
        ILCursor c = new(il);
        c.GotoNext(MoveType.After, i => i.MatchMul(), i => i.MatchStloc(7)); //match to saving of num at the line float num = ptr2->Opacity * (isBackgroundDraw ? 1f : DEFAULT_OPACITY[ptr2->Type]);
        c.EmitLdloca(7);
        //parse through num with a reference through the delegate
        //Ldloc2 or ptr2 is a pointer, (pointers are just accesses to fields through memory) which means that we can't parse them through a delegate by themselves
        //Here we parse through the pointer (ptr2) value for Type and Opacity since thats the only LiquidDrawCache values we use

        c.EmitLdloc2();
        c.EmitLdfld(typeof(RewrittenLiquidRenderer.LiquidDrawCache).GetField("Opacity")); //we get ptr2.Opacity by parsing throgh both ptr2 and the Opacity field
        c.EmitLdloc2();
        c.EmitLdfld(typeof(RewrittenLiquidRenderer.LiquidDrawCache).GetField("Type")); //we get ptr2.Opacity by parsing throgh both ptr2 and the Type field
        c.EmitLdarg(5);
        c.EmitDelegate((ref float num, float ptr2Opacity, byte ptr2Type, bool isBackgroundDraw) =>
            {
                //Anything placed in this delegate is like calling a new method 
                float LiquidOpacity = self.WaterOpacity; //ranges from 1f to 0f
                bool opacityCondition = ptr2Type == LiquidID.Water && Main.waterStyle == ModContent.GetInstance<TarWaterStyle>().Slot;
                //the condition for when our opacity should be applied
                //This gets the liquid type water and gets the water style for our liquid, this can be changed to anything boolean related
                //We set num (or the opacity of the draw liquid) to either the original value or our value depending on the condition above

                num = opacityCondition ? ptr2Opacity * (isBackgroundDraw ? 1f : LiquidOpacity) : num;
            }
        );
    }
}
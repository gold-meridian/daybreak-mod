using Microsoft.Xna.Framework.Graphics;

using Terraria.GameContent.Liquid;
using Terraria.Graphics;
using Terraria.ModLoader;

namespace LiquidSlopesPatch.Common;

public partial class RewrittenLiquidRenderer : ModSystem
{
    public override void Load()
    {
        base.Load();

        On_LiquidRenderer.DrawNormalLiquids += (orig, self, batch, offset, style, alpha, draw) => { DrawNormalLiquids(batch, offset, style, alpha, draw); };
        On_LiquidRenderer.DrawShimmer += (orig, self, batch, offset, draw) => { DrawShimmer(batch, offset, draw); };
        On_LiquidRenderer.GetCachedDrawArea += (orig, self) => GetCachedDrawArea();
        On_LiquidRenderer.GetShimmerBaseColor += (orig, x, y) => GetShimmerBaseColor(x, y);
        On_LiquidRenderer.GetShimmerFrame += (orig, self, top, x, y) => GetShimmerFrame(top, x, y);
        On_LiquidRenderer.GetShimmerGlitterColor += (orig, top, x, y) => GetShimmerGlitterColor(top, x, y);
        On_LiquidRenderer.GetShimmerGlitterOpacity += (orig, top, x, y) => GetShimmerGlitterOpacity(top, x, y);
        On_LiquidRenderer.GetShimmerWave += (On_LiquidRenderer.orig_GetShimmerWave orig, ref float x, ref float y) => GetShimmerWave(ref x, ref y);
        On_LiquidRenderer.GetVisibleLiquid += (orig, self, i, i1) => GetVisibleLiquid(i, i1);
        On_LiquidRenderer.HasFullWater += (orig, self, i, i1) => HasFullWater(i, i1);
        On_LiquidRenderer.InternalPrepareDraw += (orig, self, area) => { InternalPrepareDraw(area); };
        // On_LiquidRenderer.LoadContent += orig => { LoadContent(); };
        // On_LiquidRenderer.PrepareAssets += (orig, self) => { PrepareAssets(); };
        On_LiquidRenderer.PrepareDraw += (orig, self, area) => { PrepareDraw(area); };
        On_LiquidRenderer.SetShimmerVertexColors += (On_LiquidRenderer.orig_SetShimmerVertexColors orig, ref VertexColors colors, float opacity, int i, int i1) => { SetShimmerVertexColors(ref colors, opacity, i, i1); };
        On_LiquidRenderer.SetShimmerVertexColors_Sparkle += (On_LiquidRenderer.orig_SetShimmerVertexColors_Sparkle orig, ref VertexColors colors, float opacity, int i, int i1, bool top) => SetShimmerVertexColors_Sparkle(ref colors, opacity, i, i1, top);
        On_LiquidRenderer.SetWaveMaskData += (On_LiquidRenderer.orig_SetWaveMaskData orig, LiquidRenderer self, ref Texture2D texture) => { SetWaveMaskData(ref texture); };
        On_LiquidRenderer.SimpleWhiteNoise += (orig, u, u1) => SimpleWhiteNoise(u, u1);
        On_LiquidRenderer.Update += (orig, self, time) => { Update(time); };
    }
}
using JetBrains.Annotations;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using Daybreak.Rendering.V1;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Daybreak.Hooks.V1;

file delegate void RenderLayerDefinition(
    [Omittable] SpriteBatch sb,
    [Omittable] GraphicsDevice device
);

/// <summary>
///     Automatically calls the decorated function before the associated <see cref="Overlay"/>s at the given layer.
/// </summary>
/// <param name="layer"></param>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
[HookMetadata(DelegateType = typeof(RenderLayerDefinition))]
public sealed class RenderLayerAttribute(RenderLayers layer) : BaseHookAttribute
{
    /// <inheritdoc />
    public override void Apply(MethodInfo bindingMethod, object? instance)
    {
        var method = HookSubscriber.BuildWrapper<RenderLayerDefinition>(bindingMethod, instance);

        RenderLayerRenderer.LAYERS.TryAdd(layer, []);
        RenderLayerRenderer.LAYERS[layer].Add(method);
    }
}

file static class RenderLayerRenderer
{
    public static readonly Dictionary<RenderLayers, List<RenderLayerDefinition>> LAYERS = [];

    [OnLoad(Side = ModSide.Client)]
    private static void Load()
    {
        On_OverlayManager.Draw += Draw_RenderLayers;
        IL_Main.DoDraw += _ => { };
        IL_Main.DoDraw_WallsAndBlacks += _ => { };
    }

    private static void Draw_RenderLayers(On_OverlayManager.orig_Draw orig, OverlayManager self, SpriteBatch sb, RenderLayers layer, bool beginSpriteBatch)
    {
        using (sb.Scope())
        {
            if (LAYERS.TryGetValue(layer, out var layers))
            {
                foreach (var renderLayer in layers)
                {
                    renderLayer(sb, Main.graphics.GraphicsDevice);
                }
            }
        }

        orig(self, sb, layer, beginSpriteBatch);
    }
}

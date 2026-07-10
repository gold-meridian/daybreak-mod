using System.Collections.Generic;
using System.Diagnostics;
using Daybreak.EarlyLoader;
using Daybreak.Hooks;
using Daybreak.Rendering.Buffers;
using Daybreak.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Rendering.ItemBuffering;

/// <summary>
///     Determines whether a re-render of the cached item texture is required.
/// </summary>
public delegate bool ShouldRenderItemDelegate(
    int itemType,
    Asset<Texture2D> originalAsset,
    Texture2D? renderedTexture
);

/// <summary>
///     Renders and caches the item texture.
/// </summary>
public delegate void RenderItemDelegate(
    SpriteBatch spriteBatch,
    int itemType,
    Asset<Texture2D> originalAsset
);

/// <summary>
///     Responsible for rendering cacheable item texture overrides.
/// </summary>
[Autoload(Side = ModSide.Client)]
public static class ItemTextureRenderer
{
    /// <summary>
    ///     Configurable rendering callbacks for cached texture overrides.
    /// </summary>
    public sealed record RenderSettings(
        ShouldRenderItemDelegate ShouldRender,
        RenderItemDelegate Render
    );

    private static readonly Dictionary<int, Asset<Texture2D>> original_assets = [];
    private static readonly Dictionary<int, RenderTarget2D> render_targets = [];
    private static readonly HashSet<int> has_rendered = [];

    private static bool unloading;

    [OnLoad]
    private static void Load()
    {
        On_Main.DoDraw += UpdateItemRenders;

        EarlyLoadHooks.OnEarlyModUnload += _ =>
        {
            unloading = true;
        };
    }

    [OnUnload]
    private static void Unload()
    {
        foreach (var (itemType, asset) in original_assets)
        {
            TextureAssets.Item[itemType] = asset;
        }

        foreach (var (_, rt) in render_targets)
        {
            rt.Dispose();
        }

        original_assets.Clear();
        render_targets.Clear();
    }

    [ModSystemHooks.PostSetupContent]
    private static void PostSetupContent()
    {
        for (var i = 0; i < ItemLoader.ItemCount; i++)
        {
            if (ItemID.Sets.RenderSettings[i] is not null)
            {
                continue;
            }

            if (ItemLoader.GetItem(i) is not { } modItem)
            {
                continue;
            }

#pragma warning disable CS0618 // Type or member is obsolete
            // ReSharper disable SuspiciousTypeConversion.Global
            if (modItem is IBufferedItemTexture buffered)
            {
                ItemID.Sets.RenderSettings[i] = new RenderSettings(
                    buffered.ShouldRenderCachedTexture,
                    buffered.RenderCachedTexture
                );
            }
            else if (modItem is IPreRenderedItem preRendered)
            {
                ItemID.Sets.RenderSettings[i] = new RenderSettings(
                    (_, _, _) => true,
                    (sb, _, asset) =>
                    {
                        sb.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
                        {
                            preRendered.PreRender(asset.ImmediateValue);
                        }
                        sb.End();
                    }
                );
            }
            // ReSharper restore SuspiciousTypeConversion.Global
#pragma warning restore CS0618 // Type or member is obsolete
        }

        Main.RunOnMainThread(
            () =>
            {
                for (var i = 0; i < ItemLoader.ItemCount; i++)
                {
                    if (ItemID.Sets.RenderSettings[i] is null)
                    {
                        continue;
                    }

                    var asset = original_assets[i] = TextureAssets.Item[i];
                    var texture = asset.ImmediateValue;
                    var renderTarget = new RenderTarget2D(
                        Graphics.Device,
                        texture.Width,
                        texture.Height
                    );

                    render_targets[i] = renderTarget;
                    TextureAssets.Item[i] = UntrackedAssetProvider.CreateUntracked<Texture2D>(renderTarget, texture.Name);
                }
            }
        );
    }

    [StackTraceHidden]
    private static void UpdateItemRenders(On_Main.orig_DoDraw orig, Main self, GameTime gameTime)
    {
        if (!unloading)
        {
            PopulateTargets();
        }

        orig(self, gameTime);
    }

    private static void PopulateTargets()
    {
        for (var i = 0; i < ItemLoader.ItemCount; i++)
        {
            if (ItemID.Sets.RenderSettings[i] is not { } settings)
            {
                continue;
            }

            var originalAsset = original_assets[i];
            var renderTarget = render_targets[i];

            if (!settings.ShouldRender.Invoke(i, originalAsset, has_rendered.Contains(i) ? renderTarget : null))
            {
                continue;
            }

            has_rendered.Add(i);

            using (render_targets[i].Scope(clearColor: Color.Transparent))
            {
                settings.Render(Main.spriteBatch, i, originalAsset);
            }
        }
    }
}

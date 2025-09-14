using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using Daybreak.Common.IDs;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Rendering;

/// <summary>
///     Implementing this interface allows the item to pre-render a texture for
///     the frame which all item rendering will use instead of the original
///     texture.
/// </summary>
public interface IPreRenderedItem
{
    /// <summary>
    ///     Renders the item's texture for use for the current frame.
    /// </summary>
    /// <param name="sourceTexture">The actual texture of the item.</param>
    void PreRender(Texture2D sourceTexture);
}

/// <summary>
///     Pre-renders items each frame.  Useful for items which have shaders
///     applied for visual effects that should also apply in contexts such as
///     item use and hover.
///     <br />
///     Best if the intent is essentially to create a procedural animation that
///     may be used for all instances of the item without variation.
/// </summary>
[Autoload(Side = ModSide.Client)]
public sealed class ItemPreRenderer : ModSystem
{
    private static readonly Dictionary<int, Texture2D> original_textures = [];
    private static readonly Dictionary<int, IPreRenderedItem> pre_rendered_items = [];
    private static readonly Dictionary<int, RenderTarget2D> render_targets = [];

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        Main.RunOnMainThread(() =>
            {
                On_Main.DoDraw += UpdateItemRenders;
            }
        );
    }

    /// <inheritdoc />
    public override void Unload()
    {
        base.Unload();

        Main.RunOnMainThread(() =>
            {
                foreach (var (itemType, texture) in original_textures)
                {
                    TextureAssets.Item[itemType].ownValue = texture;
                }

                foreach (var (_, rt) in render_targets)
                {
                    rt.Dispose();
                }

                original_textures.Clear();
                pre_rendered_items.Clear();
                render_targets.Clear();
            }
        );
    }

    /// <inheritdoc />
    public override void PostSetupContent()
    {
        base.PostSetupContent();

        for (var i = 0; i < ItemLoader.ItemCount; i++)
        {
            if (!TryGetPreRenderedItem(i, out var preRenderedItem))
            {
                continue;
            }

            pre_rendered_items[i] = preRenderedItem;

            Main.instance.LoadItem(i);
            original_textures[i] = TextureAssets.Item[i].Value;
        }

        Main.RunOnMainThread(() =>
            {
                foreach (var (itemType, _) in pre_rendered_items)
                {
                    var originalTexture = original_textures[itemType];
                    var renderTarget = new RenderTarget2D(
                        Main.graphics.graphicsDevice,
                        originalTexture.Width,
                        originalTexture.Height
                    );

                    render_targets[itemType] = renderTarget;
                    TextureAssets.Item[itemType].ownValue = renderTarget;
                }
            }
        );
    }

    private static void UpdateItemRenders(On_Main.orig_DoDraw orig, Main self, GameTime gameTime)
    {
        foreach (var (itemType, preRenderedItem) in pre_rendered_items)
        {
            if (!render_targets.ContainsKey(itemType))
            {
                continue;
            }

            var originalTexture = original_textures[itemType];
            var renderTarget = render_targets[itemType];

            Main.graphics.GraphicsDevice.SetRenderTarget(renderTarget);
            Main.graphics.GraphicsDevice.Clear(Color.Transparent);

            Main.spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null);
            preRenderedItem.PreRender(originalTexture);
            Main.spriteBatch.End();

            Main.graphics.GraphicsDevice.SetRenderTarget(null);
        }

        orig(self, gameTime);
    }

    private static bool TryGetPreRenderedItem(
        int itemType,
        [NotNullWhen(returnValue: true)] out IPreRenderedItem? item
    )
    {
        if (ItemLoader.GetItem(itemType) is IPreRenderedItem preRenderedItem)
        {
            item = preRenderedItem;
            return true;
        }

        if (DaybreakItemSets.PreRenderedItems[itemType] is { } polyfillItem)
        {
            item = polyfillItem;
            return true;
        }

        item = null;
        return false;
    }
}
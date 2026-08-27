using Daybreak.Hooks;
using Terraria.ID;

namespace Daybreak.Rendering.ItemBuffering.V1;

/// <summary>
///     Item sets for item rendering.
/// </summary>
public static class ItemSets
{
    private static ItemTextureRenderer.RenderSettings?[] renderSettings = [];

    [ModSystemHooks.ResizeArrays]
    private static void ResizeArrays()
    {
        renderSettings = ItemID.Sets.Factory.CreateNamedSet("RenderSettings")
                               .RegisterCustomSet(default(ItemTextureRenderer.RenderSettings));
    }

    extension(ItemID.Sets)
    {
        /// <summary>
        ///     Cached texture render settings for an item.
        /// </summary>
        public static ItemTextureRenderer.RenderSettings?[] RenderSettings => renderSettings;
    }
}

using System;

using JetBrains.Annotations;

using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.GameContent;

namespace Daybreak.Common.Assets;

// This API specifically does not mesh well with permanent modifications because
// they may be overridden by resource packs.

/// <summary>
///     Facilitates the dynamic and necessarily transient replacement of
///     <see cref="Asset{T}"/> values.
/// </summary>
/// <remarks>
///     <b>
///         If you are intending to permanently modify an asset for the length
///         of your mod's existence rather than in a specific or defined
///         context, either replace the <see cref="Asset{T}"/> instance directly
///         or use the Resource Pack API (TODO).
///     </b>
///     <br />
///     The provided APIs directly mutate the contents of an
///     <see cref="Asset{T}"/> rather than, say, the stored instances in
///     <see cref="TextureAssets"/>.  As a result, these replacements
///     unambiguously propagate throughout all contexts in which the asset may
///     be needed, but they made be superseded by direct changes to values in
///     <see cref="TextureAssets"/> by other mods.
/// </remarks>
[PublicAPI]
public static class AssetReplacer
{
    // Implementation detail for curious developers: the actual logic is indeed
    // performed in the handle, but this is not a contractual obligation of the
    // API.  All replacements should go through AssetReplacer::Replace<T>.
    /// <summary>
    ///     An asset replacement handle which, when disposed, restores the
    ///     replaced asset to its original value.
    /// </summary>
    /// <typeparam name="T">The asset type.</typeparam>
    public readonly record struct Handle<T> : IDisposable
        where T : class
    {
        private readonly IMutableAssetProvider<T> source;
        private readonly T original;

        internal Handle(IMutableAssetProvider<T> source, T target)
        {
            this.source = source;

            original = source.Asset;
            source.Asset = target;
        }

        /// <summary>
        ///     Disposes of the handle, restoring the original asset.
        /// </summary>
        public void Dispose()
        {
            source.Asset = original;
        }
    }

    /// <summary>
    ///     Replaces the given asset with the new value.
    /// </summary>
    /// <param name="oldAsset">The asset to replace.</param>
    /// <param name="newAsset">The new asset.</param>
    /// <typeparam name="T">The asset type.</typeparam>
    /// <returns>A handle to the asset replacement.</returns>
    public static Handle<T> Replace<T>(Asset<T> oldAsset, T newAsset)
        where T : class
    {
        return new Handle<T>(new ReLogicMutableAssetProvider<T>(oldAsset), newAsset);
    }

    // TODO: Add more of these APIs as they become relevant.  Users can fill
    // them in themselves if we haven't added them yet.

#region TextureAssets
    /// <summary>
    ///     <see cref="TextureAssets.Npc"/>
    /// </summary>
    public static Handle<Texture2D> Npc(int value, Texture2D newAsset)
    {
        Main.instance.LoadNPC(value); // Import to ensure the asset is loaded!
        return Replace(TextureAssets.Npc[value], newAsset);
    }

    /// <summary>
    ///     <see cref="TextureAssets.Extra"/>
    /// </summary>
    public static Handle<Texture2D> Extra(int value, Texture2D newAsset)
    {
        return Replace(TextureAssets.Extra[value], newAsset);
    }
#endregion
}
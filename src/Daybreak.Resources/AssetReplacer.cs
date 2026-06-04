using System;
using ReLogic.Content;

namespace Daybreak.Resources;

/// <summary>
///     Enables transient asset replacement.  Suitable for overriding the value
///     of an <see cref="Asset{T}"/> for the scope of a consuming API, but not
///     for arbitrary lengths of time past frame boundaries.
///     <br />
///     The asset replacement API provides a disposable handle which unapplies
///     the replacement at the end of its scope.
/// </summary>
public static class AssetReplacer
{
    /// <summary>
    ///     An asset replacement handle that, when disposed of, restores the
    ///     replaced asset to its original value.
    /// </summary>
    /// <typeparam name="T">The asset type.</typeparam>
    public readonly struct Handle<T> : IDisposable
        where T : class
    {
        private readonly T original;
        private readonly IMutableAssetProvider<T> source;

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
    ///     Replaces the given asset with the new value for the duration of the
    ///     returned scope.
    /// </summary>
    public static Handle<T> Replace<T>(this Asset<T> oldAsset, T newAsset)
        where T : class
    {
        // Always wait for loading assets to complete first, as their
        // continuations could complete arbitrarily and reset our overriden
        // value.
        if (oldAsset.State == AssetState.Loading)
        {
            oldAsset.Wait();
        }

        return new Handle<T>(new ReLogicMutableAssetProvider<T>(oldAsset), newAsset);
    }
}

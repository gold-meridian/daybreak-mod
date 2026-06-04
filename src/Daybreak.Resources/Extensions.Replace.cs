using System;
using ReLogic.Content;

namespace Daybreak.Resources;

/// <summary>
///     An asset replacement handle that, when disposed of, restores the
///     replaced asset to its original value.
/// </summary>
/// <typeparam name="T">The asset type.</typeparam>
public readonly struct AssetReplacementHandle<T> : IDisposable
    where T : class
{
    private readonly T original;
    private readonly IMutableAssetProvider<T> source;

    internal AssetReplacementHandle(IMutableAssetProvider<T> source, T target)
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

partial class Extensions
{
    extension<T>(Asset<T> asset)
        where T : class
    {
        /// <summary>
        ///     Replaces the given asset with the new value for the duration of
        ///     the returned scope.
        ///     <br />
        ///     <br />
        ///     This enables transient asset replacement that is suitable for
        ///     overriding the value of an <see cref="Asset{T}"/> within the
        ///     scope of the consuming API.  This is not suitable for replacing
        ///     assets for arbitrary lengths of time, such as past frame
        ///     boundaries.
        /// </summary>
        public AssetReplacementHandle<T> Replace(T newAsset)
        {
            // Always wait for loading assets to complete first, as their
            // continuations could complete arbitrarily and reset our overriden
            // value.
            if (asset.State == AssetState.Loading)
            {
                asset.Wait();
            }

            return new AssetReplacementHandle<T>(new ReLogicMutableAssetProvider<T>(asset), newAsset);
        }
    }
}

#region Asset provision
/// <summary>
///     Provides an API to get and set the value of an asset of type
///     <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The asset type.</typeparam>
internal interface IMutableAssetProvider<T>
    where T : class
{
    /// <summary>
    ///     The mutable asset.
    /// </summary>
    T Asset { get; set; }
}

internal readonly struct ReLogicMutableAssetProvider<T>(Asset<T> source) : IMutableAssetProvider<T>
    where T : class
{
    public T Asset
    {
        get => source.Value;
        set => source.ownValue = value;
    }
}
#endregion

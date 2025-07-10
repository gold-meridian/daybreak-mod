using JetBrains.Annotations;

using ReLogic.Content;

namespace Daybreak.Common.Assets;

/// <summary>
///     Provides an API to get and set the value of an asset of type
///     <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The asset type.</typeparam>
/// <remarks>
///     This may seem somewhat redundant but facilitates additional agnosticism
///     in the context in which an asset is provided for the
///     <see cref="AssetReplacer"/> API.
/// </remarks>
[PublicAPI]
public interface IMutableAssetProvider<T>
    where T : class
{
    /// <summary>
    ///     The mutable asset.
    /// </summary>
    T Asset { get; set; }
}

/// <summary>
///     Providers an API for mutating the value of an <see cref="Asset{T}"/>.
/// </summary>
/// <param name="Source">The <see cref="Asset{T}"/> to be mutated.</param>
/// <typeparam name="T">The type of the asset.</typeparam>
internal readonly record struct ReLogicMutableAssetProvider<T>(Asset<T> Source) : IMutableAssetProvider<T>
    where T : class
{
    public T Asset
    {
        get => Source.Value;
        set => Source.ownValue = value;
    }
}

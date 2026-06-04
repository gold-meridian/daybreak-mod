using ReLogic.Content;

namespace Daybreak.Resources;

/// <summary>
///     Provides an API to get and set the value of an asset of type
///     <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The asset type.</typeparam>
public interface IMutableAssetProvider<T>
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

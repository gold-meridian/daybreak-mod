using ReLogic.Content;

namespace Daybreak.Common.Assets;

/// <summary>
///     Manages initializing &quot;dummy&quot; <see cref="Asset{T}"/> references
///     which wrap a base value without being associated with a repository.
/// </summary>
public static class DummyAssetProvider
{
    /// <summary>
    ///     Initializes an <see cref="Asset{T}"/> with value
    ///     <paramref name="value"/> of type <typeparamref name="T"/>.  This
    ///     asset is disconnected from any asset repositories and content
    ///     sources and is fit to be used where none are required (generally any
    ///     case where the asset is just used as a strongbox for its value).
    /// </summary>
    /// <param name="value">The asset value.</param>
    /// <typeparam name="T">The asset type.</typeparam>
    /// <returns>The &quot;dummy&quot; instance.</returns>
    /// <remarks>
    ///     This object does not need to be disposed and generally shouldn't be.
    ///     <br />
    ///     It should remain safe to do so, however.
    /// </remarks>
    public static Asset<T> From<T>(T value)
        where T : class
    {
        return new Asset<T>(string.Empty)
        {
            State = AssetState.Loaded,
            IsDisposed = false,
            Source = null, // TODO: Populate with dummy value?
            ownValue = value,
            Continuation = () => { },
            Wait = () => { },
        };
    }
}

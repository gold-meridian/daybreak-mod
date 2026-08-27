using System.Runtime.CompilerServices;
using ReLogic.Content;

namespace Daybreak.Resources;

/// <summary>
///     Supplemental APIs for untracked assets.
/// </summary>
public static class UntrackedAssetProvider
{
    /// <summary>
    ///     Initializes an <see cref="Asset{T}"/> with value
    ///     <paramref name="value"/> and name <paramref name="name"/> of type
    ///     <typeparamref name="T"/>.  This asset is disconnected from any asset
    ///     repositories or content sources.
    ///     <br />
    ///     This API serves to provide a way to turn a raw asset value into the
    ///     wrapped <see cref="Asset{T}"/> type.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Asset<T> CreateUntracked<T>(T value, string name)
        where T : class
    {
        return new Asset<T>(name)
        {
            State = AssetState.Loaded,
            IsDisposed = false,
            Source = null, // This is fine to be null.
            ownValue = value,
            Continuation = NoOp,
            Wait = NoOp,
        };
    }

    // avoid allocations
    private static void NoOp() { }

    // An extension for asset repo instances to match the regular instanced
    // syntax.
    extension(AssetRepository _)
    {
        /// <inheritdoc cref="CreateUntracked{T}(T, string)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Asset<T> CreateUntracked<T>(T value, string name)
            where T : class
        {
            return CreateUntracked(value, name);
        }
    }
}

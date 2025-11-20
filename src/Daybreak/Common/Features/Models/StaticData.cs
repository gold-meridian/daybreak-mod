using System;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

/// <summary>
///     Manages access to static data.
/// </summary>
/// <remarks>
///     See <see cref="StaticData{TData}"/> for an explanation of this API.
/// </remarks>
public static class StaticData
{
    /// <summary>
    ///     Gets a static data instance, throwing if it is somehow
    ///     uninitialized.
    /// </summary>
    /// <typeparam name="TData">The data type.</typeparam>
    /// <returns>The data instance.</returns>
    /// <exception cref="InvalidOperationException">
    ///     The data is not initialized.
    /// </exception>
    /// <remarks>
    ///     See <see cref="StaticData{TData}"/> for an explanation of this API.
    /// </remarks>
    public static TData Get<TData>()
    {
        return StaticData<TData>.Data
            ?? throw new InvalidOperationException($"Attempted to get uninitialized StaticData: {typeof(TData)}");
    }
}

/// <summary>
///     A special kind of <see cref="InstanceData"/> in which the singleton
///     instance is expected to be the only instance, providing external data.
///     <br />
///     This model guarantees the provided data to be fully loaded and non-null.
///     <br />
///     Only one <see cref="StaticData{TData}"/> may exist for any
///     <typeparamref name="TData"/>.
/// </summary>
/// <typeparam name="TData">The external data to provide.</typeparam>
/// <remarks>
///     This is best used in situations where a loadable type definitely depends
///     on acquired resources or data which are initialized at load time,
///     thereby not automatically guaranting values are not nullable.
/// </remarks>
public abstract class StaticData<TData> : InstanceData
{
    /// <summary>
    ///     The data instance produced by this static data.
    /// </summary>
    internal static TData? Data
    {
        get;

        set
        {
            if (field is not null)
            {
                throw new InvalidOperationException($"Duplicate initialization of StaticData: {typeof(TData)} (do you have duplicate StaticData<{typeof(TData)}>s?)");
            }

            field = value;
        }
    }

    /// <summary>
    ///     
    /// </summary>
    /// <param name="mod">The mod this belongs to.</param>
    protected sealed override void LoadSingleton(Mod mod)
    {
        Data = LoadData(mod);
    }

    /// <summary>
    ///     Unloads the data owned by this type.
    /// </summary>
    protected sealed override void UnloadSingleton()
    {
        // Presumably never loaded in the first place, shouldn't generally be
        // possible but may occur on early unloads?  I don't think this is
        // possible since it wouldn't know to unload it if AddContent wasn't
        // first called, but better safe than sorry.
        if (Data is null)
        {
            return;
        }

        UnloadData(Data);
    }

    /// <summary>
    ///     Initializes the <typeparamref name="TData"/> instance.
    /// </summary>
    /// <param name="mod">The mod this belongs to.</param>
    /// <returns>The initialized <typeparamref name="TData"/> instance.</returns>
    protected abstract TData LoadData(Mod mod);

    /// <summary>
    ///     Responsible for uninitializing the <typeparamref name="TData"/>.
    ///     Expected to dispose of resouces, etc.
    /// </summary>
    /// <param name="data">The data to clean up/uninitialize.</param>
    protected abstract void UnloadData(TData data);
}

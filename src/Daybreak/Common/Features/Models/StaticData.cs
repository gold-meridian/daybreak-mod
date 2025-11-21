using System;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

/// <summary>
///     A special kind of data model in which a singleton instance is created on
///     load and creates a single real, non-null instance whose nullability
///     contracts must be fulfilled.
///     <br />
///     This model guarantees the provided data to be fully loaded and non-null.
///     <br />
///     Only one <see cref="IStatic{TData}"/> may exist for any
///     <typeparamref name="TData"/>.
/// </summary>
/// <typeparam name="TData">The external data to provide.</typeparam>
/// <remarks>
///     This is best used in situations where a loadable type definitely depends
///     on acquired resources or data which are initialized at load time,
///     thereby not automatically guaranting values are not nullable.
/// </remarks>
public interface IStatic<TData> : ILoadable
    where TData : IStatic<TData>, new()
{
    /// <summary>
    ///     The data instance produced by this static data.
    /// </summary>
    public static TData Instance
    {
        get => field ?? throw new InvalidOperationException($"Attempted to get uninitialized IStatic<{typeof(TData)}>");

        set
        {
            if (field is not null)
            {
                throw new InvalidOperationException($"Duplicate initialization of IStatic<{typeof(TData)}> (do you have duplicate StaticData<{typeof(TData)}>s?)");
            }

            field = value;
        }
    }

    void ILoadable.Load(Mod mod)
    {
        Instance = TData.LoadData(mod);
    }

    void ILoadable.Unload()
    {
        TData.UnloadData(Instance);
    }

    /// <summary>
    ///     Initializes the <typeparamref name="TData"/> instance.
    /// </summary>
    /// <param name="mod">The mod this belongs to.</param>
    /// <returns>The initialized <typeparamref name="TData"/> instance.</returns>
    protected static abstract TData LoadData(Mod mod);

    /// <summary>
    ///     Responsible for uninitializing the <typeparamref name="TData"/>.
    ///     Expected to dispose of resouces, etc.
    /// </summary>
    /// <param name="data">The data to clean up/uninitialize.</param>
    protected static abstract void UnloadData(TData data);
}

using System;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Models;

/// <summary>
///     Initializes <see cref="Instance"/> on load and uninitializes it on
///     unload.  Use this for cases where you need to acquire graphics resources
///     or perform other forms of initialization while desiring to maintain
///     nullability contracts.
/// </summary>
public interface IStatic<TSelf> : ILoadable
    where TSelf : IStatic<TSelf>
{
    /// <summary>
    ///     The loaded instance.
    /// </summary>
    public static TSelf Instance
    {
        get => field ?? throw new InvalidOperationException($"Attempted to get uninitialized IStatic<{typeof(TSelf)}>");

        set
        {
            if (field is not null)
            {
                throw new InvalidOperationException($"Duplicate initialization of IStatic<{typeof(TSelf)}> (do you have duplicate StaticData<{typeof(TSelf)}>s?)");
            }

            field = value;
        }
    }

    /// <summary>
    ///     Whether to run initialization and uninitialization actions on the
    ///     main thread.
    /// </summary>
    static virtual bool LoadOnMainThread => false;

    void ILoadable.Load(Mod mod)
    {
        if (TSelf.LoadOnMainThread)
        {
            Instance = Main.RunOnMainThread(() => TSelf.LoadData(mod)).GetAwaiter().GetResult();
        }
        else
        {
            Instance = TSelf.LoadData(mod);
        }
    }

    void ILoadable.Unload()
    {
        if (TSelf.LoadOnMainThread)
        {
            Main.RunOnMainThread(() => TSelf.UnloadData(Instance)).GetAwaiter().GetResult();
        }
        else
        {
            TSelf.UnloadData(Instance);
        }
    }

    /// <summary>
    ///     Initializes the <typeparamref name="TSelf"/> instance.
    /// </summary>
    /// <param name="mod">The mod this belongs to.</param>
    /// <returns>The initialized <typeparamref name="TSelf"/> instance.</returns>
    protected static abstract TSelf LoadData(Mod mod);

    /// <summary>
    ///     Responsible for uninitializing the <typeparamref name="TSelf"/>.
    ///     Expected to dispose of resources, etc.
    /// </summary>
    /// <param name="data">The data to clean up/uninitialize.</param>
    protected static abstract void UnloadData(TSelf data);
}

/// <summary>
///     Extensions to <see cref="IStatic{TSelf}"/>.
/// </summary>
public static class StaticExtensions
{
    extension<TData>(IStatic<TData>)
        where TData : IStatic<TData>
    {
        /// <inheritdoc cref="IStatic{TSelf}.Instance"/>
        public static TData Instance => IStatic<TData>.Instance;
    }
}

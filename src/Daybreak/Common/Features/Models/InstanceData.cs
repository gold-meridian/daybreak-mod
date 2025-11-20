using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

/// <summary>
///     Provides basic definitions for implemented instanced data models.  A
///     singleton instance is initialized upon load which will handle making
///     copies of itself according to the implementation.
/// </summary>
public abstract class InstanceData : ILoadable
{
    void ILoadable.Load(Mod mod)
    {
        LoadSingleton(mod);
    }

    void ILoadable.Unload()
    {
        UnloadSingleton();
    }

    /// <summary>
    ///     Called upon load for the singleton instance.
    /// </summary>
    /// <param name="mod">The mod this belongs to.</param>
    protected abstract void LoadSingleton(Mod mod);

    /// <summary>
    ///     Called upon unload for the singleton instance.
    /// </summary>
    protected abstract void UnloadSingleton();
}

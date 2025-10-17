using System;
using JetBrains.Annotations;
using Terraria.ModLoader;

namespace Daybreak.Core.Hooks;

/// <summary>
///     The basic implementation of the DAYBREAK initializer APIs.  Implements
///     <see cref="ILoadable" /> and facilitates basic execution of the
///     <see cref="ILoad" /> and <see cref="IUnload" /> hooks if present.
/// </summary>
[PublicAPI]
[UsedImplicitly(
    ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature,
    ImplicitUseTargetFlags.WithInheritors | ImplicitUseTargetFlags.WithMembers
)]
[Obsolete("Use OnLoadAttribute/OnUnloadAttribute instead")]
public interface IInitializer : ILoadable
{
    void ILoadable.Load(Mod mod)
    {
        (this as ILoad)?.Load();
    }

    void ILoadable.Unload()
    {
        (this as IUnload)?.Unload();
    }
}

/// <summary>
///     Provides a load hook.
/// </summary>
[PublicAPI]
[Obsolete("Use OnLoadAttribute instead")]
public interface ILoad : IInitializer
{
    /// <summary>
    ///     The load hook.
    /// </summary>
    [Obsolete("Use OnLoadAttribute instead")]
    void Load();
}

/// <summary>
///     Provides an unload hook.
/// </summary>
[PublicAPI]
[Obsolete("Use OnUnloadAttribute instead")]
public interface IUnload : IInitializer
{
    /// <summary>
    ///     The unload hook.
    /// </summary>
    [Obsolete("Use OnUnloadAttribute instead")]
    new void Unload();
}

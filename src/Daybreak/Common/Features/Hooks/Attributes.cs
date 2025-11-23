using System;
using System.Reflection;
using JetBrains.Annotations;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Common.Features.Hooks;

internal interface IHasSide
{
    ModSide Side { get; }
}

/// <summary>
///     Shared between all hooking attributes to provide a common base for
///     analysis.
/// </summary>
public abstract class BaseHookAttribute : Attribute;

/// <summary>
///     Automatically calls the decorated function on load.
///     <br />
///     If the method is instanced, it expects to be part of a parent type
///     implementing <see cref="ILoadable" /> (in which case it is called
///     directly after <see cref="ILoadable.Load" />).
///     <br />
///     If the method is static, it will just be called at the end of
///     <see cref="Mod.Autoload" />.
/// </summary>
/// <remarks>
///     If the type is annotated with <see cref="AutoloadAttribute"/>, whether
///     the type is autoload-able is also taken into account.  This means it
///     takes into account the Sided-ness of the containing type and whether
///     autoloading is enabled for it.
/// </remarks>
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class OnLoadAttribute : BaseHookAttribute, IHasSide
{
    /// <summary>
    ///     The side to load this on.
    /// </summary>
    public ModSide Side { get; set; } = ModSide.Both;
}

/// <summary>
///     Automatically calls the decorated function on unload.
///     <br />
///     If the method is instanced, it expects to be part of a parent type
///     implementing <see cref="ILoadable" />.
///     <br />
///     All methods will be run in reverse order at the start of
///     <see cref="ModContent.UnloadModContent" />
///     (before <see cref="MenuLoader.Unload" />).
/// </summary>
/// <remarks>
///     If the type is annotated with <see cref="AutoloadAttribute"/>, whether
///     the type is autoload-able is also taken into account.  This means it
///     takes into account the Sided-ness of the containing type and whether
///     autoloading is enabled for it.
/// </remarks>
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class OnUnloadAttribute : BaseHookAttribute, IHasSide
{
    /// <summary>
    ///     The side to load this on.
    /// </summary>
    public ModSide Side { get; set; } = ModSide.Both;
}

/// <inheritdoc cref="SubscribesToAttribute{T}"/>
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public abstract class SubscribesToAttribute : BaseHookAttribute, IHasSide
{
    /// <summary>
    ///     The side to load this on.
    /// </summary>
    public ModSide Side { get; set; } = ModSide.Both;

    /// <summary>
    ///     The type containing the hook definition.
    /// </summary>
    /// <returns></returns>
    public abstract Type GetHookType();
}

/// <summary>
///     Automatically subscribes the decorated method to the hook of type
///     <typeparamref name="T" /> if applicable.
///     <br />
///     If this decorates an instance method, the hook will be subscribed when
///     <see cref="Mod.AddContent" /> is called on the instance.  This means
///     instance hooks can only automatically be subscribed if the parent class
///     is an <see cref="ILoadable" /> and the instance has actually been loaded.
///     <br />
///     If this decorates a static method, the hook will be subscribed so long
///     as the parent type is visible under
///     <see cref="AssemblyManager.GetLoadableTypes(Assembly)" /> and the type
///     does not have any generic parameters (technical limitation).
/// </summary>
/// <typeparam name="T">The hook type to subscribe the method to.</typeparam>
/// <remarks>
///     If the type is annotated with <see cref="AutoloadAttribute"/>, whether
///     the type is autoload-able is also taken into account.  This means it
///     takes into account the Sided-ness of the containing type and whether
///     autoloading is enabled for it.
/// </remarks>
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class SubscribesToAttribute<T> : SubscribesToAttribute
{
    /// <inheritdoc />
    public override Type GetHookType()
    {
        return typeof(T);
    }
}

public abstract class IlEditAttribute : BaseHookAttribute;

public class IlEditAttribute<T> : IlEditAttribute;

public abstract class DetourAttribute : BaseHookAttribute;

public class DetourAttribute<T> : BaseHookAttribute;

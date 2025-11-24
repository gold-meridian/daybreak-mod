using System;
using System.Reflection;
using JetBrains.Annotations;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Hooks;

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
[HookMetadata(DelegateType = typeof(OnLoadHook.Definition))]
public sealed class OnLoadAttribute : BaseHookAttribute
{
    /// <inheritdoc />
    public override void Apply(MethodInfo bindingMethod, object? instance)
    {
        // Handled in HookLoader
    }
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
[HookMetadata(DelegateType = typeof(OnUnloadHook.Definition))]
public sealed class OnUnloadAttribute : BaseHookAttribute
{
    /// <inheritdoc />
    public override void Apply(MethodInfo bindingMethod, object? instance)
    {
        // Handled in HookLoader
    }
}

internal static class OnLoadHook
{
    public delegate void Definition(
        [Omittable] Mod mod
    );
}

internal static class OnUnloadHook
{
    public delegate void Definition(
        [Omittable] Mod mod
    );
}

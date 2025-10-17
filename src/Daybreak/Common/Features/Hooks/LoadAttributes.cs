using System;
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
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class OnLoadAttribute : Attribute
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
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public sealed class OnUnloadAttribute : Attribute
{
    /// <summary>
    ///     The side to load this on.
    /// </summary>
    public ModSide Side { get; set; } = ModSide.Both;
}

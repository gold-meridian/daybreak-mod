using System;
using System.Reflection;
using JetBrains.Annotations;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Common.Features.Hooks;

[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class EventAttribute : BaseHookAttribute;

[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class EventAttribute<T> : EventAttribute;

/// <inheritdoc cref="SubscribesToAttribute{T}"/>
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class SubscribesToAttribute(
    Type typeWithEvent,
    string eventName = "Event",
    string delegateName = "Definition"
) : BaseHookAttribute(
    delegateSignatureType: null,
    typeWithEvent: typeWithEvent,
    eventName: eventName,
    delegateName: delegateName,
    supportsInstancedMethods: true,
    supportsStaticMethods: true,
    supportsVoidOverload: true
)
{
    /// <inheritdoc />
    public override void Apply(MethodInfo bindingMethod, object? instance)
    {
        HookSubscriber.HandleSubscriber(this, bindingMethod, instance);
    }
}

/// <summary>
///     Automatically subscribes the decorated method to the hook of type
///     <typeparamref name="T" /> if applicable.  This variant is intended for
///     specially-generated hooks wrapping tModLoader hooks.
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
public class SubscribesToAttribute<T>(
    string eventName = "Event",
    string delegateName = "Definition"
) : SubscribesToAttribute(
    typeof(T),
    eventName,
    delegateName
);

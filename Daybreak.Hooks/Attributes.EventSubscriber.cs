using System;
using System.Reflection;
using JetBrains.Annotations;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Hooks;

/*
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class EventAttribute : BaseHookAttribute;

[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public class EventAttribute<T> : EventAttribute;
*/

/// <inheritdoc cref="SubscribesToAttribute{T}"/>
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public abstract class SubscribesToAttribute : BaseHookAttribute
{
    /// <inheritdoc />
    public override void Apply(MethodInfo bindingMethod, object? instance)
    {
        HookSubscriber.HandleSubscriber(this, bindingMethod, instance);
    }
}

using System;
using System.Reflection;
using JetBrains.Annotations;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Hooks;

internal interface IHasSide
{
    ModSide Side { get; }
}

/// <summary>
///     Indicates a parameter in a delegate is omittable.  This is a convenience
///     attribute used when building wrappers through
///     <see cref="HookSubscriber"/>.  Each omittable attribute is expected to
///     decorate a parameter with a unique type, and these parameters should
///     only be at the start (that is, they can't be broken up or appear after
///     non-omittable parameters).
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public sealed class OmittableAttribute : Attribute;

/// <summary>
///     Shared between all hooking attributes to provide a common base for
///     code analysis and code fixes.  Provides relevant information for parsing
///     out how to handle a given type.
/// </summary>
[PublicAPI]
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
public abstract class BaseHookAttribute(
    Type? delegateSignatureType = null,
    Type? typeWithEvent = null,
    string? eventName = null,
    string? delegateName = null,
    bool supportsInstancedMethods = true,
    bool supportsStaticMethods = true,
    bool supportsVoidOverload = false
) : Attribute, IHasSide
{
    /// <summary>
    ///     The delegate type representing the signature of the event.  If not
    ///     specified, it will attempt to be resolved from
    ///     <see cref="TypeContainingEvent"/> using <see cref="DelegateName"/>.
    ///     If still not resolved, it will use the event's type (assuming it was
    ///     resolved).
    /// </summary>
    public Type? DelegateType => delegateSignatureType;

    /// <summary>
    ///     The type containing the event.
    /// </summary>
    public Type? TypeContainingEvent => typeWithEvent;

    /// <summary>
    ///     The name of the event field within the type.
    /// </summary>
    public string? EventName => eventName;

    /// <summary>
    ///     The name of the delegate within <see cref="TypeContainingEvent"/> if
    ///     <see cref="DelegateType"/> is unspecified.
    /// </summary>
    public string? DelegateName => delegateName;

    /// <summary>
    ///     Whether methods annotated with this hook attribute may be instanced.
    ///     <br />
    ///     Relies on them implementing <see cref="ILoadable"/>, as it uses the
    ///     autloaded singleton/template instance.
    /// </summary>
    public bool SupportsInstancedMethods => supportsInstancedMethods;

    /// <summary>
    ///     Whether methods annotated with this hook attribute may be static.
    /// </summary>
    public bool SupportsStaticMethods => supportsStaticMethods;

    /// <summary>
    ///     Whether the hook may return void instead of the expected return
    ///     type.
    /// </summary>
    public bool SupportsVoidOverload => supportsVoidOverload;

    /// <summary>
    ///     The side to load this on.
    /// </summary>
    public ModSide Side { get; set; } = ModSide.Both;

    /// <summary>
    ///     Applies this hook to the instance.
    /// </summary>
    public abstract void Apply(MethodInfo bindingMethod, object? instance);

    /// <summary>
    ///     Attempts to resolve the delegate type from the outlined rules.
    /// </summary>
    public Type? GetDelegateType()
    {
        if (DelegateType is not null)
        {
            return DelegateType;
        }

        if (TypeContainingEvent is null)
        {
            return null;
        }

        if (DelegateName is not null)
        {
            return TypeContainingEvent.GetNestedType(DelegateName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        }

        if (GetEventInfo() is { } eventInfo)
        {
            return eventInfo.EventHandlerType;
        }

        return null;
    }

    /// <summary>
    ///     Attempts to resolve the event from the outlined rules.
    /// </summary>
    /// <returns></returns>
    public EventInfo? GetEventInfo()
    {
        if (TypeContainingEvent is null || EventName is null)
        {
            return null;
        }

        return TypeContainingEvent.GetEvent(EventName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
    }
}

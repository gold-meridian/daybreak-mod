using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Hooks;

internal interface IHasSide
{
    ModSide Side { get; }
}

/// <summary>
///     Indicates a parameter in a delegate is omittable.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public sealed class OmittableAttribute : Attribute;

/// <summary>
///     Indicates a parameter has been renamed.  Required for dynamic binding to
///     support <see cref="OmittableAttribute"/>-annotated parameters.
/// </summary>
/// <param name="name">The original parameter name.</param>
[AttributeUsage(AttributeTargets.Parameter)]
public sealed class OriginalNameAttribute(string name) : Attribute
{
    /// <summary>
    ///     The original parameter name.
    /// </summary>
    public string Name => name;
}

/// <summary>
///     
/// </summary>
[AttributeUsage(AttributeTargets.ReturnValue)]
public abstract class AbstractPermitsVoidAttribute : Attribute
{
    /// <summary>
    ///     Modifies the expression to permit void returns.
    /// </summary>
    public abstract Expression ModifyExpression(
        HookSubscriber.ReturnExpressionContext ctx
    );
}

[AttributeUsage(AttributeTargets.ReturnValue)]
internal sealed class PermitsVoidInvokeParameterWithParametersAttribute(string parameterName) : AbstractPermitsVoidAttribute
{
    public string ParameterName => parameterName;

    public override Expression ModifyExpression(HookSubscriber.ReturnExpressionContext ctx)
    {
        var handlerParam = ctx.EventParameters
                              .FirstOrDefault(x => x.Name == parameterName);

        if (handlerParam is null)
        {
            throw new InvalidOperationException($"Cannot handle void return because handler parameter {parameterName} was not found");
        }

        var handlerExpr = ctx.EventParameterExpressions[Array.IndexOf(ctx.EventParameters, handlerParam)];

        var origInvoke = handlerParam.ParameterType.GetMethod("Invoke")
                      ?? throw new InvalidOperationException($"Cannot handle void return; could not get Invoke method of {parameterName}");

        var origParameters = origInvoke.GetParameters();

        var origArguments = new List<Expression>();
        foreach (var origParameter in origParameters)
        {
            var name = origParameter.Name;
            var mappedParam = ctx.EventParameters.FirstOrDefault(x => x.Name == name);
            if (mappedParam is null || mappedParam.ParameterType != origParameter.ParameterType)
            {
                throw new InvalidOperationException("Incompatible return handler signature");
            }

            origArguments.Add(ctx.EventParameterExpressions[Array.IndexOf(ctx.EventParameters, mappedParam)]);
        }

        var origCall = Expression.Call(handlerExpr, origInvoke, origArguments);

        return Expression.Block(
            ctx.CallExpression,
            origCall
        );
    }
}

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
    bool supportsStaticMethods = true
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

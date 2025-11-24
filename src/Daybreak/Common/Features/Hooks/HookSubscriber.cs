using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Daybreak.Common.Features.Hooks;

/// <summary>
///     Provides utilities and APIs for generating delegates with omitted
///     parameters and subscribing to event hooks.  Used to implement
///     <see cref="BaseHookAttribute"/> and friends.
/// </summary>
public static class HookSubscriber
{
    /// <summary>
    ///     Context used to generate void-permissive return handlers.
    /// </summary>
    public readonly record struct ReturnExpressionContext(
        ParameterInfo[] EventParameters,
        ParameterExpression[] EventParameterExpressions,
        MethodCallExpression CallExpression
    );

    /// <summary>
    ///     Handles wrapping a method per <see cref="BuildWrapper"/> and
    ///     subscribing that wrapper to an event given by
    ///     <paramref name="hookDefinition"/>.
    /// </summary>
    public static void HandleSubscriber(
        BaseHookAttribute hookDefinition,
        MethodInfo bindingMethod,
        object? instance
    )
    {
        var eventInfo = hookDefinition.GetEventInfo()
                     ?? throw new InvalidOperationException($"Could not resolve hook event for bindingMethod={bindingMethod.Name}.");

        var handlerType = hookDefinition.GetDelegateType()
                       ?? throw new InvalidOperationException($"Could not resolve delegate type for bindingMethod={bindingMethod.Name}.");

        var invokeMethod = handlerType.GetMethod("Invoke")
                        ?? throw new InvalidOperationException("The event handler type could not resolve an Invoke method.");

        var wrapper = BuildWrapper(handlerType, invokeMethod, bindingMethod, instance);
        eventInfo.AddEventHandler(null, wrapper);
    }

    /// <summary>
    ///     Builds an invocable delegate which wraps the given
    ///     <see cref="bindingMethod"/>.  This wrapped delegate accounts for
    ///     <see cref="OmittableAttribute"/>,
    ///     <see cref="OriginalNameAttribute"/>, and
    ///     <see cref="AbstractPermitsVoidAttribute"/> to build a new delegate
    ///     which can be invoked with the parameters of
    ///     <see cref="invokeMethod"/>.
    /// </summary>
    public static Delegate BuildWrapper(
        Type delegateType,
        MethodInfo invokeMethod,
        MethodInfo bindingMethod,
        object? instance
    )
    {
        var eventParams = invokeMethod.GetParameters();
        var eventParamExprs = eventParams
                             .Select(x => Expression.Parameter(x.ParameterType, x.Name))
                             .ToArray();

        var targetParams = bindingMethod.GetParameters();

        var arguments = new List<Expression>(targetParams.Length);
        foreach (var targetParam in targetParams)
        {
            var name = targetParam.GetCustomAttribute<OriginalNameAttribute>() is { } origName
                ? origName.Name
                : targetParam.Name;

            var mappedParam = eventParams.FirstOrDefault(x => x.Name == name);
            if (mappedParam is null || mappedParam.ParameterType != targetParam.ParameterType)
            {
                throw new InvalidOperationException("Incompatible hook-binding signatures");
            }

            arguments.Add(eventParamExprs[Array.IndexOf(eventParams, mappedParam)]);
        }

        var callExpr = Expression.Call(bindingMethod.IsStatic ? null : Expression.Constant(instance), bindingMethod, arguments);

        var eventReturn = invokeMethod.ReturnType;
        var bindingReturn = bindingMethod.ReturnType;

        Expression bodyExpr;
        if (eventReturn == typeof(void))
        {
            // Simplest case is that the event returns void, meaning we can omit
            // handling return values entirely.
            bodyExpr = callExpr;
        }
        else
        {
            if (bindingReturn == eventReturn)
            {
                bodyExpr = Expression.Convert(callExpr, eventReturn);
            }
            else
            {
                if (bindingReturn.GetCustomAttribute<AbstractPermitsVoidAttribute>() is not { } permitsVoidHandler)
                {
                    throw new InvalidOperationException("Incompatible hook-binding return type");
                }

                bodyExpr = permitsVoidHandler.ModifyExpression(
                    new ReturnExpressionContext(
                        eventParams,
                        eventParamExprs,
                        callExpr
                    )
                );
            }
        }

        var lambda = Expression.Lambda(delegateType, bodyExpr, eventParamExprs);
        return lambda.Compile();
    }
}

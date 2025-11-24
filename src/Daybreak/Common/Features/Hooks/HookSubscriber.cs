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
    internal static void HandleSubscriber(
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

    public static Delegate BuildWrapper(
        Type delegateType,
        MethodInfo eventInvoke,
        MethodInfo bindingMethod,
        object? instance
    )
    {
        var eventParameters = eventInvoke.GetParameters();
        var eventParameterExpressions = eventParameters
                                       .Select(x => Expression.Parameter(x.ParameterType, x.Name))
                                       .ToArray();

        var targetParameters = bindingMethod.GetParameters();

        var arguments = new List<Expression>();
        for (var i = 0; i < eventParameterExpressions.Length; i++)
        {
            var argExpr = eventParameterExpressions[i];

            arguments.Add(argExpr);
        }

        var callExpr = Expression.Call(bindingMethod.IsStatic ? null : Expression.Constant(instance), bindingMethod, arguments);

        var eventReturn = eventInvoke.ReturnType;
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
                var origExpr = eventParameterExpressions[0];
                var origInvoke = eventParameters[0].ParameterType.GetMethod("Invoke")
                              ?? throw new InvalidOperationException("Could not get Invoke method of orig");
                var origCall = Expression.Call(origExpr, origInvoke, eventParameterExpressions.Skip(2));

                bodyExpr = Expression.Block(
                    callExpr,
                    origCall
                );
            }
        }

        var lambda = Expression.Lambda(delegateType, bodyExpr, eventParameterExpressions);
        return lambda.Compile();
    }
}

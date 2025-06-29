using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

using Terraria.ModLoader;

namespace Daybreak.Common.Features.ModCalls;

/// <summary>
///     Models aliases and their associated handlers, to be used to register
///     cross-mod API calls.
/// </summary>
public readonly record struct CallManifest(
    IReadOnlyCollection<string> Aliases,
    IReadOnlyCollection<Delegate> Handlers
);

/// <summary>
///     Handles cross-mod API calls.
/// </summary>
public static class CallHandler
{
    private readonly record struct CallResult(bool Success, object? Value)
    {
        // Explicit definitions here are useful for generating IL.

        public static CallResult FromResult(object? result)
        {
            return new CallResult(true, result);
        }

        public static CallResult FromFailure()
        {
            return new CallResult(Success: false, Value: null);
        }
    }

    private readonly record struct ConvertResult<T>(bool Success, T? Value);

    private sealed class CallInfoCache(Mod mod)
    {
        private readonly Dictionary<string, HashSet<Delegate>> handlers = [];

        private readonly Dictionary<Delegate, Func<object?[]?, CallResult>> invokeCache = [];

        public void AddManifest(CallManifest manifest)
        {
            foreach (var alias in manifest.Aliases)
            {
                var theAlias = alias.ToLower();

                if (!handlers.TryGetValue(theAlias, out var theHandlers))
                {
                    handlers[theAlias] = theHandlers = [];
                }

                theHandlers.UnionWith(manifest.Handlers);
            }
        }

        public object? InvokeCall(string command, object?[]? args)
        {
            if (!handlers.TryGetValue(command.ToLower(), out var theHandlers))
            {
                throw new ArgumentException($"DAYBREAK CallHandler for \"{mod.Name}\" could not find a command named '{command}' (case-insensitive).");
            }

            foreach (var handler in theHandlers)
            {
                var result = GetOrCreateInvoke(handler, args)(args);
                if (!result.Success)
                {
                    continue;
                }

                return result.Value;
            }

            throw new ArgumentException(
                $"DAYBREAK CallHandler for \"{mod.Name}\" could not resolve a handler for the command '{command}' accepting the given arguments:"
              + $"\n{string.Join('\n',
                  args is null
                      ? ["<null array>"]
                      : args.Select(x => x?.GetType().FullName ?? "<null argument>"))}"
            );
        }

        private Func<object?[]?, CallResult> GetOrCreateInvoke(Delegate @delegate, object?[]? args)
        {
            var method = @delegate.Method;
            var parameters = method.GetParameters();

            if (args is null)
            {
                if (parameters.Length != 0)
                {
                    return FailureCase;
                }
            }
            else if (args.Length != parameters.Length)
            {
                return FailureCase;
            }

            var paramEmitAction = new Action<int, ILGenerator>[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                Debug.Assert(args is not null);

                var argType = args[i]?.GetType() ?? typeof(Null);
                if (parameters[i].ParameterType.IsAssignableTo(typeof(ITransformableCallObject<>).MakeGenericType(argType)))
                {
                    paramEmitAction[i] = (x, il) =>
                    {
                        il.Emit(OpCodes.Unbox_Any, typeof(ITransformableCallObject<>).MakeGenericType(argType));
                        il.Emit(
                            OpCodes.Call,
                            GetType().GetMethod(nameof(ConvertTransformable), BindingFlags.NonPublic | BindingFlags.Static)!
                                     .MakeGenericMethod(argType, parameters[x].ParameterType)
                        );
                    };
                }
                else if (argType.IsAssignableTo(parameters[i].ParameterType))
                {
                    paramEmitAction[i] = (x, il) =>
                    {
                        il.Emit(OpCodes.Unbox_Any, parameters[x].ParameterType);
                        il.Emit(
                            OpCodes.Call,
                            GetType().GetMethod(nameof(Convert), BindingFlags.NonPublic | BindingFlags.Static)!
                                     .MakeGenericMethod(argType, parameters[x].ParameterType)
                        );
                    };
                }
                else if (argType == typeof(Null))
                {
                    if (parameters[i].ParameterType != typeof(Null))
                    {
                        return FailureCase;
                    }
                    
                    paramEmitAction[i] = (_, il) =>
                    {
                        il.Emit(OpCodes.Initobj, typeof(Null));
                    };
                }
            }

            if (invokeCache.TryGetValue(@delegate, out var invoke))
            {
                return invoke;
            }

            var dynMethod = new DynamicMethod(
                "Invoke",
                typeof(CallResult),
                [typeof(object[])],
                typeof(CallInfoCache).Module // TODO: Use mod's?
            );

            var il = dynMethod.GetILGenerator();

            // Prepare local registers.
            var locals = new LocalBuilder[parameters.Length];
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    var loc = il.DeclareLocal(parameters[i].ParameterType);
                    {
                        // TODO: .NET 9+
                        // loc.SetLocalSymInfo(parameter name);
                    }

                    locals[i] = loc;
                }
            }

            // Validate argument inputs.
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    // Push parameter.
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldelem_Ref);

                    // Convert type as needed.
                    {
                        paramEmitAction[i](i, il);
                    }

                    // il.Emit(OpCodes.Dup); // Keep conversion result on stack.
                    var convertLoc = il.DeclareLocal(typeof(ConvertResult<>).MakeGenericType(parameters[i].ParameterType));
                    il.Emit(OpCodes.Stloc, convertLoc);

                    var label = il.DefineLabel();

                    // First check if the conversion was successful.
                    {
                        il.Emit(OpCodes.Ldloc, convertLoc);
                        il.Emit(
                            OpCodes.Call,
                            GetType().GetMethod(nameof(GetSuccess), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(parameters[i].ParameterType)
                        );
                        il.Emit(OpCodes.Brtrue_S, label);
                        il.Emit(OpCodes.Call, typeof(CallResult).GetMethod(nameof(CallResult.FromFailure), BindingFlags.Public | BindingFlags.Static)!);
                        il.Emit(OpCodes.Ret);
                    }

                    // Then actually write it to the register.

                    il.MarkLabel(label);

                    il.Emit(OpCodes.Ldloc, convertLoc);
                    il.Emit(
                        OpCodes.Call,
                        GetType().GetMethod(nameof(GetValue), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(parameters[i].ParameterType)
                    );
                    il.Emit(OpCodes.Stloc, locals[i]);
                }
            }

            // Invoke the delegate.
            {
                // Prepare arguments (push to stack).
                for (var i = 0; i < parameters.Length; i++)
                {
                    il.Emit(OpCodes.Ldloc, locals[i]);
                    // il.Emit(OpCodes.Unbox_Any, parameters[i].ParameterType);
                }

                il.Emit(OpCodes.Call, method);

                // Handle return type cases.  If the delegate returns void, push
                // null manually since we need an object.  If it's a value type,
                // box it.
                if (method.ReturnType == typeof(void))
                {
                    il.Emit(OpCodes.Ldnull);
                }
                else if (method.ReturnType.IsValueType)
                {
                    il.Emit(OpCodes.Box, method.ReturnType);
                }

                // Create the call object.
                il.Emit(OpCodes.Call, typeof(CallResult).GetMethod(nameof(CallResult.FromResult), BindingFlags.Public | BindingFlags.Static)!);

                il.Emit(OpCodes.Ret);
            }

            return invokeCache[@delegate] = dynMethod.CreateDelegate<Func<object?[]?, CallResult>>();
        }

        private static CallResult FailureCase(object?[]? args)
        {
            return new CallResult(Success: false, Value: null);
        }

        private static ConvertResult<TTo?> Convert<TFrom, TTo>(TFrom from)
        {
            if (typeof(TFrom) == typeof(Null) && typeof(TTo) == typeof(Null))
            {
                return new ConvertResult<TTo?>(Success: true, Value: default(TTo?));
            }

            if (from is null && typeof(TTo) == typeof(Null))
            {
                return new ConvertResult<TTo?>(Success: true, Value: default(TTo?));
            }

            if (from is TTo tObj)
            {
                return new ConvertResult<TTo?>(Success: true, Value: tObj);
            }

            return new ConvertResult<TTo?>(Success: false, Value: default(TTo?));
        }

        private static ConvertResult<TTo?> ConvertTransformable<TFrom, TTo>(TFrom from)
            where TTo : ITransformableCallObject<TFrom>
        {
            if (typeof(TFrom) == typeof(Null) && typeof(TTo) == typeof(Null))
            {
                return new ConvertResult<TTo?>(Success: true, Value: default(TTo?));
            }

            if (from is null && typeof(TTo) == typeof(Null))
            {
                return new ConvertResult<TTo?>(Success: true, Value: default(TTo?));
            }

            // This case is still technically possible, but unlikely.
            if (from is TTo tObj)
            {
                return new ConvertResult<TTo?>(Success: true, Value: tObj);
            }

            if (TTo.TryTransform(from, out var result))
            {
                // TODO: Check if result is TTo and throw or Success: false if
                //       it isn't.
                return new ConvertResult<TTo?>(Success: true, Value: (TTo)result);
            }

            return new ConvertResult<TTo?>(Success: false, Value: default(TTo?));
        }

        // Explicit definitions here are useful for generating IL.

        private static bool GetSuccess<T>(ConvertResult<T> result)
        {
            return result.Success;
        }

        private static T GetValue<T>(ConvertResult<T> result)
        {
            return result.Value;
        }

        /*private static ConvertResult<T> Debug<T>()
        {
            return new ConvertResult<T>(Success: false, Value: default(T));
        }*/
    }

    private static readonly Dictionary<Mod, CallInfoCache> calls = [];

    /// <summary>
    ///     Registers a call manifest for the mod.
    /// </summary>
    public static void Register(Mod mod, CallManifest callManifest)
    {
        if (!calls.TryGetValue(mod, out var cache))
        {
            calls[mod] = cache = new CallInfoCache(mod);
        }

        cache.AddManifest(callManifest);
    }

    /// <summary>
    ///     Attempts to handle a cross-mod API call invocation by using the
    ///     registered handlers.
    /// </summary>
    public static object? HandleCall(Mod mod, object?[]? args)
    {
        if (!calls.TryGetValue(mod, out var cache))
        {
            // TODO: Throw/report exception?
            return null;
        }

        if (args?.Length < 1 || args?[0] is not string command)
        {
            throw new ArgumentException($"DAYBREAK CallHandler for \"{mod.Name}\" expected a string command name as the first argument.");
        }

        return cache.InvokeCall(command, args[1..]);
    }
}
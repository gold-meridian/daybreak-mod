
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

using Terraria.ModLoader;

namespace Tomat.TML.Library.DynamicModCalls;

internal sealed class CallHandler : ModSystem
{
    private sealed class CallInfoCache
    {
        private readonly List<ModCall> callCache = [];
        
        private readonly Dictionary<ModCall, Func<object?[], object?>> invokeCache = [];
        
        public void AddCall(ModCall call)
        {
            callCache.Add(call);
        }
        
        public ModCall? GetCallByCommand(string command)
        {
            return callCache.FirstOrDefault(
                x => x.CallCommands.Contains(
                    command,
                    StringComparer.OrdinalIgnoreCase
                )
            );
        }

        public object? InvokeCall(ModCall call, object?[]? args)
        {
            return GetOrCreateInvoke(call, args)(args!);
        }

        private Func<object?[], object?> GetOrCreateInvoke(ModCall call, object?[]? args)
        {
            var method     = call.Delegate.Method;
            var parameters = method.GetParameters();

            if (args is null || args.Length != parameters.Length)
            {
                throw new ArgumentException(
                    $"ModCall::Invoke expected {parameters.Length} arguments, but got {args?.Length ?? 0}."
                );
            }

            if (invokeCache.TryGetValue(call, out var invoke))
            {
                return invoke;
            }

            var dynMethod = new DynamicMethod(
                "Invoke",
                typeof(object),
                [typeof(object[])],
                typeof(CallInfoCache).Module
            );

            var il = dynMethod.GetILGenerator();

            // Validate argument inputs.
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldelem_Ref);
                    il.Emit(OpCodes.Isinst, parameters[i].ParameterType);
                    var label = il.DefineLabel();
                    il.Emit(OpCodes.Brtrue_S, label);
                    il.Emit(OpCodes.Ldstr,    $"Argument {i} is not of type {parameters[i].ParameterType}.");
                    il.Emit(OpCodes.Newobj,   typeof(ArgumentException).GetConstructor([typeof(string)])!);
                    il.Emit(OpCodes.Throw);
                    il.MarkLabel(label);
                }
            }

            // Invoke the delegate.
            {
                // Prepare arguments (push to stack).
                for (var i = 0; i < parameters.Length; i++)
                {
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldelem_Ref);
                    il.Emit(OpCodes.Unbox_Any, parameters[i].ParameterType);
                }

                il.Emit(OpCodes.Call, call.Delegate.Method);

                // Handle return type cases.  If the delegate returns void, push
                // null manually since we need an object.  If it's a value type,
                // box it.
                if (call.Delegate.Method.ReturnType == typeof(void))
                {
                    il.Emit(OpCodes.Ldnull);
                }
                else if (call.Delegate.Method.ReturnType.IsValueType)
                {
                    il.Emit(OpCodes.Box, call.Delegate.Method.ReturnType);
                }

                il.Emit(OpCodes.Ret);
            }

            return invokeCache[call] = (Func<object?[], object?>)dynMethod.CreateDelegate(typeof(Func<object?[], object?>));
        }
    }

    private static readonly Dictionary<TmlMod, CallInfoCache> calls = [];

    public static void Register(TmlMod mod, ModCall call)
    {
        if (!calls.TryGetValue(mod, out var cache))
        {
            calls[mod] = cache = new CallInfoCache();
        }

        cache.AddCall(call);
    }

    public static object? HandleCall(TmlMod mod, object?[]? args)
    {
        if (!calls.TryGetValue(mod, out var cache))
        {
            return null;
        }

        if (args?.Length < 1 || args?[0] is not string command)
        {
            throw new ArgumentException("Mod::Call invocation expected a string call command name as the first argument.");
        }

        if (cache.GetCallByCommand(command) is not { } modCall)
        {
            throw new ArgumentException($"Mod::Call invocation could not find a call command named '{command}'.");
        }

        return cache.InvokeCall(modCall, args[1..]);
    }
}
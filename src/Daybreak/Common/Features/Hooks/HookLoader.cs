using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Daybreak.Common.CodeAnalysis;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;

namespace Daybreak.Common.Features.Hooks;

internal static class HookLoader
{
    private static Mod? currentlyLoadingMod;

    /// <summary>
    ///     Internally used to hook into the start of mod loading; useful for
    ///     injecting content into other mods.
    /// </summary>
    public static event Action<Mod>? OnEarlyModLoad;

    public static Mod GetModOrThrow()
    {
        return currentlyLoadingMod ?? throw new InvalidOperationException("Cannot continue with operation as no mod is currently applicable to load content through Mod::AddContent");
    }

#pragma warning disable CA2255
    [ModuleInitializer]
    public static void HookIntoContentLoadingRoutine()
    {
        // Hijack the content loading process to allow us to handle every added
        // loadable.  For every loadable, resolve instanced hooks and apply
        // them.
        MonoModHooks.Add(
            typeof(Mod).GetMethods().Single(x => x.Name.Equals(nameof(Mod.AddContent)) && !x.IsGenericMethod),
            AddContent_ResolveInstancedHooks_CallOnLoads
        );

        MonoModHooks.Add(
            typeof(Mod).GetMethod(nameof(Mod.Autoload), BindingFlags.NonPublic | BindingFlags.Instance)!,
            Autoload_ResolveStaticHooks_CallOnLoads
        );

        /*
        MonoModHooks.Add(
            typeof(MenuLoader).GetMethod(nameof(MenuLoader.Unload), BindingFlags.NonPublic | BindingFlags.Static)!,
            Unload_CallOnUnloads
        );
        */

        MonoModHooks.Add(
            typeof(MonoModHooks).GetMethod(nameof(MonoModHooks.RemoveAll), BindingFlags.NonPublic | BindingFlags.Static)!,
            RemoveAll_SkipDaybreak
        );

        MonoModHooks.Modify(
            typeof(Mod).GetMethod(nameof(Mod.UnloadContent), BindingFlags.NonPublic | BindingFlags.Instance)!,
            UnloadContent_CallOnUnloads
        );

        // AutoloadConfig and EnsureResizeArraysAttributeStaticCtorsRun are the
        // first and last methods called in the routine where Mod::loading is
        // set to true.  We use these to know what mod is currently being
        // loaded.
        MonoModHooks.Add(
            typeof(Mod).GetMethod(nameof(Mod.AutoloadConfig), BindingFlags.NonPublic | BindingFlags.Instance),
            (Action<Mod> orig, Mod self) =>
            {
                Debug.Assert(currentlyLoadingMod is null);

                currentlyLoadingMod = self;

                OnEarlyModLoad?.Invoke(self);

                orig(self);
            }
        );

        MonoModHooks.Add(
            typeof(SystemLoader).GetMethod(nameof(SystemLoader.EnsureResizeArraysAttributeStaticCtorsRun), BindingFlags.NonPublic | BindingFlags.Static),
            (Action<Mod> orig, Mod mod) =>
            {
                Debug.Assert(currentlyLoadingMod == mod);

                orig(mod);
                currentlyLoadingMod = null;
            }
        );
    }
#pragma warning restore CA2255

    private static bool AddContent_ResolveInstancedHooks_CallOnLoads(Func<Mod, ILoadable, bool> orig, Mod self, ILoadable instance)
    {
        // Only attempt to resolve and apply hooks if the instance actually
        // loaded...
        if (!orig(self, instance))
        {
            return false;
        }

        ContractEnforcer.ValidateLoadable(instance);

        ResolveInstancedHooks(instance);
        CallOnLoads(instance);
        return true;
    }

    private static void Autoload_ResolveStaticHooks_CallOnLoads(Action<Mod> orig, Mod self)
    {
        orig(self);

        if (self.Code is null)
        {
            return;
        }

        var loadableTypes = AssemblyManager.GetLoadableTypes(self.Code)
                                           .Where(x => AutoloadAttribute.GetValue(x).NeedsAutoloading)
                                           .OrderBy(x => x.FullName, StringComparer.InvariantCulture)
                                           .ToArray();

        LoaderUtils.ForEachAndAggregateExceptions(loadableTypes, ResolveStaticHooks);
        LoaderUtils.ForEachAndAggregateExceptions(loadableTypes, CallOnLoads);
    }

    /*
    private static void Unload_CallOnUnloads(Action orig)
    {
        foreach (var mod in ModLoader.Mods)
        {
            var loadableTypes = AssemblyManager.GetLoadableTypes(mod.Code)
                                               .OrderBy(x => x.FullName, StringComparer.InvariantCulture)
                                               .ToArray();
            LoaderUtils.ForEachAndAggregateExceptions(Enumerable.Reverse(loadableTypes), CallOnUnloads);

            foreach (var loadable in mod.GetContent().Reverse())
            {
                CallOnUnloads(loadable);
            }
        }

        orig();
    }
    */

    private static void RemoveAll_SkipDaybreak(Action<Mod> orig, Mod mod)
    {
        if (mod is ModImpl)
        {
            return;
        }

        // Force our edits to unload when it gets to ModLoaderMod instead (late
        // stage).
        // Use TryGetMod because GetInstance may return null in early unload stages.
        if (mod is ModLoaderMod && ModLoader.TryGetMod("Daybreak", out var modImpl))
        {
            orig(modImpl);
            return;
        }

        orig(mod);
    }

    private static void UnloadContent_CallOnUnloads(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.After, x => x.MatchCallvirt<Mod>(nameof(Mod.Unload)));

        c.EmitLdarg0();
        c.EmitDelegate(
            static (Mod mod) =>
            {
                var loadableTypes = AssemblyManager.GetLoadableTypes(mod.Code)
                                                   .Where(x => AutoloadAttribute.GetValue(x).NeedsAutoloading)
                                                   .OrderBy(x => x.FullName, StringComparer.InvariantCulture)
                                                   .ToArray();
                LoaderUtils.ForEachAndAggregateExceptions(Enumerable.Reverse(loadableTypes), CallOnUnloads);
            }
        );

        c.GotoNext(MoveType.Before, x => x.MatchCallvirt<ILoadable>(nameof(ILoadable.Unload)));

        c.EmitDup();
        c.EmitDelegate(
            static (ILoadable loadable) =>
            {
                CallOnUnloads(loadable);
            }
        );
    }

    private static void CallOnUnloads(ILoadable instance)
    {
        var methods = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in Enumerable.Reverse(methods))
        {
            var attribute = method.GetCustomAttribute<OnUnloadAttribute>(inherit: false);
            if (attribute is null)
            {
                continue;
            }

            if (!ModOrganizer.LoadSide(attribute.Side))
            {
                continue;
            }

            if (method.IsGenericMethod)
            {
                throw new InvalidOperationException($"The method {method} cannot be generic.");
            }

            if (method.ReturnType != typeof(void))
            {
                throw new InvalidOperationException($"The method {method} must return void.");
            }

            if (method.GetParameters().Length != 0)
            {
                throw new InvalidOperationException($"The method {method} must not have any parameters.");
            }

            method.Invoke(instance, null);
        }
    }

    private static void CallOnUnloads(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        foreach (var method in Enumerable.Reverse(methods))
        {
            var attribute = method.GetCustomAttribute<OnUnloadAttribute>(inherit: false);
            if (attribute is null)
            {
                continue;
            }

            if (!ModOrganizer.LoadSide(attribute.Side))
            {
                continue;
            }

            if (method.IsGenericMethod)
            {
                throw new InvalidOperationException($"The method {method} cannot be generic.");
            }

            if (method.ReturnType != typeof(void))
            {
                throw new InvalidOperationException($"The method {method} must return void.");
            }

            if (method.GetParameters().Length != 0)
            {
                throw new InvalidOperationException($"The method {method} must not have any parameters.");
            }

            method.Invoke(null, null);
        }
    }

    private static void ResolveInstancedHooks(ILoadable instance)
    {
        SubscribeToHooks(
            instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
            instance
        );
    }

    private static void ResolveStaticHooks(Type type)
    {
        SubscribeToHooks(
            type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static),
            null
        );
    }

    private static void CallOnLoads(ILoadable instance)
    {
        var methods = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<OnLoadAttribute>(inherit: false);
            if (attribute is null)
            {
                continue;
            }

            if (!ModOrganizer.LoadSide(attribute.Side))
            {
                continue;
            }

            if (method.IsGenericMethod)
            {
                throw new InvalidOperationException($"The method {method} cannot be generic.");
            }

            if (method.ReturnType != typeof(void))
            {
                throw new InvalidOperationException($"The method {method} must return void.");
            }

            if (method.GetParameters().Length != 0)
            {
                throw new InvalidOperationException($"The method {method} must not have any parameters.");
            }

            method.Invoke(instance, null);
        }
    }

    private static void CallOnLoads(Type type)
    {
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        foreach (var method in methods)
        {
            var attribute = method.GetCustomAttribute<OnLoadAttribute>(inherit: false);
            if (attribute is null)
            {
                continue;
            }

            if (!ModOrganizer.LoadSide(attribute.Side))
            {
                continue;
            }

            if (method.IsGenericMethod)
            {
                throw new InvalidOperationException($"The method {method} cannot be generic.");
            }

            if (method.ReturnType != typeof(void))
            {
                throw new InvalidOperationException($"The method {method} must return void.");
            }

            if (method.GetParameters().Length != 0)
            {
                throw new InvalidOperationException($"The method {method} must not have any parameters.");
            }

            method.Invoke(null, null);
        }
    }

    private static void SubscribeToHooks(MethodInfo[] methods, object? instance)
    {
        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(SubscribesToAttribute<>), inherit: false);
            if (attributes.Length == 0)
            {
                continue;
            }

            foreach (var attribute in attributes)
            {
                if (attribute is IHasSide hasSide && !ModOrganizer.LoadSide(hasSide.Side))
                {
                    continue;
                }

                var hookType = attribute.GetType().GetGenericArguments()[0];
                Subscribe(hookType, method, instance);
            }
        }
    }

    /// <summary>
    ///     Binds the <paramref name="bindingMethod"/> to the hook contained
    ///     within the <paramref name="hookType"/>. Looks for a public, static
    ///     event named <c>Event</c> within the <paramref name="hookType"/> to
    ///     bind to.  <paramref name="instance"/> to used as part of the binding
    ///     if it's available (must be provided if the
    ///     <paramref name="bindingMethod"/> is instanced, otherwise must be
    ///     <see langword="null"/>).
    ///     <br />
    ///     This supports flexibly binding <paramref name="bindingMethod"/>s
    ///     whose signature is not directly compatible with the target event.
    ///     <br />
    ///     A signature may omit a return type (instead returning
    ///     <see langword="void"/>) to implicitly invoke the <c>orig</c>
    ///     callback.  This implicit invocation is used to consume the return
    ///     value without using a user-provided value (particularly useful if
    ///     you intend to execute some code in a given step without much care
    ///     for the hook itself).
    ///     <br />
    ///     Furthermore, the initial <c>orig</c> and <c>self</c> parameters may
    ///     be omitted if they are not used.  Additional parameters must always
    ///     be provided.
    /// </summary>
    /// <param name="hookType">
    ///     The <see cref="Type"/> containing the event to bind to.
    /// </param>
    /// <param name="bindingMethod">
    ///     The <see cref="MethodInfo"/> being bound.
    /// </param>
    /// <param name="instance">
    ///     The instance of type containing the <paramref name="bindingMethod"/>
    ///     if the <paramref name="bindingMethod"/> is instanced, otherwise
    ///     <see langword="null"/>.
    /// </param>
    /// <exception cref="InvalidOperationException"></exception>
    private static void Subscribe(
        Type hookType,
        MethodInfo bindingMethod,
        object? instance
    )
    {
        var eventInfo = hookType.GetEvent("Event", BindingFlags.Public | BindingFlags.Static)
                     ?? throw new InvalidOperationException($"The type {hookType} does not have an event named Event.");

        var handlerType = eventInfo.EventHandlerType
                       ?? throw new InvalidOperationException($"The event {eventInfo} did not have an EventHandlerType.");

        var invokeMethod = handlerType.GetMethod("Invoke")
                        ?? throw new InvalidOperationException("The event handler type could not resolve an Invoke method.");

        // If we can bind directly then we don't need to build a wrapper.
        if (TryDirectBind(eventInfo, handlerType, bindingMethod, instance))
        {
            return;
        }

        var wrapper = BuildWrapper(handlerType, invokeMethod, bindingMethod, instance);
        eventInfo.AddEventHandler(null, wrapper);

        return;

        static bool TryDirectBind(
            EventInfo eventInfo,
            Type handlerType,
            MethodInfo bindingMethod,
            object? instance
        )
        {
            try
            {
                var handler = Delegate.CreateDelegate(handlerType, instance, bindingMethod, throwOnBindFailure: false);
                if (handler is null)
                {
                    return false;
                }

                eventInfo.AddEventHandler(null, handler);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    private static Delegate BuildWrapper(
        Type handlerType,
        MethodInfo invokeMethod,
        MethodInfo bindingMethod,
        object? instance
    )
    {
        var eventParameters = invokeMethod.GetParameters();
        var eventParameterExpressions = eventParameters
                                       .Select((x, i) => Expression.Parameter(x.ParameterType, "arg" + i))
                                       .ToArray();

        var targetParameters = bindingMethod.GetParameters();

        // Find the orig and self parameters, if present.  If orig is present,
        // it will always be the first parameter.  If self is present, it may be
        // the first parameter (if no orig parameter is present) or the second
        // (if an orig parameter is present).
        FindSpecialParameters(
            eventParameters,
            targetParameters,
            out var origParameter,
            out var selfParameter
        );

        var arguments = new List<Expression>();
        for (var i = 0; i < eventParameterExpressions.Length; i++)
        {
            var argExpr = eventParameterExpressions[i];

            switch (i)
            {
                case 0:
                {
                    if (origParameter is not null)
                    {
                        arguments.Add(argExpr);
                    }

                    break;
                }

                case 1:
                {
                    if (selfParameter is not null)
                    {
                        arguments.Add(argExpr);
                    }

                    break;
                }

                default:
                    arguments.Add(argExpr);
                    break;
            }
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

        var lambda = Expression.Lambda(handlerType, bodyExpr, eventParameterExpressions);
        return lambda.Compile();

        static void FindSpecialParameters(
            ParameterInfo[] eventParameters,
            ParameterInfo[] targetParameters,
            out ParameterInfo? origParameter,
            out ParameterInfo? selfParameter
        )
        {
            origParameter = null;
            selfParameter = null;

            var origType = eventParameters[0].ParameterType;
            var selfType = eventParameters[1].ParameterType;

            for (var i = 0; i < Math.Min(targetParameters.Length, 2); i++)
            {
                var currParameter = targetParameters[i];
                var currType = currParameter.ParameterType;

                if (i == 0)
                {
                    // Handle the first parameter.  Can either be orig or self.
                    // If it's neither, abort and act as if none were found.

                    if (currType == origType)
                    {
                        origParameter = currParameter;
                        continue;
                    }

                    if (currType == selfType)
                    {
                        selfParameter = currParameter;
                    }

                    break;
                }

                if (i == 1)
                {
                    // Handle the second parameter.  Can only be self if orig is
                    // set.

                    if (currType == selfType)
                    {
                        selfParameter = currParameter;
                    }
                }

                break;
            }
        }
    }
}

using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Daybreak.EarlyLoader;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Hooks;

internal static class HookLoader
{
#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void HookIntoEarlyLoads()
    {
        EarlyLoadHooks.OnLoadStatic += LoadStatic;
        EarlyLoadHooks.OnLoadInstance += LoadInstance;
        EarlyLoadHooks.OnUnloadStatic += UnloadStatic;
        EarlyLoadHooks.OnUnloadInstance += UnloadInstance;
    }

    private static void LoadStatic(Mod mod, Type type)
    {
        SubscribeToHooks(
            type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static),
            null
        );

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

            HookSubscriber.BuildWrapper<OnLoadHook.Definition>(method, null)
                          .Invoke(mod);
        }

        // TODO: LoaderUtils.ForEachAndAggregateExceptions(loadableTypes.Where(self.PreJITFilter.ShouldJIT), RunStaticConstructors);
    }

    private static void LoadInstance(Mod mod, ILoadable loadable)
    {
        SubscribeToHooks(
            loadable.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance),
            loadable
        );

        var methods = loadable.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
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

            HookSubscriber.BuildWrapper<OnLoadHook.Definition>(method, loadable)
                          .Invoke(mod);
        }
    }

    private static void UnloadStatic(Mod mod, Type type)
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

            HookSubscriber.BuildWrapper<OnUnloadHook.Definition>(method, null)
                          .Invoke(mod);
        }
    }

    private static void UnloadInstance(Mod mod, ILoadable loadable)
    {
        var methods = loadable.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
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

            HookSubscriber.BuildWrapper<OnUnloadHook.Definition>(method, loadable)
                          .Invoke(mod);
        }
    }
#pragma warning restore CA2255

    private static void SubscribeToHooks(MethodInfo[] methods, object? instance)
    {
        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes<BaseHookAttribute>(inherit: false);

            foreach (var attribute in attributes)
            {
                if (attribute is IHasSide hasSide && !ModOrganizer.LoadSide(hasSide.Side))
                {
                    continue;
                }

                attribute.Apply(method, instance);
            }
        }
    }
}

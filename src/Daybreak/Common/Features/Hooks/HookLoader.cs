using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        if (mod is ModLoaderMod)
        {
            orig(ModContent.GetInstance<ModImpl>());
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

    private static void Subscribe(Type hookType, MethodInfo method, object? instance)
    {
        var eventInfo = hookType.GetEvent("Event");
        if (eventInfo is null)
        {
            throw new InvalidOperationException($"The type {hookType} does not have an event named Event.");
        }

        var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType!, instance, method);
        if (handler is null)
        {
            throw new InvalidOperationException($"The method {method} is not compatible with the event {eventInfo}.");
        }

        eventInfo.AddEventHandler(null, handler);
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using MonoMod.Cil;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.Default;

namespace Daybreak.EarlyLoader;

/// <summary>
///     Exposes convenient early loading hooks for mods.
/// </summary>
public static class EarlyLoadHooks
{
    private static Mod? currentlyLoadingMod;

    private static readonly Dictionary<Assembly, Type[]> loadable_types = [];

    /// <summary>
    ///     Used to hook into the start of mod loading; useful for injecting
    ///     content into other mods.
    /// </summary>
    public static event Action<Mod>? OnEarlyModLoad;

    /// <summary>
    ///     Used to hook into the start of mod unloading; useful for terminating
    ///     systems early.
    /// </summary>
    public static event Action<Mod>? OnEarlyModUnload;

    /// <summary>
    ///     Invoked whenever a type is loaded.
    /// </summary>
    public static event Action<Mod, Type>? OnLoadStatic;

    /// <summary>
    ///     Invoked whenever an instance is loaded.
    /// </summary>
    public static event Action<Mod, ILoadable>? OnLoadInstance;

    /// <summary>
    ///     Invoked whenever a type is unloaded.
    /// </summary>
    public static event Action<Mod, Type>? OnUnloadStatic;

    /// <summary>
    ///     Invoked whenever an instance is unloaded.
    /// </summary>
    public static event Action<Mod, ILoadable>? OnUnloadInstance;

    /// <summary>
    ///     Returns the currently loading mod or throws an exception when there
    ///     is none.
    /// </summary>
    public static Mod GetModOrThrow()
    {
        return currentlyLoadingMod ?? throw new InvalidOperationException("Cannot continue with operation as no mod is currently applicable to load content through Mod::AddContent");
    }

#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void HookIntoContentLoadingRoutine()
    {
        // Hijack the content loading process to allow us to handle every added
        // loadable.  For every loadable, resolve instanced hooks and apply
        // them.
        MonoModHooks.Add(
            typeof(Mod).GetMethods().Single(x => x.Name.Equals(nameof(Mod.AddContent)) && !x.IsGenericMethod),
            AddContent_Instanced_OnLoads
        );

        MonoModHooks.Add(
            typeof(Mod).GetMethod(nameof(Mod.Autoload), BindingFlags.NonPublic | BindingFlags.Instance)!,
            Autoload_Static_OnLoads
        );

        /*
        MonoModHooks.Add(
            typeof(MenuLoader).GetMethod(nameof(MenuLoader.Unload), BindingFlags.NonPublic | BindingFlags.Static)!,
            Unload_CallOnUnloads
        );
        */

        MonoModHooks.Add(
            typeof(MonoModHooks).GetMethod(nameof(MonoModHooks.RemoveAll), BindingFlags.NonPublic | BindingFlags.Static)!,
            RemoveAll_PostponeRemovingOurHooks
        );

        MonoModHooks.Modify(
            typeof(Mod).GetMethod(nameof(Mod.UnloadContent), BindingFlags.NonPublic | BindingFlags.Instance)!,
            UnloadContent_CallAllOnUnloads
        );

        MonoModHooks.Add(
            typeof(Mod).GetMethod(nameof(Mod.UnloadContent), BindingFlags.NonPublic | BindingFlags.Instance)!,
            UnloadContent_WrapToMarkUnloadingMod
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

    private static bool ModIsEligibleForSpecialLoading(Mod mod)
    {
        // TODO: Could also walk the weakReferences/modReferences trees?
        return mod.Name == ModuleLoader.OwningMod || mod.Code.GetReferencedAssemblies().Any(x => x.Name == ModuleLoader.OwningMod);
    }

    private static bool AddContent_Instanced_OnLoads(Func<Mod, ILoadable, bool> orig, Mod self, ILoadable instance)
    {
        // Only attempt to resolve and apply hooks if the instance actually
        // loaded...
        if (!orig(self, instance))
        {
            return false;
        }

        OnLoadInstance?.Invoke(self, instance);
        return true;
    }

    private static void Autoload_Static_OnLoads(Action<Mod> orig, Mod self)
    {
        orig(self);

        if (self.Code is null || !ModIsEligibleForSpecialLoading(self))
        {
            return;
        }

        var loadableTypes = loadable_types[self.Code] =
            AssemblyManager.GetLoadableTypes(self.Code)
                           .Where(x => !x.IsEnum && !x.IsSubclassOf(typeof(MulticastDelegate)) && AutoloadAttribute.GetValue(x).NeedsAutoloading)
                           .OrderBy(x => x.FullName, StringComparer.InvariantCulture)
                           .ToArray();

        LoaderUtils.ForEachAndAggregateExceptions(loadableTypes, x => OnLoadStatic?.Invoke(self, x));
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

    private static void RemoveAll_PostponeRemovingOurHooks(Action<Mod> orig, Mod mod)
    {
        if (mod.Name == ModuleLoader.OwningMod)
        {
            return;
        }

        // Force our edits to unload when it gets to ModLoaderMod instead (late
        // stage).
        // Use TryGetMod because GetInstance may return null in early unload stages.
        if (mod is ModLoaderMod && ModLoader.TryGetMod(ModuleLoader.OwningMod, out var modImpl))
        {
            orig(modImpl);

            // DAYBREAK used to return here, skipping RemoveAll for ModLoaderMod
            // since it's a no-op.  DON'T DO THIS HERE.  This detour may be
            // layered now by multiple mods including this module.
        }

        orig(mod);
    }

    private static void UnloadContent_CallAllOnUnloads(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.After, x => x.MatchCallvirt<Mod>(nameof(Mod.Unload)));

        c.EmitLdarg0();
        c.EmitDelegate(
            static (Mod mod) =>
            {
                if (!loadable_types.TryGetValue(mod.Code, out var loadableTypes))
                {
                    return;
                }

                loadableTypes = loadableTypes.AsEnumerable().Reverse().ToArray();

                LoaderUtils.ForEachAndAggregateExceptions(Enumerable.Reverse(loadableTypes), x => OnUnloadStatic?.Invoke(mod, x));
            }
        );

        c.GotoNext(MoveType.Before, x => x.MatchCallvirt<ILoadable>(nameof(ILoadable.Unload)));

        c.EmitDup();
        c.EmitLdarg0(); // Mod this
        c.EmitDelegate(
            static (ILoadable loadable, Mod self) =>
            {
                OnUnloadInstance?.Invoke(self, loadable);
            }
        );
    }

    private static void UnloadContent_WrapToMarkUnloadingMod(Action<Mod> orig, Mod mod)
    {
        OnEarlyModUnload?.Invoke(mod);
        orig(mod);
    }
}

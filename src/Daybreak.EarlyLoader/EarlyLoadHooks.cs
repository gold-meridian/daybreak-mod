using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mono.Cecil;
using MonoMod.Cil;
using MonoMod.Utils;
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

        
        var lmc = GetFirstLoadModContentCallback();
        MonoModHooks.Modify(
            lmc,
            il =>
            {
                // Used to detour AutoloadConfig and
                // EnsureResizeArraysAttributeStaticCtorsRun, since they were
                // the first and last methods ran when Mod::loading was true.
                // This fell through when AutoloadConfig seemingly started
                // getting inlined, so we use an IL edit now.

                var c = new ILCursor(il);

                c.GotoNext(MoveType.After, x => x.MatchStfld<Mod>(nameof(Mod.loading)));
                c.EmitLdarg1();
                c.EmitDelegate(
                    (Mod mod) =>
                    {
                        Debug.Assert(currentlyLoadingMod is null);

                        currentlyLoadingMod = mod;
                        OnEarlyModLoad?.Invoke(mod);
                    }
                );

                c.GotoNext(MoveType.Before, x => x.MatchStfld<Mod>(nameof(Mod.loading)));
                c.EmitDelegate(
                    (Mod mod) =>
                    {
                        Debug.Assert(currentlyLoadingMod == mod);
                        currentlyLoadingMod = null;

                        return mod;
                    }
                );
            }
        );
    }
#pragma warning restore CA2255

    private static MethodBase GetFirstLoadModContentCallback()
    {
        using var lmcDef = new DynamicMethodDefinition(typeof(ModContent).GetMethod(nameof(ModContent.Load), BindingFlags.NonPublic | BindingFlags.Static)!);
        using var il = new ILContext(lmcDef.Definition);

        var c = new ILCursor(il);

        var methodRef = default(MethodReference)!;
        c.GotoNext(x => x.MatchCall(typeof(ModContent), nameof(ModContent.LoadModContent)));
        c.GotoPrev(x => x.MatchLdftn(out methodRef));

        return typeof(ModContent).Module.ResolveMethod(methodRef.Resolve().MetadataToken.ToInt32())!;
    }

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

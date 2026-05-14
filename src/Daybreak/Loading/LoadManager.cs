using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Loading;

/// <summary>
///     Responsible for carrying out various loading tasks for Daybreak and its
///     module management.
/// </summary>
internal static partial class LoadManager
{
    private sealed record ModuleSettings(string? ParentMod);

    // The assembly currently being loaded.  Does not stay in sync with actual
    // mod loading.
    private static string? AssemblyBeingLoaded { get; set; } = "Daybreak";

    private static readonly ConditionalWeakTable<Assembly, ModuleSettings> module_settings = [];
    private static readonly Dictionary<string, List<WeakReference<Assembly>>> assembly_modules = [];

#pragma warning disable CA2255
    [ModuleInitializer]
    public static void Setup()
    {
        // dllReferences are actually loaded before the main mod assembly,
        // meaning modules provided by Daybreak would get loaded before Daybreak
        // and before this initializer is run.  We have to just maintain a list
        // manually and trigger their assembly loads after the fact instead to
        // make up for this.
        // Dependent mods which use modules should always be loading after
        // Daybreak anyway (as they should main a modReference or weakReference
        // on it), which makes this safe for other mods.

        // ModLoadContext.LoadAssemblies is how we determine the
        // currently loading assembly.  We're already in this method when
        // Daybreak's module initializers are run, so we can't rely on it for
        // us, but we can obviously just assume we're the first assembly we see
        // (hence setting AssemblyBeingLoaded to Daybreak on initialization).
        MonoModHooks.Add(
            typeof(AssemblyManager.ModLoadContext).GetMethod(nameof(AssemblyManager.ModLoadContext.LoadAssemblies), BindingFlags.Public | BindingFlags.Instance)!,
            (Action<AssemblyManager.ModLoadContext> orig, AssemblyManager.ModLoadContext self) =>
            {
                AssemblyBeingLoaded = self.Name;
                orig(self);
            }
        );

        // Now actually load our modules.
        LoadDefaultModules();

        // Despite this being called in the middle of LoadAssemblies, it should
        // still be before GetLoadableTypes, so this is safe.
        MonoModHooks.Add(
            typeof(AssemblyManager).GetMethod(nameof(AssemblyManager.GetLoadableTypes), BindingFlags.NonPublic | BindingFlags.Static, [typeof(AssemblyManager.ModLoadContext), typeof(MetadataLoadContext)])!,
            (Func<AssemblyManager.ModLoadContext, MetadataLoadContext, Dictionary<Assembly, Type[]>> orig, AssemblyManager.ModLoadContext mod, MetadataLoadContext mlc) =>
            {
                // The current approach is just to merge the modules into the
                // main assembly's type array and then clear the modules' types.
                // The other option is to manually hook into other calls to
                // GetLoadableTypes and intercept there instead.
                // This current approach means all callers will always get the
                // merged array, which is probably preferable?

                var typeMap = orig(mod, mlc);

                if (mod.Name is null || !assembly_modules.TryGetValue(mod.Name, out var modules))
                {
                    return typeMap;
                }

                var totalTypes = typeMap[mod.assembly].ToList();
                foreach (var moduleRef in modules)
                {
                    if (!moduleRef.TryGetTarget(out var module))
                    {
                        continue;
                    }

                    totalTypes.AddRange(typeMap[module]);
                    typeMap[module] = [];
                }

                typeMap[mod.assembly] = totalTypes.ToArray();
                return typeMap;
            }
        );
    }
#pragma warning restore CA2255

    /// <summary>
    ///     Registers a module.
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="parentMod"></param>
    public static void RegisterModule(Assembly assembly, string? parentMod)
    {
        // This shouldn't really be possible, but ALC::Name can technically be
        // null.
        if (AssemblyBeingLoaded is null)
        {
            throw new InvalidOperationException($"AssemblyBeingLoaded is somehow null; can't load module: '{assembly.GetName().Name ?? assembly.FullName}'");
        }

        RegisterModule(AssemblyBeingLoaded, assembly, parentMod);
    }

    private static void RegisterModule(string loadingMod, Assembly assembly, string? parentMod)
    {
        if (parentMod is not null && parentMod != loadingMod)
        {
            throw new InvalidOperationException(
                $"An error occurred loading a DAYBREAK module. THIS IS *NOT* DAYBREAK'S FAULT, it is likely the fault of: {loadingMod}"
              + $"\n\nAttempted to load a Daybreak-style module into an unexpected mod; the module may only be loaded by '{parentMod}', but it was attempted to be loaded by '{loadingMod}'."
              + $"\n\nIf you are a player, report this to the '{loadingMod}' mod's developers."
              + $"\n\nIf you are a developer, you are probably dllReferencing a module expected to be loaded by a single canonical mod. If you don't know how to fix this, contact a DAYBREAK developer through the mod homepage."
            );
        }

        module_settings.AddOrUpdate(assembly, new ModuleSettings(parentMod));

        if (!assembly_modules.TryGetValue(loadingMod, out var modules))
        {
            assembly_modules[loadingMod] = modules = [];
        }

        modules.Add(new WeakReference<Assembly>(assembly));
    }
}

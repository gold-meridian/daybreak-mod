using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using JetBrains.Annotations;
using log4net;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak;

/// <summary>
///     A DAYBREAK Module is a special kind of assembly, which has unique
///     loading behavior.
///     <br />
///     Inclusion of this type in your assembly implicitly opts you into this
///     behavior, allowing certain on-module-load actions to be configured.
/// </summary>
[UsedImplicitly]
[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class DaybreakModuleAttribute : Attribute
{
    /// <summary>
    ///     If <see langword="true"/>, this module should participate in the
    ///     load cycle of the mod that loads it.  This means it will be part of
    ///     <see cref="AssemblyManager.GetLoadableTypes(Assembly)"/> and will
    ///     have its special types (e.g. <see cref="ILoadable"/>) handled
    ///     accordingly.
    /// </summary>
    public bool UseModLoadCycle { get; init; } = false;

    /// <summary>
    ///     The mod that owns this module.  If set, then the module may only be
    ///     loaded by that mod.  This is useful for enforcing exclusivity of an
    ///     implementation, distributing references to consuming mods while
    ///     preventing them from accidentally loading the assembly themselves.
    /// </summary>
    public string? OwningMod { get; init; } = null;

#pragma warning disable CA2255
    [ModuleInitializer]
    public static void VerifyAndRegisterModule()
    {
        var asm = typeof(DaybreakModuleAttribute).Assembly;
        var settings = asm.GetCustomAttribute<DaybreakModuleAttribute>();

        // No configuration, just treat it as a regular assembly.  Whatever.
        // TODO: This may be better considered an "error" if the user has chosen
        //       to include it?
        if (settings is null)
        {
            return;
        }

        ModuleLoader.LoadModule(settings);
    }
#pragma warning restore CA2255
}

/// <summary>
///     Responsible for handling the loading of the DAYBREAK Module.
/// </summary>
internal static class ModuleLoader
{
    public static ILog Logger { get; } = LogManager.GetLogger(typeof(ModuleLoader));

    public static string? OwningMod { get; private set; }

    private static readonly Assembly module_assembly = typeof(ModuleLoader).Assembly;

    public static void LoadModule(DaybreakModuleAttribute moduleSettings)
    {
        Logger.Debug($"Loading module: {module_assembly.GetName().FullName}");

        var expectedMod = moduleSettings.OwningMod;
        if (expectedMod is not null)
        {
            Logger.Debug($"Module expects to be loaded by mod: {expectedMod}");
        }

        if (AssemblyLoadContext.GetLoadContext(module_assembly) is not { } alc)
        {
            throw new InvalidOperationException("Failed to load module; could not resolve owning AssemblyLoadContext!");
        }

        OwningMod = alc.Name;
        Logger.Debug($"Module is being loaded by: {OwningMod}");
        if (expectedMod is not null && !string.Equals(OwningMod, expectedMod, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new InvalidOperationException(
                $"The mod '{OwningMod}' failed to load due to misconfigured DAYBREAK-style modules."
              + $"\'{OwningMod}' attempted to load the module '{module_assembly.GetName().FullName}', but the module may only be loaded by '{expectedMod}'!"
              + $"\n\nIf you are a player, simply report this to the developers of '{OwningMod}'."
              + $"\n\nIf you're a developer, you have attempted to load an assembly owned by another mod.  Do not reference this module with dllReferences."
              + $"\nIf you're a developer and need help fixing this, talk to the DAYBREAK developers in the NIGHTSHADE Discord: discord.gg/yog"
            );
        }

        if (moduleSettings.UseModLoadCycle)
        {
            Logger.Debug("Module has requested to participate in the mod load cycle.");
            InjectIntoLoadCycle();
        }
        else
        {
            Logger.Debug("Module has not requested to participate in the mod load cycle.");
        }
    }

    private static void InjectIntoLoadCycle()
    {
        Logger.Debug("Injecting into the mod load cycle...");

        if (OwningMod is null)
        {
            throw new InvalidOperationException($"Module had no resolved loading mod; {nameof(OwningMod)} was null!");
        }

        MonoModHooks.Add(
            typeof(AssemblyManager).GetMethod(nameof(AssemblyManager.GetLoadableTypes), BindingFlags.NonPublic | BindingFlags.Static, [typeof(AssemblyManager.ModLoadContext), typeof(MetadataLoadContext)])!,
            (Func<AssemblyManager.ModLoadContext, MetadataLoadContext, Dictionary<Assembly, Type[]>> orig, AssemblyManager.ModLoadContext mod, MetadataLoadContext mlc) =>
            {
                var typeMap = orig(mod, mlc);
                if (OwningMod != mod.Name)
                {
                    return typeMap;
                }

                if (!typeMap.TryGetValue(module_assembly, out var types))
                {
                    Logger.Warn($"Failed to find assembly types in GetLoadableTypes type map for mod: {OwningMod}");
                    return typeMap;
                }

                // Clear it out to avoid duplicated logic but also to avoid
                // null references, since I think a mod could try to request
                // loadable types of our assembly by checking
                // ModLoadContext::assemblies.
                typeMap[module_assembly] = [];

                // Actually add our types to the main mod.  This is a little
                // gross due to ordering, but tModLoader makes no guarantees
                // about it, and we can call it an implementation detail.
                typeMap[mod.assembly] = typeMap[mod.assembly].Concat(types).ToArray();

                return typeMap;
            }
        );
    }
}

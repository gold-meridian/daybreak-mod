using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Terraria.ModLoader.Core;

namespace Daybreak;

/// <summary>
///     Decorated assemblies are known to participate in DAYBREAK's dependency
///     cycle loading.
///     <br />
///     This enables an assembly to be included in the loading cycle of a parent
///     mod as if it were part of the same assembly.
/// </summary>
/// <param name="loadCycle">
///     Whether the assembly actually participates in the including mod's load
///     cycle.
/// </param>
/// <param name="parentMod">
///     If specified, indicates this module is only valid to be loaded by the
///     named mod.  Use this for modules that may be dependencies of other mods
///     but may not be directly loaded by them (instead having to be provided by
///     the parent mod).
/// </param>
[UsedImplicitly]
[AttributeUsage(AttributeTargets.Assembly)]
internal sealed class DaybreakModuleAttribute(bool loadCycle = true, string? parentMod = null) : Attribute
{
    private static readonly DaybreakModuleAttribute default_attribute = new(loadCycle: false, parentMod: null);

    private bool ParticipatesInLoadCycle => loadCycle;

    private string? ParentMod => parentMod;

    private const string daybreak_mod = "Daybreak";
    private const string daybreak_load_manager_type = "Daybreak.Loading.LoadManager";
    private const string load_manager_register_module_method = "RegisterModule";

#pragma warning disable CA2255
    [ModuleInitializer]
    public static void VerifyAndRegisterModule()
    {
        var asm = typeof(DaybreakModuleAttribute).Assembly;
        var settings = asm.GetCustomAttribute<DaybreakModuleAttribute>()
                    ?? default_attribute;

        // Nothing to do.  If the module doesn't participate in the load cycle,
        // it's not actually a Daybreak-style module and does not need special
        // handling (nor does Daybreak need to know about its presence).
        if (!settings.ParticipatesInLoadCycle)
        {
            return;
        }

        // ModuleInitializers run early enough in the load process that
        // ModLoader.HasMod and related APIs simply won't work.  We can get
        // around this by manually fiddling with the assembly load contexts.
        if (!AssemblyManager.loadedModContexts.TryGetValue(daybreak_mod, out var daybreakAlc))
        {
            // This is primarily a safeguard against improper dependency
            // management, typically a result of including modules in
            // dllReferences when it's inappropriate.  Any modules that mark
            // loadCycle as true (default) should never be dllReferences'd by
            // anything other than the mod whose responsibility it is to
            // distribute the module to other mods.
            // That means this error will basically only throw when a dependent
            // mod includes a module as a dllReference, which it should never
            // do.
            // If a mod is strongly referencing Daybreak (or whatever mod is
            // providing the module they need), they only need to modReference
            // or weakReference the providing mod, as dependencies will be
            // transiently provided.
            // If a mod fails to load with weak references alone, you just need
            // to consult the tModLoader Expert Cross Mod Guide. It will teach
            // you how to only load assemblies when they're actually needed.
            throw new InvalidOperationException(
                "An error occurred loading a DAYBREAK module. THIS IS *NOT* DAYBREAK'S FAULT, tModLoader should indicate the mod that made the mistake."
              + "\n\nAttempted to load a Daybreak-style module without Daybreak loaded; this typically means the offending mod is not properly referencing one or more dependencies."
              + "\n\nIf you are a player, report this to the currently-loading mod, which tModLoader should indicate."
              + "\n\nIf you are the developer, you're referencing a module in dllReferences when you shouldn't be. If you don't know how to fix this, contact a DAYBREAK developer through the mod homepage."
            );
        }

        // Transfer handling to the loaded Daybreak copy now, so we can record
        // the module's presence.
        daybreakAlc.assembly
                   .GetType(daybreak_load_manager_type)!
                   .GetMethod(load_manager_register_module_method, BindingFlags.Public | BindingFlags.Static, [typeof(Assembly), typeof(string)])!
                   .Invoke(null, [asm, settings.ParentMod]);
    }
#pragma warning restore CA2255
}

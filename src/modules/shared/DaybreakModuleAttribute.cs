using System;
using JetBrains.Annotations;

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
    public bool ParticipatesInLoadCycle => loadCycle;

    public string? ParentMod => parentMod;
}

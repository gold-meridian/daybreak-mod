using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Daybreak.Common.Features.ModCalls;

/// <summary>
///     Used to mark a method as a handler of cross-mod API call.
/// </summary>
[MeansImplicitUse]
[AttributeUsage(AttributeTargets.Method)]
public sealed class ModCallAttribute(string[] aliases) : Attribute
{
    /// <summary>
    ///     A collection of (interpreted-as-case-insensitive) aliases for this
    ///     mod call.
    /// </summary>
    public IReadOnlyCollection<string> Aliases { get; } = aliases;
}
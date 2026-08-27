using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

/// <summary>
///     Implement this interface on your <see cref="Mod"/> implementation to
///     control how default names are generated for <see cref="ModType"/>s and
///     related APIs.
///     <br />
///     This API is suited for generating more unique names across broad types
///     rather than per-instance.
/// </summary>
public interface INameProvider
{
    /// <summary>
    ///     Gets the preferred name for the <paramref name="type"/>.
    /// </summary>
    /// <param name="type">The type to get a unique name for.</param>
    string GetName(Type type);
}

/// <summary>
///     Utilities for getting names from name providers.
/// </summary>
public static class NameProvider
{
    /// <summary>
    ///     Gets the name for the type, using the
    ///     <paramref name="mod"/>-provided <see cref="INameProvider"/>
    ///     implementation if available.
    /// </summary>
    public static string GetName(Mod mod, Type type)
    {
        if (mod is INameProvider nameProvider)
        {
            return nameProvider.GetName(type);
        }

        return GetDefaultName(type);
    }

    /// <summary>
    ///     The default name for this type, which is <see cref="MemberInfo.Name"/>.
    /// </summary>
    public static string GetDefaultName(Type type)
    {
        return type.Name;
    }

    /// <summary>
    ///     Generates a single name prefixed with parent type names.  The name
    ///     is concatenated with no additional characters.
    /// </summary>
    public static string GetNestedName(Type type, string? name = null)
    {
        var typeName = name ?? GetDefaultName(type);
        var nestedNames = GetNestedNames(type).Append(typeName);

        var nestedName = string.Join("", nestedNames);
        return nestedName;
    }

    private static IEnumerable<string> GetNestedNames(Type type)
    {
        var names = new List<string>();

        while (type.DeclaringType is not null)
        {
            names.Add(type.DeclaringType.Name);
            type = type.DeclaringType;
        }

        return names.AsEnumerable().Reverse();
    }

#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void HijackDefaultModTypeName()
    {
        // This is kind of evil, but it's quite useful!  Should be safe to hook
        // this property getter because it's virtual (and we only need it to
        // apply for this assembly and any assemblies loaded after).
        // If inlining is somehow an issue, we can explore other options.
        MonoModHooks.Add(
            typeof(ModType).GetProperty(nameof(ModType.Name))!.GetMethod!,
            (ModType self) => GetName(self.Mod, self.GetType())
        );
    }
#pragma warning restore CA2255
}

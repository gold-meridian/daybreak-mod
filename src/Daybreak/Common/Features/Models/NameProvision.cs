using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

/// <summary>
///     Utilities for getting names from name providers.
/// </summary>
public static class NameProvider
{
    /// <summary>
    ///     Returns the name provided by an
    ///     <see cref="AbstractNameProviderAttribute"/> if the type is annotated
    ///     with one, otherwise <see cref="Type.Name"/>.
    /// </summary>
    public static string ForType(Type type)
    {
        var nameProvider = type.GetCustomAttribute<AbstractNameProviderAttribute>();
        return nameProvider?.GetName(type) ?? GetDefaultName(type);
    }

    /// <summary>
    ///     The default name for this type, which is <see cref="Type.Name"/>.
    /// </summary>
    public static string GetDefaultName(Type type)
    {
        return type.Name;
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
            (ModType self) => ForType(self.GetType())
        );
    }
#pragma warning restore CA2255
}

/// <summary>
///     Used to provide a name to <see cref="BoundDataProvider{TProvider}"/> or
///     <see cref="ModType"/>s.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public abstract class AbstractNameProviderAttribute : Attribute
{
    /// <summary>
    ///     Produces a name for the given type.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>The name.</returns>
    public abstract string GetName(Type type);
}

/// <summary>
///     Provides the given <paramref name="name"/> as the name.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
public sealed class NameProviderAttribute(string name) : AbstractNameProviderAttribute
{
    /// <inheritdoc />
    public override string GetName(Type type)
    {
        return name;
    }
}

/// <summary>
///     Provides a name including the parent types walking up a nested type tree
///     to generate a reliable, unique name for commonly-named nested types.
/// </summary>
/// <param name="name">
///     The name.  If left <see langword="null"/>, the type's name will be used.
/// </param>
public sealed class NestedTypeNameProviderAttribute(string? name = null) : AbstractNameProviderAttribute
{
    /// <inheritdoc />
    public override string GetName(Type type)
    {
        var typeName = name ?? NameProvider.GetDefaultName(type);
        var nestedNames = GetNestedNames(type).Append(typeName);
        
        // Normally I'd use '+', but we need '_' to generate more C#-compatible
        // names.
        var nestedName = string.Join('_', nestedNames);
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
}

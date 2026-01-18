using Daybreak.Common.Features.Hooks;
using Daybreak.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Common.Features.Configuration;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
internal class DefaultConfigElementForAttribute<T>() : DefaultConfigElementForAttribute(typeof(T));

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
internal class DefaultConfigElementForAttribute(Type type) : Attribute
{
    public Type Type { get; } = type;
}

internal static class DefaultConfigElementLoader
{
    private static readonly Dictionary<Type, Type> types_by_element_type = [];

    [OnLoad]
    private static void Load(Mod mod)
    {
        var loadableTypes = AssemblyManager.GetLoadableTypes(mod.Code)
                                           .Where(x => AutoloadAttribute.GetValue(x).NeedsAutoloading)
                                           .OrderBy(x => x.FullName, StringComparer.InvariantCulture)
                                           .ToArray();

        LoaderUtils.ForEachAndAggregateExceptions(loadableTypes, AddElement);
    }

    private static void AddElement(Type type)
    {
        if (!type.IsAssignableTo(typeof(ConfigElement)))
        {
            return;
        }

        var attributes = type.GetCustomAttributes<DefaultConfigElementForAttribute>(inherit: false);

        foreach (var attribute in attributes)
        {
            types_by_element_type.Add(attribute.Type, type);
        }
    }

    internal static bool GetConfigElement(Type type, IConfigEntry entry, bool showIcon, [NotNullWhen(true)] out ConfigElement? instance)
    {
        instance = null;

        foreach (var item in types_by_element_type)
        {
            if (!type.IsAssignableTo(item.Key))
            {
                continue;
            }

            instance = (ConfigElement)Activator.CreateInstance(item.Value, entry, showIcon)!;
            return true;
        }

        return false;
    }
}


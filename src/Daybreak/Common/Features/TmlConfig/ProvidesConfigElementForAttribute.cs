using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Daybreak.Common.Features.Hooks;
using MonoMod.Cil;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.Core;
using Terraria.UI;

namespace Daybreak.Common.Features.TmlConfig;

/// <inheritdoc />
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class ProvidesConfigElementForAttribute<T>() : ProvidesConfigElementForAttribute(typeof(T));

/// <summary>
///     Annotate a <see cref="ConfigElement"/> implemnentation to make it the
///     <paramref name="type"/>'s default element implementation.
///     <br />
///     Essentially the inverse of <see cref="CustomModConfigItemAttribute"/> in
///     functionality, allowing you to set a default behavior for a type.  Does
///     not override the usage of <see cref="CustomModConfigItemAttribute"/> in
///     use code.
/// </summary>
/// <param name="type">
///     The type to give a default element implementation to.
/// </param>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class ProvidesConfigElementForAttribute(Type type) : Attribute
{
    /// <summary>
    ///     The type.
    /// </summary>
    public Type Type { get; } = type;
}

internal static class ProvidesConfigElementForAttributeLoader
{
    private static readonly Dictionary<Type, Type> types_by_element_type = [];

#pragma warning disable CA2255
    [ModuleInitializer]
    public static void HookModLoading()
    {
        MonoModHooks.Add(
            typeof(Mod).GetMethod(nameof(Mod.Autoload), BindingFlags.NonPublic | BindingFlags.Instance),
            Autoload_SearchForAttributes
        );
    }
#pragma warning restore CA2255

    [OnLoad]
    private static void ApplyHooks()
    {
        MonoModHooks.Modify(
            typeof(UIModConfig).GetMethod(nameof(UIModConfig.WrapIt), BindingFlags.Public | BindingFlags.Static)!,
            WrapIt_UseNewDefaultElements
        );
    }

    private static void Autoload_SearchForAttributes(Action<Mod> orig, Mod self)
    {
        orig(self);

        if (self.Code is null)
        {
            return;
        }

        var loadableTypes = AssemblyManager.GetLoadableTypes(self.Code)
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

        var attributes = type.GetCustomAttributes<ProvidesConfigElementForAttribute>(inherit: false);

        foreach (var attribute in attributes)
        {
            types_by_element_type.Add(attribute.Type, type);
        }
    }

    private static void WrapIt_UseNewDefaultElements(ILContext il)
    {
        var c = new ILCursor(il);

        var typeIdx = -1;
        var elementIdx = -1;

        ILLabel? jumpIfChain = null;
        c.GotoNext(
            MoveType.Before,
            i => i.MatchNewobj<ItemDefinitionElement>(),
            i => i.MatchStloc(out elementIdx),
            i => i.MatchBr(out jumpIfChain)
        );

        c.GotoPrev(
            MoveType.Before,
            i => i.MatchLdloc(out typeIdx),
            i => i.MatchLdtoken<ItemDefinition>()
        );

        c.MoveAfterLabels();

        c.EmitLdloc(typeIdx);
        c.EmitLdloca(elementIdx);

        c.EmitDelegate(
            (Type type, ref UIElement element) =>
            {
                if (!GetConfigElement(type, out var instance))
                {
                    return false;
                }

                element = instance;
                return true;
            }
        );

        c.EmitBrtrue(jumpIfChain);
    }

    private static bool GetConfigElement(Type type, [NotNullWhen(true)] out UIElement? instance)
    {
        instance = null;

        foreach (var item in types_by_element_type)
        {
            if (!type.IsAssignableTo(item.Key))
            {
                continue;
            }

            instance = (UIElement)Activator.CreateInstance(item.Value)!;
            return true;
        }

        return false;
    }
}

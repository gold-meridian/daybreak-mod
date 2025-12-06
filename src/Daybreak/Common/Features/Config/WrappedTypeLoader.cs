using Daybreak.Common.Features.Hooks;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.Core;
using Terraria.UI;

namespace Daybreak.Common.Features.Config;

// Essentially allows a type and its corresponding ConfigElement to be placed into UIModConfig.WrapIt, could be used over CustomModConfigItemAttribute.
internal static class WrappedTypeLoader
{
    private static ILHook? patchWrapIt;

    private static readonly Dictionary<Type, Type> typesByElementType = [];

#pragma warning disable CA2255

    [ModuleInitializer]
    public static void HookModLoading()
    {
        MethodInfo? autoload = typeof(Mod).GetMethod(nameof(Mod.Autoload), BindingFlags.NonPublic | BindingFlags.Instance);

        if (autoload is not null)
        {
            MonoModHooks.Add(autoload, OnAutoload);
        }
    }

    [OnLoad]
    private static void Load()
    {
        MethodInfo? wrapIt = typeof(UIModConfig).GetMethod(nameof(UIModConfig.WrapIt), BindingFlags.Public | BindingFlags.Static);

        if (wrapIt is not null)
        {
            patchWrapIt = new(wrapIt, ILWrapIt);
        }
    }

    [OnUnload]
    private static void Unload()
    {
        patchWrapIt?.Dispose();
    }

    private static void OnAutoload(Action<Mod> orig, Mod self)
    {
        orig(self);

        if (self.Code is not null)
        {
            Type[] enumerable = (from x in AssemblyManager.GetLoadableTypes(self.Code)
                                 where AutoloadAttribute.GetValue(x).NeedsAutoloading
                                 select x).OrderBy(x => x.FullName!, StringComparer.InvariantCulture).ToArray();

            LoaderUtils.ForEachAndAggregateExceptions(enumerable, AddElement);
        }
    }

    private static void AddElement(Type type)
    {
        if (!type.IsAssignableTo(typeof(ConfigElement)))
            return;

        // TODO: Allow multiple types to be under one config element.
        var attribute = type.GetCustomAttributes<WrappedTypeAttribute>(false).First();

        typesByElementType.Add(type, attribute.Type);
    }

    private static void ILWrapIt(ILContext il)
    {
        try
        {
            ILCursor c = new(il);

            ILLabel? jumpIfChain = c.DefineLabel();

            int typeIndex = -1; // loc
            int elementIndex = -1; // loc

            c.GotoNext(MoveType.Before,
                i => i.MatchNewobj<ItemDefinitionElement>(),
                i => i.MatchStloc(out elementIndex),
                i => i.MatchBr(out jumpIfChain));

            c.GotoPrev(MoveType.Before,
                i => i.MatchLdloc(out typeIndex),
                i => i.MatchLdtoken<ItemDefinition>());

            c.MoveAfterLabels();

            c.EmitLdloc(typeIndex);
            c.EmitLdloc(elementIndex);

            c.EmitDelegate((Type type, UIElement element) =>
            {
                if (typesByElementType.TryGetValue(type, out var value))
                {
                    element = (UIElement)Activator.CreateInstance(value)!;

                    return true;
                }

                return false;
            });

            c.EmitBrtrue(jumpIfChain);
        }
        catch (Exception e)
        {
            throw new ILPatchFailureException(ModContent.GetInstance<ModImpl>(), il, e);
        }
    }
}

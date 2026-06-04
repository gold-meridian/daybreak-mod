using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Daybreak.EarlyLoader;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Contracts;

internal static partial class ContractEnforcer
{
    private static readonly Dictionary<nint, bool> type_runs_cctor = [];

#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void HookIntoEarlyLoads()
    {
        EarlyLoadHooks.OnLoadStatic += (_, type) =>
        {
            RunStaticConstructors(type);
        };

        EarlyLoadHooks.OnLoadInstance += (_, loadable) =>
        {
            ValidateLoadable(loadable);
        };
    }
#pragma warning restore CA2255

    private static void ValidateLoadable(ILoadable loadable)
    {
        if (!ModCompile.DeveloperMode)
        {
            return;
        }

        CheckCloneabilityContracts(loadable);
    }

    private static void RunStaticConstructors(Type type)
    {
        var fields = type.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields)
        {
            var handle = field.FieldType.TypeHandle.Value;
            if (!type_runs_cctor.TryGetValue(handle, out var runsStaticCtor))
            {
                runsStaticCtor = type_runs_cctor[handle] = field.FieldType.GetCustomAttribute<RunsStaticConstructorsAttribute>(inherit: false) is not null;
            }

            if (!runsStaticCtor)
            {
                continue;
            }

            if (field.GetCustomAttribute<DontForceStaticConstructorAttribute>(inherit: false) is not null)
            {
                continue;
            }

            RuntimeHelpers.RunClassConstructor(type.TypeHandle);
            return;
        }
    }

    private static bool IsSubclassOfGeneric(this Type? checkType, Type baseType)
    {
        // Not a subclass if it's the same type.
        if (checkType == baseType)
        {
            return false;
        }

        while (checkType != typeof(object) && checkType != null)
        {
            // Extract and check generic type definition if it's available,
            // instead of the qualified type.
            var currentType = checkType.IsGenericType ? checkType.GetGenericTypeDefinition() : checkType;
            if (baseType == currentType)
            {
                return true;
            }

            checkType = checkType.BaseType;
        }

        return false;
    }
}

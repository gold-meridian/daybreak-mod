using System;
using System.Runtime.CompilerServices;
using Daybreak.EarlyLoader;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Contracts;

internal static partial class ContractEnforcer
{
#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void HookIntoEarlyLoads()
    {
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

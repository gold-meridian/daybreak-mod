using System;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Common.CodeAnalysis;

internal static partial class ContractEnforcer
{
    public static void ValidateLoadable(ILoadable loadable)
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

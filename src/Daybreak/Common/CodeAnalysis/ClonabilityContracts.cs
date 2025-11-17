using System;
using System.Reflection;
using Terraria.ModLoader;

namespace Daybreak.Common.CodeAnalysis;

/// <summary>
///     Requires that the annotated type's <c>IsCloneable</c> property be equal
///     to the value <paramref name="isCloneable"/>.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ExpectCloneableAttribute(bool isCloneable = true) : Attribute
{
    /// <summary>
    ///     Whether the annotated type <c>IsCloneable</c>.
    /// </summary>
    public bool IsCloneable => isCloneable;
}

internal partial class ContractEnforcer
{
    private static void CheckClonabilityContracts(ILoadable loadable)
    {
        if (loadable is not ModType)
        {
            return;
        }

        var type = loadable.GetType();
        if (type.GetCustomAttribute<ExpectCloneableAttribute>() is not { } contract)
        {
            return;
        }

        if (!type.IsSubclassOfGeneric(typeof(GlobalType<>)) && !type.IsSubclassOfGeneric(typeof(ModType<>)))
        {
            return;
        }

        var isCloneableProperty = type.GetProperty("IsCloneable", BindingFlags.Public | BindingFlags.Instance);

        // TODO: Arguably an error, but on our end?
        if (isCloneableProperty?.GetMethod is not { } getMethod)
        {
            return;
        }

        var isCloneableVal = getMethod.Invoke(loadable, null);

        // TODO: Also definitely an error.
        if (isCloneableVal is not bool isCloneable)
        {
            return;
        }

        if (isCloneable != contract.IsCloneable)
        {
            throw new InvalidOperationException($"Failed to initialize loadable; ExpectCloneable contract failed! Expected IsCloneable={contract.IsCloneable}, got {isCloneable}");
        }
    }
}

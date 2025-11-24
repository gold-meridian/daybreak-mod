using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public readonly record struct AttributeHookPair(
    INamedTypeSymbol AttributeClass,
    HookDefinition HookDefinition
)
{
    public static AttributeHookPair Null { get; } = default;
}

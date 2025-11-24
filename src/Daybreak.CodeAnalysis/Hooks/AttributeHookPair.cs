using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public sealed record AttributeHookPair(
    INamedTypeSymbol AttributeClass,
    HookDefinition HookDefinition
);

using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis.Hooks;

public sealed record AttributeHookPair(
    INamedTypeSymbol AttributeClass,
    HookDefinition HookDefinition
);

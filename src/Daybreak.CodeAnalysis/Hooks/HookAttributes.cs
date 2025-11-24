using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public readonly record struct HookAttributes(
    INamedTypeSymbol BaseHook,
    INamedTypeSymbol HookMetadata
);

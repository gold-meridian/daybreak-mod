using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis.Hooks;

public readonly record struct HookAttributes(
    INamedTypeSymbol BaseHook,
    INamedTypeSymbol HookMetadata,
    INamedTypeSymbol Omittable,
    INamedTypeSymbol OriginalName,
    INamedTypeSymbol AbstractPermitsVoid
);

using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public readonly record struct HookAttributes(
    INamedTypeSymbol BaseHook,
    INamedTypeSymbol SubscribesTo,
    INamedTypeSymbol SubscribesTo1,
    INamedTypeSymbol OnLoad,
    INamedTypeSymbol OnUnload,
    INamedTypeSymbol Event,
    INamedTypeSymbol Event1,
    INamedTypeSymbol IlEdit,
    INamedTypeSymbol IlEdit1,
    INamedTypeSymbol Detour,
    INamedTypeSymbol Detour1
);

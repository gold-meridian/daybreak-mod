using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public static class Diagnostics
{
    public static class Categories
    {
        public const string MAINTAINABILITY = "Maintainability";
    }

    public static DiagnosticDescriptor HookInstanceMismatch { get; } = new(
        id: "DB1001",
        title: "Target method instancing mismatch",
        messageFormat: "The target method '{0}' must be {1} to be bound to the hook",
        description: "Errors when the target method is either static or instanced while the hook expects the other",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static DiagnosticDescriptor InvalidHookStacking { get; } = new(
        id: "DB1002",
        title: "Hooks cannot be combined",
        messageFormat: "The target method '{0}' attempts to bind to multiple hooks; only IL hooks can be stacked",
        description: "Errors when the target method is attempting to bind to multiple different kinds of hooks",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static DiagnosticDescriptor InvalidHookParameters { get; } = new(
        id: "DB1003",
        title: "Target parameters are invalid for hook",
        messageFormat: "The signature of the target method '{0}' is incompatible with the target hook '{1}'",
        description: "Errors when the target method's parameters are incompatible with the hook type; IL edits and detours may omit <c>orig</c> while DAYBREAK-style mod loader may omit <c>orig</c> and <c>self</c>",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static DiagnosticDescriptor InvalidHookReturnType { get; } = new(
        id: "DB1004",
        title: "Target return type is invalid for the hook",
        messageFormat: "The return type '{0}' of target method '{1}' is incompatible with the hook '{2}', expected '{3}'",
        description: "Errors when the target method's return type is incompatible with the hook type; IL edits and detours must match while DAYBREAK-style mod loader return the type or <c>void</c>",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}

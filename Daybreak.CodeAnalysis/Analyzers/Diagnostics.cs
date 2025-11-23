using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public static class Diagnostics
{
    public static class Categories
    {
        public const string MAINTAINABILITY = "Maintainability";
    }

    public static DiagnosticDescriptor HookMustBeStatic { get; } = new(
        id: nameof(HookMustBeStatic),
        title: "Method must be static",
        messageFormat: "The target method '{0}' must be static to be bound to the hook",
        description: "Errors when a target method annotated to be bound to a hook is non-static",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static DiagnosticDescriptor HookParametersDontMatch { get; } = new(
        id: nameof(HookParametersDontMatch),
        title: "Target parameters are invalid for hook",
        messageFormat: "The signature of target method '{0}' is incompatible with the target hook '{1}'",
        description: "Errors when the target method's parameters are incompatible with the hook type; IL edits and detours may omit <c>orig</c> while DAYBREAK-style mod loader may omit <c>orig</c> and <c>self</c>",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static DiagnosticDescriptor HookReturnTypeDoesntMatch { get; } = new(
        id: nameof(HookReturnTypeDoesntMatch),
        title: "Target return type is invalid for hook",
        messageFormat: "The return type of target method '{0}' is incompatible with the target hook '{1}'",
        description: "Errors when the target method's return type is incompatible with the hook type; IL edits and detours must match while DAYBREAK-style mod loader return the type or <c>void</c>",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );

    public static DiagnosticDescriptor HookShouldHaveNoParameters { get; } = new(
        id: nameof(HookShouldHaveNoParameters),
        title: "Target should have no parameters",
        messageFormat: "The target method '{0}' is not allowed to have any parameters",
        description: "Errors when the the target is bound to an <c>OnLoad</c> or an <c>OnUnload</c> hook and attempts to include parameters",
        category: Categories.MAINTAINABILITY,
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true
    );
}

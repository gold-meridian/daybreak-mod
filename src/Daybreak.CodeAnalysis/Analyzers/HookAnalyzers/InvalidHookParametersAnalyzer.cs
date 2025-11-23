using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidHookParametersAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.InvalidHookParameters, Diagnostics.InvalidHookReturnType, Diagnostics.InvalidHookReturnTypeOrVoid)
{
    protected override void InitializeWorker(AnalysisContext ctx)
    {
        ctx.RegisterCompilationStartAction(
            static startCtx =>
            {
                var voidSymbol = startCtx.Compilation.GetSpecialType(SpecialType.System_Void);

                if (!startCtx.Compilation.TryGetHookAttributes(out var attrs))
                {
                    return;
                }

                startCtx.RegisterSymbolAction(
                    symbolCtx =>
                    {
                        if (symbolCtx.Symbol is not IMethodSymbol symbol)
                        {
                            return;
                        }

                        var attributes = symbol.GetAttributes();
                        if (attributes.Length == 0)
                        {
                            return;
                        }

                        // Find the first valid one.  Other analyzers will error
                        // if there are multiple and they're incompatible.
                        var (attribute, hookKind) = attributes.Select(x => (x.AttributeClass, hookKind: x.AttributeClass.GetHookKind(attrs))).FirstOrDefault(x => x.AttributeClass is not null && x.hookKind != HookKind.None);
                        if (attribute is null || hookKind == HookKind.None)
                        {
                            return;
                        }

                        CheckReturnType(symbolCtx, symbol, voidSymbol, attrs, attribute, hookKind);
                        CheckParameters(symbolCtx, symbol, attrs, attribute, hookKind);
                    },
                    SymbolKind.Method
                );
            }
        );
    }

    private static void CheckReturnType(SymbolAnalysisContext symbolCtx, IMethodSymbol symbol, INamedTypeSymbol voidSymbol, HookAttributes attributes, INamedTypeSymbol attribute, HookKind hookKind)
    {
        // Specifically means 'void' is a valid return type but is not the
        // *expected* return type.  Should be false if the hook's actual return
        // type is void!
        bool permitsVoid;
        string hookName;
        ITypeSymbol expectedReturnType;

        // attribute.FindBaseOpenGeneric(subscribesTo1) is { IsGenericType: true } closedGeneric
        var closedGeneric = attribute.GetClosedGenericAttribute(attributes);

        switch (hookKind)
        {
            case HookKind.None:
                return;

            case HookKind.OnLoad:
                permitsVoid = false;
                expectedReturnType = voidSymbol;
                hookName = "load hook";
                break;

            case HookKind.OnUnload:
                permitsVoid = false;
                expectedReturnType = voidSymbol;
                hookName = "unload hook";
                break;

            case HookKind.IlEdit:
            {
                // TODO
                var hookType = closedGeneric?.TypeArguments.First();
                if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke })
                {
                    return;
                }

                permitsVoid = false;
                expectedReturnType = voidSymbol;
                hookName = $"IL edit hook: {hookType.Name}";
                break;
            }

            case HookKind.Detour:
            {
                // TODO
                var hookType = closedGeneric?.TypeArguments.First();
                if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke })
                {
                    return;
                }

                permitsVoid = false;
                expectedReturnType = invoke.ReturnType;
                hookName = $"detour hook: {hookType.Name}";
                break;
            }

            case HookKind.Subscriber:
            {
                var hookType = closedGeneric?.TypeArguments.First();
                if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke })
                {
                    return;
                }

                permitsVoid = !SymbolEqualityComparer.Default.Equals(invoke.ReturnType, voidSymbol);
                expectedReturnType = invoke.ReturnType;
                hookName = $"event-subscriber hook: {hookType.Name}";
                break;
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(hookKind), hookKind, null);
        }

        if ((permitsVoid || SymbolEqualityComparer.Default.Equals(expectedReturnType, voidSymbol)) && SymbolEqualityComparer.Default.Equals(symbol.ReturnType, voidSymbol))
        {
            return;
        }

        var diagnostic = permitsVoid ? Diagnostics.InvalidHookReturnTypeOrVoid : Diagnostics.InvalidHookReturnType;
        if (!SymbolEqualityComparer.Default.Equals(symbol.ReturnType, expectedReturnType))
        {
            symbolCtx.ReportDiagnostic(
                Diagnostic.Create(
                    diagnostic,
                    symbol.Locations[0],
                    symbol.ReturnType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                    symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                    hookName,
                    expectedReturnType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
                )
            );
        }
    }

    private static void CheckParameters(SymbolAnalysisContext symbolCtx, IMethodSymbol symbol, HookAttributes attributes, INamedTypeSymbol attribute, HookKind hookKind) { }

    private static bool ParametersCompatible(
        ImmutableArray<IParameterSymbol> methodParams,
        ImmutableArray<IParameterSymbol> delegateParams
    )
    {
        // Sort of an invalid case, but whatever.
        if (delegateParams.Length == 0)
        {
            return methodParams.Length == 0;
        }
    }
}

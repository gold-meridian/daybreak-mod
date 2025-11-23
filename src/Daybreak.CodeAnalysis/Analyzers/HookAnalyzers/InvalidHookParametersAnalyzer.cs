using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidHookParametersAnalyzer() :
    AbstractDiagnosticAnalyzer(
        Diagnostics.InvalidHookParameters,
        Diagnostics.InvalidHookParametersNone,
        Diagnostics.InvalidHookReturnType,
        Diagnostics.InvalidHookReturnTypeOrVoid
    )
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
                        var (attribute, hookKind) = attributes.GetFirstHookAttribute(attrs);
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
                if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: not null })
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
                hookName = $"event subscriber: {hookType.Name}";
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

    private static void CheckParameters(SymbolAnalysisContext symbolCtx, IMethodSymbol symbol, HookAttributes attributes, INamedTypeSymbol attribute, HookKind hookKind)
    {
        var closedGeneric = attribute.GetClosedGenericAttribute(attributes);
        if (closedGeneric is null)
        {
            return;
        }

        var methodParams = symbol.Parameters;

        switch (hookKind)
        {
            case HookKind.None:
                return;

            // TODO
            case HookKind.IlEdit:
                break;

            // TODO
            case HookKind.Detour:
                break;

            case HookKind.Subscriber:
            {
                var hookType = closedGeneric.TypeArguments.FirstOrDefault();
                if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke } delegateType)
                {
                    return;
                }

                var delegateParams = invoke.Parameters;

                if (!ParametersCompatible(hookKind, methodParams, delegateParams))
                {
                    symbolCtx.ReportDiagnostic(
                        Diagnostic.Create(
                            Diagnostics.InvalidHookParameters,
                            symbol.Locations.First(),
                            symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                            $"event subscriber: ${delegateType.Name}"
                        )
                    );
                }

                break;
            }

            case HookKind.OnLoad:
            case HookKind.OnUnload:
                if (!ParametersCompatible(hookKind, methodParams, ImmutableArray<IParameterSymbol>.Empty))
                {
                    symbolCtx.ReportDiagnostic(
                        Diagnostic.Create(
                            Diagnostics.InvalidHookParametersNone,
                            symbol.Locations.First(),
                            symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                            hookKind == HookKind.OnLoad ? "load hook" : "unload hook"
                        )
                    );
                }

                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(hookKind), hookKind, null);
        }
    }

    private static bool ParametersCompatible(
        HookKind hookKind,
        ImmutableArray<IParameterSymbol> methodParams,
        ImmutableArray<IParameterSymbol> delegateParams
    )
    {
        switch (hookKind)
        {
            case HookKind.None:
                return true;

            case HookKind.IlEdit:
            case HookKind.Detour:
                return true; // TODO

            case HookKind.Subscriber:
            {
                // Should never happen!!
                if (delegateParams.Length < 2)
                {
                    return true;
                }

                return MatchAllInOrder(methodParams, delegateParams)
                    || MatchOmitting(methodParams, delegateParams, 0)
                    || MatchOmitting(methodParams, delegateParams, 1)
                    || MatchOmitting(methodParams, delegateParams, 0, 1);
            }

            case HookKind.OnLoad:
            case HookKind.OnUnload:
            {
                return methodParams.Length == 0;
            }

            default:
                throw new ArgumentOutOfRangeException(nameof(hookKind), hookKind, null);
        }
    }

    private static bool MatchAllInOrder(
        ImmutableArray<IParameterSymbol> methodParams,
        ImmutableArray<IParameterSymbol> delegateParams
    )
    {
        if (methodParams.Length != delegateParams.Length)
        {
            return false;
        }

        for (var i = 0; i < methodParams.Length; i++)
        {
            if (!SymbolEqualityComparer.Default.Equals(methodParams[i].Type, delegateParams[i].Type))
            {
                return false;
            }
        }

        return true;
    }

    private static bool MatchOmitting(
        ImmutableArray<IParameterSymbol> methodParams,
        ImmutableArray<IParameterSymbol> delegateParams,
        params int[] indicesToOmit
    )
    {
        var expected = new List<IParameterSymbol>(delegateParams.Length);

        for (var i = 0; i < delegateParams.Length; i++)
        {
            if (!indicesToOmit.Contains(i))
            {
                expected.Add(delegateParams[i]);
            }
        }

        if (methodParams.Length != expected.Count)
        {
            return false;
        }

        for (var i = 0; i < methodParams.Length; i++)
        {
            if (!SymbolEqualityComparer.Default.Equals(methodParams[i].Type, expected[i].Type))
            {
                return false;
            }
        }

        return true;
    }
}

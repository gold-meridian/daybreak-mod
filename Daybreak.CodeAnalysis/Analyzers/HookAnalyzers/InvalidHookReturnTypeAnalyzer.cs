using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidHookReturnTypeAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.InvalidHookReturnType, Diagnostics.InvalidHookReturnTypeOrVoid)
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

                        foreach (var attribute in attributes)
                        {
                            // TODO: Needed for when we implement On/IL hooks.
                            // Specifically means 'void' is a valid return type
                            // but is not the *expected* return type.  Should be
                            // false if the hook's actual return type is void!
                            bool permitsVoid;
                            string hookName;
                            ITypeSymbol expectedReturnType;

                            if (attribute.AttributeClass.InheritsFrom(onLoad))
                            {
                                permitsVoid = false;
                                expectedReturnType = voidSymbol;
                                hookName = "load hook";
                            }
                            else if (attribute.AttributeClass.InheritsFrom(onUnload))
                            {
                                permitsVoid = false;
                                expectedReturnType = voidSymbol;
                                hookName = "unload hook";
                            }
                            else if (attribute.AttributeClass.FindBaseOpenGeneric(subscribesTo1) is { IsGenericType: true } closedGeneric)
                            {
                                var hookType = closedGeneric.TypeArguments.First();
                                if (hookType.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke })
                                {
                                    continue;
                                }

                                permitsVoid = !SymbolEqualityComparer.Default.Equals(invoke.ReturnType, voidSymbol);
                                expectedReturnType = invoke.ReturnType;
                                hookName = $"event-subscriber hook: {hookType.Name}";
                            }
                            else
                            {
                                continue;
                            }

                            if ((permitsVoid || SymbolEqualityComparer.Default.Equals(expectedReturnType, voidSymbol)) && SymbolEqualityComparer.Default.Equals(symbol.ReturnType, voidSymbol))
                            {
                                continue;
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
                    },
                    SymbolKind.Method
                );
            }
        );
    }
}

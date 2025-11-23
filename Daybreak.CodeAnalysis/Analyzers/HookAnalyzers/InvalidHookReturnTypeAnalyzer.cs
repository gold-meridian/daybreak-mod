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
                // var subscribesToAttributeSymbol = startCtx.Compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.SubscribesToAttribute");
                var subscribesToAttributeSymbol1 = startCtx.Compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.SubscribesToAttribute`1");
                var onLoadAttributeSymbol = startCtx.Compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.OnLoadAttribute");
                var onUnloadAttributeSymbol = startCtx.Compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.OnUnloadAttribute");
                if (subscribesToAttributeSymbol1 is null || onLoadAttributeSymbol is null || onUnloadAttributeSymbol is null)
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

                            if (attribute.AttributeClass.InheritsFrom(onLoadAttributeSymbol))
                            {
                                permitsVoid = false;
                                expectedReturnType = voidSymbol;
                                hookName = "load hook";
                            }
                            else if (attribute.AttributeClass.InheritsFrom(onUnloadAttributeSymbol))
                            {
                                permitsVoid = false;
                                expectedReturnType = voidSymbol;
                                hookName = "unload hook";
                            }
                            else if (attribute.AttributeClass.FindBaseOpenGeneric(subscribesToAttributeSymbol1) is { IsGenericType: true } closedGeneric)
                            {
                                var hookType = closedGeneric.TypeArguments.First();
                                if (hookType.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke })
                                {
                                    continue;
                                }

                                permitsVoid = !SymbolEqualityComparer.Default.Equals(invoke.ReturnType, voidSymbol);
                                expectedReturnType = invoke.ReturnType;
                                hookName = $"auto-generated hook: {hookType.Name}";
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

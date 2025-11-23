using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class HookMustBeStaticAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.HookInstanceMismatch)
{
    protected override void InitializeWorker(AnalysisContext ctx)
    {
        ctx.RegisterCompilationStartAction(
            static startCtx =>
            {
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

                        // Only the first hook needs to be matched.  If a method
                        // has more than one hook, they must all be compatible
                        // or of the same type.
                        var hookKind = attributes.Select(x => x.AttributeClass.GetHookKind(attrs)).FirstOrDefault(x => x != HookKind.None);
                        if (hookKind == HookKind.None)
                        {
                            return;
                        }

                        switch (hookKind.Instancing)
                        {
                            case HookInstancing.Both:
                                return;

                            case HookInstancing.Static when !symbol.IsStatic:
                                symbolCtx.ReportDiagnostic(
                                    Diagnostic.Create(
                                        Diagnostics.HookInstanceMismatch,
                                        symbol.Locations[0],
                                        symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                                        "static"
                                    )
                                );
                                break;

                            case HookInstancing.Instanced when symbol.IsStatic:
                                symbolCtx.ReportDiagnostic(
                                    Diagnostic.Create(
                                        Diagnostics.HookInstanceMismatch,
                                        symbol.Locations[0],
                                        symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                                        "instanced"
                                    )
                                );
                                break;
                        }
                    },
                    SymbolKind.Method
                );
            }
        );
    }
}

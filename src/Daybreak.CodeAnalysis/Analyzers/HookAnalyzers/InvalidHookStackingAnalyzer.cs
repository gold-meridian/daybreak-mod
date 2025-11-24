using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidHookStackingAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.InvalidHookStacking)
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

                        var hookDefinitions = attributes.GetHooks(attrs).ToArray();
                        if (hookDefinitions.Length < 2)
                        {
                            return;
                        }

                        var legal = hookDefinitions.Distinct().All(x => x.ValidateMultiple(hookDefinitions));
                        if (legal)
                        {
                            return;
                        }

                        symbolCtx.ReportDiagnostic(
                            Diagnostic.Create(
                                Diagnostics.InvalidHookStacking,
                                symbol.Locations[0],
                                symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
                            )
                        );
                    },
                    SymbolKind.Method
                );
            }
        );
    }
}

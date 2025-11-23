using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class HookShouldHaveNoParametersAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.InvalidHookParametersNone)
{
    protected override void InitializeWorker(AnalysisContext ctx)
    {
        ctx.RegisterCompilationStartAction(
            static startCtx =>
            {
                if (
                    startCtx.Compilation.OnLoad is not { } onLoad
                 || startCtx.Compilation.OnUnload is not { } onUnload)
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

                        var hasAnyLoadAttributes = attributes.Any(x => x.AttributeClass.InheritsFrom(onLoad) || x.AttributeClass.InheritsFrom(onUnload));
                        if (!hasAnyLoadAttributes)
                        {
                            return;
                        }

                        if (symbol.Parameters.Length == 0)
                        {
                            return;
                        }

                        symbolCtx.ReportDiagnostic(
                            Diagnostic.Create(Diagnostics.InvalidHookParametersNone, symbol.Locations[0], symbol.Name)
                        );
                    },
                    SymbolKind.Method
                );
            }
        );
    }
}

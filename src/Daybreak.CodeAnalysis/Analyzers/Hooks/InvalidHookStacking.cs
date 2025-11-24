using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

public static class InvalidHookStacking
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class Analyzer() : AbstractDiagnosticAnalyzer(Diagnostics.InvalidHookStacking)
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

                            // TODO: More robust logic?
                            var legal = hookDefinitions.All(x => x.CanStack);
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
}

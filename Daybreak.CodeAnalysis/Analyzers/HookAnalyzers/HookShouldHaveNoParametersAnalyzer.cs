using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class HookShouldHaveNoParametersAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.HookShouldHaveNoParameters)
{
    protected override void InitializeWorker(AnalysisContext ctx)
    {
        ctx.RegisterCompilationStartAction(
            static startCtx =>
            {
                var onLoadAttributeSymbol = startCtx.Compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.OnLoadAttribute");
                var onUnloadAttributeSymbol = startCtx.Compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.OnUnloadAttribute");
                if (onLoadAttributeSymbol is null || onUnloadAttributeSymbol is null)
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

                        var hasAnyLoadAttributes = attributes.Any(x => x.AttributeClass.InheritsFrom(onLoadAttributeSymbol));
                        if (!hasAnyLoadAttributes)
                        {
                            return;
                        }

                        if (symbol.Parameters.Length == 0)
                        {
                            return;
                        }

                        symbolCtx.ReportDiagnostic(
                            Diagnostic.Create(Diagnostics.HookShouldHaveNoParameters, symbol.Locations[0], symbol.Name)
                        );
                    },
                    SymbolKind.Method
                );
            }
        );
    }
}

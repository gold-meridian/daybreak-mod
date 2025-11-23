using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class HookMustBeStaticAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.HookMustBeStatic)
{
    protected override void InitializeWorker(AnalysisContext ctx)
    {
        ctx.RegisterCompilationStartAction(
            static startCtx =>
            {
                var subscribesToAttributeSymbol = startCtx.Compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.SubscribesToAttribute");
                if (subscribesToAttributeSymbol is null)
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

                        if (symbol.IsStatic)
                        {
                            return;
                        }

                        var attributes = symbol.GetAttributes();
                        if (attributes.Length == 0)
                        {
                            return;
                        }

                        var hasAnyHookAttributes = attributes.Any(x => x.AttributeClass.InheritsFrom(subscribesToAttributeSymbol));

                        if (hasAnyHookAttributes)
                        {
                            symbolCtx.ReportDiagnostic(
                                Diagnostic.Create(Diagnostics.HookMustBeStatic, symbol.Locations[0], symbol.Name)
                            );
                        }
                    },
                    SymbolKind.Method
                );
            }
        );
    }
}

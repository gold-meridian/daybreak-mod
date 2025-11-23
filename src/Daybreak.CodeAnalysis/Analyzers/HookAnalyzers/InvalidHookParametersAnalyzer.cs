using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidHookParametersAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.InvalidHookParameters)
{
    protected override void InitializeWorker(AnalysisContext ctx)
    {
        ctx.RegisterCompilationStartAction(
            static startCtx =>
            {
                if (
                    startCtx.Compilation.OnLoad is not { } onLoad
                 || startCtx.Compilation.OnUnload is not { } onUnload
                 || startCtx.Compilation.SubscribesTo1 is not { } subscribesTo1
                )
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

                        var attribute
                    },
                    SymbolKind.Method
                );
            }
        );
    }

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

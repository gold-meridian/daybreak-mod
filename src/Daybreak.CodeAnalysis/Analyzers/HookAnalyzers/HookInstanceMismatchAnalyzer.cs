using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class HookInstanceMismatchAnalyzer() : AbstractDiagnosticAnalyzer(Diagnostics.HookInstanceMismatch)
{
    public readonly record struct Properties(HookInstancing RequiredInstancing)
    {
        public static Properties FromImmutable(ImmutableDictionary<string, string?> properties)
        {
            return new Properties(
                (HookInstancing)Enum.Parse(typeof(HookInstancing), properties[nameof(RequiredInstancing)] ?? nameof(HookInstancing.Both))
            );
        }

        public ImmutableDictionary<string, string?> ToImmutable()
        {
            var properties = ImmutableDictionary.CreateBuilder<string, string?>();
            {
                properties[nameof(RequiredInstancing)] = RequiredInstancing.ToString();
            }

            return properties.ToImmutable();
        }
    }

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
                        var hookKind = attributes.GetHooks(attrs).FirstOrDefault();
                        if (hookKind is null)
                        {
                            return;
                        }

                        var instancing = hookKind.Instancing;
                        var properties = new Properties(instancing);

                        switch (instancing)
                        {
                            case HookInstancing.Both:
                                return;

                            case HookInstancing.Static when !symbol.IsStatic:
                                symbolCtx.ReportDiagnostic(
                                    Diagnostic.Create(
                                        Diagnostics.HookInstanceMismatch,
                                        symbol.Locations[0],
                                        properties.ToImmutable(),
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
                                        properties.ToImmutable(),
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

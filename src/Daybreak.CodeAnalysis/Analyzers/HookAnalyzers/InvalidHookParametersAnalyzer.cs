using System.Collections.Immutable;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class InvalidHookParametersAnalyzer() :
    AbstractDiagnosticAnalyzer(
        Diagnostics.InvalidHookParameters,
        Diagnostics.InvalidHookParametersNone,
        Diagnostics.InvalidHookReturnType,
        Diagnostics.InvalidHookReturnTypeOrVoid
    )
{
    public readonly record struct Context(
        SymbolAnalysisContext SymbolCtx,
        IMethodSymbol Symbol,
        INamedTypeSymbol VoidSymbol,
        HookAttributes Attributes,
        AttributeHookPair AttributeHookPair
    )
    {
        public INamedTypeSymbol Attribute => AttributeHookPair.AttributeClass;

        public HookDefinition HookDefinition => AttributeHookPair.HookDefinition;
    }

    public readonly record struct SignatureInfo(
        string HookTypeName,
        ImmutableArray<IParameterSymbol> HookParameters,
        INamedTypeSymbol HookReturnType,
        bool ReturnTypeCanAlsoBeVoid
    );

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

                        // Find the first valid one.  Other analyzers will error
                        // if there are multiple and they're incompatible.
                        var attrHookPair = attributes.GetFirstHookAttribute(attrs);
                        if (attrHookPair == AttributeHookPair.Null)
                        {
                            return;
                        }

                        var ctx = new Context(symbolCtx, symbol, voidSymbol, attrs, attrHookPair);
                        var sigInfo = ctx.HookDefinition.GetSignatureInfo(ctx);
                        if (!sigInfo.HasValue)
                        {
                            return;
                        }

                        CheckReturnType(ctx, sigInfo.Value);
                        CheckParameters(ctx, sigInfo.Value);
                    },
                    SymbolKind.Method
                );
            }
        );
    }

    private static void CheckReturnType(Context ctx, SignatureInfo sigInfo)
    {
        if ((sigInfo.ReturnTypeCanAlsoBeVoid || SymbolEqualityComparer.Default.Equals(sigInfo.HookReturnType, ctx.VoidSymbol)) && SymbolEqualityComparer.Default.Equals(ctx.Symbol.ReturnType, ctx.VoidSymbol))
        {
            return;
        }

        var diagnostic = sigInfo.ReturnTypeCanAlsoBeVoid
            ? Diagnostics.InvalidHookReturnTypeOrVoid
            : Diagnostics.InvalidHookReturnType;

        if (!SymbolEqualityComparer.Default.Equals(ctx.Symbol.ReturnType, sigInfo.HookReturnType))
        {
            ctx.SymbolCtx.ReportDiagnostic(
                Diagnostic.Create(
                    diagnostic,
                    ctx.Symbol.Locations[0],
                    ctx.Symbol.ReturnType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                    ctx.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                    sigInfo.HookTypeName,
                    sigInfo.HookReturnType.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat)
                )
            );
        }
    }

    private static void CheckParameters(Context ctx, SignatureInfo sigInfo)
    {
        if (ctx.HookDefinition.ValidateTargetParameters(ctx, sigInfo, ctx.Symbol.Parameters) is not { } diag)
        {
            return;
        }

        ctx.SymbolCtx.ReportDiagnostic(diag);
    }
}

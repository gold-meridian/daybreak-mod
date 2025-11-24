using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Daybreak.CodeAnalysis;

public static class InvalidHookParameters
{
    public readonly record struct HookContext(
        INamedTypeSymbol VoidSymbol,
        HookAttributes Attributes,
        INamedTypeSymbol Attribute
    );

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

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class Analyzer() : AbstractDiagnosticAnalyzer(
        Diagnostics.InvalidHookParameters,
        Diagnostics.InvalidHookParametersNone,
        Diagnostics.InvalidHookReturnType,
        Diagnostics.InvalidHookReturnTypeOrVoid
    )
    {
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
                            var hookCtx = new HookContext(voidSymbol, attrs, attrHookPair.AttributeClass);
                            var sigInfo = ctx.HookDefinition.GetSignatureInfo(hookCtx);
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
            if (ctx.HookDefinition.ValidateTargetParameters(
                    ctx,
                    sigInfo,
                    ctx.Symbol.Parameters
                ) is not { } diag)
            {
                return;
            }

            ctx.SymbolCtx.ReportDiagnostic(diag);
        }
    }

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(InvalidHookParameters)), Shared]
    public sealed class CodeFixProvider() : AbstractCodeFixProvider(
        Diagnostics.InvalidHookParameters.Id,
        Diagnostics.InvalidHookParametersNone.Id,
        Diagnostics.InvalidHookReturnType.Id,
        Diagnostics.InvalidHookReturnTypeOrVoid.Id
    )
    {
        protected override Task RegisterAsync(CodeFixContext ctx, Parameters parameters)
        {
            var diagnostic = parameters.Diagnostic;

            if (parameters.Root.FindNode(diagnostic.Location.SourceSpan) is not MethodDeclarationSyntax methodDecl)
            {
                return Task.CompletedTask;
            }

            var doc = ctx.Document;
            var semantic = parameters.SemanticModel;
            var compilation = parameters.SemanticModel.Compilation;

            var voidSymbol = compilation.GetSpecialType(SpecialType.System_Void);

            if (!compilation.TryGetHookAttributes(out var attrs))
            {
                return Task.CompletedTask;
            }

            var method = semantic.GetDeclaredSymbol(methodDecl);
            if (method is null)
            {
                return Task.CompletedTask;
            }

            var attributes = method.GetAttributes();
            if (attributes.Length == 0)
            {
                return Task.CompletedTask;
            }

            // Find the first valid one.  Other analyzers will error
            // if there are multiple and they're incompatible.
            var attrPair = attributes.GetFirstHookAttribute(attrs);
            if (attrPair == AttributeHookPair.Null)
            {
                return Task.CompletedTask;
            }

            var hookCtx = new HookContext(voidSymbol, attrs, attrPair.AttributeClass);
            var sigInfo = attrPair.HookDefinition.GetSignatureInfo(hookCtx);
            if (!sigInfo.HasValue)
            {
                return Task.CompletedTask;
            }

            var relatedDiagnostics = ctx.Diagnostics
                                        .Concat(GetValidDiagnostics(semantic, methodDecl))
                                        .Distinct()
                                        .ToArray();

            ctx.RegisterCodeFix(
                CodeAction.Create(
                    $"Fix hook signature {ctx.Diagnostics.Length}",
                    ct => ApplyFixAsync(
                        doc,
                        methodDecl,
                        sigInfo.Value,
                        ct
                    ),
                    nameof(InvalidHookParameters)
                ),
                relatedDiagnostics
            );

            return Task.CompletedTask;
        }
        
        private static async Task<Document> ApplyFixAsync(
            Document doc,
            MethodDeclarationSyntax decl,
            SignatureInfo sigInfo,
            CancellationToken ct
        )
        {
            return doc;
        }
    }
}

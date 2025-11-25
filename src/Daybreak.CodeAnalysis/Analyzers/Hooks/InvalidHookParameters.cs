using System.Collections.Generic;
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
        ITypeSymbol HookReturnType,
        bool ReturnTypeCanAlsoBeVoid
    );

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class Analyzer() : AbstractDiagnosticAnalyzer(
        Diagnostics.InvalidHookParameters,
        Diagnostics.InvalidHookReturnType
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
                            if (attrHookPair is null)
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

            if (!SymbolEqualityComparer.Default.Equals(ctx.Symbol.ReturnType, sigInfo.HookReturnType))
            {
                ctx.SymbolCtx.ReportDiagnostic(
                    Diagnostic.Create(
                        Diagnostics.InvalidHookReturnType,
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
            var parameters = ctx.Symbol.Parameters;
            var hookParameters = sigInfo.HookParameters;

            var originalNameMap = new Dictionary<string, IParameterSymbol>();
            foreach (var parameter in parameters)
            {
                var name = parameter.Name;

                if (parameter.GetAttributes() is { Length: > 0 } parameterAttributes)
                {
                    foreach (var attr in parameterAttributes)
                    {
                        if (!attr.AttributeClass.InheritsFrom(ctx.Attributes.OriginalName))
                        {
                            continue;
                        }

                        if (attr.ConstructorArguments.FirstOrDefault().Value is not string attrName)
                        {
                            continue;
                        }

                        name = attrName;
                        break;
                    }
                }

                originalNameMap[name] = parameter;
            }

            var hookParameterNames = new HashSet<string>();
            var omittable = new HashSet<string>();
            foreach (var hookParameter in hookParameters)
            {
                hookParameterNames.Add(hookParameter.Name);

                if (hookParameter.GetAttributes() is not { Length: > 0 } parameterAttributes)
                {
                    continue;
                }

                foreach (var attr in parameterAttributes)
                {
                    if (!attr.AttributeClass.InheritsFrom(ctx.Attributes.Omittable))
                    {
                        continue;
                    }

                    omittable.Add(hookParameter.Name);
                    break;
                }
            }

            // Verify all non-omittable parameters are present (checks by name,
            // not index; uses OriginalNameAttribute) and verify they're of the
            // same type.
            foreach (var hookParameter in hookParameters)
            {
                if (omittable.Contains(hookParameter.Name) && !originalNameMap.ContainsKey(hookParameter.Name))
                {
                    continue;
                }

                if (!SymbolEqualityComparer.Default.Equals(hookParameter.Type, originalNameMap[hookParameter.Name].Type))
                {
                    ctx.SymbolCtx.ReportDiagnostic(
                        Diagnostic.Create(
                            Diagnostics.InvalidHookParameters,
                            ctx.Symbol.Locations[0],
                            ctx.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                            sigInfo.HookTypeName
                        )
                    );
                    return;
                }

                if (originalNameMap.ContainsKey(hookParameter.Name))
                {
                    continue;
                }

                ctx.SymbolCtx.ReportDiagnostic(
                    Diagnostic.Create(
                        Diagnostics.InvalidHookParameters,
                        ctx.Symbol.Locations[0],
                        ctx.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                        sigInfo.HookTypeName
                    )
                );
                return;
            }

            // Verify all present parameters are actually part of the hook
            // signature.
            foreach (var parameterNames in originalNameMap.Keys)
            {
                if (hookParameterNames.Contains(parameterNames))
                {
                    continue;
                }

                ctx.SymbolCtx.ReportDiagnostic(
                    Diagnostic.Create(
                        Diagnostics.InvalidHookParameters,
                        ctx.Symbol.Locations[0],
                        ctx.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
                        sigInfo.HookTypeName
                    )
                );
                return;
            }
        }
    }

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(InvalidHookParameters)), Shared]
    public sealed class CodeFixProvider() : AbstractCodeFixProvider(
        Diagnostics.InvalidHookParameters.Id,
        Diagnostics.InvalidHookReturnType.Id
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
            if (method is not IMethodSymbol symbol)
            {
                return Task.CompletedTask;
            }

            var attributes = symbol.GetAttributes();
            if (attributes.Length == 0)
            {
                return Task.CompletedTask;
            }

            // Find the first valid one.  Other analyzers will error
            // if there are multiple and they're incompatible.
            var attrPair = attributes.GetFirstHookAttribute(attrs);
            if (attrPair is null)
            {
                return Task.CompletedTask;
            }

            var hookCtx = new HookContext(voidSymbol, attrs, attrPair.AttributeClass);
            var sigInfo = attrPair.HookDefinition.GetSignatureInfo(hookCtx);
            if (!sigInfo.HasValue)
            {
                return Task.CompletedTask;
            }

            var permitsVoid = sigInfo.Value.ReturnTypeCanAlsoBeVoid && !SymbolEqualityComparer.Default.Equals(sigInfo.Value.HookReturnType, voidSymbol);
            if (diagnostic.Id == Diagnostics.InvalidHookReturnType.Id)
            {
                if (!SymbolEqualityComparer.Default.Equals(symbol.ReturnType, sigInfo.Value.HookReturnType))
                {
                    ctx.RegisterCodeFix(
                        CodeAction.Create(
                            "Change return type to hook return type",
                            ct => FixReturnTypeAsync(
                                doc,
                                methodDecl,
                                sigInfo.Value,
                                useVoid: false,
                                ct
                            ),
                            nameof(InvalidHookParameters) + "Return"
                        ),
                        diagnostic
                    );
                }

                if (permitsVoid && !SymbolEqualityComparer.Default.Equals(symbol.ReturnType, voidSymbol))
                {
                    ctx.RegisterCodeFix(
                        CodeAction.Create(
                            "Change return type to void (hook-permitted)",
                            ct => FixReturnTypeAsync(
                                doc,
                                methodDecl,
                                sigInfo.Value,
                                useVoid: true,
                                ct
                            ),
                            nameof(InvalidHookParameters) + "ReturnPermissiveVoid"
                        ),
                        diagnostic
                    );
                }
            }
            else if (diagnostic.Id == Diagnostics.InvalidHookParameters.Id)
            {
                var omittable = new List<string>();
                foreach (var hookParameter in sigInfo.Value.HookParameters)
                {
                    if (hookParameter.GetAttributes() is not { Length: > 0 } parameterAttributes)
                    {
                        continue;
                    }

                    foreach (var attr in parameterAttributes)
                    {
                        if (!attr.AttributeClass.InheritsFrom(attrs.Omittable))
                        {
                            continue;
                        }

                        omittable.Add(hookParameter.Name);
                        break;
                    }
                }

                foreach (var omissionSet in omittable.PowerSet())
                {
                    var omitText = omissionSet.Length > 0 ? $" (omit {string.Join(", ", omissionSet)})" : "";

                    ctx.RegisterCodeFix(
                        CodeAction.Create(
                            $"Fix hook parameters{omitText}",
                            ct => FixParametersAsync(
                                doc,
                                methodDecl,
                                sigInfo.Value,
                                omissionSet,
                                ct
                            ),
                            nameof(InvalidHookParameters) + string.Join(",", omissionSet)
                        ),
                        diagnostic
                    );
                }
            }

            return Task.CompletedTask;
        }

        private static async Task<Document> FixReturnTypeAsync(
            Document doc,
            MethodDeclarationSyntax decl,
            SignatureInfo sigInfo,
            bool useVoid,
            CancellationToken ct
        )
        {
            return doc;
        }

        private static async Task<Document> FixParametersAsync(
            Document doc,
            MethodDeclarationSyntax decl,
            SignatureInfo sigInfo,
            string[] omittedParams,
            CancellationToken ct
        )
        {
            return doc;
        }
    }
}

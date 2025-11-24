using System;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Editing;

namespace Daybreak.CodeAnalysis;

public static class HookInstanceMismatch
{
    public readonly record struct Properties(
        HookInstancing RequiredInstancing
    )
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

    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public sealed class Analyzer() : AbstractDiagnosticAnalyzer(Diagnostics.HookInstanceMismatch)
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

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(HookInstanceMismatch)), Shared]
    public sealed class CodeFixProvider() : AbstractCodeFixProvider(Diagnostics.HookInstanceMismatch.Id)
    {
        protected override Task RegisterAsync(CodeFixContext ctx, Parameters parameters)
        {
            var diagnostic = parameters.Diagnostic;
            var properties = Properties.FromImmutable(diagnostic.Properties);

            var required = properties.RequiredInstancing;
            if (required == HookInstancing.Both)
            {
                return Task.CompletedTask;
            }

            const string title = "Make target method static/instanced for hook";
            ctx.RegisterCodeFix(
                CodeAction.Create(
                    title,
                    ct => ApplyFixAsync(
                        ctx.Document,
                        parameters.Root,
                        diagnostic,
                        required,
                        ct
                    ),
                    nameof(HookInstanceMismatch)
                ),
                diagnostic
            );

            return Task.CompletedTask;
        }

        private static async Task<Document> ApplyFixAsync(
            Document document,
            SyntaxNode root,
            Diagnostic diagnostic,
            HookInstancing required,
            CancellationToken cancellationToken
        )
        {
            if (required == HookInstancing.Both)
            {
                return document;
            }

            var node = root.FindNode(diagnostic.Location.SourceSpan);
            if (node is not MethodDeclarationSyntax methodDecl)
            {
                return document;
            }

            var editor = await DocumentEditor.CreateAsync(document, cancellationToken);

            var newDecl = methodDecl;
            if (required == HookInstancing.Static)
            {
                var modifiers = methodDecl.Modifiers;

                if (!modifiers.Any(x => x.IsKind(SyntaxKind.StaticKeyword)))
                {
                    modifiers = modifiers.Add(
                        SyntaxFactory.Token(SyntaxKind.StaticKeyword)
                                     .WithTrailingTrivia(SyntaxFactory.Space)
                    );
                }

                newDecl = methodDecl.WithModifiers(modifiers);
            }
            else
            {
                var modifiers = methodDecl.Modifiers;
                var staticIndex = modifiers.IndexOf(SyntaxKind.StaticKeyword);

                if (staticIndex >= 0)
                {
                    var staticToken = modifiers[staticIndex];
                    var leadingTrivia = staticIndex == 0 ? staticToken.LeadingTrivia : FilterWhitespace(staticToken.LeadingTrivia);
                    var trailingTrivia = FilterWhitespace(staticToken.TrailingTrivia);

                    SyntaxToken replacementTarget;
                    if (staticIndex + 1 < modifiers.Count)
                    {
                        replacementTarget = modifiers[staticIndex + 1];

                        var newTarget = replacementTarget
                                       .WithLeadingTrivia(leadingTrivia)
                                       .WithTrailingTrivia(trailingTrivia.AddRange(replacementTarget.TrailingTrivia));

                        modifiers = modifiers.Replace(replacementTarget, newTarget);
                    }
                    else
                    {
                        replacementTarget = methodDecl.ReturnType.GetFirstToken();

                        var newTarget = replacementTarget
                                       .WithLeadingTrivia(leadingTrivia)
                                       .WithTrailingTrivia(trailingTrivia.AddRange(replacementTarget.TrailingTrivia));

                        var newReturnType = methodDecl.ReturnType.ReplaceToken(replacementTarget, newTarget);
                        newDecl = methodDecl.WithReturnType(newReturnType);
                    }

                    modifiers = modifiers.RemoveAt(staticIndex);

                    newDecl = newDecl.WithModifiers(
                        SyntaxFactory.TokenList(modifiers)
                    );
                }
            }

            editor.ReplaceNode(methodDecl, newDecl);
            return editor.GetChangedDocument();
        }

        private static SyntaxTriviaList FilterWhitespace(SyntaxTriviaList trivia)
        {
            return SyntaxFactory.TriviaList(
                trivia.Where(x => !x.IsKind(SyntaxKind.WhitespaceTrivia))
            );
        }
    }
}

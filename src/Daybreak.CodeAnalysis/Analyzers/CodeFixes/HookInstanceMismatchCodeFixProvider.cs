using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Daybreak.CodeAnalysis;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(HookInstanceMismatchCodeFixProvider)), Shared]
public sealed class HookInstanceMismatchCodeFixProvider() : AbstractCodeFixProvider(Diagnostics.HookInstanceMismatch.Id)
{
    protected override Task RegisterAsync(CodeFixContext ctx, Parameters parameters)
    {
        var diagnostic = parameters.Diagnostic;
        var properties = HookInstanceMismatchAnalyzer.Properties.FromImmutable(diagnostic.Properties);

        var required = properties.RequiredInstancing;
        if (required == HookInstancing.Both)
        {
            return Task.CompletedTask;
        }

        var title = required == HookInstancing.Static
            ? "Make method static"
            : "Make method instanced";

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
                nameof(HookInstanceMismatchCodeFixProvider)
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

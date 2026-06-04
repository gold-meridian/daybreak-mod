using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Text;

namespace Daybreak.CodeAnalysis;

public abstract class AbstractCodeFixProvider(params string[] diagnosticIds) : CodeFixProvider
{
    protected readonly record struct Parameters(
        SyntaxNode Root,
        SemanticModel SemanticModel,
        Diagnostic Diagnostic
    )
    {
        public TextSpan DiagnosticSpan => Diagnostic.Location.SourceSpan;
    }

    public override ImmutableArray<string> FixableDiagnosticIds => diagnosticIds.ToImmutableArray();

    public override FixAllProvider GetFixAllProvider()
    {
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);
        if (root is null)
        {
            return;
        }

        var semanticModel = await context.Document.GetSemanticModelAsync(context.CancellationToken).ConfigureAwait(false);
        if (semanticModel is null)
        {
            return;
        }

        var diagnostic = context.Diagnostics.First();
        var parameters = new Parameters(root, semanticModel, diagnostic);
        {
            await RegisterAsync(context, parameters);
        }
    }

    protected abstract Task RegisterAsync(CodeFixContext ctx, Parameters parameters);
}

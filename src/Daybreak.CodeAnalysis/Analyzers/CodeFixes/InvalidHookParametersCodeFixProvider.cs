using System.Composition;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Daybreak.CodeAnalysis;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(InvalidHookParametersCodeFixProvider)), Shared]
public sealed class InvalidHookParametersCodeFixProvider() : AbstractCodeFixProvider(
    Diagnostics.InvalidHookParameters.Id,
    Diagnostics.InvalidHookParametersNone.Id,
    Diagnostics.InvalidHookReturnType.Id,
    Diagnostics.InvalidHookReturnTypeOrVoid.Id
)
{
    protected override Task RegisterAsync(CodeFixContext ctx, Parameters parameters)
    {
        throw new System.NotImplementedException();
    }
}

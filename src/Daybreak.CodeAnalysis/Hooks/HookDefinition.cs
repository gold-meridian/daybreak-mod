using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public sealed class HookDefinition(
    INamedTypeSymbol hookAttribute,
    INamedTypeSymbol? delegateType,
    INamedTypeSymbol? typeContainingEvent,
    string? eventName,
    string? delegateName,
    bool canStack
)
{
    public string Name
    {
        get
        {
            var name = hookAttribute.ToDisplayString(SymbolDisplayFormat.CSharpShortErrorMessageFormat);
            if (!name.EndsWith("Attribute"))
            {
                return name;
            }
            
            return name[..^"Attribute".Length];
        }
    }

    public bool CanStack => canStack;

    // TODO: bring this back later
    public HookInstancing Instancing => HookInstancing.Both;

    public InvalidHookParameters.SignatureInfo? GetSignatureInfo(
        InvalidHookParameters.HookContext ctx
    )
    {
        // TODO
        return null;
    }

    public Diagnostic? ValidateTargetParameters(
        InvalidHookParameters.Context ctx,
        InvalidHookParameters.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    )
    {
        // TODO
        return null;
    }
}

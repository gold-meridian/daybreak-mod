using System.Collections.Immutable;
using System.Linq;
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
        var delegateSymbol = GetDelegateType();
        if (delegateSymbol is not INamedTypeSymbol { DelegateInvokeMethod: { } invoke })
        {
            return null;
        }

        var hasReturnHandler = invoke.GetReturnTypeAttributes().Any(x => x.AttributeClass?.InheritsFrom(ctx.Attributes.AbstractPermitsVoid) ?? false);

        return new InvalidHookParameters.SignatureInfo(
            HookTypeName: Name,
            HookParameters: invoke.Parameters,
            HookReturnType: invoke.ReturnType,
            ReturnTypeCanAlsoBeVoid: hasReturnHandler && !SymbolEqualityComparer.Default.Equals(invoke.ReturnType, ctx.VoidSymbol)
        );
    }

    public ITypeSymbol? GetDelegateType()
    {
        if (delegateType is not null)
        {
            return delegateType;
        }

        if (typeContainingEvent is null)
        {
            return null;
        }

        if (delegateName is not null)
        {
            return typeContainingEvent.GetTypeMembers(delegateName).FirstOrDefault();
        }

        if (GetEvent() is { } eventSymbol)
        {
            return eventSymbol.Type.OriginalDefinition;
        }

        return null;
    }

    private IEventSymbol? GetEvent()
    {
        if (typeContainingEvent is null || eventName is null)
        {
            return null;
        }

        return typeContainingEvent.GetMembers(eventName).Where(x => x is IEventSymbol).Cast<IEventSymbol>().FirstOrDefault();
    }
}

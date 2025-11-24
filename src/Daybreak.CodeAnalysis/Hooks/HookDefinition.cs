using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public record HookDefinition
{
    public string Name { get; } = "stub";

    public HookInstancing Instancing { get; } = HookInstancing.Both;

    public bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        // TODO
        return true;
    }

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

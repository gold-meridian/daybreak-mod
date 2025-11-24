using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal sealed class DefaultDefinition() : HookDefinition("Default")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => true;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        return true;
    }

    public override InvalidHookParameters.SignatureInfo? GetSignatureInfo(InvalidHookParameters.HookContext ctx)
    {
        return null;
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParameters.Context ctx,
        InvalidHookParameters.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters,
        InvalidHookParameters.Properties properties
    )
    {
        return null;
    }
}

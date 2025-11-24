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

    public override InvalidHookParametersAnalyzer.SignatureInfo? GetSignatureInfo(
        InvalidHookParametersAnalyzer.Context ctx
    )
    {
        return null;
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParametersAnalyzer.Context ctx,
        InvalidHookParametersAnalyzer.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    )
    {
        return null;
    }
}

using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal sealed class DetourDefinition() : HookDefinition("Detour")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => false;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        return false;
    }

    public override InvalidHookParameters.SignatureInfo? GetSignatureInfo(
        InvalidHookParameters.Context ctx
    )
    {
        // TODO
        /*
        var closedGeneric = ctx.Attribute.GetClosedGenericAttribute(ctx.Attributes);
        var hookType = closedGeneric?.TypeArguments.First();
        if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: not null })
        {
            return null;
        }

        return new InvalidHookParameters.SignatureInfo(
            HookTypeName: $"detour hook: {hookType.Name}",
            HookParameters: ImmutableArray<ParameterInfo>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );*/
        return null;
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParameters.Context ctx,
        InvalidHookParameters.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    )
    {
        // TODO
        return null;
    }
}

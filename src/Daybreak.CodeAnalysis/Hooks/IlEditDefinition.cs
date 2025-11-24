using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal sealed class IlEditDefinition() : HookDefinition("IlEdit")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => true;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        // Only IL hooks can stack with each other.
        return hooks.All(x => x == IlEdit);
    }

    public override InvalidHookParameters.SignatureInfo? GetSignatureInfo(InvalidHookParameters.HookContext ctx)
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
            HookTypeName: $"IL edit hook: {hookType.Name}",
            HookParameters: ImmutableArray<ParameterInfo>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );
        */
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

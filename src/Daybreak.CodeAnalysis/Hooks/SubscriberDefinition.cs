using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal sealed class SubscriberDefinition() : HookDefinition("Subscriber")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => false;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        return false;
    }

    public override InvalidHookParameters.SignatureInfo? GetSignatureInfo(InvalidHookParameters.HookContext ctx)
    {
        var closedGeneric = ctx.Attribute.GetClosedGenericAttribute(ctx.Attributes);
        var hookType = closedGeneric?.TypeArguments.First();
        if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke })
        {
            return null;
        }

        return new InvalidHookParameters.SignatureInfo(
            HookTypeName: $"event subscriber: {hookType.Name}",
            HookParameters: invoke.Parameters,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: !SymbolEqualityComparer.Default.Equals(invoke.ReturnType, ctx.VoidSymbol)
        );
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParameters.Context ctx,
        InvalidHookParameters.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters,
        InvalidHookParameters.Properties properties
    )
    {
        var closedGeneric = ctx.Attribute.GetClosedGenericAttribute(ctx.Attributes);
        if (closedGeneric?.TypeArguments.FirstOrDefault() is not { } hookType)
        {
            return null;
        }

        // Should never happen!!
        if (sigInfo.HookParameters.Length < 2)
        {
            return null;
        }

        if
            (ParameterComparison.MatchAllInOrder(targetParameters, sigInfo.HookParameters)
          || ParameterComparison.MatchOmitting(targetParameters, sigInfo.HookParameters, 0)
          || ParameterComparison.MatchOmitting(targetParameters, sigInfo.HookParameters, 1)
          || ParameterComparison.MatchOmitting(targetParameters, sigInfo.HookParameters, 0, 1)
            )
        {
            return null;
        }

        return Diagnostic.Create(
            Diagnostics.InvalidHookParameters,
            ctx.Symbol.Locations.First(),
            properties.ToImmutable(),
            ctx.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
            $"event subscriber: {hookType.Name}"
        );
    }
}

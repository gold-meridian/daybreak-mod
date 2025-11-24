using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal abstract class SharedLoadDefinition(string name, string typeName) : HookDefinition(name)
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
        return new InvalidHookParameters.SignatureInfo(
            HookTypeName: typeName,
            HookParameters: ImmutableArray<IParameterSymbol>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParameters.Context ctx,
        InvalidHookParameters.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    )
    {
        if (targetParameters.Length <= 0)
        {
            return null;
        }

        return Diagnostic.Create(
            Diagnostics.InvalidHookParametersNone,
            ctx.Symbol.Locations.First(),
            ctx.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
            typeName
        );
    }
}

internal sealed class OnLoadDefinition() : SharedLoadDefinition("OnLoad", "load hook");

internal sealed class OnUnloadDefinition() : SharedLoadDefinition("OnUnload", "unload hook");

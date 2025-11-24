using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

public enum HookInstancing
{
    Static,
    Instanced,
    Both,
}

public abstract class HookDefinition(string name) : IEquatable<HookDefinition>, IComparable<HookDefinition>
{
#region Well-known definitions
    public static HookDefinition Default { get; } = Register(new DefaultDefinition());

    public static HookDefinition IlEdit { get; } = Register(new IlEditDefinition());

    public static HookDefinition Detour { get; } = Register(new DetourDefinition());

    public static HookDefinition Subscriber { get; } = Register(new SubscriberDefinition());

    public static HookDefinition OnLoad { get; } = Register(new OnLoadDefinition());

    public static HookDefinition OnUnload { get; } = Register(new OnUnloadDefinition());

#region Lookup
    private static readonly Dictionary<string, HookDefinition> name_map = [];

    public static HookDefinition FromName(string name)
    {
        return name_map.TryGetValue(name, out var hook) ? hook : Default;
    }

    private static HookDefinition Register(HookDefinition definition)
    {
        return name_map[definition.Name] = definition;
    }
#endregion
#endregion

    public string Name { get; } = name;

    public abstract HookInstancing Instancing { get; }

    public abstract bool PermitsMultiple { get; }

    public abstract bool ValidateMultiple(IEnumerable<HookDefinition> hooks);

    public abstract InvalidHookParametersAnalyzer.SignatureInfo? GetSignatureInfo(
        InvalidHookParametersAnalyzer.Context ctx
    );

    public abstract Diagnostic? ValidateTargetParameters(
        InvalidHookParametersAnalyzer.Context ctx,
        InvalidHookParametersAnalyzer.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    );

#region Comparisons
    public bool Equals(HookDefinition? other)
    {
        return other?.Name == Name;
    }

    public sealed override bool Equals(object? obj)
    {
        return obj is HookDefinition other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public int CompareTo(HookDefinition? other)
    {
        return string.Compare(Name, other?.Name, StringComparison.Ordinal);
    }

    public static bool operator ==(HookDefinition left, HookDefinition right)
    {
        return left.Name == right.Name;
    }

    public static bool operator !=(HookDefinition left, HookDefinition right)
    {
        return left.Name != right.Name;
    }
#endregion
}

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

internal sealed class IlEditDefinition() : HookDefinition("IlEdit")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => true;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        // Only IL hooks can stack with each other.
        return hooks.All(x => x == IlEdit);
    }

    public override InvalidHookParametersAnalyzer.SignatureInfo? GetSignatureInfo(
        InvalidHookParametersAnalyzer.Context ctx
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

        return new InvalidHookParametersAnalyzer.SignatureInfo(
            HookTypeName: $"IL edit hook: {hookType.Name}",
            HookParameters: ImmutableArray<ParameterInfo>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );
        */
        return null;
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParametersAnalyzer.Context ctx,
        InvalidHookParametersAnalyzer.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    )
    {
        // TODO
        return null;
    }
}

internal sealed class DetourDefinition() : HookDefinition("Detour")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => false;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        return false;
    }

    public override InvalidHookParametersAnalyzer.SignatureInfo? GetSignatureInfo(
        InvalidHookParametersAnalyzer.Context ctx
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

        return new InvalidHookParametersAnalyzer.SignatureInfo(
            HookTypeName: $"detour hook: {hookType.Name}",
            HookParameters: ImmutableArray<ParameterInfo>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );*/
        return null;
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParametersAnalyzer.Context ctx,
        InvalidHookParametersAnalyzer.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    )
    {
        // TODO
        return null;
    }
}

internal sealed class SubscriberDefinition() : HookDefinition("Subscriber")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => false;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        return false;
    }

    public override InvalidHookParametersAnalyzer.SignatureInfo? GetSignatureInfo(
        InvalidHookParametersAnalyzer.Context ctx
    )
    {
        var closedGeneric = ctx.Attribute.GetClosedGenericAttribute(ctx.Attributes);
        var hookType = closedGeneric?.TypeArguments.First();
        if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: not null })
        {
            return null;
        }

        return new InvalidHookParametersAnalyzer.SignatureInfo(
            HookTypeName: $"event subscriber: {hookType.Name}",
            HookParameters: ImmutableArray<ParameterInfo>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParametersAnalyzer.Context ctx,
        InvalidHookParametersAnalyzer.SignatureInfo sigInfo,
        ImmutableArray<IParameterSymbol> targetParameters
    )
    {
        var closedGeneric = ctx.Attribute.GetClosedGenericAttribute(ctx.Attributes);
        var hookType = closedGeneric?.TypeArguments.FirstOrDefault();
        if (hookType?.GetTypeMembers("Definition").FirstOrDefault() is not { DelegateInvokeMethod: { } invoke } delegateType)
        {
            return null;
        }

        var delegateParams = invoke.Parameters;

        // Should never happen!!
        if (delegateParams.Length < 2)
        {
            return null;
        }

        if
            (ParameterComparison.MatchAllInOrder(targetParameters, delegateParams)
          || ParameterComparison.MatchOmitting(targetParameters, delegateParams, 0)
          || ParameterComparison.MatchOmitting(targetParameters, delegateParams, 1)
          || ParameterComparison.MatchOmitting(targetParameters, delegateParams, 0, 1)
            )
        {
            return null;
        }

        return Diagnostic.Create(
            Diagnostics.InvalidHookParameters,
            ctx.Symbol.Locations.First(),
            ctx.Symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat),
            $"event subscriber: ${delegateType.Name}"
        );
    }
}

internal sealed class OnLoadDefinition() : HookDefinition("OnLoad")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => false;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        return false;
    }

    public override InvalidHookParametersAnalyzer.SignatureInfo? GetSignatureInfo(
        InvalidHookParametersAnalyzer.Context ctx
    )
    {
        return new InvalidHookParametersAnalyzer.SignatureInfo(
            HookTypeName: "load hook",
            HookParameters: ImmutableArray<ParameterInfo>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParametersAnalyzer.Context ctx,
        InvalidHookParametersAnalyzer.SignatureInfo sigInfo,
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
            "load hook"
        );
    }
}

internal sealed class OnUnloadDefinition() : HookDefinition("OnUnload")
{
    public override HookInstancing Instancing => HookInstancing.Both;

    public override bool PermitsMultiple => false;

    public override bool ValidateMultiple(IEnumerable<HookDefinition> hooks)
    {
        return false;
    }

    public override InvalidHookParametersAnalyzer.SignatureInfo? GetSignatureInfo(
        InvalidHookParametersAnalyzer.Context ctx
    )
    {
        return new InvalidHookParametersAnalyzer.SignatureInfo(
            HookTypeName: "unload hook",
            HookParameters: ImmutableArray<ParameterInfo>.Empty,
            HookReturnType: ctx.VoidSymbol,
            ReturnTypeCanAlsoBeVoid: false
        );
    }

    public override Diagnostic? ValidateTargetParameters(
        InvalidHookParametersAnalyzer.Context ctx,
        InvalidHookParametersAnalyzer.SignatureInfo sigInfo,
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
            "unload hook"
        );
    }
}

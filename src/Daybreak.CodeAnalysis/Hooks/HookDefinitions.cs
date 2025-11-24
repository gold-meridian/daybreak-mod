using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    public static HookDefinition Default { get; }

    public static HookDefinition IlEdit { get; }

    public static HookDefinition Detour { get; }

    public static HookDefinition Subscriber { get; }

    public static HookDefinition OnLoad { get; }

    public static HookDefinition OnUnload { get; }

    private static readonly Dictionary<string, HookDefinition> name_map = [];

    static HookDefinition()
    {
        Default = Register(new DefaultDefinition());
        IlEdit = Register(new IlEditDefinition());
        Detour = Register(new DetourDefinition());
        Subscriber = Register(new SubscriberDefinition());
        OnLoad = Register(new OnLoadDefinition());
        OnUnload = Register(new OnUnloadDefinition());
    }

    public static HookDefinition FromName(string name)
    {
        return name_map.TryGetValue(name, out var hook) ? hook : Default;
    }

    private static HookDefinition Register(HookDefinition definition)
    {
        return name_map[definition.Name] = definition;
    }
#endregion

    public string Name { get; } = name;

    public abstract HookInstancing Instancing { get; }

    public abstract bool PermitsMultiple { get; }

    public abstract bool ValidateMultiple(IEnumerable<HookDefinition> hooks);

    public abstract InvalidHookParameters.SignatureInfo? GetSignatureInfo(
        InvalidHookParameters.HookContext ctx
    );

    public abstract Diagnostic? ValidateTargetParameters(
        InvalidHookParameters.Context ctx,
        InvalidHookParameters.SignatureInfo sigInfo,
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

    public static bool operator ==(HookDefinition? left, HookDefinition? right)
    {
        return left?.Name == right?.Name;
    }

    public static bool operator !=(HookDefinition? left, HookDefinition? right)
    {
        return left?.Name != right?.Name;
    }
#endregion
}

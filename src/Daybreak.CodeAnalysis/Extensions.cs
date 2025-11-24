using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal static class Extensions
{
    extension(INamedTypeSymbol? symbol)
    {
        public bool InheritsFrom(INamedTypeSymbol baseType)
        {
            while (symbol is not null)
            {
                if (SymbolEqualityComparer.Default.Equals(symbol.OriginalDefinition, baseType))
                {
                    return true;
                }

                symbol = symbol.BaseType;
            }

            return false;
        }

        public INamedTypeSymbol? FindBaseOpenGeneric(INamedTypeSymbol openGenericBaseType)
        {
            while (symbol is not null)
            {
                if (SymbolEqualityComparer.Default.Equals(symbol.OriginalDefinition, openGenericBaseType))
                {
                    return symbol;
                }

                symbol = symbol.BaseType;
            }

            return null;
        }

        public HookDefinition GetHookDefinition(HookAttributes attrs)
        {
            if (symbol is null)
            {
                return HookDefinition.Default;
            }

            if (symbol.InheritsFrom(attrs.OnLoad))
            {
                return HookDefinition.OnLoad;
            }

            if (symbol.InheritsFrom(attrs.OnUnload))
            {
                return HookDefinition.OnUnload;
            }

            if (symbol.InheritsFrom(attrs.SubscribesTo))
            {
                return HookDefinition.Subscriber;
            }

            if (symbol.InheritsFrom(attrs.IlEdit))
            {
                return HookDefinition.IlEdit;
            }

            if (symbol.InheritsFrom(attrs.Detour))
            {
                return HookDefinition.Detour;
            }

            return HookDefinition.Default;
        }

        public INamedTypeSymbol? GetClosedGenericAttribute(HookAttributes attributes)
        {
            var closedGeneric = symbol.FindBaseOpenGeneric(attributes.SubscribesTo1)
                             ?? symbol.FindBaseOpenGeneric(attributes.IlEdit1)
                             ?? symbol.FindBaseOpenGeneric(attributes.Detour1);

            return (closedGeneric?.IsGenericType ?? false) ? closedGeneric : null;
        }
    }

    extension(Compilation compilation)
    {
        public INamedTypeSymbol? BaseHook => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.BaseHookAttribute");

        public INamedTypeSymbol? SubscribesTo => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.SubscribesToAttribute");

        public INamedTypeSymbol? SubscribesTo1 => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.SubscribesToAttribute`1");

        public INamedTypeSymbol? OnLoad => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.OnLoadAttribute");

        public INamedTypeSymbol? OnUnload => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.OnUnloadAttribute");

        public INamedTypeSymbol? IlEdit => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.IlEditAttribute");

        public INamedTypeSymbol? IlEdit1 => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.IlEditAttribute`1");

        public INamedTypeSymbol? Detour => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.DetourAttribute");

        public INamedTypeSymbol? Detour1 => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.DetourAttribute`1");

        public bool TryGetHookAttributes(out HookAttributes attributes)
        {
            if (
                compilation.BaseHook is not { } baseHook
             || compilation.SubscribesTo is not { } subscribesTo
             || compilation.SubscribesTo1 is not { } subscribesTo1
             || compilation.OnLoad is not { } onLoad
             || compilation.OnUnload is not { } onUnload
             || compilation.IlEdit is not { } ilEdit
             || compilation.IlEdit1 is not { } ilEdit1
             || compilation.Detour is not { } detour
             || compilation.Detour1 is not { } detour1
            )
            {
                attributes = default(HookAttributes);
                return false;
            }

            attributes = new HookAttributes(
                baseHook,
                subscribesTo,
                subscribesTo1,
                onLoad,
                onUnload,
                ilEdit,
                ilEdit1,
                detour,
                detour1
            );
            return true;
        }
    }

    extension(ImmutableArray<AttributeData> attributes)
    {
        public IEnumerable<HookDefinition> GetHooks(HookAttributes attrs)
        {
            return attributes.Where(x => x.AttributeClass is not null)
                             .Select(x => x.AttributeClass)
                             .Cast<INamedTypeSymbol>()
                             .Select(x => x.GetHookDefinition(attrs))
                             .Where(x => x != HookDefinition.Default);
        }

        public AttributeHookPair GetFirstHookAttribute(HookAttributes attrs)
        {
            return attributes.Where(x => x.AttributeClass is not null)
                             .Select(x => x.AttributeClass)
                             .Cast<INamedTypeSymbol>()
                             .Select(x => new AttributeHookPair(x, x.GetHookDefinition(attrs)))
                             .FirstOrDefault(x => x.HookDefinition != HookDefinition.Default);
        }
    }
}

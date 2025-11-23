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

        public HookKind GetHookKind(HookAttributes attrs)
        {
            if (symbol is null)
            {
                return HookKind.None;
            }

            if (symbol.InheritsFrom(attrs.OnLoad))
            {
                return HookKind.OnLoad;
            }

            if (symbol.InheritsFrom(attrs.OnUnload))
            {
                return HookKind.OnUnload;
            }

            if (symbol.InheritsFrom(attrs.SubscribesTo))
            {
                return HookKind.Subscriber;
            }

            if (symbol.InheritsFrom(attrs.IlEdit))
            {
                return HookKind.IlEdit;
            }

            if (symbol.InheritsFrom(attrs.Detour))
            {
                return HookKind.Detour;
            }

            return HookKind.None;
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
        public (INamedTypeSymbol? AttributeClass, HookKind hookKind) GetFirstHookAttribute(HookAttributes attrs)
        {
            return attributes.Select(x => (x.AttributeClass, hookKind: x.AttributeClass.GetHookKind(attrs))).FirstOrDefault(x => x.AttributeClass is not null && x.hookKind != HookKind.None);
        }
    }
}

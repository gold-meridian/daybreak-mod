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

        public HookDefinition? GetHookDefinition(HookAttributes attrs)
        {
            if (!symbol?.InheritsFrom(attrs.BaseHook) ?? false)
            {
                return null;
            }

            var candidates = symbol!.GetAttributes();
            foreach (var candidate in candidates)
            {
                if (!candidate.AttributeClass?.InheritsFrom(attrs.HookMetadata) ?? false)
                {
                    continue;
                }

                var arguments = candidate.NamedArguments;

                var delegateType = arguments
                                  .FirstOrDefault(x => x.Key == "DelegateType")
                                  .Value.Value as INamedTypeSymbol;

                var typeContainingEvent = arguments
                                         .FirstOrDefault(x => x.Key == "TypeContainingEvent")
                                         .Value.Value as INamedTypeSymbol;

                var eventName = arguments
                               .FirstOrDefault(x => x.Key == "EventName")
                               .Value.Value as string;

                var delegateName = arguments
                                  .FirstOrDefault(x => x.Key == "DelegateName")
                                  .Value.Value as string;

                var canStack = arguments
                              .FirstOrDefault(x => x.Key == "CanStack")
                              .Value.Value is true;

                return new HookDefinition(
                    symbol,
                    delegateType,
                    typeContainingEvent,
                    eventName,
                    delegateName,
                    canStack
                );
            }

            return null;
        }
    }

    extension(Compilation compilation)
    {
        public INamedTypeSymbol? BaseHook => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.BaseHookAttribute");

        public INamedTypeSymbol? HookMetadata => compilation.GetTypeByMetadataName("Daybreak.Common.Features.Hooks.HookMetadataAttribute");

        public bool TryGetHookAttributes(out HookAttributes attributes)
        {
            if (
                compilation.BaseHook is not { } baseHook
             || compilation.HookMetadata is not { } hookMetadata
            )
            {
                attributes = default(HookAttributes);
                return false;
            }

            attributes = new HookAttributes(
                baseHook,
                hookMetadata
            );
            return true;
        }
    }

    extension(ImmutableArray<AttributeData> attributes)
    {
        public IEnumerable<HookDefinition> GetHooks(HookAttributes attrs)
        {
            return attributes.Select(attribute => attribute.AttributeClass?.GetHookDefinition(attrs)).OfType<HookDefinition>();
        }

        public AttributeHookPair? GetFirstHookAttribute(HookAttributes attrs)
        {
            foreach (var attribute in attributes)
            {
                var hook = attribute.AttributeClass?.GetHookDefinition(attrs);
                if (hook is null)
                {
                    continue;
                }

                return new AttributeHookPair(attribute.AttributeClass!, hook);
            }

            return null;
        }
    }
}

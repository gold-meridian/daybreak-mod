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

            // TODO
            return new HookDefinition();
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

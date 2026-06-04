using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal static class Extensions
{
    extension(ISymbol? symbol)
    {
        // https://stackoverflow.com/a/27106959
        public string GetFullMetadataName() 
        {
            if (symbol is null || symbol.IsRootNamespace())
            {
                return string.Empty;
            }

            var sb = new StringBuilder(symbol.MetadataName);
            var last = symbol;

            symbol = symbol.ContainingSymbol;

            while (!symbol.IsRootNamespace())
            {
                if (symbol is ITypeSymbol && last is ITypeSymbol)
                {
                    sb.Insert(0, '+');
                }
                else
                {
                    sb.Insert(0, '.');
                }

                sb.Insert(0, symbol.OriginalDefinition.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat));
                // sb.Insert(0, s.MetadataName);
                symbol = symbol.ContainingSymbol;
            }

            return sb.ToString();
        }

        private bool IsRootNamespace() 
        {
            return symbol is INamespaceSymbol { IsGlobalNamespace: true };
        }
    }
    
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
    }

    extension<T>(IReadOnlyList<T> items)
    {
        // https://stackoverflow.com/questions/19890781/creating-a-power-set-of-a-sequence
        public IEnumerable<T[]> PowerSet()
        {
            var count = 1 << items.Count;
            for (var mask = 0; mask < count; mask++)
            {
                var subset = new List<T>();
                for (var i = 0; i < items.Count; i++)
                {
                    if ((mask & (1 << i)) != 0)
                    {
                        subset.Add(items[i]);
                    }
                }

                yield return subset.ToArray();
            }
        }
    }
}

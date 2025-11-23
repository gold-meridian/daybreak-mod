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
    }
}

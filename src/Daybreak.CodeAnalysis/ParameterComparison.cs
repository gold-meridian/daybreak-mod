using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis;

internal static class ParameterComparison
{
    public static bool MatchAllInOrder(
        ImmutableArray<IParameterSymbol> methodParams,
        ImmutableArray<IParameterSymbol> delegateParams
    )
    {
        if (methodParams.Length != delegateParams.Length)
        {
            return false;
        }

        for (var i = 0; i < methodParams.Length; i++)
        {
            if (!SymbolEqualityComparer.Default.Equals(methodParams[i].Type, delegateParams[i].Type))
            {
                return false;
            }
        }

        return true;
    }

    public static bool MatchOmitting(
        ImmutableArray<IParameterSymbol> methodParams,
        ImmutableArray<IParameterSymbol> delegateParams,
        params int[] indicesToOmit
    )
    {
        var expected = new List<IParameterSymbol>(delegateParams.Length);

        for (var i = 0; i < delegateParams.Length; i++)
        {
            if (!indicesToOmit.Contains(i))
            {
                expected.Add(delegateParams[i]);
            }
        }

        if (methodParams.Length != expected.Count)
        {
            return false;
        }

        for (var i = 0; i < methodParams.Length; i++)
        {
            if (!SymbolEqualityComparer.Default.Equals(methodParams[i].Type, expected[i].Type))
            {
                return false;
            }
        }

        return true;
    }
}

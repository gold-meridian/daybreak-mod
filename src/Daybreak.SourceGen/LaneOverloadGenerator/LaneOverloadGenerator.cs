using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Daybreak.Core.SourceGen;

[Generator]
public sealed class LaneOverloadGenerator : IIncrementalGenerator
{
    private enum ConversionKind
    {
        ToLane,
        FromLane,
    }

    private record ConversionModel(
        ConversionKind Kind,
        ITypeSymbol DomainType,
        ITypeSymbol LaneType,
        IMethodSymbol Method
    )
    {
        public static ConversionModel ToLane(
            ITypeSymbol domain,
            ITypeSymbol lane,
            IMethodSymbol method
        )
        {
            return new ConversionModel(ConversionKind.ToLane, domain, lane, method);
        }

        public static ConversionModel FromLane(
            ITypeSymbol lane,
            ITypeSymbol domain,
            IMethodSymbol method
        )
        {
            return new ConversionModel(ConversionKind.FromLane, domain, lane, method);
        }
    }

    private sealed record GenericBinding(
        ITypeParameterSymbol Generic,
        ITypeSymbol DomainType,
        ITypeSymbol LaneType,
        IMethodSymbol ToLane,
        IMethodSymbol FromLane
    );

    private readonly record struct LanePair(
        ITypeSymbol Domain,
        ITypeSymbol Lane,
        IMethodSymbol ToLane,
        IMethodSymbol FromLane
    );

    private readonly record struct MethodModel(
        IMethodSymbol Method,
        ImmutableArray<(ITypeParameterSymbol Symbol, bool IsLaneType)> Generics
    );

    private const string sourcegen_namespace = "Daybreak.Core.SourceGen";
    private const string generate_lane_overloads_attribute_name = sourcegen_namespace + "." + "GenerateLaneOverloadsAttribute";
    private const string to_lane_attribute_name = sourcegen_namespace + "." + "ToLaneAttribute";
    private const string from_lane_attribute_name = sourcegen_namespace + "." + "FromLaneAttribute";
    private const string lane_parameter_attribute_name = sourcegen_namespace + "." + "LaneParameterAttribute";

    private static readonly SymbolDisplayFormat full_method_name_no_parameters_format = new(
        globalNamespaceStyle: SymbolDisplayGlobalNamespaceStyle.Omitted,
        typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
        propertyStyle: SymbolDisplayPropertyStyle.NameOnly,
        genericsOptions: SymbolDisplayGenericsOptions.None,
        memberOptions: SymbolDisplayMemberOptions.IncludeContainingType,
        parameterOptions: SymbolDisplayParameterOptions.None,
        miscellaneousOptions: SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers
    );

    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        var compilation = context.CompilationProvider;

        // TODO: cancellation token
        var conversions = compilation.Select(
            static (c, _) => BuildLaneMap(DiscoverConversions(c))
        );

        var curveMethods =
            context.SyntaxProvider
                   .ForAttributeWithMetadataName(
                        generate_lane_overloads_attribute_name,
                        predicate: static (n, _) => n is MethodDeclarationSyntax,
                        transform: static (ctx, _) => GetCandidate(ctx)
                    )
                   .Where(static m => m is not null)
                   .Select(static (m, _) => m!.Value);

        var combined = curveMethods.Combine(conversions);

        context.RegisterSourceOutput(combined, Emit);
    }

    private static ImmutableArray<ConversionModel> DiscoverConversions(
        Compilation compilation
    )
    {
        var builder = ImmutableArray.CreateBuilder<ConversionModel>();

        foreach (var type in GetAllTypes(compilation.GlobalNamespace))
        {
            foreach (var member in type.GetMembers())
            {
                if (member is not IMethodSymbol method)
                {
                    continue;
                }

                if (!method.IsStatic)
                {
                    continue;
                }

                if (method.Parameters.Length != 1)
                {
                    continue;
                }

                ConversionKind? conversionKind = null;
                foreach (var attr in method.GetAttributes())
                {
                    if (attr.AttributeClass is not { } attributeClass)
                    {
                        continue;
                    }

                    conversionKind = attributeClass.ToDisplayString() switch
                    {
                        to_lane_attribute_name => ConversionKind.ToLane,
                        from_lane_attribute_name => ConversionKind.FromLane,
                        _ => null,
                    };

                    if (conversionKind.HasValue)
                    {
                        break;
                    }
                }

                if (conversionKind is not { } kind)
                {
                    continue;
                }

                switch (kind)
                {
                    case ConversionKind.ToLane:
                        builder.Add(
                            ConversionModel.ToLane(
                                domain: method.Parameters[0].Type,
                                lane: method.ReturnType,
                                method
                            )
                        );
                        break;

                    case ConversionKind.FromLane:
                        builder.Add(
                            ConversionModel.FromLane(
                                lane: method.Parameters[0].Type,
                                domain: method.ReturnType,
                                method
                            )
                        );
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        return builder.ToImmutable();
    }

    private static ImmutableDictionary<ITypeSymbol, ImmutableArray<LanePair>> BuildLaneMap(
        ImmutableArray<ConversionModel> conversions
    )
    {
        var map = new Dictionary<ITypeSymbol, List<LanePair>>(SymbolEqualityComparer.Default);

        foreach (var group in conversions.GroupBy(x => x.DomainType, SymbolEqualityComparer.Default))
        {
            foreach (var to in group.Where(x => x.Kind == ConversionKind.ToLane))
            {
                var from = conversions.FirstOrDefault(
                    x => x.Kind == ConversionKind.FromLane
                      && SymbolEqualityComparer.Default.Equals(x.LaneType, to.LaneType)
                      && SymbolEqualityComparer.Default.Equals(x.DomainType, to.DomainType)
                );

                if (from is null)
                {
                    continue;
                }

                if (!map.TryGetValue(to.DomainType, out var pairs))
                {
                    map[to.DomainType] = pairs = [];
                }

                pairs.Add(
                    new LanePair(
                        Domain: to.DomainType,
                        Lane: to.LaneType,
                        ToLane: to.Method,
                        FromLane: from.Method
                    )
                );
            }
        }

        return map.ToImmutableDictionary(
            x => x.Key,
            x => x.Value.ToImmutableArray(),
            (IEqualityComparer<ITypeSymbol>)SymbolEqualityComparer.Default
        );
    }

    private static MethodModel? GetCandidate(GeneratorAttributeSyntaxContext ctx)
    {
        if (ctx.TargetNode is not MethodDeclarationSyntax methodDecl)
        {
            return null;
        }

        if (ctx.SemanticModel.GetDeclaredSymbol(methodDecl) is not IMethodSymbol methodSym)
        {
            return null;
        }

        // Handled by FAWMN
        /*
        var attr = methodSym
                  .GetAttributes()
                  .FirstOrDefault(x => x.AttributeClass?.ToDisplayString() == generate_lane_overloads_attribute_name);
        if (attr is null)
        {
            return null;
        }
        */

        var generics = methodSym.TypeParameters
                                .Select(x => (x, IsLaneGeneric(x)))
                                .ToImmutableArray();

        return new MethodModel(methodSym, generics);
    }

    private static void Emit(SourceProductionContext ctx, (MethodModel Left, ImmutableDictionary<ITypeSymbol, ImmutableArray<LanePair>> Right) tuple)
    {
        var (model, laneMap) = tuple;
        var method = model.Method;

        var ns = method.ContainingNamespace.ToDisplayString();
        var className = method.ContainingType.Name;
        var methodName = method.Name;

        var sb = new StringBuilder();
        {
            sb.AppendLine($"namespace {ns};");
            sb.AppendLine();
            sb.AppendLine($"partial class {className}");
            sb.AppendLine("{");

            var bindings = GenerateGenericBindings(model, laneMap).ToArray();

            var i = 0;
            foreach (var binding in bindings)
            {
                EmitOverload(sb, model, binding);

                if (i != bindings.Length - 1)
                {
                    sb.AppendLine();
                }

                i++;
            }

            sb.AppendLine("}");
        }

        ctx.AddSource($"{className}.{methodName}.LaneOverloads.g.cs", sb.ToString());
    }

    private static IEnumerable<ImmutableArray<GenericBinding>> GenerateGenericBindings(
        MethodModel model,
        ImmutableDictionary<ITypeSymbol, ImmutableArray<LanePair>> laneMap
    )
    {
        var perGeneric =
            model
               .Generics
               .Where(x => x.IsLaneType)
               .Select(x => x.Symbol)
               .Select(
                    x => laneMap.Values.SelectMany(
                        y => y
                    ).Select(
                        y => new GenericBinding(
                            x,
                            y.Domain,
                            y.Lane,
                            y.ToLane,
                            y.FromLane
                        )
                    ).ToImmutableArray()
                ).ToImmutableArray();

        return CartestianProduct(perGeneric);

        static IEnumerable<ImmutableArray<GenericBinding>> CartestianProduct(
            IReadOnlyList<ImmutableArray<GenericBinding>> lists
        )
        {
            IEnumerable<ImmutableArray<GenericBinding>> acc = [ImmutableArray<GenericBinding>.Empty];

            return lists.Aggregate(
                acc,
                (current, list) => current.SelectMany(
                    _ => list,
                    (x, y) => x.Add(y)
                )
            );
        }
    }

    private static ITypeSymbol RewriteType(
        ITypeSymbol original,
        ImmutableArray<GenericBinding> bindings,
        bool domain
    )
    {
        if (original is ITypeParameterSymbol tp)
        {
            var b = bindings.FirstOrDefault(
                x => SymbolEqualityComparer.Default.Equals(x.Generic, tp)
            );

            if (b is not null)
            {
                return domain ? b.DomainType : b.LaneType;
            }
        }

        return original;
    }

    private static void EmitOverload(
        StringBuilder sb,
        MethodModel model,
        ImmutableArray<GenericBinding> bindings
    )
    {
        var method = model.Method;
        var methodName = method.Name;
        var hasReturn = !method.ReturnsVoid;

        var returnType = hasReturn
            ? RewriteType(method.ReturnType, bindings, domain: true).ToDisplayString()
            : "void";

        var paramList = method.Parameters.Select(
            x =>
            {
                var t = RewriteType(x.Type, bindings, domain: true).ToDisplayString();
                return $"{t} {x.Name}";
            }
        );

        var passthroughGenerics =
            model.Generics
                 .Where(x => !x.IsLaneType)
                 .Select(x => x.Symbol)
                 .ToArray();
        var genericList = passthroughGenerics.Length != 0
            ? $"<{string.Join(", ", passthroughGenerics.Select(x => x.Name))}>"
            : "";

        sb.AppendLine("    /// <inheritdoc />");
        sb.AppendLine("    [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");
        sb.AppendLine($"    public static {returnType} {methodName}{genericList}({string.Join(", ", paramList)})");

        foreach (var generic in passthroughGenerics)
        {
            var parts = new List<string>();

            if (generic.HasUnmanagedTypeConstraint)
            {
                parts.Add("unmanaged");
            }
            else if (generic.HasValueTypeConstraint)
            {
                parts.Add("struct");
            }

            if (generic.HasReferenceTypeConstraint)
            {
                parts.Add("class");
            }

            foreach (var gt in generic.ConstraintTypes)
            {
                parts.Add(gt.ToDisplayString());
            }

            if (generic.HasConstructorConstraint)
            {
                parts.Add("new()");
            }

            if (parts.Count > 0)
            {
                sb.AppendLine($"            where {generic.Name} : {string.Join(", ", parts)}");
            }
        }

        sb.AppendLine("    {");

        var callArgs = method.Parameters.Select(
            x =>
            {
                var binding = bindings.FirstOrDefault(
                    y => SymbolEqualityComparer.Default.Equals(y.Generic, x.Type)
                );

                return binding is null
                    ? x.Name
                    : $"{binding.ToLane.ToDisplayString(full_method_name_no_parameters_format)}({x.Name})";
            }
        ).ToArray();

        sb.Append("        ");
        if (hasReturn)
        {
            sb.Append("var retVal = ");
        }

        sb.AppendLine($"{method.ContainingType}.{method.Name}<");

        var laneGenericIdx = 0;
        for (var i = 0; i < model.Generics.Length; i++)
        {
            var generic = model.Generics[i];

            if (generic.IsLaneType)
            {
                sb.Append($"            {bindings[laneGenericIdx++].LaneType.ToDisplayString()}");
            }
            else
            {
                sb.Append($"            {generic.Symbol.ToDisplayString()}");
            }

            if (i != model.Generics.Length - 1)
            {
                sb.Append(',');
            }

            sb.AppendLine();
        }

        sb.AppendLine("        >(");

        for (var i = 0; i < callArgs.Length; i++)
        {
            var callArg = callArgs[i];

            sb.Append($"            {callArg}");

            if (i != callArgs.Length - 1)
            {
                sb.Append(',');
            }

            sb.AppendLine();
        }

        sb.AppendLine("        );");

        if (hasReturn)
        {
            sb.AppendLine();

            var retBindings = bindings.FirstOrDefault(
                x => SymbolEqualityComparer.Default.Equals(x.Generic, method.ReturnType)
            );

            if (retBindings is not null)
            {
                sb.AppendLine($"        return {retBindings.FromLane.ToDisplayString(full_method_name_no_parameters_format)}(retVal);");
            }
            else
            {
                sb.AppendLine("        return retVal;");
            }
        }

        sb.AppendLine("    }");
    }

    private static bool IsLaneGeneric(ITypeParameterSymbol tp)
    {
        // Can also use constraint-based matching:
        // tp.HasUnamangedTypeConstraint
        // foreach constraint in tp.ConstraintTypes
        //     if constraint is not INamedTypeSymbol named continue
        //     if named.Name != lane type || named.TypeArguments.Length != 1 continue
        //     compare equality of TypeArguments[0] to self (tp)

        return tp.GetAttributes().Any(x => x.AttributeClass?.ToDisplayString() == lane_parameter_attribute_name);
    }

    private static IEnumerable<INamedTypeSymbol> GetAllTypes(INamespaceSymbol ns)
    {
        foreach (var member in ns.GetMembers())
        {
            switch (member)
            {
                case INamespaceSymbol nsMember:
                {
                    foreach (var t in GetAllTypes(nsMember))
                    {
                        yield return t;
                    }

                    break;
                }

                case INamedTypeSymbol type:
                    yield return type;

                    break;
            }
        }
    }
}

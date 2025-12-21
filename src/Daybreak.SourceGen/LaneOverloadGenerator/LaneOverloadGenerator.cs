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

    private readonly record struct LanePair(
        ITypeSymbol Domain,
        ITypeSymbol Lane,
        IMethodSymbol ToLane,
        IMethodSymbol FromLane
    );

    private readonly record struct MethodModel(
        IMethodSymbol Method
    );

    private const string sourcegen_namespace = "Daybreak.Core.SourceGen";
    private const string generate_lane_overloads_attribute_name = sourcegen_namespace + "." + "GenerateLaneOverloadsAttribute";
    private const string to_lane_attribute_name = sourcegen_namespace + "." + "ToLaneAttribute";
    private const string from_lane_attribute_name = sourcegen_namespace + "." + "FromLaneAttribute";

    private static readonly SymbolDisplayFormat full_method_name_no_parameters_formnat = new(
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

    private static ImmutableDictionary<ITypeSymbol, LanePair> BuildLaneMap(
        ImmutableArray<ConversionModel> conversions
    )
    {
        var map = new Dictionary<ITypeSymbol, LanePair>(SymbolEqualityComparer.Default);

        foreach (var group in conversions.GroupBy(x => x.LaneType, SymbolEqualityComparer.Default))
        {
            var to = group.FirstOrDefault(x => x.Kind == ConversionKind.ToLane);
            var from = group.FirstOrDefault(x => x.Kind == ConversionKind.FromLane);

            if (to is null || from is null)
            {
                // TODO: We could emit a diagnostic.
                continue;
            }

            map[to.DomainType] = new LanePair(
                Domain: to.DomainType,
                Lane: to.LaneType,
                ToLane: to.Method,
                FromLane: from.Method
            );
        }

        return map.ToImmutableDictionary(SymbolEqualityComparer.Default);
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

        return new MethodModel(methodSym);
    }

    private static void Emit(SourceProductionContext ctx, (MethodModel Left, ImmutableDictionary<ITypeSymbol, LanePair> Right) tuple)
    {
        var (model, laneMap) = tuple;

        var ns = model.Method.ContainingNamespace.ToDisplayString();
        var className = model.Method.ContainingType.Name;
        var methodName = model.Method.Name;

        var sb = new StringBuilder();
        {
            sb.AppendLine($"namespace {ns};");
            sb.AppendLine();
            sb.AppendLine($"partial class {className}");
            sb.AppendLine("{");

            var i = 0;
            foreach (var kvp in laneMap)
            {
                var lanePair = kvp.Value;

                EmitOverload(sb, model.Method, lanePair);

                if (i != laneMap.Count - 1)
                {
                    sb.AppendLine();
                }

                i++;
            }

            sb.AppendLine("}");
        }

        ctx.AddSource($"{className}.{methodName}.LaneOverloads.g.cs", sb.ToString());
    }

    private static void EmitOverload(
        StringBuilder sb,
        IMethodSymbol method,
        LanePair binding
    )
    {
        var methodName = method.Name;

        sb.AppendLine("     [global::System.Runtime.CompilerServices.MethodImpl(global::System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]");
        sb.AppendLine($"     public static {binding.Domain} {methodName}({binding.Domain} a, {binding.Domain} b, float t)");
        sb.AppendLine("     {");

        sb.AppendLine($"        var retVal = global::{method.ContainingType}.{methodName}<{binding.Lane}>(");
        sb.AppendLine($"            {binding.ToLane.ToDisplayString(full_method_name_no_parameters_formnat)}(a),");
        sb.AppendLine($"            {binding.ToLane.ToDisplayString(full_method_name_no_parameters_formnat)}(b),");
        sb.AppendLine($"            t");
        sb.AppendLine("        );");
        sb.AppendLine();
        sb.AppendLine($"        return {binding.FromLane.ToDisplayString(full_method_name_no_parameters_formnat)}(retVal);");

        sb.AppendLine("    }");
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

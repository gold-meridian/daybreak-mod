using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis.Hooks.Generators;

// Ported grossly from the original text file generator... :(

internal sealed class RoslynGenerator(Compilation compilation, INamedTypeSymbol type)
{
    private static readonly Dictionary<string, string> csharp_keyword_map = new()
    {
        { typeof(bool).FullName!, "bool" },
        { typeof(byte).FullName!, "byte" },
        { typeof(sbyte).FullName!, "sbyte" },
        { typeof(short).FullName!, "short" },
        { typeof(ushort).FullName!, "ushort" },
        { typeof(int).FullName!, "int" },
        { typeof(uint).FullName!, "uint" },
        { typeof(long).FullName!, "long" },
        { typeof(ulong).FullName!, "ulong" },
        { typeof(float).FullName!, "float" },
        { typeof(double).FullName!, "double" },
        { typeof(decimal).FullName!, "decimal" },
        { typeof(string).FullName!, "string" },
        { typeof(object).FullName!, "object" },
        { typeof(void).FullName!, "void" },
        { typeof(char).FullName!, "char" },
        { typeof(nint).FullName!, "nint" },
        { typeof(nuint).FullName!, "nuint" },
    };

    public string BuildType(string ns, string typeName, TypeHookDefinition def)
    {
        var sb = new StringBuilder();

        var hooks = ResolveHooksFromType(type, def.Exclusions);
        foreach (var super in def.SuperTypes)
        {
            var superType = compilation.GetTypeByMetadataName(super.TypeMetadataName);
            if (superType is null)
            {
                continue;
            }

            hooks = hooks.Concat(ResolveHooksFromType(superType, super.Exclusions)).ToArray();
        }

        sb.AppendLine("#nullable enable");
        sb.AppendLine();
        sb.AppendLine("using Daybreak.EarlyLoader;");
        sb.AppendLine("using Daybreak.Hooks;");
        sb.AppendLine("using Terraria.ModLoader;");
        sb.AppendLine();
        sb.AppendLine("// ReSharper disable ConvertToPrimaryConstructor");
        sb.AppendLine("// ReSharper disable RedundantLambdaParameterType");
        sb.AppendLine("// ReSharper disable PartialTypeWithSinglePart");
        sb.AppendLine("// ReSharper disable UnusedType.Global");
        sb.AppendLine("// ReSharper disable InconsistentNaming");
        sb.AppendLine("// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident");
        sb.AppendLine("// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract");
        sb.AppendLine("#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member");
        sb.AppendLine();
        sb.AppendLine($"namespace {ns};");
        sb.AppendLine();
        sb.AppendLine($"// Hooks to generate for '{def.TypeMetadataName}':");
        foreach (var hook in hooks)
        {
            sb.AppendLine($"//     {hook}");
        }

        sb.AppendLine($"internal static partial class {typeName}");
        sb.AppendLine("{");
        var ranOnce = false;
        foreach (var method in hooks)
        {
            if (ranOnce)
            {
                sb.AppendLine();
            }

            ranOnce = true;

            sb.Append(BuildHook(method, hasOverloads: type.GetMembers().OfType<IMethodSymbol>().Count(x => x.Name == method.Name) > 1));
        }

        sb.AppendLine("}");

        foreach (var method in hooks)
        {
            sb.AppendLine();
            sb.Append(BuildImpl(method, hasOverloads: type.GetMembers().OfType<IMethodSymbol>().Count(x => x.Name == method.Name) > 1, typeName));
        }

        return sb.ToString().TrimEnd();
    }

    private string BuildImpl(IMethodSymbol method, bool hasOverloads, string hooksName)
    {
        var sb = new StringBuilder();

        var name = method.Name;
        var hookName = GetNameWithoutOverloadCollision(method, hasOverloads);

        var implName = $"{type.Name}_{hookName}_Impl";
        sb.AppendLine("[Terraria.ModLoader.Autoload(false)]");
        sb.AppendLine($"internal sealed partial class {implName} : {type.GetFullMetadataName()}");
        sb.AppendLine("{");

        sb.AppendLine("    [field: Terraria.ModLoader.CloneByReference]");
        sb.AppendLine($"    private readonly {hooksName}.{hookName}.Definition hook;");
        sb.AppendLine();

        sb.AppendLine("    [field: Terraria.ModLoader.CloneByReference]");
        sb.AppendLine("    public override string Name => base.Name + '_' + field;");
        sb.AppendLine();

        if (HasProperty(type, "InstancePerEntity"))
        {
            sb.AppendLine("    public override bool InstancePerEntity => true;");
            sb.AppendLine();
        }

        if (HasProperty(type, "CloneNewInstances"))
        {
            sb.AppendLine("    protected override bool CloneNewInstances => true;");
            sb.AppendLine();
        }

        sb.AppendLine($"    public {implName}({hooksName}.{hookName}.Definition hook)");
        sb.AppendLine("    {");
        sb.AppendLine("        this.hook = hook;");
        sb.AppendLine("        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));");
        sb.AppendLine("    }");
        sb.AppendLine();

        /*
        if (GetGenericTypeOfName(type, "Terraria.ModLoader.GlobalType`2") is { } gGlobalType)
        {
            var tEntity = gGlobalType.GenericArguments[0].FullName;
            var tGlobal = gGlobalType.GenericArguments[1].FullName;
            sb.AppendLine($"    public override {tGlobal} Clone({tEntity}? from, {tEntity} to)");
            sb.AppendLine("    {");
            sb.AppendLine($"        return new {implName}(hook);");
            sb.AppendLine("    }");
            sb.AppendLine();
        }
        else if (GetGenericTypeOfName(type, "Terraria.ModLoader.ModType`2") is { } gModType)
        {
            var tEntity = gModType.GenericArguments[0].FullName;
            var tModType = gModType.GenericArguments[1].FullName;
            sb.AppendLine($"    public override {tModType} Clone({tEntity} newEntity)");
            sb.AppendLine("    {");
            sb.AppendLine($"        return new {implName}(hook);");
            sb.AppendLine("    }");
            sb.AppendLine();
        }
        */

        sb.Append($"    public override {GetFullTypeNameOrCSharpKeyword(method.ReturnType, method.ReturnsByRef, includeRefPrefix: true)} {name}(");
        if (method.Parameters.Length > 0)
        {
            sb.AppendLine();

            for (var i = 0; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                sb.Append($"        {GetParameterDefinition(parameter)}");
                if (i < method.Parameters.Length - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            sb.AppendLine("    )");
        }
        else
        {
            sb.AppendLine(")");
        }

        sb.AppendLine("    {");

        if (method.ReturnType.SpecialType == SpecialType.System_Void)
        {
            sb.AppendLine("        hook(");
        }
        else
        {
            sb.AppendLine("        return hook(");
        }

        if (method.Parameters.Length > 0)
        {
            sb.AppendLine("            (");

            for (var i = 0; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                sb.Append($"                {GetParameterDefinition(parameter)}_captured");
                if (i < method.Parameters.Length - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }

            sb.Append("            )");
        }
        else
        {
            sb.Append("            ()");
        }

        if (method.Parameters.Length > 0)
        {
            sb.AppendLine($" => base.{name}(");

            for (var i = 0; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                sb.AppendLine($"                {GetParameterReference(parameter)}_captured{(i < method.Parameters.Length - 1 ? "," : "")}");
            }

            sb.AppendLine("            ),");
        }
        else
        {
            sb.AppendLine($" => base.{name}(),");
        }

        sb.Append("            this");

        if (method.Parameters.Length > 0)
        {
            sb.AppendLine(",");

            for (var i = 0; i < method.Parameters.Length; i++)
            {
                var parameter = method.Parameters[i];
                sb.AppendLine($"            {GetParameterReference(parameter)}{(i < method.Parameters.Length - 1 ? "," : "")}");
            }
        }
        else
        {
            sb.AppendLine();
        }

        sb.AppendLine("        );");

        sb.AppendLine("    }");

        sb.AppendLine("}");

        return sb.ToString();

        /*
        static GenericInstanceType? GetGenericTypeOfName(TypeDefinition? type, string name)
        {
            while (type is not null)
            {
                if (type.BaseType is GenericInstanceType genericInstanceType && genericInstanceType.ElementType.FullName == name)
                {
                    return genericInstanceType;
                }

                type = type.BaseType?.Resolve();
            }

            return null;
        }
        */

        static bool HasProperty(ITypeSymbol? type, string name)
        {
            while (true)
            {
                if (type is null)
                {
                    return false;
                }

                if (type.GetMembers(name).OfType<IPropertySymbol>().Any())
                {
                    return true;
                }

                type = type.BaseType;
            }
        }
    }

    private string BuildHook(IMethodSymbol method, bool hasOverloads)
    {
        var sb = new StringBuilder();
        var name = GetNameWithoutOverloadCollision(method, hasOverloads);

        var typeName = type.Name;

        sb.AppendLine("    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]");
        sb.AppendLine($"    [HookMetadata(TypeContainingEvent = typeof({name}), EventName = \"Event\", DelegateName = \"Definition\")]");
        sb.AppendLine($"    public sealed class {name}Attribute : SubscribesToAttribute;");
        sb.AppendLine();
        sb.AppendLine($"    public sealed partial class {name}");
        sb.AppendLine("    {");
        sb.AppendLine(GetDescriptionForMethod(method, original: true));
        sb.AppendLine();
        sb.AppendLine(GetDescriptionForMethod(method, original: false));
        sb.AppendLine();
        sb.AppendLine("        public static event Definition? Event");
        sb.AppendLine("        {");
        sb.AppendLine($"            add => EarlyLoadHooks.GetModOrThrow().AddContent(new {typeName}_{name}_Impl(value ?? throw new System.InvalidOperationException(\"Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: {typeName}::{name}\")));");
        sb.AppendLine();
        sb.AppendLine($"            remove => throw new System.InvalidOperationException(\"Cannot remove DAYBREAK-generated mod loader hook: {typeName}::{name}; use a flag to disable behavior.\");");
        sb.AppendLine("        }");
        sb.AppendLine("    }");

        return sb.ToString();
    }

    private static string GetNameWithoutOverloadCollision(IMethodSymbol method, bool hasOverloads)
    {
        // TODO: This logic for resolving overloads is *very* naive.  For a more
        //       reliable approach, see how MonoMod does it:
        // https://github.com/MonoMod/MonoMod/blob/reorganize/src/MonoMod.RuntimeDetour.HookGen/HookGenerator.cs#L234

        var name = method.Name;
        if (!hasOverloads)
        {
            return name;
        }

        foreach (var param in method.Parameters)
        {
            name += "_" + GetFullTypeNameOrCSharpKeyword(param.Type, isByRef: false, includeRefPrefix: false).Split('.').Last();
        }

        return name;
    }

    private static IMethodSymbol[] ResolveHooksFromType(INamedTypeSymbol type, List<string> exclusions)
    {
        return type.GetMembers()
                   .OfType<IMethodSymbol>()
                   .Where(
                        m =>
                            m.DeclaredAccessibility == Accessibility.Public
                         && m is
                            {
                                IsVirtual: true,
                                IsSealed: false,
                                IsStatic: false,
                            }
                         && !exclusions.Contains(m.Name)
                         && !m.GetAttributes().Any(
                                a =>
                                    a.AttributeClass?.ToDisplayString() == typeof(System.ObsoleteAttribute).FullName
                            )
                    )
                   .ToArray();
    }

    private static string GetDescriptionForMethod(IMethodSymbol method, bool original)
    {
        var sb = new StringBuilder();

        var name = original ? "Original" : "Definition";

        if (!original && method.ReturnType.SpecialType != SpecialType.System_Void)
        {
            sb.AppendLine($"        [return: PermitsVoidInvokeParameterWithParameters(\"orig\")]");
        }

        sb.Append($"        public delegate {GetFullTypeNameOrCSharpKeyword(method.ReturnType, method.ReturnsByRef, includeRefPrefix: true)} {name}(");

        var parameters = method.Parameters;

        if (!original)
        {
            sb.AppendLine();

            sb.AppendLine("            [Omittable] Original orig,");
            sb.Append($"            [Omittable] {GetFullTypeNameOrCSharpKeyword(method.ContainingType, isByRef: false, includeRefPrefix: false)} self");
        }

        if (parameters.Length > 0)
        {
            if (!original)
            {
                sb.AppendLine(",");
            }
            else
            {
                sb.AppendLine();
            }

            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                sb.Append($"            {GetParameterDefinition(parameter)}");
                if (i < parameters.Length - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }
        }

        if (original && parameters.Length == 0)
        {
            sb.Append(");");
        }
        else
        {
            if (!original && parameters.Length == 0)
            {
                sb.AppendLine();
            }

            sb.Append("        );");
        }

        return sb.ToString();
    }

    private static string GetParameterDefinition(IParameterSymbol parameter)
    {
        var prefix = GetReferencePrefix(parameter);
        var type = GetFullTypeNameOrCSharpKeyword(parameter.Type, isByRef: false, includeRefPrefix: false);
        var name = parameter.Name;

        return prefix + type + ' ' + name;
    }

    private static string GetFullTypeNameOrCSharpKeyword(ITypeSymbol type, bool isByRef, bool includeRefPrefix = false)
    {
        var prefix = includeRefPrefix && isByRef ? "ref " : "";

        // Nullable<T>
        if (type is INamedTypeSymbol { OriginalDefinition.SpecialType: SpecialType.System_Nullable_T } nullable)
        {
            var inner = nullable.TypeArguments[0];
            return prefix +
                   GetFullTypeNameOrCSharpKeyword(inner, isByRef: false, includeRefPrefix: false) +
                   "?";
        }

        // Arrays
        if (type is IArrayTypeSymbol arrayType)
        {
            var elementType = GetFullTypeNameOrCSharpKeyword(arrayType.ElementType, isByRef: false, includeRefPrefix: false);
            var rank = new string(',', arrayType.Rank - 1);
            return prefix + $"{elementType}[{rank}]";
        }

        // Named types (classes, structs, generics, primitives)
        if (type is INamedTypeSymbol named)
        {
            // Generic types aside from Nullable<T>
            if (named.IsGenericType)
            {
                var baseName = GetCSharpRepresentation(named.OriginalDefinition.GetFullMetadataName());

                var args = string.Join(
                    ", ",
                    named.TypeArguments.Select(
                        a =>
                            GetFullTypeNameOrCSharpKeyword(a, isByRef: false, includeRefPrefix: false)
                    )
                );

                return prefix + $"{baseName}<{args}>";
            }
            
            var simpleName = GetCSharpRepresentation(named.GetFullMetadataName());

            if (csharp_keyword_map.TryGetValue(simpleName, out var keyword))
            {
                return prefix + keyword;
            }

            return prefix + simpleName;
        }

        // Fallback (type parameters, pointers, etc.)
        var fallback = type.GetFullMetadataName();
        return prefix + GetCSharpRepresentation(fallback);
    }

    private static string GetReferencePrefix(IParameterSymbol parameter)
    {
        return parameter.RefKind switch
        {
            RefKind.None => "",
            RefKind.Ref => "ref ",
            RefKind.Out => "out ",
            RefKind.In => "in ",
            _ => "", // TODO: scary
        };
    }

    private static string GetCSharpRepresentation(string fullName)
    {
        fullName = fullName.Replace('/', '.');
        fullName = fullName.Replace('+', '.');

        if (fullName.EndsWith("&"))
        {
            fullName = fullName[..^1];
        }

        // Generic parameters are denoted as `n at the end of the name where n
        // is the # of generic parameters.
        var index = fullName.IndexOf('`');
        if (index != -1)
        {
            fullName = fullName[..index];
        }

        return fullName;
    }

    private static string GetParameterReference(IParameterSymbol parameter)
    {
        return GetReferencePrefix(parameter) + parameter.Name;
    }
}

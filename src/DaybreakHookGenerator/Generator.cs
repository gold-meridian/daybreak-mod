using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mono.Cecil;
using Mono.Cecil.Rocks;

using MonoMod.Utils;

namespace DaybreakHookGenerator;

public sealed class Generator(ModuleDefinition module, TypeDefinition type)
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

    private static readonly InvokeStrategy default_invoke_strategy = new SimpleVoidInvokeStrategy();

    public string BuildType(string typeNamespace, string typeName, List<string> excludedHooks, Dictionary<string, InvokeStrategy> strategies)
    {
        var sb = new StringBuilder();

        var hooks = ResolveHooksFromType(type, excludedHooks);

        sb.AppendLine($"namespace {typeNamespace};");
        sb.AppendLine();
        sb.AppendLine("using System.Linq;");
        sb.AppendLine();
        sb.AppendLine("// ReSharper disable PartialTypeWithSinglePart");
        sb.AppendLine("// ReSharper disable UnusedType.Global");
        sb.AppendLine("// ReSharper disable InconsistentNaming");
        sb.AppendLine("// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident");
        sb.AppendLine("// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract");
        sb.AppendLine("#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member");
        sb.AppendLine();
        sb.AppendLine($"// Hooks to generate for '{type.FullName}':");
        foreach (var hook in hooks)
        {
            sb.AppendLine($"//     {hook}");
        }
        sb.AppendLine($"public static partial class {typeName}");
        sb.AppendLine("{");
        var ranOnce = false;
        foreach (var hook in hooks)
        {
            if (ranOnce)
            {
                sb.AppendLine();
            }
            ranOnce = true;

            sb.Append(BuildHook(hook, hasOverloads: type.GetMethods().Count(x => x.Name == hook.Name) > 1, strategies));
        }
        sb.AppendLine("}");

        sb.AppendLine();
        sb.AppendLine($"public sealed partial class {type.Name}Impl : {type.FullName}");
        sb.AppendLine("{");
        ranOnce = false;
        foreach (var hook in hooks)
        {
            if (ranOnce)
            {
                sb.AppendLine();
            }
            ranOnce = true;

            sb.Append(BuildImpl(hook, hasOverloads: type.GetMethods().Count(x => x.Name == hook.Name) > 1, typeName));
        }
        sb.AppendLine("}");

        return sb.ToString();
    }

    private static string BuildImpl(MethodDefinition method, bool hasOverloads, string hooksName)
    {
        var sb = new StringBuilder();
        var name = method.Name;
        var hookName = GetNameWithoutOverloadCollision(method, hasOverloads);

        sb.Append($"    public override {GetFullTypeNameOrCSharpKeyword(method.ReturnType, includeRefPrefix: true)} {name}(");
        if (method.Parameters.Count > 0)
        {
            sb.AppendLine();

            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                sb.Append($"        {GetParameterDefinition(parameter)}");
                if (i < method.Parameters.Count - 1)
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
        sb.AppendLine($"        if (!{hooksName}.{hookName}.GetInvocationList().Any())");
        sb.AppendLine("        {");
        if (method.ReturnType.FullName == "System.Void")
        {
            sb.Append($"            base.{name}(");
            if (method.Parameters.Count > 0)
            {
                sb.AppendLine();

                for (var i = 0; i < method.Parameters.Count; i++)
                {
                    var parameter = method.Parameters[i];
                    sb.AppendLine($"                {GetParameterReference(parameter)}{(i < method.Parameters.Count - 1 ? "," : "")}");
                }
                sb.AppendLine("            );");
            }
            else
            {
                sb.AppendLine(");");
            }

            sb.AppendLine("            return;");
        }
        else
        {
            // return base
            sb.Append($"            return base.{name}(");
            if (method.Parameters.Count > 0)
            {
                sb.AppendLine();

                for (var i = 0; i < method.Parameters.Count; i++)
                {
                    var parameter = method.Parameters[i];
                    sb.AppendLine($"                {GetParameterReference(parameter)}{(i < method.Parameters.Count - 1 ? "," : "")}");
                }

                sb.AppendLine("            );");
            }
            else
            {
                sb.AppendLine(");");
            }
        }

        sb.AppendLine("        }");
        sb.AppendLine();

        if (method.ReturnType.FullName == "System.Void")
        {
            sb.AppendLine($"        {hooksName}.{hookName}.Invoke(");
        }
        else
        {
            sb.AppendLine($"        return {hooksName}.{hookName}.Invoke(");
        }

        sb.Append("            this");

        if (method.Parameters.Count > 0)
        {
            sb.AppendLine(",");

            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                sb.AppendLine($"            {GetParameterReference(parameter)}{(i < method.Parameters.Count - 1 ? "," : "")}");
            }
        }
        else
        {
            sb.AppendLine();
        }

        sb.AppendLine("        );");

        sb.AppendLine("    }");

        return sb.ToString();
    }

    private static string BuildHook(
        MethodDefinition method,
        bool hasOverloads,
        Dictionary<string, InvokeStrategy> strategies
    )
    {
        var sb = new StringBuilder();
        var name = GetNameWithoutOverloadCollision(method, hasOverloads);

        sb.AppendLine($"    public sealed partial class {name}");
        sb.AppendLine("    {");
        sb.AppendLine(GetDescriptionForMethod(method));
        sb.AppendLine();
        sb.AppendLine("        public static event Definition? Event;");
        sb.AppendLine();
        sb.AppendLine("        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()");
        sb.AppendLine("        {");
        sb.AppendLine("            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine(GenerateInvokeMethod(method, strategies.GetValueOrDefault(name)));
        sb.AppendLine("    }");

        return sb.ToString();
    }

    private static string GetNameWithoutOverloadCollision(MethodDefinition method, bool hasOverloads)
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
            name += "_" + GetFullTypeNameOrCSharpKeyword(param.ParameterType, includeRefPrefix: false).Split('.').Last();
        }

        return name;
    }

    private static MethodDefinition[] ResolveHooksFromType(TypeDefinition typeDef, List<string> excludedHooks)
    {
        var methods = typeDef.GetMethods().Where(
            x => x is
                 {
                     IsPublic: true, // Is accessible (ignore protected, too)
                     IsVirtual: true, // Is overridable
                     IsFinal: false, // Is not sealed
                 }
              && !excludedHooks.Contains(x.Name)
              && x.GetCustomAttribute(typeof(ObsoleteAttribute).FullName!) is null
        );

        return methods.ToArray();
    }

    private static string GetDescriptionForMethod(MethodDefinition method)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"        public delegate {GetFullTypeNameOrCSharpKeyword(method.ReturnType, includeRefPrefix: true)} Definition(");

        var parameters = method.Parameters;

        sb.Append($"            {GetFullTypeNameOrCSharpKeyword(method.DeclaringType, includeRefPrefix: false)} self");
        if (parameters.Count > 0)
        {
            sb.AppendLine(",");

            for (var i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                sb.Append($"            {GetParameterDefinition(parameter)}");
                if (i < parameters.Count - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }
        }
        else
        {
            sb.AppendLine();
        }

        sb.Append("        );");

        return sb.ToString();
    }

    private static string GenerateInvokeMethod(MethodDefinition method, InvokeStrategy? strategy)
    {
        var sb = new StringBuilder();

        sb.AppendLine("        public static " + GetFullTypeNameOrCSharpKeyword(method.ReturnType, includeRefPrefix: true) + " Invoke(");
        sb.Append($"            {GetFullTypeNameOrCSharpKeyword(method.DeclaringType, includeRefPrefix: false)} self");
        if (method.Parameters.Count > 0)
        {
            sb.AppendLine(",");

            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                sb.Append($"            {GetParameterDefinition(parameter)}");
                if (i < method.Parameters.Count - 1)
                {
                    sb.AppendLine(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }
        }
        else
        {
            sb.AppendLine();
        }

        sb.AppendLine("        )");
        sb.AppendLine("        {");
        sb.Append((strategy ?? default_invoke_strategy).GenerateMethodBody(method));
        sb.Append("        }");

        return sb.ToString();
    }

    public static string GetParameterDefinition(ParameterDefinition parameter)
    {
        var prefix = GetReferencePrefix(parameter);
        var type = GetFullTypeNameOrCSharpKeyword(parameter.ParameterType, includeRefPrefix: false);
        var name = parameter.Name;

        return prefix + type + ' ' + name;
    }

    public static string GetFullTypeNameOrCSharpKeyword(TypeReference type, bool includeRefPrefix)
    {
        var prefix = includeRefPrefix && type.IsByReference ? "ref " : "";

        if (type is GenericInstanceType genericType)
        {
            // Special case for System.Nullable
            if (genericType.ElementType.FullName == "System.Nullable`1")
            {
                return prefix + GetFullTypeNameOrCSharpKeyword(genericType.GenericArguments[0], includeRefPrefix) + "?";
            }

            var genericArgs = string.Join(", ", genericType.GenericArguments.Select(arg => GetFullTypeNameOrCSharpKeyword(arg, includeRefPrefix: false)));
            var baseTypeName = GetCSharpRepresentation(genericType.ElementType.FullName);
            return prefix + $"{baseTypeName}<{genericArgs}>";
        }

        if (type is ArrayType arrayType)
        {
            var elementType = GetFullTypeNameOrCSharpKeyword(arrayType.ElementType, includeRefPrefix: false);
            var rank = new string(',', arrayType.Rank - 1);
            return prefix + $"{elementType}[{rank}]";
        }

        var csharpName = GetCSharpRepresentation(type.FullName);

        if (csharp_keyword_map.TryGetValue(csharpName, out var keyword))
        {
            return prefix + keyword;
        }

        return prefix + csharpName;
    }

    public static string GetReferencePrefix(ParameterDefinition parameter)
    {
        if (parameter.IsOut)
        {
            return "out ";
        }

        if (parameter.IsIn)
        {
            return "in ";
        }

        if (parameter.ParameterType.IsByReference)
        {
            return "ref ";
        }

        return "";
    }

    public static string GetCSharpRepresentation(string fullName)
    {
        fullName = fullName.Replace('/', '.');
        fullName = fullName.Replace('+', '.');

        if (fullName.EndsWith('&'))
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

    public static string GetParameterReference(ParameterDefinition parameter)
    {
        return GetReferencePrefix(parameter) + parameter.Name;
    }
}
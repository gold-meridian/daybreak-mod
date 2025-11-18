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

    public string BuildType(string typeNamespace, string typeName, TypeHookDefinition hookDefinition)
    {
        var sb = new StringBuilder();

        var hooks = ResolveHooksFromType(type, hookDefinition.Exclusions);
        foreach (var superType in hookDefinition.SuperTypes)
        {
            var superHooks = ResolveHooksFromType(module.GetType(superType.Type.FullName), superType.Exclusions);
            hooks = hooks.Concat(superHooks).ToArray();
        }

        sb.AppendLine($"namespace {typeNamespace};");
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

            sb.Append(BuildHook(hook, hasOverloads: type.GetMethods().Count(x => x.Name == hook.Name) > 1));
        }

        sb.AppendLine("}");

        foreach (var hook in hooks)
        {
            sb.AppendLine();
            sb.Append(BuildImpl(hook, hasOverloads: type.GetMethods().Count(x => x.Name == hook.Name) > 1, typeName));
        }

        return sb.ToString().TrimEnd();
    }

    private string BuildImpl(MethodDefinition method, bool hasOverloads, string hooksName)
    {
        var sb = new StringBuilder();

        var name = method.Name;
        var hookName = GetNameWithoutOverloadCollision(method, hasOverloads);

        var implName = $"{type.Name}_{hookName}_Impl";
        sb.AppendLine("[Terraria.ModLoader.Autoload(false)]");
        sb.AppendLine($"public sealed partial class {implName} : {type.FullName}");
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

        if (method.ReturnType.FullName == "System.Void")
        {
            sb.AppendLine("        hook(");
        }
        else
        {
            sb.AppendLine("        return hook(");
        }

        if (method.Parameters.Count > 0)
        {
            sb.AppendLine("            (");

            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                sb.Append($"                {GetParameterDefinition(parameter)}_captured");
                if (i < method.Parameters.Count - 1)
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

        if (method.Parameters.Count > 0)
        {
            sb.AppendLine($" => base.{name}(");

            for (var i = 0; i < method.Parameters.Count; i++)
            {
                var parameter = method.Parameters[i];
                sb.AppendLine($"                {GetParameterReference(parameter)}_captured{(i < method.Parameters.Count - 1 ? "," : "")}");
            }

            sb.AppendLine("            ),");
        }
        else
        {
            sb.AppendLine($" => base.{name}(),");
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

        sb.AppendLine("}");

        return sb.ToString();

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

        static bool HasProperty(TypeDefinition? type, string name)
        {
            while (true)
            {
                if (type is null)
                {
                    return false;
                }

                if (type.FindProperty(name) is not null)
                {
                    return true;
                }

                type = type.BaseType?.Resolve();
            }
        }
    }

    private string BuildHook(
        MethodDefinition method,
        bool hasOverloads
    )
    {
        var sb = new StringBuilder();
        var name = GetNameWithoutOverloadCollision(method, hasOverloads);

        var typeName = type.Name;

        sb.AppendLine($"    public sealed partial class {name}");
        sb.AppendLine("    {");
        sb.AppendLine(GetDescriptionForMethod(method, original: true));
        sb.AppendLine();
        sb.AppendLine(GetDescriptionForMethod(method, original: false));
        sb.AppendLine();
        sb.AppendLine("        public static event Definition? Event");
        sb.AppendLine("        {");
        sb.AppendLine($"            add => HookLoader.GetModOrThrow().AddContent(new {typeName}_{name}_Impl(value ?? throw new System.InvalidOperationException(\"Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: {typeName}::{name}\")));");
        sb.AppendLine();
        sb.AppendLine($"            remove => throw new System.InvalidOperationException(\"Cannot remove DAYBREAK-generated mod loader hook: {typeName}::{name}; use a flag to disable behavior.\");");
        sb.AppendLine("        }");
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
                     IsPublic: true,  // Is accessible (ignore protected, too)
                     IsVirtual: true, // Is overridable
                     IsFinal: false,  // Is not sealed
                 }
              && !excludedHooks.Contains(x.Name)
              && x.GetCustomAttribute(typeof(ObsoleteAttribute).FullName!) is null
        );

        return methods.ToArray();
    }

    private static string GetDescriptionForMethod(MethodDefinition method, bool original)
    {
        var sb = new StringBuilder();

        var name = original ? "Original" : "Definition";
        sb.Append($"        public delegate {GetFullTypeNameOrCSharpKeyword(method.ReturnType, includeRefPrefix: true)} {name}(");

        var parameters = method.Parameters;

        if (!original)
        {
            sb.AppendLine();

            sb.AppendLine("            Original orig,");
            sb.Append($"            {GetFullTypeNameOrCSharpKeyword(method.DeclaringType, includeRefPrefix: false)} self");
        }

        if (parameters.Count > 0)
        {
            if (!original)
            {
                sb.AppendLine(",");
            }
            else
            {
                sb.AppendLine();
            }

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

        if (original && parameters.Count == 0)
        {
            sb.Append(");");
        }
        else
        {
            if (!original && parameters.Count == 0)
            {
                sb.AppendLine();
            }

            sb.Append("        );");
        }

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

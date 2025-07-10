using System.Linq;
using System.Text;

using Mono.Cecil;

namespace DaybreakHookGenerator;

public abstract class InvokeStrategy
{
    public const string INDENT = "            ";

    public abstract string GenerateMethodBody(MethodDefinition method);

    public static string Invoke(MethodDefinition method, string member)
    {
        var invokeExpr = method.Parameters.Count > 0
            ? "Invoke(self, " + string.Join(", ", method.Parameters.Select(Generator.GetParameterReference)) + ")"
            : "Invoke(self)";

        return $"{member}.{invokeExpr}";
    }
}

internal sealed class SimpleVoidInvokeStrategy : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        return $"{INDENT}{Invoke(method, "Event?")};\n";
    }
}

internal sealed class BoolCombinerStrategy(bool defaultValue, string combiner) : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{INDENT}var result = {defaultValue.ToString().ToLowerInvariant()};");
        sb.AppendLine($"{INDENT}if (Event == null)");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    return result;");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}foreach (var handler in GetInvocationList())");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    result {combiner} {Invoke(method, "handler")};");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}return result;");

        return sb.ToString();
    }
}

internal sealed class EarlyReturnOnTrueStrategy : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{INDENT}if (Event == null)");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    return false;");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}foreach (var handler in GetInvocationList())");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    if ({Invoke(method, "handler")})");
        sb.AppendLine($"{INDENT}    {{");
        sb.AppendLine($"{INDENT}        return true;");
        sb.AppendLine($"{INDENT}    }}");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}return false;");

        return sb.ToString();
    }
}

internal sealed class EarlyReturnOnFalseStrategy : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{INDENT}if (Event == null)");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    return true;");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}foreach (var handler in GetInvocationList())");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    if (!{Invoke(method, "handler")})");
        sb.AppendLine($"{INDENT}    {{");
        sb.AppendLine($"{INDENT}        return false;");
        sb.AppendLine($"{INDENT}    }}");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}return true;");

        return sb.ToString();
    }
}

internal sealed class NullableValueMayBeOverriddenStrategy(string typeName) : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{INDENT}var result = default({typeName}?);");
        sb.AppendLine($"{INDENT}if (Event == null)");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    return result;");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}foreach (var handler in GetInvocationList())");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    var newValue = {Invoke(method, "handler")};");
        sb.AppendLine($"{INDENT}    if (newValue != null)");
        sb.AppendLine($"{INDENT}    {{");
        sb.AppendLine($"{INDENT}        result = newValue;");
        sb.AppendLine($"{INDENT}    }}");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}return result;");

        return sb.ToString();
    }
}

internal sealed class NullableBooleanCombinerStrategy : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{INDENT}var result = default(bool?);");
        sb.AppendLine($"{INDENT}if (Event == null)");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    return result;");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}foreach (var handler in GetInvocationList())");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    var newValue = {Invoke(method, "handler")};");
        sb.AppendLine($"{INDENT}    if (newValue.HasValue)");
        sb.AppendLine($"{INDENT}    {{");
        sb.AppendLine($"{INDENT}        result &= newValue;");
        sb.AppendLine($"{INDENT}    }}");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}return result;");

        return sb.ToString();
    }
}

internal sealed class NullableBooleanEarlyReturnStrategy : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{INDENT}var result = default(bool?);");
        sb.AppendLine($"{INDENT}if (Event == null)");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    return result;");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}foreach (var handler in GetInvocationList())");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    var newValue = {Invoke(method, "handler")};");
        sb.AppendLine($"{INDENT}    if (newValue.HasValue)");
        sb.AppendLine($"{INDENT}    {{");
        sb.AppendLine($"{INDENT}        if (!newValue.Value)");
        sb.AppendLine($"{INDENT}        {{");
        sb.AppendLine($"{INDENT}            return false;");
        sb.AppendLine($"{INDENT}        }}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}        result = true;");
        sb.AppendLine($"{INDENT}    }}");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}return result;");

        return sb.ToString();
    }
}

internal sealed class ArrayCombinerStrategy(string typeName) : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine($"{INDENT}var result = new System.Collections.Generic.List<{typeName}>();");
        sb.AppendLine($"{INDENT}if (Event == null)");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    return result.ToArray();");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}foreach (var handler in GetInvocationList())");
        sb.AppendLine($"{INDENT}{{");
        sb.AppendLine($"{INDENT}    var newValue = {Invoke(method, "handler")};");
        sb.AppendLine($"{INDENT}    if (newValue != null)");
        sb.AppendLine($"{INDENT}    {{");
        sb.AppendLine($"{INDENT}        result.AddRange(newValue);");
        sb.AppendLine($"{INDENT}    }}");
        sb.AppendLine($"{INDENT}}}");
        sb.AppendLine();
        sb.AppendLine($"{INDENT}return result.ToArray();");
        
        return sb.ToString();
    }
}

internal sealed class VoidReturnButInitializeOutParametersStrategy : InvokeStrategy
{
    public override string GenerateMethodBody(MethodDefinition method)
    {
        var sb = new StringBuilder();
        
        foreach (var parameter in method.Parameters)
        {
            if (parameter.IsOut)
            {
                sb.AppendLine($"{INDENT}{parameter.Name} = default;");
            }
        }

        sb.AppendLine();
        sb.AppendLine($"{INDENT}{Invoke(method, "Event?")};");

        return sb.ToString();
    }
}
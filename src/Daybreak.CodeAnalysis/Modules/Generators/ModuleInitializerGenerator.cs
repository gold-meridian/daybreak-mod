using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Daybreak.CodeAnalysis.Modules.Generators;

[Generator]
public sealed class ModuleInitializerGenerator : IIncrementalGenerator
{
    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        var compilation = context.CompilationProvider;

        context.RegisterImplementationSourceOutput(
            compilation,
            static (ctx, compilation) =>
            {
                var assemblyName = compilation.AssemblyName;
                if (assemblyName is null)
                {
                    return;
                }

                ctx.AddSource("ModuleInitializerRunner.g.cs", SourceText.From(GenerateModuleInitializer(assemblyName), Encoding.UTF8));
            }
        );
    }

    private static string GenerateModuleInitializer(string assemblyName)
    {
        var sb = new StringBuilder();

        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using System.Reflection;");
        sb.AppendLine("using System.Runtime.CompilerServices;");
        sb.AppendLine("using System.Runtime.Loader;");
        sb.AppendLine("using Terraria.ModLoader.Core;");
        sb.AppendLine();
        sb.AppendLine($"namespace {assemblyName}.Core;");
        sb.AppendLine();
        sb.AppendLine("internal static partial class __ModuleInitializerRunner_ToFixDumbBug");
        sb.AppendLine("{");
        sb.AppendLine("#pragma warning disable CA2255");
        sb.AppendLine("    [ModuleInitializer]");
        sb.AppendLine("    public static void EnsureDependencyModuleInitializersAreRun()");
        sb.AppendLine("    {");
        sb.AppendLine("        if (AssemblyLoadContext.GetLoadContext(typeof(__ModuleInitializerRunner_ToFixDumbBug).Assembly) is not { } alc)");
        sb.AppendLine("        {");
        sb.AppendLine($"            throw new InvalidOperationException(\"Failed to load mod '{assemblyName}'; could not resolve owning AssemblyLoadContext!\");");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        var type = alc.GetType();");
        sb.AppendLine("        var assembliesField = type.GetField(\"assemblies\", BindingFlags.Public | BindingFlags.Instance);");
        sb.AppendLine("        if (assembliesField is null || assembliesField.GetValue(alc) is not Dictionary<string, Assembly> assemblies)");
        sb.AppendLine("        {");
        sb.AppendLine($"            throw new InvalidOperationException($\"Failed to load mod '{assemblyName}'; could not resolve 'assemblies' field from ALC: {{alc.GetType()}}!\");");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        foreach (var assembly in assemblies.Values)");
        sb.AppendLine("        {");
        sb.AppendLine("            foreach (var module in assembly.GetModules())");
        sb.AppendLine("            {");
        sb.AppendLine("                RuntimeHelpers.RunModuleConstructor(module.ModuleHandle);");
        sb.AppendLine("            }");
        sb.AppendLine("        }");
        sb.AppendLine("    }");
        sb.AppendLine("#pragma warning restore CA2255");
        sb.AppendLine("}");

        return sb.ToString();
    }
}

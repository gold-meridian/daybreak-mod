using System;
using System.Collections.Generic;
using System.IO;

using Terraria.ModLoader;

using ModuleDefinition = Mono.Cecil.ModuleDefinition;

namespace DaybreakHookGenerator;

public sealed class TypeHookDefinition(Type type)
{
    public Type Type { get; } = type;

    public List<string> Exclusions { get; } = [];

    public List<TypeHookDefinition> SuperTypes { get; } = [];

    public TypeHookDefinition WithExclusions(params string[] exclusions)
    {
        Exclusions.AddRange(exclusions);
        return this;
    }

    public TypeHookDefinition WithSuperTypes(params TypeHookDefinition[] superTypes)
    {
        SuperTypes.AddRange(superTypes);
        return this;
    }
}

internal static class Program
{
    private const string the_namespace = "Daybreak.Common.Features.Hooks";

    public static void Main()
    {
        // Run this from the repository root.
        var path = Path.Combine("src", "Daybreak", "Common", "Features", "Hooks", "_TML");

        var globalBlockTypeDef = new TypeHookDefinition(typeof(GlobalBlockType));

        var definitions = new[]
        {
            new TypeHookDefinition(typeof(GlobalBossBar)),
            new TypeHookDefinition(typeof(GlobalBuff)),
            new TypeHookDefinition(typeof(GlobalEmoteBubble)),
            new TypeHookDefinition(typeof(GlobalInfoDisplay)),
            new TypeHookDefinition(typeof(GlobalItem))
               .WithExclusions(
                    nameof(GlobalItem.NetSend),
                    nameof(GlobalItem.NetReceive),
                    nameof(GlobalItem.SaveData),
                    nameof(GlobalItem.LoadData)
                ),
            new TypeHookDefinition(typeof(GlobalNPC))
               .WithExclusions(
                    nameof(GlobalNPC.SendExtraAI),
                    nameof(GlobalNPC.ReceiveExtraAI),
                    nameof(GlobalNPC.SaveData),
                    nameof(GlobalNPC.LoadData)
                ),
            new TypeHookDefinition(typeof(GlobalProjectile))
               .WithExclusions(
                    nameof(GlobalProjectile.SendExtraAI),
                    nameof(GlobalProjectile.ReceiveExtraAI)
                ),
            new TypeHookDefinition(typeof(GlobalPylon)),
            new TypeHookDefinition(typeof(GlobalTile))
               .WithSuperTypes(
                    globalBlockTypeDef
                ),
            new TypeHookDefinition(typeof(GlobalWall))
               .WithSuperTypes(
                    globalBlockTypeDef
                ),
            new TypeHookDefinition(typeof(ModSystem))
               .WithExclusions(
                    nameof(ModSystem.NetSend),
                    nameof(ModSystem.NetReceive),
                    nameof(ModSystem.SaveWorldData),
                    nameof(ModSystem.LoadWorldData),
                    nameof(ModSystem.SaveWorldHeader)
                ),
            new TypeHookDefinition(typeof(ModPlayer))
               .WithExclusions(
                    nameof(ModPlayer.NewInstance),
                    nameof(ModPlayer.SyncPlayer),
                    nameof(ModPlayer.CopyClientState),
                    nameof(ModPlayer.SendClientChanges),
                    nameof(ModPlayer.SaveData),
                    nameof(ModPlayer.LoadData)
                ),
        };

        var modDef = ModuleDefinition.ReadModule(typeof(ModLoader).Assembly.Location);

        foreach (var definition in definitions)
        {
            GenerateHookDefinition(path, definition, modDef);
        }
    }

    private static void GenerateHookDefinition(string path, TypeHookDefinition definition, ModuleDefinition modDef)
    {
        Console.WriteLine("GENERATING HOOK DEFINITION FOR " + definition.Type.Name);

        var className = definition.Type.Name + "Hooks";
        var fileName = Path.Combine(path, className + ".cs");

        Console.WriteLine($"    {className} @ {fileName}");

        var typeDef = modDef.GetType(definition.Type.FullName);
        var generator = new Generator(modDef, typeDef);
        var contents = generator.BuildType(the_namespace, className, definition);

        File.WriteAllText(fileName, contents);
    }
}
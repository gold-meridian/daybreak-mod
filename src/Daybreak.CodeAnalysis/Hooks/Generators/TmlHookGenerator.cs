using Microsoft.CodeAnalysis;

namespace Daybreak.CodeAnalysis.Hooks.Generators;

[Generator]
public sealed class TmlHookGenerator : IIncrementalGenerator
{
    private const string target_namespace = "Daybreak.Hooks";

    void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
    {
        var compilation = context.CompilationProvider;
        
        context.RegisterSourceOutput(
            compilation,
            static (ctx, compilation) =>
            {
                if (!compilation.TryGetHookAttributes(out _))
                {
                    return;
                }
                
                var definitions = BuildDefinitions(); foreach (var def in definitions)
                {
                    var typeSymbol = compilation.GetTypeByMetadataName(def.TypeMetadataName);
                    if (typeSymbol is null)
                    {
                        continue;
                    }

                    var gen = new RoslynGenerator(compilation, typeSymbol);
                    var result = gen.BuildType(target_namespace, def.TypeName + "Hooks", def);

                    ctx.AddSource(def.TypeName + "Hooks.g.cs", result);
                }
            }
        );
    }

    private static TypeHookDefinition[] BuildDefinitions()
    {
        var globalBlockTypeDef = new TypeHookDefinition("Terraria.ModLoader.GlobalBlockType");

        return
        [
            new TypeHookDefinition("Terraria.ModLoader.GlobalBossBar"),
            new TypeHookDefinition("Terraria.ModLoader.GlobalBuff"),
            new TypeHookDefinition("Terraria.ModLoader.GlobalEmoteBubble"),
            new TypeHookDefinition("Terraria.ModLoader.GlobalInfoDisplay"),

            new TypeHookDefinition("Terraria.ModLoader.GlobalItem")
               .WithExclusions(
                    "NetSend",
                    "NetReceive",
                    "SaveData",
                    ".LoadData"
                ),

            new TypeHookDefinition("Terraria.ModLoader.GlobalNPC")
               .WithExclusions(
                    "SendExtraAI",
                    "ReceiveExtraAI",
                    "SaveData",
                    "LoadData"
                ),

            new TypeHookDefinition("Terraria.ModLoader.GlobalProjectile")
               .WithExclusions(
                    "SendExtraAI",
                    "ReceiveExtraAI"
                ),

            new TypeHookDefinition("Terraria.ModLoader.GlobalPylon"),

            new TypeHookDefinition("Terraria.ModLoader.GlobalTile")
               .WithSuperTypes(globalBlockTypeDef),

            new TypeHookDefinition("Terraria.ModLoader.GlobalWall")
               .WithSuperTypes(globalBlockTypeDef),

            new TypeHookDefinition("Terraria.ModLoader.ModSystem")
               .WithExclusions(
                    "NetSend",
                    "NetReceive",
                    "SaveWorldData",
                    "LoadWorldData",
                    "SaveWorldHeader"
                ),

            new TypeHookDefinition("Terraria.ModLoader.ModPlayer")
               .WithExclusions(
                    "NewInstance",
                    "SyncPlayer",
                    "CopyClientState",
                    "SendClientChanges",
                    "SaveData",
                    "LoadData"
                ),
        ];
    }
}

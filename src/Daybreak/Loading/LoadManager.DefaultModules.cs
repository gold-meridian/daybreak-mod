using Terraria.ModLoader.Core;

namespace Daybreak.Loading;

partial class LoadManager
{
    private static readonly string[] default_modules = ["Daybreak.Resources"];

    private static void LoadDefaultModules()
    {
        var daybreakAlc = AssemblyManager.loadedModContexts["Daybreak"];

        foreach (var module in default_modules)
        {
            // We get to benefit from even loading the PDB, which tModLoader
            // doesn't bother with for some reason?
            daybreakAlc.LoadAssembly(
                daybreakAlc.modFile.GetBytes($"lib/{module}.dll"),
                daybreakAlc.modFile.GetBytes($"lib/{module}.pdb")
            );
        }
    }
}

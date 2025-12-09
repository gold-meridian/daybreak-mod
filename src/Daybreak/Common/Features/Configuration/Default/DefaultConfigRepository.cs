using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Configuration;

internal sealed class DefaultConfigRepository : ConfigRepository
{
    public override Mod Mod => ModContent.GetInstance<ModImpl>();

    public override string Name => "Settings";

    private static string ConfigsDirectory => Path.Combine(Main.SavePath, "daybreak", "configs");

    public override void SerializeCategories(params ConfigCategoryHandle[] categories)
    {
        if (categories.Length == 0)
        {
            return;
        }

        var dir = ConfigsDirectory;
        {
            Directory.CreateDirectory(dir);
        }

        foreach (var categoryHandle in categories)
        {
            Debug.Assert(categoryHandle.Repository == this);

            if (!TryGetCategory(categoryHandle, out var category))
            {
                Debug.Fail($"Category was not found: repo=({FullName}) category=({categoryHandle})");
                continue;
            }

            var fileName = $"{LanguageHelpers.GetModName(categoryHandle.Mod)}_{categoryHandle.Name}.json";
            File.WriteAllText(
                Path.Combine(dir, fileName),
                ConfigSerialization.SerializeCategory(
                    this,
                    category,
                    ConfigSerialization.Mode.File,
                    Entries.Values.Where(x => x.MainCategory == categoryHandle)
                )
            );
        }

        // TODO: Put this in the UI when we make it.
        // Main.Configuration.Save();
        // Also remember to handle dirtied ModConfigs...
    }

    public override void SynchronizeEntries(params ConfigEntryHandle[] entries)
    {
        if (entries.Length == 0)
        {
            return;
        }

        var entryTokens = new Dictionary<IConfigEntry, JToken>();
        foreach (var entryHandle in entries)
        {
            Debug.Assert(entryHandle.Repository == this);

            if (!TryGetEntry(entryHandle, out var entry))
            {
                Debug.Fail($"Entry was not found: repo=({FullName}) entry=({entryHandle})");
                continue;
            }

            if (entry.Side != ConfigSide.Both)
            {
                continue;
            }

            entryTokens[entry] = ConfigSerialization.SerializeEntry(entry, ConfigSerialization.Mode.Network);
        }
    }
}

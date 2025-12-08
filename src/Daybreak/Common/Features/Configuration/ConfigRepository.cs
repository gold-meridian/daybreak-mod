using System;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     A config repository manages ownership of config entries and categories.
/// </summary>
public abstract class ConfigRepository
{
    protected readonly record struct ModKey(Mod? Mod);

    public static ConfigRepository Default { get; } = new DefaultConfigRepository();

    protected Dictionary<ModKey, Dictionary<string, ConfigCategory>> CategoriesByMod { get; } = [];

    protected Dictionary<ConfigCategoryHandle, Dictionary<string, IConfigEntry>> EntriesByCategory { get; } = [];

    public ConfigCategoryHandle GetCategoryHandle(Mod? mod, string uniqueKey)
    {
        return new ConfigCategoryHandle(this, mod, uniqueKey);
    }

    public ConfigEntryHandle GetEntryHandle(Mod? mod, string uniqueKey)
    {
        return new ConfigEntryHandle(this, mod, uniqueKey);
    }

    public ConfigCategoryHandle RegisterCategory(
        Mod? mod,
        string uniqueKey,
        LocalizedText? displayName = null
    )
    {
        var categories = GetCategories(mod);
        if (categories.ContainsKey(uniqueKey))
        {
            throw new InvalidOperationException($"Cannot create category \"{uniqueKey}\" for mod \"{GetModName(mod)}\" because a category of the same name already exists!");
        }

        var handle = GetCategoryHandle(mod, uniqueKey);
        {
            var category = categories[uniqueKey] = new ConfigCategory(handle, displayName);
            _ = category.DisplayName;
        }
        return handle;
    }

    protected Dictionary<string, ConfigCategory> GetCategories(Mod? mod)
    {
        if (CategoriesByMod.TryGetValue(new ModKey(mod), out var categories))
        {
            return categories;
        }

        return CategoriesByMod[new ModKey(mod)] = [];
    }

    protected Dictionary<string, IConfigEntry> GetEntries(ConfigCategoryHandle categoryHandle)
    {
        if (EntriesByCategory.TryGetValue(categoryHandle, out var entries))
        {
            return entries;
        }

        return EntriesByCategory[categoryHandle] = [];
    }

    public static string GetModName(Mod? mod)
    {
        return mod is null ? "Terraria" : mod.Name;
    }
}

internal sealed class DefaultConfigRepository : ConfigRepository { }

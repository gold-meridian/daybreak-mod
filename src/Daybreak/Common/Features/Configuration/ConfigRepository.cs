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

    public ConfigCategory RegisterCategory(ConfigCategory category)
    {
        var categories = GetCategories(category.Handle.Mod);
        if (categories.ContainsKey(category.Handle.Name))
        {
            throw new InvalidOperationException($"Cannot create category \"{category.Handle.Name}\" for mod \"{LanguageHelpers.GetModName(category.Handle.Mod)}\" because a category of the same name already exists!");
        }

        _ = category.DisplayName;

        return category;
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
}

internal sealed class DefaultConfigRepository : ConfigRepository { }

using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     Handles creating and managing various aspects of the DAYBREAK config
///     system, a versatile alternative to <see cref="ModConfig"/>.
/// </summary>
public static class ConfigSystem
{
    private readonly record struct ModKey(Mod? Mod);

    private static Dictionary<ModKey, Dictionary<string, ConfigCategoryHandle>> categories_by_mod = [];
    private static Dictionary<ModKey, Dictionary<string, IConfigEntry>> entries_by_mod = [];

#region Category handles
    /// <summary>
    ///     Creates a handle to a category, regardless of whether that category
    ///     exists.
    /// </summary>
    public static ConfigCategoryHandle GetCategoryHandle(Mod? mod, string uniqueKey)
    {
        return new ConfigCategoryHandle(mod, uniqueKey);
    }
#endregion

#region Entry handles
    /// <summary>
    ///     Creates a handle to an entry, regardless of whether that entry
    ///     exists.
    /// </summary>
    public static ConfigEntryHandle GetEntryHandle(Mod? mod, string uniqueKey)
    {
        return new ConfigEntryHandle(mod, uniqueKey);
    }
#endregion
    
    private static Dictionary<TKey, TValue> GetModItems<TKey, TValue>(
        Mod? mod,
        Dictionary<ModKey, Dictionary<TKey, TValue>> map
    ) where TKey : notnull
    {
        if (map.TryGetValue(new ModKey(mod), out var items))
        {
            return items;
        }

        return map[new ModKey(mod)] = items = [];
    }
}

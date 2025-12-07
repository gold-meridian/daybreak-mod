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
    private static Dictionary<Mod, Dictionary<string, ConfigCategoryIdentity>> categories_by_mod = [];
}

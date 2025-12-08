using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common;

internal static class LanguageHelpers
{
    public static LocalizedText GetLocalization(
        Mod? mod,
        string category,
        string suffix,
        Func<string>? makeDefaultValue = null
    )
    {
        return Language.GetOrRegister($"Mods.{GetModName(mod)}.{category}.{suffix}", makeDefaultValue);
    }

    public static string GetModName(Mod? mod)
    {
        return mod is null ? "Terraria" : mod.Name;
    }
}

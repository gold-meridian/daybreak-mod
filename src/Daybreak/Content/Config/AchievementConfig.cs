using System.ComponentModel;

using Daybreak.Common.Features.Achievements;

using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Daybreak.Content.Config;

internal sealed class AchievementConfig : ModConfig
{
    public enum AchievementOverride
    {
        Default,
        AlwaysEnable,
        AlwaysDisable,
    }

    public override ConfigScope Mode => ConfigScope.ClientSide;

    public bool AreAchievementsPresent => AchievementImpl.ACHIEVEMENTS.Count > VanillaAchievements.VANILLA_ACHIEVEMENTS_BY_NAME.Keys.Count;

    [DrawTicks]
    [ReloadRequired]
    [DefaultValue(AchievementOverride.Default)]
    public AchievementOverride UseAchievements { get; set; } = AchievementOverride.Default;

    public static bool AreAchievementsEnabled()
    {
        return ModContent.GetInstance<AchievementConfig>().UseAchievements switch
        {
            AchievementOverride.AlwaysEnable => true,
            AchievementOverride.AlwaysDisable => false,
            _ => ModContent.GetInstance<AchievementConfig>().AreAchievementsPresent,
        };
    }
}
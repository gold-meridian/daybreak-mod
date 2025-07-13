using Terraria.Localization;

namespace Daybreak.Core;

// ReSharper disable MemberHidesStaticFromOuterClass
internal static class LocalizationReferences
{
    public static class Mods
    {
        public const string KEY = "Mods";

        public static LocalizedText GetChildText(string childKey)
        {
            return Language.GetText(KEY + '.' + childKey);
        }

        public static string GetChildTextValue(string childKey, params object?[] values)
        {
            return Language.GetTextValue(KEY + '.' + childKey, values);
        }

        public static class Daybreak
        {
            public const string KEY = "Mods.Daybreak";

            public static LocalizedText GetChildText(string childKey)
            {
                return Language.GetText(KEY + '.' + childKey);
            }

            public static string GetChildTextValue(string childKey, params object?[] values)
            {
                return Language.GetTextValue(KEY + '.' + childKey, values);
            }

            public static class UI
            {
                public const string KEY = "Mods.Daybreak.UI";

                public static LocalizedText GetChildText(string childKey)
                {
                    return Language.GetText(KEY + '.' + childKey);
                }

                public static string GetChildTextValue(string childKey, params object?[] values)
                {
                    return Language.GetTextValue(KEY + '.' + childKey, values);
                }

                public static class ModIcon
                {
                    public const string KEY = "Mods.Daybreak.UI.ModIcon";

                    public static LocalizedText GetChildText(string childKey)
                    {
                        return Language.GetText(KEY + '.' + childKey);
                    }

                    public static string GetChildTextValue(string childKey, params object?[] values)
                    {
                        return Language.GetTextValue(KEY + '.' + childKey, values);
                    }

                    public static class ModName
                    {
                        public const string KEY = "Mods.Daybreak.UI.ModIcon.ModName";
                        public const int ARG_COUNT = 0;

                        public static LocalizedText GetText()
                        {
                            return Language.GetText(KEY);
                        }

                        public static string GetTextValue()
                        {
                            return Language.GetTextValue(KEY);
                        }

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }
                    }
                }
            }

            public static class Configs
            {
                public const string KEY = "Mods.Daybreak.Configs";

                public static LocalizedText GetChildText(string childKey)
                {
                    return Language.GetText(KEY + '.' + childKey);
                }

                public static string GetChildTextValue(string childKey, params object?[] values)
                {
                    return Language.GetTextValue(KEY + '.' + childKey, values);
                }

                public static class AchievementConfig
                {
                    public const string KEY = "Mods.Daybreak.Configs.AchievementConfig";

                    public static LocalizedText GetChildText(string childKey)
                    {
                        return Language.GetText(KEY + '.' + childKey);
                    }

                    public static string GetChildTextValue(string childKey, params object?[] values)
                    {
                        return Language.GetTextValue(KEY + '.' + childKey, values);
                    }

                    public static class DisplayName
                    {
                        public const string KEY = "Mods.Daybreak.Configs.AchievementConfig.DisplayName";
                        public const int ARG_COUNT = 0;

                        public static LocalizedText GetText()
                        {
                            return Language.GetText(KEY);
                        }

                        public static string GetTextValue()
                        {
                            return Language.GetTextValue(KEY);
                        }

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }
                    }

                    public static class AreAchievementsPresent
                    {
                        public const string KEY = "Mods.Daybreak.Configs.AchievementConfig.AreAchievementsPresent";

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }

                        public static class Label
                        {
                            public const string KEY = "Mods.Daybreak.Configs.AchievementConfig.AreAchievementsPresent.Label";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }

                        public static class Tooltip
                        {
                            public const string KEY = "Mods.Daybreak.Configs.AchievementConfig.AreAchievementsPresent.Tooltip";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }
                    }

                    public static class UseAchievements
                    {
                        public const string KEY = "Mods.Daybreak.Configs.AchievementConfig.UseAchievements";

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }

                        public static class Label
                        {
                            public const string KEY = "Mods.Daybreak.Configs.AchievementConfig.UseAchievements.Label";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }

                        public static class Tooltip
                        {
                            public const string KEY = "Mods.Daybreak.Configs.AchievementConfig.UseAchievements.Tooltip";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }
                    }
                }

                public static class AchievementOverride
                {
                    public const string KEY = "Mods.Daybreak.Configs.AchievementOverride";

                    public static LocalizedText GetChildText(string childKey)
                    {
                        return Language.GetText(KEY + '.' + childKey);
                    }

                    public static string GetChildTextValue(string childKey, params object?[] values)
                    {
                        return Language.GetTextValue(KEY + '.' + childKey, values);
                    }

                    public static class Tooltip
                    {
                        public const string KEY = "Mods.Daybreak.Configs.AchievementOverride.Tooltip";
                        public const int ARG_COUNT = 0;

                        public static LocalizedText GetText()
                        {
                            return Language.GetText(KEY);
                        }

                        public static string GetTextValue()
                        {
                            return Language.GetTextValue(KEY);
                        }

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }
                    }

                    public static class Default
                    {
                        public const string KEY = "Mods.Daybreak.Configs.AchievementOverride.Default";

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }

                        public static class Label
                        {
                            public const string KEY = "Mods.Daybreak.Configs.AchievementOverride.Default.Label";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }
                    }

                    public static class AlwaysEnable
                    {
                        public const string KEY = "Mods.Daybreak.Configs.AchievementOverride.AlwaysEnable";

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }

                        public static class Label
                        {
                            public const string KEY = "Mods.Daybreak.Configs.AchievementOverride.AlwaysEnable.Label";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }
                    }

                    public static class AlwaysDisable
                    {
                        public const string KEY = "Mods.Daybreak.Configs.AchievementOverride.AlwaysDisable";

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }

                        public static class Label
                        {
                            public const string KEY = "Mods.Daybreak.Configs.AchievementOverride.AlwaysDisable.Label";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }
                    }
                }

                public static class LiquidConfig
                {
                    public const string KEY = "Mods.Daybreak.Configs.LiquidConfig";

                    public static LocalizedText GetChildText(string childKey)
                    {
                        return Language.GetText(KEY + '.' + childKey);
                    }

                    public static string GetChildTextValue(string childKey, params object?[] values)
                    {
                        return Language.GetTextValue(KEY + '.' + childKey, values);
                    }

                    public static class DisplayName
                    {
                        public const string KEY = "Mods.Daybreak.Configs.LiquidConfig.DisplayName";
                        public const int ARG_COUNT = 0;

                        public static LocalizedText GetText()
                        {
                            return Language.GetText(KEY);
                        }

                        public static string GetTextValue()
                        {
                            return Language.GetTextValue(KEY);
                        }

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }
                    }

                    public static class FixLiquidSlopes
                    {
                        public const string KEY = "Mods.Daybreak.Configs.LiquidConfig.FixLiquidSlopes";

                        public static LocalizedText GetChildText(string childKey)
                        {
                            return Language.GetText(KEY + '.' + childKey);
                        }

                        public static string GetChildTextValue(string childKey, params object?[] values)
                        {
                            return Language.GetTextValue(KEY + '.' + childKey, values);
                        }

                        public static class Label
                        {
                            public const string KEY = "Mods.Daybreak.Configs.LiquidConfig.FixLiquidSlopes.Label";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }

                        public static class Tooltip
                        {
                            public const string KEY = "Mods.Daybreak.Configs.LiquidConfig.FixLiquidSlopes.Tooltip";
                            public const int ARG_COUNT = 0;

                            public static LocalizedText GetText()
                            {
                                return Language.GetText(KEY);
                            }

                            public static string GetTextValue()
                            {
                                return Language.GetTextValue(KEY);
                            }

                            public static LocalizedText GetChildText(string childKey)
                            {
                                return Language.GetText(KEY + '.' + childKey);
                            }

                            public static string GetChildTextValue(string childKey, params object?[] values)
                            {
                                return Language.GetTextValue(KEY + '.' + childKey, values);
                            }
                        }
                    }
                }
            }
        }
    }
}
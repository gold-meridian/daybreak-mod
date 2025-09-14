using Terraria.Localization;

namespace LiquidSlopesPatch.Core;

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

        public static class LiquidSlopesPatch
        {
            public const string KEY = "Mods.LiquidSlopesPatch";

            public static LocalizedText GetChildText(string childKey)
            {
                return Language.GetText(KEY + '.' + childKey);
            }

            public static string GetChildTextValue(string childKey, params object?[] values)
            {
                return Language.GetTextValue(KEY + '.' + childKey, values);
            }

            public static class Configs
            {
                public const string KEY = "Mods.LiquidSlopesPatch.Configs";

                public static LocalizedText GetChildText(string childKey)
                {
                    return Language.GetText(KEY + '.' + childKey);
                }

                public static string GetChildTextValue(string childKey, params object?[] values)
                {
                    return Language.GetTextValue(KEY + '.' + childKey, values);
                }

                public static class Config
                {
                    public const string KEY = "Mods.LiquidSlopesPatch.Configs.Config";

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
                        public const string KEY = "Mods.LiquidSlopesPatch.Configs.Config.DisplayName";
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

                    public static class Enabled
                    {
                        public const string KEY = "Mods.LiquidSlopesPatch.Configs.Config.Enabled";

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
                            public const string KEY = "Mods.LiquidSlopesPatch.Configs.Config.Enabled.Label";
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
                            public const string KEY = "Mods.LiquidSlopesPatch.Configs.Config.Enabled.Tooltip";
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
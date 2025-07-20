#pragma warning disable CS8981

namespace Daybreak.Core;

// ReSharper disable InconsistentNaming
internal static class AssetReferences
{
    public static class icon
    {
        public const string KEY = "Daybreak/icon";

        public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D> Asset => lazy.Value;

        private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(KEY));
    }

    public static class Assets
    {
        public static class Images
        {
            public static class DaybreakPanel
            {
                public const string KEY = "Daybreak/Assets/Images/DaybreakPanel";

                public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D> Asset => lazy.Value;

                private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(KEY));
            }

            public static class DaybreakSun
            {
                public const string KEY = "Daybreak/Assets/Images/DaybreakSun";

                public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D> Asset => lazy.Value;

                private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(KEY));
            }

            public static class DaybreakSunPulse
            {
                public const string KEY = "Daybreak/Assets/Images/DaybreakSunPulse";

                public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D> Asset => lazy.Value;

                private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(KEY));
            }
        }

        public static class Shaders
        {
            public static class UI
            {
                public static class ModPanelShader
                {
                    public sealed class Parameters : IShaderParameters
                    {
                        public Microsoft.Xna.Framework.Graphics.Texture2D? uImage0 { get; set; }

                        public float uTime { get; set; }

                        public Microsoft.Xna.Framework.Vector4 uSource { get; set; }

                        public float uHoverIntensity { get; set; }

                        public float uPixel { get; set; }

                        public float uColorResolution { get; set; }

                        public float uGrayness { get; set; }

                        public Microsoft.Xna.Framework.Vector3 uInColor { get; set; }

                        public float uSpeed { get; set; }

                        public void Apply(Microsoft.Xna.Framework.Graphics.EffectParameterCollection parameters)
                        {
                            parameters["uImage0"]?.SetValue(uImage0);
                            parameters["uTime"]?.SetValue(Terraria.Main.GlobalTimeWrappedHourly);
                            parameters["uSource"]?.SetValue(uSource);
                            parameters["uHoverIntensity"]?.SetValue(uHoverIntensity);
                            parameters["uPixel"]?.SetValue(uPixel);
                            parameters["uColorResolution"]?.SetValue(uColorResolution);
                            parameters["uGrayness"]?.SetValue(uGrayness);
                            parameters["uInColor"]?.SetValue(uInColor);
                            parameters["uSpeed"]?.SetValue(uSpeed);
                        }
                    }

                    public const string KEY = "Daybreak/Assets/Shaders/UI/ModPanelShader";

                    public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect> Asset => lazy.Value;

                    private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Effect>(KEY, ReLogic.Content.AssetRequestMode.ImmediateLoad));

                    public static WrapperShaderData<Parameters> CreatePanelShader()
                    {
                        return new WrapperShaderData<Parameters>(Asset, "PanelShader");
                    }
                }

                public static class ModPanelShaderNew
                {
                    public sealed class Parameters : IShaderParameters
                    {
                        public Microsoft.Xna.Framework.Graphics.Texture2D? uImage0 { get; set; }

                        public Microsoft.Xna.Framework.Graphics.Texture2D? uTexture { get; set; }

                        public Microsoft.Xna.Framework.Graphics.Texture2D? tex0 { get; set; }

                        public float uTime { get; set; }

                        public Microsoft.Xna.Framework.Vector4 uSource { get; set; }

                        public float uHoverIntensity { get; set; }

                        public float uPixel { get; set; }

                        public float uColorResolution { get; set; }

                        public float uGrayness { get; set; }

                        public Microsoft.Xna.Framework.Vector4 uColor { get; set; }

                        public Microsoft.Xna.Framework.Vector4 uSecondaryColor { get; set; }

                        public float uSpeed { get; set; }

                        public void Apply(Microsoft.Xna.Framework.Graphics.EffectParameterCollection parameters)
                        {
                            parameters["uImage0"]?.SetValue(uImage0);
                            parameters["uTexture"]?.SetValue(uTexture);
                            parameters["tex0"]?.SetValue(tex0);
                            parameters["uTime"]?.SetValue(Terraria.Main.GlobalTimeWrappedHourly);
                            parameters["uSource"]?.SetValue(uSource);
                            parameters["uHoverIntensity"]?.SetValue(uHoverIntensity);
                            parameters["uPixel"]?.SetValue(uPixel);
                            parameters["uColorResolution"]?.SetValue(uColorResolution);
                            parameters["uGrayness"]?.SetValue(uGrayness);
                            parameters["uColor"]?.SetValue(uColor);
                            parameters["uSecondaryColor"]?.SetValue(uSecondaryColor);
                            parameters["uSpeed"]?.SetValue(uSpeed);
                        }
                    }

                    public const string KEY = "Daybreak/Assets/Shaders/UI/ModPanelShaderNew";

                    public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect> Asset => lazy.Value;

                    private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Effect>(KEY, ReLogic.Content.AssetRequestMode.ImmediateLoad));

                    public static WrapperShaderData<Parameters> CreatePanelShader()
                    {
                        return new WrapperShaderData<Parameters>(Asset, "PanelShader");
                    }
                }

                public static class ModPanelShaderSampler
                {
                    public sealed class Parameters : IShaderParameters
                    {
                        public Microsoft.Xna.Framework.Graphics.Texture2D? uImage0 { get; set; }

                        public Microsoft.Xna.Framework.Graphics.Texture2D? uImage1 { get; set; }

                        public float uTime { get; set; }

                        public Microsoft.Xna.Framework.Vector4 uSource { get; set; }

                        public void Apply(Microsoft.Xna.Framework.Graphics.EffectParameterCollection parameters)
                        {
                            parameters["uImage0"]?.SetValue(uImage0);
                            parameters["uImage1"]?.SetValue(uImage1);
                            parameters["uTime"]?.SetValue(Terraria.Main.GlobalTimeWrappedHourly);
                            parameters["uSource"]?.SetValue(uSource);
                        }
                    }

                    public const string KEY = "Daybreak/Assets/Shaders/UI/ModPanelShaderSampler";

                    public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect> Asset => lazy.Value;

                    private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Effect>(KEY, ReLogic.Content.AssetRequestMode.ImmediateLoad));

                    public static WrapperShaderData<Parameters> CreatePanelShader()
                    {
                        return new WrapperShaderData<Parameters>(Asset, "PanelShader");
                    }
                }

                public static class PowerfulSunIcon
                {
                    public sealed class Parameters : IShaderParameters
                    {
                        public Microsoft.Xna.Framework.Graphics.Texture2D? uImage0 { get; set; }

                        public float uTime { get; set; }

                        public float uHoverIntensity { get; set; }

                        public float uPixel { get; set; }

                        public float uColorResolution { get; set; }

                        public float uGrayness { get; set; }

                        public float uSpeed { get; set; }

                        public Microsoft.Xna.Framework.Vector4 uSource { get; set; }

                        public Microsoft.Xna.Framework.Vector3 uInColor { get; set; }

                        public void Apply(Microsoft.Xna.Framework.Graphics.EffectParameterCollection parameters)
                        {
                            parameters["uImage0"]?.SetValue(uImage0);
                            parameters["uTime"]?.SetValue(Terraria.Main.GlobalTimeWrappedHourly);
                            parameters["uHoverIntensity"]?.SetValue(uHoverIntensity);
                            parameters["uPixel"]?.SetValue(uPixel);
                            parameters["uColorResolution"]?.SetValue(uColorResolution);
                            parameters["uGrayness"]?.SetValue(uGrayness);
                            parameters["uSpeed"]?.SetValue(uSpeed);
                            parameters["uSource"]?.SetValue(uSource);
                            parameters["uInColor"]?.SetValue(uInColor);
                        }
                    }

                    public const string KEY = "Daybreak/Assets/Shaders/UI/PowerfulSunIcon";

                    public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect> Asset => lazy.Value;

                    private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Effect>(KEY, ReLogic.Content.AssetRequestMode.ImmediateLoad));

                    public static WrapperShaderData<Parameters> CreatePanelShader()
                    {
                        return new WrapperShaderData<Parameters>(Asset, "PanelShader");
                    }
                }
            }
        }
    }
}
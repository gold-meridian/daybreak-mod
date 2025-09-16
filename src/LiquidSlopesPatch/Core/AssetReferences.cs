#pragma warning disable CS8981

namespace LiquidSlopesPatch.Core;

// ReSharper disable InconsistentNaming
internal static class AssetReferences
{
    public static class icon
    {
        public const string KEY = "LiquidSlopesPatch/icon";

        public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D> Asset => lazy.Value;

        private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(KEY));
    }

    public static class Assets
    {
        public static class Images
        {
            public static class DefaultTileLiquidMask
            {
                public const string KEY = "LiquidSlopesPatch/Assets/Images/DefaultTileLiquidMask";

                public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D> Asset => lazy.Value;

                private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Texture2D>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Texture2D>(KEY));
            }
        }

        public static class Shaders
        {
            public static class LiquidMask
            {
                public sealed class Parameters : IShaderParameters
                {
                    public Microsoft.Xna.Framework.Graphics.Texture2D? uImage0 { get; set; }

                    public void Apply(Microsoft.Xna.Framework.Graphics.EffectParameterCollection parameters)
                    {
                        parameters["uImage0"]?.SetValue(uImage0);
                    }
                }

                public const string KEY = "LiquidSlopesPatch/Assets/Shaders/LiquidMask";

                public static ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect> Asset => lazy.Value;

                private static readonly System.Lazy<ReLogic.Content.Asset<Microsoft.Xna.Framework.Graphics.Effect>> lazy = new(() => Terraria.ModLoader.ModContent.Request<Microsoft.Xna.Framework.Graphics.Effect>(KEY, ReLogic.Content.AssetRequestMode.ImmediateLoad));

                public static WrapperShaderData<Parameters> CreateMaskShader()
                {
                    return new WrapperShaderData<Parameters>(Asset, "MaskShader");
                }
            }
        }
    }
}
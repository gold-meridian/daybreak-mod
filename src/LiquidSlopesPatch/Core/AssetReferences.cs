using System;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

#pragma warning disable CS8981

namespace LiquidSlopesPatch.Core;

// ReSharper disable InconsistentNaming
internal static class AssetReferences
{
    public static class icon
    {
        public const string KEY = "LiquidSlopesPatch/icon";

        private static readonly Lazy<Asset<Texture2D>> lazy = new(() => ModContent.Request<Texture2D>(KEY));

        public static Asset<Texture2D> Asset => lazy.Value;
    }

    public static class Assets
    {
        public static class Images
        {
            public static class DefaultTileLiquidMask
            {
                public const string KEY = "LiquidSlopesPatch/Assets/Images/DefaultTileLiquidMask";

                private static readonly Lazy<Asset<Texture2D>> lazy = new(() => ModContent.Request<Texture2D>(KEY));

                public static Asset<Texture2D> Asset => lazy.Value;
            }
        }

        public static class Shaders
        {
            public static class LiquidMask
            {
                public const string KEY = "LiquidSlopesPatch/Assets/Shaders/LiquidMask";

                private static readonly Lazy<Asset<Effect>> lazy = new(() => ModContent.Request<Effect>(KEY, AssetRequestMode.ImmediateLoad));

                public static Asset<Effect> Asset => lazy.Value;

                public static WrapperShaderData<Parameters> CreateMaskShader()
                {
                    return new WrapperShaderData<Parameters>(Asset, "MaskShader");
                }

                public sealed class Parameters : IShaderParameters
                {
                    public Texture2D? uImage0 { get; set; }

                    public void Apply(EffectParameterCollection parameters)
                    {
                        parameters["uImage0"]?.SetValue(uImage0);
                    }
                }
            }
        }
    }
}

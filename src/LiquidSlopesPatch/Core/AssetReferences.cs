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
}
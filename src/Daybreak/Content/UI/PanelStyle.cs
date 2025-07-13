using System;
using System.Diagnostics;
using System.Text;

using Daybreak.Common.Features.ModPanel;
using Daybreak.Common.Rendering;
using Daybreak.Core;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI.Chat;

namespace Daybreak.Content.UI;

internal sealed class PanelStyle : ModPanelStyleExt
{
    public sealed class ModName : UIText
    {
        private readonly string originalText;

        public ModName(string text, float textScale = 1, bool large = false) : base(text, textScale, large)
        {
            if (ChatManager.Regexes.Format.Matches(text).Count != 0)
            {
                throw new InvalidOperationException("The text cannot contain formatting.");
            }

            originalText = text;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var formattedText = GetPulsatingText(originalText, Main.GlobalTimeWrappedHourly);
            SetText(formattedText);

            base.DrawSelf(spriteBatch);
        }

        private static string GetPulsatingText(string text, float time)
        {
            var lightPurple = color_1;
            var darkPurple = color_2;

            const float speed = 3f;
            const float offset = 0.3f;

            // [c/______:x]
            const int character_length = 12;

            var sb = new StringBuilder(character_length * text.Length);
            for (var i = 0; i < text.Length; i++)
            {
                var wave = MathF.Sin(time * speed + i * offset);

                // Factor normalized 0-1.
                var color = Color.Lerp(lightPurple, darkPurple, (wave + 1f) / 2f);

                sb.Append($"[c/{color.Hex3()}:{text[i]}]");
            }
            return sb.ToString();
        }
    }

    private sealed class ModIcon() : UIImage(TextureAssets.MagicPixel)
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.End(out var ss);
            spriteBatch.Begin(
                SpriteSortMode.Immediate,
                BlendState.Additive,
                SamplerState.PointClamp,
                DepthStencilState.None,
                ss.RasterizerState,
                null,
                Main.UIScaleMatrix
            );

            // const int offset = (80 - 46) / 2;
            var dims = GetDimensions().ToRectangle();

            Debug.Assert(whenDayBreaksShaderData is not null);
            whenDayBreaksShaderData.Parameters.uGrayness = 1f;
            whenDayBreaksShaderData.Parameters.uInColor = new Vector3(1f, 0f, 1f);
            whenDayBreaksShaderData.Parameters.uSpeed = 0.2f;
            whenDayBreaksShaderData.Parameters.uSource = Transform(new Vector4(dims.Width, dims.Height - 2f, dims.X, dims.Y));
            whenDayBreaksShaderData.Parameters.uPixel = 2f;
            whenDayBreaksShaderData.Parameters.uColorResolution = 10f;
            whenDayBreaksShaderData.Apply();

            spriteBatch.Draw(
                TextureAssets.MagicPixel.Value,
                dims.TopLeft(),
                dims,
                Color.Red,
                0f,
                Vector2.Zero,
                1f,
                SpriteEffects.None,
                0f
            );

            spriteBatch.Restart(ss);
        }
    }

    private static readonly Color color_1 = new(255, 147, 0);
    private static readonly Color color_2 = new(255, 182, 55);

    private static WrapperShaderData<Assets.Shaders.UI.ModPanelShader.Parameters>? panelShaderData;
    private static WrapperShaderData<Assets.Shaders.UI.ModPanelShaderSampler.Parameters>? panelShaderDataSampler;
    private static WrapperShaderData<Assets.Shaders.UI.PowerfulSunIcon.Parameters>? whenDayBreaksShaderData;

    private static RenderTarget2D? panelTargetToBeUpscaled;

    private static float hoverIntensity;

    public override void Load()
    {
        base.Load();

        panelShaderData = Assets.Shaders.UI.ModPanelShader.CreatePanelShader();
        panelShaderDataSampler = Assets.Shaders.UI.ModPanelShaderSampler.CreatePanelShader();
        whenDayBreaksShaderData = Assets.Shaders.UI.PowerfulSunIcon.CreatePanelShader();

        Main.RunOnMainThread(() =>
            {
                Main.graphics.GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
                Main.graphics.ApplyChanges();
            }
        );
    }

    public override bool PreInitialize(UIModItem element)
    {
        element.BorderColor = new Color(25, 5, 5);

        return base.PreInitialize(element);
    }

    public override UIImage ModifyModIcon(UIModItem element, UIImage modIcon, ref int modIconAdjust)
    {
        return new ModIcon
        {
            Left = modIcon.Left,
            Top = modIcon.Top,
            Width = modIcon.Width,
            Height = modIcon.Height,
        };
    }

    public override UIText ModifyModName(UIModItem element, UIText modName)
    {
        var name = Mods.Daybreak.UI.ModIcon.ModName.GetTextValue();
        return new ModName(name + $" v{element._mod.Version}")
        {
            Left = modName.Left,
            Top = modName.Top,
        };
    }

    public override bool PreSetHoverColors(UIModItem element, bool hovered)
    {
        element.BorderColor = new Color(25, 5, 5);

        return false;
    }

    public override bool PreDrawPanel(UIModItem element, SpriteBatch sb, ref bool drawDivider)
    {
        drawDivider = false;
        
        if (element._needsTextureLoading)
        {
            element._needsTextureLoading = false;
            element.LoadTextures();
        }

        // Render our cool custom panel with a shader.
        {
            sb.End(out var ss);

            var dims = element.GetDimensions();

            var targetWidth = (int)(dims.Width / 2f);
            var targetHeight = (int)((dims.Height - 2f) / 2f);

            if (panelTargetToBeUpscaled is null || panelTargetToBeUpscaled.Width != targetWidth || panelTargetToBeUpscaled.Height != targetHeight)
            {
                panelTargetToBeUpscaled?.Dispose();
                panelTargetToBeUpscaled = new RenderTarget2D(
                    Main.instance.GraphicsDevice,
                    targetWidth,
                    targetHeight,
                    false,
                    SurfaceFormat.Color,
                    DepthFormat.None,
                    0,
                    RenderTargetUsage.PreserveContents
                );
            }

            var oldRts = Main.instance.GraphicsDevice.GetRenderTargets();
            var scissor = Main.instance.GraphicsDevice.ScissorRectangle;
            foreach (var oldRt in oldRts)
            {
                if (oldRt.RenderTarget is RenderTarget2D rt)
                {
                    rt.RenderTargetUsage = RenderTargetUsage.PreserveContents;
                }
            }

            Main.instance.GraphicsDevice.SetRenderTarget(panelTargetToBeUpscaled);
            Main.instance.GraphicsDevice.Clear(Color.Transparent);
            Main.instance.GraphicsDevice.ScissorRectangle = new Rectangle(0, 0, Main.graphics.PreferredBackBufferWidth, Main.graphics.PreferredBackBufferHeight);

            sb.Begin(
                SpriteSortMode.Immediate,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                DepthStencilState.None,
                ss.RasterizerState,
                null,
                Main.UIScaleMatrix
            );
            {
                hoverIntensity += (element.IsMouseHovering ? 1f : -1f) / 15f;
                hoverIntensity = Math.Clamp(hoverIntensity, 0f, 1f);

                Debug.Assert(panelShaderData is not null);
                panelShaderData.Parameters.uGrayness = 1f;
                panelShaderData.Parameters.uInColor = new Vector3(1f, 0f, 1f);
                panelShaderData.Parameters.uSpeed = 0.2f;
                panelShaderData.Parameters.uSource = new Vector4(targetWidth, targetHeight, 0, 0);
                panelShaderData.Parameters.uHoverIntensity = hoverIntensity;
                panelShaderData.Parameters.uPixel = 1f;
                panelShaderData.Parameters.uColorResolution = 10f;
                panelShaderData.Apply();

                sb.Draw(
                    TextureAssets.MagicPixel.Value,
                    new Rectangle(0, 0, targetWidth, targetHeight),
                    Color.White
                );
            }
            sb.End();

            Main.instance.GraphicsDevice.SetRenderTargets(oldRts);
            Main.instance.GraphicsDevice.ScissorRectangle = scissor;

            sb.Begin(
                SpriteSortMode.Immediate,
                BlendState.NonPremultiplied,
                SamplerState.PointClamp,
                DepthStencilState.None,
                ss.RasterizerState,
                null,
                Main.UIScaleMatrix
            );
            {
                Debug.Assert(panelShaderDataSampler is not null);
                panelShaderDataSampler.Parameters.uSource = Transform(new Vector4(dims.Width, dims.Height, dims.X, dims.Y));

                Main.instance.GraphicsDevice.Textures[1] = panelTargetToBeUpscaled;
                Main.instance.GraphicsDevice.SamplerStates[1] = SamplerState.PointClamp;

                panelShaderDataSampler.Apply();
                Debug.Assert(element._backgroundTexture is not null);
                element.DrawPanel(sb, element._backgroundTexture.Value, element.BackgroundColor);
            }
            sb.Restart(in ss);
        }

        Debug.Assert(element._borderTexture is not null);
        element.DrawPanel(sb, element._borderTexture.Value, element.BorderColor);

        return false;
    }

    public override Color ModifyEnabledTextColor(bool enabled, Color color)
    {
        return enabled ? color_2 : color_1;
    }

    private static Vector4 Transform(Vector4 vector)
    {
        var vec1 = Vector2.Transform(new Vector2(vector.X, vector.Y), Main.UIScaleMatrix);
        var vec2 = Vector2.Transform(new Vector2(vector.Z, vector.W), Main.UIScaleMatrix);
        return new Vector4(vec1, vec2.X, vec2.Y);
    }
}
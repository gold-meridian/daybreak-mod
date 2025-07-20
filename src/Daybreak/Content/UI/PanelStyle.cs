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

        public static string GetPulsatingText(string text, float time)
        {
            var lightOrange = color_1 with { A = 200 };
            var darkOrange = color_2 with { A = 200 };

            const float speed = 3f;
            const float offset = 0.3f;

            // [c/______:x]
            const int character_length = 12;

            var sb = new StringBuilder(character_length * text.Length);
            for (var i = 0; i < text.Length; i++)
            {
                var wave = MathF.Sin(time * speed + i * offset);

                // Factor normalized 0-1.
                var color = Color.Lerp(lightOrange, darkOrange, (wave + 1f) / 2f);

                sb.Append($"[c/{color.Hex3()}:{text[i]}]");
            }
            return sb.ToString();
        }
    }

	private sealed class ModIcon() : UIImage(TextureAssets.MagicPixel)
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            bool aprilFools = DateTime.Now.Month == 4 && DateTime.Now.Day == 1;

            var dims = GetDimensions().ToRectangle();

            Texture2D texture = aprilFools ?
                TextureAssets.Sun2.Value :
                Assets.Images.DaybreakSun.Asset.Value;
            Texture2D pulseTexture = Assets.Images.DaybreakSunPulse.Asset.Value;

			float scale = 1f + hoverIntensity * 0.1f + MathF.Sin(Main.GlobalTimeWrappedHourly / 4f) * 0.1f;
            Vector2 center = dims.Center() + new Vector2(0, MathF.Sin(Main.GlobalTimeWrappedHourly) * 2);
            float rotation = MathF.Sin(Main.GlobalTimeWrappedHourly / 2) * 0.15f;

			spriteBatch.Draw(
				texture,
				center,
				texture.Frame(),
				Color.Orange,
				rotation,
				texture.Size() / 2,
				scale,
				SpriteEffects.None,
				0f
			);

			spriteBatch.Draw(
				texture,
                center,
				texture.Frame(),
                Color.White with { A = 200 },
				rotation,
				texture.Size() / 2,
                scale,
                SpriteEffects.None,
                0f
            );

			float pulseTime = (Main.GlobalTimeWrappedHourly / 3f) % 1f;
			float upScale = scale * (0.2f + pulseTime * 0.8f);
			float colorFade = 0.8f * MathF.Sin(pulseTime * MathHelper.Pi);
			spriteBatch.Draw(
				pulseTexture,
				center,
				pulseTexture.Frame(),
				Color.DarkOrange with { A = 0 } * colorFade,
				rotation,
				pulseTexture.Size() / 2,
				upScale,
				SpriteEffects.None,
				0f
			);

            if (aprilFools)
                return;

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

			Debug.Assert(whenDayBreaksShaderData is not null);
			whenDayBreaksShaderData.Parameters.uSpeed = 0.3f;
			whenDayBreaksShaderData.Parameters.uPixel = 2f;
			whenDayBreaksShaderData.Parameters.uColorResolution = 10f;
            whenDayBreaksShaderData.Parameters.uSource = new Vector4(0, 0, texture.Width, texture.Height);
            whenDayBreaksShaderData.Parameters.uInColor = new Vector3(1f, 0.1f, 0f);
			whenDayBreaksShaderData.Apply();

			spriteBatch.Draw(
				texture,
				center,
				texture.Frame(),
				Color.Orange with { A = 128 },
				rotation,
				texture.Size() / 2,
				scale,
				SpriteEffects.None,
				0f
			);

			spriteBatch.Restart(ss);
		}
	}

    private static readonly Color color_1 = new(255, 147, 0);
    private static readonly Color color_2 = new(255, 182, 55);

    private static WrapperShaderData<Assets.Shaders.UI.ModPanelShaderNew.Parameters>? panelShaderData;
    private static WrapperShaderData<Assets.Shaders.UI.PowerfulSunIcon.Parameters>? whenDayBreaksShaderData;

    private static float hoverIntensity;

    public override void Load()
    {
        base.Load();

        panelShaderData = Assets.Shaders.UI.ModPanelShaderNew.CreatePanelShader();
        whenDayBreaksShaderData = Assets.Shaders.UI.PowerfulSunIcon.CreatePanelShader();
    }

    /*public override bool PreInitialize(UIModItem element)
    {
        element.BorderColor = new Color(25, 5, 5);

        return base.PreInitialize(element);
    }*/

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

    /*public override bool PreSetHoverColors(UIModItem element, bool hovered)
    {
        element.BorderColor = new Color(25, 5, 5);

        return false;
    }*/

    public override bool PreDrawPanel(UIModItem element, SpriteBatch sb, ref bool drawDivider)
    {
        if (element._needsTextureLoading)
        {
            element._needsTextureLoading = false;
            element.LoadTextures();
        }

        // Render our cool custom panel with a shader.
        {
            sb.End(out var ss);

            var dims = element.GetDimensions();
			
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
                Debug.Assert(panelShaderData is not null);

                hoverIntensity = MathHelper.Lerp(hoverIntensity, element.IsMouseHovering ? 1f : 0f, 0.2f);
				hoverIntensity = Math.Clamp(MathF.Round(hoverIntensity, 2), 0f, 1f);

				panelShaderData.Parameters.uGrayness = 1f;
				panelShaderData.Parameters.uColor = new Vector4(1.3f, 0.7f, 0f, 1f);
				panelShaderData.Parameters.uSecondaryColor = new Vector4(0.7f, 0f, 0f, 1f);
				panelShaderData.Parameters.uSpeed = 0.3f;
				panelShaderData.Parameters.uSource = Transform(new Vector4(dims.Width, dims.Height, dims.X, dims.Y));
				panelShaderData.Parameters.uHoverIntensity = hoverIntensity;
				panelShaderData.Parameters.uPixel = 2f;
				panelShaderData.Parameters.uColorResolution = 10f;
				panelShaderData.Apply();

                Debug.Assert(element._backgroundTexture is not null);
                element.DrawPanel(sb, Assets.Images.DaybreakPanel.Asset.Value, new Color(83, 92, 170));
            }
            sb.Restart(in ss);
        }

        // Debug.Assert(element._borderTexture is not null);
        // element.DrawPanel(sb, element._borderTexture.Value, element.BorderColor);

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
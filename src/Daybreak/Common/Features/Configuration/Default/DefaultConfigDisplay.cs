using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Features.Models;
using Daybreak.Common.Rendering;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Configuration.Default;

internal static class DefaultConfigDisplay
{
    private sealed class Data : IStatic<Data>
    {
        public required WrapperShaderData<AssetReferences.Assets.Shaders.UI.PowerfulSunIcon.Parameters> SunShader { get; init; }

        public static Data LoadData(Mod mod)
        {
            return Main.RunOnMainThread(
                () => new Data
                {
                    SunShader = AssetReferences.Assets.Shaders.UI.PowerfulSunIcon.CreatePanelShader(),
                }
            ).GetAwaiter().GetResult();
        }

        public static void UnloadData(Data data) { }
    }

    // Hardcoding this is evil, but it's our only practical choice.
    private const int text_settings_menu_y_offset = 585;

    private static bool daybreakOverVanillaSettings;
    private static float hoverIntensity;

    [OnLoad]
    private static void ApplyHooks()
    {
        MonoModHooks.Add(
            typeof(MenuLoader).GetMethod(nameof(MenuLoader.UpdateAndDrawModMenu), BindingFlags.NonPublic | BindingFlags.Static)!,
            UpdateAndDrawModMenu_DrawDaybreakSettingsIcon
        );

        IL_Main.DrawMenu += DrawMenu_OpenDaybreakSettingsWhenRequestingVanillaMenu;
    }

    private static void UpdateAndDrawModMenu_DrawDaybreakSettingsIcon(
        Action<SpriteBatch, GameTime, Color, float, float> orig,
        SpriteBatch spriteBatch,
        GameTime gameTime,
        Color color,
        float logoRotation,
        float logoScale
    )
    {
        orig(spriteBatch, gameTime, color, logoRotation, logoScale);

        var center = new Vector2(Main.screenWidth / 2f, text_settings_menu_y_offset);
        var hitbox = new Rectangle((int)center.X - 15, (int)center.Y - 15, 30, 30);
        var isHovering = Main.menuMode == MenuID.Settings && hitbox.Contains(Main.MouseScreen.ToPoint());

        // Keep updating intensity regardless of whether we're on the menu so it
        // doesn't keep its size when exiting.
        hoverIntensity = MathHelper.Lerp(hoverIntensity, isHovering ? 1f : 0f, 0.2f);

        if (Main.menuMode != MenuID.Settings)
        {
            return;
        }

        if (isHovering)
        {
            Main.instance.MouseText(Mods.Daybreak.UI.SwitchToDaybreak.GetTextValue());

            if (Main.mouseLeft && Main.mouseLeftRelease)
            {
                daybreakOverVanillaSettings = true;
                ConfigRepository.Default.ShowInterface(
                    onExit:
                    () =>
                    {
                        Main.menuMode = MenuID.Title;
                    }
                );
            }
        }

        var texture = AssetReferences.Assets.Images.DaybreakSun.Asset.Value;
        var pulseTexture = AssetReferences.Assets.Images.DaybreakSunPulse.Asset.Value;

        var scale = 0.85f + hoverIntensity * 0.15f + MathF.Sin(Main.GlobalTimeWrappedHourly / 4f) * 0.1f;
        var rotation = MathF.Sin(Main.GlobalTimeWrappedHourly / 2) * 0.15f;

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

        var pulseTime = Main.GlobalTimeWrappedHourly / 3f % 1f;
        var upScale = scale * (0.2f + (pulseTime * 0.8f));
        var colorFade = 0.8f * MathF.Sin(pulseTime * MathHelper.Pi);
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

        var shaderData = IStatic<Data>.Instance.SunShader;
        shaderData.Parameters.uSpeed = 0.3f;
        shaderData.Parameters.uPixel = 2f;
        shaderData.Parameters.uColorResolution = 10f;
        shaderData.Parameters.uSource = new Vector4(0, 0, texture.Width, texture.Height);
        shaderData.Parameters.uInColor = new Vector3(1f, 0.1f, 0f);
        shaderData.Apply();

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

    private static void DrawMenu_OpenDaybreakSettingsWhenRequestingVanillaMenu(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(
            MoveType.After,
            x => x.MatchLdstr("UI.Workshop")
        );

        c.GotoNext(
            MoveType.After,
            x => x.MatchLdcI4(MenuID.Settings),
            x => x.MatchStsfld<Main>(nameof(Main.menuMode))
        );

        c.MoveBeforeLabels();

        c.EmitDelegate(
            () =>
            {
                if (daybreakOverVanillaSettings)
                {
                    ConfigRepository.Default.ShowInterface(
                        onExit:
                        () =>
                        {
                            Main.menuMode = MenuID.Title;
                        }
                    );
                }
            }
        );
    }
}

using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Features.Models;
using Daybreak.Common.Rendering;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MonoMod.Cil;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.GameContent;

namespace Daybreak.Common.Features.Configuration;

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

    private sealed class DaybreakSettingsIcon : UIElement
    {
        private float HoverIntensity { get; set; }

        public Action? OnExit { get; set; }

        public ConfigCategoryHandle? CategoryHandle { get; set; }

        public ConfigEntryHandle? EntryHandle { get; set; }

        public UIElement? ElementThatDecidesClicks { get; set; }

        public bool IsHoveringAtAll => IsMouseHovering || ContainsPoint(Main.MouseScreen);

        public bool IsHoveringButNotReally => !IsMouseHovering && ContainsPoint(Main.MouseScreen);

        public override void OnInitialize()
        {
            base.OnInitialize();

            ElementThatDecidesClicks?.OnLeftClick += ReceiveLeftClickFromParent;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsHoveringAtAll)
            {
                Main.instance.MouseText(Mods.Daybreak.UI.SwitchToDaybreak.GetTextValue());
            }

            var dimensions = GetDimensions();
            var center = dimensions.Center();

            HoverIntensity = MathHelper.Lerp(HoverIntensity, IsHoveringAtAll ? 1f : 0f, 0.2f);

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

        private void ReceiveLeftClickFromParent(UIMouseEvent evt, UIElement listeningElement)
        {
            if (IsHoveringButNotReally)
            {
                HandleClick();
            }
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            base.LeftClick(evt);

            HandleClick();
        }

        private void HandleClick()
        {
            ConfigRepository.Default.ShowInterface(
                categoryHandle: CategoryHandle,
                entryHandle: EntryHandle,
                onExit: OnExit
            );
        }
    }

    // Hardcoding this is evil, but it's our only practical choice.
    private const int text_settings_menu_y_offset = 585;

    private static bool daybreakOverVanillaSettings;
    private static bool daybreakOverModdedSettings;
    private static float hoverIntensity;

    [OnLoad]
    private static void ApplyHooks()
    {
        MonoModHooks.Add(
            typeof(MenuLoader).GetMethod(nameof(MenuLoader.UpdateAndDrawModMenu), BindingFlags.NonPublic | BindingFlags.Static)!,
            UpdateAndDrawModMenu_DrawDaybreakSettingsIcon
        );

        MonoModHooks.Modify(
            typeof(UIModItem).GetMethod(nameof(UIModItem.OnInitialize), BindingFlags.Public | BindingFlags.Instance)!,
            OnInitialize_AddOurConfigButton
        );

        MonoModHooks.Add(
            typeof(UIModItem).GetMethod(nameof(UIModItem.OpenConfig), BindingFlags.NonPublic | BindingFlags.Instance)!,
            OpenConfig_OpenOurConfig
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

    private static void OnInitialize_AddOurConfigButton(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.After, x => x.MatchCallvirt<IDictionary<Mod, List<ModConfig>>>(nameof(IDictionary<,>.ContainsKey)));
        c.EmitLdarg0();
        c.EmitDelegate(
            (bool show, UIModItem self) =>
            {
                if (show)
                {
                    return show;
                }

                if (!ModLoader.TryGetMod(self._mod.Name, out var mod))
                {
                    return false;
                }

                return ConfigRepository.Default.Categories.Any(x => x.Handle.Mod == mod);
            }
        );
    }

    private static void OpenConfig_OpenOurConfig(
        // Action<UIModItem, UIMouseEvent, UIElement> orig,
        UIModItem self,
        UIMouseEvent evt,
        UIElement listeningElement
    )
    {
        SoundEngine.PlaySound(SoundID.MenuOpen);

        if (!ModLoader.TryGetMod(self._mod.Name, out var mod))
        {
            return;
        }

        if (daybreakOverModdedSettings || !ConfigManager.Configs.TryGetValue(mod, out var modConfigs) || modConfigs.Count == 0)
        {
            ConfigRepository.Default.ShowInterface(
                categoryHandle: ConfigRepository.Default.Categories.FirstOrDefault(x => x.Handle.Mod == mod) ?? default(ConfigCategoryHandle),
                onExit: () =>
                {
                    Main.menuMode = Interface.modsMenuID;
                }
            );
        }
        else
        {
            Interface.modConfigList.ModToSelectOnOpen = mod;
            Main.menuMode = Interface.modConfigListID;
        }
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

    [OnLoad]
    private static void UpdateModsList()
    {
        MonoModHooks.Add(
            typeof(UIMods).GetMethod(nameof(UIMods.OnInitialize), BindingFlags.Public | BindingFlags.Instance)!,
            OnInitialize_AddDaybreakButtonToModsList
        );

        if (Interface.modsMenu is { } menu && menu.uIElement is { } element)
        {
            element.Append(MakeDaybreakButtonForModsList(Interface.modsMenu));
            menu.Recalculate();
        }
    }

    // Mostly in case some other mod ever attempts to reinitialize the UI state.
    private static void OnInitialize_AddDaybreakButtonToModsList(Action<UIMods> orig, UIMods self)
    {
        orig(self);

        self.uIElement.Append(MakeDaybreakButtonForModsList(self));
    }

    [OnUnload]
    private static void RevertModsList()
    {
        if (Interface.modsMenu is not { } menu)
        {
            return;
        }

        if (menu.Children?.FirstOrDefault(x => x is DaybreakSettingsIcon) is not { } element)
        {
            return;
        }

        menu.RemoveChild(element);
        menu.Recalculate();
    }

    private static DaybreakSettingsIcon MakeDaybreakButtonForModsList(UIMods parent)
    {
        var button = new DaybreakSettingsIcon();
        {
            button.CategoryHandle = null;
            button.EntryHandle = null;
            button.OnExit = () =>
            {
                SoundEngine.PlaySound(10);
                Main.MenuUI.SetState(Interface.modsMenu);
            };

            button.Width.Set(30f, 0f);
            button.Height.Set(30f, 0f);

            button.VAlign = 1f;
            button.HAlign = 1f;
            button.Top.Set(-30f - /* gap between buttons */ 2.5f - (button.Height.Pixels / 2f), 0f);
            button.Left.Set(button.Height.Pixels + 10f, 0f);

            button.ElementThatDecidesClicks = parent;
        }

        return button;
    }
}

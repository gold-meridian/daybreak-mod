using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Daybreak.Common.Features.Hooks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.ModPanel;

internal sealed class ModPanelRenderer
{
    // TODO : 
    // UIModItem:
    // Swap UIImage for Config/More info buttons (delayed)
    // swap all modifytext methods for one that uses an enum (if tomat says theres too many methods)
    // add ModPanelStyleExt methods

    // This exists instead of a regular IL edit/detour because, for some reason,
    // it would seem that some part of the drawing routine may get inlined such
    // that our changes are not properly reflected.  This, of course, only
    // happens if you start the game with DAYBREAK disabled, entire the Mods
    // List, and then enable DAYBREAK (since the JIT would have JITed the
    // UI-related code).  I cannot, for the life of me, figure out how or why
    // this is happening to non-concrete, virtual symbols.
    private sealed class ModItemWithCustomDrawing(LocalMod mod) : UIModItem(mod)
    {
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!ModLoader.TryGetMod(_mod.Name, out var mod) || !TryGetPanelStyle(mod, out var style))
            {
                base.Draw(spriteBatch);
                return;
            }

            using (style.OverrideTextures())
            {
                currentMod = mod;

                if (style.PreDraw(this, spriteBatch))
                {
                    base.Draw(spriteBatch);
                }

                style.PostDraw(this, spriteBatch);

                currentMod = null;
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            var ptr = typeof(UIPanel).GetMethod("DrawSelf", BindingFlags.NonPublic | BindingFlags.Instance)!.MethodHandle.GetFunctionPointer();
            var baseDrawSelf = (Action<SpriteBatch>)Activator.CreateInstance(typeof(Action<SpriteBatch>), this, ptr)!;
            var drawPanelDivider = true;
            if (!TryGetPanelStyle(currentMod, out var style))
            {
                baseDrawSelf(spriteBatch);
            }
            else
            {
                if (style.PreDrawPanel(this, spriteBatch, ref drawPanelDivider))
                {
                    baseDrawSelf(spriteBatch);
                }

                style.PostDrawPanel(this, spriteBatch);
            }

            var innerDimensions = GetInnerDimensions();
            var drawPos = new Vector2(innerDimensions.X + 5f + _modIconAdjust, innerDimensions.Y + 30f);
            if (drawPanelDivider)
            {
                spriteBatch.Draw(UICommon.DividerTexture.Value, drawPos, null, Color.White, 0f, Vector2.Zero, new Vector2((innerDimensions.Width - 10f - _modIconAdjust) / 8f, 1f), SpriteEffects.None, 0f);
            }

            drawPos = new Vector2(innerDimensions.X + 10f + _modIconAdjust, innerDimensions.Y + 45f);

            // TODO: These should just be UITexts
            if (_mod.properties.side != ModSide.Server && (_mod.Enabled != _loaded || _configChangesRequireReload))
            {
                drawPos += new Vector2(_uiModStateText.Width.Pixels + left2ndLine, 0f);

                var reloadText = _configChangesRequireReload ? Language.GetTextValue("tModLoader.ModReloadForced") : Language.GetTextValue("tModLoader.ModReloadRequired");
                if (style is not null)
                {
                    reloadText = style.ModifyReloadRequiredText(this, reloadText);
                    if (style.PreDrawReloadRequiredText(this))
                    {
                        Utils.DrawBorderString(spriteBatch, reloadText, drawPos, Color.White);
                    }

                    style.PostDrawReloadRequiredText(this);
                }
                else
                {
                    Utils.DrawBorderString(spriteBatch, reloadText, drawPos, Color.White);
                }
            }

            if (_mod.properties.side == ModSide.Server)
            {
                drawPos += new Vector2(90f, -2f);
                spriteBatch.Draw(UICommon.ModBrowserIconsTexture.Value, drawPos, new Rectangle(5 * 34, 3 * 34, 32, 32), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                if (new Rectangle((int)drawPos.X, (int)drawPos.Y, 32, 32).Contains(Main.MouseScreen.ToPoint()))
                {
                    UICommon.DrawHoverStringInBounds(spriteBatch, Language.GetTextValue("tModLoader.ModIsServerSide"));
                }
            }

            if (_moreInfoButton?.IsMouseHovering == true)
            {
                _tooltip = Language.GetTextValue("tModLoader.ModsMoreInfo");
            }
            else if (_deleteModButton?.IsMouseHovering == true)
            {
                _tooltip = Language.GetTextValue("UI.Delete");
            }
            else if (_modName?.IsMouseHovering == true && _mod?.properties.author.Length > 0)
            {
                _tooltip = Language.GetTextValue("tModLoader.ModsByline", _mod.properties.author);
            }
            else if (_uiModStateText?.IsMouseHovering == true)
            {
                _tooltip = ToggleModStateText;
            }
            else if (_configButton?.IsMouseHovering == true)
            {
                _tooltip = Language.GetTextValue("tModLoader.ModsOpenConfig");
            }
            else if (updatedModDot?.IsMouseHovering == true)
            {
                _tooltip = previousVersionHint == null ? Language.GetTextValue("tModLoader.ModAddedSinceLastLaunchMessage") : Language.GetTextValue("tModLoader.ModUpdatedSinceLastLaunchMessage", previousVersionHint);
            }
            else if (tMLUpdateRequired?.IsMouseHovering == true)
            {
                _tooltip = Language.GetTextValue("tModLoader.SwitchVersionInfoButton");
            }
            else if (_modReferenceIcon?.IsMouseHovering == true)
            {
                _tooltip = _modRequiresTooltip;
            }
            else if (_translationModIcon?.IsMouseHovering == true)
            {
                var refs = string.Join(", ", _mod!.properties.RefNames(true)); // Translation mods can be strong or weak references.
                _tooltip = Language.GetTextValue("tModLoader.TranslationModTooltip", refs);
            }

            if (style is not null)
            {
                _tooltip = style.ModifyHoverTooltip(this, _tooltip);
            }
        }
    }

    // The current mod whose panel style to use.
    private static Mod? currentMod;

    private static readonly Dictionary<Mod, List<ModPanelStyle>> panel_styles = [];

    public static void AddPanelStyle(Mod mod, ModPanelStyle style)
    {
        if (panel_styles.TryGetValue(mod, out var styles))
        {
            // TODO: We should support multiple styles.  Issue is handling
            //       reinitialization.
            ModContent.GetInstance<ModImpl>().Logger.Warn($"Mod \"${mod.Name}\" has already registered a ModPanelStyle and is attempting to add another one; only the first one loaded will be displayed.");
        }
        else
        {
            panel_styles[mod] = styles = [];
        }

        styles.Add(style);
    }

    public static bool TryGetPanelStyle(Mod? mod, [NotNullWhen(returnValue: true)] out ModPanelStyle? style)
    {
        if (mod is null)
        {
            style = null;
            return false;
        }

        if (panel_styles.TryGetValue(mod, out var styles))
        {
            // We should always have at least one style, but let's be safe.
            style = styles.FirstOrDefault();
            return style is not null;
        }

        style = null;
        return false;
    }

    [OnLoad(Side = ModSide.Client)]
    private static void Load()
    {
        if (!ConciseModListCompat())
        {
            MonoModHooks.Add(
                typeof(UIMods).GetMethod(nameof(UIMods.Populate), BindingFlags.NonPublic | BindingFlags.Instance)!,
                (UIMods self) =>
                {
                    self.modItemsTask = Task.Run(
                        () =>
                        {
                            return ModOrganizer.FindMods(logDuplicates: true).Select(mod => new ModItemWithCustomDrawing(mod)).Cast<UIModItem>().ToList();
                        }
                    );
                }
            );

            MonoModHooks.Add(
                GetMethod(nameof(UIModItem.OnInitialize)),
                OnInitialize_RunHooks
            );

            MonoModHooks.Modify(
                GetMethod(nameof(UIModItem.OnInitialize)),
                OnInitialize_ModifyFieldsBeingAppended
            );
        }

        MonoModHooks.Add(
            GetMethod(nameof(UIModItem.SetHoverColors)),
            SetHoverColors_RunHooks
        );

        MonoModHooks.Modify(
            typeof(UIModStateText).GetMethod(nameof(UIModStateText.DrawEnabledText), BindingFlags.NonPublic | BindingFlags.Instance),
            DrawEnabledText_RunHooks
        );

        MonoModHooks.Modify(
            typeof(UIModStateText).GetProperty("DisplayText", BindingFlags.NonPublic | BindingFlags.Instance)!.GetGetMethod(true),
            DisplayText_UIModStateText_RunHooks
        );

        MonoModHooks.Add(
            typeof(UIModStateText).GetMethod("DrawPanel", BindingFlags.NonPublic | BindingFlags.Instance)!,
            DrawPanel_UIModStateText_RunHooks
        );

        MonoModHooks.Add(
            typeof(UIModStateText).GetMethod("DrawEnabledText", BindingFlags.NonPublic | BindingFlags.Instance)!,
            DrawEnabledText_UIModStateText_RunHooks
        );

        // readjust dependency button position
        if (!ModLoader.HasMod("ConciseModList"))
        {
            MonoModHooks.Modify(
                GetMethod(nameof(UIModItem.UpdateUIForEnabledChange)),
                static il =>
                {
                    var c = new ILCursor(il);

                    c.GotoNext(MoveType.After, i => i.MatchCallvirt<UIModStateText>(nameof(UIModStateText.SetEnabled)));

                    c.EmitLdarg0();
                    c.EmitDelegate(
                        (UIModItem self) =>
                        {
                            self._modReferenceIcon?.Left.Set(self._uiModStateText.Left.Pixels + self._uiModStateText.Width.Pixels + 5, 0);
                        }
                    );

                    c.GotoNext(MoveType.After, i => i.MatchCallvirt<UIModStateText>(nameof(UIModStateText.SetDisabled)));

                    c.EmitLdarg0();
                    c.EmitDelegate(
                        (UIModItem self) =>
                        {
                            self._modReferenceIcon?.Left.Set(self._uiModStateText.Left.Pixels + self._uiModStateText.Width.Pixels + 5, 0);
                        }
                    );
                }
            );
        }

        return;

        static MethodInfo GetMethod(string name)
        {
            return typeof(UIModItem).GetMethod(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance)!;
        }

        static bool ConciseModListCompat()
        {
            if (!ModLoader.TryGetMod("ConciseModList", out var conciseModList))
            {
                return false;
            }

            var asm = conciseModList.Code;

            var type = asm.GetType("ConciseModList.ConciseUIModItem");
            if (type is null)
            {
                return false;
            }

            MonoModHooks.Add(
                type.GetMethod(nameof(UIModItem.OnInitialize), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance),
                OnInitialize_RunHooks
            );

            MonoModHooks.Add(
                type.GetMethod("DrawSelf", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance),
                Draw
            );
            MonoModHooks.Add(
                type.GetMethod("ManageDrawing", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance),
                OverrideRegularPanelDrawing
            );

            MonoModHooks.Modify(
                type.GetMethod(nameof(UIModItem.OnInitialize), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance),
                il =>
                {
                    var c = new ILCursor(il);

                    c.GotoNext(MoveType.After, x => x.MatchStfld<UIModItem>(nameof(UIModItem._modIconAdjust)));

                    c.EmitLdarg0();
                    c.EmitDelegate(
                        (UIModItem self) =>
                        {
                            if (self._mod.modFile.HasFile("icon.png"))
                            {
                                try
                                {
                                    using (self._mod.modFile.Open())
                                    using (var s = self._mod.modFile.GetStream("icon.png"))
                                    {
                                        var iconTexture = Main.Assets.CreateUntracked<Texture2D>(s, ".png").Value;

                                        if (iconTexture.Width != 80 || iconTexture.Height != 80)
                                        {
                                            return;
                                        }

                                        self._modIcon.ImageScale = 1f;
                                        self._modIcon.Left.Pixels = -1;
                                        self._modIcon.Top.Pixels = -1;
                                        self._modIcon.SetImage(iconTexture);
                                    }
                                }
                                catch (Exception e)
                                {
                                    Logging.tML.Error("Unknown error", e);
                                }
                            }

                            self._modIcon = !TryGetPanelStyle(currentMod, out var style)
                                ? self._modIcon
                                : style.ModifyModIcon(self, self._modIcon, ref self._modIconAdjust);
                        }
                    );

                    c.GotoNext(MoveType.Before, x => x.MatchCall("ConciseModList.ConciseUIModItem", "SetIconImage"));
                    c.Remove();
                    c.EmitPop();
                }
            );

            return true;
        }
    }

    [OnUnload(Side = ModSide.Client)]
    private static void Unload()
    {
        currentMod = null;
        panel_styles.Clear();
    }

    private static void OnInitialize_RunHooks(Action<UIModItem> orig, UIModItem self)
    {
        if (!ModLoader.TryGetMod(self._mod.Name, out currentMod) || !TryGetPanelStyle(currentMod, out var style))
        {
            orig(self);
            return;
        }

        using (style.OverrideTextures())
        {
            if (style.PreInitialize(self))
            {
                orig(self);
            }

            style.PostInitialize(self);
        }

        currentMod = null;
    }

    private static void OnInitialize_ModifyFieldsBeingAppended(ILContext il)
    {
        var c = new ILCursor(il);

        // Modify the mod icon.
        {
            c.GotoNext(MoveType.Before, x => x.MatchStfld<UIModItem>(nameof(UIModItem._modIcon)));
            c.EmitLdarg0();
            c.EmitDelegate(
                (UIImage originalImage, UIModItem self) => !TryGetPanelStyle(currentMod, out var style)
                    ? originalImage
                    : style.ModifyModIcon(self, originalImage, ref self._modIconAdjust)
            );

            c.GotoNext(MoveType.After, x => x.MatchStfld<UIModItem>(nameof(UIModItem._modIcon)));

            // Support not appending the icon by returning null.
            var skipAppend = c.DefineLabel();
            c.EmitLdarg0();
            c.EmitLdfld(typeof(UIModItem).GetField(nameof(UIModItem._modIcon), BindingFlags.NonPublic | BindingFlags.Instance)!);
            c.EmitLdcI4(0); // null
            c.EmitBeq(skipAppend);

            c.GotoNext(MoveType.After, x => x.MatchCall<UIElement>(nameof(UIElement.Append)));
            c.MarkLabel(skipAppend);
        }

        // Modify the mod name.
        {
            c.GotoNext(MoveType.Before, x => x.MatchStfld<UIModItem>(nameof(UIModItem._modName)));
            c.EmitLdarg0();
            c.EmitDelegate(
                (UIText originalText, UIModItem self) => !TryGetPanelStyle(currentMod, out var style)
                    ? originalText
                    : style.ModifyModName(self, originalText)
            );
        }

        // Modify the info buttons.
        {
            // Move into the actual block that handles this.
            c.GotoNext(MoveType.After, x => x.MatchStfld<UIModItem>(nameof(UIModItem._loaded)));
            var pos = c.Index;

            // Do this manually since currentMod is null if the mod doesn't
            // provide a style.
            var modInfoLoc = -1;
            c.GotoPrev(x => x.MatchLdloc(out modInfoLoc));
            Debug.Assert(modInfoLoc != -1);

            c.Index = pos;
            c.EmitLdarg0();
            c.EmitLdloc(modInfoLoc);
            c.EmitDelegate(
                (UIModItem self, Mod mod) =>
                {
                    var infos = TryGetPanelStyle(mod, out var style)
                        ? style.GetInfos(mod).ToArray()
                        : ModPanelStyle.GetDefaultInfos(mod).ToArray();

                    var xOffset = -40;
                    foreach (var info in infos)
                    {
                        var image = info.InfoImage;
                        {
                            image.Left = StyleDimension.FromPixelsAndPercent(xOffset, 1f);
                        }

                        self.Append(image);
                        xOffset -= 18;
                    }
                }
            );

            // Actually jump out now to avoid running the original logic.
            var label = c.DefineLabel();
            c.EmitBr(label);

            c.GotoNext(MoveType.After, x => x.MatchBlt(out _));
            c.MarkLabel(label);
        }
    }

    private static void DrawEnabledText_UIModStateText_RunHooks(Action<UIModStateText, SpriteBatch> orig, UIModStateText self, SpriteBatch sb)
    {
        var modName = ((UIModItem)self.Parent)._mod.Name;

        if (!ModLoader.TryGetMod(modName, out var mod) || !TryGetPanelStyle(mod, out var style))
        {
            orig(self, sb);
            return;
        }

        if (style.PreDrawModStateText(self, self._enabled))
        {
            orig(self, sb);
        }

        style.PostDrawModStateText(self, self._enabled);
    }

    private static void DrawPanel_UIModStateText_RunHooks(Action<UIModStateText, SpriteBatch> orig, UIModStateText self, SpriteBatch sb)
    {
        var modName = ((UIModItem)self.Parent)._mod.Name;

        if (!ModLoader.TryGetMod(modName, out var mod) || !TryGetPanelStyle(mod, out var style))
        {
            orig(self, sb);
            return;
        }

        if (style.PreDrawModStateTextPanel(self, self._enabled))
        {
            orig(self, sb);
        }

        style.PostDrawModStateTextPanel(self, self._enabled);
    }

    // TODO: Don't remember if we can use currentMod here, but I'd rather
    // minimize its usage in cases where we actually can determine the context.
    private static void SetHoverColors_RunHooks(
        Action<UIModItem, bool> orig,
        UIModItem self,
        bool hovered
    )
    {
        if (!ModLoader.TryGetMod(self._mod.Name, out var mod) || !TryGetPanelStyle(mod, out var style))
        {
            orig(self, hovered);
            return;
        }

        if (style.PreSetHoverColors(self, hovered))
        {
            orig(self, hovered);
        }

        style.PostSetHoverColors(self, hovered);
    }

    private static void Draw(
        Action<UIModItem, SpriteBatch> orig,
        UIModItem self,
        SpriteBatch spriteBatch
    )
    {
        if (!ModLoader.TryGetMod(self._mod.Name, out var mod) || !TryGetPanelStyle(mod, out var style))
        {
            orig(self, spriteBatch);
            return;
        }

        using (style.OverrideTextures())
        {
            currentMod = mod;

            if (style.PreDraw(self, spriteBatch))
            {
                orig(self, spriteBatch);
            }

            style.PostDraw(self, spriteBatch);

            currentMod = null;
        }
    }

    private static void OverrideRegularPanelDrawing(
        Action<UIPanel, SpriteBatch> orig,
        UIPanel self,
        SpriteBatch spriteBatch
    )
    {
        if (!TryGetPanelStyle(currentMod, out var style) || self is not UIModItem uiModItem)
        {
            orig(self, spriteBatch);
            return;
        }

        var drawDivider = false;
        if (style.PreDrawPanel(uiModItem, spriteBatch, ref drawDivider))
        {
            orig(self, spriteBatch);
        }

        style.PostDrawPanel(uiModItem, spriteBatch);
    }

    private static void DrawEnabledText_RunHooks(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.After, x => x.MatchCall<UIModStateText>("get_DisplayColor"));

        c.EmitLdarg0(); // this
        c.EmitDelegate(
            static (Color displayColor, UIModStateText self) =>
            {
                if (!TryGetPanelStyle(currentMod, out var style))
                {
                    return displayColor;
                }

                return style.ModifyEnabledTextColor(self._enabled, displayColor);
            }
        );
    }

    private static void DisplayText_UIModStateText_RunHooks(ILContext il)
    {
        var c = new ILCursor(il);

        c.EmitLdarg0();
        c.EmitDelegate(
            static (UIModStateText self) =>
            {
                var text = self._enabled ? Language.GetTextValue("GameUI.Enabled") : Language.GetTextValue("GameUI.Disabled");
                if (self.Parent is null)
                {
                    return text;
                }

                var modName = ((UIModItem)self.Parent)._mod.Name;
                if (!ModLoader.TryGetMod(modName, out var mod) || !TryGetPanelStyle(mod, out var style))
                {
                    return text;
                }

                text = style.ModifyEnabledText((UIPanel)self.Parent, text, self._enabled);
                return text;
            }
        );

        c.EmitRet();
    }
}

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.ModPanel;

/// <summary>
///     Facilitates the customization of <see cref="UIModItem"/>s through
///     various hooks.
/// </summary>
/// <remarks>
///     This style may be applied to any mod, technically, so references to your
///     mod instance should be explicit and not assumed.
///     <br />
///     If you are using an assembly publicizer, you may instead extend
///     <see cref="ModPanelStyleExt"/>, which lets you directly interface with
///     the <see cref="UIModItem"/> instead of the generic <see cref="UIPanel"/>
///     instance.
/// </remarks>
[PublicAPI]
[Autoload(Side = ModSide.Client)]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithInheritors)]
public abstract class ModPanelStyle : ModType
{
    private readonly struct TextureOverrider : IDisposable
    {
        private readonly Dictionary<TextureKind, Asset<Texture2D>> originals = [];

        public TextureOverrider(Dictionary<TextureKind, Asset<Texture2D>> overrides)
        {
            foreach (var (kind, @override) in overrides)
            {
                originals[kind] = Get(kind);
                Set(kind, @override);
            }
        }

        public void Dispose()
        {
            foreach (var (kind, original) in originals)
            {
                Set(kind, original);
            }
        }

        private static Asset<Texture2D> Get(TextureKind kind)
        {
            switch (kind)
            {
                case TextureKind.ModInfo:
                    return UICommon.ButtonModInfoTexture;

                case TextureKind.ModConfig:
                    return UICommon.ButtonModConfigTexture;

                case TextureKind.Deps:
                    return UICommon.ButtonDepsTexture;

                case TextureKind.TranslationMod:
                    return UICommon.ButtonTranslationModTexture;

                case TextureKind.Error:
                    return UICommon.ButtonErrorTexture;

                case TextureKind.InnerPanel:
                    return UICommon.InnerPanelTexture;

                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }

        private static void Set(TextureKind kind, Asset<Texture2D> asset)
        {
            switch (kind)
            {
                case TextureKind.ModInfo:
                    UICommon.ButtonModInfoTexture = asset;
                    break;

                case TextureKind.ModConfig:
                    UICommon.ButtonModConfigTexture = asset;
                    break;

                case TextureKind.Deps:
                    UICommon.ButtonDepsTexture = asset;
                    break;

                case TextureKind.TranslationMod:
                    UICommon.ButtonTranslationModTexture = asset;
                    break;

                case TextureKind.Error:
                    UICommon.ButtonErrorTexture = asset;
                    break;

                case TextureKind.InnerPanel:
                    UICommon.InnerPanelTexture = asset;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
        }
    }

    /// <summary>
    ///     An "information" item for the panel, such as the amount of items the
    ///     mod adds.
    /// </summary>
    public readonly record struct PanelInfo(UIHoverImage InfoImage);

    /// <summary>
    ///     The 'texture kind', denoting known textures that can be overridden.
    /// </summary>
    public enum TextureKind
    {
        /// <summary>
        ///     The 'Mod Info' button.
        /// </summary>
        ModInfo,

        /// <summary>
        ///     The 'Mod Config' button.
        /// </summary>
        ModConfig,

        /// <summary>
        ///     The 'Deps' icon, shown to display dependencies and dependents.
        /// </summary>
        Deps,

        /// <summary>
        ///     The 'Translation Mod' icon, shown to display that this mod
        ///     implements translations for other mods.
        /// </summary>
        TranslationMod,

        /// <summary>
        ///     The 'Error' icon, shown to display errors (namely unloading
        ///     issues) to mod developers.
        /// </summary>
        Error,

        /// <summary>
        ///     The inner panel used to render the 'Enabled'/'Disabled' text.
        /// </summary>
        InnerPanel,
    }

    /// <summary>
    ///     Supplying this dictionary with values for the
    ///     <see cref="TextureKind"/> keys will allow you to override the
    ///     default textures used by tModLoader with your own.
    /// </summary>
    public virtual Dictionary<TextureKind, Asset<Texture2D>> TextureOverrides { get; } = [];

    /// <summary>
    ///     Registers this style as known to the implementation.
    /// </summary>
    protected sealed override void Register()
    {
        CustomModPanelImpl.AddPanelStyle(Mod, this);
    }

    /// <summary>
    ///     Unused; this type is a singleton.
    /// </summary>
    protected sealed override void InitTemplateInstance() { }

    // I guess if someone was really crazy, they could do all the initialization
    // themselves.
    /// <summary>
    ///     Invoked before <see cref="UIModItem.OnInitialize"/> is called.
    /// </summary>
    /// <returns>
    ///     <see langword="false"/> to cancel regular initialization behavior,
    ///     <see langword="true"/> to enable regular execution.
    /// </returns>
    public virtual bool PreInitialize(UIPanel element)
    {
        return true;
    }

    /// <summary>
    ///     Invoked after <see cref="UIModItem.OnInitialize"/> is called
    ///     regardless of what <see cref="PreInitialize"/> returns.
    /// </summary>
    public virtual void PostInitialize(UIPanel element) { }

    /// <summary>
    ///     If <see cref="PreInitialize"/> returns <see langword="true"/>, this
    ///     method is invoked to modify the mod icon during initialization.
    ///     <br />
    ///     To remove the icon and shift relevant elements to the left, return
    ///     <see langword="null"/>
    /// </summary>
    public virtual UIImage? ModifyModIcon(UIPanel element, UIImage modIcon, ref int modIconAdjust)
    {
        return modIcon;
    }

    /// <summary>
    ///     If <see cref="PreInitialize"/> returns <see langword="true"/>, this
    ///     method is invoked to modify the mod name during initialization.
    /// </summary>
    public virtual UIText ModifyModName(UIPanel element, UIText modName)
    {
        return modName;
    }

    /// <summary>
    ///     Lets you modify the value of the "Enabled/Disabled" text.
    /// </summary>
    public virtual string ModifyEnabledText(UIPanel element, string text, bool enabled)
    {
        return text;
    }

    /// <summary>
    ///     Lets you modify the value of the "Reload Required" text.
    /// </summary>
    public virtual string ModifyReloadRequiredText(UIPanel element, string text)
    {
        return text;
    }

    /// <summary>
    ///     Invoked before the "Reload Required" text is drawn, return
    ///     <see langword="false"/> to stop it from drawing.
    /// </summary>
    public virtual bool PreDrawReloadRequiredText(UIPanel element)
    {
        return true;
    }

    /// <summary>
    ///     Invoked after the "Reload Required" text is drawn, regardless of
    ///     what <see cref="PreDrawReloadRequiredText(UIPanel)"/> returns.
    /// </summary>
    public virtual void PostDrawReloadRequiredText(UIPanel element) { }

    /// <summary>
    ///     Invoked before hover colors are set.
    /// </summary>
    public virtual bool PreSetHoverColors(UIPanel element, bool hovered)
    {
        return true;
    }

    /// <summary>
    ///     Allows you to modify the text drawn on the "mouse-over" hover panel.
    /// </summary>
    public virtual string ModifyHoverTooltip(UIPanel element, string tooltip)
    {
        return tooltip;
    }

    /// <summary>
    ///     Invoked after hover colors are set.
    /// </summary>
    public virtual void PostSetHoverColors(UIPanel element, bool hovered) { }

    /// <summary>
    ///     Invoked before the element is drawn.
    /// </summary>
    public virtual bool PreDraw(UIPanel element, SpriteBatch sb)
    {
        return true;
    }

    /// <summary>
    ///     Invoked after the element is drawn.
    /// </summary>
    public virtual void PostDraw(UIPanel element, SpriteBatch sb) { }

    /// <summary>
    ///     Invoked specifically before the panel is drawn, assuming
    ///     <see cref="PreDraw"/> returned <see langword="true"/>.
    /// </summary>
    public virtual bool PreDrawPanel(UIPanel element, SpriteBatch sb, ref bool drawDivider)
    {
        return true;
    }

    /// <summary>
    ///     Invoked before the "Enabled/Disabled" text is drawn, 
    ///     return <see langword="false"/> to stop it from drawing.
    /// </summary>
    public virtual bool PreDrawModStateText(UIElement self, bool enabled)
    {
        return true;
    }

    /// <summary>
    ///     Invoked after the
    ///     <see cref="UIModStateText.DrawEnabledText(SpriteBatch)"/> is called,
    ///     regardless of what
    ///     <see cref="PreDrawModStateText(UIElement, bool)"/> returns.
    /// </summary>
    public virtual void PostDrawModStateText(UIElement self, bool enabled) { }

    /// <summary>
    ///     Invoked before <see cref="UIModStateText.DrawPanel(SpriteBatch)"/>
    ///     is called, return <see langword="false"/> to stop it from drawing.
    /// </summary>
    public virtual bool PreDrawModStateTextPanel(UIElement self, bool enabled)
    {
        return true;
    }

    /// <summary>
    ///     Invoked after <see cref="UIModStateText.DrawPanel(SpriteBatch)"/> is
    ///     called, regardless of what
    ///     <see cref="PreDrawModStateTextPanel(UIElement, bool)"/> returns.
    /// </summary>
    public virtual void PostDrawModStateTextPanel(UIElement self, bool enabled) { }

    /// <summary>
    ///     Invoked specifically after the panel is drawn, assuming
    ///     <see cref="PreDraw"/> returned <see langword="true"/>.
    /// </summary>
    public virtual void PostDrawPanel(UIPanel element, SpriteBatch sb) { }

    /// <summary>
    ///     Modifies the "Enabled"/"Disabled" button text.
    /// </summary>
    public virtual Color ModifyEnabledTextColor(bool enabled, Color color)
    {
        return color;
    }

    private static readonly string[] info_keys =
    [
        "tModLoader.ModsXItems",
        "tModLoader.ModsXNPCs",
        "tModLoader.ModsXTiles",
        "tModLoader.ModsXWalls",
        "tModLoader.ModsXBuffs",
        "tModLoader.ModsXMounts",
    ];

    /// <summary>
    ///     Gets the panel information items for this mod.  Offsets are
    ///     automatically calculated.
    /// </summary>
    /// <param name="mod">The mod instance to get information from.</param>
    /// <returns>A collection of panel infos to be displayed.</returns>
    /// <remarks>
    ///     The <paramref name="mod"/> is passed because, despite panels being a
    ///     per-mod thing, there is nothing necessarily limiting mods from
    ///     sharing panel styles or similar cases.
    /// </remarks>
    public virtual IEnumerable<PanelInfo> GetInfos(Mod mod)
    {
        return GetDefaultInfos(mod);
    }

    internal IDisposable OverrideTextures()
    {
        return new TextureOverrider(TextureOverrides);
    }

    internal static IEnumerable<PanelInfo> GetDefaultInfos(Mod mod)
    {
        // Mirrors tML's default behavior of showing items, NPCs, tiles, walls,
        // buffs, and mounts, but more optimized.

        var values = new int[info_keys.Length];
        foreach (var content in mod.GetContent())
        {
            values[0] += content is ModItem ? 1 : 0;
            values[1] += content is ModNPC ? 1 : 0;
            values[2] += content is ModTile ? 1 : 0;
            values[3] += content is ModWall ? 1 : 0;
            values[4] += content is ModBuff ? 1 : 0;
            values[5] += content is ModMount ? 1 : 0;
        }

        for (var i = 0; i < info_keys.Length; i++)
        {
            var count = values[i];
            if (count <= 0)
            {
                continue;
            }

            // Our implementation will handle determining offsets.
            // TODO: Should we let people override this?
            yield return new PanelInfo(
                new UIHoverImage(Main.Assets.Request<Texture2D>(TextureAssets.InfoIcon[i].Name), Language.GetTextValue(info_keys[i], count))
                {
                    RemoveFloatingPointsFromDrawPosition = true,
                }
            );
        }
    }
}
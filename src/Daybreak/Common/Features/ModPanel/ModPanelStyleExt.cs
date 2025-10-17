using JetBrains.Annotations;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.ModPanel;

/// <summary>
///     An API extension to <see cref="ModPanelStyle" /> that enables you to
///     directly interface with the <see cref="UIModItem" /> instance rather than
///     the diluted <see cref="UIPanel" /> instance.
///     <br />
///     Intended for use by developers with assembly publicizers who need
///     convenient access to the object.
/// </summary>
[PublicAPI]
[Autoload(Side = ModSide.Client)]
public abstract class ModPanelStyleExt : ModPanelStyle
{
    /// <inheritdoc cref="ModPanelStyle.PreInitialize" />
    public sealed override bool PreInitialize(UIPanel element)
    {
        return PreInitialize((UIModItem)element);
    }

    /// <inheritdoc cref="ModPanelStyle.PostInitialize" />
    public sealed override void PostInitialize(UIPanel element)
    {
        PostInitialize((UIModItem)element);
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyModIcon" />
    public sealed override UIImage? ModifyModIcon(UIPanel element, UIImage modIcon, ref int modIconAdjust)
    {
        return ModifyModIcon((UIModItem)element, modIcon, ref modIconAdjust);
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyEnabledText" />
    public sealed override string ModifyEnabledText(UIPanel element, string text, bool enabled)
    {
        return ModifyEnabledText((UIModItem)element, text, enabled);
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyHoverTooltip" />
    public sealed override string ModifyHoverTooltip(UIPanel element, string tooltip)
    {
        return ModifyHoverTooltip((UIModItem)element, tooltip);
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyModName" />
    public sealed override UIText ModifyModName(UIPanel element, UIText modName)
    {
        return ModifyModName((UIModItem)element, modName);
    }

    /// <inheritdoc cref="ModPanelStyle.PreSetHoverColors" />
    public sealed override bool PreSetHoverColors(UIPanel element, bool hovered)
    {
        return PreSetHoverColors((UIModItem)element, hovered);
    }

    /// <inheritdoc cref="ModPanelStyle.PostSetHoverColors" />
    public sealed override void PostSetHoverColors(UIPanel element, bool hovered)
    {
        PostSetHoverColors((UIModItem)element, hovered);
    }

    /// <inheritdoc cref="ModPanelStyle.PreDraw" />
    public sealed override bool PreDraw(UIPanel element, SpriteBatch sb)
    {
        return PreDraw((UIModItem)element, sb);
    }

    /// <inheritdoc cref="ModPanelStyle.PostDraw" />
    public sealed override void PostDraw(UIPanel element, SpriteBatch sb)
    {
        PostDraw((UIModItem)element, sb);
    }

    /// <inheritdoc cref="ModPanelStyle.PreDrawPanel" />
    public sealed override bool PreDrawPanel(UIPanel element, SpriteBatch sb, ref bool drawDivider)
    {
        return PreDrawPanel((UIModItem)element, sb, ref drawDivider);
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawPanel" />
    public sealed override void PostDrawPanel(UIPanel element, SpriteBatch sb)
    {
        PostDrawPanel((UIModItem)element, sb);
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyReloadRequiredText" />
    public sealed override string ModifyReloadRequiredText(UIPanel element, string text)
    {
        return ModifyReloadRequiredText((UIModItem)element, text);
    }

    /// <inheritdoc cref="ModPanelStyle.PreDrawReloadRequiredText" />
    public sealed override bool PreDrawReloadRequiredText(UIPanel element)
    {
        return PreDrawReloadRequiredText((UIModItem)element);
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawReloadRequiredText" />
    public sealed override void PostDrawReloadRequiredText(UIPanel element)
    {
        PostDrawReloadRequiredText((UIModItem)element);
    }

    /// <inheritdoc cref="ModPanelStyle.PreDrawModStateText" />
    public sealed override bool PreDrawModStateText(UIElement self, bool enabled)
    {
        return PreDrawModStateText((UIModStateText)self, enabled);
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawModStateText" />
    public sealed override void PostDrawModStateText(UIElement self, bool enabled)
    {
        PostDrawModStateText((UIModStateText)self, enabled);
    }

    /// <inheritdoc cref="ModPanelStyle.PreDrawModStateTextPanel" />
    public sealed override bool PreDrawModStateTextPanel(UIElement self, bool enabled)
    {
        return PreDrawModStateTextPanel((UIModStateText)self, enabled);
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawModStateTextPanel" />
    public sealed override void PostDrawModStateTextPanel(UIElement self, bool enabled)
    {
        PostDrawModStateTextPanel((UIModStateText)self, enabled);
    }

    /// <inheritdoc cref="ModPanelStyle.PreInitialize" />
    public virtual bool PreInitialize(UIModItem element)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostInitialize" />
    public virtual void PostInitialize(UIModItem element) { }

    // ReSharper disable once ReturnTypeCanBeNotNullable
    /// <inheritdoc cref="ModPanelStyle.ModifyModIcon" />
    public virtual UIImage? ModifyModIcon(UIModItem element, UIImage modIcon, ref int modIconAdjust)
    {
        return modIcon;
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyModName" />
    public virtual UIText ModifyModName(UIModItem element, UIText modName)
    {
        return modName;
    }

    /// <inheritdoc cref="ModPanelStyle.PreSetHoverColors" />
    public virtual bool PreSetHoverColors(UIModItem element, bool hovered)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostSetHoverColors" />
    public virtual void PostSetHoverColors(UIModItem element, bool hovered) { }

    /// <inheritdoc cref="ModPanelStyle.PreDraw" />
    public virtual bool PreDraw(UIModItem element, SpriteBatch sb)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostDraw" />
    public virtual void PostDraw(UIModItem element, SpriteBatch sb) { }

    /// <inheritdoc cref="ModPanelStyle.PreDrawPanel" />
    public virtual bool PreDrawPanel(UIModItem element, SpriteBatch sb, ref bool drawDivider)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawPanel" />
    public virtual void PostDrawPanel(UIModItem element, SpriteBatch sb) { }

    /// <inheritdoc cref="ModPanelStyle.ModifyEnabledText(UIPanel, string, bool)" />
    public virtual string ModifyEnabledText(UIModItem element, string text, bool enabled)
    {
        return text;
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyHoverTooltip(UIPanel, string)" />
    public virtual string ModifyHoverTooltip(UIModItem element, string tooltip)
    {
        return tooltip;
    }

    /// <inheritdoc cref="ModPanelStyle.ModifyReloadRequiredText(UIPanel, string)" />
    public virtual string ModifyReloadRequiredText(UIModItem element, string text)
    {
        return text;
    }

    /// <inheritdoc cref="ModPanelStyle.PreDrawReloadRequiredText(UIPanel)" />
    public virtual bool PreDrawReloadRequiredText(UIModItem element)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawReloadRequiredText(UIPanel)" />
    public virtual void PostDrawReloadRequiredText(UIModItem element) { }

    /// <inheritdoc cref="ModPanelStyle.PreDrawModStateText(UIElement, bool)" />
    public virtual bool PreDrawModStateText(UIModStateText element, bool enabled)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawModStateText(UIElement, bool)" />
    public virtual void PostDrawModStateText(UIModStateText element, bool enabled) { }

    /// <inheritdoc cref="ModPanelStyle.PreDrawModStateTextPanel(UIElement, bool)" />
    public virtual bool PreDrawModStateTextPanel(UIModStateText element, bool enabled)
    {
        return true;
    }

    /// <inheritdoc cref="ModPanelStyle.PostDrawModStateTextPanel(UIElement, bool)" />
    public virtual void PostDrawModStateTextPanel(UIModStateText element, bool enabled) { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Daybreak.Common.Features.Hooks;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Daybreak.Common.Features.InfoIcons;

/// <summary>
///     The basic definition of an informative icon that may be rendered.
/// </summary>
[Autoload(Side = ModSide.Client)]
public abstract class InfoIcon : ModTexturedType, ILocalizedModType
{
    /// <inheritdoc />
    public virtual string LocalizationCategory => nameof(InfoIcon);

    /// <summary>
    ///     The description of the icon to show on hover.
    /// </summary>
    public virtual LocalizedText Description => this.GetLocalization(nameof(Description));

    /// <summary>
    ///     The sorting priority of this icon, relative to other icons.  This
    ///     value is clamped to a [0, 1] range.
    /// </summary>
    public virtual float Priority => 0.5f;

    /// <inheritdoc />
    protected sealed override void InitTemplateInstance()
    {
        base.InitTemplateInstance();
    }

    /// <inheritdoc />
    public sealed override void SetupContent()
    {
        base.SetupContent();

        SetStaticDefaults();
    }
}

/// <summary>
///     An icon rendered on a player select UI item.
/// </summary>
[Autoload(Side = ModSide.Client)]
public abstract class PlayerIcon : InfoIcon
{
    /// <inheritdoc />
    protected sealed override void Register()
    {
        InfoIcons.Register(this);
        // ModTypeLookup<PlayerIcon>.Register(this);
    }

    /// <summary>
    ///     Whether this icon is visible.
    /// </summary>
    public abstract bool IsVisible(PlayerFileData playerFile);
}

/// <summary>
///     An icon rendered on a world select UI item.
/// </summary>
[Autoload(Side = ModSide.Client)]
public abstract class WorldIcon : InfoIcon
{
    /// <inheritdoc />
    protected sealed override void Register()
    {
        InfoIcons.Register(this);
        // ModTypeLookup<WorldIcon>.Register(this);
    }

    /// <summary>
    ///     Whether this icon is visible.
    /// </summary>
    public abstract bool IsVisible(WorldFileData worldFile);
}

[Autoload(Side = ModSide.Client)]
internal static class InfoIcons
{
    private sealed class ScrollableIconPanel : UIElement
    {
        
    }
    
    private static List<PlayerIcon> playerIcons = [];
    private static List<WorldIcon> worldIcons = [];

    public static void Register(PlayerIcon playerIcon)
    {
        playerIcons.Add(playerIcon);
    }

    public static void Register(WorldIcon worldIcon)
    {
        worldIcons.Add(worldIcon);
    }

    [ModSystemHooks.PostSetupContent]
    private static void SortIcons()
    {
        playerIcons = playerIcons.OrderBy(x => Math.Clamp(x.Priority, 0f, 1f)).ToList();
        worldIcons = worldIcons.OrderBy(x => Math.Clamp(x.Priority, 0f, 1f)).ToList();
    }

    [OnLoad]
    private static void ApplyHooks()
    {
        // This system is derivative of Luminance's, so we can't load it too
        // early.
        if (
            ModLoader.TryGetMod("Luminance", out var luminance)
         && luminance.Version <= new Version(1, 0, 13)
        )
        {
            return;
        }

        On_UICharacterListItem.DrawSelf += DrawSelf_DrawPlayerInfoIcons;
        On_UICharacterSelect.Draw += Draw_DrawPlayerInfoIconHoverText;

        On_UIWorldListItem.DrawSelf += DrawSelf_DrawWorldInfoIcons;
        On_UIWorldSelect.Draw += Draw_DrawWorldInfoIconHoverText;
    }

    private static void DrawSelf_DrawPlayerInfoIcons(
        On_UICharacterListItem.orig_DrawSelf orig,
        UICharacterListItem self,
        SpriteBatch spriteBatch
    )
    {
        orig(self, spriteBatch);

        /*
        var playerFile = self.Data;

        foreach (var icon in playerIcons)
        {
            if (!icon.IsVisible(playerFile))
            {
                continue;
            }

            icon.DrawIcon();
        }
        */
    }

    private static void Draw_DrawPlayerInfoIconHoverText(
        On_UICharacterSelect.orig_Draw orig,
        UICharacterSelect self,
        SpriteBatch spriteBatch
    )
    {
        orig(self, spriteBatch);
    }

    private static void DrawSelf_DrawWorldInfoIcons(
        On_UIWorldListItem.orig_DrawSelf orig,
        UIWorldListItem self,
        SpriteBatch spriteBatch
    )
    {
        orig(self, spriteBatch);
    }

    private static void Draw_DrawWorldInfoIconHoverText(
        On_UIWorldSelect.orig_Draw orig,
        UIWorldSelect self,
        SpriteBatch spriteBatch
    )
    {
        orig(self, spriteBatch);
    }
}

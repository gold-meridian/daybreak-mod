using System;
using System.Reflection;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Features.InfoIcons;
using Luminance.Core.MenuInfoUI;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;

#pragma warning disable CS0618 // Type or member is obsolete

namespace Daybreak.Content.Compatibility;

/// <summary>
///     Redirects Luminance's Info Icon API to DAYBREAK's.
/// </summary>
[ExtendsFromMod("Luminance")]
internal static class LuminanceCompat
{
    [Autoload(false)]
    private sealed class LuminanceDaybreakPlayerIcon(PlayerInfoIcon icon) : PlayerIcon
    {
        public override string Texture => icon.TexturePath;

        public override LocalizedText Description => Language.GetText(icon.HoverTextKey);

        public override float Priority => icon.Priority / 255f;

        public override bool IsVisible(PlayerFileData playerFile)
        {
            return icon.ShouldAppear(playerFile.Player);
        }
    }

    [Autoload(false)]
    private sealed class LuminanceDaybreakWorldIcon(WorldInfoIcon icon) : WorldIcon
    {
        public override string Texture => icon.TexturePath;

        public override LocalizedText Description => Language.GetText(icon.HoverTextKey);

        public override float Priority => icon.Priority / 255f;

        public override bool IsVisible(WorldFileData worldFile)
        {
            return icon.ShouldAppear(worldFile);
        }
    }

    [OnLoad]
    private static void ApplyHooks()
    {
        MonoModHooks.Add(
            typeof(InternalInfoUIManager).GetMethod(nameof(InternalInfoUIManager.RegisterManager), BindingFlags.NonPublic | BindingFlags.Static),
            RegisterManager_RegisterToDaybreakSystem
        );
    }

    private static void RegisterManager_RegisterToDaybreakSystem(
        Action<InfoUIManager> orig,
        InfoUIManager manager
    )
    {
        orig(manager);

        foreach (var playerIcon in manager.GetPlayerInfoIcons())
        {
            manager.Mod.AddContent(new LuminanceDaybreakPlayerIcon(playerIcon));
        }

        foreach (var worldIcon in manager.GetWorldInfoIcons())
        {
            manager.Mod.AddContent(new LuminanceDaybreakWorldIcon(worldIcon));
        }
    }
}

using Daybreak.Hooks.V1;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;

namespace Daybreak.ChatTags.V1;

/// <summary>
///     When implemented by a <see cref="Mod" />, allows the mod to modify their
///     Author text in the Mod Panel.
/// </summary>
/// <remarks>
///     For more advanced use cases, directly implement
///     <see cref="ModPanelStyle"/> and change the text there.
/// </remarks>
public interface IHasCustomAuthorMessage
{
    /// <summary>
    ///     Returns the author text to display.
    /// </summary>
    string GetAuthorText();

    [OnLoad]
    private static void Load()
    {
        MonoModHooks.Add(
            typeof(UIModItem).GetMethod(
                "DrawSelf",
                BindingFlags.Instance | BindingFlags.NonPublic
            ),
            DrawSelf_AuthorTooltip
        );
    }

    private static void DrawSelf_AuthorTooltip(Action<UIModItem, SpriteBatch> orig, UIModItem self, SpriteBatch sb)
    {
        orig(self, sb);

        if (self._modName?.IsMouseHovering is not true
         || self._tooltip != Language.GetTextValue("tModLoader.ModsByline", self._mod.properties.author)
         || !ModLoader.TryGetMod(self._mod.Name, out var mod)
         || mod is not IHasCustomAuthorMessage authorMessageProvider)
        {
            return;
        }

        self._tooltip = authorMessageProvider.GetAuthorText();
    }
}
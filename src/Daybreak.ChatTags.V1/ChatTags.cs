using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Daybreak.Hooks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Chat;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Daybreak.ChatTags;

/// <summary>
///     Utilities and other APIs relating to chat tags.
/// </summary>
public static class ChatTags
{
    private const string color_tag = "color";
    private const string item_tag = "item";
    private const string name_tag = "name";
    private const string achievement_tag = "achievement";
    private const string glyph_tag = "g";
    private const string xbox_glyph_tag = "gx";
    private const string ps_glyph_tag = "gp";
    private const string switch_glyph_tag = "gn";

    /// <summary>
    ///     The handler for the color tag.
    /// </summary>
    public static ColorTagHandler Color => (ColorTagHandler)ChatManager.GetHandler(color_tag);

    /// <summary>
    ///     The handler for the item tag.
    /// </summary>
    public static ItemTagHandler Item => (ItemTagHandler)ChatManager.GetHandler(item_tag);

    /// <summary>
    ///     The handler for the name tag.
    /// </summary>
    public static NameTagHandler Name => (NameTagHandler)ChatManager.GetHandler(name_tag);

    /// <summary>
    ///     The handler for the achievement tag.
    /// </summary>
    public static AchievementTagHandler Achievement => (AchievementTagHandler)ChatManager.GetHandler(achievement_tag);

    // FIXME: A bug in vanilla means we can't use the `glyph` tag here.  We have
    //        to use the guaranteed `g` alias.  `glyph` *should* correspond to
    //        the plugged-in controller's glyphs, but it gets overwritten.
    /// <summary>
    ///     The handler for the controller-dependent glyph tag.  This tag will
    ///     show the glyph corresponding to the plugged-in controller.
    /// </summary>
    public static GlyphTagHandler Glyph => (GlyphTagHandler)ChatManager.GetHandler(glyph_tag);

    /// <summary>
    ///     The handler for the XBOX controller glyphs.
    /// </summary>
    public static GlyphTagHandler.GlyphXboxTagHandler XboxGlyph => (GlyphTagHandler.GlyphXboxTagHandler)ChatManager.GetHandler(xbox_glyph_tag);

    /// <summary>
    ///     The handler for the PlayStation controller glyphs.
    /// </summary>
    public static GlyphTagHandler.GlyphPSTagHandler PsGlyph => (GlyphTagHandler.GlyphPSTagHandler)ChatManager.GetHandler(ps_glyph_tag);

    /// <summary>
    ///     The handler for the Nintendo Switch controller glyphs.
    /// </summary>
    public static GlyphTagHandler.GlyphSwitchTagHandler SwitchGlyph => (GlyphTagHandler.GlyphSwitchTagHandler)ChatManager.GetHandler(switch_glyph_tag);

    private static readonly List<ChatTag> tags = [];

    private static readonly string[] cached_tags_glyph =
    [
        $"[{glyph_tag}:0]",
        $"[{glyph_tag}:1]",
        $"[{glyph_tag}:2]",
        $"[{glyph_tag}:3]",
        $"[{glyph_tag}:4]",
        $"[{glyph_tag}:5]",
        $"[{glyph_tag}:6]",
        $"[{glyph_tag}:7]",
        $"[{glyph_tag}:8]",
        $"[{glyph_tag}:9]",
        $"[{glyph_tag}:10]",
        $"[{glyph_tag}:11]",
        $"[{glyph_tag}:12]",
        $"[{glyph_tag}:13]",
        $"[{glyph_tag}:14]",
        $"[{glyph_tag}:15]",
        $"[{glyph_tag}:16]",
        $"[{glyph_tag}:17]",
        $"[{glyph_tag}:18]",
        $"[{glyph_tag}:19]",
        $"[{glyph_tag}:20]",
        $"[{glyph_tag}:21]",
        $"[{glyph_tag}:22]",
        $"[{glyph_tag}:23]",
        $"[{glyph_tag}:24]",
        $"[{glyph_tag}:25]",
    ];

    private static readonly string[] cached_tags_glyph_xbox =
    [
        $"[{xbox_glyph_tag}:0]",
        $"[{xbox_glyph_tag}:1]",
        $"[{xbox_glyph_tag}:2]",
        $"[{xbox_glyph_tag}:3]",
        $"[{xbox_glyph_tag}:4]",
        $"[{xbox_glyph_tag}:5]",
        $"[{xbox_glyph_tag}:6]",
        $"[{xbox_glyph_tag}:7]",
        $"[{xbox_glyph_tag}:8]",
        $"[{xbox_glyph_tag}:9]",
        $"[{xbox_glyph_tag}:10]",
        $"[{xbox_glyph_tag}:11]",
        $"[{xbox_glyph_tag}:12]",
        $"[{xbox_glyph_tag}:13]",
        $"[{xbox_glyph_tag}:14]",
        $"[{xbox_glyph_tag}:15]",
        $"[{xbox_glyph_tag}:16]",
        $"[{xbox_glyph_tag}:17]",
        $"[{xbox_glyph_tag}:18]",
        $"[{xbox_glyph_tag}:19]",
        $"[{xbox_glyph_tag}:20]",
        $"[{xbox_glyph_tag}:21]",
        $"[{xbox_glyph_tag}:22]",
        $"[{xbox_glyph_tag}:23]",
        $"[{xbox_glyph_tag}:24]",
        $"[{xbox_glyph_tag}:25]",
    ];

    private static readonly string[] cached_tags_glyph_ps =
    [
        $"[{ps_glyph_tag}:0]",
        $"[{ps_glyph_tag}:1]",
        $"[{ps_glyph_tag}:2]",
        $"[{ps_glyph_tag}:3]",
        $"[{ps_glyph_tag}:4]",
        $"[{ps_glyph_tag}:5]",
        $"[{ps_glyph_tag}:6]",
        $"[{ps_glyph_tag}:7]",
        $"[{ps_glyph_tag}:8]",
        $"[{ps_glyph_tag}:9]",
        $"[{ps_glyph_tag}:10]",
        $"[{ps_glyph_tag}:11]",
        $"[{ps_glyph_tag}:12]",
        $"[{ps_glyph_tag}:13]",
        $"[{ps_glyph_tag}:14]",
        $"[{ps_glyph_tag}:15]",
        $"[{ps_glyph_tag}:16]",
        $"[{ps_glyph_tag}:17]",
        $"[{ps_glyph_tag}:18]",
        $"[{ps_glyph_tag}:19]",
        $"[{ps_glyph_tag}:20]",
        $"[{ps_glyph_tag}:21]",
        $"[{ps_glyph_tag}:22]",
        $"[{ps_glyph_tag}:23]",
        $"[{ps_glyph_tag}:24]",
        $"[{ps_glyph_tag}:25]",
    ];

    private static readonly string[] cached_tags_glyph_switch =
    [
        $"[{switch_glyph_tag}:0]",
        $"[{switch_glyph_tag}:1]",
        $"[{switch_glyph_tag}:2]",
        $"[{switch_glyph_tag}:3]",
        $"[{switch_glyph_tag}:4]",
        $"[{switch_glyph_tag}:5]",
        $"[{switch_glyph_tag}:6]",
        $"[{switch_glyph_tag}:7]",
        $"[{switch_glyph_tag}:8]",
        $"[{switch_glyph_tag}:9]",
        $"[{switch_glyph_tag}:10]",
        $"[{switch_glyph_tag}:11]",
        $"[{switch_glyph_tag}:12]",
        $"[{switch_glyph_tag}:13]",
        $"[{switch_glyph_tag}:14]",
        $"[{switch_glyph_tag}:15]",
        $"[{switch_glyph_tag}:16]",
        $"[{switch_glyph_tag}:17]",
        $"[{switch_glyph_tag}:18]",
        $"[{switch_glyph_tag}:19]",
        $"[{switch_glyph_tag}:20]",
        $"[{switch_glyph_tag}:21]",
        $"[{switch_glyph_tag}:22]",
        $"[{switch_glyph_tag}:23]",
        $"[{switch_glyph_tag}:24]",
        $"[{switch_glyph_tag}:25]",
    ];

    // This lets us keep track of registered modded ChatTags, but also allows us
    // to register them to ChatManager without needing a generic parameter (and
    // without allocating a new instance).
    /// <summary>
    ///     Registers the <paramref name="tag"/> instance under the primary
    ///     <paramref name="name"/> and any possible
    ///     <paramref name="aliases"/>.
    /// </summary>
    public static void Register(ChatTag tag, string name, IEnumerable<string> aliases)
    {
        tags.Add(tag);

        // We use ToLowerInvariant instead of ToLower... just in case.
        ChatManager._handlers[name.ToLowerInvariant()] = tag;

        foreach (var alias in aliases)
        {
            ChatManager._handlers[alias.ToLowerInvariant()] = tag;
        }
    }

    [OnUnload]
    private static void UnloadTags()
    {
        foreach (var tag in tags)
        {
            ChatManager._handlers.TryRemove(tag.TagName.ToLowerInvariant(), out _);

            foreach (var alias in tag.AliasNames)
            {
                ChatManager._handlers.TryRemove(alias.ToLowerInvariant(), out _);
            }
        }
    }

    extension(ColorTagHandler)
    {
        /// <summary>
        ///     Generates a color tag.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateTag(Color color, string message)
        {
            return $"[c/{color.Hex3()}:{message}]";
        }
    }

    /*
    private static readonly List<string> item_tag_parts = new(capacity: 2);

    private static void PopulateItemTagParts(int stack, int prefix)
    {
        item_tag_parts.Clear();

        if (stack > 1)
        {
            item_tag_parts.Add($"s{stack}");
        }

        if (prefix > 0)
        {
            item_tag_parts.Add($"p{prefix}");
        }
    }

    extension(ItemTagHandler _)
    {
        public string MakeString(int itemId, int stack = 1, int prefix = 0)
        {
            PopulateItemTagParts(stack, prefix);

            if (item_tag_parts.Count == 0)
            {
                return $"[i:{itemId}]";
            }

            return $"[i/{string.Join(",", item_tag_parts)}:{itemId}]";
        }

        public string MakeString(string itemId, int stack = 1, int prefix = 0)
        {
            PopulateItemTagParts(stack, prefix);

            if (item_tag_parts.Count == 0)
            {
                return $"[i:{itemId}]";
            }

            return $"[i/{string.Join(",", item_tag_parts)}:{itemId}]";
        }
    }
    */

    /*
    extension(NameTagHandler _)
    {
        public string MakeString(string name)
        {
            return ""
        }
    }
    */

    extension(AchievementTagHandler)
    {
        /// <summary>
        ///     Generates an achievement tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateTag(ModAchievement achievement)
        {
            return AchievementTagHandler.GenerateTag(achievement.Achievement);
        }
    }

    extension(GlyphTagHandler)
    {
        /// <summary>
        ///     Generates a glyph tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateTag(Glyphs glyph)
        {
            return cached_tags_glyph[(int)glyph];
        }

        /// <summary>
        ///     Generates a glyph tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateXboxTag(int index)
        {
            return cached_tags_glyph_xbox[index];
        }

        /// <summary>
        ///     Generates a glyph tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateXboxTag(Glyphs glyph)
        {
            return cached_tags_glyph_xbox[(int)glyph];
        }
        
        /// <summary>
        ///     Generates a glyph tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GeneratePsTag(int index)
        {
            return cached_tags_glyph_ps[index];
        }

        /// <summary>
        ///     Generates a glyph tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GeneratePsTag(Glyphs glyph)
        {
            return cached_tags_glyph_ps[(int)glyph];
        }
        
        /// <summary>
        ///     Generates a glyph tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateSwitchTag(int index)
        {
            return cached_tags_glyph_switch[index];
        }

        /// <summary>
        ///     Generates a glyph tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GenerateSwitchTag(Glyphs glyph)
        {
            return cached_tags_glyph_switch[(int)glyph];
        }
    }
}

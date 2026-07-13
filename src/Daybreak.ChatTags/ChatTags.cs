using System.Collections.Generic;
using Terraria.GameContent.UI.Chat;
using Terraria.UI.Chat;

namespace Daybreak.ChatTags;

/// <summary>
///     Utilities and other APIs relating to chat tags.
/// </summary>
public static class ChatTags
{
    /// <summary>
    ///     The handler for the color tag.
    /// </summary>
    public static ColorTagHandler Color => (ColorTagHandler)ChatManager.GetHandler("color");
    
    /// <summary>
    ///     The handler for the item tag.
    /// </summary>
    public static ItemTagHandler Item => (ItemTagHandler)ChatManager.GetHandler("item");
    
    /// <summary>
    ///     The handler for the name tag.
    /// </summary>
    public static NameTagHandler Name => (NameTagHandler)ChatManager.GetHandler("name");
    
    /// <summary>
    ///     The handler for the achievement tag.
    /// </summary>
    public static AchievementTagHandler Achievement => (AchievementTagHandler)ChatManager.GetHandler("achievement");
    
    // FIXME: A bug in vanilla means we can't use the `glyph` tag here.  We have
    //        to use the guaranteed `g` alias.  `glyph` *should* correspond to
    //        the plugged-in controller's glyphs, but it gets overwritten.
    /// <summary>
    ///     The handler for the controller-dependent glyph tag.  This tag will
    ///     show the glyph corresponding to the plugged-in controller.
    /// </summary>
    public static GlyphTagHandler Glyph => (GlyphTagHandler)ChatManager.GetHandler("g");
    
    /// <summary>
    ///     The handler for the XBOX controller glyphs.
    /// </summary>
    public static GlyphTagHandler.GlyphXboxTagHandler XboxGlyph => (GlyphTagHandler.GlyphXboxTagHandler)ChatManager.GetHandler("gx");
    
    /// <summary>
    ///     The handler for the PlayStation controller glyphs.
    /// </summary>
    public static GlyphTagHandler.GlyphPSTagHandler PsGlyph => (GlyphTagHandler.GlyphPSTagHandler)ChatManager.GetHandler("gp");
    
    /// <summary>
    ///     The handler for the Nintendo Switch controller glyphs.
    /// </summary>
    public static GlyphTagHandler.GlyphSwitchTagHandler SwitchGlyph => (GlyphTagHandler.GlyphSwitchTagHandler)ChatManager.GetHandler("gn");
    
    extension(ChatManager)
    {
        // This overload exists to avoid CRTP in our ChatTag API and to avoid
        // allocations when registering with aliases.
        /// <summary>
        ///     Registers the <paramref name="tag"/> instance under the primary
        ///     <paramref name="name"/> and any possible
        ///     <paramref name="aliases"/>.
        /// </summary>
        public static void Register(ITagHandler tag, string name, IEnumerable<string> aliases)
        {
            // We use ToLowerInvariant instead of ToLower... just in case.
            ChatManager._handlers[name.ToLowerInvariant()] = tag;

            foreach (var alias in aliases)
            {
                ChatManager._handlers[alias.ToLowerInvariant()] = tag;
            }
        }
    }
}

using Daybreak.Common.Features.Hooks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ReLogic.Graphics;
using ReLogic.Localization.IME;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.GameInput;
using Terraria.Initializers;
using Terraria.UI.Chat;

namespace Daybreak.Common.UI;

internal enum InputCancellationType : byte
{
    None,
    Escaped,
    Confirmed,
}

/// <summary>
///     Simpler input system based on Vanilla's <see cref="Main.GetInputText"/>
///     with logic for input cancellation via the
///     <see cref="InputCancellationType"/> enum.
/// </summary>
internal static class InputHelpers
{
    private const int max_stroke_length = 20;

    private const int key_timer_delay = 45;

    private static readonly char[] invalidChars =
        [.. Enumerable.Range('\x0', '\x1F' + 1).Select(i => (char)i), // Invisible characters from Null to Unit Separator, stopping before Space.
        '\x7F',                                                       // Delete.
        ];

    private static int backspaceTimer = key_timer_delay;

    private static int leftArrowTimer = key_timer_delay;

    private static int rightArrowTimer = key_timer_delay;

    private static readonly StringBuilder keyStroke = new StringBuilder();

    // Stores the state of WritingText for use outside of drawing scopes.
    private static bool wasWritingText;

    private static float blinkerStartTime;

    public static bool WritingText
    {
        get => PlayerInput.WritingText;
        set
        {
            wasWritingText |= value;
            PlayerInput.WritingText = value;
        }
    }

    /// <summary>
    /// The index of the "cursor" where written text is inserted.
    /// </summary>
    public static int CursorPositon { get; set; }

    [OnLoad]
    public static void Load()
    {
        On_Main.DoUpdate_HandleInput += DoUpdate_HandleInput_UpdateWasWritingText;
        On_UILinksInitializer.FancyExit += FancyExit_IgnoreExitIfWriting;
        Platform.Get<IImeService>().AddKeyListener(OnKeyStroke);
    }

    [OnUnload]
    public static void Unload()
    {
        Platform.Get<IImeService>().RemoveKeyListener(OnKeyStroke);
    }

    private static void DoUpdate_HandleInput_UpdateWasWritingText(On_Main.orig_DoUpdate_HandleInput orig, Main self)
    {
        wasWritingText = WritingText;
        orig(self);
    }

    private static void OnKeyStroke(char key)
    {
        if (WritingText &&
            keyStroke.Length <= max_stroke_length)
        {
            keyStroke.Append(key);
        }
    }

    private static void FancyExit_IgnoreExitIfWriting(On_UILinksInitializer.orig_FancyExit orig)
    {
        if (!wasWritingText)
        {
            orig();
        }
    }

    public static InputCancellationType GetInput(
        string input,
        out string output,
        bool allowLineBreaks = false,
        bool allowChatTags = false,
        IEnumerable<char>? blacklistedChars = null,
        IEnumerable<char>? whitelistedChars = null)
    {
        output = input;

        // Perhaps use our own KeyboardStates?
        Main.oldInputText = Main.inputText;
        Main.inputText = Keyboard.GetState();

        WritingText &= Main.hasFocus;

        if (!WritingText)
        {
            return InputCancellationType.None;
        }

        Main.instance.HandleIME();

        blacklistedChars ??= [];
        blacklistedChars = blacklistedChars.Concat(invalidChars);

        // Arrow key movement
        {
            int left = 1;
            int right = 1;

            if (allowChatTags)
            {
                TryGetNeighborSnippets(input, out var previous, out var next);

                left = previous?.DeleteWhole is true ? previous.TextOriginal.Length : left;
                right = next?.DeleteWhole is true ? next.TextOriginal.Length : right;
            }

            // Left
            if (Keys.Left.Held)
            {
                leftArrowTimer--;
            }
            else
            {
                leftArrowTimer = key_timer_delay;
            }

            if (Keys.Left.JustPressed ||
                (Keys.Left.Held &&
                leftArrowTimer <= 0))
            {
                CursorPositon -= left;
            }

            // Right
            if (Keys.Right.Held)
            {
                rightArrowTimer--;
            }
            else
            {
                rightArrowTimer = key_timer_delay;
            }

            if (Keys.Right.JustPressed ||
                (Keys.Right.Held &&
                rightArrowTimer <= 0))
            {
                CursorPositon += right;
            }
        }

        CursorPositon = Math.Clamp(CursorPositon, 0, output.Length);

        // Special actions
        {
            var controlPressed =
                (Keys.LeftControl.Pressed ||
                Keys.RightControl.Pressed) &&
                !Keys.LeftAlt.Pressed &&
                !Keys.RightAlt.Pressed;

            var shiftPressed =
                Keys.LeftShift.Pressed ||
                Keys.RightShift.Pressed;

            // Clear
            if (controlPressed && Keys.Z.JustPressed)
            {
                output = string.Empty;
                CursorPositon = 0;
            }
            // Cut
            else if (
                (controlPressed && Keys.X.JustPressed) ||
                (shiftPressed && Keys.Delete.JustPressed))
            {
                Platform.Get<IClipboard>().Value = output;
                output = string.Empty;
                CursorPositon = 0;
            }
            // Copy
            else if (Keys.C.JustPressed)
            {
                Platform.Get<IClipboard>().Value = output;
            }
            // Paste
            else if (
                (controlPressed && Keys.V.JustPressed) ||
                (shiftPressed && Keys.Insert.JustPressed))
            {
                var paste = RemoveInvalidCharacters(GetPaste(output, allowLineBreaks));

                output = output.Insert(CursorPositon, paste);

                CursorPositon += paste.Length;
            }
        }

        // Input
        {
            if (keyStroke.Length >= 1)
            {
                var stroke = RemoveInvalidCharacters(keyStroke.ToString());

                keyStroke.Clear();

                output = output.Insert(CursorPositon, stroke);

                CursorPositon += stroke.Length;
            }
        }

        // Backspace
        {
            if (Keys.Back.Held)
            {
                backspaceTimer--;
            }
            else
            {
                backspaceTimer = key_timer_delay;
            }
            if ((Keys.Back.JustPressed ||
                (Keys.Back.Held &&
                backspaceTimer <= 0)) &&
                output.Length >= 1 &&
                CursorPositon >= 1)
            {
                output = string.Concat(output.AsSpan(0, CursorPositon - 1),
                    output.AsSpan(CursorPositon, output.Length - CursorPositon));

                CursorPositon--;
            }
        }

        // Escapes
        {
            // Unsure of why vanilla checks if you're on Windows before allowing escape inputs.
            // if (!Platform.IsWindows && Main.inputText.IsKeyDown(Keys.Escape) && !Main.oldInputText.IsKeyDown(Keys.Escape))
            if (Keys.Escape.JustPressed)
            {
                // Definitly sketchy, but is designed to prevent UI from vanishing whilst typing.
                PlayerInput.WritingText = false;
                return InputCancellationType.Escaped;
            }

            if (Keys.Enter.JustPressed)
            {
                WritingText = false;
                return InputCancellationType.Confirmed;
            }
        }

        return InputCancellationType.None;

        void TryGetNeighborSnippets(string input, out TextSnippet? previous, out TextSnippet? next)
        {
            var snippets = ChatManager.ParseMessage(input, Color.White);

            previous = null;
            next = null;

            int length = 0;

            for (int i = 0; i < snippets.Count; i++)
            {
                if (length + snippets[i].TextOriginal.Length > CursorPositon)
                {
                    next = snippets[i];

                    if (CursorPositon > length)
                    {
                        previous = snippets[i];
                    }

                    break;
                }

                length += snippets[i].TextOriginal.Length;

                previous = snippets[i];
            }
        }

        string RemoveInvalidCharacters(string input)
        {
            foreach (var c in blacklistedChars)
            {
                input = input.Replace(c.ToString(), string.Empty);
            }

            if (whitelistedChars is not null &&
                whitelistedChars.Any())
            {
                input = new string(input.Where(c => whitelistedChars.Contains(c)).ToArray());
            }

            return input;
        }
    }

    private static string GetPaste(string input, bool allowLineBreaks)
    {
        return input.Insert(
                            CursorPositon,
                            allowLineBreaks ?
                                Platform.Get<IClipboard>().MultiLineValue :
                                Platform.Get<IClipboard>().Value);
    }

    public static void DrawInputString(
        this SpriteBatch spriteBatch,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        Vector2 origin,
        Vector2 scale,
        bool drawBlinker = false,
        int blinkerIndex = -1,
        bool allowChatTags = false
    )
    {
        spriteBatch.DrawInputStringWithShadow(
            font,
            text,
            position,
            color,
            Color.Black,
            origin,
            scale,
            drawBlinker,
            blinkerIndex,
            allowChatTags,
            -1f
        );
    }

    public static void DrawInputStringWithShadow(
        this SpriteBatch spriteBatch,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        Vector2 origin,
        Vector2 scale,
        bool drawBlinker = false,
        int blinkerIndex = -1,
        bool allowChatTags = false
    )
    {
        spriteBatch.DrawInputStringWithShadow(
            font,
            text,
            position,
            color,
            Color.Black,
            origin,
            scale,
            drawBlinker,
            blinkerIndex,
            allowChatTags
        );
    }

    public static void DrawInputStringWithShadow(
        this SpriteBatch spriteBatch,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        Color shadowColor,
        Vector2 origin,
        Vector2 scale,
        bool drawBlinker = false,
        int blinkerIndex = -1,
        bool allowChatTags = false,
        float spread = 2f
    )
    {
        DrawStringWithShadow(
            text,
            position,
            color,
            shadowColor,
            origin,
            scale
        );

        // Blinker
        {
            const float blink_duration_in_seconds = 2f / 3f;
            const float blink_percent = 0.5f;

            var blinkTimer = MathF.Max(0f, (Main.GlobalTimeWrappedHourly - blinkerStartTime) % blink_duration_in_seconds);

            if (drawBlinker && blinkerIndex != -1 && blinkTimer < blink_duration_in_seconds * blink_percent)
            {
                const float blinker_x_offset = -2f;

                var blinkerX =
                    allowChatTags
                    ? ChatManager.GetStringSize(font, text[..blinkerIndex], Vector2.One).X
                    : font.MeasureString(text[..blinkerIndex]).X;

                blinkerX += blinker_x_offset;

                blinkerX *= scale.X;

                var blinkerOffset = new Vector2(blinkerX, 0) - origin;
                blinkerOffset *= scale;

                DrawStringWithShadow(
                    "|",
                    position + blinkerOffset,
                    color,
                    shadowColor,
                    Vector2.Zero,
                    scale
                );
            }
        }

        return;

        void DrawStringWithShadow(
            string text,
            Vector2 position,
            Color color,
            Color shadowColor,
            Vector2 origin,
            Vector2 scale
        )
        {
            if (allowChatTags)
            {
                ChatManager.DrawColorCodedStringWithShadow(
                    spriteBatch,
                    font,
                    text,
                    position - (origin * scale),
                    color,
                    shadowColor,
                    0f,
                    Vector2.Zero,
                    scale,
                    -1,
                    spread
                );

                return;
            }

            if (spread > 0f)
            {
                for (var i = 0; i < ChatManager.ShadowDirections.Length; i++)
                {
                    spriteBatch.DrawString(
                        font,
                        text,
                        position + ChatManager.ShadowDirections[i] * spread,
                        shadowColor,
                        0f,
                        origin,
                        scale,
                        SpriteEffects.None,
                        0f
                    );
                }
            }

            spriteBatch.DrawString(
                font,
                text,
                position,
                color,
                0f,
                origin,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }

    public static int GetHoveredCharacter(
        this DynamicSpriteFont font,
        string text,
        Vector2 mousePosition,
        Vector2 position,
        Vector2 origin,
        Vector2 scale,
        bool allowChatTags = false
    )
    {
        mousePosition -= position;
        mousePosition -= origin;
        mousePosition *= scale;

        if (allowChatTags)
        {
            var snippets = ChatManager.ParseMessage(text, Color.White);

            var totalWidth = -origin.X * 2;

            int charCount = 0;

            foreach (var snippet in snippets)
            {
                totalWidth += snippet.GetStringLength(font);

                if (totalWidth <= mousePosition.X)
                {
                    charCount += snippet.TextOriginal.Length;

                    continue;
                }

                if (snippet.DeleteWhole)
                {
                    return charCount + snippet.TextOriginal.Length;
                }
                else if (FindHoveredCharacter(charCount, snippet.TextOriginal.Length, out int index))
                {
                    return index;
                }

                break;
            }
        }
        else if (FindHoveredCharacter(0, text.Length, out int index))
        {
            return index;
        }

        return text.Length;

        bool FindHoveredCharacter(int start, int length, out int index)
        {
            var first = true;
            var lastKerning = 0f;

            var totalWidth = -origin.X * 2;

            if (start > 0)
            {
                totalWidth +=
                    allowChatTags
                    ? ChatManager.GetStringSize(font, text[..start], Vector2.One).X
                    : font.MeasureString(text[..start]).X;
            }

            index = 0;

            for (var i = start; i < start + length; i++)
            {
                var c = text[i];

                var charSize = font.MeasureChar(c, first, scale, lastKerning, out lastKerning);

                if (mousePosition.X >= totalWidth && mousePosition.X <= totalWidth + charSize.X)
                {
                    index = mousePosition.X >= totalWidth + (charSize.X * .5f) ? i + 1 : i;

                    return true;
                }

                totalWidth += charSize.X;
                first = false;
            }

            return false;
        }
    }

    private static Vector2 MeasureChar(this DynamicSpriteFont font, char c, bool firstChar, Vector2 scale, float lastKerning, out float kerningZ)
    {
        var output = Vector2.Zero;
        output.Y = font.LineSpacing;

        var characterData = font.GetCharacterData(c);
        var kerning = characterData.Kerning;

        if (firstChar)
        {
            kerning.X = Math.Max(kerning.X, 0f);
        }
        else
        {
            output.X += font.CharacterSpacing + lastKerning;
        }

        output.X += kerning.X + kerning.Y;

        output.Y = Math.Max(output.Y, characterData.Padding.Height);

        output.X += Math.Max(kerning.Z, 0f);

        output *= scale;

        kerningZ = kerning.Z;

        kerningZ *= scale.X;

        return output;
    }

    public static void SyncBlinkerStartTime()
    {
        blinkerStartTime = Main.GlobalTimeWrappedHourly;
    }

    extension(Keys key)
    {
        public bool Pressed =>
            Main.inputText.IsKeyDown(key);

        public bool Held =>
            Main.inputText.IsKeyDown(key) &&
            Main.oldInputText.IsKeyDown(key);

        public bool JustPressed =>
            Main.inputText.IsKeyDown(key) &&
            !Main.oldInputText.IsKeyDown(key);

        public bool JustReleased =>
            !Main.inputText.IsKeyDown(key) &&
            Main.oldInputText.IsKeyDown(key);
    }
}

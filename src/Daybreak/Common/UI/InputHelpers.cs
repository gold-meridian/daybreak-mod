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
using static ReLogic.Graphics.DynamicSpriteFont;

namespace Daybreak.Common.UI;

internal enum InputCancellationType : byte
{
    None,
    Escaped,
    Confirmed
}

/// <summary>
/// Simpler input system based on Vanilla's <see cref="Main.GetInputText"/>;
/// with logic for input cancellation via the <see cref="InputCancellationType"/> enum.
/// </summary>
internal static class InputHelpers
{
    private const int max_stroke_length = 20;

    private const int key_timer_delay = 45;

    private static readonly char[] invalidChars =
        [.. Enumerable.Range('\x0', '\x1F' + 1).Select(i => (char)i), // Invisible characters from Null to Unit Separator, stopping before Space.
        '\x7F' // Delete.
        ];

    private static int backspaceTimer = key_timer_delay;

    private static int leftArrowTimer = key_timer_delay;

    private static int rightArrowTimer = key_timer_delay;

    private static readonly StringBuilder keyStroke = new();

    // Stores the state of WritingText for use outside of drawing scopes.
    private static bool wasWritingText;

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
            keyStroke.Append(key);
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
        IEnumerable<char>? blacklistedChars = null,
        IEnumerable<char>? whitelistedChars = null)
    {
        output = input;

        // Perhaps use our own KeyboardStates?
        Main.oldInputText = Main.inputText;
        Main.inputText = Keyboard.GetState();

        WritingText &= Main.hasFocus;

        if (!WritingText)
            return InputCancellationType.None;

        Main.instance.HandleIME();

        blacklistedChars ??= [];
        blacklistedChars = blacklistedChars.Concat(invalidChars);

        // Arrow key movement
        {
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
                CursorPositon--;
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
                CursorPositon++;
            }
        }

        CursorPositon = Math.Clamp(CursorPositon, 0, output.Length);

        // Special actions
        {
            bool controlPressed =
                (Keys.LeftControl.Pressed ||
                Keys.RightControl.Pressed) &&
                !Keys.LeftAlt.Pressed &&
                !Keys.RightAlt.Pressed;

            bool shiftPressed =
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
                string paste = RemoveInvalidCharacters(GetPaste(output, allowLineBreaks));

                output = output.Insert(CursorPositon, paste);

                CursorPositon += paste.Length;
            }
        }

        // Input
        {
            if (keyStroke.Length >= 1)
            {
                string stroke = RemoveInvalidCharacters(keyStroke.ToString());

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

        string RemoveInvalidCharacters(string input)
        {
            foreach (char c in blacklistedChars)
            {
                input = input.Replace(c.ToString(), string.Empty);
            }

            if (whitelistedChars is not null &&
                whitelistedChars.Any())
            {
                input = new(input.Where(c => whitelistedChars.Contains(c)).ToArray());
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
        Vector2 mousePosition,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        out int hoveredChar,
        bool drawBlinker = false,
        int blinkerIndex = -1) =>
        spriteBatch.DrawInputStringWithShadow(mousePosition, font, text, position, color, Color.Black, rotation, origin, scale, out hoveredChar, drawBlinker, blinkerIndex, -1f);

    public static void DrawInputStringWithShadow(
        this SpriteBatch spriteBatch,
        Vector2 mousePosition,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        out int hoveredChar,
        bool drawBlinker = false,
        int blinkerIndex = -1,
        float spread = 2f) =>
        spriteBatch.DrawInputStringWithShadow(mousePosition, font, text, position, color, Color.Black, rotation, origin, scale, out hoveredChar, drawBlinker, blinkerIndex, spread);

    public static void DrawInputStringWithShadow(
        this SpriteBatch spriteBatch,
        Vector2 mousePosition,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        Color shadowColor,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        out int hoveredChar,
        bool drawBlinker = false,
        int blinkerIndex = -1,
        float spread = 2f)
    {
        // Mirrors the matrix created by DynamicSpriteFont.InternalDraw.
        Matrix matrix = Matrix.CreateTranslation((0f - origin.X) * scale.X, (0f - origin.Y) * scale.Y, 0f) * Matrix.CreateRotationZ(rotation);

        spriteBatch.DrawStringWithShadow(font, text, position, color, shadowColor, rotation, origin, scale, spread);

        if (drawBlinker &&
            blinkerIndex != -1 &&
            Main.GlobalTimeWrappedHourly % .666f > .333f)
        {
            blinkerIndex = Math.Min(blinkerIndex, text.Length);

            float blinkerX = font.MeasureString(text[..blinkerIndex]).X;

            Vector2 blinkerPosition = position + Vector2.Transform(new(blinkerX, 0), matrix);

            spriteBatch.DrawStringWithShadow(font, "|", blinkerPosition, color, shadowColor, rotation, Vector2.Zero, scale, spread);
        }

        // Find hovered char
        {
            mousePosition -= position;
            mousePosition = Vector2.Transform(mousePosition, matrix);

            bool first = true;
            float lastKerning = 0f;

            float totalWidth = -origin.X * 2;

            hoveredChar = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                Vector2 charSize = font.MeasureChar(c, first, scale, lastKerning, out lastKerning);

                if (mousePosition.X >= totalWidth && mousePosition.X <= totalWidth + charSize.X)
                {
                    hoveredChar = mousePosition.X >= totalWidth + (charSize.X * .5f) ? i + 1 : i;
                }

                totalWidth += charSize.X;
                first = false;
            }

            if (mousePosition.X >= totalWidth)
            {
                hoveredChar = text.Length;
            }
        }
    }

    // Notably we don't use ChatManager to avoid allowing the player to type chat tags.
    private static void DrawStringWithShadow(this SpriteBatch spriteBatch,
        DynamicSpriteFont font,
        string text,
        Vector2 position,
        Color color,
        Color shadowColor,
        float rotation,
        Vector2 origin,
        Vector2 scale,
        float spread = 2f)
    {
        if (spread > 0f)
        {
            for (int i = 0; i < ChatManager.ShadowDirections.Length; i++)
                spriteBatch.DrawString(font, text, position + ChatManager.ShadowDirections[i] * spread, shadowColor, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, 0f);
    }

    private static Vector2 MeasureChar(this DynamicSpriteFont font, char c, bool firstChar, Vector2 scale, float lastKerning, out float kerningZ)
    {
        Vector2 output = Vector2.Zero;
        output.Y = font.LineSpacing;

        SpriteCharacterData characterData = font.GetCharacterData(c);
        Vector3 kerning = characterData.Kerning;

        if (firstChar)
            kerning.X = Math.Max(kerning.X, 0f);
        else
            output.X += font.CharacterSpacing + lastKerning;

        output.X += kerning.X + kerning.Y;

        output.Y = Math.Max(output.Y, characterData.Padding.Height);

        output.X += Math.Max(kerning.Z, 0f);

        output *= scale;

        kerningZ = kerning.Z;

        kerningZ *= scale.X;

        return output;
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

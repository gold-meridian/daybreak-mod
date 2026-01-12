using Daybreak.Common.Rendering;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.UI;

/// <summary>
///     An interface panel accepting complex text input.
/// </summary>
public class InputField : UIPanel
{
    /// <summary>
    ///     The scale of the text in the field.
    /// </summary>
    public float TextScale { get; set; } = 1f;

    /// <summary>
    ///     The text within the field.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    ///     The hint to display when there is no text.
    /// </summary>
    public Func<string> Hint { get; }

    /// <summary>
    ///     The maximum number of characters allowed to be entered.
    /// </summary>
    public int MaxChars { get; set; } = 100;

    /// <summary>
    ///     Any characters which may not be entered into the field.
    /// </summary>
    public HashSet<char> BlacklistedChars { get; } = [];

    /// <summary>
    ///     If populated, only these characters will be allowed to be entered in
    ///     the field.  Any characters that are also in
    ///     <see cref="BlacklistedChars"/> will still be ignored.
    /// </summary>
    public HashSet<char> WhitelistedChars { get; } = [];

    /// <summary>
    ///     The horizontal alignment of text within the field, represented as a
    ///     [0,1] float serving as a percentage.
    /// </summary>
    public float TextAlignX { get; set; }

    /// <summary>
    ///     Ran on the frame that this field begins capturing input.
    /// </summary>
    public event Action<InputField>? OnEnter;

    /// <summary>
    ///     Ran of the frame that this field stops capturing input.
    /// </summary>
    public event Action<InputField>? OnEscape;

    /// <summary>
    ///     Ran every time the text is modified.
    /// </summary>
    public event Action<InputField>? OnTextChanged;

    private int mousePosition;
    private bool currentlyWriting;

    // The text from before writing started.
    private string? lastText;
    private string? oldText;

    /// <summary>
    ///     Initializes this input field with some default styling.
    /// </summary>
    public InputField(Func<string> hint)
    {
        Hint = hint;

        _backgroundTexture = AssetReferences.Assets.Images.UI.EmptyPanel.Asset;
        BorderColor = Color.Transparent;

        Width.Set(0f, 1f);
        Height.Set(30f, 0f);

        SetPadding(0f);

        PaddingLeft = 6f;
        PaddingRight = 6f;
    }

    /// <inheritdoc />
    public InputField(string hint) : this(() => hint) { }

    /// <inheritdoc />
    public InputField(LocalizedText text) : this(() => text.Value) { }

    /// <inheritdoc />
    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);

        if (evt.Target != this || !this.InnerDimensions.Contains(evt.MousePosition.ToPoint()))
        {
            currentlyWriting = false;
            return;
        }

        InputHelpers.SyncBlinkerStartTime();
        currentlyWriting = true;
        lastText = Text;

        InputHelpers.CursorPositon = 0;

        if (Text.Length <= 0)
        {
            return;
        }

        InputHelpers.CursorPositon = mousePosition;
    }

    /// <inheritdoc />
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Text != oldText)
        {
            OnTextChanged?.Invoke(this);
        }

        oldText = Text;

        var cap = MaxChars;
        if (Text.Length > cap)
        {
            Text = Text[..cap];

            mousePosition = Math.Min(mousePosition, cap);
        }

        var clickedOff = !Main.hasFocus || (Main.mouseLeft && !IsMouseHovering);
        if (!clickedOff || !currentlyWriting)
        {
            return;
        }

        OnEnter?.Invoke(this);
        currentlyWriting = false;
    }

    private void HandleInput()
    {
        InputHelpers.WritingText = true;

        var cancellationType = InputHelpers.GetInput(
            Text,
            out var newText,
            false,
            BlacklistedChars,
            WhitelistedChars
        );
        switch (cancellationType)
        {
            case InputCancellationType.Confirmed:
                Text = newText;
                OnEnter?.Invoke(this);
                currentlyWriting = false;
                break;

            case InputCancellationType.Escaped:
                Text = lastText ?? string.Empty;
                OnEscape?.Invoke(this);
                currentlyWriting = false;
                break;

            case InputCancellationType.None:
            default:
                Text = newText;
                break;
        }
    }

    /// <inheritdoc />
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        spriteBatch.End(out var ss);

        var oldScissor = spriteBatch.GraphicsDevice.ScissorRectangle;
        spriteBatch.GraphicsDevice.ScissorRectangle = GetClippingRectangle(spriteBatch);

        var dims = this.InnerDimensions;
        var cursorMargin = 5f * TextScale;
        {
            dims.Width -= (int)cursorMargin;
        }

        spriteBatch.Begin(ss with { RasterizerState = OverflowHiddenRasterizerState });
        {
            var hint = Hint();

            var font = FontAssets.MouseText.Value;
            var position = new Vector2(dims.X + dims.Width * TextAlignX + 2f, dims.Y + dims.Height * 0.5f + 4);
            var textSize = font.MeasureString(Text == string.Empty ? hint : Text);
            var origin = new Vector2(textSize.X * TextAlignX, textSize.Y * 0.5f);

            if (Text == string.Empty)
            {
                ChatManager.DrawColorCodedStringWithShadow(
                    spriteBatch,
                    font,
                    hint,
                    position,
                    Color.Gray,
                    0f,
                    origin,
                    new Vector2(TextScale)
                );
            }

            var cursorIndex = Math.Min(InputHelpers.CursorPositon, Text.Length);

            // Move the text position based on the cursor when text is out
            // of the frame.
            if (currentlyWriting && textSize.X >= dims.Width)
            {
                var cursorPosition = font.MeasureString(Text[..cursorIndex]).X;
                {
                    cursorPosition -= origin.X;
                }

                var offset = cursorPosition * TextScale;

                // Each half of the text seperated by the alignment.
                var width = Math.Sign(offset) <= 0
                    ? (dims.Width * TextAlignX)
                    : (dims.Width * (1f - TextAlignX));

                offset = Utils.Remap(Math.Abs(offset), width, textSize.X, 0f, textSize.X - width) * Math.Sign(offset);
                {
                    position.X -= offset;
                }
            }

            spriteBatch.DrawInputStringWithShadow(
                UserInterface.ActiveInstance.MousePosition,
                font,
                Text,
                position,
                Color.White,
                0f,
                origin,
                new Vector2(TextScale),
                out mousePosition,
                currentlyWriting,
                cursorIndex
            );
        }
        spriteBatch.End();

        spriteBatch.GraphicsDevice.ScissorRectangle = oldScissor;

        spriteBatch.Begin(in ss);

        if (currentlyWriting)
        {
            HandleInput();
        }
    }
}

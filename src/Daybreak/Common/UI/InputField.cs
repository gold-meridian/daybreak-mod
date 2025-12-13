using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.UI;

public class InputField : UIPanel
{
    private readonly float textScale;

    protected int mousePosition;

    protected bool writing;

    // The text from before writing started.
    private string lastText = string.Empty;

    private string oldText;

    public string Text;

    public string Hint;

    public int MaxChars;

    public char[] BlacklistedChars = [];

    public char[] WhitelistedChars = [];

    public float TextAlignX;

    public event Action<InputField>? OnEnter;

    public event Action<InputField>? OnTextChanged;

    public event Action<InputField>? OnEscape;

    public InputField(string hint, int maxChars, float textScale = 1f)
    {
        Text = string.Empty;
        Hint = hint;
        MaxChars = maxChars;

        this.textScale = textScale;

        _backgroundTexture = AssetReferences.Assets.Images.UI.EmptyPanel.Asset;
        BorderColor = Color.Transparent;

        Width.Set(0f, 1f);
        Height.Set(30f, 0f);

        SetPadding(0f);

        PaddingLeft = 6f;
        PaddingRight = 16f;
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);

        if (evt.Target != this ||
            !this.InnerDimensions.Contains(evt.MousePosition.ToPoint()))
        {
            writing = false;
            return;
        }

        writing = true;
        lastText = Text;

        InputHelpers.CursorPositon = 0;

        if (Text.Length <= 0)
            return;

        InputHelpers.CursorPositon = mousePosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Text != oldText)
        {
            OnTextChanged?.Invoke(this);
        }

        oldText = Text;

        int cap = MaxChars;

        DynamicSpriteFont font = FontAssets.MouseText.Value;

        Vector2 textSize = font.MeasureString(Text == string.Empty ? Hint : Text) * new Vector2(textScale);

        if (textSize.X >= this.InnerDimensions.Width)
        {
            cap = Text.Length - 1;
        }

        if (Text.Length > cap)
        {
            Text = Text[..cap];
        }

        bool clickedOff =
            !Main.hasFocus ||
            (Main.mouseLeft &&
            !IsMouseHovering);

        if (clickedOff && writing)
        {
            OnEnter?.Invoke(this);
            writing = false;
        }
    }

    private void HandleInput()
    {
        InputHelpers.WritingText = true;

        switch (InputHelpers.GetInput(Text, out string newText, false, BlacklistedChars, WhitelistedChars))
        {
            case InputCancellationType.Confirmed:
                Text = newText;
                OnEnter?.Invoke(this);
                writing = false;
                break;

            case InputCancellationType.Escaped:
                Text = lastText;
                OnEscape?.Invoke(this);
                writing = false;
                break;

            default:
                Text = newText;
                break;
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        DynamicSpriteFont font = FontAssets.MouseText.Value;

        Rectangle dims = this.InnerDimensions;

        Vector2 position = new(dims.X + (dims.Width * TextAlignX), dims.Y + (dims.Height * .5f) + 4);

        Vector2 textSize = font.MeasureString(Text == string.Empty ? Hint : Text);
        Vector2 origin = new(textSize.X * TextAlignX, textSize.Y * .5f);

        bool drawBlinker = writing && Main.GlobalTimeWrappedHourly % .666f > .333f;

        if (Text == string.Empty)
        {
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, Hint, position, Color.Gray, 0f, origin, new(textScale));
        }

        spriteBatch.DrawInputStringWithShadow(UserInterface.ActiveInstance.MousePosition, font, Text, position, Color.White, origin, new(textScale), out mousePosition, drawBlinker, InputHelpers.CursorPositon);

        if (writing)
        {
            HandleInput();
        }
    }
}

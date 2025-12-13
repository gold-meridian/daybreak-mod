using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrettyRarities.Core;
using ReLogic.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.UI;

public class InputField : UIPanel
{
    protected int mousePosition;

    protected bool writing;

    public string OldText = string.Empty;

    public string Text;

    public string Hint;

    public int MaxChars;

    public char[] BlacklistedChars = [];

    public char[] WhitelistedChars = [];

    public float TextAlignX;

    public event Action<InputField>? OnEnter;

    public event Action<InputField>? OnEscape;

    public InputField(string hint, int maxChars)
    {
        Text = string.Empty;
        Hint = hint;
        MaxChars = maxChars;

        _backgroundTexture = AssetReferences.Assets.Images.UI.EmptyPanel.Asset;
        BorderColor = Color.Transparent;

        Width.Set(0f, 1f);
        Height.Set(26f, 0f);

        SetPadding(6);
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);

        if (evt.Target != this)
        {
            writing = false;
            return;
        }

        writing = true;
        OldText = Text;

        InputHelpers.CursorPositon = 0;

        if (Text.Length <= 0)
            return;

        InputHelpers.CursorPositon = mousePosition;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Text.Length > MaxChars)
        {
            Text = Text[..MaxChars];
        }

        bool clickedOff =
            !Main.hasFocus ||
            (Main.mouseLeft &&
            !IsMouseHovering);

        if (!clickedOff || !writing)
        {
            return;
        }

        // Should clicking off the panel count as confirming?
        // OnEnter?.Invoke(this);
        writing = false;
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
                Text = OldText;
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
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, Hint, position, Color.Gray, 0f, origin, Vector2.One);
        }

        spriteBatch.DrawInputStringWithShadow(UserInterface.ActiveInstance.MousePosition, font, Text, position, Color.White, origin, Vector2.One, out mousePosition, drawBlinker, InputHelpers.CursorPositon);

        if (writing)
        {
            HandleInput();
        }
    }
}

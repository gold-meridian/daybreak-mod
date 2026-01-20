using Daybreak.Common.Features.Hooks;
using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoMod.Cil;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Common.UI;

public class Slider : UIElement
{
    public Asset<Texture2D> InnerTexture { get; set; }

    public Asset<Texture2D> BlipTexture { get; set; }

    public Color InnerColor { get; set; }

    public float Ratio { get; set; }

    public event Action<Slider>? OnChanged;

    protected bool IsHeld;

#region Mouse Movement Edit

    private const float slow_cursor_speed = 0.15f;

    protected static bool SlowCursor;

    [OnLoad]
    private static void Load()
    {
        IL_PlayerInput.MouseInput += DoUpdate_HandleInput_SlowCursor;
    }

    private static void DoUpdate_HandleInput_SlowCursor(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.After,
            i => i.MatchCall(typeof(Mouse), nameof(Mouse.GetState)),
            i => i.MatchStsfld<PlayerInput>(nameof(PlayerInput.MouseInfo))
        );

        c.EmitDelegate(
            () =>
            {
                if (SlowCursor)
                {
                    Mouse.SetPosition(
                        PlayerInput.MouseInfoOld.X + (int)((PlayerInput.MouseInfo.X - PlayerInput.MouseInfoOld.X) * slow_cursor_speed),
                        PlayerInput.MouseInfoOld.Y + (int)((PlayerInput.MouseInfo.Y - PlayerInput.MouseInfoOld.Y) * slow_cursor_speed)
                    );

                    PlayerInput.MouseInfo = Mouse.GetState();
                }

                SlowCursor = false;
            }
        );
    }

#endregion

    public Slider()
    {
        Width.Set(0, 1f);
        Height.Set(16, 0f);

        InnerColor = Color.White;

        InnerTexture = AssetReferences.Assets.Images.UI.Gradient.Asset;
        BlipTexture = TextureAssets.ColorSlider;
    }

    public override void LeftMouseDown(UIMouseEvent evt)
    {
        base.LeftMouseDown(evt);

        if (evt.Target == this)
        {
            IsHeld = true;
        }
    }

    public override void LeftMouseUp(UIMouseEvent evt)
    {
        base.LeftMouseUp(evt);

        IsHeld = false;
    }

    public override void MouseOver(UIMouseEvent evt)
    {
        base.MouseOver(evt);

        IsMouseHovering &= !Main.mouseLeft || IsHeld;

        if (!IsMouseHovering || IsHeld)
        {
            return;
        }

        SoundEngine.PlaySound(SoundID.MenuTick);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Rectangle dims = this.Dimensions;

        if (IsHeld)
        {
            float oldRatio = Ratio;

            float num = Main.mouseX - dims.X;
            Ratio = Math.Clamp(num / dims.Width, 0f, 1f);

            if (oldRatio != Ratio)
            {
                OnChanged?.Invoke(this);
            }

            if (PlayerInput.Triggers.Current.SmartCursor || Main.keyState.IsKeyDown(Keys.LeftShift))
            {
                const float speed = 7f;

                SlowCursor = true;
            }
        }
        else if (!IsMouseHovering && ContainsPoint(Main.MouseScreen) && !Main.mouseLeft)
        {
            IsMouseHovering = true;
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        Rectangle dims = this.Dimensions;

        Texture2D slider = AssetReferences.Assets.Images.UI.Slider.Asset.Value;
        Texture2D sliderOutline = AssetReferences.Assets.Images.UI.SliderHighlight.Asset.Value;

        DrawBar(slider, Color.White);

        if (IsHeld || IsMouseHovering)
        {
            DrawBar(sliderOutline, Main.OurFavoriteColor);
        }

        dims.Inflate(-4, -4);
        spriteBatch.Draw(InnerTexture.Value, dims, InnerColor);

        Texture2D blip = BlipTexture.Value;

        Vector2 blipOrigin = blip.Size() * .5f;
        Vector2 blipPosition = new(dims.X + Ratio * dims.Width, dims.Center.Y);

        spriteBatch.Draw(blip, blipPosition, null, Color.White, 0f, blipOrigin, 1f, 0, 0f);

        return;

        void DrawBar(Texture2D texture, Color color)
        {
            spriteBatch.Draw(texture, new Rectangle(dims.X, dims.Y, 6, dims.Height), new(0, 0, 6, texture.Height), color);
            spriteBatch.Draw(texture, new Rectangle(dims.X + 6, dims.Y, dims.Width - 12, dims.Height), new(6, 0, 2, texture.Height), color);
            spriteBatch.Draw(texture, new Rectangle(dims.X + dims.Width - 6, dims.Y, 6, dims.Height), new(8, 0, 6, texture.Height), color);
        }
    }
}

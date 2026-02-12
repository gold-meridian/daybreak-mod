using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Mathematics;
using Daybreak.Common.Rendering;
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

    public bool Vertical { get; init; }

    /// <summary>
    /// Used over IsMouseHovering to determine if the mouse is hovering the slider and NOT draging another.
    /// </summary>
    public bool Hovering { get; private set; }

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

    public Slider(bool vertical = false)
    {
        Vertical = vertical;

        if (vertical)
        {
            Width.Set(16f, 0f);
            Height.Set(0, 1f);
        }
        else
        {
            Width.Set(0, 1f);
            Height.Set(16, 0f);
        }

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

        Hovering = IsMouseHovering && (!Main.mouseLeft || IsHeld);

        if (!Hovering || IsHeld)
        {
            return;
        }

        SoundEngine.PlaySound(SoundID.MenuTick);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Rectangle dims = this.Dimensions;

        var mousePosition = UserInterface.ActiveInstance.MousePosition;

        if (IsHeld)
        {
            float oldRatio = Ratio;

            float num =
                Vertical
                ? mousePosition.Y - dims.Y
                : mousePosition.X - dims.X;

            Ratio = Math.Clamp(num / (Vertical ? dims.Height : dims.Width), 0f, 1f);

            if (oldRatio != Ratio)
            {
                OnChanged?.Invoke(this);
            }

            if (PlayerInput.Triggers.Current.SmartCursor || Main.keyState.IsKeyDown(Keys.LeftShift))
            {
                SlowCursor = true;
            }
        }
        else if (!Hovering && IsMouseHovering && !Main.mouseLeft)
        {
            Hovering = true;
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

        if (Hovering || IsHeld)
        {
            DrawBar(sliderOutline, Main.OurFavoriteColor);
        }

        dims.Inflate(-4, -4);
        spriteBatch.Draw(InnerTexture.Value, dims, InnerColor);

        Texture2D blip = BlipTexture.Value;

        Vector2 blipOrigin = blip.Size() * 0.5f;
        Vector2 blipPosition =
            Vertical
            ? new Vector2(dims.Center.X, dims.Y + Ratio * dims.Height)
            : new Vector2(dims.X + Ratio * dims.Width, dims.Center.Y);

        spriteBatch.Draw(blip, blipPosition, null, Color.White, 0f, blipOrigin, 1f, 0, 0f);

        return;

        void DrawBar(Texture2D texture, Color color)
        {
            var rotation = Vertical ? Angle.FromRadians(MathHelper.PiOver2) : Angle.Zero;

            var startDest = new Rectangle(
                dims.X,
                dims.Y,
                Vertical ? dims.Width : 6,
                Vertical ? 6 : dims.Height
            );

            var centerDest = new Rectangle(
                dims.X + (Vertical ? 0 : 6),
                dims.Y + (Vertical ? 6 : 0),
                dims.Width - (Vertical ? 0 : 12),
                dims.Height - (Vertical ? 12 : 0)
            );

            var endDest = new Rectangle(
                dims.X + (Vertical ? 0 : (dims.Width - 6)),
                dims.Y + (Vertical ? (dims.Height- 6) : 0),
                Vertical ? dims.Width : 6,
                Vertical ? 6 : dims.Height
            );

            spriteBatch.Draw(
                new DrawParameters(texture)
                {
                    Source = new(0, 0, 6, texture.Height),
                    Destination = startDest,
                    Rotation = rotation,
                    Color = color
                }
            );
            spriteBatch.Draw(
                new DrawParameters(texture)
                {
                    Source = new(6, 0, 2, texture.Height),
                    Destination = centerDest,
                    Rotation = rotation,
                    Color = color
                }
            );
            spriteBatch.Draw(
                new DrawParameters(texture)
                {
                    Source = new(8, 0, 6, texture.Height),
                    Destination = endDest,
                    Rotation = rotation,
                    Color = color
                }
            );
        }
    }
}

using System;
using System.Diagnostics;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Rendering;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     Responsible for capturing the UI.
/// </summary>
public sealed class InterfaceCapturer : ModSystem
{
    private static bool openSettingsWithEsc;
    private static RenderTargetLease? rtLease;
    private static RenderTargetScope? rtScope;

    private static bool blockInventory;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        Main.RunOnMainThread(
            () =>
            {
                rtLease = ScreenspaceTargetPool.Shared.Rent(Main.instance.GraphicsDevice);

                IL_Main.DoDraw += DoDraw_CaptureUserInterfaces;
                On_Main.DoUpdate_WhilePaused += BlockMenuInput;
            }
        );
    }

    private static void BlockMenuInput(On_Main.orig_DoUpdate_WhilePaused orig)
    {
        var blockInputOrig = Main.blockInput;
        Main.blockInput |= blockInventory;
        orig();
        Main.blockInput = blockInputOrig;
    }

    [ModSystemHooks.PostUpdatePlayers]
    private static void BlockPlayerInput()
    {
        if (!openSettingsWithEsc)
        {
            return;
        }

        blockInventory = true;
    }

    [ModSystemHooks.UpdateUI]
    private static void OpenInventoryIfExpected(GameTime gameTime)
    {
        if (!openSettingsWithEsc)
        {
            blockInventory = false;
            return;
        }

        if (PlayerInput.Triggers.JustPressed.Inventory)
        {
            if (Main.ingameOptionsWindow)
            {
                Main.ingameOptionsWindow = false;
                IngameOptions.Close();
            }
            else
            {
                Main.ingameOptionsWindow = true;
                IngameOptions.Open();   
            }
        }
    }

    private static void DoDraw_CaptureUserInterfaces(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(x => x.MatchLdsfld<Main>(nameof(Main.hideUI)));
        c.GotoNext(MoveType.Before, x => x.MatchLdsfld<Main>(nameof(Main.spriteBatch)));

        // initialize and apply scope
        c.EmitDelegate(
            static () =>
            {
                if (!UserInterfaceModifier.ShouldCaptureThisFrame)
                {
                    return;
                }

                Debug.Assert(rtLease is not null);

                rtScope = rtLease.Scope(clearColor: Color.Transparent);
            }
        );

        c.GotoNext(x => x.MatchCall<Main>(nameof(Main.DrawInterface)));
        c.GotoNext(MoveType.Before, x => x.MatchCall(typeof(TimeLogger), nameof(TimeLogger.DetailedDrawTime)));

        // unapply scope
        c.EmitDelegate(
            static () =>
            {
                try
                {
                    if (!rtScope.HasValue)
                    {
                        openSettingsWithEsc = false;
                        return;
                    }

                    rtScope.Value.Dispose();
                    rtScope = null;

                    Debug.Assert(rtLease is not null);

                    var uiInfo = new UserInterfaceInfo(Vector2.Zero, rtLease.Target);
                    UserInterfaceModifier.ApplyTo(ref uiInfo);

                    openSettingsWithEsc = uiInfo.InventoryButtonOpensSettings;

                    Main.spriteBatch.Begin();
                    Main.spriteBatch.Draw(
                        uiInfo.Texture,
                        uiInfo.Position,
                        null,
                        uiInfo.Color,
                        uiInfo.Rotation,
                        Vector2.Zero,
                        uiInfo.Scale,
                        uiInfo.SpriteEffects,
                        0f
                    );
                    Main.spriteBatch.End();
                }
                finally
                {
                    foreach (var layer in UserInterfaceModifier.PostDrawLayers)
                    {
                        if (!layer.Draw())
                        {
                            break;
                        }
                    }

                    UserInterfaceModifier.PostDrawLayers.Clear();
                }
            }
        );
    }

    /// <summary>
    ///     Pauses the capture, rendering it to the screen rather than the UI
    ///     target.
    /// </summary>
    [Obsolete("This API is still not finalized and may not be kept, use at your own risk")]
    public static void PauseCapture()
    {
        if (!rtScope.HasValue)
        {
            return;
        }

        Main.spriteBatch.End(out var ss);

        rtScope.Value.Dispose();

        Main.spriteBatch.Begin(ss);
    }

    /// <summary>
    ///     Resumes capture.
    /// </summary>
    [Obsolete("This API is still not finalized and may not be kept, use at your own risk")]
    public static void ResumeCapture()
    {
        if (rtLease is null)
        {
            return;
        }

        Main.spriteBatch.End(out var ss);

        rtScope = rtLease.Scope(clearColor: Color.Transparent);

        Main.spriteBatch.Begin(ss);
    }
}

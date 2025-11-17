using System.Diagnostics;
using Daybreak.Common.Rendering;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.InterfaceModifiers;

/// <summary>
///     Responsible for capturing the UI.
/// </summary>
public sealed class InterfaceCapturer : ModSystem
{
    private static RenderTargetLease? rtLease;
    private static RenderTargetScope? rtScope;

    /// <inheritdoc />
    public override void Load()
    {
        base.Load();

        Main.RunOnMainThread(
            () =>
            {
                rtLease = ScreenspaceTargetPool.Shared.Rent(
                    Main.instance.GraphicsDevice,
                    (width, height, _, _) => (width, height),
                    RenderTargetDescriptor.Default
                );
            }
        );

        IL_Main.DoDraw += DoDraw_CaptureUserInterfaces;
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
                if (!rtScope.HasValue)
                {
                    return;
                }

                rtScope.Value.Dispose();
                rtScope = null;

                Debug.Assert(rtLease is not null);

                var uiInfo = new UserInterfaceInfo(Vector2.Zero, rtLease.Target);
                UserInterfaceModifier.ApplyTo(ref uiInfo);

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
        );
    }

    /// <summary>
    ///     Pauses the capture, rendering it to the screen rather than the UI
    ///     target.
    /// </summary>
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

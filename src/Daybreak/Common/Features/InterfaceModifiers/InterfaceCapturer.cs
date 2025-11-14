using System.Diagnostics;
using Daybreak.Common.CIL;
using Daybreak.Common.Rendering;
using Microsoft.Xna.Framework;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.InterfaceModifiers;

internal sealed class InterfaceCapturer : ModSystem
{
    private static RenderTargetLease? rtLease;

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

        var rtScopeAppliedVar = c.AddVariable<bool>();
        var rtScopeVar = c.AddVariable<RenderTargetScope>();

        c.GotoNext(x => x.MatchLdsfld<Main>(nameof(Main.hideUI)));
        c.GotoNext(MoveType.Before, x => x.MatchLdsfld<Main>(nameof(Main.spriteBatch)));

        // initialize and apply scope
        c.EmitLdloca(rtScopeAppliedVar);
        c.EmitDelegate(
            static (ref bool scopeApplied) =>
            {
                if (!UserInterfaceModifier.ShouldCaptureThisFrame)
                {
                    return default(RenderTargetScope);
                }

                Debug.Assert(rtLease is not null);

                scopeApplied = true;
                return new RenderTargetScope(
                    Main.instance.GraphicsDevice,
                    rtLease.Target,
                    preserveContents: true,
                    clear: true,
                    clearColor: Color.Transparent
                );
            }
        );
        c.EmitStloc(rtScopeVar);

        c.GotoNext(x => x.MatchCall<Main>(nameof(Main.DrawInterface)));
        c.GotoNext(MoveType.Before, x => x.MatchCall(typeof(TimeLogger), nameof(TimeLogger.DetailedDrawTime)));

        // unapply scope
        c.EmitLdloc(rtScopeAppliedVar);
        c.EmitLdloc(rtScopeVar);
        c.EmitDelegate(
            static (bool scopeApplied, RenderTargetScope rtScope) =>
            {
                if (!scopeApplied)
                {
                    return;
                }

                rtScope.Dispose();

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
}

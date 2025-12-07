using Daybreak.Common.Features.Config.Types;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Features.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using System;
using System.Reflection;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;

namespace Daybreak.Common.Features.Config.Elements;

// TODO: Potential generic impl for more than bool wrappers.
[WrappedType<BooleanContainer>]
internal class BooleanContainerElement : BooleanElement
{

#region Highlight Edit

    private static ILHook? patchDrawSelf;

    [OnLoad]
    private static void Load()
    {
        MethodInfo? drawSelf = typeof(ConfigElement).GetMethod("DrawSelf", BindingFlags.NonPublic | BindingFlags.Instance);

        if (drawSelf is not null)
        {
            patchDrawSelf = new(drawSelf, ILDrawSelf);
        }
    }

    [OnUnload]
    private static void Unload()
    {
        patchDrawSelf?.Dispose();
    }

    private static void ILDrawSelf(ILContext il)
    {
        try
        {
            ILCursor c = new(il);

            ILLabel jumpDrawPanel = c.DefineLabel();

            int elementIndex = -1;

            int spriteBatchIndex = -1;

            int colorIndex = -1;

            c.GotoNext(MoveType.After,
                i => i.MatchLdarg(out elementIndex),
                i => i.MatchLdarg(out _),
                i => i.MatchCall<UIElement>("DrawSelf"));

            c.GotoNext(MoveType.After,
                i => i.MatchLdloc(out colorIndex),
                i => i.MatchCall<ConfigElement>(nameof(ConfigElement.DrawPanel2)));

            c.MarkLabel(jumpDrawPanel);

            c.GotoPrev(MoveType.Before,
                i => i.MatchLdarg(out spriteBatchIndex),
                i => i.MatchLdloc(out _),
                i => i.MatchLdsfld(typeof(TextureAssets).FullName!, nameof(TextureAssets.SettingsPanel)));

            c.MoveAfterLabels();

            c.EmitLdarg(elementIndex);

            c.EmitLdarg(spriteBatchIndex);
            c.EmitLdloc(colorIndex);

            c.EmitDelegate((ConfigElement element, SpriteBatch sb, Color color) =>
            {
                if (element is not BooleanContainerElement wrapper)
                    return false;

                Rectangle dims = wrapper.Dimensions;

                DrawStaticHighlightPanel(sb, color, dims);

                return true;
            });

            c.EmitBrtrue(jumpDrawPanel);
        }
        catch (Exception e)
        {
            throw new ILPatchFailureException(ModContent.GetInstance<ModImpl>(), il, e);
        }
    }

    // Draw the panel with the division between the top and bottom half placed at a constant position, this keeps the highlight consistent when the panel is 'expanded.'
    private static void DrawStaticHighlightPanel(SpriteBatch sb, Color color, Rectangle dims)
    {
        const int split = 15;

        Texture2D texture = TextureAssets.SettingsPanel.Value;

        // Left/Right bars.
        sb.Draw(texture, new Rectangle(dims.X, dims.Y + 2, 2, dims.Height - 4), new(0, 2, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + dims.Width - 2, dims.Y + 2, 2, dims.Height - 4), new(0, 2, 1, 1), color);

        // Up/Down bars.
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y, dims.Width - 4, 2), new(2, 0, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + dims.Height - 2, dims.Width - 4, 2), new(2, 0, 1, 1), color);

        // Inner Panel.
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + 2, dims.Width - 4, split - 2), new(2, 2, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + split, dims.Width - 4, dims.Height - split - 2), new(2, 16, 1, 1), color);
    }

#endregion


}

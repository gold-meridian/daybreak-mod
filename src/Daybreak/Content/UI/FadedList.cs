using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Daybreak.Core;
using Luminance.Common.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace Daybreak.Content.UI;

internal class FadedList : UIList
{
    protected override void DrawChildren(SpriteBatch spriteBatch)
    {
        AssetReferences.Assets.Shaders.UI.SlightListFade.Asset.Wait();

        using var rtLease = ScreenspaceTargetPool.Shared.Rent(
            Main.instance.GraphicsDevice,
            RenderTargetDescriptor.DefaultPreserveContents
        );

        spriteBatch.End(out var ss);

        using (rtLease.Scope(preserveContents: true, clearColor: Color.Transparent))
        {
            spriteBatch.Begin(ss);
            base.DrawChildren(spriteBatch);
            spriteBatch.End();
        }
        spriteBatch.Begin(ss with { SortMode = SpriteSortMode.Immediate, RasterizerState = RasterizerState.CullNone, TransformMatrix = Matrix.Identity });

        var dims = this.Dimensions;

        var position = dims.TopLeft().Transform(ss.TransformMatrix);
        var size = dims.BottomRight().Transform(ss.TransformMatrix) - position;

        var fadeShader = AssetReferences.Assets.Shaders.UI.SlightListFade.CreateFadeShader();
        fadeShader.Parameters.uPanelDimensions = new Vector4(position.X, position.Y, size.X, size.Y);
        fadeShader.Parameters.uScreenSize = new Vector2(rtLease.Target.Width, rtLease.Target.Height);
        fadeShader.Apply();

        var rect = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

        spriteBatch.Draw(rtLease.Target, rect, rect, Color.White);
        spriteBatch.Restart(ss);
    }
}

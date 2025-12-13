using Daybreak.Common.Rendering;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace Daybreak.Common.UI;

public class FadedList : UIList
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

        spriteBatch.Begin(ss with { SortMode = SpriteSortMode.Immediate });

        var fadeShader = AssetReferences.Assets.Shaders.UI.SlightListFade.CreateFadeShader();
        fadeShader.Parameters.uPanelDimensions = new Vector4(_dimensions.X, _dimensions.Y, _dimensions.Width, _dimensions.Height);
        fadeShader.Parameters.uScreenSize = new Vector2(rtLease.Target.Width, rtLease.Target.Height);
        fadeShader.Apply();

        spriteBatch.Draw(rtLease.Target, _dimensions.Position(), _dimensions.ToRectangle(), Color.White);
        spriteBatch.Restart(ss);
    }
}

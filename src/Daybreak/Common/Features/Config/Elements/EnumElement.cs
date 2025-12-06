using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.Config.Elements;

public class EnumElement<T> : ConfigElement<T> where T : struct
{
    private string[]? enumNames;

    public override void OnBind()
    {
        base.OnBind();

        OnLeftClick += (_, _) => Value = Value.NextEnum();

        OnRightClick += (_, _) => Value = Value.PreviousEnum();

        enumNames = Enum.GetNames(typeof(T));

        for (var i = 0; i < enumNames.Length; i++)
        {
            var enumFieldInfo = MemberInfo.Type.GetField(enumNames[i]);

            if (enumFieldInfo is null)
            {
                continue;
            }

            var name = ConfigManager.GetLocalizedLabel(new(enumFieldInfo));
            enumNames[i] = name;
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        var dims = GetDimensions().ToRectangle();

        var values = (T[])Enum.GetValues(typeof(T));
        var index = Array.IndexOf(values, Value);

        var text = enumNames?[index] ?? string.Empty;

        var font = FontAssets.ItemStack.Value;

        var textSize = font.MeasureString(text);
        var origin = new Vector2(textSize.X, 0);

        var position = new Vector2(dims.X + dims.Width - 8f, dims.Y + 8f);
        var baseScale = new Vector2(0.8f);

        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, position, Color.White, 0f, origin, baseScale);
    }
}

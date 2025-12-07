using System;
using System.Collections.Generic;
using Daybreak.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.TmlConfig;

/// <summary>
///     An alternative enum display provider which renders closer to a boolean
///     and with bidirectional cycling using the mouse buttons.
/// </summary>
public sealed class CyclicalTextEnumAttribute<T>() : CustomModConfigItemAttribute(typeof(CyclicalTextEnumAttribute<T>));

internal sealed class CyclicalTextEnumElement<T> : ConfigElement<T>
    where T : struct, Enum
{
    private readonly List<PropertyFieldWrapper> enumFields = [];
    private readonly T[] values = Enum.GetValues<T>();

    /// <summary>
    ///     Initializes this element while populating localizable names for enum
    ///     values.
    /// </summary>
    public CyclicalTextEnumElement()
    {
        var names = Enum.GetNames(typeof(T));
        foreach (var name in names)
        {
            if (MemberInfo.Type.GetField(name) is not { } enumField)
            {
                continue;
            }

            enumFields.Add(new PropertyFieldWrapper(enumField));
        }
    }

    /// <inheritdoc />
    public override void OnBind()
    {
        base.OnBind();

        OnLeftClick += (_, _) => Value = Value.NextEnum();
        OnRightClick += (_, _) => Value = Value.PreviousEnum();
    }

    /// <inheritdoc />
    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        base.DrawSelf(spriteBatch);

        var valueIdx = Array.IndexOf(values, Value);
        if (valueIdx == -1)
        {
            return;
        }

        var dims = this.Dimensions;
        var text = ConfigManager.GetLocalizedLabel(enumFields[valueIdx]);
        var font = FontAssets.ItemStack.Value;
        var textSize = font.MeasureString(text);
        var origin = new Vector2(textSize.X, 0);
        var position = new Vector2(dims.X + dims.Width - 8f, dims.Y + 8f);
        var baseScale = new Vector2(0.8f);

        ChatManager.DrawColorCodedStringWithShadow(
            spriteBatch,
            font,
            text,
            position,
            Color.White,
            0f,
            origin,
            baseScale
        );
    }
}

using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Mathematics;
using Daybreak.Common.Rendering;
using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.UI;

namespace Daybreak.Common.Features.Configuration;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
internal class DefaultConfigElementForAttribute<T>() : DefaultConfigElementForAttribute(typeof(T));

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
internal class DefaultConfigElementForAttribute(Type type) : Attribute
{
    public Type Type { get; } = type;
}

internal static class DefaultConfigElementLoader
{
    private static readonly Dictionary<Type, Type> types_by_element_type = [];

    [OnLoad]
    private static void Load(Mod mod)
    {
        var loadableTypes = AssemblyManager.GetLoadableTypes(mod.Code)
                                           .Where(x => AutoloadAttribute.GetValue(x).NeedsAutoloading)
                                           .OrderBy(x => x.FullName, StringComparer.InvariantCulture)
                                           .ToArray();

        LoaderUtils.ForEachAndAggregateExceptions(loadableTypes, AddElement);
    }

    private static void AddElement(Type type)
    {
        if (!type.IsAssignableTo(typeof(ConfigElement)))
        {
            return;
        }

        var attributes = type.GetCustomAttributes<DefaultConfigElementForAttribute>(inherit: false);

        foreach (var attribute in attributes)
        {
            types_by_element_type.Add(attribute.Type, type);
        }
    }

    internal static bool GetConfigElement(Type type, IConfigEntry entry, bool showIcon, [NotNullWhen(true)] out ConfigElement? instance)
    {
        instance = null;

        foreach (var item in types_by_element_type)
        {
            if (!type.IsAssignableTo(item.Key))
            {
                continue;
            }

            instance = (ConfigElement)Activator.CreateInstance(item.Value, entry, showIcon)!;
            return true;
        }

        return false;
    }
}

[DefaultConfigElementFor<bool>]
public class BoolElement : ConfigElement<bool>
{
    protected Color BUTTON_COLOR = Color.White * 0.75f;
    protected Color BUTTON_HOVER_COLOR = Color.White;

    protected ToggleImage Toggle;

    protected MarqueeText<LocalizedText> Text;

    public BoolElement(IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        const float upper_height = 30f;

        var container = new UIElement();
        {
            container.Width.Set(0f, 0.2f);
            container.Height.Set(upper_height, 0f);

            container.HAlign = 1f;

            container.OnLeftClick += OnLeftClick_UpdateValue;

            container.OnMouseOver += OnMouseOver_UpdateColors;
            container.OnMouseOut += OnMouseOut_UpdateColors;
        }
        Append(container);

        Toggle = new ToggleImage();
        {
            AssetReferences.Assets.Images.UI.Toggle.Asset.Wait();

            Toggle.Texture = AssetReferences.Assets.Images.UI.Toggle.Asset;

            Toggle.Frame = GetButtonFrame();

            Toggle.Color = BUTTON_COLOR;

            Toggle.HAlign = 1f;

            Toggle.Width.Set(30f, 0f);
            Toggle.Height.Set(30f, 0f);

            Toggle.SetPadding(3f);

            Toggle.IgnoresMouseInteraction = true;
        }
        container.Append(Toggle);

        Text = new MarqueeText<LocalizedText>(GetDisplayText(), this.Label.ScrollSpeed);
        {
            Text.Width.Set(-30f, 1f);
            Text.Height.Set(upper_height, 0f);

            Text.Left.Set(-30, 0f);

            Text.HAlign = 1f;
            Text.VAlign = 0.5f;

            Text.MaxTextScale = 0.9f;

            Text.TextAlignX = 1f;

            Text.TextColor = BUTTON_COLOR;

            Text.PaddingTop = 3f;

            Text.IgnoresMouseInteraction = true;
        }
        container.Append(Text);

        return;

        void OnLeftClick_UpdateValue(UIMouseEvent evt, UIElement listeningElement)
        {
            Value = ConfigValue<bool>.Set(!Value.Value);

            Text?.SetText(GetDisplayText());

            Toggle?.Frame = GetButtonFrame();

            SoundEngine.PlaySound(in SoundID.MenuTick);
        }

        LocalizedText GetDisplayText()
        {
            return Value.Value ? Lang.menu[126] : Lang.menu[124];
        }

        Rectangle? GetButtonFrame()
        {
            return AssetReferences.Assets.Images.UI.Toggle.Asset.Frame(1, 2, 0, Value.Value.ToInt());
        }

        void OnMouseOver_UpdateColors(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle?.Color = BUTTON_HOVER_COLOR;
            Text?.TextColor = BUTTON_HOVER_COLOR;

            SoundEngine.PlaySound(in SoundID.MenuTick);
        }

        void OnMouseOut_UpdateColors(UIMouseEvent evt, UIElement listeningElement)
        {
            Toggle?.Color = BUTTON_COLOR;
            Text?.TextColor = BUTTON_COLOR;
        }
    }

    protected sealed class ToggleImage : UIElement
    {
        public Asset<Texture2D>? Texture { get; set; }

        public Rectangle? Frame { get; set; }

        public Color Color { get; set; }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (Texture is null)
            {
                return;
            }

            var dims = this.InnerDimensions;

            spriteBatch.Draw(
                Texture.Value,
                dims,
                Frame,
                Color
            );
        }
    }
}

[DefaultConfigElementFor<int>]
public class IntElement : RangeElement<int>
{
    public IntElement(IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        Min = -50;
        Max = 100;
    }
}

[DefaultConfigElementFor<float>]
public class FloatElement : RangeElement<float>
{
    public FloatElement(IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        Min = -93.4f;
        Max = 55.432f;
    }
}

public abstract class RangeElement<T> : ConfigElement<T> where T : unmanaged, INumber<T>, IConvertible
{
    protected static readonly char[] NUMBER_CHARACTERS =
        [.. Enumerable.Range('0', '9' + 1).Select(i => (char)i), '-', '.'];

    // TODO: Parse options for Min/Max value.
    protected T Min { get; set; }

    protected T Max { get; set; }

    protected float Ratio
    {
        get
        {
            return (Value.Value - Min).ToSingle(null) / (Max - Min).ToSingle(null);
        }

        set
        {
            T val = T.CreateChecked(value * (Max - Min).ToSingle(null)) + Min;

            val = Utils.Clamp(val, Min, Max);

            Value = ConfigValue<T>.Set(val);

            Slider.Ratio = value;

            Input.Text = string.Empty;
        }
    }

    protected Slider Slider;

    protected InputField Input;

    public RangeElement(IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        const float slider_ratio = 0.65f;

        const float slider_padding = 6f;

        var rightContainer = new UIElement();
        {
            rightContainer.Width.Set(0f, 0.4f);
            rightContainer.Height.Set(30, 0f);

            rightContainer.MinWidth.Set(60f, 0f);

            rightContainer.HAlign = 1f;
        }
        Append(rightContainer);

        Slider = new Slider();
        {
            Slider.Width.Set(-slider_padding, slider_ratio);

            Slider.Left.Set(-slider_padding, 0f);

            Slider.MinWidth.Set(30, 0f);

            Slider.HAlign = 1f;

            Slider.VAlign = 0.5f;

            Slider.OnChanged += Slider_UpdateValue;
        }
        rightContainer.Append(Slider);

        Input = new InputField(() => Value.Value.ToString() ?? string.Empty);
        {
            Input.Width.Set(-slider_padding, 1f - slider_ratio);
            Input.Height.Set(26, 0f);

            Input.MinWidth.Set(30, 0f);

            Input.VAlign = 0.5f;

            Input.TextScale = 0.9f;

            Input.OnTextChanged += Input_ParseText;

            Input.OnEnter += Input_AcceptText;

            Input.WhitelistedChars.UnionWith(NUMBER_CHARACTERS);
        }
        rightContainer.Append(Input);

        void Slider_UpdateValue(Slider obj)
        {
            Ratio = obj.Ratio;
        }

        void Input_ParseText(InputField obj)
        {
            if (!T.TryParse(obj.Text, null, out T value))
            {
                return;
            }

            value = Utils.Clamp(value, Min, Max);

            Value = ConfigValue<T>.Set(value);

            Slider.Ratio = Ratio;
        }

        void Input_AcceptText(InputField obj)
        {
            // Refresh the text to show the correct value.
            obj.Text = Value.Value.ToString() ?? string.Empty;
        }
    }

    public override void OnActivate()
    {
        Slider.Ratio = Ratio;
    }
}

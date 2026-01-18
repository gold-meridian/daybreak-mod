using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Mathematics;
using Daybreak.Common.UI;
using Daybreak.Content.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Terraria;
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

[DefaultConfigElementFor<int>]
public class IntElement : RangeElement<int>
{
    public IntElement(IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        Min = 0;
        Max = 100;
    }
}

[DefaultConfigElementFor<float>]
public class FloatElement : RangeElement<float>
{
    public FloatElement(IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        Min = 0;
        Max = 100;
    }
}

public abstract class RangeElement<T> : ConfigElement<T> where T : unmanaged, INumber<T>, IConvertible
{
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
        const float slider_ratio = 0.75f;

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
            Input.Height.Set(24, 0f);

            Input.MinWidth.Set(30, 0f);

            Input.VAlign = 0.5f;

            Input.TextScale = 0.8f;

            Input.OnTextChanged += Input_ParseText;
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
                obj.Text = string.Empty;
                return;
            }

            value = Utils.Clamp(value, Min, Max);

            // Should not show unclamped value
            obj.Text = value.ToString() ?? string.Empty;

            Value = ConfigValue<T>.Set(value);

            Slider.Ratio = Ratio;
        }
    }
}

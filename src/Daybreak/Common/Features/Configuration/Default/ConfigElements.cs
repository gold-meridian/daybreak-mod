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
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI;
using Terraria.ModLoader.UI.Elements;
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
    private static readonly Dictionary<Type, Type> element_type_by_provided_type = [];

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
            element_type_by_provided_type.Add(attribute.Type, type);
        }
    }

    internal static Type GetConfigElementType<T>()
    {
        if (!element_type_by_provided_type.TryGetValue(typeof(T), out var type))
        {
            return typeof(ConfigElement);
        }

        return type;
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
            container.Width.Set(90f, 0f);
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

            Toggle.IgnoresMouseInteraction = true;
        }
        container.Append(Toggle);

        Text = new MarqueeText<LocalizedText>(GetDisplayText(), Label.ScrollSpeed);
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
        ['0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '.'];

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

public abstract class DropdownConfigElement<T> : ConfigElement<T>
{
    protected const float DROPDOWN_MARGIN = 30f;

    public bool Open { get; protected set; }

    private UIElement container;

    protected UIElement InnerElement;

    protected DropdownIcon DropdownButton;

    private int openProgress;

    public DropdownConfigElement(float height, IConfigEntry entry, bool showIcon) : base(entry, showIcon)
    {
        container = new UIElement();
        {
            container.VAlign = 1f;

            container.Width.Set(0f, 1f);

            container.OverflowHidden = true;
        }
        Append(container);

        var divider = new UIHorizontalSeparator();
        {
            const float margin = 4f;

            divider.Width.Set(-margin * 2f, 1f);

            divider.Left.Set(margin, 0f);

            divider.Height.Set(2f, 0f);
            divider.Color = new Color(85, 88, 159) * 0.75f;
        }
        container.Append(divider);

        InnerElement = new UIElement();
        {
            InnerElement.VAlign = 1f;

            InnerElement.MinHeight.Set(height, 0f);

            InnerElement.Width.Set(0f, 1f);

            InnerElement.SetPadding(4f);

            InnerElement.PaddingTop = 8f;
        }
        container.Append(InnerElement);

        DropdownButton = new DropdownIcon();
        {
            DropdownButton.Width.Set(30f, 0f);
            DropdownButton.Height.Set(30f, 0f);

            DropdownButton.HAlign = 1f;

            DropdownButton.OnLeftClick += (_, _) =>
            {
                Open = !Open;
                SoundEngine.PlaySound(Open ? SoundID.MenuOpen : SoundID.MenuClose);
            };
        }
        Append(DropdownButton);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        const int open_frames = 12;
        {
            openProgress = MathHelper.Clamp(openProgress + Open.ToDirectionInt(), 0, open_frames);

            var eased = Open ? Ease(openProgress / (float)open_frames) : EaseClose(openProgress / (float)open_frames);

            DropdownButton.Rotation = MathHelper.PiOver2 * eased;

            var height = InnerElement.Dimensions.Height * eased;

            container.MinHeight.Set(height, 0f);
            container.MaxHeight.Set(height, 0f);

            Height.Set(DEFAULT_HEIGHT + height, 0f);
        }

        static float Ease(float x)
        {
            // return x < 0.5f ? 4 * x * x * x : 1 - MathF.Pow(-2 * x + 2, 3) / 2;
            return MathF.Sqrt(1 - MathF.Pow(x - 1, 2));
        }

        static float EaseClose(float x)
        {
            return x * x;
        }
    }

    protected sealed class DropdownIcon : UIElement
    {
        public float Rotation { get; set; }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            SoundEngine.PlaySound(SoundID.MenuTick);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            var texture = AssetReferences.Assets.Images.UI.Dropdown.Asset.Value;

            var dims = this.Dimensions;
            var origin = texture.Size() * 0.5f;

            spriteBatch.Draw(
                new DrawParameters(texture)
                {
                    Position = dims.Center(),
                    Rotation = Angle.FromRadians(Rotation),
                    Color = IsMouseHovering ? Color.White : (Color.White * 0.75f),
                    Origin = origin
                }
            );
        }
    }
}

[DefaultConfigElementFor<Color>]
public class ColorElement : DropdownConfigElement<Color>
{
    protected static readonly char[] HEX_CHARACTERS =
        ['#',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f',
        'A', 'B', 'C', 'D', 'E', 'F'];

    protected ColorPicker Picker;

    protected UIPanel ColorPreview;

    protected InputField ColorInput;

    protected bool ShowAlpha;

    public ColorElement(IConfigEntry entry, bool showIcon) : base(0f, entry, showIcon)
    {
        const float slider_margin = 16f + 4f;

        const float picker_size = 170f;

        float pickerWidth = picker_size;
        float pickerHeight = picker_size + slider_margin;

        ShowAlpha = true; // TODO: parse

        if (ShowAlpha)
        {
            pickerHeight += slider_margin;
        }

        Picker = new ColorPicker(ShowAlpha);
        {
            Picker.HAlign = 1f;

            Picker.Width.Set(pickerWidth, 0f);

            Picker.Height.Set(pickerHeight, 0f);

            Picker.Color = Value.Value;

            Picker.OnChanged += OnChanged_UpdateColor;
        }
        InnerElement.Append(Picker);

        InnerElement.MinHeight.Set(pickerHeight + InnerElement.PaddingTop, 0f);

        const float margin = 4f;

        ColorPreview = new UIPanel();
        {
            ColorPreview._backgroundTexture = AssetReferences.Assets.Images.UI.FullPanel.Asset;
            ColorPreview.BorderColor = Color.Transparent;

            ColorPreview.HAlign = 1f;

            ColorPreview.Width.Set(30, 0f);
            ColorPreview.Height.Set(30, 0f);

            ColorPreview.Left.Set(-DROPDOWN_MARGIN, 0f);

            ColorPreview.BackgroundColor = Value.Value;

            ColorPreview.OnLeftClick += OnLeftClick_ShowPopup;
        }
        Append(ColorPreview);

        ColorInput = new InputField(ShowAlpha ? "#RRGGBBAA" : "#RRGGBB");
        {
            ColorInput.HAlign = 1f;

            ColorInput.Top.Set(2f, 0f);
            ColorInput.Height.Set(26f, 0f);

            ColorInput.Width.Set(ShowAlpha ? 135f : 100f, 0f);

            ColorInput.Left.Set(-DROPDOWN_MARGIN - 30f - margin, 0f);

            ColorInput.WhitelistedChars.UnionWith(HEX_CHARACTERS);

            ColorInput.OnTextChanged += Input_ParseText;
            ColorInput.OnEnter += Input_AcceptText;

            ColorInput.MaxChars = ColorInput.Hint().Length;
        }
        Append(ColorInput);

        LabelContainer.Width.Set(ColorInput.Width.Pixels - DROPDOWN_MARGIN - 30f - (margin * 2f), 1f);

        return;

        void OnChanged_UpdateColor(ColorPicker obj)
        {
            ColorPreview?.BackgroundColor = obj.Color.MultiplyRGBA(new(obj.Color.A, obj.Color.A, obj.Color.A, byte.MaxValue));
            Value = ConfigValue<Color>.Set(obj.Color);
        }

        void OnLeftClick_ShowPopup(UIMouseEvent evt, UIElement listeningElement)
        {
            if (Open)
            {
                return;
            }

            var panel = new UIPanel();
            {
                panel._backgroundTexture = AssetReferences.Assets.Images.UI.EmptyPanel.Asset;
                panel.BorderColor = Color.Transparent;

                panel.Width.Set(Picker.Width.Pixels + panel.PaddingLeft + panel.PaddingRight, 0f);
                panel.Height.Set(Picker.Height.Pixels + panel.PaddingTop + panel.PaddingBottom, 0f);
            }

            var picker = new ColorPicker(ShowAlpha);
            {
                picker.Width.Set(0f, 1f);

                picker.Height.Set(0f, 1f);

                picker.Color = Value.Value;

                picker.OnChanged += OnChanged_UpdateColor;
            }
            panel.Append(picker);

            var position = listeningElement.Dimensions.Center();

            PopupLayer?.AppendPopup(panel, position);

            SoundEngine.PlaySound(SoundID.MenuOpen);
        }

        void Input_ParseText(InputField obj)
        {
            if (!TryParseHex(obj.Text, ShowAlpha, out var color))
            {
                return;
            }

            Value = ConfigValue<Color>.Set(color);

            Picker.Color = color;
            ColorPreview?.BackgroundColor = color;
        }

        void Input_AcceptText(InputField obj)
        {
            obj.Text = string.Empty;
        }
    }
    protected static bool TryParseHex(string hexString, bool alpha, out Color color)
    {
        color = default;

        if (hexString.StartsWith('#'))
        {
            hexString = hexString[1..];
        }

        if (uint.TryParse(hexString, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out uint hex))
        {
            uint a = 0xFFu;

            if (alpha && hexString.Length == 8)
            {
                a = hex & 0xFFu;
                hex >>= 8;
            }

            uint r = (hex >> 16) & 0xFFu;
            uint g = (hex >> 8) & 0xFFu;
            uint b = hex & 0xFFu;

            color = new((int)r, (int)g, (int)b, (int)a);

            return true;
        }

        return false;
    }
}

public class EnumElement<T> : DropdownConfigElement<T> where T : struct, Enum
{
    protected Grid Grid;

    protected readonly EnumOption<T>[] Options;

    public EnumElement(IConfigEntry entry, bool showIcon) : base(0f, entry, showIcon)
    {
        const int max_columns = 6;

        var type = Value.Value.GetType();

        var values = Enum.GetValues<T>();

        var names = GetLocalizedNames();

        Grid = new Grid();
        {
            Grid.Width.Set(0f, 1f);
            Grid.Height.Set(0f, 1f);

            Grid.Columns = Math.Min(values.Length, max_columns);
        }
        InnerElement.Append(Grid);

        Options = new EnumOption<T>[values.Length];

        for (int i = 0; i < Options.Length; i++)
        {
            var option = new EnumOption<T>(values[i], names[i]);
            {
                option.OnLeftClick += OnLeftClick_UpdateValue;

                if (values[i].CompareTo(Value.Value) == 0)
                {
                    option.Selected = true;
                }
            }
            Options[i] = option;
        }

        Grid.AddRange(Options);

        return;

        void OnLeftClick_UpdateValue(UIMouseEvent evt, UIElement listeningElement)
        {
            if (listeningElement is not EnumOption<T> option || option.Selected)
            {
                return;
            }

            ResetList();

            option.Selected = true;

            Value = ConfigValue<T>.Set(option.Value);

            SoundEngine.PlaySound(in SoundID.MenuTick);
        }

        void ResetList()
        {
            foreach (var element in Grid)
            {
                if (element is not EnumOption<T> option)
                {
                    continue;
                }

                option.Selected = false;
            }
        }
    }

    public override void Recalculate()
    {
        base.Recalculate();

        InnerElement.MinHeight.Set(Grid.GetTotalHeight() + InnerElement.PaddingTop, 0f);
    }

    protected LocalizedText[] GetLocalizedNames()
    {
        var names = Enum.GetNames<T>();

        var result = new LocalizedText[names.Length];

        for (int i = 0; i < names.Length; i++)
        {
            string tmlConfigKey = ConfigManager.GetConfigKey<LabelKeyAttribute>(typeof(T).GetField(names[i])!, "Label");

            result[i] =
                Language.Exists(tmlConfigKey)
                ? Language.GetText(tmlConfigKey)
                : LanguageHelpers.GetLocalization(
                    Entry.Handle.Mod,
                    Entry.Handle.Name,
                    nameof(ConfigEntry<>),
                    $"{typeof(T).Name}.{names[i]}",
                    () => names[i]
                );
        }

        return result;
    }

    protected sealed class EnumOption<T> : UIAutoScaleTextTextPanel<LocalizedText> where T : struct, Enum
    {
        public T Value;

        public bool Selected
        {
            get;

            set
            {
                _backgroundTexture =
                    value
                    ? AssetReferences.Assets.Images.UI.FullPanel.Asset
                    : AssetReferences.Assets.Images.UI.EmptyPanel.Asset;

                field = value;
            }
        }

        public EnumOption(T value, LocalizedText displayName) : base(displayName)
        {
            Value = value;

            _backgroundTexture = AssetReferences.Assets.Images.UI.EmptyPanel.Asset;
            BorderColor = Color.Transparent;

            BackgroundColor = UICommon.DefaultUIBlue;

            Width.Set(0f, 1f);
            Height.Set(30f, 0f);

            SetPadding(0f);

            UseInnerDimensions = false;
        }

        public override void MouseOver(UIMouseEvent evt)
        {
            base.MouseOver(evt);

            SoundEngine.PlaySound(in SoundID.MenuTick);

            BackgroundColor = UICommon.DefaultUIBlue * 1.2f;
        }

        public override void MouseOut(UIMouseEvent evt)
        {
            base.MouseOut(evt);

            BackgroundColor = UICommon.DefaultUIBlue;
        }
    }
}

using Daybreak.Common.Features.Config.Types;
using Daybreak.Common.Features.Hooks;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using MonoMod.RuntimeDetour;
using Newtonsoft.Json;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Daybreak.Common.Features.TmlConfig;
using Daybreak.Common.UI;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.Config.Elements;

// TODO: Potential generic impl for more than bool containers.
// TODO: Generalized public ExpandableElement impl.
// TODO: Better name for 'containers.'
[WrappedType<BooleanContainer>]
internal class BooleanContainerElement : ConfigElement<BooleanContainer>
{
    private const int defaultHeight = 30;

    private NestedUIList? list;

    public bool Enabled
    {
        get => Value.Enabled;
        set
        {
            if (Locked)
            {
                return;
            }

            Expanded = value;

            Value.Enabled = value;
            Interface.modConfig.SetPendingChanges();
        }
    }

    public bool Locked => Value.Locked;

    public bool Expanded
    {
        get => field;
        set
        {
            if (Expanded == value)
            {
                return;
            }

            field = value;

            if (value)
            {
                OnExpand();
            }
            else
            {
                OnContract();
            }

            Recalculate();
        }
    } = false;

    public bool HoveringTop { get; private set; }

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

    public override void OnBind()
    {
        base.OnBind();

        if (!Locked)
        {
            Expanded = Enabled;
        }
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Rectangle rectangle = this.Dimensions;

        HoveringTop = IsMouseHovering && Main.mouseY < rectangle.Y + defaultHeight;

        if (Locked)
        {
            Expanded = false;
        }
    }

    public override void LeftClick(UIMouseEvent evt)
    {
        if (HoveringTop)
        {
            Enabled = !Enabled;
        }
    }

    protected virtual void OnExpand()
    {
        const float margin = 5f;
        const float horizontalMargin = 10f;

        list = [];

        list.Left.Set(horizontalMargin, 0f);
        list.Top.Set(defaultHeight + margin, 0f);

        list.Width.Set(-(horizontalMargin * 2), 1f);
        list.Height.Set(-(defaultHeight + (margin * 2)), 1f);

        list.ListPadding = 5f;

        Append(list);

        int order = 0;

        var members = GetFieldsAndProperties(Value.GetType(), BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        foreach (PropertyFieldWrapper member in members)
        {
            if (Attribute.IsDefined(member.MemberInfo, typeof(JsonIgnoreAttribute)) &&
                !Attribute.IsDefined(member.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute)))
            {
                continue;
            }

            int top = 0;

            UIModConfig.HandleHeader(list, ref top, ref order, member);

            Tuple<UIElement, UIElement> wrapped = UIModConfig.WrapIt(list, ref top, member, Value, order++);
        }

        list.RecalculateChildren();

        float height = list.GetTotalHeight() + defaultHeight + (margin * 2);

        Height.Set(height, 0f);
        Parent?.Height.Set(height, 0f);
    }

    protected virtual void OnContract()
    {
        RemoveAllChildren();

        Height.Set(defaultHeight, 0f);
        Parent?.Height.Set(defaultHeight, 0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        bool isMouseHovering = IsMouseHovering;
        IsMouseHovering = HoveringTop;

        const float lockedBackgroundMultiplier = 0.4f;

        backgroundColor = Locked ? UICommon.DefaultUIBlue * lockedBackgroundMultiplier : UICommon.DefaultUIBlue;

        base.DrawSelf(spriteBatch);

        IsMouseHovering = isMouseHovering;

        Texture2D texture = AssetReferences.Assets.Images.UI.LockableSettingsToggle.Asset.Value;

        Rectangle dims = this.Dimensions;

        string text = Enabled ? Lang.menu[126].Value : Lang.menu[124].Value;

        DynamicSpriteFont font = FontAssets.ItemStack.Value;

        ChatManager.DrawColorCodedStringWithShadow(spriteBatch, font, text, new Vector2(dims.X + dims.Width - 60f, dims.Y + 8f), Color.White, 0f, Vector2.Zero, new(0.8f));

        Vector2 position = new(dims.X + dims.Width - 28, dims.Y + 4);
        Rectangle rectangle = texture.Frame(2, 2, Enabled.ToInt(), Locked.ToInt());

        spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
    }

    public static IEnumerable<PropertyFieldWrapper> GetFieldsAndProperties(Type type, BindingFlags bindingFlags)
    {
        PropertyInfo[] properties = type.GetProperties(bindingFlags);
        return (from x in type.GetFields(bindingFlags)
                select new PropertyFieldWrapper(x)).Concat(properties.Select(x => new PropertyFieldWrapper(x)));
    }
}

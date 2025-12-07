using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.UI;
using Daybreak.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using Newtonsoft.Json;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.UI;
using Terraria.UI;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.TmlConfig;

/// <summary>
///     Represents a boolean condition for configs.
///     <br />
///     This type should be inherited to form a group of configuration options
///     which are directly dependent on whether an existing feature is enabled
///     (<see cref="Enabled"/>).
///     <br />
///     A <see cref="ModConfig"/> should declare a property of the inheriting
///     type to represent this option group.
/// </summary>
public abstract class ConditionalContainer
{
    /// <summary>
    ///     Whether this container is enabled.  This determines whether the
    ///     state contained within the container is accessible.
    /// </summary>
    public virtual bool Enabled { get; set; }

    /// <summary>
    ///     Whether this container is locked.  A locked container renders its
    ///     state immutable from the user's perspective.
    /// </summary>
    [JsonIgnore]
    public virtual bool Locked => false;
}

[ProvidesConfigElementFor<ConditionalContainer>]
internal sealed class ConditionalContainerElement : ConfigElement<ConditionalContainer>
{
    private const int default_height = 30;

    private NestedUIList list = [];

    private bool Enabled
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

    private bool Locked => Value.Locked;

    public bool Expanded
    {
        get;

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

    private bool HoveringTop { get; set; }

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

        var rectangle = this.Dimensions;

        HoveringTop = IsMouseHovering && Main.mouseY < rectangle.Y + default_height;

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

    private void OnExpand()
    {
        const float margin = 5f;
        const float horizontal_margin = 10f;

        list = [];

        list.Left.Set(horizontal_margin, 0f);
        list.Top.Set(default_height + margin, 0f);

        list.Width.Set(-(horizontal_margin * 2), 1f);
        list.Height.Set(-(default_height + (margin * 2)), 1f);

        list.ListPadding = 5f;

        Append(list);

        var order = 0;

        var members = GetFieldsAndProperties(
            Value.GetType(),
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly
        );

        foreach (var member in members)
        {
            if (
                Attribute.IsDefined(member.MemberInfo, typeof(JsonIgnoreAttribute)) &&
                !Attribute.IsDefined(member.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute)))
            {
                continue;
            }

            var top = 0;
            UIModConfig.HandleHeader(list, ref top, ref order, member);
            _ = UIModConfig.WrapIt(list, ref top, member, Value, order++);
        }

        list.RecalculateChildren();

        var height = list.GetTotalHeight() + default_height + (margin * 2);

        Height.Set(height, 0f);
        Parent?.Height.Set(height, 0f);
    }

    private void OnContract()
    {
        RemoveAllChildren();

        Height.Set(default_height, 0f);
        Parent?.Height.Set(default_height, 0f);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        var isMouseHovering = IsMouseHovering;
        IsMouseHovering = HoveringTop;

        const float locked_background_multiplier = 0.4f;

        backgroundColor = Locked ? UICommon.DefaultUIBlue * locked_background_multiplier : UICommon.DefaultUIBlue;

        base.DrawSelf(spriteBatch);

        IsMouseHovering = isMouseHovering;

        var texture = AssetReferences.Assets.Images.UI.LockableSettingsToggle.Asset.Value;
        var dims = this.Dimensions;
        var text = Enabled ? Lang.menu[126].Value : Lang.menu[124].Value;
        var font = FontAssets.ItemStack.Value;
        ChatManager.DrawColorCodedStringWithShadow(
            spriteBatch,
            font,
            text,
            new Vector2(dims.X + dims.Width - 60f, dims.Y + 8f),
            Color.White,
            0f,
            Vector2.Zero,
            new Vector2(0.8f)
        );

        Vector2 position = new(dims.X + dims.Width - 28, dims.Y + 4);
        var rectangle = texture.Frame(2, 2, Enabled.ToInt(), Locked.ToInt());

        spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
    }

    private static IEnumerable<PropertyFieldWrapper> GetFieldsAndProperties(Type type, BindingFlags bindingFlags)
    {
        var properties = type.GetProperties(bindingFlags);
        return (from x in type.GetFields(bindingFlags)
                select new PropertyFieldWrapper(x)).Concat(properties.Select(x => new PropertyFieldWrapper(x)));
    }

#region Highlight Edit
    [OnLoad]
    private static void ApplyHighlightHooks()
    {
        MonoModHooks.Modify(
            typeof(ConfigElement).GetMethod("DrawSelf", BindingFlags.NonPublic | BindingFlags.Instance)!,
            DrawSelf_AddHighlight
        );
    }

    private static void DrawSelf_AddHighlight(ILContext il)
    {
        var c = new ILCursor(il);

        var elementIndex = -1;
        c.GotoNext(
            MoveType.After,
            i => i.MatchLdarg(out elementIndex),
            i => i.MatchLdarg(out _),
            i => i.MatchCall<UIElement>("DrawSelf")
        );

        var colorIndex = -1;
        c.GotoNext(
            MoveType.After,
            i => i.MatchLdloc(out colorIndex),
            i => i.MatchCall<ConfigElement>(nameof(ConfigElement.DrawPanel2))
        );

        var jumpDrawPanel = c.MarkLabel();

        var spriteBatchIndex = -1;
        c.GotoPrev(
            MoveType.Before,
            i => i.MatchLdarg(out spriteBatchIndex),
            i => i.MatchLdloc(out _),
            i => i.MatchLdsfld(typeof(TextureAssets).FullName!, nameof(TextureAssets.SettingsPanel))
        );

        c.MoveAfterLabels();

        c.EmitLdarg(elementIndex);

        c.EmitLdarg(spriteBatchIndex);
        c.EmitLdloc(colorIndex);

        c.EmitDelegate(
            (ConfigElement element, SpriteBatch sb, Color color) =>
            {
                if (element is not ConditionalContainerElement wrapper)
                {
                    return false;
                }

                var dims = wrapper.Dimensions;

                DrawStaticHighlightPanel(sb, color, dims);

                return true;
            }
        );

        c.EmitBrtrue(jumpDrawPanel);
    }

    // Draw the panel with the division between the top and bottom half placed
    // at a constant position, this keeps the highlight consistent when the
    // panel is 'expanded.'
    private static void DrawStaticHighlightPanel(
        SpriteBatch sb,
        Color color,
        Rectangle dims
    )
    {
        const int split = 15;

        TextureAssets.SettingsPanel.Wait();
        var texture = TextureAssets.SettingsPanel.Value;

        // Left/Right bars.
        sb.Draw(texture, new Rectangle(dims.X, dims.Y + 2, 2, dims.Height - 4), new Rectangle(0, 2, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + dims.Width - 2, dims.Y + 2, 2, dims.Height - 4), new Rectangle(0, 2, 1, 1), color);

        // Up/Down bars.
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y, dims.Width - 4, 2), new Rectangle(2, 0, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + dims.Height - 2, dims.Width - 4, 2), new Rectangle(2, 0, 1, 1), color);

        // Inner Panel.
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + 2, dims.Width - 4, split - 2), new Rectangle(2, 2, 1, 1), color);
        sb.Draw(texture, new Rectangle(dims.X + 2, dims.Y + split, dims.Width - 4, dims.Height - split - 2), new Rectangle(2, 16, 1, 1), color);
    }
#endregion
}

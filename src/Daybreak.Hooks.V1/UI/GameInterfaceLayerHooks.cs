using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Terraria.ModLoader;
using Terraria.UI;

namespace Daybreak.Hooks.V1;

[Autoload(Side = ModSide.Client)]
public static class GameInterfaceLayers
{
    public const string INTERFACE_LOGIC_1 = "Vanilla: Interface Logic 1";
    public const string MP_PLAYER_NAMES = "Vanilla: MP Player Names";
    public const string EMOTE_BUBBLES = "Vanilla: Emote Bubbles";
    public const string ENTITY_MARKERS = "Vanilla: Entity Markers";
    public const string SMART_CURSOR_TARGETS = "Vanilla: Smart Cursor Targets";
    public const string LASER_RULER = "Vanilla: Laser Ruler";
    public const string RULER = "Vanilla: Ruler";
    public const string GAMEPAD_LOCK_ON = "Vanilla: Gamepad Lock On";
    public const string TILE_GRID_OPTION = "Vanilla: Tile Grid Option";
    public const string TOWN_NPC_HOUSE_BANNERS = "Vanilla: Town NPC House Banners";
    public const string HIDE_UI_TOGGLE = "Vanilla: Hide UI Toggle";
    public const string WIRE_SELECTION = "Vanilla: Wire Selection";
    public const string CAPTURE_MANAGER_CHECK = "Vanilla: Capture Manager Check";
    public const string IN_GAME_OPTIONS = "Vanilla: Ingame Options";
    public const string FANCY_UI = "Vanilla: Fancy UI";
    public const string ACHIEVEMENT_COMPLETE_POPUPS = "Vanilla: Achievement Complete Popups";
    public const string ENTITY_HEALTH_BARS = "Vanilla: Entity Health Bars";
    public const string INVASION_PROGRESS_BARS = "Vanilla: Invasion Progress Bars";
    public const string MAP_MINIMAP = "Vanilla: Map / Minimap";
    public const string DIAGNOSE_NET = "Vanilla: Diagnose Net";
    public const string SIGN_TILE_BUBBLE = "Vanilla: Sign Tile Bubble";
    public const string HAIR_WINDOW = "Vanilla: Hair Window";
    public const string DRESSER_WINDOW = "Vanilla: Dresser Window";
    public const string NPC_SIGN_DIALOG = "Vanilla: NPC / Sign Dialog";
    public const string INTERFACE_LOGIC_2 = "Vanilla: Interface Logic 2";
    public const string RESOURCE_BARS = "Vanilla: Resource Bars";
    public const string INTERFACE_LOGIC_3 = "Vanilla: Interface Logic 3";
    public const string INVENTORY = "Vanilla: Inventory";
    public const string INFO_ACCESSORIES_BAR = "Vanilla: Info Accessories Bar";
    public const string SETTINGS_BUTTON = "Vanilla: Settings Button";
    public const string CONTROL_HINTS = "Vanilla: Control Hints";
    public const string HOTBAR = "Vanilla: Hotbar";
    public const string BUILDER_ACCESSORIES_BAR = "Vanilla: Builder Accessories Bar";
    public const string RADIAL_HOTBARS = "Vanilla: Radial Hotbars";
    public const string MOUSE_TEXT = "Vanilla: Mouse Text";
    public const string PLAYER_CHAT = "Vanilla: Player Chat";
    public const string DEATH_TEXT = "Vanilla: Death Text";
    public const string CURSOR = "Vanilla: Cursor";
    public const string DEBUG_STUFF = "Vanilla: Debug Stuff";
    public const string MOUSE_ITEM_NPC_HEAD = "Vanilla: Mouse Item / NPC Head";
    public const string MOUSE_OVER = "Vanilla: Mouse Over";
    public const string INTERACT_ITEM_ICON = "Vanilla: Interact Item Icon";
    public const string INTERFACE_LOGIC_4 = "Vanilla: Interface Logic 4";

    [AttributeUsage(AttributeTargets.ReturnValue)]
    private sealed class PermitsVoidWithTrueAttribute : AbstractPermitsVoidAttribute
    {
        public override Expression ModifyExpression(HookSubscriber.ReturnExpressionContext ctx)
        {
            return Expression.Block(ctx.CallExpression, Expression.Constant(true));
        }
    }

    // Identical to GameInterfaceDrawMethod
    /// <returns>
    ///     <see langword="false"/> to stop all further <see cref="GameInterfaceLayer"/>s from drawing (permits <see langword="void"/>.)
    /// </returns>
    [return: PermitsVoidWithTrue]
    private delegate bool InterfaceDrawDefinition();

    /// <summary>
    ///     Inserts the decorated method as a <see cref="GameInterfaceLayer"/> before/after the target layer.<br/>
    /// </summary>
    /// <param name="targetLayer">
    ///     Name of the target <see cref="GameInterfaceLayer"/>, constants are provided for vanilla layers in <see cref="GameInterfaceLayers"/>.
    /// </param>
    /// <param name="scaleType">
    ///     Changes how the cursor/screen is scaled, along with what matrix is used in the given active SpriteBatch.
    /// </param>
    /// <inheritdoc cref="InterfaceDrawDefinition" />
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    [HookMetadata(DelegateType = typeof(InterfaceDrawDefinition))]
    public class InsertAttribute(string targetLayer, InterfaceScaleType scaleType, bool after) : BaseHookAttribute
    {
        /// <summary>
        ///     Name of the target <see cref="GameInterfaceLayer"/>, constants are provided for vanilla layers in <see cref="GameInterfaceLayers"/>.
        /// </summary>
        public readonly string TargetLayer = targetLayer;

        /// <summary>
        ///     Changes how the cursor/screen is scaled, along with what matrix is used in the given active SpriteBatch.
        /// </summary>
        public readonly InterfaceScaleType ScaleType = scaleType;

        public readonly bool After = after;

        /// <summary>
        ///     Identifier of this layer, usually <c>"ModName: UIName"</c>,
        ///     leave <see langword="null"/> to use default identifier based on <c>"AssemblyName: MethodName"</c>.
        /// </summary>
        public string? Name { get; set; }

        /// <inheritdoc />
        public override void Apply(MethodInfo bindingMethod, object? instance)
        {
            // TODO: More formal generated names?
            var name = Name ?? $"{bindingMethod.DeclaringType!.Assembly.GetName().Name}: {bindingMethod.Name}";

            var method = new GameInterfaceDrawMethod(HookSubscriber.BuildWrapper<InterfaceDrawDefinition>(bindingMethod, instance));

            var interfaceLayer = new LegacyGameInterfaceLayer(
                name,
                method,
                ScaleType
            );

            layers.Add(
                new Layer(
                    interfaceLayer,
                    TargetLayer,
                    After
                )
            );
        }
    }

    /// <summary>
    ///     Inserts the decorated method as a <see cref="GameInterfaceLayer"/> before the target layer.
    /// </summary>
    /// <inheritdoc cref="InsertAttribute"/>
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    [HookMetadata(DelegateType = typeof(InterfaceDrawDefinition))]
    public sealed class BeforeAttribute(string targetLayer, InterfaceScaleType scaleType)
        : InsertAttribute(targetLayer, scaleType, false);

    /// <summary>
    ///     Inserts the decorated method as a <see cref="GameInterfaceLayer"/> after the target layer.
    /// </summary>
    /// <inheritdoc cref="InsertAttribute"/>
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    [HookMetadata(DelegateType = typeof(InterfaceDrawDefinition))]
    public sealed class AfterAttribute(string targetLayer, InterfaceScaleType scaleType)
        : InsertAttribute(targetLayer, scaleType, true);

    private record struct Layer(LegacyGameInterfaceLayer InterfaceLayer, string TargetLayer, bool After);

    private static readonly HashSet<Layer> layers = [];

    [ModSystemHooks.ModifyInterfaceLayers]
    private static void ModifyInterfaceLayers([OriginalName("layers")] List<GameInterfaceLayer> interfaceLayers)
    {
        foreach (var layer in layers)
        {
            var index = interfaceLayers.FindIndex(l => l.Name.Equals(layer.TargetLayer));

            if (index <= -1)
            {
                continue;
            }

            if (layer.After)
            {
                index++;
            }

            interfaceLayers.Insert(
                index,
                layer.InterfaceLayer
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Daybreak.Common.CodeAnalysis;
using Daybreak.Common.Features.Configuration;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Cil;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;
using TerrariaOverhaul;
using TerrariaOverhaul.Common.ConfigurationScreen;

namespace Daybreak.Content.Compatibility;

/// <summary>
///     Integrates Terraria Overhaul's bespoke configuration API into ours for
///     UI compatibility purposes.
/// </summary>
[ExtendsFromMod("TerrariaOverhaul")]
internal static class TerrariaOverhaulCompat
{
    private sealed class RecalculableElement : UIElement
    {
        public event Action? OnRecalculate;

        public override void Recalculate()
        {
            OnRecalculate?.Invoke();
            base.Recalculate();
        }
    }

    private sealed class FancierGrid : UIGrid
    {
        public override void RecalculateChildren()
        {
            var maxRowWidth = GetInnerDimensions().Width;

            base.RecalculateChildren();

            var index = 0;
            var rowY = 0f;

            var widths = (Span<int>)stackalloc int[_items.Count];
            
            do
            {
                var maxItemHeight = 0f;
                var rowWidth = 0f;
                var rowLength = 0;

                for (var i = index; i < _items.Count; i++)
                {
                    var item = _items[i];

                    var parentDimensions = item.Parent?.GetInnerDimensions() ?? UserInterface.ActiveInstance.GetDimensions();
                    var dim = item.GetOuterDimensions();
                    var minWidth = item.MinWidth.GetValue(parentDimensions.Width);
                    
                    if (rowLength > 0 && rowWidth + minWidth > maxRowWidth)
                    {
                        break;
                    }

                    rowWidth += minWidth;
                    if (rowLength > 0)
                    {
                        rowWidth += ListPadding;
                    }

                    maxItemHeight = Math.Max(maxItemHeight, dim.Height);
                    rowLength++;
                }

                if (rowLength == 0)
                {
                    break;
                }

                var totalPadding = ListPadding * (rowLength - 1);
                var minContentWidth = rowWidth - totalPadding;
                var extraSpace = Math.Max(0f, maxRowWidth - (minContentWidth + totalPadding));
                var extraPerItem = extraSpace / rowLength;

                var x = 0f;

                for (var i = 0; i < rowLength; i++)
                {
                    var item = _items[index + i];
                    
                    var parentDimensions = item.Parent?.GetInnerDimensions() ?? UserInterface.ActiveInstance.GetDimensions();
                    // var dim = item.GetOuterDimensions();
                    var minWidth = item.MinWidth.GetValue(parentDimensions.Width);
                    var newWidth = minWidth + extraPerItem;

                    item.Left.Set(x, 0f);
                    item.Top.Set(rowY, 0f);

                    item.Width.Set(newWidth, 0f);

                    x += newWidth + ListPadding;
                }

                index += rowLength;
                rowY += maxItemHeight + ListPadding;
            }
            while (index < _items.Count);
        }
    }

    [ExtendsFromMod("TerrariaOverhaul", "ThisModDoesNotExist_d426fe6781e47f3ca724d06792c60a12")]
    private sealed class NewCategoryCardPanel(
        string category,
        Asset<Texture2D>? backgroundTexture = null,
        Asset<Texture2D>? borderTexture = null
    ) : CategoryCardPanel(category, backgroundTexture, borderTexture)
    {
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            // base.DrawSelf(spriteBatch);

            DrawStaticHighlightPanel(
                spriteBatch,
                IsMouseHovering ? BackgroundColor : BackgroundColor.MultiplyRGBA(new Color(180, 180, 180)),
                this.Dimensions
            );
        }

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
    }

    [ExtendsFromMod("TerrariaOverhaul")]
    private sealed class SuperSpecialOverhaulCardPageProvider : IDefaultModPageProvider
    {
        [NoJIT]
        void IDefaultModPageProvider.AddCategoriesToContainer(UIList container, ConfigCategory[] categories)
        {
            var gridContainer = new RecalculableElement();
            {
                gridContainer.Width.Set(0f, 1f);
                gridContainer.Height.Set(0f, 0f);
            }
            container.Add(gridContainer);

            var grid = new FancierGrid();
            {
                grid.ManualSortMethod = _ => { };
                grid.Width.Set(0f, 1f);
                grid.Height.Set(0f, 1f);
                grid.ListPadding = 8f;
                grid.SetPadding(0f);

                gridContainer.OnRecalculate += () =>
                {
                    gridContainer.Height.Set(grid.GetTotalHeight(), 0f);
                };
            }
            gridContainer.Append(grid);

            var i = 0;
            foreach (var category in categories.OrderBy(x => x.Handle.Name))
            {
                var cardPanel = new NewCategoryCardPanel(category.Handle.Name, CommonAssets.GetBackgroundTexture(i++))
                {
                    UserObject = category.Handle.Name,
                };
                {
                    cardPanel.MinWidth = cardPanel.Width;
                    cardPanel.OnLeftClick += (_, _) =>
                    {
                        SoundEngine.PlaySound(SoundID.MenuOpen);
                        // SetCategoryScreen(category);
                    };
                }

                if (!TerrariaOverhaul.Core.Configuration.ConfigSystem.CategoriesByName.TryGetValue(category.Handle.Name, out var categoryData))
                {
                    continue;
                }

                if (categoryData.EntriesByName.Values.All(x => x.IsHidden))
                {
                    continue;
                }
                
                grid.Add(cardPanel);
            }

            container.Recalculate();
        }
    }

    [OnLoad]
    private static void ApplyHooks()
    {
        var registerEntryMethod =
            typeof(TerrariaOverhaul.Core.Configuration.ConfigSystem)
               .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
               .First(
                    x =>
                        x.Name == nameof(TerrariaOverhaul.Core.Configuration.ConfigSystem.RegisterEntry)
                     && x.GetParameters().Any(y => y.ParameterType == typeof(TerrariaOverhaul.Core.Configuration.IConfigEntry))
                );

        MonoModHooks.Add(
            registerEntryMethod,
            RegisterEntry_RegisterAsDaybreakEntry
        );

        MonoModHooks.Modify(
            typeof(TerrariaOverhaul.Core.Configuration.ConfigSystem).GetMethod(nameof(TerrariaOverhaul.Core.Configuration.ConfigSystem.ForceInitializeStaticConstructors), BindingFlags.NonPublic | BindingFlags.Instance)!,
            ForceInitializeStaticConstructors_FixLoadErrors
        );
    }

    [ModSystemHooks.PostSetupContent]
    private static void AddConfigProperties()
    {
        ConfigSystem.RegisterDefaultModCategorySorter(
            ConfigValue<Mod?>.Set(ModLoader.GetMod("TerrariaOverhaul")),
            categories => categories.OrderBy(x => x.Handle.Name)
        );

        ConfigSystem.RegisterDefaultModPageProvider(
            ConfigValue<Mod?>.Set(ModLoader.GetMod("TerrariaOverhaul")),
            new SuperSpecialOverhaulCardPageProvider()
        );
    }

    private static void ForceInitializeStaticConstructors_FixLoadErrors(ILContext il)
    {
        var c = new ILCursor(il);

        if (!c.TryGotoNext(MoveType.Before, x => x.MatchCallvirt<Assembly>(nameof(Assembly.GetTypes))))
        {
            // This hopefully means it's fixed.
            return;
        }

        c.Remove();
        c.EmitCall(typeof(AssemblyManager).GetMethod(nameof(AssemblyManager.GetLoadableTypes), BindingFlags.Public | BindingFlags.Static, [typeof(Assembly)])!);
    }

    private static void RegisterEntry_RegisterAsDaybreakEntry(
        Action<Mod, TerrariaOverhaul.Core.Configuration.IConfigEntry, string?> orig,
        Mod mod,
        TerrariaOverhaul.Core.Configuration.IConfigEntry entry,
        string? nameFallback
    )
    {
        orig(mod, entry, nameFallback);

        var categories = new List<ConfigCategory>();
        foreach (var category in entry.Categories)
        {
            categories.Add(GetOrRegisterCategory(category));
        }

        MoveOrAddToFront(categories, GetOrRegisterCategory(entry.Category));

        define_entry_method.MakeGenericMethod(entry.ValueType).Invoke(
            null,
            [
                mod,
                categories,
                entry,
            ]
        );
    }

    private static ConfigCategory GetOrRegisterCategory(string categoryName)
    {
        var handle = ConfigRepository.Default.GetCategoryHandle(ModContent.GetInstance<OverhaulMod>(), categoryName);
        if (ConfigRepository.Default.TryGetCategory(handle, out var category))
        {
            return category;
        }

        return ConfigCategory
              .Define()
              .WithDisplayName(_ => Language.GetText($"Mods.TerrariaOverhaul.Configuration.{categoryName}.DisplayName"))
              .Register(ConfigRepository.Default, handle.Mod, handle.Name);
    }

    private static void MoveOrAddToFront<T>(List<T> list, T item)
    {
        var idx = list.IndexOf(item);
        if (idx == 0)
        {
            return;
        }

        if (idx != -1)
        {
            list.RemoveAt(idx);
        }

        list.Insert(0, item);
    }

    private static readonly MethodInfo define_entry_method = typeof(TerrariaOverhaulCompat).GetMethod(nameof(DefineEntry), BindingFlags.NonPublic | BindingFlags.Static)!;

    private static void DefineEntry<T>(
        Mod mod,
        List<ConfigCategory> categories,
        TerrariaOverhaul.Core.Configuration.IConfigEntry entry
    )
    {
        ConfigEntry<T>.Define()
                      .WithSerialization(
                           serializer: (_, _) => null,
                           deserializer: (e, _) => e.GetLayerValue(ConfigValueLayer.Default)
                       )
                      .WithDisplayName(_ => entry.DisplayName ?? Language.GetText($"Mods.{mod.Name}.Configuration.{entry.Category}.{entry.Name}.DisplayName"))
                      .WithDescription(_ => entry.Description ?? Language.GetText($"Mods.{mod.Name}.Configuration.{entry.Category}.{entry.Name}.Description"))
                      .WithCategories(categories.Select(x => x.Handle).ToArray())
                      .WithConfigSide(ToConfigSide(entry.Side))
                      .Register(ConfigRepository.Default, mod, entry.Name);
    }

    private static ConfigSide ToConfigSide(TerrariaOverhaul.Core.Configuration.ConfigSide side)
    {
        return side switch
        {
            TerrariaOverhaul.Core.Configuration.ConfigSide.Both => ConfigSide.Both,
            TerrariaOverhaul.Core.Configuration.ConfigSide.ClientOnly => ConfigSide.ClientSide,
            TerrariaOverhaul.Core.Configuration.ConfigSide.ServerOnly => ConfigSide.ServerSide,
            _ => throw new ArgumentOutOfRangeException(nameof(side), side, null),
        };
    }
}

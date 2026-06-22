using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Daybreak.Common.Features.Configuration;
using Daybreak.Common.Features.Hooks;
using MonoMod.Cil;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;
using TerrariaOverhaul;

namespace Daybreak.Content.Compatibility;

/// <summary>
///     Integrates Terraria Overhaul's bespoke configuration API into ours for
///     UI compatibility purposes.
/// </summary>
[ExtendsFromMod("TerrariaOverhaul")]
internal static class TerrariaOverhaulCompat
{
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

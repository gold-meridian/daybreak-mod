using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Daybreak.Common.Features.Hooks;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.ModLoader.Core;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     The system responsible for managing <see cref="ConfigRepository"/>
///     instances.
/// </summary>
public static class ConfigSystem
{
    private static readonly Dictionary<Mod, Dictionary<string, ConfigRepository>> repositories_by_mod = [];

    /// <summary>
    ///     Adds this repository to the list of known repositories.
    /// </summary>
    public static void AddRepository(ConfigRepository repository)
    {
        if (!repositories_by_mod.TryGetValue(repository.Mod, out var repositories))
        {
            repositories_by_mod[repository.Mod] = repositories = [];
        }

        if (!repositories.TryAdd(repository.Name, repository))
        {
            throw new InvalidOperationException($"A config repository of the name \"{repository.Name}\" already exists in mod \"{repository.Mod.Name}\"!");
        }
    }

    /// <summary>
    ///     Attempts to get a config repository by its owning mod and name.
    /// </summary>
    public static bool TryGetRepository(
        Mod mod,
        string name,
        [NotNullWhen(returnValue: true)] out ConfigRepository? repository
    )
    {
        if (repositories_by_mod.TryGetValue(mod, out var repositories))
        {
            return repositories.TryGetValue(name, out repository);
        }

        repository = null;
        return false;
    }

    [OnLoad]
    private static void AddDefaultRepository()
    {
        AddRepository(ConfigRepository.Default);
        {
            _ = ConfigRepository.Default.DisplayName;
        }
    }

#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void InitializeConfigEntries()
    {
        HookLoader.OnEarlyModLoad += mod =>
        {
            if (mod.Code is not { } asm)
            {
                return;
            }

            try
            {
                foreach (var type in AssemblyManager.GetLoadableTypes(asm))
                {
                    if (type.IsEnum)
                    {
                        continue;
                    }

                    var hasEntries = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                                         .Any(x => typeof(IConfigEntry).IsAssignableFrom(x.FieldType));
                    hasEntries |= type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                                      .Any(x => typeof(IConfigEntry).IsAssignableFrom(x.PropertyType));

                    if (hasEntries)
                    {
                        RuntimeHelpers.RunClassConstructor(type.TypeHandle);
                    }
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"DAYBREAK: Failed to run static constructors for mod \"{mod.Name}\" to initialize config fields", e);
            }
        };

        MonoModHooks.Add(
            typeof(Mod).GetMethod(nameof(Mod.AddConfig), BindingFlags.Public | BindingFlags.Instance)!,
            AddConfig_RegisterModConfigsInDefaultRepository
        );
    }
#pragma warning restore CA2255

    private static void AddConfig_RegisterModConfigsInDefaultRepository(
        Action<Mod, string, ModConfig> orig,
        Mod self,
        string name,
        ModConfig mc
    )
    {
        orig(self, name, mc);

        var category = ConfigCategory
                      .Define()
                      .WithDisplayName(_ => mc.DisplayName)
                      .Register(ConfigRepository.Default, self, name);

        /*
        foreach (var member in GetSerializableMembers(category, ConfigManager.serializerSettingsCompact.ContractResolver))
        {
            if (member.PropertyType is null || member.PropertyName is not { } propertyName)
            {
                continue;
            }

            define_entry_method.MakeGenericMethod(member.PropertyType).Invoke(null, [member, self, propertyName]);
        }
        */

        // Transcribed largely from
        // ConfigManager::RegisterLocalizationKeysForMembers.
        foreach (var wrapper in ConfigManager.GetFieldsAndProperties(mc))
        {
            var labelObsolete = ConfigManager.GetLegacyLabelAttribute(wrapper.MemberInfo);
            // var tooltipObsolete = ConfigManager.GetLegacyTooltipAttribute(wrapper.MemberInfo);

            if (Attribute.IsDefined(wrapper.MemberInfo, typeof(JsonIgnoreAttribute)) && labelObsolete is null && !Attribute.IsDefined(wrapper.MemberInfo, typeof(ShowDespiteJsonIgnoreAttribute)))
            {
                continue;
            }

            // RegisterLocalizationKeysForMemberType(variable.Type, type.Assembly);

            var labelKey = ConfigManager.GetConfigKey<LabelKeyAttribute>(wrapper.MemberInfo, "Label");
            var tooltipKey = ConfigManager.GetConfigKey<TooltipKeyAttribute>(wrapper.MemberInfo, "Tooltip");

            // TODO: Get around to properly defining behaviors.

            define_entry_method.MakeGenericMethod(wrapper.Type).Invoke(
                null,
                [
                    wrapper, category,
                    mc.Mode == ConfigScope.ClientSide ? ConfigSide.ClientSide : ConfigSide.Both,
                    self,
                    labelKey,
                    tooltipKey,
                ]
            );
        }
    }

    private static readonly MethodInfo define_entry_method = typeof(ConfigSystem).GetMethod(nameof(DefineEntry), BindingFlags.NonPublic | BindingFlags.Static)!;

    private static void DefineEntry<T>(
        PropertyFieldWrapper wrapper,
        ConfigCategory category,
        ConfigSide side,
        Mod mod,
        string labelKey,
        string tooltipKey
    )
    {
        ConfigEntry<T>.Define()
                       /*
                      .WithValueTransformer(
                           getter: (_, _, value) => value,
                           setter: (_, layer, value) => { }
                       )
                      .WithLocalValue(
                           getter: (_, value) => value,
                           setter: (_, ref value, newValue) => value = newValue
                       )
                       */
                      .WithSerialization(
                           serializer: (_, _) => null,
                           deserializer: (e, _) => e.GetLayerValue(ConfigValueLayer.Default)
                       )
                      .WithDisplayName(_ => Language.GetText(labelKey))
                      .WithDescription(_ => Language.GetText(tooltipKey))
                      .WithCategories(category)
                      .WithConfigSide(side)
                      .Register(ConfigRepository.Default, mod, category.Handle.Name + '_' + wrapper.Name);
    }

    /*
    private static void DefineEntry<T>(JsonProperty property, Mod mod, string name)
    {
        ConfigEntry<T>.Define()
                      .Register(ConfigRepository.Default, mod, name);
    }

    private static IEnumerable<JsonProperty> GetSerializableMembers(
        object obj,
        IContractResolver? resolver = null
    )
    {
        resolver ??= new DefaultContractResolver();

        var contract = resolver.ResolveContract(obj.GetType());

        if (contract is not JsonObjectContract objectContract)
        {
            return [];
        }

        return objectContract.Properties
                             .Where(x => !x.Ignored);
    }
    */
}

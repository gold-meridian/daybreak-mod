using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Daybreak.Common.Features.Hooks;
using Terraria.ModLoader;
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

    public static ConfigRepository GetRepository<T>() where T : ConfigRepository
    {
        foreach (var item in repositories_by_mod)
        {
            foreach (var item1 in item.Value)
            {
                if (item1.Value is T)
                {
                    return item1.Value;
                }
            }
        }

        return ConfigRepository.Default;
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
    }
#pragma warning restore CA2255
}

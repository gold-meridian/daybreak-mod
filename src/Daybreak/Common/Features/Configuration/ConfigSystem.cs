using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Terraria.ModLoader;

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
}

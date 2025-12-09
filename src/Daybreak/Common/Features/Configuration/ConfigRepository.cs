using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Daybreak.Common.Features.Hooks;
using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     A config repository manages ownership of config entries and categories.
///     <br />
///     Repositories are responsible for tracking entries and categories as well
///     as responding to changes in their values
///     (<see cref="CommitPendingChanges"/>) and serializing and synchronizing
///     them as necessary (<see cref="SerializeCategories"/>,
///     <see cref="SynchronizeEntries"/>).
/// </summary>
public abstract class ConfigRepository : ILocalizedModType
{
    /// <summary>
    ///     For using a nullable mod value as a key in a hashmap.
    /// </summary>
    protected readonly record struct ModKey(
        [UsedImplicitly] Mod? Mod
    );

    /// <inheritdoc />
    protected readonly record struct CategoryKey(
        [UsedImplicitly] Mod? Mod,
        [UsedImplicitly] string Name
    )
    {
        /// <inheritdoc />
        public CategoryKey(ConfigCategoryHandle handle) : this(handle.Mod, handle.Name) { }

        /// <inheritdoc />
        public CategoryKey(ConfigCategory category) : this(category.Handle) { }
    }

    /// <inheritdoc />
    protected readonly record struct EntryKey(
        [UsedImplicitly] Mod? Mod,
        [UsedImplicitly] string Name
    )
    {
        /// <inheritdoc />
        public EntryKey(ConfigEntryHandle handle) : this(handle.Mod, handle.Name) { }

        /// <inheritdoc />
        public EntryKey(IConfigEntry entry) : this(entry.Handle) { }
    }

    private static readonly DefaultConfigRepository default_repository = new();

    /// <summary>
    ///     The default config repository.
    ///     <br />
    ///     This repository is what's rendered in the settings menu, and
    ///     contains vanilla settings, tModLoader settings, and settings
    ///     produced by the tModLoader <see cref="ModConfig"/> API.
    ///     <br />
    ///     Items in this repository will have serialization/deserialization and
    ///     syncing handled automatically through the mechanisms in which
    ///     tModLoader handles <see cref="ModConfig"/>s.
    /// </summary>
    public static ConfigRepository Default => default_repository;

    [OnLoad]
    private static void AddDefaultRepository()
    {
        ConfigSystem.AddRepository(Default);
    }

    /// <summary>
    ///     The mod that owns this repository.
    /// </summary>
    public abstract Mod Mod { get; }

    /// <summary>
    ///     The unique name of this repository.
    /// </summary>
    public abstract string Name { get; }

    /// <inheritdoc />
    public string FullName => $"{Mod.Name}/{Name}";

    /// <inheritdoc />
    public virtual string LocalizationCategory => nameof(ConfigRepository);

    /// <summary>
    ///     The display name of this repository.
    /// </summary>
    public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), () => "Settings");

    /// <summary>
    ///     All categories registered to this repository.
    /// </summary>
    protected virtual Dictionary<CategoryKey, ConfigCategory> Categories { get; } = [];

    /// <summary>
    ///     All entries registered to this repository.
    /// </summary>
    protected virtual Dictionary<EntryKey, IConfigEntry> Entries { get; } = [];

    /// <summary>
    ///     Gets a category handle from this repository.
    /// </summary>
    public virtual ConfigCategoryHandle GetCategoryHandle(Mod? mod, string uniqueKey)
    {
        return new ConfigCategoryHandle(this, mod, uniqueKey);
    }

    /// <summary>
    ///     Gets an entry handle from this repository.
    /// </summary>
    public virtual ConfigEntryHandle GetEntryHandle(Mod? mod, string uniqueKey)
    {
        return new ConfigEntryHandle(this, mod, uniqueKey);
    }

    /// <summary>
    ///     Gets a category owned by this repository.
    /// </summary>
    public virtual ConfigCategory GetCategory(ConfigCategoryHandle handle)
    {
        if (handle.Repository != this)
        {
            throw new InvalidOperationException("Category handle is not registered to this repository: " + handle);
        }

        return Categories.TryGetValue(new CategoryKey(handle), out var category)
            ? category
            : throw new InvalidOperationException("Category handle is not registered to this repository: " + handle);
    }

    /// <summary>
    ///     Gets a category owned by this repository.
    /// </summary>
    public virtual bool TryGetCategory(
        ConfigCategoryHandle handle,
        [NotNullWhen(returnValue: true)] out ConfigCategory? category
    )
    {
        if (handle.Repository != this)
        {
            category = null;
            return false;
        }

        return Categories.TryGetValue(new CategoryKey(handle), out category);
    }

    /// <summary>
    ///     Gets an entry owned by this repository.
    /// </summary>
    public virtual IConfigEntry GetEntry(ConfigEntryHandle handle)
    {
        if (handle.Repository != this)
        {
            throw new InvalidOperationException("Entry handle is not registered to this repository: " + handle);
        }

        return Entries.TryGetValue(new EntryKey(handle), out var entry)
            ? entry
            : throw new InvalidOperationException("Entry handle is not registered to this repository: " + handle);
    }

    /// <summary>
    ///     Gets an entry owned by this repository.
    /// </summary>
    public virtual bool TryGetEntry(
        ConfigEntryHandle handle,
        [NotNullWhen(returnValue: true)] out IConfigEntry? entry
    )
    {
        if (handle.Repository != this)
        {
            entry = null;
            return false;
        }

        return Entries.TryGetValue(new EntryKey(handle), out entry);
    }

    /// <summary>
    ///     Gets an entry owned by this repository.
    /// </summary>
    public virtual ConfigEntry<T> GetEntry<T>(ConfigEntryHandle handle)
    {
        return GetEntry(handle) as ConfigEntry<T>
            ?? throw new InvalidOperationException($"Entry does not wrap value of type {typeof(T)}: " + handle);
    }

    /// <summary>
    ///     Gets an entry owned by this repository.
    /// </summary>
    public virtual bool TryGetEntry<T>(
        ConfigEntryHandle handle,
        [NotNullWhen(returnValue: true)] out ConfigEntry<T>? entry
    )
    {
        if (!TryGetEntry(handle, out var baseEntry))
        {
            entry = null;
            return false;
        }

        if (baseEntry is ConfigEntry<T> typedEntry)
        {
            entry = typedEntry;
            return true;
        }

        entry = null;
        return false;
    }

    /// <summary>
    ///     Registers a category as belonging to this repository.
    /// </summary>
    public virtual ConfigCategory RegisterCategory(ConfigCategory category)
    {
        if (Categories.ContainsKey(new CategoryKey(category.Handle)))
        {
            throw new InvalidOperationException($"Cannot create category \"{category.Handle.Name}\" for mod \"{LanguageHelpers.GetModName(category.Handle.Mod)}\" because a category of the same name already exists!");
        }

        Categories[new CategoryKey(category.Handle)] = category;
        {
            _ = category.DisplayName;
        }

        return category;
    }

    /// <summary>
    ///     Registers an entry as belonging to this repository.
    /// </summary>
    public virtual ConfigEntry<T> RegisterEntry<T>(ConfigEntry<T> entry)
    {
        if (Entries.ContainsKey(new EntryKey(entry.Handle)))
        {
            throw new InvalidOperationException($"Cannot create entry \"{entry.Handle.Name}\" for mod \"{LanguageHelpers.GetModName(entry.Handle.Mod)}\" because a category of the same name already exists!");
        }

        Entries[new EntryKey(entry.Handle)] = entry;
        {
            _ = entry.DisplayName;
            _ = entry.Tooltip;
            _ = entry.Description;
        }

        return entry;
    }

    /// <summary>
    ///     Commits any pending changes to stored <see cref="Entries"/>.
    /// </summary>
    /// <returns>
    ///     A list of all entries whose <see cref="IConfigEntry.LocalValue"/>s
    ///     were updated.
    /// </returns>
    public virtual List<IConfigEntry> CommitPendingChanges()
    {
        var modifiedEntries = new List<IConfigEntry>();
        foreach (var entry in Entries.Values)
        {
            if (entry.CommitPendingChanges(bulk: true))
            {
                modifiedEntries.Add(entry);
            }
        }

        return modifiedEntries;
    }

    /// <summary>
    ///     Serializes any provided categories to their files.
    /// </summary>
    public abstract void SerializeCategories(params ConfigCategoryHandle[] categories);

    /// <summary>
    ///     Synchronizes the given entries over the network.
    /// </summary>
    public abstract void SynchronizeEntries(params ConfigEntryHandle[] entries);
}

internal sealed class DefaultConfigRepository : ConfigRepository
{
    public override Mod Mod => ModContent.GetInstance<ModImpl>();

    public override string Name => "Settings";

    private static string ConfigsDirectory => Path.Combine(Main.SavePath, "daybreak", "configs");

    public override void SerializeCategories(params ConfigCategoryHandle[] categories)
    {
        if (categories.Length == 0)
        {
            return;
        }

        var dir = ConfigsDirectory;
        {
            Directory.CreateDirectory(dir);
        }

        foreach (var categoryHandle in categories)
        {
            Debug.Assert(categoryHandle.Repository == this);

            if (!TryGetCategory(categoryHandle, out var category))
            {
                Debug.Fail($"Category was not found: repo=({FullName}) category=({categoryHandle})");
                continue;
            }

            var fileName = $"{LanguageHelpers.GetModName(categoryHandle.Mod)}_{categoryHandle.Name}.json";
            File.WriteAllText(
                Path.Combine(dir, fileName),
                ConfigSerialization.SerializeCategory(
                    this,
                    category,
                    ConfigSerialization.Mode.File,
                    Entries.Values.Where(x => x.MainCategory == categoryHandle)
                )
            );
        }
    }

    public override void SynchronizeEntries(params ConfigEntryHandle[] entries)
    {
        if (entries.Length == 0)
        {
            return;
        }

        var entryTokens = new Dictionary<IConfigEntry, JToken>();
        foreach (var entryHandle in entries)
        {
            Debug.Assert(entryHandle.Repository == this);

            if (!TryGetEntry(entryHandle, out var entry))
            {
                Debug.Fail($"Entry was not found: repo=({FullName}) entry=({entryHandle})");
                continue;
            }

            if (entry.Side != ConfigSide.Both)
            {
                continue;
            }

            entryTokens[entry] = ConfigSerialization.SerializeEntry(entry, ConfigSerialization.Mode.Network);
        }
    }
}

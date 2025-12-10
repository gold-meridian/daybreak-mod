using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Daybreak.Common.Features.Configuration.Default;
using JetBrains.Annotations;
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
    protected virtual Dictionary<CategoryKey, ConfigCategory> CategoryMap { get; } = [];

    /// <summary>
    ///     All entries registered to this repository.
    /// </summary>
    protected virtual Dictionary<EntryKey, IConfigEntry> EntryMap { get; } = [];

    /// <summary>
    ///     All categories registered to this repository.
    /// </summary>
    public virtual IEnumerable<ConfigCategory> Categories => CategoryMap.Values;

    /// <summary>
    ///     All entries registered to this repository.
    /// </summary>
    public virtual IEnumerable<IConfigEntry> Entries => EntryMap.Values;

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

        return CategoryMap.TryGetValue(new CategoryKey(handle), out var category)
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

        return CategoryMap.TryGetValue(new CategoryKey(handle), out category);
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

        return EntryMap.TryGetValue(new EntryKey(handle), out var entry)
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

        return EntryMap.TryGetValue(new EntryKey(handle), out entry);
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
        if (CategoryMap.ContainsKey(new CategoryKey(category.Handle)))
        {
            throw new InvalidOperationException($"Cannot create category \"{category.Handle.Name}\" for mod \"{LanguageHelpers.GetModName(category.Handle.Mod)}\" because a category of the same name already exists!");
        }

        CategoryMap[new CategoryKey(category.Handle)] = category;
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
        if (EntryMap.ContainsKey(new EntryKey(entry.Handle)))
        {
            throw new InvalidOperationException($"Cannot create entry \"{entry.Handle.Name}\" for mod \"{LanguageHelpers.GetModName(entry.Handle.Mod)}\" because am emtry of the same name already exists!");
        }

        EntryMap[new EntryKey(entry.Handle)] = entry;
        {
            _ = entry.DisplayName;
            _ = entry.Tooltip;
            _ = entry.Description;
        }

        return entry;
    }

    /// <summary>
    ///     Commits any pending changes to stored <see cref="EntryMap"/>.
    /// </summary>
    /// <returns>
    ///     A list of all entries whose <see cref="IConfigEntry.LocalValue"/>s
    ///     were updated.
    /// </returns>
    public virtual List<IConfigEntry> CommitPendingChanges()
    {
        var modifiedEntries = new List<IConfigEntry>();
        foreach (var entry in EntryMap.Values)
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

    /// <summary>
    ///     Opens the associated UI.
    /// </summary>
    public abstract void ShowInterface(
        Action? onExit = null
    );

    /// <summary>
    ///     Opens the associated UI and goes to the requested category.
    /// </summary>
    public abstract void ShowInterface(
        ConfigCategoryHandle categoryHandle,
        Action? onExit = null
    );

    /// <summary>
    ///     Opens the associated UI and goes ot the requested entry with the
    ///     page set to its main category.
    /// </summary>
    public abstract void ShowInterface(
        ConfigEntryHandle entryHandle,
        Action? onExit = null
    );

    /// <summary>
    ///     Opens the associated UI and goes ot the requested entry with the
    ///     page set to its requested category.
    /// </summary>
    public abstract void ShowInterface(
        ConfigCategoryHandle categoryHandle,
        ConfigEntryHandle entryHandle,
        Action? onExit = null
    );
}

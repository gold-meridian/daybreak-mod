using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     A config repository manages ownership of config entries and categories.
/// </summary>
public abstract class ConfigRepository
{
    /// <summary>
    ///     For using a nullable mod value as a key in a hashmap.
    /// </summary>
    protected readonly record struct ModKey(Mod? Mod);

    /// <inheritdoc />
    protected readonly record struct CategoryKey(Mod? Mod, string Name)
    {
        /// <inheritdoc />
        public CategoryKey(ConfigCategoryHandle handle) : this(handle.Mod, handle.Name) { }

        /// <inheritdoc />
        public CategoryKey(ConfigCategory category) : this(category.Handle) { }
    }

    /// <inheritdoc />
    protected readonly record struct EntryKey(Mod? Mod, string Name)
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
    public virtual ConfigEntry<T> GetEntry<T>(ConfigEntryHandle handle)
        where T : IEquatable<T>
    {
        return GetEntry(handle) as ConfigEntry<T>
            ?? throw new InvalidOperationException($"Entry does not wrap value of type {typeof(T)}: " + handle);
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
        _ = category.DisplayName;

        return category;
    }

    /// <summary>
    ///     Registers an entry as belonging to this repository.
    /// </summary>
    public virtual ConfigEntry<T> RegisterEntry<T>(ConfigEntry<T> entry)
        where T : IEquatable<T>
    {
        if (Entries.ContainsKey(new EntryKey(entry.Handle)))
        {
            throw new InvalidOperationException($"Cannot create entry \"{entry.Handle.Name}\" for mod \"{LanguageHelpers.GetModName(entry.Handle.Mod)}\" because a category of the same name already exists!");
        }

        Entries[new EntryKey(entry.Handle)] = entry;
        _ = entry.DisplayName;
        _ = entry.Tooltip;
        _ = entry.Description;

        return entry;
    }
}

internal sealed class DefaultConfigRepository : ConfigRepository { }

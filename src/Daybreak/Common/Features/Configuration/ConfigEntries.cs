using System;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     Describes what side a config option pertains to.
/// </summary>
public enum ConfigSide
{
    /// <summary>
    ///     The entry is only client-side and does not need syncing across the
    ///     boundary.
    /// </summary>
    ClientSide,

    /// <summary>
    ///     The entry is only server-side and does not need syncing across the
    ///     boundary.
    /// </summary>
    ServerSide,

    /// <summary>
    ///     The entry applies to both the client and server and needs syncing
    ///     across the boundary.
    /// </summary>
    Both,

    /// <summary>
    ///     The entry may apply to both the client and server, but does not need
    ///     syncing across the boundary.
    /// </summary>
    /// <remarks>
    ///     This mode is seldom useful, but is particularly important for
    ///     no-sync mods.
    /// </remarks>
    NoSync,
}

/// <summary>
///     Uniquely identifies a config entry.
///     <br />
///     Identities may be freely shared across mods without referencing
///     assemblies of other mods, and can be used to inspect the status of an
///     entry without requiring it be registered.
/// </summary>
/// <param name="Mod">
///     The mod this entry belongs to.  If the mod is
///     <see langword="null"/>, then this entry is considered as belonging
///     to vanilla.
/// </param>
/// <param name="UniqueKey">
///     The unique key which identifies this entry (sub-categorized with
///     <see cref="Mod"/>).
///     <br />
///     This key should <b>not</b> contain the mod name. The key needs to
///     only be unique when compared against other keys in the same mod.
/// </param>
public readonly record struct ConfigEntryIdentity(
    Mod? Mod,
    string UniqueKey
);

/// <summary>
///     Uniquely identifies a config category.
///     <br />
///     Identities may be freely shared across mods without referencing
///     assemblies of other mods, and can be used to inspect the status of a
///     category without requiring it be registered.
/// </summary>
/// <param name="Mod">
///     The mod this category belongs to.  If the mod is
///     <see langword="null"/>, then this category is considered as belonging
///     to vanilla.
/// </param>
/// <param name="UniqueKey">
///     The unique key which identifies this category (sub-categorized with
///     <see cref="Mod"/>).
///     <br />
///     This key should <b>not</b> contain the mod name. The key needs to
///     only be unique when compared against other keys in the same mod.
/// </param>
public readonly record struct ConfigCategoryIdentity(
    Mod? Mod,
    string UniqueKey
);

/// <summary>
///     The type-safe contract over <see cref="IConfigEntry"/>.
/// </summary>
public interface IConfigEntry<T> : IConfigEntry
{
    Type IConfigEntry.ValueType => typeof(T);

    /// <summary>
    ///     The actual value being worked with.  Depending on the game context,
    ///     we either use <see cref="LocalValue"/> or <see cref="RemoteValue"/>
    ///     as its backing property.  Mutating this value with modify either the
    ///     local or remote value according to the context.
    ///     <br />
    ///     On servers, the remote and local value are always the same.
    /// </summary>
    T? Value { get; set; }

    /// <summary>
    ///     The local (client-sided) value.
    ///     <br />
    ///     On servers, this is the same as <see cref="RemoteValue"/>.
    /// </summary>
    T? LocalValue { get; set; }

    /// <summary>
    ///     The remote (server-sided) value.
    ///     <br />
    ///     On servers, this is the same as <see cref="LocalValue"/>.
    /// </summary>
    T? RemoteValue { get; set; }

    /// <summary>
    ///     The default value.
    /// </summary>
    T? DefaultValue { get; set; }
}

/// <summary>
///     The type-generic config entry contract.
/// </summary>
public interface IConfigEntry
{
    /// <summary>
    ///     The config entry identity which may be used to uniquely identify
    ///     this entry and obtain it as necessary.
    /// </summary>
    ConfigEntryIdentity Id { get; }

    /// <summary>
    ///     This entry's side, which controls syncing and which version of the
    ///     config to use and display.
    /// </summary>
    ConfigSide Side { get; }

    /// <summary>
    ///     The type this entry stores.
    /// </summary>
    Type ValueType { get; }

    /// <summary>
    ///     The display name of this config entry.
    /// </summary>
    LocalizedText DisplayName { get; }

    /// <summary>
    ///     The short-form tooltip briefly summarizing this entry, to be
    ///     displayed next to the cursor on hover.
    /// </summary>
    LocalizedText Tooltip { get; }

    /// <summary>
    ///     The long-form description which may be rendered at the bottom of a
    ///     config UI.
    /// </summary>
    LocalizedText Description { get; }

    /// <summary>
    ///     The categories this entry belongs to.  The first category is the
    ///     &quot;main&quot; category to which this entry canonically belongs
    ///     to.
    /// </summary>
    /// <remarks>
    ///     There must be <b>at least 1</b> category.
    /// </remarks>
    ReadOnlySpan<ConfigCategoryIdentity> Categories { get; }

    /// <summary>
    ///     The main category of this entry, derived from the first item in
    ///     <see cref="Categories"/>.
    /// </summary>
    ConfigCategoryIdentity MainCategory => Categories[0];

    /// <summary>
    ///     Called when this entry is registered.
    /// </summary>
    void OnRegister();
}

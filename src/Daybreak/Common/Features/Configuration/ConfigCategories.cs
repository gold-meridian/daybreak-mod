using Terraria.ModLoader;

namespace Daybreak.Common.Features.Configuration;

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

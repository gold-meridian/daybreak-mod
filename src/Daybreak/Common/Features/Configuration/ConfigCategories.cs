using Terraria.ModLoader;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     Uniquely identifies a config category.
///     <br />
///     Handles may be freely shared across mods without referencing assemblies
///     of other mods, and can be used to inspect the status of a category
///     without requiring it be registered.
/// </summary>
public readonly struct ConfigCategoryHandle
{
    /// <summary>
    ///     The mod this category belongs to.  If the mod is
    ///     <see langword="null"/>, then this category is considered as belonging
    ///     to vanilla.
    /// </summary>
    public Mod? Mod { get; private init; }

    /// <summary>
    ///     The unique key which identifies this category (sub-categorized with
    ///     <see cref="Mod"/>).
    ///     <br />
    ///     This key should <b>not</b> contain the mod name. The key needs to
    ///     only be unique when compared against other keys in the same mod.
    /// </summary>
    public string FullName { get; private init; } = string.Empty;

    internal ConfigCategoryHandle(Mod? mod, string fullName)
    {
        Mod = mod;
        FullName = fullName;
    }
}

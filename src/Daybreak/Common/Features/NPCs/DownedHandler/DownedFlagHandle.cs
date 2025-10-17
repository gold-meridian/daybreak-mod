using Terraria;

namespace Daybreak.Common.Features.NPCs;

/// <summary>
///     A handle to the status of a &quot;downed&quot; flag, representing
///     generally the defeat of an NPC within a world (e.g.
///     <see cref="NPC.downedBoss1" />).
/// </summary>
public readonly struct DownedFlagHandle
{
    /// <summary>
    ///     The full name; a unique identifier for each handle.  Expected to
    ///     be the mod name followed by the unique key
    ///     (<c>ModName/NpcName</c>).
    /// </summary>
    /// <remarks>
    ///     Any supplementary vanilla handles should follow this pattern too:
    ///     <c>Terraria/EyeOfCthulhu</c>.
    /// </remarks>
    public string FullName { get; }

    /// <summary>
    ///     Whether this handle represents a flag that is known to the handler.
    ///     <br />
    ///     It is possible to create handles prior to initialization of their
    ///     respective handlers for convenience, so their behavior will be
    ///     stubbed otherwise; this exists to differentiate this state.
    /// </summary>
    public bool IsRegistered => DownedFlagHandler.IsHandleRegistered(this);

    /// <summary>
    ///     The mutable value of the associated flag.
    /// </summary>
    public bool Value
    {
        get => DownedFlagHandler.GetValue(this);
        set => DownedFlagHandler.SetValue(this, value);
    }

    internal DownedFlagHandle(string fullName)
    {
        FullName = fullName;
    }
}

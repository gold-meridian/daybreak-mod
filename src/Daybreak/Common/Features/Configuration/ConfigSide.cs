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

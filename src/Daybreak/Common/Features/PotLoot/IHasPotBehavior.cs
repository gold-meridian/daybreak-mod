namespace Daybreak.Common.Features.PotLoot;

/// <summary>
///     Denotes a tile as providing a pot behavior, since it's a pot.
/// </summary>
public interface IHasPotBehavior
{
    /// <summary>
    ///     The pot behavior that this tile provides, which should be a
    ///     singleton.
    /// </summary>
    PotBehavior Behavior { get; }
}

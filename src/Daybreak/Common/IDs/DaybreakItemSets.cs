using Daybreak.Common.Features.Rendering;

using JetBrains.Annotations;

using Terraria.ID;
using Terraria.ModLoader;

namespace Daybreak.Common.IDs;

/// <summary>
///     Provides item ID sets.
/// </summary>
[PublicAPI]
public sealed class DaybreakItemSets : ModSystem
{
    public static IPreRenderedItem?[] PreRenderedItems { get; private set; } = [];

    /// <inheritdoc />
    public override void ResizeArrays()
    {
        base.ResizeArrays();

        PreRenderedItems = CreateSet<IPreRenderedItem?>(nameof(PreRenderedItems), null);

        return;

        T[] CreateSet<T>(string name, T defaultValue)
        {
            return ItemID.Sets.Factory.CreateNamedSet(Mod, name)
                         .RegisterCustomSet(defaultValue);
        }
    }
}
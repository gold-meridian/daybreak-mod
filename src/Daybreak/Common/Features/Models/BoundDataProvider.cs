using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

// TODO: Item, NPC, Projectile

/// <summary>
///     Abstraction designed to handle a collection of <see cref="Bound{T}"/>
///     objects.
/// </summary>
public abstract class BoundDataProvider : InstanceData
{
    /// <inheritdoc />
    protected override void LoadSingleton(Mod mod)
    {
        var properties = GetType()
                               .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                               .Where(x => x.PropertyType.IsAssignableTo(typeof(IBinding)))
                               .Select(x => x.GetValue(this))
                               .Cast<IBound>()
                               .ToArray();

        mod.AddContent(CreateContent(properties));
    }

    /// <inheritdoc />
    protected override void UnloadSingleton()
    {
        // TODO
    }

    /// <summary>
    ///     Initializes a loadable type to be added to the mod.
    /// </summary>
    /// <param name="properties">The built collection of properties.</param>
    protected abstract ILoadable CreateContent(IBound[] properties);
}

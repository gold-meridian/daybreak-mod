using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

// TODO: Item, NPC, Projectile

/// <summary>
///     Abstraction designed to handle a collection of <see cref="Bound{T}"/>
///     objects.
/// </summary>
public abstract class BoundDataProvider : ModType
{
    /// <summary>
    ///     The name of this data provider, which is used for things such as
    ///     data serialization.
    /// </summary>
    public override string Name { get; }

    /// <inheritdoc />
    protected BoundDataProvider()
    {
        Name = NameProvider.ForType(GetType());
    }

    /// <inheritdoc />
    public sealed override void Load()
    {
        base.Load();

        var properties = GetType()
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(x => x.PropertyType.IsAssignableTo(typeof(IBinding)))
                        .Select(x => x.GetValue(this))
                        .Cast<IBound>()
                        .ToArray();
    }

    /// <inheritdoc />
    public sealed override void Unload()
    {
        base.Unload();
    }

#region Unused hooks
    /// <inheritdoc />
    protected sealed override void Register() { }

    /// <inheritdoc />
    public sealed override void SetupContent()
    {
        base.SetupContent();
    }

    /// <inheritdoc />
    public sealed override void SetStaticDefaults()
    {
        base.SetStaticDefaults();
    }

    /// <inheritdoc />
    protected sealed override void InitTemplateInstance()
    {
        base.InitTemplateInstance();
    }

    /// <inheritdoc />
    protected sealed override void ValidateType()
    {
        base.ValidateType();
    }
#endregion
}

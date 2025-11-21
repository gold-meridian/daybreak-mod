using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

// TODO: Item, NPC, Projectile

/// <summary>
///     The type-unsafe contract of a
///     <see cref="BoundDataProvider{TProvider}"/>.
/// </summary>
public interface IBoundDataProvider
{
    /// <inheritdoc cref="BoundDataProvider{TProvider}.Properties"/>
    IBound[] Properties { get; }

    /// <inheritdoc cref="BoundDataProvider{TProvider}.Name"/>
    string Name { get; }

    /// <inheritdoc cref="BoundDataProvider{TProvider}.Mod"/>
    Mod Mod { get; }
}

/// <summary>
///     Abstraction designed to handle a collection of <see cref="Bound{T}"/>
///     objects.
/// </summary>
/// <typeparam name="TProvider">The self.</typeparam>
public abstract class BoundDataProvider<TProvider> : ILoadable
    where TProvider : BoundDataProvider<TProvider>
{
    // ReSharper disable once StaticMemberInGenericType
    private static IBound[] properties = [];

    // ReSharper disable once StaticMemberInGenericType
    private static string name = NameProvider.GetDefaultName(typeof(TProvider));

    // ReSharper disable once StaticMemberInGenericType
    private static Mod ownedMod = null!;

    /// <summary>
    ///     The bound values belonging to the <see cref="TProvider"/>.
    /// </summary>
    public IBound[] Properties => properties;

    /// <summary>
    ///     The name of this data provider, which is used for things such as
    ///     data serialization.
    /// </summary>
    public string Name => name;

    /// <summary>
    ///     The mod that
    /// </summary>
    public Mod Mod => ownedMod;

    /// <summary>
    ///     Initializes the <see cref="Name"/> based on any providers.
    /// </summary>
    protected BoundDataProvider()
    {
        name = NameProvider.ForType(GetType());
    }

    void ILoadable.Load(Mod mod)
    {
        ownedMod = mod;
        properties = GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(x => x.PropertyType.IsAssignableTo(typeof(IBinding)))
                    .Select(x => x.GetValue(this))
                    .Cast<IBound>()
                    .ToArray();

        Load();
    }

    /// <summary>
    ///     Loads the template instance after resolving properties.
    /// </summary>
    protected abstract void Load();

    void ILoadable.Unload()
    {
        Unload();
    }

    /// <summary>
    ///     Unloads the template instance.
    /// </summary>
    protected abstract void Unload();
}

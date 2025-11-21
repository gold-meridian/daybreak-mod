using System.Collections.Generic;
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
    /// <inheritdoc cref="BoundDataProvider{TProvider}.PropertyMap"/>
    Dictionary<PropertyInfo, IBound> PropertyMap { get; }

    /// <inheritdoc cref="BoundDataProvider{TProvider}.Properties"/>
    IEnumerable<IBound> Properties { get; }

    /// <inheritdoc cref="BoundDataProvider{TProvider}.Name"/>
    string Name { get; }

    /// <inheritdoc cref="BoundDataProvider{TProvider}.Mod"/>
    Mod Mod { get; }

    /// <inheritdoc cref="BoundDataProvider{TProvider}.Mod"/>
    IBoundDataProvider Clone();
}

/// <summary>
///     Abstraction designed to handle a collection of <see cref="Bound{T}"/>
///     objects.
/// </summary>
/// <typeparam name="TProvider">The self.</typeparam>
public abstract class BoundDataProvider<TProvider> : IBoundDataProvider, ILoadable
    where TProvider : BoundDataProvider<TProvider>, new()
{
    // ReSharper disable once StaticMemberInGenericType
    private static Dictionary<PropertyInfo, IBound> propertyMap = [];

    // ReSharper disable once StaticMemberInGenericType
    private static string name = NameProvider.GetDefaultName(typeof(TProvider));

    // ReSharper disable once StaticMemberInGenericType
    private static Mod ownedMod = null!;

    /// <summary>
    ///     The bound values belonging to the <typeparamref name="TProvider"/>.
    /// </summary>
    public Dictionary<PropertyInfo, IBound> PropertyMap => propertyMap;

    /// <summary>
    ///     The bound properties for a real instance.  Empty on the template
    ///     instance.
    /// </summary>
    public IEnumerable<IBound> Properties
    {
        get => field ?? [];
        private set;
    }

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
        propertyMap = GetType()
                     .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .Where(x => x.PropertyType.IsAssignableTo(typeof(IBound)))
                     .ToDictionary(x => x, x => (IBound)x.GetValue(this)!);

        foreach (var (propertyInfo, bound) in propertyMap)
        {
            bound.Binding.Configure(propertyInfo);
        }

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

    /// <summary>
    ///     Creates a real instance of this data provider from the template
    ///     instance.
    /// </summary>
    /// <returns></returns>
    public IBoundDataProvider Clone()
    {
        var provider = new TProvider();
        var properties = new List<IBound>(propertyMap.Count);

        foreach (var (propertyInfo, bound) in propertyMap)
        {
            var property = bound.Clone();
            propertyInfo.SetValue(provider, property);
            properties.Add(property);
        }

        provider.Properties = properties;
        return provider;
    }
}

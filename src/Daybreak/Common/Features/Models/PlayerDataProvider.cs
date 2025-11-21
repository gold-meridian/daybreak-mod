using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Daybreak.Common.CodeAnalysis;
using MonoMod.Utils;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

/// <summary>
///     A <see cref="BoundDataProvider{TProvider}"/> implemented for
///     <see cref="Player"/> data.
/// </summary>
/// <typeparam name="TProvider">The self.</typeparam>
public abstract class PlayerDataProvider<TProvider> : BoundDataProvider<TProvider>
    where TProvider : PlayerDataProvider<TProvider>, new()
{
    /// <inheritdoc />
    protected override void Load()
    {
        PlayerDataProviderImpl.KnownDataProviders.Add(GetType(), this);
    }

    /// <inheritdoc />
    protected override void Unload() { }
}

[ExpectCloneable(false)]
internal sealed class PlayerDataProviderImpl : ModPlayer
{
    /// <summary>
    ///     Carries all known providers' template instances to create instanced
    ///     data from.
    /// </summary>
    public static Dictionary<Type, IBoundDataProvider> KnownDataProviders { get; } = [];

    // We specifically do not want to clone because we store a map of providers.
    // We want to rebuild this map by hand.
    protected override bool CloneNewInstances => false;

    public Dictionary<Type, IBoundDataProvider> DataProviders { get; private set; } = [];

    public IEnumerable<IBoundDataProvider> Providers => DataProviders.Values;

    public override void Load()
    {
        base.Load();

        // Assign the template instance's map to the static map so we can use it
        // during initial NewInstance creation later.
        DataProviders = KnownDataProviders;
    }

    public override ModPlayer NewInstance(Player entity)
    {
        var newInstance = (PlayerDataProviderImpl)base.NewInstance(entity)!;
        {
            // Build a new map from the old one by cloning the template values
            // into real instances.
            newInstance.DataProviders.AddRange(
                DataProviders.ToDictionary(
                    x => x.Key,
                    x => x.Value.Clone()
                )
            );
        }

        return newInstance;
    }

    public override void ResetEffects()
    {
        base.ResetEffects();

        foreach (var provider in Providers)
        {
            foreach (var property in provider.Properties)
            {
                property.Binding.Reset(property);
            }
        }
    }
}

/// <summary>
///     Extensions to get data from players.
/// </summary>
public static class PlayerExtensions
{
    /// <summary>
    ///     Extensions to get data from players.
    /// </summary>
    extension(Player player)
    {
        /// <summary>
        ///     Attempts to get data from the player.
        ///     <br />
        ///     Returns <see langword="true"/> if the data was present,
        ///     otherwise <see langword="false"/>.
        /// </summary>
        public bool TryGet<T>([NotNullWhen(returnValue: true)] out T? data)
            where T : PlayerDataProvider<T>, new()
        {
            data = null;

            if (!player.TryGetModPlayer<PlayerDataProviderImpl>(out var impl))
            {
                return false;
            }

            if (!impl.DataProviders.TryGetValue(typeof(T), out var provider))
            {
                return false;
            }

            if (provider is not T tProvider)
            {
                return false;
            }

            data = tProvider;
            return true;
        }

        /// <summary>
        ///     Gets data from the player, throwing if it isn't present.
        /// </summary>
        public T Get<T>()
            where T : PlayerDataProvider<T>, new()
        {
            player.TryGet<T>(out var data);
            return data ?? throw new InvalidOperationException($"Cannot get player data: {typeof(T)}");
        }
    }
}

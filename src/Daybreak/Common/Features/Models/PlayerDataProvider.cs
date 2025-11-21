using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

/// <summary>
///     A <see cref="BoundDataProvider{TProvider}"/> implemented for
///     <see cref="Player"/> data.
/// </summary>
/// <typeparam name="TProvider">The self.</typeparam>
public abstract class PlayerDataProvider<TProvider> : BoundDataProvider<TProvider>
    where TProvider : PlayerDataProvider<TProvider>
{
    /// <inheritdoc />
    protected override void Load() { }

    /// <inheritdoc />
    protected override void Unload() { }
}

internal sealed class PlayerDataProviderImpl(Type type) : ModPlayer
{
    public Dictionary<Type, IBoundDataProvider> DataProviders { get; } = [];
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
            where T : PlayerDataProvider<T>
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
            where T : PlayerDataProvider<T>
        {
            player.TryGet<T>(out var data);
            return data ?? throw new InvalidOperationException($"Cannot get player data: {typeof(T)}");
        }
    }
}

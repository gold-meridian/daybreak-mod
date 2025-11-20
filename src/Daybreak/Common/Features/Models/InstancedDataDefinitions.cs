using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

// GlobalItem
// GlobalNPC
// GlobalProjectile
// ModPlayer

/// <summary>
///     Base implementation of <see cref="InstanceData"/> belonging to an entity
///     <typeparamref name="TEntity"/>.
/// </summary>
/// <typeparam name="TEntity">The entity type.</typeparam>
public abstract class EntityData<TEntity> : InstanceData
{
    public abstract TEntity Entity { get; protected set; }
}

public abstract class PlayerData : EntityData<Player>
{
    protected sealed override void LoadSingleton(Mod mod)
    {
        throw new System.NotImplementedException();
    }

    protected sealed override void UnloadSingleton()
    {
        throw new System.NotImplementedException();
    }
}

public abstract class ItemData : EntityData<Player> { }

public abstract class NpcData : EntityData<Player> { }

public abstract class ProjectileData : EntityData<Player> { }

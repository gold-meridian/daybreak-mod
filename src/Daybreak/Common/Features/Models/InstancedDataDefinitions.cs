using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

// GlobalItem
// GlobalNPC
// GlobalProjectile
// ModPlayer

public abstract class PlayerData : InstanceData
{
    private sealed class ModPlayerImpl<T> : ModPlayer
    {
        public override void ResetEffects()
        {
            base.ResetEffects();
        }

        public override ModPlayer Clone(Player newEntity)
        {
            return base.Clone(newEntity);
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            base.SyncPlayer(toWho, fromWho, newPlayer);
        }

        public override void CopyClientState(ModPlayer targetCopy)
        {
            base.CopyClientState(targetCopy);
        }

        public override void SendClientChanges(ModPlayer clientPlayer)
        {
            base.SendClientChanges(clientPlayer);
        }
    }
    
    public required Player Player { get; init; }

    public required ModPlayer ModPlayer { get; init; }

    protected sealed override void LoadSingleton(Mod mod)
    {
        throw new System.NotImplementedException();
    }

    protected sealed override void UnloadSingleton()
    {
        throw new System.NotImplementedException();
    }
}

/*
public abstract class ItemData : EntityData<Player> { }

public abstract class NpcData : EntityData<Player> { }

public abstract class ProjectileData : EntityData<Player> { }
*/

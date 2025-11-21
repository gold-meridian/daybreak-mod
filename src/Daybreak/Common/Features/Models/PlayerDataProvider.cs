using System;
using Terraria;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Models;

public abstract class PlayerDataProvider : BoundDataProvider
{
    protected override ILoadable CreateContent(IBound[] properties)
    {
        
    }
}

file sealed class ModPlayerImpl(Type type) : ModPlayer
{
    
}

public static class PlayerExtensions
{
    extension(Player player)
    {
        public T Get<T>()
            where T : PlayerDataProvider
        {
            
        }
    }
}

using System.ComponentModel;

using Terraria.ModLoader.Config;

namespace LiquidSlopesPatch.Common;

public sealed class Config : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool Enabled { get; set; }
}
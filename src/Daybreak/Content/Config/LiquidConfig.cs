using System.ComponentModel;

using Terraria.ModLoader.Config;

namespace Daybreak.Content.Config;

internal sealed class LiquidConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ClientSide;

    [DefaultValue(true)]
    public bool FixLiquidSlopes { get; set; }
}
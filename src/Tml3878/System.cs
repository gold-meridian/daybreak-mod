using System;
using System.Threading.Tasks;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Tml3878;

internal sealed class System : ModSystem
{
    private static readonly int random_value = new Random().Next(1000, 10000);

    public override void Load()
    {
        base.Load();

        Task.Run(
            async () =>
            {
                var item = new Item(ItemID.IronPickaxe);
                Mod.Logger.Info($"item damage is 5: {item.damage == 5}");

                Mod.Logger.Info("Installing hook...");
                IL_Item.SetDefaults1 += il =>
                {
                    // change damage of iron pickaxe as really simple test case
                    var c = new ILCursor(il);
                    c.GotoNext(MoveType.After, x => x.MatchLdcI4(5));
                    c.EmitPop();
                    c.EmitLdcI4(random_value);
                };
                Mod.Logger.Info("Installed hook!");

                while (true)
                {
                    item.SetDefaults(ItemID.IronPickaxe);
                    Mod.Logger.Info($"item damage is random value: {item.damage == random_value} ({item.damage})");
                    await Task.Delay(250);
                }
                
                // ReSharper disable once FunctionNeverReturns
            }
        );
    }
}

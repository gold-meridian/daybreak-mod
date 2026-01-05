using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Core;

namespace Daybreak.Common.Features.Hooks;

public static partial class ModifyItemDrawBasics
{
    public interface IItem
    {
        void ModifyItemDrawBasics(
            int slot,
            ref Texture2D texture,
            ref Rectangle frame,
            ref Rectangle glowmaskFrame
        );
    }

    public interface IGlobal
    {
        void ModifyItemDrawBasics(
            Item item,
            int slot,
            ref Texture2D texture,
            ref Rectangle frame,
            ref Rectangle glowmaskFrame
        );
    }
    
    private static readonly GlobalHookList<GlobalItem> hook_list = ItemLoader.AddModHook(
        GlobalHookList<GlobalItem>.Create(x => ((IGlobal)x).ModifyItemDrawBasics)
    );

    public static void Invoke(
        Item item,
        int slot,
        ref Texture2D texture,
        ref Rectangle frame,
        ref Rectangle glowmaskFrame
    )
    {
        if (item.ModItem is IItem modItem)
        {
            modItem.ModifyItemDrawBasics(slot, ref texture, ref frame, ref glowmaskFrame);
        }
        
        foreach (var g in hook_list.Enumerate(item))
        {
            if (g is not IGlobal hook)
            {
                continue;
            }

            hook.ModifyItemDrawBasics(item, slot, ref texture, ref frame, ref glowmaskFrame);
        }
    }

    [OnLoad]
    private static void ApplyHooks()
    {
        On_Main.DrawItem_GetBasics += (orig, self, item, slot, out texture, out frame, out glowmaskFrame) =>
        {
            orig(self, item, slot, out texture, out frame, out glowmaskFrame);

            Invoke(item, slot, ref texture, ref frame, ref glowmaskFrame);
        };
    }
}

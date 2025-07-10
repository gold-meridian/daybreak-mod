namespace Daybreak.Common.Features.Hooks;

using System.Linq;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalBuff':
//     System.Void Terraria.ModLoader.GlobalBuff::Update(System.Int32,Terraria.Player,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalBuff::Update(System.Int32,Terraria.NPC,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalBuff::ReApply(System.Int32,Terraria.Player,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalBuff::ReApply(System.Int32,Terraria.NPC,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalBuff::ModifyBuffText(System.Int32,System.String&,System.String&,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalBuff::CustomBuffTipSize(System.String,System.Collections.Generic.List`1<Microsoft.Xna.Framework.Vector2>)
//     System.Void Terraria.ModLoader.GlobalBuff::DrawCustomBuffTip(System.String,Microsoft.Xna.Framework.Graphics.SpriteBatch,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalBuff::PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch,System.Int32,System.Int32,Terraria.DataStructures.BuffDrawParams&)
//     System.Void Terraria.ModLoader.GlobalBuff::PostDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch,System.Int32,System.Int32,Terraria.DataStructures.BuffDrawParams)
//     System.Boolean Terraria.ModLoader.GlobalBuff::RightClick(System.Int32,System.Int32)
public static partial class GlobalBuffHooks
{
    public sealed partial class Update_int_Player_int
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.Player player,
            ref int buffIndex
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.Player player,
            ref int buffIndex
        )
        {
            Event?.Invoke(self, type, player, ref buffIndex);
        }
    }

    public sealed partial class Update_int_NPC_int
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.NPC npc,
            ref int buffIndex
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.NPC npc,
            ref int buffIndex
        )
        {
            Event?.Invoke(self, type, npc, ref buffIndex);
        }
    }

    public sealed partial class ReApply_int_Player_int_int
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.Player player,
            int time,
            int buffIndex
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.Player player,
            int time,
            int buffIndex
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, type, player, time, buffIndex))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class ReApply_int_NPC_int_int
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.NPC npc,
            int time,
            int buffIndex
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.NPC npc,
            int time,
            int buffIndex
        )
        {
            if (Event == null)
            {
                return false;
            }

            foreach (var handler in GetInvocationList())
            {
                if (handler.Invoke(self, type, npc, time, buffIndex))
                {
                    return true;
                }
            }

            return false;
        }
    }

    public sealed partial class ModifyBuffText
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            ref string buffName,
            ref string tip,
            ref int rare
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            ref string buffName,
            ref string tip,
            ref int rare
        )
        {
            Event?.Invoke(self, type, ref buffName, ref tip, ref rare);
        }
    }

    public sealed partial class CustomBuffTipSize
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalBuff self,
            string buffTip,
            System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> sizes
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalBuff self,
            string buffTip,
            System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> sizes
        )
        {
            Event?.Invoke(self, buffTip, sizes);
        }
    }

    public sealed partial class DrawCustomBuffTip
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalBuff self,
            string buffTip,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int originX,
            int originY
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalBuff self,
            string buffTip,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int originX,
            int originY
        )
        {
            Event?.Invoke(self, buffTip, spriteBatch, originX, originY);
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalBuff self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            ref Terraria.DataStructures.BuffDrawParams drawParams
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalBuff self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            ref Terraria.DataStructures.BuffDrawParams drawParams
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, spriteBatch, type, buffIndex, ref drawParams);
            }

            return result;
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalBuff self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            Terraria.DataStructures.BuffDrawParams drawParams
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalBuff self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            Terraria.DataStructures.BuffDrawParams drawParams
        )
        {
            Event?.Invoke(self, spriteBatch, type, buffIndex, drawParams);
        }
    }

    public sealed partial class RightClick
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            int buffIndex
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalBuff self,
            int type,
            int buffIndex
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, type, buffIndex);
            }

            return result;
        }
    }
}

public sealed partial class GlobalBuffImpl : Terraria.ModLoader.GlobalBuff
{
    public override void Update(
        int type,
        Terraria.Player player,
        ref int buffIndex
    )
    {
        if (!GlobalBuffHooks.Update_int_Player_int.GetInvocationList().Any())
        {
            base.Update(
                type,
                player,
                ref buffIndex
            );
            return;
        }

        GlobalBuffHooks.Update_int_Player_int.Invoke(
            this,
            type,
            player,
            ref buffIndex
        );
    }

    public override void Update(
        int type,
        Terraria.NPC npc,
        ref int buffIndex
    )
    {
        if (!GlobalBuffHooks.Update_int_NPC_int.GetInvocationList().Any())
        {
            base.Update(
                type,
                npc,
                ref buffIndex
            );
            return;
        }

        GlobalBuffHooks.Update_int_NPC_int.Invoke(
            this,
            type,
            npc,
            ref buffIndex
        );
    }

    public override bool ReApply(
        int type,
        Terraria.Player player,
        int time,
        int buffIndex
    )
    {
        if (!GlobalBuffHooks.ReApply_int_Player_int_int.GetInvocationList().Any())
        {
            return base.ReApply(
                type,
                player,
                time,
                buffIndex
            );
        }

        return GlobalBuffHooks.ReApply_int_Player_int_int.Invoke(
            this,
            type,
            player,
            time,
            buffIndex
        );
    }

    public override bool ReApply(
        int type,
        Terraria.NPC npc,
        int time,
        int buffIndex
    )
    {
        if (!GlobalBuffHooks.ReApply_int_NPC_int_int.GetInvocationList().Any())
        {
            return base.ReApply(
                type,
                npc,
                time,
                buffIndex
            );
        }

        return GlobalBuffHooks.ReApply_int_NPC_int_int.Invoke(
            this,
            type,
            npc,
            time,
            buffIndex
        );
    }

    public override void ModifyBuffText(
        int type,
        ref string buffName,
        ref string tip,
        ref int rare
    )
    {
        if (!GlobalBuffHooks.ModifyBuffText.GetInvocationList().Any())
        {
            base.ModifyBuffText(
                type,
                ref buffName,
                ref tip,
                ref rare
            );
            return;
        }

        GlobalBuffHooks.ModifyBuffText.Invoke(
            this,
            type,
            ref buffName,
            ref tip,
            ref rare
        );
    }

    public override void CustomBuffTipSize(
        string buffTip,
        System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> sizes
    )
    {
        if (!GlobalBuffHooks.CustomBuffTipSize.GetInvocationList().Any())
        {
            base.CustomBuffTipSize(
                buffTip,
                sizes
            );
            return;
        }

        GlobalBuffHooks.CustomBuffTipSize.Invoke(
            this,
            buffTip,
            sizes
        );
    }

    public override void DrawCustomBuffTip(
        string buffTip,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        int originX,
        int originY
    )
    {
        if (!GlobalBuffHooks.DrawCustomBuffTip.GetInvocationList().Any())
        {
            base.DrawCustomBuffTip(
                buffTip,
                spriteBatch,
                originX,
                originY
            );
            return;
        }

        GlobalBuffHooks.DrawCustomBuffTip.Invoke(
            this,
            buffTip,
            spriteBatch,
            originX,
            originY
        );
    }

    public override bool PreDraw(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        int type,
        int buffIndex,
        ref Terraria.DataStructures.BuffDrawParams drawParams
    )
    {
        if (!GlobalBuffHooks.PreDraw.GetInvocationList().Any())
        {
            return base.PreDraw(
                spriteBatch,
                type,
                buffIndex,
                ref drawParams
            );
        }

        return GlobalBuffHooks.PreDraw.Invoke(
            this,
            spriteBatch,
            type,
            buffIndex,
            ref drawParams
        );
    }

    public override void PostDraw(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        int type,
        int buffIndex,
        Terraria.DataStructures.BuffDrawParams drawParams
    )
    {
        if (!GlobalBuffHooks.PostDraw.GetInvocationList().Any())
        {
            base.PostDraw(
                spriteBatch,
                type,
                buffIndex,
                drawParams
            );
            return;
        }

        GlobalBuffHooks.PostDraw.Invoke(
            this,
            spriteBatch,
            type,
            buffIndex,
            drawParams
        );
    }

    public override bool RightClick(
        int type,
        int buffIndex
    )
    {
        if (!GlobalBuffHooks.RightClick.GetInvocationList().Any())
        {
            return base.RightClick(
                type,
                buffIndex
            );
        }

        return GlobalBuffHooks.RightClick.Invoke(
            this,
            type,
            buffIndex
        );
    }
}

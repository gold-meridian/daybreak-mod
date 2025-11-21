namespace Daybreak.Common.Features.Hooks;

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
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class Update_int_Player_intAttribute : SubscribesToAttribute<Update_int_Player_int>;

    public sealed partial class Update_int_Player_int
    {
        public delegate void Original(
            int type,
            Terraria.Player player,
            ref int buffIndex
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.Player player,
            ref int buffIndex
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_Update_int_Player_int_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::Update_int_Player_int")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::Update_int_Player_int; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class Update_int_NPC_intAttribute : SubscribesToAttribute<Update_int_NPC_int>;

    public sealed partial class Update_int_NPC_int
    {
        public delegate void Original(
            int type,
            Terraria.NPC npc,
            ref int buffIndex
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.NPC npc,
            ref int buffIndex
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_Update_int_NPC_int_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::Update_int_NPC_int")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::Update_int_NPC_int; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ReApply_int_Player_int_intAttribute : SubscribesToAttribute<ReApply_int_Player_int_int>;

    public sealed partial class ReApply_int_Player_int_int
    {
        public delegate bool Original(
            int type,
            Terraria.Player player,
            int time,
            int buffIndex
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.Player player,
            int time,
            int buffIndex
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_ReApply_int_Player_int_int_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::ReApply_int_Player_int_int")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::ReApply_int_Player_int_int; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ReApply_int_NPC_int_intAttribute : SubscribesToAttribute<ReApply_int_NPC_int_int>;

    public sealed partial class ReApply_int_NPC_int_int
    {
        public delegate bool Original(
            int type,
            Terraria.NPC npc,
            int time,
            int buffIndex
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            int type,
            Terraria.NPC npc,
            int time,
            int buffIndex
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_ReApply_int_NPC_int_int_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::ReApply_int_NPC_int_int")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::ReApply_int_NPC_int_int; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class ModifyBuffTextAttribute : SubscribesToAttribute<ModifyBuffText>;

    public sealed partial class ModifyBuffText
    {
        public delegate void Original(
            int type,
            ref string buffName,
            ref string tip,
            ref int rare
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            int type,
            ref string buffName,
            ref string tip,
            ref int rare
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_ModifyBuffText_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::ModifyBuffText")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::ModifyBuffText; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class CustomBuffTipSizeAttribute : SubscribesToAttribute<CustomBuffTipSize>;

    public sealed partial class CustomBuffTipSize
    {
        public delegate void Original(
            string buffTip,
            System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> sizes
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            string buffTip,
            System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> sizes
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_CustomBuffTipSize_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::CustomBuffTipSize")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::CustomBuffTipSize; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class DrawCustomBuffTipAttribute : SubscribesToAttribute<DrawCustomBuffTip>;

    public sealed partial class DrawCustomBuffTip
    {
        public delegate void Original(
            string buffTip,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int originX,
            int originY
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            string buffTip,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int originX,
            int originY
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_DrawCustomBuffTip_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::DrawCustomBuffTip")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::DrawCustomBuffTip; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PreDrawAttribute : SubscribesToAttribute<PreDraw>;

    public sealed partial class PreDraw
    {
        public delegate bool Original(
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            ref Terraria.DataStructures.BuffDrawParams drawParams
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            ref Terraria.DataStructures.BuffDrawParams drawParams
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_PreDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::PreDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::PreDraw; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PostDrawAttribute : SubscribesToAttribute<PostDraw>;

    public sealed partial class PostDraw
    {
        public delegate void Original(
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            Terraria.DataStructures.BuffDrawParams drawParams
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            int type,
            int buffIndex,
            Terraria.DataStructures.BuffDrawParams drawParams
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_PostDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::PostDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::PostDraw; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class RightClickAttribute : SubscribesToAttribute<RightClick>;

    public sealed partial class RightClick
    {
        public delegate bool Original(
            int type,
            int buffIndex
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBuff self,
            int type,
            int buffIndex
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBuff_RightClick_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBuff::RightClick")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBuff::RightClick; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_Update_int_Player_int_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.Update_int_Player_int.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_Update_int_Player_int_Impl(GlobalBuffHooks.Update_int_Player_int.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void Update(
        int type,
        Terraria.Player player,
        ref int buffIndex
    )
    {
        hook(
            (
                int type_captured,
                Terraria.Player player_captured,
                ref int buffIndex_captured
            ) => base.Update(
                type_captured,
                player_captured,
                ref buffIndex_captured
            ),
            this,
            type,
            player,
            ref buffIndex
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_Update_int_NPC_int_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.Update_int_NPC_int.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_Update_int_NPC_int_Impl(GlobalBuffHooks.Update_int_NPC_int.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void Update(
        int type,
        Terraria.NPC npc,
        ref int buffIndex
    )
    {
        hook(
            (
                int type_captured,
                Terraria.NPC npc_captured,
                ref int buffIndex_captured
            ) => base.Update(
                type_captured,
                npc_captured,
                ref buffIndex_captured
            ),
            this,
            type,
            npc,
            ref buffIndex
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_ReApply_int_Player_int_int_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.ReApply_int_Player_int_int.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_ReApply_int_Player_int_int_Impl(GlobalBuffHooks.ReApply_int_Player_int_int.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ReApply(
        int type,
        Terraria.Player player,
        int time,
        int buffIndex
    )
    {
        return hook(
            (
                int type_captured,
                Terraria.Player player_captured,
                int time_captured,
                int buffIndex_captured
            ) => base.ReApply(
                type_captured,
                player_captured,
                time_captured,
                buffIndex_captured
            ),
            this,
            type,
            player,
            time,
            buffIndex
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_ReApply_int_NPC_int_int_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.ReApply_int_NPC_int_int.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_ReApply_int_NPC_int_int_Impl(GlobalBuffHooks.ReApply_int_NPC_int_int.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool ReApply(
        int type,
        Terraria.NPC npc,
        int time,
        int buffIndex
    )
    {
        return hook(
            (
                int type_captured,
                Terraria.NPC npc_captured,
                int time_captured,
                int buffIndex_captured
            ) => base.ReApply(
                type_captured,
                npc_captured,
                time_captured,
                buffIndex_captured
            ),
            this,
            type,
            npc,
            time,
            buffIndex
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_ModifyBuffText_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.ModifyBuffText.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_ModifyBuffText_Impl(GlobalBuffHooks.ModifyBuffText.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void ModifyBuffText(
        int type,
        ref string buffName,
        ref string tip,
        ref int rare
    )
    {
        hook(
            (
                int type_captured,
                ref string buffName_captured,
                ref string tip_captured,
                ref int rare_captured
            ) => base.ModifyBuffText(
                type_captured,
                ref buffName_captured,
                ref tip_captured,
                ref rare_captured
            ),
            this,
            type,
            ref buffName,
            ref tip,
            ref rare
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_CustomBuffTipSize_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.CustomBuffTipSize.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_CustomBuffTipSize_Impl(GlobalBuffHooks.CustomBuffTipSize.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void CustomBuffTipSize(
        string buffTip,
        System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> sizes
    )
    {
        hook(
            (
                string buffTip_captured,
                System.Collections.Generic.List<Microsoft.Xna.Framework.Vector2> sizes_captured
            ) => base.CustomBuffTipSize(
                buffTip_captured,
                sizes_captured
            ),
            this,
            buffTip,
            sizes
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_DrawCustomBuffTip_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.DrawCustomBuffTip.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_DrawCustomBuffTip_Impl(GlobalBuffHooks.DrawCustomBuffTip.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void DrawCustomBuffTip(
        string buffTip,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        int originX,
        int originY
    )
    {
        hook(
            (
                string buffTip_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                int originX_captured,
                int originY_captured
            ) => base.DrawCustomBuffTip(
                buffTip_captured,
                spriteBatch_captured,
                originX_captured,
                originY_captured
            ),
            this,
            buffTip,
            spriteBatch,
            originX,
            originY
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_PreDraw_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.PreDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_PreDraw_Impl(GlobalBuffHooks.PreDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDraw(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        int type,
        int buffIndex,
        ref Terraria.DataStructures.BuffDrawParams drawParams
    )
    {
        return hook(
            (
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                int type_captured,
                int buffIndex_captured,
                ref Terraria.DataStructures.BuffDrawParams drawParams_captured
            ) => base.PreDraw(
                spriteBatch_captured,
                type_captured,
                buffIndex_captured,
                ref drawParams_captured
            ),
            this,
            spriteBatch,
            type,
            buffIndex,
            ref drawParams
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_PostDraw_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.PostDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_PostDraw_Impl(GlobalBuffHooks.PostDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDraw(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        int type,
        int buffIndex,
        Terraria.DataStructures.BuffDrawParams drawParams
    )
    {
        hook(
            (
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                int type_captured,
                int buffIndex_captured,
                Terraria.DataStructures.BuffDrawParams drawParams_captured
            ) => base.PostDraw(
                spriteBatch_captured,
                type_captured,
                buffIndex_captured,
                drawParams_captured
            ),
            this,
            spriteBatch,
            type,
            buffIndex,
            drawParams
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBuff_RightClick_Impl : Terraria.ModLoader.GlobalBuff
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBuffHooks.RightClick.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBuff_RightClick_Impl(GlobalBuffHooks.RightClick.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool RightClick(
        int type,
        int buffIndex
    )
    {
        return hook(
            (
                int type_captured,
                int buffIndex_captured
            ) => base.RightClick(
                type_captured,
                buffIndex_captured
            ),
            this,
            type,
            buffIndex
        );
    }
}
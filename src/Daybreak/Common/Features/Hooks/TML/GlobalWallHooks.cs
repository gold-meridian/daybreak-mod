namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalWall':
//     System.Boolean Terraria.ModLoader.GlobalWall::Drop(System.Int32,System.Int32,System.Int32,System.Int32&)
//     System.Void Terraria.ModLoader.GlobalWall::KillWall(System.Int32,System.Int32,System.Int32,System.Boolean&)
//     System.Boolean Terraria.ModLoader.GlobalWall::WallFrame(System.Int32,System.Int32,System.Int32,System.Boolean,System.Int32&,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalWall::CanBeTeleportedTo(System.Int32,System.Int32,System.Int32,Terraria.Player,System.String)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::KillSound(System.Int32,System.Int32,System.Int32,System.Boolean)
//     System.Void Terraria.ModLoader.GlobalBlockType::NumDust(System.Int32,System.Int32,System.Int32,System.Boolean,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::CreateDust(System.Int32,System.Int32,System.Int32,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::CanPlace(System.Int32,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::CanExplode(System.Int32,System.Int32,System.Int32)
//     System.Boolean Terraria.ModLoader.GlobalBlockType::PreDraw(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)
//     System.Void Terraria.ModLoader.GlobalBlockType::PostDraw(System.Int32,System.Int32,System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch)
//     System.Void Terraria.ModLoader.GlobalBlockType::RandomUpdate(System.Int32,System.Int32,System.Int32)
//     System.Void Terraria.ModLoader.GlobalBlockType::PlaceInWorld(System.Int32,System.Int32,System.Int32,Terraria.Item)
//     System.Void Terraria.ModLoader.GlobalBlockType::ModifyLight(System.Int32,System.Int32,System.Int32,System.Single&,System.Single&,System.Single&)
public static partial class GlobalWallHooks
{
    public sealed partial class Drop
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            ref int dropType
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            ref int dropType
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_Drop_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::Drop")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::Drop; use a flag to disable behavior.");
        }
    }

    public sealed partial class KillWall
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            ref bool fail
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            ref bool fail
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_KillWall_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::KillWall")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::KillWall; use a flag to disable behavior.");
        }
    }

    public sealed partial class WallFrame
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            bool randomizeFrame,
            ref int style,
            ref int frameNumber
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            bool randomizeFrame,
            ref int style,
            ref int frameNumber
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_WallFrame_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::WallFrame")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::WallFrame; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanBeTeleportedTo
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            Terraria.Player player,
            string context
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            Terraria.Player player,
            string context
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_CanBeTeleportedTo_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::CanBeTeleportedTo")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::CanBeTeleportedTo; use a flag to disable behavior.");
        }
    }

    public sealed partial class KillSound
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            bool fail
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            bool fail
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_KillSound_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::KillSound")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::KillSound; use a flag to disable behavior.");
        }
    }

    public sealed partial class NumDust
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            bool fail,
            ref int num
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            bool fail,
            ref int num
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_NumDust_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::NumDust")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::NumDust; use a flag to disable behavior.");
        }
    }

    public sealed partial class CreateDust
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            ref int dustType
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            ref int dustType
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_CreateDust_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::CreateDust")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::CreateDust; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanPlace
    {
        public delegate bool Original(
            int i,
            int j,
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_CanPlace_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::CanPlace")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::CanPlace; use a flag to disable behavior.");
        }
    }

    public sealed partial class CanExplode
    {
        public delegate bool Original(
            int i,
            int j,
            int type
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_CanExplode_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::CanExplode")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::CanExplode; use a flag to disable behavior.");
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_PreDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::PreDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::PreDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_PostDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::PostDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::PostDraw; use a flag to disable behavior.");
        }
    }

    public sealed partial class RandomUpdate
    {
        public delegate void Original(
            int i,
            int j,
            int type
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_RandomUpdate_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::RandomUpdate")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::RandomUpdate; use a flag to disable behavior.");
        }
    }

    public sealed partial class PlaceInWorld
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            Terraria.Item item
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            Terraria.Item item
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_PlaceInWorld_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::PlaceInWorld")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::PlaceInWorld; use a flag to disable behavior.");
        }
    }

    public sealed partial class ModifyLight
    {
        public delegate void Original(
            int i,
            int j,
            int type,
            ref float r,
            ref float g,
            ref float b
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBlockType self,
            int i,
            int j,
            int type,
            ref float r,
            ref float g,
            ref float b
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalWall_ModifyLight_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalWall::ModifyLight")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalWall::ModifyLight; use a flag to disable behavior.");
        }
    }
}

public sealed partial class GlobalWall_Drop_Impl(GlobalWallHooks.Drop.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool Drop(
        int i,
        int j,
        int type,
        ref int dropType
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref int dropType_captured
            ) => base.Drop(
                i_captured,
                j_captured,
                type_captured,
                ref dropType_captured
            ),
            this,
            i,
            j,
            type,
            ref dropType
        );
    }
}

public sealed partial class GlobalWall_KillWall_Impl(GlobalWallHooks.KillWall.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override void KillWall(
        int i,
        int j,
        int type,
        ref bool fail
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref bool fail_captured
            ) => base.KillWall(
                i_captured,
                j_captured,
                type_captured,
                ref fail_captured
            ),
            this,
            i,
            j,
            type,
            ref fail
        );
    }
}

public sealed partial class GlobalWall_WallFrame_Impl(GlobalWallHooks.WallFrame.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool WallFrame(
        int i,
        int j,
        int type,
        bool randomizeFrame,
        ref int style,
        ref int frameNumber
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                bool randomizeFrame_captured,
                ref int style_captured,
                ref int frameNumber_captured
            ) => base.WallFrame(
                i_captured,
                j_captured,
                type_captured,
                randomizeFrame_captured,
                ref style_captured,
                ref frameNumber_captured
            ),
            this,
            i,
            j,
            type,
            randomizeFrame,
            ref style,
            ref frameNumber
        );
    }
}

public sealed partial class GlobalWall_CanBeTeleportedTo_Impl(GlobalWallHooks.CanBeTeleportedTo.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool CanBeTeleportedTo(
        int i,
        int j,
        int type,
        Terraria.Player player,
        string context
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Terraria.Player player_captured,
                string context_captured
            ) => base.CanBeTeleportedTo(
                i_captured,
                j_captured,
                type_captured,
                player_captured,
                context_captured
            ),
            this,
            i,
            j,
            type,
            player,
            context
        );
    }
}

public sealed partial class GlobalWall_KillSound_Impl(GlobalWallHooks.KillSound.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool KillSound(
        int i,
        int j,
        int type,
        bool fail
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                bool fail_captured
            ) => base.KillSound(
                i_captured,
                j_captured,
                type_captured,
                fail_captured
            ),
            this,
            i,
            j,
            type,
            fail
        );
    }
}

public sealed partial class GlobalWall_NumDust_Impl(GlobalWallHooks.NumDust.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override void NumDust(
        int i,
        int j,
        int type,
        bool fail,
        ref int num
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                bool fail_captured,
                ref int num_captured
            ) => base.NumDust(
                i_captured,
                j_captured,
                type_captured,
                fail_captured,
                ref num_captured
            ),
            this,
            i,
            j,
            type,
            fail,
            ref num
        );
    }
}

public sealed partial class GlobalWall_CreateDust_Impl(GlobalWallHooks.CreateDust.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool CreateDust(
        int i,
        int j,
        int type,
        ref int dustType
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref int dustType_captured
            ) => base.CreateDust(
                i_captured,
                j_captured,
                type_captured,
                ref dustType_captured
            ),
            this,
            i,
            j,
            type,
            ref dustType
        );
    }
}

public sealed partial class GlobalWall_CanPlace_Impl(GlobalWallHooks.CanPlace.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool CanPlace(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.CanPlace(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

public sealed partial class GlobalWall_CanExplode_Impl(GlobalWallHooks.CanExplode.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool CanExplode(
        int i,
        int j,
        int type
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.CanExplode(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

public sealed partial class GlobalWall_PreDraw_Impl(GlobalWallHooks.PreDraw.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override bool PreDraw(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        return hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured
            ) => base.PreDraw(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch
        );
    }
}

public sealed partial class GlobalWall_PostDraw_Impl(GlobalWallHooks.PostDraw.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override void PostDraw(
        int i,
        int j,
        int type,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured
            ) => base.PostDraw(
                i_captured,
                j_captured,
                type_captured,
                spriteBatch_captured
            ),
            this,
            i,
            j,
            type,
            spriteBatch
        );
    }
}

public sealed partial class GlobalWall_RandomUpdate_Impl(GlobalWallHooks.RandomUpdate.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override void RandomUpdate(
        int i,
        int j,
        int type
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured
            ) => base.RandomUpdate(
                i_captured,
                j_captured,
                type_captured
            ),
            this,
            i,
            j,
            type
        );
    }
}

public sealed partial class GlobalWall_PlaceInWorld_Impl(GlobalWallHooks.PlaceInWorld.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override void PlaceInWorld(
        int i,
        int j,
        int type,
        Terraria.Item item
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                Terraria.Item item_captured
            ) => base.PlaceInWorld(
                i_captured,
                j_captured,
                type_captured,
                item_captured
            ),
            this,
            i,
            j,
            type,
            item
        );
    }
}

public sealed partial class GlobalWall_ModifyLight_Impl(GlobalWallHooks.ModifyLight.Definition hook) : Terraria.ModLoader.GlobalWall
{
    public override string Name => base.Name + '_' + System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));

    public override void ModifyLight(
        int i,
        int j,
        int type,
        ref float r,
        ref float g,
        ref float b
    )
    {
        hook(
            (
                int i_captured,
                int j_captured,
                int type_captured,
                ref float r_captured,
                ref float g_captured,
                ref float b_captured
            ) => base.ModifyLight(
                i_captured,
                j_captured,
                type_captured,
                ref r_captured,
                ref g_captured,
                ref b_captured
            ),
            this,
            i,
            j,
            type,
            ref r,
            ref g,
            ref b
        );
    }
}
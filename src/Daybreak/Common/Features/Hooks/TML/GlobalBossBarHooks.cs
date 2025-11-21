namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalBossBar':
//     System.Boolean Terraria.ModLoader.GlobalBossBar::PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.NPC,Terraria.DataStructures.BossBarDrawParams&)
//     System.Void Terraria.ModLoader.GlobalBossBar::PostDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.NPC,Terraria.DataStructures.BossBarDrawParams)
public static partial class GlobalBossBarHooks
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PreDrawAttribute : SubscribesToAttribute<PreDraw>;

    public sealed partial class PreDraw
    {
        public delegate bool Original(
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.NPC npc,
            ref Terraria.DataStructures.BossBarDrawParams drawParams
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalBossBar self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.NPC npc,
            ref Terraria.DataStructures.BossBarDrawParams drawParams
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBossBar_PreDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBossBar::PreDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBossBar::PreDraw; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PostDrawAttribute : SubscribesToAttribute<PostDraw>;

    public sealed partial class PostDraw
    {
        public delegate void Original(
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.NPC npc,
            Terraria.DataStructures.BossBarDrawParams drawParams
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalBossBar self,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.NPC npc,
            Terraria.DataStructures.BossBarDrawParams drawParams
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalBossBar_PostDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalBossBar::PostDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalBossBar::PostDraw; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBossBar_PreDraw_Impl : Terraria.ModLoader.GlobalBossBar
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBossBarHooks.PreDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBossBar_PreDraw_Impl(GlobalBossBarHooks.PreDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDraw(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Terraria.NPC npc,
        ref Terraria.DataStructures.BossBarDrawParams drawParams
    )
    {
        return hook(
            (
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Terraria.NPC npc_captured,
                ref Terraria.DataStructures.BossBarDrawParams drawParams_captured
            ) => base.PreDraw(
                spriteBatch_captured,
                npc_captured,
                ref drawParams_captured
            ),
            this,
            spriteBatch,
            npc,
            ref drawParams
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalBossBar_PostDraw_Impl : Terraria.ModLoader.GlobalBossBar
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalBossBarHooks.PostDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public GlobalBossBar_PostDraw_Impl(GlobalBossBarHooks.PostDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDraw(
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Terraria.NPC npc,
        Terraria.DataStructures.BossBarDrawParams drawParams
    )
    {
        hook(
            (
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Terraria.NPC npc_captured,
                Terraria.DataStructures.BossBarDrawParams drawParams_captured
            ) => base.PostDraw(
                spriteBatch_captured,
                npc_captured,
                drawParams_captured
            ),
            this,
            spriteBatch,
            npc,
            drawParams
        );
    }
}
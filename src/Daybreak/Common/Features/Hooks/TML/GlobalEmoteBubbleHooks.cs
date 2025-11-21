namespace Daybreak.Common.Features.Hooks;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeDefaultValueWhenTypeNotEvident
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

// Hooks to generate for 'Terraria.ModLoader.GlobalEmoteBubble':
//     System.Void Terraria.ModLoader.GlobalEmoteBubble::OnSpawn(Terraria.GameContent.UI.EmoteBubble)
//     System.Boolean Terraria.ModLoader.GlobalEmoteBubble::UpdateFrame(Terraria.GameContent.UI.EmoteBubble)
//     System.Boolean Terraria.ModLoader.GlobalEmoteBubble::UpdateFrameInEmoteMenu(System.Int32,System.Int32&)
//     System.Boolean Terraria.ModLoader.GlobalEmoteBubble::PreDraw(Terraria.GameContent.UI.EmoteBubble,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Graphics.Texture2D,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Graphics.SpriteEffects)
//     System.Void Terraria.ModLoader.GlobalEmoteBubble::PostDraw(Terraria.GameContent.UI.EmoteBubble,Microsoft.Xna.Framework.Graphics.SpriteBatch,Microsoft.Xna.Framework.Graphics.Texture2D,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Graphics.SpriteEffects)
//     System.Boolean Terraria.ModLoader.GlobalEmoteBubble::PreDrawInEmoteMenu(System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.GameContent.UI.Elements.EmoteButton,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Vector2)
//     System.Void Terraria.ModLoader.GlobalEmoteBubble::PostDrawInEmoteMenu(System.Int32,Microsoft.Xna.Framework.Graphics.SpriteBatch,Terraria.GameContent.UI.Elements.EmoteButton,Microsoft.Xna.Framework.Vector2,Microsoft.Xna.Framework.Rectangle,Microsoft.Xna.Framework.Vector2)
//     System.Nullable`1<Microsoft.Xna.Framework.Rectangle> Terraria.ModLoader.GlobalEmoteBubble::GetFrame(Terraria.GameContent.UI.EmoteBubble)
//     System.Nullable`1<Microsoft.Xna.Framework.Rectangle> Terraria.ModLoader.GlobalEmoteBubble::GetFrameInEmoteMenu(System.Int32,System.Int32,System.Int32)
public static partial class GlobalEmoteBubbleHooks
{
    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class OnSpawnAttribute : SubscribesToAttribute<OnSpawn>;

    public sealed partial class OnSpawn
    {
        public delegate void Original(
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_OnSpawn_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::OnSpawn")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::OnSpawn; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class UpdateFrameAttribute : SubscribesToAttribute<UpdateFrame>;

    public sealed partial class UpdateFrame
    {
        public delegate bool Original(
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_UpdateFrame_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::UpdateFrame")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::UpdateFrame; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class UpdateFrameInEmoteMenuAttribute : SubscribesToAttribute<UpdateFrameInEmoteMenu>;

    public sealed partial class UpdateFrameInEmoteMenu
    {
        public delegate bool Original(
            int emoteType,
            ref int frameCounter
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            ref int frameCounter
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_UpdateFrameInEmoteMenu_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::UpdateFrameInEmoteMenu")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::UpdateFrameInEmoteMenu; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PreDrawAttribute : SubscribesToAttribute<PreDraw>;

    public sealed partial class PreDraw
    {
        public delegate bool Original(
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_PreDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::PreDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::PreDraw; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PostDrawAttribute : SubscribesToAttribute<PostDraw>;

    public sealed partial class PostDraw
    {
        public delegate void Original(
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_PostDraw_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::PostDraw")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::PostDraw; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PreDrawInEmoteMenuAttribute : SubscribesToAttribute<PreDrawInEmoteMenu>;

    public sealed partial class PreDrawInEmoteMenu
    {
        public delegate bool Original(
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        );

        public delegate bool Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_PreDrawInEmoteMenu_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::PreDrawInEmoteMenu")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::PreDrawInEmoteMenu; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class PostDrawInEmoteMenuAttribute : SubscribesToAttribute<PostDrawInEmoteMenu>;

    public sealed partial class PostDrawInEmoteMenu
    {
        public delegate void Original(
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        );

        public delegate void Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_PostDrawInEmoteMenu_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::PostDrawInEmoteMenu")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::PostDrawInEmoteMenu; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class GetFrameAttribute : SubscribesToAttribute<GetFrame>;

    public sealed partial class GetFrame
    {
        public delegate Microsoft.Xna.Framework.Rectangle? Original(
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public delegate Microsoft.Xna.Framework.Rectangle? Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_GetFrame_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::GetFrame")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::GetFrame; use a flag to disable behavior.");
        }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class GetFrameInEmoteMenuAttribute : SubscribesToAttribute<GetFrameInEmoteMenu>;

    public sealed partial class GetFrameInEmoteMenu
    {
        public delegate Microsoft.Xna.Framework.Rectangle? Original(
            int emoteType,
            int frame,
            int frameCounter
        );

        public delegate Microsoft.Xna.Framework.Rectangle? Definition(
            Original orig,
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            int frame,
            int frameCounter
        );

        public static event Definition? Event
        {
            add => HookLoader.GetModOrThrow().AddContent(new GlobalEmoteBubble_GetFrameInEmoteMenu_Impl(value ?? throw new System.InvalidOperationException("Cannot subscribe to a DAYBREAK-generated mod loader hook with a null value: GlobalEmoteBubble::GetFrameInEmoteMenu")));

            remove => throw new System.InvalidOperationException("Cannot remove DAYBREAK-generated mod loader hook: GlobalEmoteBubble::GetFrameInEmoteMenu; use a flag to disable behavior.");
        }
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_OnSpawn_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.OnSpawn.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_OnSpawn_Impl(GlobalEmoteBubbleHooks.OnSpawn.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void OnSpawn(
        Terraria.GameContent.UI.EmoteBubble emoteBubble
    )
    {
        hook(
            (
                Terraria.GameContent.UI.EmoteBubble emoteBubble_captured
            ) => base.OnSpawn(
                emoteBubble_captured
            ),
            this,
            emoteBubble
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_UpdateFrame_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.UpdateFrame.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_UpdateFrame_Impl(GlobalEmoteBubbleHooks.UpdateFrame.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool UpdateFrame(
        Terraria.GameContent.UI.EmoteBubble emoteBubble
    )
    {
        return hook(
            (
                Terraria.GameContent.UI.EmoteBubble emoteBubble_captured
            ) => base.UpdateFrame(
                emoteBubble_captured
            ),
            this,
            emoteBubble
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_UpdateFrameInEmoteMenu_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.UpdateFrameInEmoteMenu.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_UpdateFrameInEmoteMenu_Impl(GlobalEmoteBubbleHooks.UpdateFrameInEmoteMenu.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool UpdateFrameInEmoteMenu(
        int emoteType,
        ref int frameCounter
    )
    {
        return hook(
            (
                int emoteType_captured,
                ref int frameCounter_captured
            ) => base.UpdateFrameInEmoteMenu(
                emoteType_captured,
                ref frameCounter_captured
            ),
            this,
            emoteType,
            ref frameCounter
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_PreDraw_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.PreDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_PreDraw_Impl(GlobalEmoteBubbleHooks.PreDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDraw(
        Terraria.GameContent.UI.EmoteBubble emoteBubble,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Graphics.Texture2D texture,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Vector2 origin,
        Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        return hook(
            (
                Terraria.GameContent.UI.EmoteBubble emoteBubble_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Graphics.Texture2D texture_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Rectangle frame_captured,
                Microsoft.Xna.Framework.Vector2 origin_captured,
                Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.PreDraw(
                emoteBubble_captured,
                spriteBatch_captured,
                texture_captured,
                position_captured,
                frame_captured,
                origin_captured,
                spriteEffects_captured
            ),
            this,
            emoteBubble,
            spriteBatch,
            texture,
            position,
            frame,
            origin,
            spriteEffects
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_PostDraw_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.PostDraw.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_PostDraw_Impl(GlobalEmoteBubbleHooks.PostDraw.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDraw(
        Terraria.GameContent.UI.EmoteBubble emoteBubble,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Microsoft.Xna.Framework.Graphics.Texture2D texture,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Vector2 origin,
        Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
    )
    {
        hook(
            (
                Terraria.GameContent.UI.EmoteBubble emoteBubble_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Microsoft.Xna.Framework.Graphics.Texture2D texture_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Rectangle frame_captured,
                Microsoft.Xna.Framework.Vector2 origin_captured,
                Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects_captured
            ) => base.PostDraw(
                emoteBubble_captured,
                spriteBatch_captured,
                texture_captured,
                position_captured,
                frame_captured,
                origin_captured,
                spriteEffects_captured
            ),
            this,
            emoteBubble,
            spriteBatch,
            texture,
            position,
            frame,
            origin,
            spriteEffects
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_PreDrawInEmoteMenu_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.PreDrawInEmoteMenu.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_PreDrawInEmoteMenu_Impl(GlobalEmoteBubbleHooks.PreDrawInEmoteMenu.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override bool PreDrawInEmoteMenu(
        int emoteType,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Vector2 origin
    )
    {
        return hook(
            (
                int emoteType_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Rectangle frame_captured,
                Microsoft.Xna.Framework.Vector2 origin_captured
            ) => base.PreDrawInEmoteMenu(
                emoteType_captured,
                spriteBatch_captured,
                uiEmoteButton_captured,
                position_captured,
                frame_captured,
                origin_captured
            ),
            this,
            emoteType,
            spriteBatch,
            uiEmoteButton,
            position,
            frame,
            origin
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_PostDrawInEmoteMenu_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.PostDrawInEmoteMenu.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_PostDrawInEmoteMenu_Impl(GlobalEmoteBubbleHooks.PostDrawInEmoteMenu.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override void PostDrawInEmoteMenu(
        int emoteType,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Vector2 origin
    )
    {
        hook(
            (
                int emoteType_captured,
                Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch_captured,
                Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton_captured,
                Microsoft.Xna.Framework.Vector2 position_captured,
                Microsoft.Xna.Framework.Rectangle frame_captured,
                Microsoft.Xna.Framework.Vector2 origin_captured
            ) => base.PostDrawInEmoteMenu(
                emoteType_captured,
                spriteBatch_captured,
                uiEmoteButton_captured,
                position_captured,
                frame_captured,
                origin_captured
            ),
            this,
            emoteType,
            spriteBatch,
            uiEmoteButton,
            position,
            frame,
            origin
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_GetFrame_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.GetFrame.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_GetFrame_Impl(GlobalEmoteBubbleHooks.GetFrame.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override Microsoft.Xna.Framework.Rectangle? GetFrame(
        Terraria.GameContent.UI.EmoteBubble emoteBubble
    )
    {
        return hook(
            (
                Terraria.GameContent.UI.EmoteBubble emoteBubble_captured
            ) => base.GetFrame(
                emoteBubble_captured
            ),
            this,
            emoteBubble
        );
    }
}

[Terraria.ModLoader.Autoload(false)]
public sealed partial class GlobalEmoteBubble_GetFrameInEmoteMenu_Impl : Terraria.ModLoader.GlobalEmoteBubble
{
    [field: Terraria.ModLoader.CloneByReference]
    private readonly GlobalEmoteBubbleHooks.GetFrameInEmoteMenu.Definition hook;

    [field: Terraria.ModLoader.CloneByReference]
    public override string Name => base.Name + '_' + field;

    public override bool InstancePerEntity => true;

    protected override bool CloneNewInstances => true;

    public GlobalEmoteBubble_GetFrameInEmoteMenu_Impl(GlobalEmoteBubbleHooks.GetFrameInEmoteMenu.Definition hook)
    {
        this.hook = hook;
        Name = System.Convert.ToBase64String(System.BitConverter.GetBytes(System.DateTime.Now.Ticks));
    }

    public override Microsoft.Xna.Framework.Rectangle? GetFrameInEmoteMenu(
        int emoteType,
        int frame,
        int frameCounter
    )
    {
        return hook(
            (
                int emoteType_captured,
                int frame_captured,
                int frameCounter_captured
            ) => base.GetFrameInEmoteMenu(
                emoteType_captured,
                frame_captured,
                frameCounter_captured
            ),
            this,
            emoteType,
            frame,
            frameCounter
        );
    }
}
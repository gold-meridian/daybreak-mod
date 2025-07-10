namespace Daybreak.Common.Features.Hooks;

using System.Linq;

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
    public sealed partial class OnSpawn
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        )
        {
            Event?.Invoke(self, emoteBubble);
        }
    }

    public sealed partial class UpdateFrame
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, emoteBubble);
            }

            return result;
        }
    }

    public sealed partial class UpdateFrameInEmoteMenu
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            ref int frameCounter
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            ref int frameCounter
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, emoteType, ref frameCounter);
            }

            return result;
        }
    }

    public sealed partial class PreDraw
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, emoteBubble, spriteBatch, texture, position, frame, origin, spriteEffects);
            }

            return result;
        }
    }

    public sealed partial class PostDraw
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Graphics.Texture2D texture,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin,
            Microsoft.Xna.Framework.Graphics.SpriteEffects spriteEffects
        )
        {
            Event?.Invoke(self, emoteBubble, spriteBatch, texture, position, frame, origin, spriteEffects);
        }
    }

    public sealed partial class PreDrawInEmoteMenu
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        )
        {
            var result = true;
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                result &= handler.Invoke(self, emoteType, spriteBatch, uiEmoteButton, position, frame, origin);
            }

            return result;
        }
    }

    public sealed partial class PostDrawInEmoteMenu
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
            Microsoft.Xna.Framework.Vector2 position,
            Microsoft.Xna.Framework.Rectangle frame,
            Microsoft.Xna.Framework.Vector2 origin
        )
        {
            Event?.Invoke(self, emoteType, spriteBatch, uiEmoteButton, position, frame, origin);
        }
    }

    public sealed partial class GetFrame
    {
        public delegate Microsoft.Xna.Framework.Rectangle? Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static Microsoft.Xna.Framework.Rectangle? Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            Terraria.GameContent.UI.EmoteBubble emoteBubble
        )
        {
            var result = default(Microsoft.Xna.Framework.Rectangle?);
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                var newValue = handler.Invoke(self, emoteBubble);
                if (newValue != null)
                {
                    result = newValue;
                }
            }

            return result;
        }
    }

    public sealed partial class GetFrameInEmoteMenu
    {
        public delegate Microsoft.Xna.Framework.Rectangle? Definition(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            int frame,
            int frameCounter
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static Microsoft.Xna.Framework.Rectangle? Invoke(
            Terraria.ModLoader.GlobalEmoteBubble self,
            int emoteType,
            int frame,
            int frameCounter
        )
        {
            var result = default(Microsoft.Xna.Framework.Rectangle?);
            if (Event == null)
            {
                return result;
            }

            foreach (var handler in GetInvocationList())
            {
                var newValue = handler.Invoke(self, emoteType, frame, frameCounter);
                if (newValue != null)
                {
                    result = newValue;
                }
            }

            return result;
        }
    }
}

public sealed partial class GlobalEmoteBubbleImpl : Terraria.ModLoader.GlobalEmoteBubble
{
    public override void OnSpawn(
        Terraria.GameContent.UI.EmoteBubble emoteBubble
    )
    {
        if (!GlobalEmoteBubbleHooks.OnSpawn.GetInvocationList().Any())
        {
            base.OnSpawn(
                emoteBubble
            );
            return;
        }

        GlobalEmoteBubbleHooks.OnSpawn.Invoke(
            this,
            emoteBubble
        );
    }

    public override bool UpdateFrame(
        Terraria.GameContent.UI.EmoteBubble emoteBubble
    )
    {
        if (!GlobalEmoteBubbleHooks.UpdateFrame.GetInvocationList().Any())
        {
            return base.UpdateFrame(
                emoteBubble
            );
        }

        return GlobalEmoteBubbleHooks.UpdateFrame.Invoke(
            this,
            emoteBubble
        );
    }

    public override bool UpdateFrameInEmoteMenu(
        int emoteType,
        ref int frameCounter
    )
    {
        if (!GlobalEmoteBubbleHooks.UpdateFrameInEmoteMenu.GetInvocationList().Any())
        {
            return base.UpdateFrameInEmoteMenu(
                emoteType,
                ref frameCounter
            );
        }

        return GlobalEmoteBubbleHooks.UpdateFrameInEmoteMenu.Invoke(
            this,
            emoteType,
            ref frameCounter
        );
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
        if (!GlobalEmoteBubbleHooks.PreDraw.GetInvocationList().Any())
        {
            return base.PreDraw(
                emoteBubble,
                spriteBatch,
                texture,
                position,
                frame,
                origin,
                spriteEffects
            );
        }

        return GlobalEmoteBubbleHooks.PreDraw.Invoke(
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
        if (!GlobalEmoteBubbleHooks.PostDraw.GetInvocationList().Any())
        {
            base.PostDraw(
                emoteBubble,
                spriteBatch,
                texture,
                position,
                frame,
                origin,
                spriteEffects
            );
            return;
        }

        GlobalEmoteBubbleHooks.PostDraw.Invoke(
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

    public override bool PreDrawInEmoteMenu(
        int emoteType,
        Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
        Terraria.GameContent.UI.Elements.EmoteButton uiEmoteButton,
        Microsoft.Xna.Framework.Vector2 position,
        Microsoft.Xna.Framework.Rectangle frame,
        Microsoft.Xna.Framework.Vector2 origin
    )
    {
        if (!GlobalEmoteBubbleHooks.PreDrawInEmoteMenu.GetInvocationList().Any())
        {
            return base.PreDrawInEmoteMenu(
                emoteType,
                spriteBatch,
                uiEmoteButton,
                position,
                frame,
                origin
            );
        }

        return GlobalEmoteBubbleHooks.PreDrawInEmoteMenu.Invoke(
            this,
            emoteType,
            spriteBatch,
            uiEmoteButton,
            position,
            frame,
            origin
        );
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
        if (!GlobalEmoteBubbleHooks.PostDrawInEmoteMenu.GetInvocationList().Any())
        {
            base.PostDrawInEmoteMenu(
                emoteType,
                spriteBatch,
                uiEmoteButton,
                position,
                frame,
                origin
            );
            return;
        }

        GlobalEmoteBubbleHooks.PostDrawInEmoteMenu.Invoke(
            this,
            emoteType,
            spriteBatch,
            uiEmoteButton,
            position,
            frame,
            origin
        );
    }

    public override Microsoft.Xna.Framework.Rectangle? GetFrame(
        Terraria.GameContent.UI.EmoteBubble emoteBubble
    )
    {
        if (!GlobalEmoteBubbleHooks.GetFrame.GetInvocationList().Any())
        {
            return base.GetFrame(
                emoteBubble
            );
        }

        return GlobalEmoteBubbleHooks.GetFrame.Invoke(
            this,
            emoteBubble
        );
    }

    public override Microsoft.Xna.Framework.Rectangle? GetFrameInEmoteMenu(
        int emoteType,
        int frame,
        int frameCounter
    )
    {
        if (!GlobalEmoteBubbleHooks.GetFrameInEmoteMenu.GetInvocationList().Any())
        {
            return base.GetFrameInEmoteMenu(
                emoteType,
                frame,
                frameCounter
            );
        }

        return GlobalEmoteBubbleHooks.GetFrameInEmoteMenu.Invoke(
            this,
            emoteType,
            frame,
            frameCounter
        );
    }
}

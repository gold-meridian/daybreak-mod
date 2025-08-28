namespace Daybreak.Common.Features.Hooks;

using System.Linq;

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
public static partial class GlobalWallHooks
{
    public sealed partial class Drop
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            ref int dropType
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            ref int dropType
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, i, j, type, ref dropType))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class KillWall
    {
        public delegate void Definition(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            ref bool fail
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static void Invoke(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            ref bool fail
        )
        {
            Event?.Invoke(self, i, j, type, ref fail);
        }
    }

    public sealed partial class WallFrame
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            bool randomizeFrame,
            ref int style,
            ref int frameNumber
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            bool randomizeFrame,
            ref int style,
            ref int frameNumber
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, i, j, type, randomizeFrame, ref style, ref frameNumber))
                {
                    return false;
                }
            }

            return true;
        }
    }

    public sealed partial class CanBeTeleportedTo
    {
        public delegate bool Definition(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            Terraria.Player player,
            string context
        );

        public static event Definition? Event;

        internal static System.Collections.Generic.IEnumerable<Definition> GetInvocationList()
        {
            return Event?.GetInvocationList().Select(x => (Definition)x) ?? [];
        }

        public static bool Invoke(
            Terraria.ModLoader.GlobalWall self,
            int i,
            int j,
            int type,
            Terraria.Player player,
            string context
        )
        {
            if (Event == null)
            {
                return true;
            }

            foreach (var handler in GetInvocationList())
            {
                if (!handler.Invoke(self, i, j, type, player, context))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

public sealed partial class GlobalWallImpl : Terraria.ModLoader.GlobalWall
{
    public override bool Drop(
        int i,
        int j,
        int type,
        ref int dropType
    )
    {
        if (!GlobalWallHooks.Drop.GetInvocationList().Any())
        {
            return base.Drop(
                i,
                j,
                type,
                ref dropType
            );
        }

        return GlobalWallHooks.Drop.Invoke(
            this,
            i,
            j,
            type,
            ref dropType
        );
    }

    public override void KillWall(
        int i,
        int j,
        int type,
        ref bool fail
    )
    {
        if (!GlobalWallHooks.KillWall.GetInvocationList().Any())
        {
            base.KillWall(
                i,
                j,
                type,
                ref fail
            );
            return;
        }

        GlobalWallHooks.KillWall.Invoke(
            this,
            i,
            j,
            type,
            ref fail
        );
    }

    public override bool WallFrame(
        int i,
        int j,
        int type,
        bool randomizeFrame,
        ref int style,
        ref int frameNumber
    )
    {
        if (!GlobalWallHooks.WallFrame.GetInvocationList().Any())
        {
            return base.WallFrame(
                i,
                j,
                type,
                randomizeFrame,
                ref style,
                ref frameNumber
            );
        }

        return GlobalWallHooks.WallFrame.Invoke(
            this,
            i,
            j,
            type,
            randomizeFrame,
            ref style,
            ref frameNumber
        );
    }

    public override bool CanBeTeleportedTo(
        int i,
        int j,
        int type,
        Terraria.Player player,
        string context
    )
    {
        if (!GlobalWallHooks.CanBeTeleportedTo.GetInvocationList().Any())
        {
            return base.CanBeTeleportedTo(
                i,
                j,
                type,
                player,
                context
            );
        }

        return GlobalWallHooks.CanBeTeleportedTo.Invoke(
            this,
            i,
            j,
            type,
            player,
            context
        );
    }
}

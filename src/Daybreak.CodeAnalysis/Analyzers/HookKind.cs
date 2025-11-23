using System;
using System.Collections.Generic;
using System.Linq;

namespace Daybreak.CodeAnalysis;

public enum HookKind : byte
{
    None,

    /// <summary>
    ///     IL edits.
    /// </summary>
    IlEdit,

    /// <summary>
    ///     Detours.
    /// </summary>
    Detour,

    /// <summary>
    ///     DAYBREAK-style event subscriptions.
    /// </summary>
    Subscriber,
    
    /// <summary>
    ///     <c>OnLoad</c> attribute.
    /// </summary>
    OnLoad,
    
    /// <summary>
    ///     <c>OnUnload</c> attribute.
    /// </summary>
    OnUnload,
}

public enum HookInstancing
{
    Static,
    Instanced,
    Both,
}

public static class HookKindExtensions
{
    extension(HookKind kind)
    {
        public HookInstancing Instancing
        {
            get
            {
                return kind switch
                {
                    HookKind.None => HookInstancing.Both,
                    HookKind.IlEdit => HookInstancing.Both,
                    HookKind.Detour => HookInstancing.Both,
                    HookKind.Subscriber => HookInstancing.Both,
                    HookKind.OnLoad => HookInstancing.Both,
                    HookKind.OnUnload => HookInstancing.Both,
                    _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
                };
            }
        }

        public bool PermitsMultiple
        {
            get
            {
                return kind switch
                {
                    HookKind.None => true,
                    HookKind.IlEdit => true,
                    HookKind.Detour => false,
                    HookKind.Subscriber => false,
                    HookKind.OnLoad => false,
                    HookKind.OnUnload => false,
                    _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null),
                };
            }
        }

        public bool ValidateMultiple(IEnumerable<HookKind> kinds)
        {
            if (!kind.PermitsMultiple)
            {
                return false;
            }
            
            // TODO: We can introduce more complex solving later.  For now, only
            //       handle the case of IL edits which expect only themselves.

            return kinds.All(x => x == kind);
        }
    }
}

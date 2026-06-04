using System;
using System.Reflection;
using Mono.Cecil;
using MonoMod.Cil;
using MonoMod.Utils;

namespace Daybreak.MonoMod;

partial class Extensions
{
    extension(ILCursor c)
    {
        /// <summary>
        ///     Emits the IL to invoke the <paramref name="delegate"/> as if it
        ///     were a method.
        ///     <br />
        ///     <br />
        ///     This is an optimized variant of
        ///     <see cref="ILCursor.EmitDelegate"/> which is capable of lifting
        ///     anonymous delegates into static functions, allowing for a direct
        ///     <c>call</c> to the method without the overhead of virtualization
        ///     or hanging onto a reference to the delegate target.
        ///     <br />
        ///     This method performs the IL emission slower than
        ///     <see cref="ILCursor.EmitDelegate"/> in exchange for less
        ///     overhead in the execution of the function itself.
        ///     <br />
        ///     <br />
        ///     Validation of the emission is confirmed upon invocation of this
        ///     API, meaning it will throw an exception if a reference to the
        ///     <see langword="this"/> parameter of the
        ///     <paramref name="delegate"/> is found.
        ///     <br />
        ///     <br />
        ///     <b>
        ///         Only use this function if you have marked your
        ///         <paramref name="delegate"/> as <see langword="static"/>.
        ///         Usage of this API will prevent breakpoints, hot reloading,
        ///         and other debug functions from working within the scope of
        ///         the delegate, as its body is cloned to a new method for
        ///         emission.
        ///     </b>
        /// </summary>
        /// <param name="delegate">
        ///     The delegate to clone and emit a call to.
        /// </param>
        public void EmitStaticDelegateUnsafe(Delegate @delegate)
        {
            var compiled = DelegateLifter.LiftDelegateToStaticMethod(@delegate);
            if (compiled is null)
            {
                c.EmitDelegate(@delegate);
                return;   
            }
            
            c.EmitCall(compiled);
        }
    }
}

internal static class DelegateLifter
{
    public static MethodInfo? LiftDelegateToStaticMethod(Delegate @delegate)
    {
        using var dmd = new DynamicMethodDefinition(@delegate.Method);
        var method = dmd.Definition;

        if (method is { HasThis: false, IsStatic: true })
        {
            return null;
        }

        using (var methodCtx = new ILContext(method))
        {
            var methodCursor = new ILCursor(methodCtx);

            // Convert raw argument-referencing opcodes to their Cecil
            // "safe" variants.  MonoMod already resolves the raw index from
            // "safe" variants, so remaking them here is fine.
            while (true)
            {
                var parameterIdx = -1;
                if (methodCursor.TryGotoNext(MoveType.Before, x => x.MatchLdarg(out parameterIdx)))
                {
                    VerifyAndModifyInstruction(methodCursor, parameterIdx, methodCursor.EmitLdarg);
                    continue;
                }

                if (methodCursor.TryGotoNext(MoveType.Before, x => x.MatchLdarga(out parameterIdx)))
                {
                    VerifyAndModifyInstruction(methodCursor, parameterIdx, methodCursor.EmitLdarga);
                    continue;
                }

                if (methodCursor.TryGotoNext(MoveType.Before, x => x.MatchStarg(out parameterIdx)))
                {
                    VerifyAndModifyInstruction(methodCursor, parameterIdx, methodCursor.EmitStarg);
                    continue;
                }

                break;
            }
        }

        // Doesn't matter much where this happens as long as it's after
        // we analyze and edit the method (and before we generate the
        // new method, duh).
        method.HasThis = false;
        method.ExplicitThis = false;
        method.IsStatic = true;

        return dmd.Generate();
    }

    private static void VerifyAndModifyInstruction(ILCursor c, int parameterIndex, Func<ParameterDefinition, ILCursor> emitter)
    {
        if (parameterIndex == 0)
        {
            throw new InvalidOperationException("Cannot emit a static delegate that references the 'this' parameter.");
        }

        c.Remove();
        emitter(c.Method.Parameters[parameterIndex]);
    }
}

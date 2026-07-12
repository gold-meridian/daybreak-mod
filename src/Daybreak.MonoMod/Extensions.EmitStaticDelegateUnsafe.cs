using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
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
        public void EmitStaticDelegateUnsafe<T>(T @delegate)
            where T : Delegate
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
    private static DynamicMethodDefinition CloneMethodWithoutOrig(Delegate @delegate)
    {
        // This is a really dumb fix we need to use because when we override the
        // parameters in LiftDelegateToStaticMethod, they get overwritten by the
        // arguments of the original method.  Cloning it with the DMD overload
        // will not preserve the original method.  Stupid.
        using var dmd = new DynamicMethodDefinition(@delegate.Method);
        return new DynamicMethodDefinition(dmd);
    }
    
    public static MethodInfo? LiftDelegateToStaticMethod(Delegate @delegate)
    {
        using var dmd = CloneMethodWithoutOrig(@delegate);
        var method = dmd.Definition;

        // Can't be reasonable and check this here because it isn't set when
        // copying from MonoMod.
        /*
        if (method is { HasThis: false, IsStatic: true })
        {
            return null;
        }
        */

        if (@delegate.GetInvocationList().Length != 1 || @delegate.Target is null)
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
                    VerifyAndModifyInstruction(methodCursor, parameterIdx, OpCodes.Ldarg);
                    continue;
                }

                if (methodCursor.TryGotoNext(MoveType.Before, x => x.MatchLdarga(out parameterIdx)))
                {
                    VerifyAndModifyInstruction(methodCursor, parameterIdx, OpCodes.Ldarga);
                    continue;
                }

                if (methodCursor.TryGotoNext(MoveType.Before, x => x.MatchStarg(out parameterIdx)))
                {
                    VerifyAndModifyInstruction(methodCursor, parameterIdx, OpCodes.Starg);
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
        method.Parameters.RemoveAt(0);

        return dmd.Generate();
    }

    private static void VerifyAndModifyInstruction(ILCursor c, int parameterIndex, OpCode opcode)
    {
        if (parameterIndex == 0)
        {
            throw new InvalidOperationException("Cannot emit a static delegate that references the 'this' parameter.");
        }

        var instr = c.Next;
        {
            Debug.Assert(instr is not null);
        }

        instr.OpCode = opcode;
        instr.Operand = c.Method.Parameters[parameterIndex];
    }
}

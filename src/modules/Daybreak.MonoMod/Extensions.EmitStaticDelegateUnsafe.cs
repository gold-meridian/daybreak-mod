using System;
using MonoMod.Cil;

namespace Daybreak.MonoMod;

partial class Extensions
{
    extension(ILCursor c)
    {
        /// <summary>
        ///     Emits the IL to invoke the <paramref name="delegate"/> as if it
        ///     where a method.
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
        }
    }
}

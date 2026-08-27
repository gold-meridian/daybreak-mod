using System;
using MonoMod.Cil;

namespace Daybreak.MonoMod;

partial class Extensions
{
    extension(ILCursor cursor)
    {
        /// <summary>
        ///     Consumes the value preceding it, popping it off the stack.
        ///     <br />
        ///     If the function <paramref name="shouldSubstitute"/>
        ///     returns <see langword="true"/>, the
        ///     <paramref name="substitute"/> will be pushed; otherwise, the
        ///     popped value will be pushed once again.
        /// </summary>
        /// <param name="substitute">
        ///     The value to substitute the original value with.
        /// </param>
        /// <param name="shouldSubstitute">
        ///     Determines whether the value should be substituted.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the incoming value and what to substitute it with.
        /// </typeparam>
        public void Substitute<T>(T substitute, Func<T, bool> shouldSubstitute)
        {
            cursor.EmitDelegate((T val) => shouldSubstitute(val) ? substitute : val);
        }
    }
}

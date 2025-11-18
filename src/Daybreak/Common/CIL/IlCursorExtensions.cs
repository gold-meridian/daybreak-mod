using System;
using JetBrains.Annotations;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Daybreak.Common.CIL;

/// <summary>
///     API extensions for <see cref="ILCursor" />s and related APIs.
/// </summary>
[PublicAPI]
public static class IlCursorExtensions
{
#region Substitute
    /// <param name="cursor"></param>
    extension(ILCursor cursor)
    {
        /// <summary>
        ///     Attempts to substitute the value before the cursor given the
        ///     current value meets a given condition at runtime.
        /// </summary>
        /// <param name="substitute">
        ///     The value to substitute the real value with.
        /// </param>
        /// <param name="shouldSubstitute">
        ///     Determines whether the value should be substituted.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the incoming value and what to potentially
        ///     substitute it with.
        /// </typeparam>
        /// <remarks>
        ///     This API is designed 
        /// </remarks>
        public void Substitute<T>(T substitute, Func<T, bool> shouldSubstitute)
        {
            cursor.EmitDelegate((T val) => shouldSubstitute(val) ? substitute : val);
        }
    }
#endregion

#region AddVariable
    /// <param name="cursor">
    ///     The <see cref="ILCursor" /> whose <see cref="ILCursor.Context" /> to
    ///     use to determine the <see cref="MethodBody" /> to append to import
    ///     the CLR-represented type into a Cecil-represented type.
    /// </param>
    extension(ILCursor cursor)
    {
        /// <summary>
        ///     Creates a new <see cref="VariableDefinition" /> appended to the body
        ///     contextualized by the <see cref="ILCursor" />'s
        ///     <see cref="ILContext" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The CLR-represented type to import as a Cecil-represented type to
        ///     define the variable.
        /// </typeparam>
        /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
        public VariableDefinition AddVariable<T>()
        {
            return AddVariable(cursor.Body, cursor.Context.Import(typeof(T)));
        }

        /// <summary>
        ///     Creates a new <see cref="VariableDefinition" /> appended to the body
        ///     contextualized by the <see cref="ILCursor" />'s
        ///     <see cref="ILContext" />.
        /// </summary>
        /// <param name="type">The type of the variable.</param>
        /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
        public VariableDefinition AddVariable(TypeReference type)
        {
            return AddVariable(cursor.Body, type);
        }
    }

    /// <param name="il"></param>
    extension(ILContext il)
    {
        /// <summary>
        ///     Creates a new <see cref="VariableDefinition" /> appended to the body
        ///     contextualized by this <see cref="ILContext" />.
        /// </summary>
        /// <typeparam name="T">
        ///     The CLR-represented type to import as a Cecil-represented type to
        ///     define the variable.
        /// </typeparam>
        /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
        public VariableDefinition AddVariable<T>()
        {
            return AddVariable(il.Body, il.Import(typeof(T)));
        }

        /// <summary>
        ///     Creates a new <see cref="VariableDefinition" /> appended to the body
        ///     contextualized by this <see cref="ILContext" />.
        /// </summary>
        /// <param name="type">The type of the variable.</param>
        /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
        public VariableDefinition AddVariable(TypeReference type)
        {
            return AddVariable(il.Body, type);
        }
    }

    /// <param name="body">
    ///     The <see cref="MethodBody" /> to add a new variable to.
    /// </param>
    extension(MethodBody body)
    {
        /// <summary>
        ///     Creates a new <see cref="VariableDefinition" /> appended to this
        ///     <see cref="MethodBody" />.
        /// </summary>
        /// <param name="type">The type of the variable.</param>
        /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
        public VariableDefinition AddVariable(TypeReference type)
        {
            var variable = new VariableDefinition(type);
            {
                body.Variables.Add(variable);
            }

            return variable;
        }
    }
#endregion
}

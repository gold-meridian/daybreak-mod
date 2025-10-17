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
    public static void Substitute<T>(this ILCursor @this, T substitute, Func<T, bool> shouldSubstitute)
    {
        @this.EmitDelegate((T val) => shouldSubstitute(val) ? substitute : val);
    }

#region AddVariable
    /// <summary>
    ///     Creates a new <see cref="VariableDefinition" /> appended to the body
    ///     contextualized by the <see cref="ILCursor" />'s
    ///     <see cref="ILContext" />.
    /// </summary>
    /// <param name="this">
    ///     The <see cref="ILCursor" /> whose <see cref="ILCursor.Context" /> to
    ///     use to determine the <see cref="MethodBody" /> to append to import
    ///     the CLR-represented type into a Cecil-represented type.
    /// </param>
    /// <typeparam name="T">
    ///     The CLR-represented type to import as a Cecil-represented type to
    ///     define the variable.
    /// </typeparam>
    /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
    public static VariableDefinition AddVariable<T>(this ILCursor @this)
    {
        return AddVariable(@this.Body, @this.Context.Import(typeof(T)));
    }

    /// <summary>
    ///     Creates a new <see cref="VariableDefinition" /> appended to the body
    ///     contextualized by the <see cref="ILCursor" />'s
    ///     <see cref="ILContext" />.
    /// </summary>
    /// <param name="this">
    ///     The <see cref="ILCursor" /> whose <see cref="ILCursor.Context" /> to
    ///     use to determine the <see cref="MethodBody" /> to append to.
    /// </param>
    /// <param name="type">The type of the variable.</param>
    /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
    public static VariableDefinition AddVariable(this ILCursor @this, TypeReference type)
    {
        return AddVariable(@this.Body, type);
    }

    /// <summary>
    ///     Creates a new <see cref="VariableDefinition" /> appended to the body
    ///     contextualized by this <see cref="ILContext" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The CLR-represented type to import as a Cecil-represented type to
    ///     define the variable.
    /// </typeparam>
    /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
    public static VariableDefinition AddVariable<T>(this ILContext @this)
    {
        return AddVariable(@this.Body, @this.Import(typeof(T)));
    }

    /// <summary>
    ///     Creates a new <see cref="VariableDefinition" /> appended to the body
    ///     contextualized by this <see cref="ILContext" />.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="type">The type of the variable.</param>
    /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
    public static VariableDefinition AddVariable(this ILContext @this, TypeReference type)
    {
        return AddVariable(@this.Body, type);
    }

    /// <summary>
    ///     Creates a new <see cref="VariableDefinition" /> appended to this
    ///     <see cref="MethodBody" />.
    /// </summary>
    /// <param name="this">
    ///     The <see cref="MethodBody" /> to add a new variable to.
    /// </param>
    /// <param name="type">The type of the variable.</param>
    /// <returns>The newly-created <see cref="VariableDefinition" />.</returns>
    public static VariableDefinition AddVariable(this MethodBody @this, TypeReference type)
    {
        var variable = new VariableDefinition(type);
        {
            @this.Variables.Add(variable);
        }

        return variable;
    }
#endregion
}

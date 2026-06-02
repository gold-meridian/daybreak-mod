using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using MonoMod.Cil;

namespace Daybreak.MonoMod;

partial class Extensions
{
    extension(ILCursor cursor)
    {
        /// <inheritdoc cref="AddVariable(MethodBody, TypeReference, bool)"/>
        public VariableDefinition AddVariable<T>(bool makePinned = false)
        {
            return cursor.Body.AddVariable(cursor.Context.Import(typeof(T)), makePinned);
        }

        /// <inheritdoc cref="AddVariable(MethodBody, TypeReference, bool)"/>
        public VariableDefinition AddVariable(TypeReference type, bool makePinned = false)
        {
            return cursor.Body.AddVariable(type, makePinned);
        }
    }

    extension(ILContext il)
    {
        /// <inheritdoc cref="AddVariable(MethodBody, TypeReference, bool)"/>
        public VariableDefinition AddVariable<T>(bool makePinned = false)
        {
            return AddVariable(il.Body, il.Import(typeof(T)), makePinned);
        }

        /// <inheritdoc cref="AddVariable(MethodBody, TypeReference, bool)"/>
        public VariableDefinition AddVariable(TypeReference type,bool makePinned = false)
        {
            return AddVariable(il.Body, type, makePinned);
        }
    }

    extension(MethodBody body)
    {
        /// <summary>
        ///     Creates and appends a new <see cref="VariableDefinition" />,
        ///     returning it.
        /// </summary>
        public VariableDefinition AddVariable(TypeReference type, bool makePinned = false)
        {
            if (makePinned)
            {
                type = type.MakePinnedType();
            }
            
            var variable =  new VariableDefinition(type);
            {
                body.Variables.Add(variable);
            }

            return variable;
        }
    }
}

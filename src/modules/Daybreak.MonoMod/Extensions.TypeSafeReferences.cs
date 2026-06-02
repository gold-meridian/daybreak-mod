using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace Daybreak.MonoMod;

/// <summary>
///     A strongly typed parameter index in a method definition.
/// </summary>
public enum ParameterIndex
{
    /// <summary>
    ///     An invalid parameter index.
    /// </summary>
    Invalid = -1,
}

/// <summary>
///     A strongly typed variable index in a method body.
/// </summary>
public enum VariableIndex
{
    /// <summary>
    ///     An invalid variable index.
    /// </summary>
    Invalid = -1,
}

partial class Extensions
{
    extension(Instruction instr)
    {
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdarg(Instruction, int)"/>
        public bool MatchLdarg(ParameterIndex idx)
        {
            if (idx <= ParameterIndex.Invalid)
            {
                return false;
            }

            return instr.MatchLdarg((int)idx);
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdarg(Instruction, out int)"/>
        public bool MatchLdarg(out ParameterIndex idx)
        {
            var ret = instr.MatchLdarg(out int i);
            idx = (ParameterIndex)i;
            return ret;
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdarga(Instruction, int)"/>
        public bool MatchLdarga(ParameterIndex idx)
        {
            if (idx <= ParameterIndex.Invalid)
            {
                return false;
            }

            return instr.MatchLdarga((int)idx);
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdarga(Instruction, out int)"/>
        public bool MatchLdarga(out ParameterIndex idx)
        {
            var ret = instr.MatchLdarga(out int i);
            idx = (ParameterIndex)i;
            return ret;
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchStarg(Instruction, int)"/>
        public bool MatchStarg(ParameterIndex idx)
        {
            if (idx <= ParameterIndex.Invalid)
            {
                return false;
            }

            return instr.MatchStarg((int)idx);
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchStarg(Instruction, out int)"/>
        public bool MatchStarg(out ParameterIndex idx)
        {
            var ret = instr.MatchStarg(out int i);
            idx = (ParameterIndex)i;
            return ret;
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdloc(Instruction, int)"/>
        public bool MatchLdloc(VariableIndex idx)
        {
            if (idx <= VariableIndex.Invalid)
            {
                return false;
            }

            return instr.MatchLdloc((int)idx);
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdloc(Instruction, out int)"/>
        public bool MatchLdloc(out VariableIndex idx)
        {
            var ret = instr.MatchLdloc(out int i);
            idx = (VariableIndex)i;
            return ret;
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdloca(Instruction, int)"/>
        public bool MatchLdloca(VariableIndex idx)
        {
            if (idx <= VariableIndex.Invalid)
            {
                return false;
            }

            return instr.MatchLdloca((int)idx);
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchLdloca(Instruction, out int)"/>
        public bool MatchLdloca(out VariableIndex idx)
        {
            var ret = instr.MatchLdloca(out int i);
            idx = (VariableIndex)i;
            return ret;
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchStloc(Instruction, int)"/>
        public bool MatchStloc(VariableIndex idx)
        {
            if (idx <= VariableIndex.Invalid)
            {
                return false;
            }

            return instr.MatchStloc((int)idx);
        }
        
        /// <inheritdoc cref="ILPatternMatchingExt.MatchStloc(Instruction, out int)"/>
        public bool MatchStloc(out VariableIndex idx)
        {
            var ret = instr.MatchStloc(out int i);
            idx = (VariableIndex)i;
            return ret;
        }
    }
}

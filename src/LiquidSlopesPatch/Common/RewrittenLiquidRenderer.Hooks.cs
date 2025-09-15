using System;
using System.Linq;
using System.Runtime.CompilerServices;

using Mono.Cecil.Cil;

using MonoMod.Cil;
using MonoMod.Utils;

using Terraria.GameContent.Liquid;
using Terraria.ModLoader;

namespace LiquidSlopesPatch.Common;

public static partial class RewrittenLiquidRenderer
{
    [ModuleInitializer]
    internal static void EarlyHook()
    {
        ReplaceMethodBody(LiquidRenderer.Instance.DrawNormalLiquids, DrawNormalLiquids);
        ReplaceMethodBody(LiquidRenderer.Instance.DrawShimmer, DrawShimmer);
        ReplaceMethodBody(LiquidRenderer.Instance.GetCachedDrawArea, GetCachedDrawArea);
        ReplaceMethodBody(LiquidRenderer.GetShimmerBaseColor, GetShimmerBaseColor);
        ReplaceMethodBody(LiquidRenderer.Instance.GetShimmerFrame, GetShimmerFrame);
        ReplaceMethodBody(LiquidRenderer.GetShimmerGlitterColor, GetShimmerGlitterColor);
        ReplaceMethodBody(LiquidRenderer.GetShimmerGlitterOpacity, GetShimmerGlitterOpacity);
        ReplaceMethodBody(LiquidRenderer.GetShimmerWave, GetShimmerWave);
        ReplaceMethodBody(LiquidRenderer.Instance.GetVisibleLiquid, GetVisibleLiquid);
        ReplaceMethodBody(LiquidRenderer.Instance.HasFullWater, HasFullWater);
        ReplaceMethodBody(LiquidRenderer.Instance.InternalPrepareDraw, InternalPrepareDraw);
        ReplaceMethodBody(LiquidRenderer.Instance.PrepareDraw, PrepareDraw);
        ReplaceMethodBody(LiquidRenderer.SetShimmerVertexColors, SetShimmerVertexColors);
        ReplaceMethodBody(LiquidRenderer.SetShimmerVertexColors_Sparkle, SetShimmerVertexColors_Sparkle);
        ReplaceMethodBody(LiquidRenderer.Instance.SetWaveMaskData, SetWaveMaskData);
        ReplaceMethodBody(LiquidRenderer.SimpleWhiteNoise, SimpleWhiteNoise);
        ReplaceMethodBody(LiquidRenderer.Instance.Update, Update);

        /*
        On_LiquidRenderer.DrawNormalLiquids += (orig, self, batch, offset, style, alpha, draw) => { DrawNormalLiquids(self, batch, offset, style, alpha, draw); };
        On_LiquidRenderer.DrawShimmer += (orig, self, batch, offset, draw) => { DrawShimmer(self, batch, offset, draw); };
        On_LiquidRenderer.GetCachedDrawArea += (orig, self) => GetCachedDrawArea(self);
        On_LiquidRenderer.GetShimmerBaseColor += (orig, x, y) => GetShimmerBaseColor(x, y);
        On_LiquidRenderer.GetShimmerFrame += (orig, self, top, x, y) => GetShimmerFrame(self, top, x, y);
        On_LiquidRenderer.GetShimmerGlitterColor += (orig, top, x, y) => GetShimmerGlitterColor(top, x, y);
        On_LiquidRenderer.GetShimmerGlitterOpacity += (orig, top, x, y) => GetShimmerGlitterOpacity(top, x, y);
        On_LiquidRenderer.GetShimmerWave += (On_LiquidRenderer.orig_GetShimmerWave orig, ref float x, ref float y) => GetShimmerWave(ref x, ref y);
        On_LiquidRenderer.GetVisibleLiquid += (orig, self, i, i1) => GetVisibleLiquid(self, i, i1);
        On_LiquidRenderer.HasFullWater += (orig, self, i, i1) => HasFullWater(self, i, i1);
        On_LiquidRenderer.InternalPrepareDraw += (orig, self, area) => { InternalPrepareDraw(self, area); };
        // On_LiquidRenderer.LoadContent += orig => { LoadContent(); };
        // On_LiquidRenderer.PrepareAssets += (orig, self) => { PrepareAssets(); };
        On_LiquidRenderer.PrepareDraw += (orig, self, area) => { PrepareDraw(self, area); };
        On_LiquidRenderer.SetShimmerVertexColors += (On_LiquidRenderer.orig_SetShimmerVertexColors orig, ref VertexColors colors, float opacity, int i, int i1) => { SetShimmerVertexColors(ref colors, opacity, i, i1); };
        On_LiquidRenderer.SetShimmerVertexColors_Sparkle += (On_LiquidRenderer.orig_SetShimmerVertexColors_Sparkle orig, ref VertexColors colors, float opacity, int i, int i1, bool top) => SetShimmerVertexColors_Sparkle(ref colors, opacity, i, i1, top);
        On_LiquidRenderer.SetWaveMaskData += (On_LiquidRenderer.orig_SetWaveMaskData orig, LiquidRenderer self, ref Texture2D texture) => { SetWaveMaskData(self, ref texture); };
        On_LiquidRenderer.SimpleWhiteNoise += (orig, u, u1) => SimpleWhiteNoise(u, u1);
        On_LiquidRenderer.Update += (orig, self, time) => { Update(self, time); };
        */
    }

    private static void ReplaceMethodBody(Delegate from, Delegate to)
    {
        MethodBody? toBody = null;
        MonoModHooks.Modify(
            to.Method,
            il =>
            {
                toBody = il.Body;
            }
        );

        if (toBody is null)
        {
            throw new InvalidOperationException("Failed to capture IL method body");
        }

        MonoModHooks.Modify(
            from.Method,
            il =>
            {
                il.Body.Instructions.Clear();
                il.Body.ExceptionHandlers.Clear();
                il.Body.Variables.Clear();
                il.Method.CustomDebugInformations.Clear();
                il.Method.DebugInformation.SequencePoints.Clear();

                var c = new ILCursor(il);
                CloneMethodBodyToCursor(toBody, c);
            }
        );
    }

    public static void CloneMethodBodyToCursor(MethodBody body, ILCursor c)
    {
        c.Index = 0;

        c.Body.MaxStackSize = body.MaxStackSize;
        c.Body.InitLocals = body.InitLocals;
        c.Body.LocalVarToken = body.LocalVarToken;

        foreach (var instr in body.Instructions)
        {
            c.Emit(instr.OpCode, instr.Operand);
        }

        for (var i = 0; i < body.Instructions.Count; i++)
        {
            c.Instrs[i].Offset = body.Instructions[i].Offset;
        }

        foreach (var instr in c.Body.Instructions)
        {
            instr.Operand = instr.Operand switch
            {
                Instruction target => c.Body.Instructions[body.Instructions.IndexOf(target)],
                Instruction[] targets => targets.Select(x => c.Body.Instructions[body.Instructions.IndexOf(x)]).ToArray(),
                _ => instr.Operand,
            };
        }

        c.Body.ExceptionHandlers.AddRange(
            body.ExceptionHandlers.Select(x => new ExceptionHandler(x.HandlerType)
                {
                    TryStart = x.TryStart is null ? null : c.Body.Instructions[body.Instructions.IndexOf(x.TryStart)],
                    TryEnd = x.TryEnd is null ? null : c.Body.Instructions[body.Instructions.IndexOf(x.TryEnd)],
                    FilterStart = x.FilterStart is null ? null : c.Body.Instructions[body.Instructions.IndexOf(x.FilterStart)],
                    HandlerStart = x.HandlerStart is null ? null : c.Body.Instructions[body.Instructions.IndexOf(x.HandlerStart)],
                    HandlerEnd = x.HandlerEnd is null ? null : c.Body.Instructions[body.Instructions.IndexOf(x.HandlerEnd)],
                    CatchType = x.CatchType is null ? null : c.Body.Method.Module.ImportReference(x.CatchType),
                }
            )
        );

        c.Body.Variables.AddRange(body.Variables.Select(x => new VariableDefinition(x.VariableType)));

        c.Method.CustomDebugInformations.AddRange(
            body.Method.CustomDebugInformations.Select(x =>
                {
                    switch (x)
                    {
                        case AsyncMethodBodyDebugInformation asyncInfo:
                        {
                            AsyncMethodBodyDebugInformation info = new();

                            if (asyncInfo.CatchHandler.Offset >= 0)
                            {
                                info.CatchHandler = asyncInfo.CatchHandler.IsEndOfMethod ? new InstructionOffset() : new InstructionOffset(resolveInstrOff(info.CatchHandler.Offset));
                            }

                            info.Yields.AddRange(asyncInfo.Yields.Select(y => y.IsEndOfMethod ? new InstructionOffset() : new InstructionOffset(resolveInstrOff(y.Offset))));
                            info.Resumes.AddRange(asyncInfo.Resumes.Select(y => y.IsEndOfMethod ? new InstructionOffset() : new InstructionOffset(resolveInstrOff(y.Offset))));

                            return info;
                        }

                        case StateMachineScopeDebugInformation stateInfo:
                        {
                            StateMachineScopeDebugInformation info = new();
                            info.Scopes.AddRange(stateInfo.Scopes.Select(y => new StateMachineScope(resolveInstrOff(y.Start.Offset), y.End.IsEndOfMethod ? null : resolveInstrOff(y.End.Offset))));

                            return info;
                        }

                        default:
                            return x;
                    }
                }
            )
        );

        c.Method.DebugInformation.SequencePoints.AddRange(
            body.Method.DebugInformation.SequencePoints.Select(x => new SequencePoint(resolveInstrOff(x.Offset), x.Document)
                {
                    StartLine = x.StartLine,
                    StartColumn = x.StartColumn,
                    EndLine = x.EndLine,
                    EndColumn = x.EndColumn,
                }
            )
        );

        c.Index = 0;

        return;

        Instruction resolveInstrOff(int off)
        {
            for (var i = 0; i < body.Instructions.Count; i++)
            {
                if (body.Instructions[i].Offset == off)
                {
                    return c.Body.Instructions[i];
                }
            }

            throw new Exception("Could not resolve instruction offset");
        }
    }
}
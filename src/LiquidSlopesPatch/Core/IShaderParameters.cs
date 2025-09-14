using Microsoft.Xna.Framework.Graphics;

namespace LiquidSlopesPatch.Core;

internal interface IShaderParameters
{
    void Apply(EffectParameterCollection parameters);
}
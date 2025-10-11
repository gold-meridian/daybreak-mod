using Microsoft.Xna.Framework.Graphics;

namespace DaybreakHookGenerator.Core;

internal interface IShaderParameters
{
    void Apply(EffectParameterCollection parameters);
}
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Core;

internal interface IShaderParameters
{
    void Apply(EffectParameterCollection parameters);
}
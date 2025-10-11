using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria.Graphics.Shaders;

namespace DaybreakHookGenerator.Core;

internal sealed class WrapperShaderData<TParameters>(Asset<Effect> shader, string passName) : ShaderData(shader, passName)
    where TParameters : IShaderParameters, new()
{
    public TParameters Parameters { get; } = new();

    public override void Apply()
    {
        Parameters.Apply(Shader.Parameters);

        base.Apply();
    }
}
using JetBrains.Annotations;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.ChatTags;

[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithInheritors)]
internal interface ILoadableTagHandler<TSelf> : ITagHandler, ILoadable
    where TSelf : ILoadableTagHandler<TSelf>, new()
{
    string[] TagNames { get; }

    void ILoadable.Load(Mod mod)
    {
        ChatManager.Register<TSelf>(TagNames);
    }

    void ILoadable.Unload()
    {
        foreach (var tagName in TagNames)
        {
            ChatManager._handlers.TryRemove(tagName, out _);
        }
    }
}

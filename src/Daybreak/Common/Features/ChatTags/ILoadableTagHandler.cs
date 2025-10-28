using JetBrains.Annotations;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace Daybreak.Common.Features.ChatTags;

/// <summary>
///     Automatically registers itself as a chat tag.
/// </summary>
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithInheritors)]
public interface ILoadableTagHandler<TSelf> : ITagHandler, ILoadable
    where TSelf : ILoadableTagHandler<TSelf>, new()
{
    /// <summary>
    ///     The tag names.
    /// </summary>
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

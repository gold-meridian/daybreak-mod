using JetBrains.Annotations;

namespace Daybreak.Common.Features.Config.Types;

[PublicAPI]
public abstract class BooleanContainer
{
    public virtual bool Value { get; set; }
}

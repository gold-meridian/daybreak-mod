using JetBrains.Annotations;

namespace Daybreak.Common.Features.Config.Types;

[PublicAPI]
public abstract class BooleanContainer
{
    public virtual bool Enabled { get; set; }

    public virtual bool Locked => false;
}

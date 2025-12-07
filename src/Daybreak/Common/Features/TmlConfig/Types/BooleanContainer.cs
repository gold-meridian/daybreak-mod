using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Daybreak.Common.Features.TmlConfig;

[PublicAPI]
public abstract class BooleanContainer
{
    public virtual bool Enabled { get; set; }

    [JsonIgnore]
    public virtual bool Locked => false;
}

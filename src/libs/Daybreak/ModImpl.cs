using Daybreak.Common.Features.ModCalls;

namespace Daybreak;

/// <summary/>
partial class ModImpl
{
    /// <inheritdoc />
    public override object? Call(params object?[]? args)
    {
        return CallHandler.HandleCall(this, args);
    }
}
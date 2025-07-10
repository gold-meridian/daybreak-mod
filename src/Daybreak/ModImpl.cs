using Tomat.TML.Library.DynamicModCalls;

namespace Daybreak;

partial class ModImpl
{
    public override object? Call(params object?[]? args)
    {
        return CallHandler.HandleCall(this, args);
    }
}
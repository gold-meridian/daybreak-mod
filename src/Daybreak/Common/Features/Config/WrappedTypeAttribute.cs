using JetBrains.Annotations;
using System;

namespace Daybreak.Common.Features.Config;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class)]
public class WrappedTypeAttribute<T> : WrappedTypeAttribute
{
    public WrappedTypeAttribute() : base(typeof(T))
    { }
}

[PublicAPI]
[AttributeUsage(AttributeTargets.Class)]
public class WrappedTypeAttribute : Attribute
{
    public WrappedTypeAttribute(Type type)
    {
        Type = type;
    }

    public Type Type;
}

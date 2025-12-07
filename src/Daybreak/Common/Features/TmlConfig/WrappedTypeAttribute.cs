using System;
using JetBrains.Annotations;

namespace Daybreak.Common.Features.TmlConfig;

[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class WrappedTypeAttribute<T> : WrappedTypeAttribute
{
    public WrappedTypeAttribute() : base(typeof(T))
    { }
}

[PublicAPI]
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class WrappedTypeAttribute : Attribute
{
    public WrappedTypeAttribute(Type type)
    {
        Type = type;
    }

    public Type Type;
}

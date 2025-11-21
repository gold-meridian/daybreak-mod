using System;
using Terraria.ModLoader.IO;

namespace Daybreak.Common.Features.Models;

#region Base attributes
/// <summary>
///     An abstract attribute definition which can perform arbitrary
///     configuration of a <see cref="Binding{T}"/>.  Should be applied to
///     <see cref="Bound{T}"/> definitions.
/// </summary>
public abstract class BindingConfigurationAttribute : Attribute
{
    /// <summary>
    ///     Configures the <paramref name="binding"/>.
    /// </summary>
    /// <param name="binding">The <see cref="Binding{T}"/> to configure.</param>
    public abstract void Configure<T>(Binding<T> binding);
}

/// <inheritdoc cref="BindingConfigurationAttribute"/>
/// <remarks>
///     Helper for attributes that need the type parameter.
/// </remarks>
/// <typeparam name="T">The binding kind.</typeparam>
public abstract class BindingConfigurationAttribute<T> : BindingConfigurationAttribute
{
    /// <inheritdoc />
    public override void Configure<TBinding>(Binding<TBinding> binding)
    {
        // TODO: Throw exception or something?
        if (binding is not Binding<T> realBinding)
        {
            return;
        }

        Configure(realBinding);
    }

    /// <inheritdoc cref="Configure{T}"/>
    protected abstract void Configure(Binding<T> binding);
}
#endregion

#region ResetTo
/// <summary>
///     Configures the annotated <see cref="Bound{T}"/>-object to set the value
///     to <paramref name="value"/> in <c>ResetEffects</c>.
/// </summary>
/// <param name="value">The value to reset to.</param>
/// <typeparam name="T">The value type.</typeparam>
public sealed class ResetToAttribute<T>(T value) : BindingConfigurationAttribute<T>
{
    /// <summary>
    ///     The value to reset to.
    /// </summary>
    public T Value => value;

    /// <inheritdoc />
    protected override void Configure(Binding<T> binding)
    {
        binding.Reset = b => b.Value = Value;
    }
}

/// <summary>
///     Configures the annotated <see cref="Bound{T}"/>-object to set the value
///     to its default value specified in the <see cref="Bound{T}"/>
///     constructor.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class ResetToDefaultAttribute<T> : BindingConfigurationAttribute<T>
{
    /// <inheritdoc />
    protected override void Configure(Binding<T> binding)
    {
        binding.Reset = b => b.Value = b.DefaultValueProvider();
    }
}
#endregion

#region SerializeTag
/// <summary>
///     Marks that the annotated <see cref="Bound{T}"/>-object should be
///     serialized and deserialized as <see cref="TagCompound"/> data.
/// </summary>
/// <param name="tagName">
///     The name to serialize with.  Uses the property name if not set.
/// </param>
public sealed class SerializeTagAttribute(string? tagName = null) : Attribute;
#endregion

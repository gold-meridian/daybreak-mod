using System;
using System.Runtime.CompilerServices;
using Terraria.ModLoader.IO;

namespace Daybreak.Common.Features.Models;

public interface IBinding { }

/// <summary>
///     A configuration object determining the behavior of a
///     <see cref="Bound{T}"/> object.
///     <br />
///     A binding is a singleton object which will be shared across
///     <see cref="Bound{T}"/> instances bound to it.  The binding does not hold
///     any outside state.
/// </summary>
/// <typeparam name="T">The kind of value.</typeparam>
public sealed class Binding<T> : IBinding
{
    /// <summary>
    ///     Used to reset the <see cref="Bound{T}"/> object in
    ///     <c>ResetEffects</c>.
    /// </summary>
    public delegate void ResetToAction(Bound<T> b);

    /// <summary>
    ///     Controls the behavior in <c>ResetEffects</c>.
    /// </summary>
    public ResetToAction? ResetTo { get; set; }

    /// <summary>
    ///     Used to write data in <c>SaveData</c>.
    /// </summary>
    public delegate void SaveTagAction(Bound<T> b, TagCompound tag);

    /// <summary>
    ///     Used to read data in <c>LoadData</c>.
    /// </summary>
    public delegate void LoadTagAction(Bound<T> b, TagCompound tag);

    /// <summary>
    ///     Controls behavior in <c>SaveTag</c>.
    /// </summary>
    public SaveTagAction? SaveTag { get; set; }

    /// <summary>
    ///     Controls behavior in <c>LoadTag</c>.
    /// </summary>
    public LoadTagAction? LoadTag { get; set; }
}

/// <summary>
///     The type-unsafe contract of a <see cref="Binding{T}"/>.
/// </summary>
public interface IBound
{
    /// <inheritdoc cref="Bound{T}.Binding"/>
    IBinding Binding { get; }

    /// <inheritdoc cref="Bound{T}.Name"/>
    string Name { get; }

    /// <inheritdoc cref="Bound{T}.DefaultValueProvider"/>
    Func<object?> DefaultValueProvider { get; }

    /// <inheritdoc cref="Bound{T}.Value"/>
    object? Value { get; set; }

    /// <inheritdoc cref="Bound{T}.Clone"/>
    IBound Clone();
}

/// <summary>
///     Represents a value bound to a <see cref="Binding{T}"/>.
///     <br />
///     A bound object wraps an actual data instance and may be cloned and
///     recreated as needed.
/// </summary>
/// <typeparam name="T">The kind of value.</typeparam>
public sealed class Bound<T> : IBound
{
    /// <summary>
    ///     The shared binding instance this bound object is bound to.
    /// </summary>
    public Binding<T> Binding { get; }

    IBinding IBound.Binding => Binding;

    /// <summary>
    ///     The name of this bound object, which should correspond to a property
    ///     on the owning type.
    /// </summary>
    public string Name { get; }

    /// <summary>
    ///     Creates a new default value.
    /// </summary>
    public Func<T> DefaultValueProvider { get; }

    Func<object?> IBound.DefaultValueProvider => () => DefaultValueProvider();

    /// <summary>
    ///     The value held by this bound object.
    /// </summary>
    public T Value { get; set; }

    object? IBound.Value
    {
        get => Value;

        set
        {
            if (value == null)
            {
                // We have to accept nulls here.
                Value = default(T)!;
            }
            else
            {
                Value = (T)value;
            }
        }
    }

    /// <summary>
    ///     Initializes the template bound-object. 
    /// </summary>
    /// <param name="defaultValue">The default value of this object.</param>
    /// <param name="name">The name of the property this object is for.</param>
    public Bound(T defaultValue, [CallerMemberName] string name = "")
    {
        DefaultValueProvider = () => defaultValue;
        Value = defaultValue;
        Name = name;
        Binding = new Binding<T>();
    }

    /// <summary>
    ///     Initializes the template bound-object. 
    /// </summary>
    /// <param name="defaultValueProvider">
    ///     A function used to initialize the default value of this object.
    ///     <br />
    ///     Useful if you need to instantiate a new object each time or
    ///     something.  Also called on creation of the template!
    /// </param>
    /// <param name="name">The name of the property this object is for.</param>
    public Bound(Func<T> defaultValueProvider, [CallerMemberName] string name = "")
    {
        DefaultValueProvider = defaultValueProvider;
        Value = defaultValueProvider();
        Name = name;
        Binding = new Binding<T>();
    }

    private Bound(Func<T> defaultValueProvider, string name, Binding<T> binding)
    {
        DefaultValueProvider = defaultValueProvider;
        Value = defaultValueProvider();
        Name = name;
        Binding = binding;
    }

    /// <summary>
    ///     Clones this bound object, reinitializing it as needed.  The
    ///     <see cref="Binding"/>, <see cref="DefaultValueProvider"/>, and
    ///     <see cref="Name"/> properties will all be shared, but it will point
    ///     to a different value.
    /// </summary>
    /// <returns></returns>
    public Bound<T> Clone()
    {
        return new Bound<T>(DefaultValueProvider, Name, Binding);
    }

    IBound IBound.Clone()
    {
        return Clone();
    }

    /// <summary>
    ///     Returns the value.
    /// </summary>
    public static implicit operator T(Bound<T> b) => b.Value;

#region Binding configuration
    /// <summary>
    ///     Configures the binding to reset to the provided value.
    /// </summary>
    public Bound<T> ResetTo(Binding<T>.ResetToAction callback)
    {
        Binding.ResetTo = callback;
        return this;
    }

    /// <summary>
    ///     Configures the binding to reset to the provided value.
    /// </summary>
    public Bound<T> ResetTo(Func<T> callback)
    {
        Binding.ResetTo = _ => callback();
        return this;
    }

    /// <summary>
    ///     Configures the binding to reset to the default value.
    /// </summary>
    public Bound<T> ResetToDefault()
    {
        Binding.ResetTo = b => b.DefaultValueProvider();
        return this;
    }

    /// <summary>
    ///     Configures the binding to save its value to a tag.
    /// </summary>
    public Bound<T> SaveTag(Binding<T>.SaveTagAction callback)
    {
        Binding.SaveTag = callback;
        return this;
    }

    /// <summary>
    ///     Configures the binding to save its value to a tag.
    /// </summary>
    public Bound<T> SaveTag(string name)
    {
        Binding.SaveTag = (b, tag) => tag[name] = b.Value;
        return this;
    }

    /// <summary>
    ///     Configures the binding to save its value to a tag.
    /// </summary>
    public Bound<T> SaveTag()
    {
        Binding.SaveTag = (b, tag) => tag[b.Name] = b.Value;
        return this;
    }

    /// <summary>
    ///     Configures the binding to load its value from a tag.
    /// </summary>
    public Bound<T> LoadTag(Binding<T>.LoadTagAction callback)
    {
        Binding.LoadTag = callback;
        return this;
    }

    /// <summary>
    ///     Configures the binding to load its value from a tag.
    /// </summary>
    public Bound<T> LoadTag(string name)
    {
        Binding.LoadTag = (b, tag) => b.Value = tag.Get<T>(name);
        return this;
    }

    /// <summary>
    ///     Configures the binding to load its value from a tag.
    /// </summary>
    public Bound<T> LoadTag()
    {
        Binding.LoadTag = (b, tag) => tag[b.Name] = b.Value;
        return this;
    }
#endregion
}

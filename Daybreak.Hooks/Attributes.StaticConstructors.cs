using System;

namespace Daybreak.Hooks;

/// <summary>
///     Any types decorated with this attribute will cause a type to run its
///     static constructor if it contains static fields or properties of the
///     type.  Use <see cref="DontForceStaticConstructorAttribute"/> for members
///     that should not respect this behavior.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class RunsStaticConstructorsAttribute : Attribute;

/// <summary>
///     Overrides the behavior of <see cref="RunsStaticConstructorsAttribute"/>
///     to not apply for the given member.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class DontForceStaticConstructorAttribute : Attribute;

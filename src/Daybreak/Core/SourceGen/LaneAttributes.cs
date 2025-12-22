using System;
using Daybreak.Common;

namespace Daybreak.Core.SourceGen;

/// <summary>
///     Marks a method receiving a <see cref="ILane{TSelf}"/> generic parameter
///     to generate overloads for known, Lane-convertible types.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class GenerateLaneOverloadsAttribute : Attribute;

/// <summary>
///     Marks a generic parameter as being a lane type to be substituted.
/// </summary>
[AttributeUsage(AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
public sealed class LaneParameterAttribute : Attribute;

/// <summary>
///     Marks a method as being a valid factory method to convert a type to a
///     lane.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class ToLaneAttribute : Attribute;

/// <summary>
///     Marks a method as being a valid factory method to convert a lane to a
///     type.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public sealed class FromLaneAttribute : Attribute;

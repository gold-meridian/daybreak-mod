using System.Diagnostics.CodeAnalysis;

namespace Daybreak.Common.Features.ModCalls;

/// <summary>
///     Allows for handling transforming 
/// </summary>
public interface ITransformableCallObject<T>
{
    /// <summary>
    ///     The transformed value.
    /// </summary>
    T Value { get; }

    /// <summary>
    ///     Attempts to transform the incoming object into the accepted output
    ///     type.
    /// </summary>
    /// <param name="obj">The object to transform.</param>
    /// <param name="result">The transformed type.</param>
    /// <typeparam name="TFrom">The source type to transform.</typeparam>
    /// <returns>
    ///     <see langword="true"/> if the type may be transformed and an output
    ///     is produced, otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    ///     A failure to transform is considered a type mismatch and means the
    ///     handler will be skipped over entirely.
    ///     <br />
    ///     If there are special error cases for values, you should handle them
    ///     yourself in the handler.
    /// </remarks>
    static abstract bool TryTransform<TFrom>(
        TFrom obj,
        [NotNullWhen(returnValue: true)] out ITransformableCallObject<T>? result
    );
}

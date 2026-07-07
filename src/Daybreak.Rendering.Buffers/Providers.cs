namespace Daybreak.Rendering.Buffers;

/// <summary>
///     Handles the provision of buffers.
/// </summary>
public interface IBufferProvider<TBuffer>
{
    /// <summary>
    ///     Returns a leased buffer to the provider.
    /// </summary>
    void Return(IBufferLease<TBuffer> lease);
}

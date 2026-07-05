using System;
using Microsoft.Xna.Framework.Graphics;

namespace Daybreak.Rendering;

/// <summary>
///     Temporarily suspends a <see cref="SpriteBatch"/>'s active <c>Begin</c>
///     call for the duration of the scope, restoring its original parameters
///     when the scope is disposed.
/// </summary>
/// <remarks>
///     If the <see cref="SpriteBatch"/> has already begun when this scope is
///     created, its current parameters are captured, and it is ended
///     immediately.  On disposal, the <see cref="SpriteBatch"/> is ended again
///     (if something began it in the meantime) and then restarted with the
///     originally captured parameters.
///     <br />
///     If the <see cref="SpriteBatch"/> had not been begun when the scope was
///     created, disposal will simply end it (if it was begun during the scope)
///     without restarting it (as there is nothing to restore).
///     <br />
///     This is intended for cases (typically mods drawing &quot;on top of&quot;
///     or &quot;in between&quot; someone else's rendering code) where you need
///     to use a shared <see cref="SpriteBatch"/> without knowing or disturbing
///     whatever state its owner left it in.
/// </remarks>
public readonly struct SpriteBatchScope : IDisposable
{
    private readonly SpriteBatch spriteBatch;
    private readonly SpriteBatchSnapshot? oldState;

    /// <summary>
    ///     Initializes a new scope. If the <see cref="SpriteBatch"/> has
    ///     already begun, its current parameters are saved and the
    ///     <see cref="SpriteBatch"/> is ended; the original parameters will
    ///     then be reapplied on disposal.
    /// </summary>
    /// <param name="spriteBatch">The <see cref="SpriteBatch"/>.</param>
    public SpriteBatchScope(SpriteBatch spriteBatch)
    {
        this.spriteBatch = spriteBatch;

        if (!spriteBatch.beginCalled)
        {
            return;
        }

        spriteBatch.End(out var old);
        oldState = old;
    }

    /// <summary>
    ///     Ends the <see cref="SpriteBatch"/> and starts it with the old
    ///     parameters if it has already begun prior.
    /// </summary>
    public void Dispose()
    {
        if (spriteBatch.beginCalled)
        {
            spriteBatch.End();
        }

        if (oldState.HasValue)
        {
            spriteBatch.Begin(oldState.Value);
        }
    }
}

/// <summary>
///     Extensions to types for <see cref="SpriteBatch"/> scopes.
/// </summary>
public static class SpriteBatchScopeExtensions
{
    /// <summary>
    ///     Creates a <see cref="SpriteBatchScope"/> for this
    ///     <see cref="SpriteBatch"/>, suspending any in-progress <c>Begin</c>
    ///     calls and restoring it when the scope is disposed.
    /// </summary>
    public static SpriteBatchScope Scope(this SpriteBatch @this)
    {
        return new SpriteBatchScope(@this);
    }
}

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using Terraria.ModLoader;

namespace Daybreak.Common.Features.ModCalls;

/// <summary>
///     An autoloaded type used to register cross-mod API call handlers.
/// </summary>
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedWithFixedConstructorSignature, ImplicitUseTargetFlags.WithInheritors)]
public abstract class ModCall : ModType
{
    /// <summary>
    ///     A read-only collection of (interpreted-as-case-insensitive) aliases
    ///     for this mod call.
    /// </summary>
    protected abstract IReadOnlyCollection<string> Aliases { get; }

    /// <summary>
    ///     A read-only collection of invokable methods that arguments may be
    ///     passed to.  Generally, these should all exhibit the same behavior
    ///     for a given API alias, but may differ in parameter handling or
    ///     permissiveness.
    /// </summary>
    protected abstract IReadOnlyCollection<Delegate> Handlers { get; }

    /// <inheritdoc />
    protected override void Register()
    {
        CallHandler.Register(Mod, new CallManifest(Aliases, Handlers));
    }

    /// <inheritdoc />
    public sealed override void SetupContent()
    {
        base.SetupContent();

        SetStaticDefaults();
    }
}
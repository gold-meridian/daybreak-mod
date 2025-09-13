using System;

using JetBrains.Annotations;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Achievements;

/// <summary>
///     An achievement category, which an achievement may belong to an arbitrary
///     number of.
/// </summary>
[PublicAPI]
[Obsolete("Use upcoming tML ModAchievement API")]
public abstract class AchievementCategory : ModTexturedType, ILocalizedModType
{
    /// <inheritdoc cref="ILocalizedModType.LocalizationCategory"/>
    public virtual string LocalizationCategory => "AchievementCategories";

    /// <summary>
    ///     The display name (friendly name) of this achievement category.
    /// </summary>
    public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);

    internal int Id { get; set; }

    /// <summary>
    ///     Lets the implementation know about the category and registers it as
    ///     known content.
    /// </summary>
    protected sealed override void Register()
    {
        AchievementImpl.RegisterCategory(this);
        ModTypeLookup<AchievementCategory>.Register(this);
    }

    /// <summary>
    ///     Unused; this type is a singleton.
    /// </summary>
    protected sealed override void InitTemplateInstance() { }

    /// <summary>
    ///     Gets the icon texture and its 'on' frame.
    ///     <br />
    ///     The off-frame is <c>34</c> pixels below the 'on' frame.
    /// </summary>
    /// <param name="frame">The frame of the icon in the texture.</param>
    /// <returns>The icon texture.</returns>
    public virtual Asset<Texture2D> GetIcon(out Rectangle frame)
    {
        frame = new Rectangle(0, 0, 32, 32);
        return ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad);
    }
}
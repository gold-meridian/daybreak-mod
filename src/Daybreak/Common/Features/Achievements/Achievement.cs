using System.Collections.Generic;

using JetBrains.Annotations;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Content;

using Terraria.Localization;
using Terraria.ModLoader;

namespace Daybreak.Common.Features.Achievements;

/// <summary>
///     A modded achievement.  Unrelated to the vanilla
///     <see cref="Terraria.Achievements.Achievement"/> type, which this
///     implementation wholly replaces.
/// </summary>
[PublicAPI]
public abstract class Achievement : ModTexturedType, ILocalizedModType
{
    /// <inheritdoc cref="ILocalizedModType.LocalizationCategory"/>
    public virtual string LocalizationCategory => "Achievements";

    /// <summary>
    ///     The display name (friendly name) of this achievement.
    /// </summary>
    public virtual LocalizedText DisplayName => this.GetLocalization(nameof(DisplayName), PrettyPrintName);

    /// <summary>
    ///     The description of this achievement if it has one.
    /// </summary>
    public virtual LocalizedText Description => this.GetLocalization(nameof(Description), static () => "");

    /// <summary>
    ///     The order of this achievement in the context of the achievement
    ///     advisor cards.  Return a value below <c>0f</c> to omit it from the
    ///     advisor card system.
    /// </summary>
    public virtual float AdvisorOrder => -1f;

    /// <summary>
    ///     Whether this achievement has been completed.
    /// </summary>
    public bool IsCompleted => AchievementImpl.GetCompletedStatus(this);

    internal int Id { get; set; }

    /// <summary>
    ///     Lets the implementation know about the achievement and registers it
    ///     as known content.
    /// </summary>
    protected sealed override void Register()
    {
        AchievementImpl.Register(this);
        ModTypeLookup<Achievement>.Register(this);
    }

    /// <summary>
    ///     Unused; this type is a singleton.
    /// </summary>
    protected sealed override void InitTemplateInstance() { }

    /// <summary>
    ///     Marks this achievement as completed, which will additionally put a
    ///     message in chat, display an in-game popup notification, and award
    ///     the achievement to the user.  If the achievement is already
    ///     completed, this will do nothing.
    /// </summary>
    public void Complete()
    {
        AchievementImpl.Complete(this);
    }

    /// <summary>
    ///     Gets the categories this achievement is in.  Achievements must
    ///     belong to at least one category, but may belong to an arbitrary
    ///     amount past that.
    /// </summary>
    public abstract IEnumerable<AchievementCategory> GetCategories();

    /// <summary>
    ///     Whether this achievement is available to be earned in the current
    ///     world.  Used for the achievement advisor cards.
    /// </summary>
    public virtual bool IsPresentlyAvailable()
    {
        return true;
    }

    /// <summary>
    ///     Gets the texture and frame used for the advisor card icon.
    /// </summary>
    public virtual Asset<Texture2D> GetIcon(out Rectangle frame, out int lockedOffset)
    {
        frame = new Rectangle(0, 0, 64, 64);
        lockedOffset = 66;
        return ModContent.Request<Texture2D>(Texture, AssetRequestMode.ImmediateLoad);
    }

    /// <summary>
    ///     Gets a normalized (0f to 1f) value representing the current progress
    ///     of this achievement to display as a progress bar in the achievement
    ///     menu.  If this achievement does not track progress, or you do not
    ///     want to display a progress bar, return <c>null</c>.
    /// </summary>
    /// <returns>
    ///     A normalized value representing the current progress of this
    ///     achievement, or <c>null</c> if this achievement does not track
    ///     progress.
    /// </returns>
    /// <remarks>
    ///     Fulfillment of this value (<c>&gt;= 1f</c>) has no bearing on the
    ///     completeness of this achievement.  Developers are expected to
    ///     manually invoke <see cref="Achievement.Complete()"/> still.
    /// </remarks>
    public virtual float? GetProgress(out string progressText)
    {
        progressText = string.Empty;
        return null;
    }
}
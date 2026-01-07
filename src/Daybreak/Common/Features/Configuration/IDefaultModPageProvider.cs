using Terraria.UI;

namespace Daybreak.Common.Features.Configuration;

/// <summary>
///     Used by the default config repository to create unique pages of
///     categories for each mod that owns the category.
/// </summary>
public interface IDefaultModPageProvider
{
    /// <summary>
    ///     Adds the category items to the containing UI element.  This should
    ///     provide your arbitrary UI to display.
    /// </summary>
    void AddCategoriesToContainer(
        UIElement container,
        ConfigCategory[] categories
    );
}

internal sealed class DefaultModPageProvider : IDefaultModPageProvider
{
    void IDefaultModPageProvider.AddCategoriesToContainer(UIElement container, ConfigCategory[] categories) { }
}

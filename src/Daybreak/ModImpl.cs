using Daybreak.Common.Features.ModPanel;
using Daybreak.Content.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;

namespace Daybreak;

/// <summary>
///     The <see cref="Mod" /> implementation for DAYBREAK.
/// </summary>
partial class ModImpl : IHasCustomModConfigButton
{
    /// <inheritdoc />
    public ModImpl()
    {
        // Handled by the asset generator.
        MusicAutoloadingEnabled = false;
    }

    UIButton<string> IHasCustomModConfigButton.CreateModConfigButton(UIButton<string> button)
    {
        var originalText = button.Text;

        button.OnUpdate += _ =>
        {
            button.SetText(PanelStyle.ModName.GetPulsatingText(originalText, Main.GlobalTimeWrappedHourly));
        };

        return button;
    }
}

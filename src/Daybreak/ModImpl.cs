using Daybreak.Common.Features.Authorship;
using Daybreak.Common.Features.Hooks;
using Daybreak.Common.Features.ModPanel;
using Daybreak.Content.UI;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;

namespace Daybreak;

/// <summary>
///     The <see cref="Mod" /> implementation for DAYBREAK.
/// </summary>
partial class ModImpl : IHasCustomModConfigButton, IHasCustomAuthorMessage
{
    /// <inheritdoc />
    public ModImpl()
    {
        // Handled by the asset generator.
        MusicAutoloadingEnabled = false;
    }

    [ModSystemHooks.AddRecipeGroups]
    private static void A(ModSystemHooks.AddRecipeGroups.Original orig) { }

    [ModSystemHooks.AddRecipeGroups]
    static void A(ModSystem self) { }

    [ModSystemHooks.AddRecipeGroups]
    static void A(ModSystemHooks.AddRecipeGroups.Original orig, ModSystem self) { }

    [ModSystemHooks.AddRecipeGroups]
    static void A(ModSystem self, ModSystemHooks.AddRecipeGroups.Original orig) { }

    [ModSystemHooks.AddRecipeGroups]
    static void A(ModSystemHooks.AddRecipeGroups.Original orig, ModSystem self, [OriginalName("test")] int invalid) { }

    [ModSystemHooks.CanWorldBePlayed]
    void B(
        Terraria.IO.PlayerFileData playerData,
        Terraria.IO.WorldFileData worldFileData
    ) { }

    UIButton<string> IHasCustomModConfigButton.CreateModConfigButton(UIButton<string> button)
    {
        var originalText = button.Text;

        button.OnUpdate += _ =>
        {
            button.SetText(PanelStyle.ModName.GetPulsatingText(originalText, Main.GlobalTimeWrappedHourly));
        };

        return button;
    }

    string IHasCustomAuthorMessage.GetAuthorText()
    {
        return AuthorText.GetAuthorTooltip(this, headerText: null);
    }
}

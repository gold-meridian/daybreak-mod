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
    private static void A(ModSystemHooks.AddRecipeGroups.Original orig)
    {
        ;
    }

    [ModSystemHooks.AddRecipeGroups]
    private static void A(ModSystem self)
    {
        ;
    }

    [ModSystemHooks.AddRecipeGroups]
    private static void A(ModSystemHooks.AddRecipeGroups.Original orig, ModSystem self)
    {
        ;
    }

    [ModSystemHooks.AddRecipeGroups]
    private static void A(ModSystem self, ModSystemHooks.AddRecipeGroups.Original orig)
    {
        ;
    }

    [ModSystemHooks.AddRecipeGroups]
    private static void B([OriginalName("orig")] ModSystemHooks.AddRecipeGroups.Original original, ModSystem self)
    {
        ;
    }

    [ModSystemHooks.AddRecipeGroups]
    private static void C([OriginalName("orig")] ModSystemHooks.AddRecipeGroups.Original original, [OriginalName("self")] ModSystem @this)
    {
        ;
    }

    [ModSystemHooks.CanWorldBePlayed]
    private static void B(
        Terraria.IO.PlayerFileData playerData,
        Terraria.IO.WorldFileData worldFileData
    )
    {
        ;
    }

    [ModSystemHooks.CanWorldBePlayed]
    private static bool B(
        [OriginalName("orig")] ModSystemHooks.CanWorldBePlayed.Original original,
        ModSystem self,
        Terraria.IO.PlayerFileData playerData,
        Terraria.IO.WorldFileData worldFileData
    )
    {
        return original(playerData, worldFileData);
    }

    [OnLoad]
    private static void OnLoad()
    {
        ;
    }

    [OnLoad]
    private static void OnLoad(Mod mod)
    {
        ;
    }

    [OnLoad]
    private static void OnLoad2([OriginalName("mod")] Mod theMod)
    {
        ;
    }

    [OnUnload]
    private static void OnUnoad()
    {
        ;
    }

    [OnUnload]
    private static void OnUnoad(Mod mod)
    {
        ;
    }

    [OnUnload]
    private static void OnUnoad2([OriginalName("mod")] Mod theMod)
    {
        ;
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

    string IHasCustomAuthorMessage.GetAuthorText()
    {
        return AuthorText.GetAuthorTooltip(this, headerText: null);
    }
}

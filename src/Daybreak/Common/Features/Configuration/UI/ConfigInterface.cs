using Daybreak.Common.Features.Hooks;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Common.Features.Configuration.UI;

public static class ConfigInterface
{
    private static readonly UIConfigInterface state = new();

    [OnLoad]
    private static void Load()
    {
        IL_Main.DrawMenu += DrawMenu_SettingsButton;
    }

    private static void DrawMenu_SettingsButton(ILContext il)
    {
        var c = new ILCursor(il);

        c.GotoNext(MoveType.After,
            i => i.MatchLdcI4(MenuID.Settings),
            i => i.MatchStsfld<Main>(nameof(Main.menuMode)));

        c.EmitDelegate(() => EnterRepository(ConfigRepository.Default));
    }

    public static void EnterRepository(ConfigRepository repository)
    {
        Main.menuMode = MenuID.FancyUI;

        state.CurrentRepository = repository;

        Main.MenuUI.SetState(state);
    }
}

internal sealed class UIConfigInterface : UIState
{
    public ConfigRepository? CurrentRepository;


}

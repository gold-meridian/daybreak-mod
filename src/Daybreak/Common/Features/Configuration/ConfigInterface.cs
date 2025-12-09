using Daybreak.Common.Features.Hooks;
using MonoMod.Cil;
using Terraria;
using Terraria.ID;
using Terraria.UI;

namespace Daybreak.Common.Features.Configuration;

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

        c.EmitDelegate(EnterConfigAt<DefaultConfigRepository>);
    }

    public static void EnterConfigAt<T>() where T : ConfigRepository
    {
        var repository = ConfigSystem.GetRepository<T>();

        Main.menuMode = MenuID.FancyUI;

        state.CurrentRepository = repository;

        Main.MenuUI.SetState(state);
    }
}

internal sealed class UIConfigInterface : UIState
{
    public ConfigRepository? CurrentRepository;
}

using System;
using System.Collections.Generic;

using Daybreak.Common.Features.ModCalls;

using Terraria.ModLoader;

namespace Daybreak;

public class A : ModCall
{
    protected override IReadOnlyCollection<string> Aliases { get; } = ["log"];

    protected override IReadOnlyCollection<Delegate> Handlers { get; } = [LogNone, LogNull, LogInt, LogString, LogObject];

    private static void LogNone()
    {
        ModContent.GetInstance<ModImpl>().Logger.Info("it's none");
    }

    private static void LogNull(Null _)
    {
        ModContent.GetInstance<ModImpl>().Logger.Info("it's null");
    }

    private static void LogInt(int i)
    {
        ModContent.GetInstance<ModImpl>().Logger.Info("int: " + i);
    }

    private static void LogString(string s)
    {
        ModContent.GetInstance<ModImpl>().Logger.Info("string: " + s);
    }

    private static void LogObject(object o)
    {
        ModContent.GetInstance<ModImpl>().Logger.Info("object: " + o.GetType().FullName!);
    }
}

public class TestShit : ModSystem
{
    public override void PostSetupContent()
    {
        base.PostSetupContent();

        Mod.Call("log");
        Mod.Call("log", null);
        Mod.Call("log", 1);
        Mod.Call("log", "hi");
        Mod.Call("log", new { });
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Daybreak.Common.Features.NPCs;

public delegate bool DownedGetter();

public delegate void DownedSetter(bool value);

internal readonly record struct DownedHandler(
    DownedGetter Getter,
    DownedSetter Setter
);

/// <summary>
///     Manages &quot;downed&quot; flags, which are typically the status of the
///     defeat of an NPC within a world (e.g. <see cref="NPC.downedBoss1" />).
/// </summary>
public static class DownedFlagHandler
{
    [Autoload(false)]
    private sealed class DownedFlagSystem : ModSystem
    {
        public Dictionary<string, bool> NamedDowns { get; } = [];

        public override void SaveWorldData(TagCompound tag)
        {
            base.SaveWorldData(tag);

            foreach (var (name, val) in NamedDowns)
            {
                tag.Add(name, val);
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            base.LoadWorldData(tag);

            foreach (var name in NamedDowns.Keys)
            {
                if (tag.TryGet<bool>(name, out var val))
                {
                    NamedDowns[name] = val;
                }
            }
        }

        // TODO: Reduce network bandwidth.  We need to sync names, but can maybe
        //       write all boolean values to a bit-array structure.

        public override void NetSend(BinaryWriter writer)
        {
            base.NetSend(writer);

            if (Mod.NetID < 0)
            {
                return;
            }

            writer.Write(NamedDowns.Count);
            foreach (var (name, val) in NamedDowns)
            {
                writer.Write(name);
                writer.Write(val);
            }
        }

        public override void NetReceive(BinaryReader reader)
        {
            base.NetReceive(reader);

            if (Mod.NetID < 0)
            {
                return;
            }

            var amt = reader.ReadInt32();
            for (var i = 0; i < amt; i++)
            {
                NamedDowns[reader.ReadString()] = reader.ReadBoolean();
            }
        }
    }

    private static readonly Dictionary<Mod, DownedFlagSystem> systems = [];
    private static readonly Dictionary<string, DownedHandler> handlers = [];

    internal static bool IsHandleRegistered(DownedFlagHandle handle)
    {
        return handlers.ContainsKey(handle.FullName);
    }

    /// <summary>
    ///     Gets the handle of a &quot;downed&quot; flag handler.
    /// </summary>
    /// <param name="mod">The mod.</param>
    /// <param name="name">The unique name, per-mod.</param>
    /// <returns>The handle.</returns>
    /// <remarks>
    ///     Always returns a value, even if the handler is not registered.
    /// </remarks>
    public static DownedFlagHandle GetHandle(Mod mod, string name)
    {
        return GetHandle(mod.Name, name);
    }

    internal static DownedFlagHandle GetHandle(string modName, string name)
    {
        return new DownedFlagHandle(GetId(modName, name));
    }

    /// <summary>
    ///     Registers a handler with default behavior (that is, handled by
    ///     DAYBREAK).
    /// </summary>
    /// <param name="mod">The mod.</param>
    /// <param name="name">The unique name, per-mod.</param>
    /// <returns>The handle to this handler.</returns>
    public static DownedFlagHandle RegisterDefaultHandle(Mod mod, string name)
    {
        if (!systems.TryGetValue(mod, out var system))
        {
            // I had to rewrite our systems to instead inject a handler system
            // with network syncing into existing Both-synced mods, but we still
            // need to ensure DAYBREAK is present on both the client and server
            // for syncing to actually work.  If we don't guarantee this, we run
            // into similar issues with DAYBREAK doing it on its own.  Even if
            // we don't run any code ourselves, the presence of a system will
            // write data automatically -- which causes de-syncs -- this
            // circumvents that.
            if (Main.netMode != NetmodeID.SinglePlayer && !ModContent.GetInstance<ModImpl>().IsNetSynced)
            {
                throw new InvalidOperationException($"Cannot register default handle: \"{mod.Name}/{name}\"; DAYBREAK needs to be present on both the client and server!");
            }

            // Similar to the above, we need to ensure it exists on both the
            // client and server.
            if (mod.Side != ModSide.Both)
            {
                throw new InvalidOperationException($"Cannot register default handle: \"{mod.Name}/{name}\"; the mod is not labeled as ModSide.Both and cannot be guaranteed to sync!");
            }

            // We don't actually need this restriction for registration, but we
            // do need the mod to be loading to initialize our syncing system on
            // the mod.  Since this code is unreachable if the system *IS*
            // present, we technically do allow mods to register after loading
            // so long as they call it during loading at least once.
            if (!mod.loading)
            {
                throw new InvalidOperationException($"Cannot register default handle: \"{mod.Name}/{name}\"; initial registration needs to occur during mod load!");
            }

            system = systems[mod] = new DownedFlagSystem();
            mod.AddContent(system);
        }

        var id = GetId(mod.Name, name);
        if (handlers.ContainsKey(id))
        {
            throw new InvalidOperationException($"Duplicate handle ID: {id}");
        }

        system.NamedDowns[name] = false;

        handlers.Add(
            id,
            new DownedHandler(
                () => system.NamedDowns[name],
                val => system.NamedDowns[name] = val
            )
        );
        return new DownedFlagHandle(id);
    }

    /// <summary>
    ///     Registers a handler with custom, arbitrary behavior.
    /// </summary>
    /// <param name="mod">The mod.</param>
    /// <param name="name">The unique name, per-mod.</param>
    /// <param name="getter">The <c>get</c> handler.</param>
    /// <param name="setter">The <c>set</c> handler.</param>
    /// <returns>The handle to this handler.</returns>
    public static DownedFlagHandle RegisterCustomHandle(Mod mod, string name, DownedGetter getter, DownedSetter setter)
    {
        return RegisterCustomHandle(mod.Name, name, getter, setter);
    }

    internal static DownedFlagHandle RegisterCustomHandle(string modName, string name, DownedGetter getter, DownedSetter setter)
    {
        var id = GetId(modName, name);
        if (handlers.ContainsKey(id))
        {
            throw new InvalidOperationException($"Duplicate handle ID: {id}");
        }

        handlers.Add(id, new DownedHandler(getter, setter));
        return new DownedFlagHandle(id);
    }

    internal static bool GetValue(DownedFlagHandle handle)
    {
        return handlers.TryGetValue(handle.FullName, out var value) && value.Getter();
    }

    internal static void SetValue(DownedFlagHandle handle, bool value)
    {
        if (handlers.TryGetValue(handle.FullName, out var handler))
        {
            handler.Setter(value);
        }
    }

    private static string GetId(string modName, string name)
    {
        return modName + '/' + name;
    }
}

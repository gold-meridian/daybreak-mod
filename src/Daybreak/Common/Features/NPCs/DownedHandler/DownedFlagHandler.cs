using System;
using System.Collections.Generic;
using System.IO;

using Terraria;
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
///     defeat of an NPC within a world (e.g. <see cref="NPC.downedBoss1"/>).
/// </summary>
public static class DownedFlagHandler
{
    private sealed class DownedFlagSystem : ModSystem
    {
        internal static readonly Dictionary<string, bool> NAMED_DOWNS = [];

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            base.SaveWorldData(tag);

            foreach (var (name, val) in NAMED_DOWNS)
            {
                tag.Add(name, val);
            }
        }

        public override void LoadWorldData(TagCompound tag)
        {
            base.LoadWorldData(tag);

            foreach (var name in NAMED_DOWNS.Keys)
            {
                if (tag.TryGet<bool>(name, out var val))
                {
                    NAMED_DOWNS[name] = val;
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

            writer.Write(NAMED_DOWNS.Count);
            foreach (var (name, val) in NAMED_DOWNS)
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
                NAMED_DOWNS[reader.ReadString()] = reader.ReadBoolean();
            }
        }
    }
    
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
        var id = GetId(mod.Name, name);
        if (handlers.ContainsKey(id))
        {
            throw new InvalidOperationException($"Duplicate handle ID: {id}");
        }

        DownedFlagSystem.NAMED_DOWNS[name] = false;

        handlers.Add(
            id,
            new DownedHandler(
                () => DownedFlagSystem.NAMED_DOWNS[name],
                val => DownedFlagSystem.NAMED_DOWNS[name] = val
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
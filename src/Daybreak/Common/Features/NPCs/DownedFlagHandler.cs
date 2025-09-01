using System;
using System.Collections.Generic;
using System.IO;

using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Daybreak.Common.Features.NPCs;

/// <summary>
///     Manages "downed" flags for bosses or other NPCs.
/// </summary>
public static class DownedFlagHandler
{
    /// <summary>
    ///     A handle to a "downed" flag.
    /// </summary>
    public readonly struct Handle
    {
        /// <summary>
        ///     The full name, a unique identifier for each handle.  Expected to
        ///     be the mod name followed by the unique key
        ///     (<c>ModName/NpcName</c>).
        /// </summary>
        public string FullName { get; }

        /// <summary>
        ///     Whether this handle maps to a registered handler.
        /// </summary>
        public bool IsRegistered => IsHandleRegistered(this);

        /// <summary>
        ///     The value of the associated "downed" flag.
        /// </summary>
        public bool Value
        {
            get => GetValue(this);
            set => SetValue(this, value);
        }

        internal Handle(string fullName)
        {
            FullName = fullName;
        }
    }

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
        // write all boolean values to a bit-array structure.

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

    private readonly record struct DownedHandler(Func<bool> Getter, Action<bool> Setter);

    private static readonly Dictionary<string, DownedHandler> handlers = [];

    private static bool IsHandleRegistered(Handle handle)
    {
        return handlers.ContainsKey(handle.FullName);
    }

    /// <summary>
    ///     Gets the handle of a "downed" flag handler.
    /// </summary>
    /// <param name="mod">The mod.</param>
    /// <param name="name">The unique name, per-mod.</param>
    /// <returns>The handle.</returns>
    /// <remarks>
    ///     Always returns a value, even if the handler is not registered.
    /// </remarks>
    public static Handle GetHandle(Mod mod, string name)
    {
        return new Handle(GetId(mod, name));
    }

    /// <summary>
    ///     Registers a handler with default behavior (that is, handled by
    ///     DAYBREAK).
    /// </summary>
    /// <param name="mod">The mod.</param>
    /// <param name="name">The unique name, per-mod.</param>
    /// <returns>The handle to this handler.</returns>
    public static Handle RegisterDefaultHandle(Mod mod, string name)
    {
        var id = GetId(mod, name);
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
        return new Handle(id);
    }

    /// <summary>
    ///     Registers a handler with custom, arbitrary behavior.
    /// </summary>
    /// <param name="mod">The mod.</param>
    /// <param name="name">The unique name, per-mod.</param>
    /// <param name="getter">The <c>get</c> handler.</param>
    /// <param name="setter">The <c>set</c> handler.</param>
    /// <returns>The handle to this handler.</returns>
    public static Handle RegisterCustomHandle(Mod mod, string name, Func<bool> getter, Action<bool> setter)
    {
        var id = GetId(mod, name);
        if (handlers.ContainsKey(id))
        {
            throw new InvalidOperationException($"Duplicate handle ID: {id}");
        }

        handlers.Add(id, new DownedHandler(getter, setter));
        return new Handle(id);
    }

    private static bool GetValue(Handle handle)
    {
        return handlers.TryGetValue(handle.FullName, out var value) && value.Getter();
    }

    private static void SetValue(Handle handle, bool value)
    {
        if (handlers.TryGetValue(handle.FullName, out var handler))
        {
            handler.Setter(value);
        }
    }

    private static string GetId(Mod mod, string name)
    {
        return mod.Name + '/' + name;
    }
}
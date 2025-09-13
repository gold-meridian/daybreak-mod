using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Mono.Cecil;
using Mono.Cecil.Rocks;

using Terraria.ModLoader;

using ModuleDefinition = Mono.Cecil.ModuleDefinition;

namespace DaybreakHookGenerator;

internal static class Program
{
    private class TypeHookDefinition(Type type)
    {
        public Type Type { get; } = type;

        public List<string> Exclusions { get; } = [];

        public Dictionary<string, InvokeStrategy> InvokeStrategies { get; } = [];

        public TypeHookDefinition WithExclusions(params string[] exclusions)
        {
            Exclusions.AddRange(exclusions);
            return this;
        }

        public TypeHookDefinition WithInvokeStrategy(string methodName, InvokeStrategy strategy)
        {
            InvokeStrategies[methodName] = strategy;
            return this;
        }
    }

    private const string the_namespace = "Daybreak.Common.Features.Hooks";

    public static void Main()
    {
        // Run this from the repository root.
        var path = Path.Combine("src", "Daybreak", "Common", "Features", "Hooks", "_TML");

        // TODO: Can we include GlobalBlockType?
        // TODO: GlobalBuilderToggle
        // TODO: Mod hooks?
        var definitions = new[]
        {
            new TypeHookDefinition(typeof(GlobalBossBar))
               .WithInvokeStrategy(nameof(GlobalBossBar.PreDraw), new BoolCombinerStrategy(true, "&=")),
            new TypeHookDefinition(typeof(GlobalBuff))
               .WithInvokeStrategy("ReApply_int_Player_int_int", new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy("ReApply_int_NPC_int_int", new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(GlobalBuff.PreDraw), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalBuff.RightClick), new BoolCombinerStrategy(true, "&=")),
            new TypeHookDefinition(typeof(GlobalEmoteBubble))
               .WithInvokeStrategy(nameof(GlobalEmoteBubble.UpdateFrame), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalEmoteBubble.UpdateFrameInEmoteMenu), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalEmoteBubble.PreDraw), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalEmoteBubble.PreDrawInEmoteMenu), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalEmoteBubble.GetFrame), new NullableValueMayBeOverriddenStrategy("Microsoft.Xna.Framework.Rectangle"))
               .WithInvokeStrategy(nameof(GlobalEmoteBubble.GetFrameInEmoteMenu), new NullableValueMayBeOverriddenStrategy("Microsoft.Xna.Framework.Rectangle")),
            new TypeHookDefinition(typeof(GlobalInfoDisplay))
               .WithInvokeStrategy(nameof(GlobalInfoDisplay.Active), new NullableBooleanCombinerStrategy()),
            new TypeHookDefinition(typeof(GlobalItem))
               .WithExclusions(
                    nameof(GlobalItem.ChoosePrefix),
                    nameof(GlobalItem.PrefixChance),
                    nameof(GlobalItem.CanAutoReuseItem),
                    nameof(GlobalItem.UseTimeMultiplier),
                    nameof(GlobalItem.UseAnimationMultiplier),
                    nameof(GlobalItem.UseSpeedMultiplier),
                    nameof(GlobalItem.CanConsumeBait),
                    nameof(GlobalItem.CanChooseAmmo),
                    nameof(GlobalItem.CanBeChosenAsAmmo),
                    nameof(GlobalItem.CanBeConsumedAsAmmo),
                    nameof(GlobalItem.CanCatchNPC),
                    nameof(GlobalItem.CanHitNPC),
                    nameof(GlobalItem.CanMeleeAttackCollideWithNPC),
                    nameof(GlobalItem.UseItem),
                    nameof(GlobalItem.IsArmorSet),
                    nameof(GlobalItem.IsVanitySet),
                    nameof(GlobalItem.GetAlpha),
                    nameof(GlobalItem.HoldoutOffset),
                    nameof(GlobalItem.CanAccessoryBeEquippedWith),
                    nameof(GlobalItem.IsAnglerQuestAvailable),
                    nameof(GlobalItem.HoldoutOrigin),
                    nameof(GlobalItem.NetSend),
                    nameof(GlobalItem.NetReceive)
                )
               .WithInvokeStrategy(nameof(GlobalItem.AllowPrefix), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalItem.CanUseItem), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.CanResearch), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.NeedsAmmo), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.CanConsumeAmmo), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.CanShoot), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.Shoot), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalItem.CanHitPvp), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.ConsumeItem), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.AltFunctionUse), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.CanRightClick), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.CanStack), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.CanStackInWorld), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.ReforgePrice), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalItem.CanReforge), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalItem.WingUpdate), new BoolCombinerStrategy(false, "|="))
               .WithInvokeStrategy(nameof(GlobalItem.GrabStyle), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.CanPickup), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.OnPickup), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.ItemSpace), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.PreDrawInWorld), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalItem.PreDrawInInventory), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalItem.CanEquipAccessory), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalItem.PreDrawTooltip), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalItem.PreDrawTooltipLine), new BoolCombinerStrategy(true, "&=")),
            new TypeHookDefinition(typeof(GlobalNPC))
               .WithExclusions(
                    nameof(GlobalNPC.ModifyTownNPCProfile),
                    nameof(GlobalNPC.SpecialOnKill),
                    nameof(GlobalNPC.CanFallThroughPlatforms),
                    nameof(GlobalNPC.CanBeCaughtBy),
                    nameof(GlobalNPC.CanBeHitByItem),
                    nameof(GlobalNPC.CanCollideWithPlayerMeleeAttack),
                    nameof(GlobalNPC.CanBeHitByProjectile),
                    nameof(GlobalNPC.GetAlpha),
                    nameof(GlobalNPC.DrawHealthBar),
                    nameof(GlobalNPC.CanChat),
                    nameof(GlobalNPC.CanGoToStatue),
                    nameof(GlobalNPC.PickEmote),
                    nameof(GlobalNPC.SendExtraAI),
                    nameof(GlobalNPC.ReceiveExtraAI),
                    nameof(GlobalNPC.PreHoverInteract) // TODO
                )
               .WithInvokeStrategy(nameof(GlobalNPC.PreAI), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalNPC.CheckActive), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalNPC.CheckDead), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalNPC.PreKill), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalNPC.CanHitPlayer), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalNPC.CanHitNPC), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalNPC.CanBeHitByNPC), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalNPC.PreDraw), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalNPC.PreChatButtonClicked), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalNPC.ModifyCollisionData), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalNPC.NeedSaving), new BoolCombinerStrategy(false, "|=")) // TODO: Should we?
               .WithInvokeStrategy(nameof(GlobalNPC.ModifyDeathMessage), new BoolCombinerStrategy(true, "&=")),
            new TypeHookDefinition(typeof(GlobalProjectile))
               .WithExclusions(
                    nameof(GlobalProjectile.CanCutTiles),
                    nameof(GlobalProjectile.CanDamage),
                    nameof(GlobalProjectile.CanHitNPC),
                    nameof(GlobalProjectile.CanHitPvp),
                    nameof(GlobalProjectile.CanHitPlayer),
                    nameof(GlobalProjectile.Colliding),
                    nameof(GlobalProjectile.GetAlpha),
                    nameof(GlobalProjectile.CanUseGrapple),
                    nameof(GlobalProjectile.GrappleCanLatchOnTo),
                    nameof(GlobalProjectile.SendExtraAI),
                    nameof(GlobalProjectile.ReceiveExtraAI)
                ) // lazy
               .WithInvokeStrategy(nameof(GlobalProjectile.PreAI), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalProjectile.ShouldUpdatePosition), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalProjectile.TileCollideStyle), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalProjectile.OnTileCollide), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalProjectile.PreKill), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalProjectile.MinionContactDamage), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(GlobalProjectile.PreDrawExtras), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalProjectile.PreDraw), new BoolCombinerStrategy(true, "&=")),
            new TypeHookDefinition(typeof(GlobalPylon))
               .WithInvokeStrategy(nameof(GlobalPylon.PreDrawMapIcon), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalPylon.PreCanPlacePylon), new NullableBooleanEarlyReturnStrategy())
               .WithInvokeStrategy(nameof(GlobalPylon.ValidTeleportCheck_PreNPCCount), new NullableBooleanEarlyReturnStrategy())
               .WithInvokeStrategy(nameof(GlobalPylon.ValidTeleportCheck_PreAnyDanger), new NullableBooleanEarlyReturnStrategy())
               .WithInvokeStrategy(nameof(GlobalPylon.ValidTeleportCheck_PreBiomeRequirements), new NullableBooleanEarlyReturnStrategy()),
            new TypeHookDefinition(typeof(GlobalTile))
               .WithExclusions(nameof(GlobalTile.AutoSelect), nameof(GlobalTile.Slope)) // lazy
               .WithInvokeStrategy(nameof(GlobalTile.CanDrop), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalTile.CanKillTile), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalTile.IsTileDangerous), new NullableBooleanCombinerStrategy())
               .WithInvokeStrategy(nameof(GlobalTile.IsTileBiomeSightable), new NullableBooleanCombinerStrategy())
               .WithInvokeStrategy(nameof(GlobalTile.IsTileSpelunkable), new NullableBooleanCombinerStrategy())
               .WithInvokeStrategy(nameof(GlobalTile.TileFrame), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(GlobalTile.AdjTiles), new ArrayCombinerStrategy("int"))
               .WithInvokeStrategy(nameof(GlobalTile.PreHitWire), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalTile.CanReplace), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalTile.ShakeTree), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(GlobalTile.PreDrawPlacementPreview), new BoolCombinerStrategy(true, "&=")),
            new TypeHookDefinition(typeof(GlobalWall))
               .WithInvokeStrategy(nameof(GlobalWall.Drop), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalWall.WallFrame), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(GlobalWall.CanBeTeleportedTo), new EarlyReturnOnFalseStrategy()),
            new TypeHookDefinition(typeof(ModSystem))
               .WithExclusions(
                    nameof(ModSystem.OnModLoad),
                    nameof(ModSystem.OnModUnload),
                    nameof(ModSystem.SetupContent),
                    nameof(ModSystem.CanWorldBePlayed),
                    nameof(ModSystem.WorldCanBePlayedRejectionMessage),
                    nameof(ModSystem.HijackGetData),
                    nameof(ModSystem.HijackSendData),
                    nameof(ModSystem.NetSend),
                    nameof(ModSystem.NetReceive)
                )
            /*.WithInvokeStrategy(nameof(ModSystem.CanWorldBePlayed), new EarlyReturnOnFalseStrategy())*/,
            new TypeHookDefinition(typeof(ModPlayer))
               .WithExclusions(
                    nameof(ModPlayer.NewInstance),
                    nameof(ModPlayer.UseTimeMultiplier),
                    nameof(ModPlayer.UseAnimationMultiplier),
                    nameof(ModPlayer.UseSpeedMultiplier),
                    nameof(ModPlayer.CanCatchNPC),
                    nameof(ModPlayer.CanMeleeAttackCollideWithNPC),
                    nameof(ModPlayer.CanHitNPCWithItem),
                    nameof(ModPlayer.CanHitNPCWithProj),
                    nameof(ModPlayer.CanConsumeBait),
                    nameof(ModPlayer.CanAutoReuseItem),
                    nameof(ModPlayer.AddStartingItems),
                    nameof(ModPlayer.AddMaterialsForCrafting)
                )
               .WithInvokeStrategy(nameof(ModPlayer.ModifyMaxStats), new VoidReturnButInitializeOutParametersStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanStartExtraJump), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanShowExtraJumpVisuals), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.ImmuneTo), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.FreeDodge), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.ConsumableDodge), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.PreKill), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(ModPlayer.PreModifyLuck), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(ModPlayer.PreItemCheck), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(ModPlayer.CanConsumeAmmo), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanShoot), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(ModPlayer.Shoot), new BoolCombinerStrategy(true, "&="))
               .WithInvokeStrategy(nameof(ModPlayer.CanHitNPC), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanHitPvp), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanHitPvpWithProj), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanBeHitByNPC), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanBeHitByProjectile), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.ShiftClickSlot), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.HoverSlot), new EarlyReturnOnTrueStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanSellItem), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanBuyItem), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanUseItem), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.ModifyNurseHeal), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.OnPickup), new EarlyReturnOnFalseStrategy())
               .WithInvokeStrategy(nameof(ModPlayer.CanBeTeleportedTo), new EarlyReturnOnFalseStrategy()),
        };

        var modDef = ModuleDefinition.ReadModule(typeof(ModLoader).Assembly.Location);

        foreach (var definition in definitions)
        {
            GenerateHookDefinition(path, definition, modDef);
        }
    }

    private static void GenerateHookDefinition(string path, TypeHookDefinition definition, ModuleDefinition modDef)
    {
        Console.WriteLine("GENERATING HOOK DEFINITION FOR " + definition.Type.Name);

        var className = definition.Type.Name + "Hooks";
        var fileName = Path.Combine(path, className + ".cs");

        Console.WriteLine($"    {className} @ {fileName}");

        var typeDef = modDef.GetType(definition.Type.FullName);
        var generator = new Generator(modDef, typeDef);
        var contents = generator.BuildType(the_namespace, className, definition.Exclusions, definition.InvokeStrategies);

        File.WriteAllText(fileName, contents);
    }
}
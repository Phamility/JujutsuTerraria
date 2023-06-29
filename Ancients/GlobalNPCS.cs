using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using TenShadows.Items.Materials;
using TenShadows.Items.Techniques;
using TenShadows.Projectiles;
using TenShadows.Items.Accessories.Eyes;
using TenShadows.Items.Techniques.Blood;
using TenShadows.Items.Techniques.AEquip;
using Microsoft.Xna.Framework;
using TenShadows.Buffs;
using TenShadows.Armor;

namespace TenShadows.Ancients
{
    public class GlobalNPCS : GlobalNPC
    {

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (npc.HasBuff<CursedBuff>())
            {
                drawColor = Color.MediumPurple;
            }
        }
        public override void OnKill(NPC npc)
        {
            if (npc.boss == true)
            {
                DogSummon.KYS = true;
                DogSummon2.KYS = true;

            }




        }
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            bool Accounted = false;
            if (npc.type == NPCID.AngryBones || npc.type == NPCID.DarkCaster || npc.type == NPCID.CursedSkull
                || npc.type == NPCID.Skeleton || npc.type == NPCID.AngryBonesBig || npc.type == NPCID.AngryBonesBigHelmet || npc.type == NPCID.AngryBonesBigMuscle)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HeavenlyCursed>(), 80, 1));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HeavenlyPhysical>(), 80, 1));
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 50, 70, 100));

                Accounted = true;

            }
            if (npc.type == NPCID.BlueSlime || npc.type == NPCID.GreenSlime)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 50, 30, 60));
                Accounted = true;

            }
            if (npc.type == NPCID.Zombie || npc.type == NPCID.DemonEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 50, 30, 60));
                Accounted = true;

            }
            if (npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 25, 70, 100));
                Accounted = true;

            }
            if (npc.type == NPCID.FaceMonster || npc.type == NPCID.EaterofSouls || npc.type == NPCID.Crimera)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 25, 70, 100));
                Accounted = true;

            }


            if (npc.type == NPCID.PossessedArmor || npc.type == NPCID.WanderingEye)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedSpeech>(), 100));

                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 25, 100, 150));
                Accounted = true;

            }
            if (npc.type == NPCID.Werewolf || npc.type == NPCID.DungeonSpirit || npc.type == NPCID.Demon)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 10, 100, 150));
                Accounted = true;

            }

            if (npc.boss == true)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 1, 75, 100));



                npcLoot.Add(notExpertRule);
                Accounted = true;
            }

            if (Accounted == false)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 100, 30, 60));

            }

            //---------------CURSED ENERGY ^^^


            if (npc.type == NPCID.BlueJellyfish || npc.type == NPCID.GreenJellyfish || npc.type == NPCID.PinkJellyfish)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FishEye>(), 50, 1));

            }
            if (npc.type == NPCID.BlueJellyfish || npc.type == NPCID.GreenJellyfish || npc.type == NPCID.PinkJellyfish)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FishEye>(), 50, 1));

            }
            if (npc.type == NPCID.Demon || npc.type == NPCID.VoodooDemon)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Horn>(), 5));

            }
            if (npc.type == NPCID.WallofFlesh)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RestlessGambler>(), 5));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BloodEdge>(), 5));


                npcLoot.Add(notExpertRule);
            }
            if (npc.type == NPCID.EyeofCthulhu)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FlowingRed>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BlazingCourage>(), 3));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<Bottomless>(), 3));


                npcLoot.Add(notExpertRule);
            }
            if (System.Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1)
            {

                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
                npcLoot.Add(notExpertRule);



                LeadingConditionRule leadingConditionRule = new(new Conditions.LegacyHack_IsABoss());
                leadingConditionRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WrappedCleaver>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EyeEye>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PiercingBlood>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NobaraHead>(), 3));


                notExpertRule.OnSuccess(leadingConditionRule);
            }

            if (npc.type == NPCID.BrainofCthulhu)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WrappedCleaver>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EyeEye>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PiercingBlood>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NobaraHead>(), 3));


                npcLoot.Add(notExpertRule);
            }

            if (npc.type == NPCID.KingSlime)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CountryHammer>(), 3));

                npcLoot.Add(notExpertRule);
            }

        }
    }
    public class GlobalItems : GlobalItem
    {



        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (ItemID.Sets.BossBag[item.type]) {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CursedEnergy>(), 1, 75, 125));

            }

            // In addition to this code, we also do similar code in Common/GlobalNPCs/ExampleNPCLoot.cs to edit the boss loot for non-expert drops. Remember to do both if your edits should affect non-expert drops as well.
            if (item.type == ItemID.WallOfFleshBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RestlessGambler>(), 4));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodEdge>(), 4));


            }
            if (item.type == ItemID.EyeOfCthulhuBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlowingRed>(), 3));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BlazingCourage>(), 3));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Bottomless>(), 3));


            }
            if (item.type == ItemID.EaterOfWorldsBossBag || item.type == ItemID.BrainOfCthulhuBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<WrappedCleaver>(), 3));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EyeEye>(), 3));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<PiercingBlood>(), 2));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<NobaraHead>(), 3));




            }
            if (item.type == ItemID.KingSlimeBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CountryHammer>(), 3));


            }
        }
    }

}


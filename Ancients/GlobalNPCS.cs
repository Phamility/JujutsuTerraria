using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Items.Techniques;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Accessories.Eyes;
using JujutsuTerraria.Items.Techniques.Blood;
using Microsoft.Xna.Framework;
using JujutsuTerraria.Buffs;
using JujutsuTerraria.Armor;
using JujutsuTerraria.Items.Techniques.ARestrictions;
using static Terraria.ModLoader.PlayerDrawLayer;
using System;
using System.Runtime.InteropServices;
using JujutsuTerraria.Items.Techniques.Domains;
using JujutsuTerraria.Items.Shadows;
using JujutsuTerraria.Items.Techniques.AEquip;
using JujutsuTerraria.Misc2;

namespace JujutsuTerraria.Ancients
{
    public class GlobalNPCS : GlobalNPC
    {
        private static int EyeCD;
        private static int SlimeCD;


        public override void AI(NPC npc)
        {
            int Mode;
            if(Main.masterMode == true)
            {
                Mode = 3;
            } else if (Main.expertMode == true)
            {
                Mode = 2;
            }
            else
            {
                Mode = 1;
            }
            //EOC Boss Buff
            if (npc.type == NPCID.EyeofCthulhu)
            {
                int EyeHealth;
                if(Mode == 3)
                {
                    EyeHealth = 3016;
                } else
                if (Mode == 2)
                {
                    EyeHealth = 2366;
                } else
                {
                    EyeHealth = 1400;
                }
                if(npc.life <= EyeHealth)
                {
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                        {
                            return;
                        }
                        if((npc.velocity.Y > 11 || npc.velocity.Y < -11 || npc.velocity.X > 11 || npc.velocity.X < -11) && (EyeCD >= 20) && (NPC.CountNPCS(NPCID.ServantofCthulhu) < 32))
                        {
                            var entitySource = npc.GetSource_FromAI();
                            int type = NPCID.ServantofCthulhu;
                            NPC minionNPC = NPC.NewNPCDirect(entitySource, (int)npc.Center.X, (int)npc.Center.Y, type, npc.whoAmI);
                        EyeCD = 0;

                    }
                    else
                    {
                        EyeCD += 1;

                    }
                }
            }
            if (npc.type == NPCID.KingSlime)
            {
                int SlimeHealth;
                if (Mode == 3)
                {
                    SlimeHealth = 1428;
                }
                else
                if (Mode == 2)
                {
                    SlimeHealth = 1120;
                }
                else
                {
                    SlimeHealth = 800;
                }
                if (npc.life <= SlimeHealth)
                {
                    int SlimeRage = 240;
                    if(npc.life <= SlimeHealth / 3)
                    {
                        SlimeRage = 60;
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {

                        return;
                    }
                    if (SlimeCD >= SlimeRage)
                    {
                        var entitySource = npc.GetSource_FromAI();
                        int type = NPCID.BlueSlime;
                        for (int i = 0; i < 3; i++)
                        {
                            if ((NPC.CountNPCS(NPCID.BlueSlime) < 30)){
                                Player player = Main.player[npc.target];

                                int positive;
                                if (Main.rand.Next(1, 3) == 2) { positive = 1; } else { positive = -1; }
                                Vector2 position2 = new Vector2(player.position.X + (Main.rand.Next(300, 600) * positive), player.position.Y - Main.rand.Next(400, 600));

                                NPC minionNPC = NPC.NewNPCDirect(entitySource, (int)position2.X, (int)position2.Y, type, npc.whoAmI);

                            }
                        }
                        SlimeCD = 0;
                    }
                    else
                    {

                        SlimeCD += 1;

                    }
                }
            }
        }

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
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<PainKiller>(), 100, 1));

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
            if (npc.type == NPCID.MoonLordCore)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DEInfinity>(), 5));
    
                npcLoot.Add(notExpertRule);
            }
            if (npc.type == NPCID.Deerclops)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RoundDeer>(), 4));

                npcLoot.Add(notExpertRule);
            }
            if (npc.type == NPCID.WallofFlesh)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<RestlessGambler>(), 5));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BloodEdge>(), 5));

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DEFire>(), 5));

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
            if (npc.type == NPCID.QueenSlimeBoss)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DESimple>(), 3));



                npcLoot.Add(notExpertRule);
            }
            if (npc.type == NPCID.QueenBee)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<DEJungle>(), 3));


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


                notExpertRule.OnSuccess(leadingConditionRule);
            }

            if (npc.type == NPCID.BrainofCthulhu)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<WrappedCleaver>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<EyeEye>(), 4));
                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<PiercingBlood>(), 4));


                npcLoot.Add(notExpertRule);
            }

            if (npc.type == NPCID.KingSlime)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CountryHammer>(), 3));

                npcLoot.Add(notExpertRule);
            }
            if (npc.type == NPCID.Plantera)
            {
                LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());

                notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<AmplifiedShred>(), 5));

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
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SukunaFinger>(), 10, 1));

            }

            if (item.type == ItemID.MoonLordBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DEInfinity>(), 4));



            }
            if (item.type == ItemID.DeerclopsBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RoundDeer>(), 3));



            }
            if (item.type == ItemID.PlanteraBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<AmplifiedShred>(), 4));



            }
            if (item.type == ItemID.QueenSlimeBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DESimple>(), 2));



            }
            if (item.type == ItemID.WallOfFleshBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RestlessGambler>(), 4));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<BloodEdge>(), 4));
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DEFire>(), 4));


            }
            if (item.type == ItemID.QueenBeeBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<DEJungle>(), 3));


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




            }
            if (item.type == ItemID.KingSlimeBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<CountryHammer>(), 3));


            }
        }
    }

}


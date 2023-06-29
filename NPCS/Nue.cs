using System;
using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using static Terraria.ModLoader.PlayerDrawLayer;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using IL.Terraria.GameContent.UI.ResourceSets;
using TenShadows.Items.Accessories;
using TenShadows.Items.Accessories.Eyes;
using TenShadows.Ancients;
using TenShadows.Ancients;

namespace TenShadows.NPCS
{
    [AutoloadBossHead]
    public class Nue : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.GiantFlyingFox];

            // NPCID.Sets.ShimmerTransformToNPC[NPC.type] = NPCID.Skeleton;
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            { // Influences how the NPC looks in the Bestiary
                Velocity = 5f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
               PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 6f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void BossHeadSlot(ref int index)
        {
            base.BossHeadSlot(ref index);
        }

        public override void SetDefaults()
        {
            NPCID.Sets.CantTakeLunchMoney[Type] = true;
            NPCID.Sets.DontDoHardmodeScaling[Type] = true;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
            NPCID.Sets.MPAllowedEnemies[Type] = true;
            NPC.BossBar = ModContent.GetInstance<ExampleBossBar>();
            NPC.value = 30000;
            NPC.SpawnWithHigherTime(30);
            NPC.boss = true;
            NPC.npcSlots = 10f;
           // NPC.scale = 2.5f;
            NPC.width = 228;
            NPC.height = 105;
            NPC.damage = 10;
            NPC.defense = 7;
            NPC.lifeMax = 750;
            NPC.buffImmune[BuffID.Confused] = true;
            if (!Main.dedServ)
            {
                Music = MusicID.Boss5;
            }
            NPC.HitSound = SoundID.DD2_LightningAuraZap;
            NPC.DeathSound = SoundID.Thunder;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = 14; // Fighter AI, important to choose the aiStyle that matches the NPCID that we want to mimic
                              //  NPC.noTileCollide = true;
            AIType = NPCID.GiantFlyingFox; // Use vanilla zombie's type when executing AI code. (This also means it will try to despawn during daytime)
            AnimationType = NPCID.GiantFlyingFox; // Use vanilla zombie's type when executing animation code. Important to also match Main.npcFrameCount[NPC.type] in SetStaticDefaults.
            //Banner = Item.NPCtoBanner(NPCID.Zombie); // Makes this NPC get affected by the normal zombie banner.
            //BannerItem = Item.BannerToItem(Banner); // Makes kills of this NPC go towards dropping the banner it's associated with.
        }
   
        public override bool? CanBeHitByItem(Player player, Item item)
        {
            if (item.DamageType.CountsAsClass(ModContent.GetInstance<CursedDamage>()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if(projectile.DamageType.CountsAsClass(ModContent.GetInstance<CursedDamage>()) != true)
            {
                damage = 0;
            }


        }
        /* public override float SpawnChance(NPCSpawnInfo spawnInfo)
         {
             return SpawnCondition.OverworldNightMonster.Chance * 0.2f; // Spawn with 1/5th the chance of a regular zombie.
         }*/

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
                            BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("An ancient electric bird that lays dormant in the shadows, terrorizing thousands for generations. Legend says the only way effective way to harm it is through cursed damage"),
                                 
				// By default the last added IBestiaryBackgroundImagePathAndColorProvider will be used to show the background image.
				// The ExampleSurfaceBiome ModBiomeBestiaryInfoElement is automatically populated into bestiaryEntry.Info prior to this method being called
				// so we use this line to tell the game to prioritize a specific InfoElement for sourcing the background image.
			//	new BestiaryPortraitBackgroundProviderPreferenceInfoElement(Fore),
            });
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            // Spawn confetti when this zombie is hit.

            for (int i = 0; i < 10; i++)
            {
                int dustType = 272;
                var dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }
        }
        private int timer;
        private int timer2;

        private int Rage;

        private int Positive;
        private int timer3 = 0;
        public override void AI()
        {
            NPC.noTileCollide = true;
 
            Player player = Main.player[NPC.target];
            
            if (player.dead)
            {
                // If the targeted player is dead, flee
                NPC.velocity.Y -= 0.075f;
                // This method makes it so when the boss is in "despawn range" (outside of the screen), it despawns in 10 ticks
                NPC.EncourageDespawn(10);
                return;
            }
            NPC.damage = 7;

            if (NPC.life <= 600)
            {
                Rage = 2;
            }
            else
            {
                Rage = 1;
            }
            timer2 += Rage;
            timer += Rage;
            if (Main.expertMode == true)
            {
                if (NPC.life <= 1000)
                {
                    if (timer2 >= 4)
                    {
                        var entitySource = NPC.GetSource_FromAI();
                        int type = ModContent.ProjectileType<NueAggFeather>();
                        int damage = NPC.damage;

                        if (Main.rand.Next(1, 3) == 2)
                        {
                            Positive = 1;
                        }
                        else
                        {
                            Positive = -1;
                        }
                        Vector2 position2 = NPC.position - new Vector2(Main.rand.Next(600,700) * Positive, 600);
                        if (Rage == 2)
                        {
                            Vector2 position3 = NPC.position - new Vector2(Main.rand.Next(650, 750)/ Rage * Positive, 600);

                            Projectile.NewProjectile(entitySource, position3, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                        }



                        Projectile.NewProjectile(entitySource, position2, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                        timer2 = 0;
                    }
                }
            }
            if (timer >= 38)
            {
                Vector2 position = NPC.position - new Vector2(Main.rand.Next(-750, 750), 600);

                var entitySource = NPC.GetSource_FromAI();
                int type = ModContent.ProjectileType<NueLightning>();
                int damage = NPC.damage;



                Projectile.NewProjectile(entitySource, position, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                timer = 0;
            }



            if (Main.rand.Next(1, 600) == 2)
            {
                SoundEngine.PlaySound(SoundID.Thunder, NPC.position);

            }
            if (Main.rand.Next(1, 100) == 2)
            {
                SoundEngine.PlaySound(SoundID.DD2_LightningAuraZap, NPC.position);

            }
            if (Main.rand.Next(1, 50) == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    int dustType = 272;
                    var dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType);

                    dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                    dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                    dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
                }
            }

        }

        public override void OnKill()
        {
            DogSummon.KYS = true;
            DogSummon2.KYS = true;
            NPC.SetEventFlagCleared(ref DownedBossSystem.downedNue, -1);

            for (int i = 0; i < 30; i++)
            {
                int dustType = 272;
                var dust = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.07f, 0.07f);
            }
        }
        
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            SoundEngine.PlaySound(SoundID.Item94, NPC.position);

            // Here we can make things happen if this NPC hits a player via its hitbox (not projectiles it shoots, this is handled in the projectile code usually)
            // Common use is applying buffs/debuffs:

            int buffType = BuffID.Electrified;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 1 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // Do NOT misuse the ModifyNPCLoot and OnKill hooks: the former is only used for registering drops, the latter for everything else

            // Add the treasure bag using ItemDropRule.BossBag (automatically checks for expert mode)
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Tiles.TrophyItem>(), 10));

            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<NueBossBag>()));

            // Trophies are spawned with 1/10 chance
            // ItemDropRule.MasterModeCommonDrop for the relic
            //  npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<Items.Placeable.Furniture.MinionBossRelic>()));

            // ItemDropRule.MasterModeDropOnAllPlayers for the pet
            // npcLoot.Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MinionBossPetItem>(), 4));
            // All our drops here are based on "not expert", meaning we use .OnSuccess() to add them into the rule, which then gets added
            LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NueFeather>(), 1, 28, 38));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NueLucky>(), 3));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<NueEye>(), 3));

            npcLoot.Add(notExpertRule);

            // Notice we use notExpertRule.OnSuccess instead of npcLoot.Add so it only applies in normal mode
            // Boss masks are spawned with 1/7 chance
            //  notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MinionBossMask>(), 7));

        }
    }
}

using System;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JujutsuTerraria.GamblingBuffs;
using JujutsuTerraria.Items.Shadows;
using JujutsuTerraria.Projectiles;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Enums;

using Terraria;
using Terraria.ModLoader;
using System.Numerics;
using JujutsuTerraria.Items.Techniques;
using System.Drawing.Imaging;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Ancients;
using JujutsuTerraria.Armor;
using JujutsuTerraria.Items.Techniques.ARestrictions;
using JujutsuTerraria.Items.Accessories;

namespace JujutsuTerraria.Tiles
{
    public class MP : ModPlayer
    {
        public int ZoneChance;
        public int ZoneDamage;
        public int ZoneDuration;

        public int ZoneEffectDamage;
        public int ZoneEffectChance;

        public int ZoneChanceFactorRestless;
        public int ZoneChanceFactorTiger;
        public bool NueMaskOn;

        public int BlackFlashDamageBooster;


        public static bool QuantifiedNue;

        public override void PreUpdateBuffs()
        {
            if(Player.HasBuff<TigerBuff>() == true)
            {
                ZoneChanceFactorTiger = 1;
            }
            else
            {
                ZoneChanceFactorTiger = 0;
            }


        }
        public override void PostUpdateBuffs()
        {
     

            if (BindingVowTile.AmPlaced == true)
            {
                if (Player.HasBuff<BindingVowBuff>() == false)
                {
                    Player.AddBuff(ModContent.BuffType<BindingVowDebuff>(), 6);

                }
            }
        }
        public int ZoneBody;
        public bool ZoneBodyWorn;
        public bool ZoneYujiWorn;
        public int ZoneYuji;
        public int PandaBrass;
        public bool PandaBrassWorn;

        public override void PostUpdateEquips()
        
        
        {
            if (PandaBrassWorn)
            {
                PandaBrass = 1;
            }
            else
            {
                PandaBrass = 0;
            }
            if (ZoneYujiWorn)
            {
                ZoneYuji = 2;
            }
            else
            {
                ZoneYuji = 0;
            }
            if (UniformLegs.BlackFlashBody == true && ZoneBodyWorn == true)
            {
                ZoneBody = 1;
            }
            else
            {
                ZoneBody = 0;
            }
            if (Player.HasBuff<ZoneBuff>())
            {
                if (NueMaskOn == true)
                {
                    Player.GetDamage<CursedDamage>() += 0.15f;
                    ZoneEffectChance = 2;
                }
                else
                {
                    Player.GetDamage<CursedDamage>() += 0.10f;
                    ZoneEffectChance = 1;
                }

            } 
            ZoneChance = 1 + ZoneChanceFactorRestless + ZoneEffectChance + ZoneChanceFactorTiger + ZoneBody + ZoneYuji + PandaBrass; 
            ZoneDamage = 4 + BlackFlashDamageBooster;
            //   Player.wingTimeMax += 30 * FlightBuff.Wearing;
            if (Player.HasBuff<FlightBuff>())
                {
                Player.wingTimeMax += 30;
            }
            if (Player.HasBuff<WingDebuff>())
            {
                Player.wingTimeMax -= 240;
            }


        }
        public override void PostUpdate()
        {
if(Player.HasBuff<HeavenlyBuff>() == true)
            {
                Player.statMana = 0;

                Player.statManaMax2 = 0;
            }        
                }
        public override IEnumerable<Item> AddStartingItems(bool mediumCoreDeath)
        {
        

            return new[] {
                new Item(ModContent.ItemType<DivineDog>()),
                new Item(ModContent.ItemType<CursedEnergy>(), 100),
                new Item(ModContent.ItemType<StandardBV>()),



            };
        }
        public override void ResetEffects()
        {
            ZoneBodyWorn = false;
            ZoneEffectChance = 0;
            ZoneDuration = 5;
            NueMaskOn = false;
            ZoneYujiWorn = false;
            PandaBrassWorn = false;


            if (NPC.downedMoonlord)
            {
                DivineDog.MYDAMAGE = 55;
                UniformBody.UniformBodyDefense = 25;
                UniformBody.CursedStatIncrease = 18;

                UniformLegs.UniformLegsDefense = 13;
                UniformLegs.CursedStatIncrease = 11;
                UniformLegs.BlackFlashBody = true;

            }
            else if (NPC.downedAncientCultist)
            {
                DivineDog.MYDAMAGE = 48;
                UniformBody.UniformBodyDefense = 20;
                UniformBody.CursedStatIncrease = 16;

                UniformLegs.UniformLegsDefense = 12;
                UniformLegs.CursedStatIncrease = 10;
                UniformLegs.BlackFlashBody = true;

            }
            else if (NPC.downedEmpressOfLight)
            {
                DivineDog.MYDAMAGE = 44;
                UniformBody.UniformBodyDefense = 19;
                UniformBody.CursedStatIncrease = 15;

                UniformLegs.UniformLegsDefense = 11;
                UniformLegs.CursedStatIncrease = 9;
                UniformLegs.BlackFlashBody = true;

            }
            else if (NPC.downedFishron)
            {
                DivineDog.MYDAMAGE = 41;
                UniformBody.UniformBodyDefense = 19;
                UniformBody.CursedStatIncrease = 15;

                UniformLegs.UniformLegsDefense = 11;
                UniformLegs.CursedStatIncrease = 9;
                UniformLegs.BlackFlashBody = true;

            }
            else if (NPC.downedGolemBoss)
            {
                DivineDog.MYDAMAGE = 38;
                UniformBody.UniformBodyDefense = 18;
                UniformBody.CursedStatIncrease = 15;

                UniformLegs.UniformLegsDefense = 11;
                UniformLegs.CursedStatIncrease = 9;
                UniformLegs.BlackFlashBody = true;

            }

            else if (NPC.downedPlantBoss)
            {
                DivineDog.MYDAMAGE = 35;
                UniformBody.UniformBodyDefense = 17;
                UniformBody.CursedStatIncrease = 14;

                UniformLegs.UniformLegsDefense = 10;
                UniformLegs.CursedStatIncrease = 8;
                UniformLegs.BlackFlashBody = true;

            }
            else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                DivineDog.MYDAMAGE = 32;
                UniformBody.UniformBodyDefense = 15;
                UniformBody.CursedStatIncrease = 13;

                UniformLegs.UniformLegsDefense = 9;
                UniformLegs.CursedStatIncrease = 7;
                UniformLegs.BlackFlashBody = true;

            }
            else if ((NPC.downedMechBoss1 && NPC.downedMechBoss2) || (NPC.downedMechBoss1 && NPC.downedMechBoss3) || (NPC.downedMechBoss2 && NPC.downedMechBoss3))
            {
                DivineDog.MYDAMAGE = 30;
                UniformBody.UniformBodyDefense = 13;
                UniformBody.CursedStatIncrease = 12;

                UniformLegs.UniformLegsDefense = 8;
                UniformLegs.CursedStatIncrease = 6;
                UniformLegs.BlackFlashBody = true;

            }
            else if (NPC.downedMechBossAny)
            {
                DivineDog.MYDAMAGE = 28;
                UniformBody.UniformBodyDefense = 12;
                UniformBody.CursedStatIncrease = 12;

                UniformLegs.UniformLegsDefense = 8;
                UniformLegs.CursedStatIncrease = 6;
                UniformLegs.BlackFlashBody = true;

            }
            else if (NPC.downedQueenSlime)
            {
                DivineDog.MYDAMAGE = 26;
                UniformBody.UniformBodyDefense = 11;
                UniformBody.CursedStatIncrease = 11;

                UniformLegs.UniformLegsDefense = 7;
                UniformLegs.CursedStatIncrease = 5;
                UniformLegs.BlackFlashBody = true;


            }

            else if (Main.hardMode)
            {
                DivineDog.MYDAMAGE = 24;
                UniformBody.UniformBodyDefense = 10;
                UniformBody.CursedStatIncrease = 10;
                UniformLegs.BlackFlashBody = true;

                UniformLegs.UniformLegsDefense = 7;
                UniformLegs.CursedStatIncrease = 5;

            }
            else if (NPC.downedDeerclops)
            {
                DivineDog.MYDAMAGE = 17;
                UniformBody.UniformBodyDefense = 8;
                UniformBody.CursedStatIncrease = 8;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 5;
                UniformLegs.CursedStatIncrease = 4;


            }
            else if (NPC.downedBoss3)
            {
                DivineDog.MYDAMAGE = 15;
                UniformBody.UniformBodyDefense = 7;
                UniformBody.CursedStatIncrease = 7;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 5;
                UniformLegs.CursedStatIncrease = 4;


            }
            else if (NPC.downedQueenBee)
            {
                DivineDog.MYDAMAGE = 13;
                UniformBody.UniformBodyDefense = 6;
                UniformBody.CursedStatIncrease = 6;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 4;
                UniformLegs.CursedStatIncrease = 3;


            }
            else if (NPC.downedBoss2)
            {
                DivineDog.MYDAMAGE = 11;
                UniformBody.UniformBodyDefense = 5;
                UniformBody.CursedStatIncrease = 5;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 4;
                UniformLegs.CursedStatIncrease = 3;

            }

            else if (NPC.downedBoss1)
            {

                DivineDog.MYDAMAGE = 9;
                UniformBody.UniformBodyDefense = 4;
                UniformBody.CursedStatIncrease = 4;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 3;
                UniformLegs.CursedStatIncrease = 3;


            }
            else if (NPC.downedSlimeKing)
            {
                DivineDog.MYDAMAGE = 7;
                UniformBody.UniformBodyDefense = 3;
                UniformBody.CursedStatIncrease = 3;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 2;
                UniformLegs.CursedStatIncrease = 2;


            }
            else if (DownedBossSystem.downedNue)
            {
                DivineDog.MYDAMAGE = 5;
                UniformBody.UniformBodyDefense = 2;
                UniformBody.CursedStatIncrease = 2;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 2;
                UniformLegs.CursedStatIncrease = 2;


            }
            else {
                DivineDog.MYDAMAGE = 2 ;
                UniformBody.UniformBodyDefense = 1;
                UniformBody.CursedStatIncrease = 1;
                UniformLegs.BlackFlashBody = false;

                UniformLegs.UniformLegsDefense = 1;
                UniformLegs.CursedStatIncrease = 1;

            }


        }
    }
    
}

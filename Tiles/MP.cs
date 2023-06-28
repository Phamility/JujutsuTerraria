using System;
using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenShadows.GamblingBuffs;
using TenShadows.Items.Shadows;
using TenShadows.Projectiles;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Enums;

using Terraria;
using Terraria.ModLoader;
using System.Numerics;
using TenShadows.Items.Techniques;
using System.Drawing.Imaging;
using TenShadows.Items.Materials;
using TenShadows.Ancients;

namespace TenShadows.Tiles
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
        public override void PostUpdateEquips()
        
        
        {
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
            ZoneChance = 1 + ZoneChanceFactorRestless + ZoneEffectChance + ZoneChanceFactorTiger;
            ZoneDamage = 5 + BlackFlashDamageBooster;
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


            };
        }
        public override void ResetEffects()
        {
            ZoneEffectChance = 0;
            ZoneDuration = 5;
            NueMaskOn = false;


            if (NPC.downedMoonlord)
            {
                DivineDog.MYDAMAGE = 55;
          

            }
            else if (NPC.downedAncientCultist)
            {
                DivineDog.MYDAMAGE = 48;


            }
            else if (NPC.downedEmpressOfLight)
            {
                DivineDog.MYDAMAGE = 44;


            }
            else if (NPC.downedFishron)
            {
                DivineDog.MYDAMAGE = 41;


            }
            else if (NPC.downedGolemBoss)
            {
                DivineDog.MYDAMAGE = 38;


            }

            else if (NPC.downedPlantBoss)
            {
                DivineDog.MYDAMAGE = 35;


            }
            else if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
            {
                DivineDog.MYDAMAGE = 32;


            }
            else if ((NPC.downedMechBoss1 && NPC.downedMechBoss2) || (NPC.downedMechBoss1 && NPC.downedMechBoss3) || (NPC.downedMechBoss2 && NPC.downedMechBoss3))
            {
                DivineDog.MYDAMAGE = 30;


            }
            else if (NPC.downedMechBossAny)
            {
                DivineDog.MYDAMAGE = 28;


            }
            else if (NPC.downedQueenSlime)
            {
                DivineDog.MYDAMAGE = 26;



            }

            else if (Main.hardMode)
            {
                DivineDog.MYDAMAGE = 24;
            


            }
            else if (NPC.downedDeerclops)
            {
                DivineDog.MYDAMAGE = 17;


            }
            else if (NPC.downedBoss3)
            {
                DivineDog.MYDAMAGE = 15;


            }
            else if (NPC.downedQueenBee)
            {
                DivineDog.MYDAMAGE = 13;


            }
            else if (NPC.downedBoss2)
            {
                DivineDog.MYDAMAGE = 11;


            }

            else if (NPC.downedBoss1)
            {

                DivineDog.MYDAMAGE = 9;
          
            }
            else if (NPC.downedSlimeKing)
            {
                DivineDog.MYDAMAGE = 7;


            }
            else if (DownedBossSystem.downedNue)
            {
                DivineDog.MYDAMAGE = 5;


            }
            else {
                DivineDog.MYDAMAGE = 2 ;
            

            }


        }
    }
    
}

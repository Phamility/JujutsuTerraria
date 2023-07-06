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

namespace JujutsuTerraria.Ancients
{
    public class MPArmors : ModPlayer
    {
        public bool NobaraHeadOn;
        public bool MakiHeadOn;

        public bool GojoHeadOn;

        public bool DomainActive;

        public int MakiDamageNumber;
        public int MakiMoveNumber;
        public int MakiDefenseNumber;



        public override void ResetEffects()
        {
            DomainActive = false;
            NobaraHeadOn = false;
            MakiHeadOn = false;
            GojoHeadOn = false;

            MakiDamageNumber = 8;
            MakiMoveNumber = 4;
            MakiDefenseNumber = 2;
        }
        public override void PostUpdateEquips()
        {
            if (Player.HasBuff<HeavenlyBuff>())
            {
                if (Player.GetModPlayer<MPArmors>().MakiDamageNumber == 16)
                {
                    Player.GetDamage(DamageClass.Melee) += 0.16f;
                    Player.GetDamage(DamageClass.Ranged) += 0.16f;
                    Player.GetDamage<CursedDamage>() += 0.16f;

                    Player.maxRunSpeed *= 1.08f;
                    //  Player.runAcceleration *= 1.08f;
                    Player.accRunSpeed *= 1.08f;

                    Player.statDefense += 4;
                }
                else
                {
                    Player.GetDamage(DamageClass.Melee) += 0.08f;
                    Player.GetDamage(DamageClass.Ranged) += 0.08f;
                    Player.GetDamage<CursedDamage>() += 0.08f;

                    Player.maxRunSpeed *= 1.04f;
                //    Player.runAcceleration *= 1.04f;
                    Player.accRunSpeed *= 1.04f;

                    Player.statDefense += 2;
                }
            }
        }
    }
}

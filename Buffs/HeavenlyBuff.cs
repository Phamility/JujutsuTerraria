using System; using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using TenShadows.Ancients;

namespace TenShadows.Buffs
{
    public class HeavenlyBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Heavenly Restriction");
            Description.SetDefault("Cursed, melee and ranged damage increased%\nMovement speed increased\nDefense increased\nCan't use cursed energy, mana, or minions"); Main.debuff[Type] = true;

            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.GetModPlayer<MPArmors>().MakiHeadOn == true)
            {
                player.GetDamage(DamageClass.Melee) += 0.16f;
                player.GetDamage(DamageClass.Ranged) += 0.16f;
                player.GetDamage<CursedDamage>() += 0.16f;

                player.maxRunSpeed *= 1.08f;
                player.runAcceleration *= 1.08f;

                player.statDefense += 4;
            }
            else
            {
                player.GetDamage(DamageClass.Melee) += 0.08f;
                player.GetDamage(DamageClass.Ranged) += 0.08f;
                player.GetDamage<CursedDamage>() += 0.08f;

                player.maxRunSpeed *= 1.04f;
                player.runAcceleration *= 1.04f;

                player.statDefense += 2;
            }
            player.statMana = 0;
            player.statManaMax2 = 0;
            player.maxMinions = 0;
            player.maxTurrets = 0;


        }
    }
}

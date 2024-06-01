using System; using JujutsuTerraria.Buffs;
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
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Ancients;

namespace JujutsuTerraria.Buffs
{
    public class HeavenlyCursedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Heavenly Restriction");
            // Description.SetDefault("Cursed, magic and summon damage increased by 18%\nReduced max health and defense"); Main.debuff[Type] = true;

            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {

            player.GetDamage(DamageClass.Summon) += 0.18f;
            player.GetDamage(DamageClass.Magic) += 0.18f;
            player.GetDamage<CursedDamage>() += 0.18f;

            player.statDefense -= 2;
            player.statLifeMax2 -= 100;


        }
    }
}

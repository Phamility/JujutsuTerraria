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

namespace TenShadows.Buffs
{
    public class GeneralBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adapt and Counter");
            Description.SetDefault("Reduces damage taken by 8%\nArmor penetration is increased by 8");
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
            Main.debuff[Type] = true;

        }
        public override void Update(Player player, ref int buffIndex)
        {

            player.GetArmorPenetration(DamageClass.Generic) += 8;
            player.endurance += .08f;



        }
    }
}

using System; using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Audio;

using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using TenShadows.Ancients;

namespace TenShadows.Buffs
{
    public class FlowingRedBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flowing Red Scale");
            Description.SetDefault("Increased cursed damage in exchange for a decrease life regen");
            //  Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {


            player.GetDamage<CursedDamage>() += 0.12f;
      
            player.lifeRegen -= 4;


        }

    }
}

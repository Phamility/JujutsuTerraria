using System; using JujutsuTerraria.Buffs;
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
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
namespace JujutsuTerraria.Misc2
{
    public class SukunaBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vessel");
            // Description.SetDefault("Damaged increased");
            Main.debuff[Type] = false;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            if (MP2.Quantified == 20) {
                tip = ("You stand at the top.!\nDamaged increased by " + MP2.Quantified + "%");

            }
            else
            {
                tip = ("Damaged increased by " + MP2.Quantified + "%");
            }
        }
  

    }
}

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
using JujutsuTerraria.Ancients;
using System.Diagnostics.Metrics;

namespace JujutsuTerraria.Buffs
{
    public class LimitlessBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cursed Speech");
            // Description.SetDefault("Unable to move");
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        private int counter;
        public override void Update(Player player, ref int buffIndex)
        {




        }

    }

}

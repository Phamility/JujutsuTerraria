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

namespace JujutsuTerraria.Buffs
{
    public class FlightBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nue's Wing Reinforcement");
            // Description.SetDefault("Increases movement speed and flight time!\nFlight effects are amplified for Nue-type Wings!");
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.moveSpeed *= 1.10f;
            player.maxRunSpeed *= 1.10f;
            player.accRunSpeed *= 1.10f;
            player.runAcceleration *= 1.10f;



        }

    }

}

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
using JujutsuTerraria.Items.Techniques;

namespace JujutsuTerraria.Buffs
{
    public class GamblingDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Resting Gambler");
            Description.SetDefault("Unable to cast 'Restless Gambler'");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public static int timer = 0;
        public static bool once = true;


        public override void Update(Player player, ref int buffIndex)
        {
            timer++;

            if (timer > 60 * 41 - 1 && once == false)
            {
                once = true;
                if (Main.rand.Next(1, 4) == 2)
                {
                    Main.NewText(player.name + "'s ready to gamble again!", Color.HotPink);
                }
                else
                {
                    if (Main.rand.Next(1, 3) == 2)
                    {
                        Main.NewText(player.name + "'s ready to roll again!", Color.HotPink);
                    }
                    else
                    {
                        Main.NewText(player.name + "'s ready to play again!", Color.HotPink);

                    }
                }

            }
        }
    }
}

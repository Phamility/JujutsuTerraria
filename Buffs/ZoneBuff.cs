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
using TenShadows.Tiles;

namespace TenShadows.Buffs
{
    public class ZoneBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zone");
            Description.SetDefault("10% increased cursed damage\n1% increased black flash chance");
            //  Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            if (player.GetModPlayer<MP>().NueMaskOn)
            {
                tip = "15% increased cursed damage\n2% increased black flash chance";
            }
            else
            {
                tip = "10% increased cursed damage\n1% increased black flash chance";
            }
        }
        public override void Update(Player player, ref int buffIndex)
        {



        }

    }
}

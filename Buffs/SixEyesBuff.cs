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
using JujutsuTerraria.Tiles;
using JujutsuTerraria.Ancients;

namespace JujutsuTerraria.Buffs
{
    public class SixEyesBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Six Eyes");
            // Description.SetDefault("All cursed energy usages are reduced down to 1\nReduces mana usage by 20%\n3 defense\nSee a lot"); Main.debuff[Type] = true;

            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.AddBuff(BuffID.Hunter, 2);

            player.AddBuff(BuffID.Spelunker, 2);
            player.nightVision = true;
            player.sonarPotion = true;
            player.dangerSense = true;

            player.CanSeeInvisibleBlocks = true;
            player.GetDamage<CursedDamage>() += 0.12f;

            player.statDefense += 3;
            player.manaCost -= .2f;
            player.GetModPlayer<MP>().PandaBrassWorn = true;


        }
    }
}

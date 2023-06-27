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

namespace TenShadows.Buffs
{
    public class BindingVowDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Binding Vow: Restrained");
            Description.SetDefault("Damaged reduced by 15%");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {

            player.GetDamage(DamageClass.Generic) -= 0.15f;
            if (player.HasBuff<BindingVowBuff>())
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }


        }

    }
}

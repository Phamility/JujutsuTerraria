using System;
using JujutsuTerraria.Buffs;

using Microsoft.Xna.Framework;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace JujutsuTerraria.Buffs
{
    public class LightningBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Bird Whisperer");
            // Description.SetDefault("Nue fights by your side!");
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            int damage;

            if (player.HasBuff(ModContent.BuffType<JJKBuff>()) == true)
            {
                damage = 1300;
            }
            else
            {


            damage = 20; 
            }
            
            int type = ModContent.ProjectileType<LightningProj>();

            if (player.ownedProjectileCounts[ModContent.ProjectileType<LightningProj>()] <= 3)
            {
                Vector2 position = player.position;
                Projectile.NewProjectile(player.GetSource_Buff(buffIndex), position, -Vector2.UnitY, type, damage, 1f, player.whoAmI);
                player.buffTime[buffIndex] = 18000;

            }
            else
            {

                player.DelBuff(buffIndex);
                buffIndex--;

            }
        }
    }
}

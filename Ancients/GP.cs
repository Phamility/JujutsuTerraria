using System;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JujutsuTerraria.GamblingBuffs;
using JujutsuTerraria.Items.Shadows;
using JujutsuTerraria.Projectiles;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Enums;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using System.Numerics;
using JujutsuTerraria.Items.Techniques;
using System.Drawing.Imaging;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Ancients;
using Terraria.DataStructures;
using JujutsuTerraria.Items.Techniques.Blood;
using JujutsuTerraria.Tiles;
using System.Runtime.Intrinsics.X86;

namespace JujutsuTerraria.Ancients
{
    public class GP : GlobalProjectile
    {
        public static bool AMSHOT = false;
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
    
                if (source is EntitySource_ItemUse_WithAmmo use && use.Item.type == ModContent.ItemType<PiercingBlood>())
            {
                AMSHOT = true;
                projectile.DamageType = ModContent.GetInstance<CursedDamage>();
                // Projectile was spawned from using a YourItem weapon
                // set a bool variable in the class to true here, then check it in ModifyHitNPC
            }
            else
            {
                AMSHOT = false;
            }
 
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
           
            Player player = Main.player[projectile.owner];
            if(hit.Crit && (projectile.arrow) && AMSHOT)
            {
                SoundEngine.PlaySound(SoundID.NPCHit53, target.position);
                int pos;
                int dustType;
                damageDone *= player.GetModPlayer<MP>().ZoneDamage;

                CombatText.clearAll();

                for (int i = 0; i < 30; i++)
                {
                    if (Main.rand.Next(1, 3) == 2)
                    {
                        pos = 1;
                    }
                    else
                    {
                        pos = -1;
                    }
                    if (Main.rand.Next(1, 4) == 2)
                    {
                        dustType = ModContent.DustType<CustomDust>();
                    }
                    else
                    {
                        if (Main.rand.Next(1, 3) == 2)
                        {
                            dustType = ModContent.DustType<CustomDust2>();
                        }
                        else
                        {
                            dustType = ModContent.DustType<CustomDust3>();

                        }
                    }
                    var dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType);

                    dust.velocity.X += Main.rand.NextFloat(.5f, 1f) * pos;
                    dust.velocity.Y += Main.rand.NextFloat(.5f, 1f) * pos;

                    dust.scale *= 1f + Main.rand.NextFloat(-0.05f, 0.05f);
                }
                player.AddBuff(ModContent.BuffType<ZoneBuff>(), 60 * player.GetModPlayer<MP>().ZoneDuration);

                CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkRed, damageDone * 2, true, false);
            }


        }
    }
}

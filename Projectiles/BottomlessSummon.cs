using System;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Ancients;
using Terraria.Audio;
using JujutsuTerraria.Tiles;
using Terraria.Utilities;
using System.Threading;

namespace JujutsuTerraria.Projectiles
{
    public class BottomlessSummon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 24;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public sealed override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.VampireFrog);
            AIType = ProjectileID.VampireFrog;
            Projectile.aiStyle = 67;

            Projectile.width = 136;
            Projectile.height = 50;
            Projectile.tileCollide = true;
            Projectile.friendly = true; 
            Projectile.minion = true;
            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();
            Projectile.minionSlots = 0f; 
            Projectile.penetrate = 12;
            Projectile.Opacity = 1f;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox = new Rectangle((int)Projectile.Center.X, (int)Projectile.Center.Y, 20, 30);
            base.ModifyDamageHitbox(ref hitbox);

            if (Projectile.frame == 24 || Projectile.frame == 21 || Projectile.frame == 23 || Projectile.frame == 20 || Projectile.frame == 22)
            {
                hitbox = new Rectangle((int)Projectile.Center.X, (int)Projectile.Center.Y, 136, 40);

            }
        }
        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Projectile.owner];

            if (crit)
            {
                SoundEngine.PlaySound(SoundID.NPCHit53, target.position);
                int pos;
                int dustType;
                damage *= player.GetModPlayer<MP>().ZoneDamage;

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

                Projectile.active = false;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (crit)
            {
                CombatText.clearAll();

                CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkRed, damage * 2, true, false);

            }

        }
        private int timer;
        public override void AI()
        {
            timer++;
                if(timer > 180)
            {
                Projectile.Opacity -= .015f;
                if(Projectile.Opacity <= 0)
                {
                    Projectile.active = false;
                }
            }

            if (timer > 1800)
            {
                Projectile.active = false;

            }

        }
        
        // The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.


        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
    }
}
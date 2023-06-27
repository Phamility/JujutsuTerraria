using Microsoft.Xna.Framework;

using Terraria;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria.Audio;
using System.Linq;
using Terraria.ModLoader.IO;
using System.Text;
using System.Threading.Tasks;
using TenShadows.Ancients;
using TenShadows.Buffs;
using TenShadows.Tiles;

namespace TenShadows.Projectiles
{
    public class IchorBubble : ModProjectile
    {
        float xspeed;
        float yspeed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Bubble");
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = false;
            // Sets the amount of frames this minion has on its spritesheet
            //    Main.projFrames[Projectile.type] = 4;
            // This is necessary for right-click targeting
            //ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;

            // These below are needed for a minion
            // Denotes that this projectile is a pet or minion
            //Main.projPet[projectile.type] = false;
            // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            //	ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            // Don't mistake this with "if this is true, then it will automatically home". It is just for damage reduction for certain NPCs
            //  ProjectileID.Sets.[projectile.type] = true;
        }

        private int lifespan;
        private int posneg1;
        private int posneg2;
        public sealed override void SetDefaults()
        {
            if (Main.rand.Next(1, 3) == 2){ posneg1 = 1; } else { posneg1 = -1; }

            if (Main.rand.Next(1, 3) == 2) { posneg2 = 1; } else { posneg2 = -1; }
            Projectile.scale = Main.rand.NextFloat(.8f, 1.2f);
            yspeed = Main.rand.NextFloat(.2f, .4f) * posneg1;
            xspeed = Main.rand.NextFloat(.2f, .4f) * posneg2;

            Projectile.width = 48;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;
            //projectile.velocity.X = -rspeed;
            Projectile.velocity.Y = yspeed;
            Projectile.velocity.X = xspeed;
            Projectile.Opacity = 0;
            Projectile.damage = 2;
            Projectile.height = 48;
            // Makes the minion go through tiles freely
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            // These below are needed for a minion weapon
            // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.friendly = true;
            // Only determines the damage type
            //	projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            Projectile.penetrate = 1;
            //Projectile.DamageType = DamageClass.Magic;
            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();

            lifespan = Main.rand.Next(200, 235);
           // Projectile.timeLeft = Main.rand.Next(200,280);
            // Needed so the minion doesn't despawn on collision with enemies or tiles

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        private int timer;
        public override void AI()

        {
            if(Projectile.Opacity < 1)
            {
                Projectile.Opacity += .02f;
            }
            Projectile.rotation += (Projectile.velocity.Y * .02f);

            timer++;
            if (timer >= lifespan)
            {
                SoundEngine.PlaySound(SoundID.Item54, Projectile.position);

                for (int i = 0; i < 10; i++)
                {
                    int dustType = DustID.YellowTorch;
                    var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType);

                    dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                    dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                    dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
                }
                Projectile.active = false;
            }
            Projectile.velocity.X = xspeed;

            // Projectile.rotation += (Projectile.velocity.Y * .00533f);

            Projectile.velocity.Y = yspeed;


            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not

            //projectile.rotation = projectile.velocity.X * .5f;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
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

                CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkRed, damage * 2, true, false);
            }
            SoundEngine.PlaySound(SoundID.Item54, Projectile.position);
            int buffType = BuffID.Ichor;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 12 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;
            for (int i = 0; i < 10; i++)
            {
                int dustType = DustID.YellowTorch;
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }
            Projectile.active = false;


        }

    }
}


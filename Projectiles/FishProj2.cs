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
using JujutsuTerraria.Ancients;

namespace JujutsuTerraria.Projectiles
{
    public class FishProj2 : ModProjectile
    {
        float xspeed;
        float yspeed;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Formed Ice");
            Main.projFrames[Projectile.type] = 6;
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
            Projectile.scale = Main.rand.NextFloat(1f, 1.1f);
            yspeed = 0;
            xspeed = Main.rand.NextFloat(2.5f, 2.76f);

            Projectile.width = 76;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;
            //projectile.velocity.X = -rspeed;
            Projectile.velocity.Y = yspeed;
            Projectile.velocity.X = xspeed;
            Projectile.Opacity = 0;
            Projectile.damage = 2;
            Projectile.CritChance = 0;

            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();
            Projectile.spriteDirection = -1;

            Projectile.height = 26;
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
            lifespan = 90;
            // Projectile.timeLeft = Main.rand.Next(200,280);
            // Needed so the minion doesn't despawn on collision with enemies or tiles
            once = false;
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
        private bool once;
        private void Visuals()
        {
            Projectile.CritChance = 0;

            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 6;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
        }
        public override void AI()

        {
            Visuals();
            if(Projectile.Opacity < 1 && once ==false)
            {
                Projectile.Opacity += .017f;
                if(Projectile.Opacity >= 1)
                {
                    once = true;

                }
            }
          //  Projectile.rotation += (Projectile.velocity.Y * .01f);

            timer++;
            if (timer >= lifespan)
            {
                Projectile.Opacity -= .017f;


            }
            if(Projectile.Opacity <= 0)
            {

                Projectile.active = false;

            }
            Projectile.velocity.X = xspeed;

            // Projectile.rotation += (Projectile.velocity.Y * .00533f);

            Projectile.velocity.Y = yspeed;


            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not

            //projectile.rotation = projectile.velocity.X * .5f;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(SoundID.NPCHit1, Projectile.position);
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

        
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;
            for (int i = 0; i < 10; i++)
            {
                int dustType = DustID.Water;
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }
            Projectile.active = false;


        }

    }
}


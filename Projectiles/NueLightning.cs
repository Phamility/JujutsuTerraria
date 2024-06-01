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
using JujutsuTerraria.NPCS;

namespace JujutsuTerraria.Projectiles
{
    public class NueLightning : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nue's Lightning");
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 4;
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

        private void Visuals()
        {
      
            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 10;

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
        public sealed override void SetDefaults()
        {
           Projectile.scale = Main.rand.NextFloat(.5f, 1);
            yspeed = Main.rand.Next(6, 10);
            Projectile.width = 96;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;
            Projectile.hostile = true;
            //projectile.velocity.X = -rspeed;
            Projectile.velocity.Y = yspeed;
            Projectile.velocity.X = 0;
           
            Projectile.damage = 2;
            Projectile.height = 96;
            // Makes the minion go through tiles freely
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            // These below are needed for a minion weapon
            // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.friendly = false;
            // Only determines the damage type
            //	projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            Projectile.penetrate = 1;
          //  Projectile.DamageType = DamageClass.Magic;
            
            Projectile.timeLeft = 600;
            // Needed so the minion doesn't despawn on collision with enemies or tiles
            Projectile.Opacity = 0;

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
            if (Projectile.Opacity < 1)
            {
                Projectile.Opacity += .02f;
            }
            Visuals();
            Projectile.velocity.X = 0;

            if (Main.rand.Next(10) == 0) // only spawn 20% of the time
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WitherLightning, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);


            }
           // Projectile.rotation += (Projectile.velocity.Y * .00533f);

            Projectile.velocity.Y = yspeed;

  
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not

            //projectile.rotation = projectile.velocity.X * .5f;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {

            for (int i = 0; i < 10; i++)
            {
                int dustType = 272;
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }
            SoundEngine.PlaySound(SoundID.Item94, Projectile.position);

            int buffType = BuffID.Electrified;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 1 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WitherLightning, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);

            Projectile.active = false;
        }

    }
}


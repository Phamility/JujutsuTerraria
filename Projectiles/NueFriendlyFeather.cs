using System; using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JujutsuTerraria.Ancients;

namespace JujutsuTerraria.Projectiles
{
    public class NueFriendlyFeather : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Feather");
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = false;




        }
        public sealed override void SetDefaults()
        {

            yspeed = Main.rand.Next(11, 15);
            Projectile.width = 14;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;

            //projectile.velocity.X = -rspeed;
            Projectile.velocity.Y = yspeed;
            Projectile.DamageType = DamageClass.Magic;

            Projectile.height = 34;
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
            // Needed so the minion doesn't despawn on collision with enemies or tiles
            Projectile.timeLeft = 480; Projectile.Opacity = 0;

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {

        }
        int xspeed;

        public override void AI()
        {
            if (Projectile.Opacity < 1)
            {
                Projectile.Opacity += .025f;
            }
            // xspeed = Main.rand.Next(1, 6) - 3; Projectile.velocity.X = xspeed;
            if (Main.rand.Next(180) == 0) // only spawn 20% of the time
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.WitherLightning, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default(Color), 0.7f);


            }
        
            Player player = Main.player[Projectile.owner];

            //projectile.velocity.X = -rspeed;
            yspeed = Main.rand.Next(11, 15);
            Projectile.velocity.Y = yspeed;
          //  Projectile.rotation += (Projectile.velocity.Y * .02f);

            //projectile.velocity.X = ((rspeed - 10) / 4.5f);
            //projectile.rotation = (rspeed - 10) / (-13.5f);

            Vector2 idlePosition = (player.Center);
            Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();

            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2100)
            {
                // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
                // and then set netUpdate to true
                //projectile.position = idlePosition + new Vector2(lecock2, -lecock);
                Projectile.active = false;
            }

        
        }

    }
}
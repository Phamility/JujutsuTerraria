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
    public class NueAggFeather : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nue's Feather");
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

    
        public sealed override void SetDefaults()
        {
          //  Projectile.scale = Main.rand.NextFloat(.5f, 1);
            yspeed = Main.rand.Next(12, 15);
            Projectile.width = 14;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;
            Projectile.hostile = true;
            //projectile.velocity.X = -rspeed;
            Projectile.velocity.Y = yspeed;
            Projectile.velocity.X = 0;

            Projectile.damage = 2;
            Projectile.height = 34;
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
            Projectile.DamageType = DamageClass.Magic;
            Projectile.Opacity = 0;

            Projectile.timeLeft = 600;
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
            if (Projectile.Opacity < 1)
            {
                Projectile.Opacity += .025f;
            }
            Projectile.velocity.X = 0;

            // Projectile.rotation += (Projectile.velocity.Y * .00533f);

            Projectile.velocity.Y = yspeed;


            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not

            //projectile.rotation = projectile.velocity.X * .5f;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.active = false;
        }

    }
}


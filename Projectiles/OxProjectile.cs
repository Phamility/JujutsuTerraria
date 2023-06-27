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
using TenShadows.Items.Shadows;

using static Terraria.ModLoader.PlayerDrawLayer;
using TenShadows.Ancients;
using TenShadows.Buffs;
using TenShadows.Tiles;

namespace TenShadows.Projectiles
{
    public class OxProjectile : ModProjectile
    {
        int rspeed;
        int xspeed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Piercing Ox");
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 6;
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
        public int positive;
        public sealed override void SetDefaults()
        {
            //Projectile.scale = 3;
            xspeed = 10 * PiercingOx.positive;
            Projectile.spriteDirection = PiercingOx.positive;
            Projectile.width = 234;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;
            Projectile.hostile = false;
            //projectile.velocity.X = -rspeed;
            Projectile.velocity.Y = 0;
            Projectile.velocity.X = xspeed;
            Projectile.friendly = true;
            Projectile.damage = 2;
            Projectile.height = 190;
            // Makes the minion go through tiles freely
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            // These below are needed for a minion weapon
            // Only controls if it deals damage to enemies on contact (more on that later)
            // Only determines the damage type
            //	projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            Projectile.penetrate = -1;
            //  Projectile.DamageType = DamageClass.Magic;
            Projectile.Opacity = 0; Projectile.DamageType = ModContent.GetInstance<CursedDamage>();


            Projectile.timeLeft = 480;
            // Needed so the minion doesn't despawn on collision with enemies or tiles

        }
        public override bool? CanCutTiles()
        {
            return false;
        }
    
        private int timer;
        public override void AI()

        {

            if (Projectile.Opacity < 1)
            {
                Projectile.Opacity += .025f;
            }
            Visuals();
            Projectile.velocity.X = 0;

            if (Main.rand.Next(1, 4) == 2)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                if (Main.rand.Next(1, 3) == 2)
                {
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
                }
                else
                {
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Mud);




                    dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                    dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                    dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
                }
            }

            // Projectile.rotation += (Projectile.velocity.Y * .00533f);

            Projectile.velocity.X = xspeed;

  
            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not

            //projectile.rotation = projectile.velocity.X * .5f;
        }
        private Color trailColor;
        private bool trailActive;

        // Here, a method is provided for setting the above fields.

    
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
            SoundEngine.PlaySound(SoundID.NPCDeath43, Projectile.position);
            int buffType = BuffID.Confused;
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            int timeToAdd = 3 * 60; //This makes it 5 seconds, one second is 60 ticks
            target.AddBuff(buffType, timeToAdd);
            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;
            for (int i = 0; i < 10; i++)
            {
                int dustType = DustID.Stone;
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }
            

        }
    }
}


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
using JujutsuTerraria.Items.Shadows;
using JujutsuTerraria.Items.Techniques;

using Terraria.GameContent;
using ReLogic.Content;
using JujutsuTerraria.Tiles;

namespace JujutsuTerraria.Projectiles
{
    public class BloodWheelProj : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Wheel");
            Main.projFrames[Projectile.type] = 3;
            Main.projPet[Projectile.type] = false;




        }
        private int lockedin;
        public sealed override void SetDefaults()
        {
            xspeed = 6.5f * CountryHammer.positive;
            Projectile.spriteDirection = CountryHammer.positive;
            lockedin = CountryHammer.positive;
            Projectile.width = 45;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;

            //projectile.velocity.X = -rspeed;
            //   Projectile.velocity.X = yspeed;
            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();

            Projectile.height = 45;
            // Makes the minion go through tiles freely
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            // These below are needed for a minion weapon
            // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.friendly = true;
            // Only determines the damage type
            //	projectile.minion = true;
            // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
           Projectile.penetrate = -1;
            // Needed so the minion doesn't despawn on collision with enemies or tiles
          //  Projectile.timeLeft = 120; 
            Projectile.Opacity = .4f;
            Projectile.aiStyle = 1;
            AIType = ProjectileID.Bullet; // Act exactly like default Bullet
        }

        public override bool? CanCutTiles()
        {
            return true;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
      
                if (Projectile.velocity.X != oldVelocity.X)
                {
                Projectile.velocity.X = -oldVelocity.X;
                }
                if (Projectile.velocity.Y != oldVelocity.Y)
                {
                Projectile.velocity.Y = -oldVelocity.Y;
                }
            Projectile.velocity *= 0.93f;
                SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            
            return false;
        }

        float xspeed;
       private int timer = 0;
        private bool once;
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            if (hit.Crit)
            {
                SoundEngine.PlaySound(SoundID.NPCHit53, target.position);
                int pos;
                int dustType;
                target.life += damageDone;
                bool cock = false;
                damageDone *= player.GetModPlayer<MP>().ZoneDamage;
                if (damageDone > target.life )
                {
                    damageDone = target.life - Main.rand.Next(5, 30);
                    cock = true;
                }
                target.life -= damageDone;
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

                if (cock == false) { CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkRed, damageDone, true, false); }
            }

            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;

            // Alternatively, you can use a vanilla buff: int buffType = BuffID.Slow;
            for (int i = 0; i < 10; i++)
            {
                int dustType = DustID.BloodWater;
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }


        }

        public override void AI()
        {
            for (int i = 0; i < 5; i++)
            {
                int dustType = DustID.BloodWater;
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType);

                dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
            }
            int frameSpeed = 5;

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
            Lighting.AddLight(Projectile.position, Color.White.ToVector3() * .5f);

            Projectile.Opacity = 1f;

            if (once == false && timer > 3)
            {
                once = true;
                Projectile.velocity.X *= 2;
                Projectile.velocity.Y *= 2;

            }
            timer++;
                if(timer > 180)
            {

                Projectile.active = false;
            }


        
        }

    }
}
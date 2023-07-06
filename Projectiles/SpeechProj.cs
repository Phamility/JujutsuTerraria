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
namespace JujutsuTerraria.Projectiles
{
    public class SpeechProj : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Speech Wave");
            Main.projFrames[Projectile.type] = 1;
            Main.projPet[Projectile.type] = false;




        }
        private int lockedin;
        public sealed override void SetDefaults()
        {
            xspeed = 6.5f * CountryHammer.positive;
            Projectile.spriteDirection = CountryHammer.positive;
            lockedin = CountryHammer.positive;
            Projectile.width = 240;
            //projectile.aiStyle = 54;
            //aiType = NPCID.Raven;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            //projectile.velocity.X = -rspeed;
            //   Projectile.velocity.X = yspeed;
            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();

            Projectile.height = 138;
            // Makes the minion go through tiles freely
            Projectile.tileCollide = false;
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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (target.type == NPCID.SkeletronHand || target.type == NPCID.QueenBee) { 
            }
            else
            {
                target.AddBuff(ModContent.BuffType<CursedBuff>(), 60 + 60 + 30);
            }
        }
        float xspeed;
       private int timer = 0;
        private bool once;

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 drawOrigin = new Vector2(projectileTexture.Width * 0.5f, Projectile.height * 0.5f);
            SpriteEffects spriteEffects = Projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        
        
            return true;
        }
        public override void AI()
        {
            Lighting.AddLight(Projectile.position, Color.White.ToVector3() * .5f);

            Projectile.Opacity = .4f;

            if (once == false)
            {
                once = true;
                Projectile.velocity.X *= 3;
                Projectile.velocity.Y *= 3  ;

            }
            timer++;
                if(timer > 150)
            {

                Projectile.active = false;
            }


        
        }

    }
}
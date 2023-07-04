using System; using TenShadows.Buffs;
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
//using static Terraria.ModLoader.ModContent;

using TenShadows.Ancients;
using TenShadows.Items.Shadows;
using TenShadows.Items.Techniques;
using Terraria.GameContent;
using ReLogic.Content;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;

namespace TenShadows.Projectiles
{
    public class DomainInfinity : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinite Domain");
            Main.projFrames[Projectile.type] = 1;
           Main.projPet[Projectile.type] = false;




        }
        private int lockedin;
        public sealed override void SetDefaults()
        {
            Projectile.width = 825;
  
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 7;
            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();

            Projectile.height = 825;
            Projectile.tileCollide = false;
           Projectile.ignoreWater = true;

            Projectile.friendly = true;
            Projectile.hostile = false;

           Projectile.penetrate = -1 ;
            Projectile.Opacity = 0f;
            Projectile.scale = 1;
            Projectile.hide = true;
        }
     

        public override bool? CanCutTiles()
        {
            return false;
        }

        public int TargetWhoAmI
        {
            get => (int)Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
 
        public override bool? CanHitNPC(NPC target)
        {
            if (target.friendly == true || target.immune[Projectile.owner] == 5) { return false; }
            else
            {
                return true;
            }
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float radius = 825/2;
            return Projectile.Center.DistanceSQ(targetHitbox.ClosestPointInRect(Projectile.Center)) < radius * Projectile.scale * radius * Projectile.scale;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.immune[Projectile.owner] = 5;

        }
        public override bool? CanDamage()
        {
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            			target.immune[Projectile.owner] = 5;


            TargetWhoAmI = target.whoAmI;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Main.instance.LoadProjectile(Projectile.type);
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);
            Main.EntitySpriteDraw(texture, Projectile.Center - Main.screenPosition, null, Color.White * Wacko, Projectile.rotation, origin, Projectile.scale, SpriteEffects.None, 0);
            return false;   
        }
 
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            Main.instance.DrawCacheProjsBehindNPCsAndTiles.Add(index);
        }


        public float Wacko;
        bool once = false;
        public override void AI()
        {
            Projectile.Size = new Vector2(522, 522) * Projectile.scale; 
           if (once == false)
            {
                Wacko = 0;
                once = true;
                Projectile.scale = 0;
                Projectile.Opacity = 0;
                
            }

            Lighting.AddLight(Projectile.Center, Color.OrangeRed.ToVector3() * 0.78f);

            Player player = Main.player[Projectile.owner];
            // If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
            if (player.channel)
            {

                if (Wacko <= .85)
                {
                    Wacko += .025f;
                    Projectile.Opacity += .005f;
                }
                if (Projectile.scale <= 1)
                {
                    Projectile.scale += .03f;
                }

                Projectile.Center = player.Center;
            }
            else
            {

                Projectile.Center = player.Center;

                Projectile.scale -= .08f;
                Wacko -= .052f;
                Projectile.Opacity -= .1f;
                if (Projectile.scale <= 0)
                {
                    Projectile.active = false;
                }

            }
        }

    }
}
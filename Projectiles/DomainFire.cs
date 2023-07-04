﻿using System; using TenShadows.Buffs;
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

namespace TenShadows.Projectiles
{
    public class DomainFire : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Volanic Domain");
         //    Main.projFrames[Projectile.type] = 1;
           Main.projPet[Projectile.type] = true;




        }
        private int lockedin;
        public sealed override void SetDefaults()
        {
            Projectile.width = 522;
  
            Projectile.damage = 20;

            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();

            Projectile.height = 522;
            Projectile.tileCollide = false;
       //     Projectile.ignoreWater = false;

            Projectile.friendly = true;
            Projectile.hostile = false;

           Projectile.penetrate = 1 ;
            Projectile.Opacity = 0f;
            Projectile.scale = 0;
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire3, 60 * 10);
            target.AddBuff(BuffID.ShadowFlame, 60 * 10);
            TargetWhoAmI = target.whoAmI;
        }
   
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            int npcIndex = TargetWhoAmI;
            if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
            {
                if (Main.npc[npcIndex].behindTiles)
                {
                    behindNPCsAndTiles.Add(index);
                }
                else
                {
                    behindNPCsAndTiles.Add(index);
                }

                return;
            }
            behindNPCsAndTiles.Add(index);

        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            // If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
            if (player.channel)
            {
                if (Projectile.Opacity < 1)
                {
                    Projectile.Opacity += .04f;
                }
                if (Projectile.Opacity < 1)
                {
                    Projectile.scale += .03f;
                }
                Projectile.Center = player.Center - new Vector2 (260, 180);
            }
            else
            {
                Projectile.Opacity -= .15f;
                Projectile.scale -= .15f;
                if(Projectile.Opacity <=0 && Projectile.scale <= 0)
                {
                    Projectile.active = false;
                }

            }
        }

    }
}
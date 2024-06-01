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
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ID;
using Terraria.ModLoader;
//using static Terraria.ModLoader.ModContent;

using JujutsuTerraria.Ancients;
using JujutsuTerraria.Items.Shadows;
using JujutsuTerraria.Items.Techniques;
using Terraria.GameContent;
using ReLogic.Content;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Items.Techniques.Domains;
using System.Diagnostics.Metrics;

namespace JujutsuTerraria.Projectiles
{
    public class DomainInfinity : ModProjectile
    {
        int rspeed;
        int yspeed;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinite Domain");
            Main.projFrames[Projectile.type] = 1;
           Main.projPet[Projectile.type] = false;




        }
        private int lockedin;
        public sealed override void SetDefaults()
        {
            Projectile.width = 820;
  
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 10;
            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();

            Projectile.height = 820;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;

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
            float radius = 820 / 2;
            return Projectile.Center.DistanceSQ(targetHitbox.ClosestPointInRect(Projectile.Center)) < radius * Projectile.scale * radius * Projectile.scale;
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            target.immune[Projectile.owner] = 5;

        }
        public override bool? CanDamage()
        {
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
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
            Main.instance.DrawCacheProjsBehindNPCs.Add(index);
        }


        public float Wacko;
        public int timer=0;
        bool once = false;
        public int counter;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            player.GetModPlayer<MPArmors>().DomainActive = true;
            Player Local = Main.LocalPlayer;
            timer++;

            if (Main.netMode != NetmodeID.Server && !Filters.Scene["Shockwave"].IsActive())
            {
                //Main.NewText("ZoneActive");
                Filters.Scene.Activate("Shockwave", player.Center).GetShader().UseColor(3, 5, 15).UseTargetPosition(player.Center);
            }
            if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
            {
              //  Main.NewText("ZoneIncreasing");

                float progress = (timer) / 60f; // Will range from -3 to 3, 0 being the point where the bomb explodes.
                Filters.Scene["Shockwave"].GetShader().UseProgress(progress).UseOpacity(100 * (1 - progress / 3f));
            }

            Projectile.Size = new Vector2(820, 820) * Projectile.scale; 
           if (once == false)
            {
                Wacko = 0;
                once = true;
                Projectile.scale = 0;
                Projectile.Opacity = 0;
                
            }
            if (Main.rand.Next(1, 8) == 2)
            {
                var dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.GolfPaticle);
                if (Main.rand.Next(1, 3) == 2)
                {
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.BoneTorch);
                }
                else
                {
                    dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.PortalBolt);




                    dust.velocity.X += Main.rand.NextFloat(-0.3f, 0.3f);
                    dust.velocity.Y += Main.rand.NextFloat(-0.3f, 0.3f);

                    dust.scale *= 1f + Main.rand.NextFloat(-0.03f, 0.03f);
                }
            }

            Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);



            // If the player channels the weapon, do something. This check only works if item.channel is true for the weapon.
            if (player.channel)
            {
                counter++;
                if (counter >= 60)
                {
                    counter = 0;
                    bool once = false;
                    for (int i = 0; i < Main.InventorySlotsTotal; i++)
                    {
                        if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>() && once == false)
                        {
                            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()))
                            {
                                player.inventory[DEInfinity.InventoryNumber].stack -= DEInfinity.Cost - DEInfinity.Reduction;
                                once = true;


                            }
                            else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
                            {
                                player.inventory[DEInfinity.InventoryNumber].stack -= DEInfinity.Cost - DEInfinity.Reduction;
                                once = true;


                            }
                            else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
                            {
                                player.inventory[DEInfinity.InventoryNumber].stack -= DEInfinity.Cost - DEInfinity.Reduction;
                                once = true;


                            }
                            else
                            {
                                player.inventory[DEInfinity.InventoryNumber].stack -= DEInfinity.Cost - DEInfinity.Reduction;
                                once = true;

                            }
                        }
                    }
                }

                if (player.inventory[DEInfinity.InventoryNumber].stack < DEInfinity.Cost - DEInfinity.Reduction)
                {
                    Projectile.Center = player.Center;

                    Projectile.scale -= .1f;
                    Wacko -= .045f;
                    if (Projectile.scale <= 0)
                    {
                        Projectile.active = false;
                    }
                }
                else
                {

                    Vector2 center = new Vector2((int)player.position.X, (int)player.position.Y);
                    const float range = 16 * 25;  // 20 tiles
                    if (Local.DistanceSQ(center) <= range * range)
                    {
                        Local.AddBuff(BuffID.NebulaUpDmg1, 60 * 10);
                    }
                    if (Wacko <= .85)
                    {
                        Wacko += .025f;
                    }
                    if (Projectile.scale <= 1)
                    {
                        Projectile.scale += .03f;
                    }

                    Projectile.Center = player.Center;
                }
            }
            else
            {

                Projectile.Center = player.Center;

                Projectile.scale -= .08f;
                Wacko -= .052f;
                if (Projectile.scale <= 0)
                {
                    if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                    {
                        Filters.Scene["Shockwave"].Deactivate();
                    }
                    Projectile.active = false;
                }

            }
        }

    }
}
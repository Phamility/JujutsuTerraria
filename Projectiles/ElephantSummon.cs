using System; using JujutsuTerraria.Buffs;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Items.Shadows;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using JujutsuTerraria.Ancients;
using System.Collections.Generic;

namespace JujutsuTerraria.Projectiles
{
    // This file contains all the code necessary for a minion
    // - ModItem - the weapon which you use to summon the minion with
    // - ModBuff - the icon you can click on to despawn the minion
    // - ModProjectile - the minion itself

    // It is not recommended to put all these classes in the same file. For demonstrations sake they are all compacted together so you get a better overwiew.
    // To get a better understanding of how everything works together, and how to code minion AI, read the guide: https://github.com/tModLoader/tModLoader/wiki/Basic-Minion-Guide
    // This is NOT an in-depth guide to advanced minion AI

    public class ElephantSummon : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Max Elephant Summon");

            // Sets the amount of frames this minion has on its spritesheet
       //     Main.projFrames[Projectile.type] = 10;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public sealed override void SetDefaults()
        {
            //  Projectile.CloneDefaults(ProjectileID.StormTigerTier1);


            //    Projectile.aiStyle = ProjectileID.StormTigerTier1;
            Projectile.spriteDirection = MaxElephant.positive;

            // AIType = ProjectileID.StormTigerTier1;
            //    Projectile.knockBack = 1;
            Projectile.hostile = false;
         //   Projectile.minionSlots = .5f;
            Projectile.minion = false;
            Projectile.width = 424;
            Projectile.height = 264;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.sentry = true;
            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();
            Projectile.penetrate = -1;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.hide = true;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return false;
        }
        public override void OnSpawn(IEntitySource source)
        {
            Player player = Main.player[Projectile.owner];

        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            behindNPCs.Add(index);
        }

        private int timerlife;
        private int fucker;
        private int NewLife;

        // The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
        public override void AI()
        {

           
           // Projectile.velocity.Y += .25f;

            var entitySource = Projectile.GetSource_FromAI();
             int type = ProjectileID.WaterStream;
          //  int type = ModContent.ProjectileType<EWater>();

            int damage = Projectile.damage;
            Player player = Main.player[Projectile.owner];

            NewLife++;
            if(NewLife >= 60 * 3)
            {
                Projectile.active = false;
            }

            timerlife++;
            if(MaxElephant.positive == 1)
            {
                fucker = 387;

            }
            else
            {
                fucker = 40;
            }
            Vector2 ShootPosition = Projectile.position + new Vector2(fucker, 90);



        
            Vector2 ShootSpeed2 = new Vector2(Main.rand.NextFloat(-8, 8), -15);
            Projectile.NewProjectile(entitySource, ShootPosition, -Vector2.UnitY + ShootSpeed2, type, damage, 0f, player.whoAmI);
            
            // Vector2 ShootSpeed2 = new Vector2(8, -15);
            //  Projectile.NewProjectile(entitySource, ShootPosition, -Vector2.UnitY + ShootSpeed2, type, damage, 0f, player.whoAmI);
            //   Vector2 ShootSpeed3 = new Vector2(-8, -15);
            //  Projectile.NewProjectile(entitySource, ShootPosition, -Vector2.UnitY + ShootSpeed3, type, damage, 0f, player.whoAmI);


            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<ElephantBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<ElephantBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            if (player.HasBuff(ModContent.BuffType<ElephantBuff>()) == false)
            {
                Projectile.active = false;
            }
            if((player.ownedProjectileCounts[ModContent.ProjectileType<ElephantSummon>()] > 1) && timerlife >= 30) {

                Projectile.active = false;

            }
            Lighting.AddLight(Projectile.Center, Color.Pink.ToVector3() * .5f);

        }

    }
       
   
    }
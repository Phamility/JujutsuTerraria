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

namespace JujutsuTerraria.Projectiles
{
    // This file contains all the code necessary for a minion
    // - ModItem - the weapon which you use to summon the minion with
    // - ModBuff - the icon you can click on to despawn the minion
    // - ModProjectile - the minion itself

    // It is not recommended to put all these classes in the same file. For demonstrations sake they are all compacted together so you get a better overwiew.
    // To get a better understanding of how everything works together, and how to code minion AI, read the guide: https://github.com/tModLoader/tModLoader/wiki/Basic-Minion-Guide
    // This is NOT an in-depth guide to advanced minion AI


    public class DogSummon2 : ModProjectile
    {
        public static bool KYS;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Divine Dog Summon");

            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 10;
            // This is necessary for right-click targeting
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public sealed override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.StormTigerTier1);


        //    Projectile.aiStyle = ProjectileID.StormTigerTier1;

                AIType = ProjectileID.StormTigerTier1;
        //    Projectile.knockBack = 1;
            Projectile.hostile = false;
         //   Projectile.minionSlots = .5f;
            Projectile.minion = false;
            Projectile.width = 52;
            Projectile.height = 32;
            Projectile.tileCollide = true;
        
            Projectile.friendly = true;

            Projectile.DamageType = ModContent.GetInstance<CursedDamage>();
            Projectile.penetrate = -1;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return true;
        }
        private bool once = false;
        private int Decide = 1;
        private int timer;
        // The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            timer++;
            if (timer >= 60)
            {
                Decide *= -1;
                timer = 0;
            }

            if (KYS == true)
            {

                Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), player.position, player.velocity, ModContent.ProjectileType<DogSummon2>(), DivineDog.MYDAMAGE, 2, player.whoAmI);
                KYS = false;
                Task.Delay(1);
                Projectile.active = false;
            }


            if (once == false)
            {
                Projectile.velocity *= 1.5f;

     once = true;
            }
            if((Projectile.velocity.X >= 0 && Projectile.velocity.X < 2) || Projectile.velocity.X <= 0 && Projectile.velocity.X > -2)
            {
                Projectile.velocity.X += 4.5f * player.direction * Decide;
            }

            // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
            if (player.dead || !player.active)
            {
                player.ClearBuff(ModContent.BuffType<DivineDogBuff>());
            }
            if (player.HasBuff(ModContent.BuffType<DivineDogBuff>()))
            {
                Projectile.timeLeft = 2;
            }
            if (player.HasBuff(ModContent.BuffType<DivineDogBuff>()) == false)
            {
                Projectile.active = false;
            }
        }
   
    }
}
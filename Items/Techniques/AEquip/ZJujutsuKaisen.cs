using System; 
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using rail;
using JujutsuTerraria.Tiles;
using JujutsuTerraria.Items.Techniques.AEquip;
using JujutsuTerraria.Items.Accessories.Eyes;
using JujutsuTerraria.Items.Accessories;
using JujutsuTerraria.Ancients;
using System.Diagnostics.Metrics;

namespace JujutsuTerraria.Items.Techniques.AEquip
{
    public class ZJujutsuKaisen : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs
 
        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            // DisplayName.SetDefault("The Six Eyes");
            // Tooltip.SetDefault("Throughout Heaven and Earth, I alone am the honored one!");
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 38;
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = -12;
            
            Item.accessory = true;

        }

        private int counter;
        private int timer;

        private int timer2;

        private int timer3;

        private int timer4;


        private int damage;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<JJKBuff>(), 2);
            player.AddBuff(ModContent.BuffType<SummonNueBuff>(), 2);


            counter++;

            player.GetModPlayer<MP>().NueMaskOn = true;

            if (player.HasBuff(ModContent.BuffType<LimitlessBuff>()))
            {
                if (counter >= 240)
                {
                    player.immune = true;
                }
                if (counter >= 360)
                {
                    counter = 0;
                }
            }
            else
            {

                if (counter >= 600)
                {
                    player.immune = true;
                }
                if (counter >= 720)
                {
                    counter = 0;
                }
            }
            if (player.statLife <= 100)
            {
                player.lifeRegen += 100;
                player.statLife += 1;
            }
            timer++;
            if (timer > 10)
            {
                Vector2 position = player.position - new Vector2(Main.rand.Next(-150, 150), Main.rand.Next(-150, 150));

                var entitySource = player.GetSource_FromAI();
                int type = ModContent.ProjectileType<IceProj>();
                if (Main.expertMode == true)
                {
                    damage = 500;

                }
                else
                {
                    damage = 400;

                }



                Projectile.NewProjectile(entitySource, position, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                timer = 0;
            }
            timer4++;
            timer2++;
            timer3++;

            if (timer4 > 5)
            {
                Vector2 position = player.position - new Vector2(Main.rand.Next(175, 200), Main.rand.Next(-150, 150));

                var entitySource = player.GetSource_FromAI();
                int type = ModContent.ProjectileType<FishProj>();
                if (Main.expertMode == true)
                {
                    damage = 300;

                }
                else
                {
                    damage = 150;

                }



                Projectile.NewProjectile(entitySource, position, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                timer4 = 0;
            }
            if (timer3 > 10)
            {
                Vector2 position = player.position - new Vector2(Main.rand.Next(175, 200), Main.rand.Next(-150, 150));

                var entitySource = player.GetSource_FromAI();
                int type = ModContent.ProjectileType<FishProj3>();
                if (Main.expertMode == true)
                {
                    damage = 400;

                }
                else
                {
                    damage = 200;

                }



                Projectile.NewProjectile(entitySource, position, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                timer3 = 0;
            }
            if (timer2 > 15)
            {
                Vector2 position = player.position - new Vector2(Main.rand.Next(175, 200), Main.rand.Next(-150, 150));

                var entitySource = player.GetSource_FromAI();
                int type = ModContent.ProjectileType<FishProj2>();
                if (Main.expertMode == true)
                {
                    damage = 600;

                }
                else
                {
                    damage = 300;

                }



                Projectile.NewProjectile(entitySource, position, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                timer2 = 0;
            }


        }
        public override void AddRecipes()
        {
            CreateRecipe()
                                .AddIngredient<CursedEnergy>(999)
                                .AddIngredient<SixEyes>(1)
                                .AddIngredient<Limitless>(1)
                                .AddIngredient<NueMask>(1)

                         .AddIngredient<IceFormation>(1)
                                                  .AddIngredient<DeathSwarm>(1)

                                .AddIngredient<Miracle>(1)
                        .AddIngredient<Ratio>(1)






        .AddTile<ShrineTile>()

                .Register();
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
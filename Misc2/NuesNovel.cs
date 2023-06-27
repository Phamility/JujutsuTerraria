using System;
using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;

namespace TenShadows.Misc2
{
    public class NuesNovel : ModItem

    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nue's Novel");
            Tooltip.SetDefault("Rain a barrage of feathers from the sky!");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<NueFeather>(8)
                .AddIngredient(ItemID.Book, 1)

                .AddTile(TileID.WorkBenches)
               .Register();
        }
        public override void SetDefaults()
        {

            Item.damage = 8;
            Item.width = 30;
            Item.mana = 8;
            Item.height = 28;

            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.HoldUp; // How you use the item (swinging, holding out, etc.)
            Item.knockBack = 3;
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            //      item.shootSpeed = 4f;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<NueFriendlyFeather>();

        }
        public int positive;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position.X = Main.MouseWorld.X;
            position.Y = player.position.Y - 500;

            int numberProjectiles = 5;
            for (int i = 0; i < numberProjectiles; i++)
            {
                position.X = Main.MouseWorld.X + (Main.rand.Next(1, 80) - 40);
                position.Y = player.position.Y - 500 + Main.rand.Next(-100, 100);

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                //Projectile.NewProjectile(Main.MouseWorld.X, player.position.Y - 800, 0f, 0f, ProjectileID.Bomb, damage, 4, player.whoAmI);
            }

            return false;

        }

    }
}



using System; using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Audio;
using TenShadows.Tiles;

using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using TenShadows.Ancients;


namespace TenShadows.Items.Shadows
{
    public class PiercingOx : ModItem

    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ox Silhouette");
            Tooltip.SetDefault("Conjure a piercing ox that charges through the screen!\nThe ox's charge causes enemies to be confused!");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(200)
                .AddIngredient<Horn>(2)

                .AddTile<ShrineTile>()
                .Register();
        }
        public override void SetDefaults()
        {

            Item.damage = 42;
            Cost = 20;
            Item.width = 30;
            Item.height = 28;
            Item.crit = 8;
            Item.useTime = 100;
            Item.useAnimation = 100;
            Item.useStyle = ItemUseStyleID.HoldUp; // How you use the item (swinging, holding out, etc.)
            Item.knockBack = 6;
            Item.rare = ItemRarityID.Orange; // The color that the item's name will be in-game.
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.UseSound = SoundID.NPCDeath43; // What sound should play when using the item
            Item.noMelee = true;
            //      item.shootSpeed = 4f;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<OxProjectile>();

        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            damage *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {

            crit = player.GetModPlayer<MP>().ZoneChance;
        }

        public override void UpdateInventory(Player player)
        {
            Cost = 20;

            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()))
            {
                Reduction = Cost - 1;
            }
            else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
            {

                Reduction = 4;
            }
            else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
            {

                Reduction = 2;
            }
            else
            {
                Reduction = 0;
            }
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
          //  TooltipLine cock = tooltips.Find(x => x.Name == "CritChance" && x.Mod == "Terraria");

            // Get the vanilla damage tooltip
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {
                // We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
                // So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
                string[] splitText = tt.Text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                // Change the tooltip text
                tt.Text = damageValue + " cursed damage";
            }
            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"Costs {Cost - Reduction} cursed energy") { OverrideColor = Color.DodgerBlue };
          //  tooltips.Remove(cock);

            tooltips.Insert(1, tooltip);
            TooltipLine COCK = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
            tooltips.Remove(COCK);
            Player player = Main.LocalPlayer;
            TooltipLine BLACKFLASHCHANCE = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MP>().ZoneChance}% black flash chance") { OverrideColor = Color.White };
            if (Item.favorited == true)
            {
                tooltips.Insert(5, BLACKFLASHCHANCE);
            }
            else
            {
                tooltips.Insert(3, BLACKFLASHCHANCE);
            }
        }
        public override bool? UseItem(Player player)
        {

            bool once = false;
            for (int i = 0; i < Main.InventorySlotsTotal; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>() && once == false)
                {
                    if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= Cost - Reduction;
                        once = true;


                    }
                    else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= Cost - Reduction;
                        once = true;


                    }
                    else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= Cost - Reduction;
                        once = true;


                    }
                    else
                    {
                        player.inventory[InventoryNumber].stack -= Cost - Reduction;
                        once = true;


                    }
                }
            }
            return true;
        }

        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;

        public override bool CanUseItem(Player player)
        {
            bool Condition2 = false;
            Cost = 20;
            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()))
            {
                Reduction = Cost - 1;
            }
            else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
            {

                Reduction = 4;
            }
            else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
            {

                Reduction = 2;
            }
            else
            {
                Reduction = 0;
            }
            bool Condition1;
            if (player.HasBuff<HeavenlyBuff>())
            {

                Condition1 = false;
            }
            else
            {
                Condition1 = true;
            }

            for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>())
                {
                    if ((player.inventory[i].stack >= Cost - Reduction) && Condition1 == true)
                    {
                        InventoryNumber = i;
                        return true;

                    }
                    else
                    {
                        Condition2 = false;

                    }
                }

            }
     
            if (player.HasBuff<HeavenlyBuff>())
            {

                Condition1 = false;
            }
            else
            {
                Condition1 = true;
            }
            if (Condition2 == true  && Condition1 == true)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        public static int positive;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position.X = Main.MouseWorld.X;
            position.Y = player.position.Y;
            if(player.direction == 1)
            {
                positive = 1;
            } else
            {
                positive = -1;

            }


            position.X = player.position.X - (600 * player.direction);
                position.Y = player.position.Y - 40;

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                //Projectile.NewProjectile(Main.MouseWorld.X, player.position.Y - 800, 0f, 0f, ProjectileID.Bomb, damage, 4, player.whoAmI);
            

            return false;

        }

    }
}



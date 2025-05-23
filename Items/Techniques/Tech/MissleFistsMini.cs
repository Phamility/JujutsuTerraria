﻿using System;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Ancients;
using JujutsuTerraria.Tiles;
using Terraria.Utilities;

namespace JujutsuTerraria.Items.Techniques.Tech
{
    public class MissleFistsMini : ModItem

    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Missle Fists");
            // Tooltip.SetDefault("Launch a barrage of giant fists!");
        }
  
        public override void SetDefaults()
        {

            Item.damage = 6;
            Item.width = 38;
            Item.height = 30;
            Cost = 1;

            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.noUseGraphic = true;

            Item.useStyle = ItemUseStyleID.HoldUp; // How you use the item (swinging, holding out, etc.)
            Item.knockBack = 3;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.noMelee = true;
               Item.shootSpeed = 4f;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<MissleFistsMiniProj>();

        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {

            crit = player.GetModPlayer<MP>().ZoneChance;
        }
        public static int positive;
        public override void UpdateInventory(Player player)
        {
            Cost = 1;

            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
            {
                Reduction = 1;
            }
            else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
            {

                Reduction = 1;
            }
            else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
            {

                Reduction = 1;
            }
            else
            {
                Reduction = 0;
            }


        }
        public override void AddRecipes()
        {
            CreateRecipe()

                .AddIngredient<CursedEnergy>(15)
                                .AddIngredient(ItemID.DirtBlock, 15)

                .AddTile<ShrineTile>()
                .Register();

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

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
            tooltips.Insert(1, tooltip);

            TooltipLine COCK = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
            tooltips.Remove(COCK);
            Player player = Main.LocalPlayer;
            TooltipLine NailCock;


            NailCock = new TooltipLine(Mod, "Ten Shadows: Cost", $"Launch a barrage of giant fists!") { OverrideColor = Color.White };


            TooltipLine BLACKFLASHCHANCE = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MP>().ZoneChance}% black flash chance") { OverrideColor = Color.White };
            if (Item.favorited == true)
            {

                tooltips.Insert(5, BLACKFLASHCHANCE);
                // tooltips.RemoveAt(5);
                // tooltips.Insert(5, NailCock);

            }
            else
            {
                tooltips.Insert(3, BLACKFLASHCHANCE);
                // tooltips.RemoveAt(3);
                //  tooltips.Insert(4, NailCock);


            }

        }
        int shotcount = 0;

        public override bool? UseItem(Player player)
        {
            bool once = false;
            for (int i = 0; i < Main.InventorySlotsTotal; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>() && once == false)
                {
                    if (shotcount >= 2)
                    {
                        shotcount = 0;
                        if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
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
                    else
                    {
                        shotcount++;
                    }

                }

            }
            return true;

        }
        public override bool CanUseItem(Player player)
        {
            bool Condition1;
            bool Condition2 = false;
            Cost = 1;
            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
            {
                Reduction = Cost - 1;
            }
            else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
            {

                Reduction = 1;
            }
            else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
            {

                Reduction = 1;
            }
            else
            {
                Reduction = 0;
            }
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
                    if (player.inventory[i].stack >= Cost - Reduction && Condition1 == true)
                    {
                        InventoryNumber = i;
                        Condition2 = true;
                    }
                    else
                    {
                        Condition2 = false;
                    }
                }

            }

            if (Condition1 == true && Condition2 == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            var prefixchooser = new WeightedRandom<int>();
            prefixchooser.Add(PrefixID.Broken, 2);
            prefixchooser.Add(PrefixID.Damaged, 2);
            prefixchooser.Add(PrefixID.Slow, 2);
            prefixchooser.Add(PrefixID.Annoying, 2);
            prefixchooser.Add(PrefixID.Quick, 2);
            prefixchooser.Add(PrefixID.Deadly, 2);
            prefixchooser.Add(PrefixID.Demonic, 2);
            prefixchooser.Add(PrefixID.Godly, 2);
            prefixchooser.Add(PrefixID.Ruthless, 2);
            prefixchooser.Add(PrefixID.Unpleasant, 2);
            prefixchooser.Add(PrefixID.Hurtful, 2);

            prefixchooser.Add(PrefixID.Rapid, 2);
            prefixchooser.Add(PrefixID.Unreal, 2);
            int choice = prefixchooser;
            if (Item.damage > 0 && Item.maxStack == 1)
            {
                return choice;
            }
            return -1;
        }
        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.direction == 1)
            {
                positive = 1;
            }
            else
            {
                positive = -1;

            }
            position.X = Main.MouseWorld.X;
            position.Y = player.position.Y - 500;

            int numberProjectiles = 1;
            for (int i = 0; i < numberProjectiles; i++)
            {
                position.X = player.position.X + Main.rand.Next(-75, 75);
                position.Y = player.position.Y + Main.rand.Next(-75, 75);

                //Projectile.NewProjectile(Main.MouseWorld.X, player.position.Y - 800, 0f, 0f, ProjectileID.Bomb, damage, 4, player.whoAmI);
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.

                Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<MissleFistsMiniProj>(), damage, 1, player.whoAmI);
            }

            return false;

        }

    }
}



using System;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.ID;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;

namespace JujutsuTerraria.Items.Techniques.Tech
{
    public class Construction : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Construction");
            // Tooltip.SetDefault("5 second cooldown\nConjure 1 lead bar and 25 wood");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(100)
.AddRecipeGroup("IronBar", 12)
                .AddIngredient(ItemID.Wood, 150)

            .AddTile<ShrineTile>()
                .Register();

        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(3 * player.direction, 3);
        }
    
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.Item4; // What sound should play when using the item
          //  Item.healLife = 25; // While we change the actual healing value in GetHealLife, Item.healLife still needs to be higher than 0 for the item to be considered a healing item
          //  Item.potion = false; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
            //   Item.damage = 9;
            Item.width = 32;
            Item.height = 32;
            Cost = 25;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.HoldUp; // How you use the item (swinging, holding out, etc.)
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
                                                  //  Item.knockBack = 3;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
            Item.noMelee = true;
            //      item.shootSpeed = 4f;
            Item.autoReuse = true;
           // Item.shoot = NPCID.Bunny;

        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the vanilla damage tooltip

            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"Costs {Cost - Reduction} cursed energy") { OverrideColor = Color.DodgerBlue };

            tooltips.Insert(1, tooltip);
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<ConDebuff>(), 60 * 5);
            Vector2 position = player.position + new Vector2(0, -13);

            Item.NewItem(Main.LocalPlayer.GetSource_FromThis(), position, ItemID.LeadBar, 1);
            Item.NewItem(Main.LocalPlayer.GetSource_FromThis(), position, ItemID.Wood, 25);

            //player.QuickSpawnItem(Main.LocalPlayer.GetSource_FromThis(), ItemID.LeadBar,1);

            bool once = false;
            for (int i = 0; i < Main.InventorySlotsTotal; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>() && once == false)
                {
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
            }
            return true;

        }
        public override void UpdateInventory(Player player)
        {
            Cost = 25;

            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
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
        public static int positive;



        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;
        public override bool CanUseItem(Player player)
        { 
            bool Condition1;
            bool Condition2 = false;
            Cost = 25;
            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
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
            if (player.HasBuff<ConDebuff>() || player.HasBuff<HeavenlyBuff>())
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


    }
}





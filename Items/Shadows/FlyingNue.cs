using System; using TenShadows.Buffs;

using Microsoft.Xna.Framework;
using TenShadows.Items.Materials;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using TenShadows.Tiles;
using Terraria.ModLoader;
using System.Transactions;

using System.Collections.Generic;
using System.Linq;

namespace TenShadows.Items.Shadows
{
    public class FlyingNue : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bird Silhouette");
            Tooltip.SetDefault("Slightly increases movement speed and flight time\nFlight effects are amplified for Nue-type Wings!\n1 minute duration");
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1; // The default value is 1, but other values are supported. See the docs for more guidance. 
        }
 
  

        public override void SetDefaults()
        {
            //  Item.damage = 8;
            //    Item.knockBack = 2f;
            Item.shoot = ItemID.None;
            Cost = 5;

            Item.width = 24;
            Item.height = 32;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.HoldUp; // how the player's arm moves when using the item
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item80; // What sound should play when using the item
            Item.autoReuse = true;
            // These below are needed for a minion weapon
            Item.noMelee = true; // this item doesn't do any melee damage
                                 // Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
           // Item.buffTime = 60 * 60;
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            //   Item.shoot = ModContent.ProjectileType<SummonedNue>(); // This item creates the minion projectile
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(6 * player.direction, 0);
        }




        public override void UpdateInventory(Player player)
        {
            Cost = 5;

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

            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"Costs {Cost - Reduction} cursed energy") { OverrideColor = Color.DodgerBlue };

            tooltips.Insert(1, tooltip);
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
                        player.AddBuff(ModContent.BuffType<FlightBuff>(), 60 * 60);


                    }
                    else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= Cost - Reduction;
                        once = true;
                        player.AddBuff(ModContent.BuffType<FlightBuff>(), 60 * 60);


                    }
                    else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= Cost - Reduction;
                        once = true;
                        player.AddBuff(ModContent.BuffType<FlightBuff>(), 60 * 60);


                    }
                    else
                    {
                        player.inventory[InventoryNumber].stack -= Cost - Reduction;
                        once = true;
                        player.AddBuff(ModContent.BuffType<FlightBuff>(), 60 * 60);


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
            Cost = 5;
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

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(100)

                .AddIngredient<NueFeather>(10)
                .AddTile<ShrineTile>()
                .Register();
        }
    }
}

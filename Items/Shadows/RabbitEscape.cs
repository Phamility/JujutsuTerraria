using System; using JujutsuTerraria.Buffs;
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
using JujutsuTerraria.Tiles;


namespace JujutsuTerraria.Items.Shadows
{
    public class RabbitEscape : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Rabbit Silhouette");
            // Tooltip.SetDefault("Conjure a swarm of bunnies!");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(100)
                .AddIngredient(ItemID.Bunny, 1)

                .AddTile<ShrineTile>()
                .Register();
        }
        public override void SetDefaults()
        {

            //   Item.damage = 9;
            Item.width = 32;
            Item.height = 28;
            Cost = 15 ;

            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.HoldUp; // How you use the item (swinging, holding out, etc.)
            Item.UseSound = SoundID.Grass; // What sound should play when using the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
                                                  //  Item.knockBack = 3;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
                                           //  Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            //      item.shootSpeed = 4f;
            Item.autoReuse = true;
            Item.shoot = NPCID.Bunny;

        }
        public int positive;
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(2 * player.direction, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            type = NPCID.Bunny;
            position.X = player.position.X;
            position.Y = player.position.Y - 300;

            int numberProjectiles = Main.rand.Next(20,25);
            for (int i = 0; i < numberProjectiles; i++)
            {
                position.X = player.position.X + (Main.rand.Next(-350, 350));
                position.Y = player.position.Y - 100 + (Main.rand.Next(-80, 80));
                if (Main.rand.Next(1, 50) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.ExplosiveBunny);
                }
                else if (Main.rand.Next(1, 200) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GemBunnyEmerald);
                }
                else if (Main.rand.Next(1, 200) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GemBunnyAmethyst);
                }
                else if (Main.rand.Next(1, 200) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GemBunnySapphire);
                }
                else if (Main.rand.Next(1, 200) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GemBunnyTopaz);
                }
                else if (Main.rand.Next(1, 200) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GemBunnyRuby);
                }
                else if (Main.rand.Next(1, 200) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GemBunnyAmber);
                }
                else if (Main.rand.Next(1, 200) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GemBunnyDiamond);
                }
                else if (Main.rand.Next(1, 500) == 2)
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, NPCID.GoldBunny);
                }
                else
                {
                    NPC.NewNPC(source, (int)position.X, (int)position.Y, type);
                }
            }
                return false;

            }


           public override void UpdateInventory(Player player)
        {
            Cost = 15;

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
            Cost = 15;
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


    }
}





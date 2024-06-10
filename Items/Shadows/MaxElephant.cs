using System; using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Audio;

using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;
using Terraria.Localization;

using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using System.Threading;
using System.Reflection;
using JujutsuTerraria.Ancients;


namespace JujutsuTerraria.Items.Shadows
{
    public class MaxElephant : ModItem

    {

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Elephant Silhouette");
            // Tooltip.SetDefault("Summons a sentry\nSummons a max elephant to shoot water at your enemies!\nOnly one max elephant can be summoned at a time");
        }
    
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(400)
                                .AddIngredient(ItemID.LihzahrdBrick, 40)

                .AddTile<ShrineTile>()
                .Register();
        }
        public override void SetDefaults()
        {
            Cost = 50;
            Item.width = 38;
            Item.height = 30;
            Item.damage = 300;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp; 
          //  Item.knockBack = 2;
            Item.rare = ItemRarityID.Yellow;
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.sentry = true;
            Item.value = Item.sellPrice(gold: 7); // How many coins the item is worth

            Item.crit = 0;
            Item.UseSound = SoundID.Zombie42;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ElephantSummon>();

        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            damage *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit += ExampleDamagePlayer.ModPlayer(player).exampleCrit;
        }
        public override void UpdateInventory(Player player)
        {
            Cost = 50;

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
        }
        public override bool? UseItem(Player player)
        {

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

        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;

        public override bool CanUseItem(Player player)
        {
            bool Condition2 = false;
            Cost = 50;
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
 
            if (Condition2 == true  && Condition1 == true)
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(4 * player.direction, 0);
        }

        public static int positive;
        public static int DeleteAMF;
        private int timer;
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

            DeleteAMF = 0;
             Vector2 position2 = player.position + new Vector2(0, -130);
      velocity = new Vector2(0, 7.5f);
            player.AddBuff(ModContent.BuffType<ElephantBuff>(), 2);
            Projectile.NewProjectileDirect(source, position2, velocity, type, damage, knockback, player.whoAmI);
            player.UpdateMaxTurrets();



            return false;

        }

    }
}



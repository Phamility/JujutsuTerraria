using System; using JujutsuTerraria.Buffs;
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
using JujutsuTerraria.Ancients;
using static Humanizer.In;

namespace JujutsuTerraria.Items.Techniques.Domains
{
    public class DEInfinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unlimited Void");
            // Tooltip.SetDefault("Conjure the power of the Void.");
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(3 * player.direction, 3);
        }
    
        public override void SetDefaults()
        {
         //  Item.CloneDefaults(ItemID.MagicMissile);

            // Item.UseSound = SoundID.Item4; // What sound should play when using the item
            //  Item.healLife = 25; // While we change the actual healing value in GetHealLife, Item.healLife still needs to be higher than 0 for the item to be considered a healing item
            //  Item.potion = false; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
            //   Item.damage = 9;
            Item.UseSound = SoundID.DD2_EtherianPortalOpen;
            Item.width = 36;
            Item.height = 36;
            Item.useAmmo = AmmoID.None;
            Item.mana = 0;
            Item.damage = 450;
            Cost = 999;
            Item.useTime = 60;
            Item.useAnimation = 60;
          Item.useStyle = ItemUseStyleID.HiddenAnimation; // How you use the item (swinging, holding out, etc.)
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.rare = ItemRarityID.Red; // The color that the item's name will be in-game.
            Item.noMelee = true;
            Item.knockBack = 0;
            Item.noUseGraphic = true;

            Item.shootSpeed = 1;
            Item.shoot = ModContent.ProjectileType<DomainInfinity>();
            Item.channel = true;
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {

            crit =0;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            damage *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
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
            TooltipLine COCK = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
            tooltips.Remove(COCK);
            Player player = Main.LocalPlayer;
            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"Costs {Cost - Reduction} cursed energy") { OverrideColor = Color.DodgerBlue };

            tooltips.Insert(1, tooltip);
         if(player.GetModPlayer<MPArmors>().GojoHeadOn == false)
            {
                TooltipLine tooltip2 = new TooltipLine(Mod, "Ten Shadows: Cost", $"Unable to use") { OverrideColor = Color.Red };

                tooltips.Insert(1, tooltip2);
            }

            


        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {


            position.X = player.position.X;
            position.Y = player.position.Y;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
            //Projectile.NewProjectile(Main.MouseWorld.X, player.position.Y - 800, 0f, 0f, ProjectileID.Bomb, damage, 4, player.whoAmI);


            return false;

        }
        public override bool? UseItem(Player player)
        {
         

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
            Cost = 999;

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



        public static int InventoryNumber;
        public static int Cost;
        public static int Reduction = 0;
        public override bool CanUseItem(Player player)
        { 
            bool Condition1;
            bool Condition2 = false;
            Cost = 999;
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
                        Condition2 = true;
                    }
                    else
                    {
                        Condition2 = false;
                    }
                }

            }

            if (Condition1 == true && (Condition2 == true) && player.GetModPlayer<MPArmors>().GojoHeadOn == true && player.ownedProjectileCounts[ModContent.ProjectileType<DomainInfinity>()] == 0
                                && player.ownedProjectileCounts[ModContent.ProjectileType<DomainJungle>()] == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<DomainFire>()] == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<DomainOcean>()] == 0)
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





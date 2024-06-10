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
using static Terraria.ModLoader.PlayerDrawLayer;
using JujutsuTerraria.Ancients;
using Terraria.Utilities;


namespace JujutsuTerraria.Items.Shadows
{
    public class Gama : ModItem
    {
 
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toad Silhouette");
            // Tooltip.SetDefault("Surround yourself with bubbles that inflict ichor and cursed inferno\nInflicted ichor and cursed inferno lasts 12 seconds");
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(300)
                .AddIngredient(ItemID.Ichor, 20)

                .AddTile<ShrineTile>()
                .Register();
           CreateRecipe()
        .AddIngredient<CursedEnergy>(300)
        .AddIngredient(ItemID.CursedFlame, 20)

        .AddTile<ShrineTile>()
        .Register();
        }
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(4 * player.direction, 3);
        }
  
        public override void SetDefaults()
        {
            Item.UseSound = SoundID.NPCHit20; // What sound should play when using the item
            // all vanilla damage types must be false for custom damage types to work
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Cost = 8;

            //   Item.damage = 9;
            Item.width = 30;
            Item.height = 30;
            Item.crit = 10;
            Item.damage = 40;
            Item.useTime = 35;
            Item.useAnimation = 35;
          //  Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.HoldUp; // How you use the item (swinging, holding out, etc.)
           // Item.buffType = ModContent.BuffType<SerpentBuff>();
            //  Item.knockBack = 3;
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
            Item.value = Item.sellPrice(gold: 3); // How many coins the item is worth
                                           //  Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;
            //      item.shootSpeed = 4f;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CursedBubble>();


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
            if ((Item.damage > 0) && Item.maxStack == 1)
            {
                return choice;
            }
            return -1;
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
            Cost = 8;

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
         //   TooltipLine cock = tooltips.Find(x => x.Name == "CritChance" && x.Mod == "Terraria");

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
         //   tooltips.Remove(cock);

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
            Cost = 8;
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

    
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {


            int numberProjectiles = 8;
            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.Next(1, 3) == 2)
                {
                    type = ModContent.ProjectileType<IchorBubble    >();
                }
                else
                {
                    type = ModContent.ProjectileType<CursedBubble>();
                }
                position.X = player.position.X + Main.rand.Next(-200, 200);
                position.Y = player.position.Y + Main.rand.Next(-200, 200);

                Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
                //Projectile.NewProjectile(Main.MouseWorld.X, player.position.Y - 800, 0f, 0f, ProjectileID.Bomb, damage, 4, player.whoAmI);

            }
            return false;

        }


    }
    }





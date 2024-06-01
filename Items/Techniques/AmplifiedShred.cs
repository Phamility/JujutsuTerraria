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
using JujutsuTerraria.Ancients;
using Terraria.Utilities;
using JujutsuTerraria.Tiles;

namespace JujutsuTerraria.Items.Techniques
{
    public class AmplifiedShred : ModItem

    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Amplified Shred");
            // Tooltip.SetDefault("Blast waves of amplified cursed energy");
        }
   
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SnowmanCannon);
            Item.useAmmo = AmmoID.None;
            Item.damage = 42;
            Item.width = 44;
           // Item.mana = 8;
            Item.height = 36;
           // Item.healLife = -4;
            Item.useTime = 6;
            Item.useAnimation = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.UseSound = SoundID.Item47;
            Item.value = Item.sellPrice(gold: 5); // How many coins the item is worth
            Cost = 5;
            Item.noMelee = true;
                  Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<Shred>();
      //     Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;
          //  Item.shoot = ModContent.ProjectileType<NueFriendlyFeather>();

        }
        public static int positive;

      
  
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


        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;
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
    
         
                 NailCock = new TooltipLine(Mod, "Ten Shadows: Cost", $"Blast waves of amplified cursed energy") { OverrideColor = Color.White };

            
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
        int shotcount=0;

        public override bool? UseItem(Player player)
        {
            bool once = false;
            for (int i = 0; i < Main.InventorySlotsTotal; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>() && once == false)
                {
                    if (shotcount >= 3)
                    {
                        shotcount = 0;
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
                    } else
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
            if ( player.HasBuff<HeavenlyBuff>())
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

            if (Condition1 == true && (Condition2 == true))
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
      
            if (player.direction == 1)
            {
                positive = 1;
            }
            else
            {
                positive = -1;

            }

            int numberProjectiles = Main.rand.Next(1, 1);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(10)); // 30 degree spread.

                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X * 3, perturbedSpeed.Y * 3, ModContent.ProjectileType<Shred>(), ((damage)), 1, player.whoAmI);
                
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        // HoldoutOffset has to return a Vector2 because it needs two values (an X and Y value) to move your flamethrower sprite. Think of it as moving a point on a cartesian plane.
        {
            return new Vector2(-8, -2); // If your own flamethrower is being held wrong, edit these values. You can test out holdout offsets using Modder's Toolkit.
        }

    }
}



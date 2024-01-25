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

namespace JujutsuTerraria.Items.Techniques
{
    public class CursedSpeech : ModItem

    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Speech");
            Tooltip.SetDefault("Unleash a shocking wave that immobilizes enemies for 2.5 seconds\n30 second cooldown");
        }
   
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SnowmanCannon);
            Item.useAmmo = AmmoID.None;
            Item.damage = 20;
            Item.width = 32;
            // Item.mana = 8;
            Cost = 40;
            Item.height = 32;
           // Item.healLife = -4;
            Item.useTime = 20;
            Item.useAnimation = 20;
           Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.knockBack = 4;
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.UseSound = SoundID.Zombie82;
            Item.value = Item.sellPrice(gold: 3); // How many coins the item is worth

            //  Item.noMelee = true;
            Item.shootSpeed = 4f;
      //     Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;
           Item.shoot = ModContent.ProjectileType<SpeechProj>();

        }


      
  
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            damage *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit= 0;

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
            Player player = Main.LocalPlayer;

            tooltips.Insert(1, tooltip);
            TooltipLine COCK = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
            tooltips.Remove(COCK);
            TooltipLine NailCock;
            if (player.GetModPlayer<MPArmors>().InumakiHeadOn == true)
            {
                NailCock = new TooltipLine(Mod, "Ten Shadows: Cost", $"5 second cooldown") { OverrideColor = Color.White };

            }
            else
            {
                NailCock = new TooltipLine(Mod, "Ten Shadows: Cost", $"30 second cooldown") { OverrideColor = Color.White };

            }
            if (Item.favorited == true)
            {

                tooltips.RemoveAt(6);
                tooltips.Insert(6, NailCock);

            }
            else
            {
                tooltips.RemoveAt(4);
                tooltips.Insert(4, NailCock);


            }

        }

        public override bool? UseItem(Player player)
        {
            if (player.GetModPlayer<MPArmors>().InumakiHeadOn == true)
            {
                player.AddBuff(ModContent.BuffType<CursedDebuff>(), 5 * 60);

            }
            else
            {
                player.AddBuff(ModContent.BuffType<CursedDebuff>(), 60 * 30);
            }
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
        public override void UpdateInventory(Player player)
        {
            Cost = 40;

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
        public static int positive;



        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;
        public override bool CanUseItem(Player player)
        {
            bool Condition1;
            bool Condition2 = false;
            Cost = 40;
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
            if (player.HasBuff<CursedDebuff>() || player.HasBuff<HeavenlyBuff>())
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {


            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(velocity.X, velocity.Y)) * 50; //This gets the direction of the flame projectile, makes its length to 1 by normalizing it. It then multiplies it by 54 (the item width) to get the position of the tip of the flamethrower.
            position += muzzleOffset;
            Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<SpeechProj>(), (damage), 1, player.whoAmI);
                
            
            return false;
        }


    }
}



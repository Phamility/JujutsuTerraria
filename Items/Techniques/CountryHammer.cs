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
    public class CountryHammer : ModItem

    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Country Girl's Hammer");
            Tooltip.SetDefault("Fires 1-3 nails per swing");
        }
   
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SnowmanCannon);
            Item.useAmmo = AmmoID.None;
            Item.damage = 9;
            Item.width = 36;
           // Item.mana = 8;
            Item.height = 32;
           // Item.healLife = -4;
            Item.useTime = 24;
            Item.useAnimation = 24;
           Item.useStyle = ItemUseStyleID.Swing; // How you use the item (swinging, holding out, etc.)
            Item.knockBack = 4;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth

            Item.noMelee = true;
                  Item.shootSpeed = 4f;
          Item.shoot = ProjectileID.NailFriendly;
      //     Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;
          //  Item.shoot = ModContent.ProjectileType<NueFriendlyFeather>();

        }
        public static int positive;


      
  
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            damage *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
            if (player.GetModPlayer<MPArmors>().NobaraHeadOn == true)
            {
                damage *= 1.45f;
            }
        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {

            crit = player.GetModPlayer<MP>().ZoneChance;
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
            TooltipLine NailCock;
            if (player.GetModPlayer<MPArmors>().NobaraHeadOn)
            {
             NailCock = new TooltipLine(Mod, "Ten Shadows: Cost", $"Fires 1-4 nails per swing") { OverrideColor = Color.White };

            }
            else
            {
                 NailCock = new TooltipLine(Mod, "Ten Shadows: Cost", $"Fires 1-3 nails per swing") { OverrideColor = Color.White };

            }
            TooltipLine BLACKFLASHCHANCE = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MP>().ZoneChance}% black flash chance") { OverrideColor = Color.White };
            if (Item.favorited == true)
            {

                tooltips.Insert(4, BLACKFLASHCHANCE);
                tooltips.RemoveAt(5);
                tooltips.Insert(5, NailCock);

            }
            else
            {
                tooltips.Insert(2, BLACKFLASHCHANCE);
                tooltips.RemoveAt(3);
                tooltips.Insert(3, NailCock);


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
            int OneMore;
            if ((player.GetModPlayer<MPArmors>().NobaraHeadOn)){
                OneMore = 1;
            }
            else
            {
                OneMore = 0;
            }
            if (player.direction == 1)
            {
                positive = 1;
            }
            else
            {
                positive = -1;

            }

            int numberProjectiles = Main.rand.Next(1, 4 + OneMore);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(30)); // 30 degree spread.

                    Projectile.NewProjectile(source, position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<Nail>(), ((damage)), 1, player.whoAmI);
                
            }
            return false;
        }


    }
}



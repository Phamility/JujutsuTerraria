using System; using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;
using JujutsuTerraria.Items.Techniques;

namespace JujutsuTerraria.Items.Accessories
{
    public class PandaBrass : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs
 
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("A Panda's Brass Knuckles");
            Tooltip.SetDefault("Increases black flash damage");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            //Item.expert = true;

        }
        private int timer;
        private int damage;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MP>().PandaBrassWorn = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(100)
                .AddIngredient(ItemID.DemoniteBar, 10)

                .AddTile<ShrineTile>()
                .Register();
            CreateRecipe()
         .AddIngredient<CursedEnergy>(100)
         .AddIngredient(ItemID.CrimtaneBar, 10)

         .AddTile<ShrineTile>()
         .Register();
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
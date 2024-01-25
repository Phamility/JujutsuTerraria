using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JujutsuTerraria.Items.Shadows
{
    public class General : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("General Wheel Silhouette");
            Tooltip.SetDefault("Players incredibly close by gain 6% damage reduction and 6 armor piercing");
        }
        public override void SetDefaults()
        {

            Item.DefaultToPlaceableTile(ModContent.TileType<GeneralTile>());
            Item.width = 44; // The item texture's width
            Item.height = 50; // The item texture's height
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.LightPurple; // The color that the item's name will be in-game.
            Item.maxStack = 99;

        }


        public override void AddRecipes()
        {
            CreateRecipe()
                                .AddIngredient<CursedEnergy>(350)

              
                .AddIngredient(ItemID.SoulofMight, 15)
                .AddIngredient(ItemID.SoulofSight, 15)
                .AddIngredient(ItemID.SoulofFright, 15)



        .AddTile<ShrineTile>()

                .Register();
        }
    }
}

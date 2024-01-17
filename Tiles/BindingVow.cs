using JujutsuTerraria.Buffs;
using JujutsuTerraria.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace JujutsuTerraria.Tiles
{
    public class BindingVow : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Binding Barrier");
            Tooltip.SetDefault("Players within this tile's range have their damage increased by 15%\nPlayers outside of this tile's range have their damage reduced by 15%\nOnly one domain may be placed in the world");
        }
        public override void SetDefaults()
        {

            Item.DefaultToPlaceableTile(ModContent.TileType<BindingVowTile>());
            Item.width = 26; // The item texture's width
            Item.height = 38; // The item texture's height
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.
            Item.maxStack = 99;

        }
        public override bool CanUseItem(Player player)
        {
            if (player.HasBuff<BindingVowDebuff>() || player.HasBuff<BindingVowBuff>())
            {
                return false;
            }
            else
            {
                return true;
            }
                        }


        public override void AddRecipes()
        {
            CreateRecipe()
                                .AddIngredient<CursedEnergy>(100)


                .AddIngredient(ItemID.LifeCrystal, 1)
                .AddIngredient(ItemID.Chain, 10)



        .AddTile<ShrineTile>()

                .Register();
        }
    }
}

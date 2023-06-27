using TenShadows.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TenShadows.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Legs)]
    public class NueArmorLegs : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Feathery Leggings");
            Tooltip.SetDefault("Slightly increases movement speed");
        }

     
        public override void SetDefaults()
        {
            Item.width = 22; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 2; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 10 / 100f; // Increase the movement speed of the player
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<NueFeather>(8)
        .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}

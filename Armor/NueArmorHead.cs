using Mono.Cecil;
using TenShadows.Buffs;
using TenShadows.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;

namespace TenShadows.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class NueArmorHead : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Feathery Helmet");
            Tooltip.SetDefault("Slightly increases critical chance");
        }


        public override void SetDefaults()
        {
            Item.width = 22; // Width of the item
            Item.height = 22; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 3; // The amount of defense the item will give when equipped
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<NueArmorBody>() && legs.type == ModContent.ItemType<NueArmorLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Summons Nue to fight along side you!"; // This is the setbonus tooltip
            player.AddBuff(ModContent.BuffType<SummonNueBuff>(), 2);

        }
        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Generic) += 3f;
     
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<NueFeather>(10)
        .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}

using Mono.Cecil;
using JujutsuTerraria.Buffs;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;
using JujutsuTerraria.Ancients;
namespace JujutsuTerraria.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class MegumiHead : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Fushiguro Megumi");
            Tooltip.SetDefault("1% increased cursed damage");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;

        }


        public override void SetDefaults()
        {
            Item.width = 28; // Width of the item
            Item.height = 26; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 1; // The amount of defense the item will give when equipped
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UniformBody>() && legs.type == ModContent.ItemType<UniformLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Summons Nue to fight along side you!"; // This is the setbonus tooltip
            player.AddBuff(ModContent.BuffType<SummonNueBuff>(), 2);

        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage<CursedDamage>() += .01f;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(60)
                .AddIngredient<NueFeather>(6)

                .AddTile<ShrineTile>()
                .Register();
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}

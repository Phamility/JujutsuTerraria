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
    public class MeiMeiHead : ModItem
    {
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Mei Mei");
            // Tooltip.SetDefault("15% increased cursed damage");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;

        }


        public override void SetDefaults()
        {
            Item.width = 24; // Width of the item
            Item.height = 30; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Pink; // The rarity of the item
            Item.defense = 11; // The amount of defense the item will give when equipped
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UniformBody>() && legs.type == ModContent.ItemType<UniformLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enhances 'Black Bird Manipulation'"; // This is the setbonus tooltip
            player.GetModPlayer<MPArmors>().MeiMeiHeadOn = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage<CursedDamage>() += .15f;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(400)
                              .AddIngredient(ItemID.BeetleHusk, 12)

                .AddTile<ShrineTile>()
                .Register();

        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}

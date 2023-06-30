﻿using Mono.Cecil;
using TenShadows.Buffs;
using TenShadows.Items.Materials;
using TenShadows.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;
using TenShadows.Ancients;
namespace TenShadows.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class NobaraHead : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Kugisaki Nobara");
            Tooltip.SetDefault("3% increased cursed damage");
        }


        public override void SetDefaults()
        {
            Item.width = 26; // Width of the item
            Item.height = 26; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = 3; // The amount of defense the item will give when equipped
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UniformBody>() && legs.type == ModContent.ItemType<UniformLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enhances 'Country Girl's Hammer'"; // This is the setbonus tooltip
            player.GetModPlayer<MPArmors>().NobaraHeadOn = true;

        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage<CursedDamage>() += (3 / 100);

        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}

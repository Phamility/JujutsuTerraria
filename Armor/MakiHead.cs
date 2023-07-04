using Mono.Cecil;
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
    public class MakiHead : ModItem
    {
        public override void SetStaticDefaults()
        {

            DisplayName.SetDefault("Zenin Maki");
            Tooltip.SetDefault("5% increased cursed damage");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
          // Don't draw the head at all. Used by Space Creature Mask
            // ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true; // Draw hair as if a hat was covering the top. Used by Wizards Hat
            // ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true; // Draw all hair as normal. Used by Mime Mask, Sunglasses
            // ArmorIDs.Head.Sets.DrawsBackHairWithoutHeadgear[Item.headSlot] = true; 
        }


        public override void SetDefaults()
        {
            Item.width = 28; // Width of the item
            Item.height = 26; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Green; // The rarity of the item
            Item.defense = 4; // The amount of defense the item will give when equipped
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UniformBody>() && legs.type == ModContent.ItemType<UniformLegs>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Enhances 'Heavenly Restriction'"; // This is the setbonus tooltip
   
            player.GetModPlayer<MPArmors>().MakiHeadOn = true;


        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage<CursedDamage>() += (5 / 100);

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(200)
                              .AddIngredient(ItemID.Bone, 25)

                .AddTile<ShrineTile>()
                .Register();
        }
            // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

        }
}

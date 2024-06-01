using System;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Audio;

using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;
using Terraria.Localization;
using JujutsuTerraria.Ancients;
using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using System.Threading;
using System.Reflection;
using System.IO;

using Terraria.Utilities;




namespace JujutsuTerraria.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Legs)]
    public class UniformLegs : ModItem
    {
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Jujutsu Uniform - Legs");
            // Tooltip.SetDefault("Stats are increased as more bosses are defeated");
        }

     
        public override void SetDefaults()
        {
            Item.width = 22; // Width of the item
            Item.height = 18; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = UniformLegsDefense; // The amount of defense the item will give when equipped
        }
        public static int UniformLegsDefense;

        public static float CursedStatIncrease;
        public static bool BlackFlashBody;


        public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
            int AddMore = 0;
            if (Item.favorited) { AddMore = 2; } else
            {
                AddMore = 0;
            }
            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"{CursedStatIncrease}% increased cursed damage") { OverrideColor = Color.White };
        tooltips.Insert(3 + AddMore, tooltip);
            if (BlackFlashBody == true)
            {
                TooltipLine Cock = new TooltipLine(Mod, "Ten Shadows: Cost", $"1% increased black flash chance") { OverrideColor = Color.White };
                tooltips.Insert(4, Cock);
            }
        }
    public override void UpdateEquip(Player player)
        {


            if (CursedStatIncrease >= 10)
            {
                Item.rare = ItemRarityID.Yellow;
            }
            else if (CursedStatIncrease >= 8)
            {
                Item.rare = ItemRarityID.Pink;
            }
            else if (UniformLegsDefense >= 7)
            {
                Item.rare = ItemRarityID.LightRed;
            }
            else if (UniformLegsDefense >= 4)
            {
                Item.rare = ItemRarityID.Green;
            }
            else { Item.rare = ItemRarityID.Blue; }
            player.GetDamage<CursedDamage>() += (CursedStatIncrease / 100);
        player.GetModPlayer<MP>().ZoneBodyWorn = true;
        Item.defense = UniformLegsDefense;
    }

    // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
    public override void AddRecipes()
    {
        CreateRecipe()
          .AddIngredient(ItemID.Silk, 8)
            .AddIngredient<CursedEnergy>(80)

            .AddTile<ShrineTile>()
            .Register();
    }
}
}
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
    [AutoloadEquip(EquipType.Body)]
    public class UniformBody : ModItem
    {

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Jujutsu Uniform - Body");
            // Tooltip.SetDefault("Stats are increased as more bosses are defeated");
  
            EatShit();
    
        }
        public override void Load()
        {
            // The code below runs only if we're not loading on a server
            if (Main.netMode == NetmodeID.Server)
                return;

            // Add equip textures
            EquipLoader.AddEquipTexture(Mod, $"{Texture}_{EquipType.Body}", EquipType.Body, this);

        }
        private void EatShit()
        {
            if (Main.netMode == NetmodeID.Server)
                return;
            int equipSlotBody = EquipLoader.GetEquipSlot(Mod, Name, EquipType.Body);
            ArmorIDs.Body.Sets.HidesTopSkin[equipSlotBody] = true;
            ArmorIDs.Body.Sets.HidesArms[equipSlotBody] = true;
        }
        public static int UniformBodyDefense;
        public static float CursedStatIncrease;

        public override void SetDefaults()
        {
            Item.width = 30; // Width of the item
            Item.height = 20; // Height of the item
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue; // The rarity of the item
            Item.defense = UniformBodyDefense; // The amount of defense the item will give when equipped
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int AddMore = 0;
            if (Item.favorited) { AddMore = 2; } else { AddMore = 0; }
            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"{CursedStatIncrease}% increased cursed damage") { OverrideColor = Color.White };
            tooltips.Insert(3 + AddMore, tooltip);
            if (UniformBodyDefense >= 15)
            {
                TooltipLine Cock = new TooltipLine(Mod, "Ten Shadows: Cost", $"Increases max health by 40") { OverrideColor = Color.White };
                tooltips.Insert(4 + AddMore, Cock);
            }
            else if (UniformBodyDefense >= 6)
            {
                TooltipLine Cock = new TooltipLine(Mod, "Ten Shadows: Cost", $"Increases max health by 20") { OverrideColor = Color.White };
                tooltips.Insert(4 + AddMore, Cock);
            }
            if (UniformBodyDefense >= 10)
            {
                TooltipLine Cock = new TooltipLine(Mod, "Ten Shadows: Cost", $"Increases armor penetration by 3") { OverrideColor = Color.White };
                tooltips.Insert(5 + AddMore, Cock);
            }



        }
        public override void UpdateEquip(Player player)
        {
            if (UniformBodyDefense >= 10)
            {
                player.GetArmorPenetration(DamageClass.Generic) += 3;
            }
            if (UniformBodyDefense >= 15)
            {
                player.statLifeMax2 += 40;
            }
            else
            if (UniformBodyDefense >= 6)
            {
                player.statLifeMax2 += 20;
            }

            if (CursedStatIncrease >= 14)
            {
                Item.rare = ItemRarityID.Yellow;
            }
            else if (CursedStatIncrease >= 12)
            {
                Item.rare = ItemRarityID.Pink;
            }
            else if (CursedStatIncrease >= 10)
            {
                Item.rare = ItemRarityID.LightRed;
            }
            else if (CursedStatIncrease >= 5)
            {
                Item.rare = ItemRarityID.Green;
            }
            else { Item.rare = ItemRarityID.Blue; }
            player.GetDamage<CursedDamage>() += (CursedStatIncrease/100);
            Item.defense = UniformBodyDefense;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
              .AddIngredient(ItemID.Silk, 12)
                .AddIngredient<CursedEnergy>(120)

                .AddTile<ShrineTile>()
                .Register();
        }
    }
}
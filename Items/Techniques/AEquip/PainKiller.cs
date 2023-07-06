using System;
using JujutsuTerraria.Buffs;
using JujutsuTerraria.Buffs;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using JujutsuTerraria.Tiles;

using Terraria.ModLoader;
using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using rail;
using JujutsuTerraria.Ancients;

namespace JujutsuTerraria.Items.Techniques.AEquip
{
    public class PainKiller : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Pain Killer");
            Tooltip.SetDefault("While below 50 health, gain a significant increase in life regeneration");
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 26;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(100)
                .AddIngredient(ItemID.BlackLens, 1)

                .AddTile<ShrineTile>()
                .Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
                if(player.statLife <= 50)
            {
                player.lifeRegen += 20;
            }

        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
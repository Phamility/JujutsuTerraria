using System; using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Personalities;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;
using JujutsuTerraria.Ancients;
using Terraria.Localization;
using System.Diagnostics.Metrics;

namespace JujutsuTerraria.Items.Techniques.AEquip
{
    public class Limitless : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            // DisplayName.SetDefault("Nue's Lucky Feathers");
            // Tooltip.SetDefault("1 defense\nIncreases max health by 20\nIncreases max mana by 20 ");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.sellPrice(gold: 5); // How many coins the item is worth
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;
          //  Item.expert = true;

        }
        private int counter;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

            if (player.HasBuff(ModContent.BuffType<JJKBuff>()) == false)
            {
                counter++;

                if (player.HasBuff(ModContent.BuffType<LimitlessBuff>()))
                {
                    if (counter >= 240)
                    {
                        player.immune = true;
                    }
                    if (counter >= 360)
                    {
                        counter = 0;
                    }
                }
                else
                {

                    if (counter >= 600)
                    {
                        player.immune = true;
                    }
                    if (counter >= 720)
                    {
                        counter = 0;
                    }
                }

            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                                                                              .AddIngredient(ItemID.HallowedBar, 25)

                                                              .AddIngredient(ItemID.CrystalShard, 25)

                                .AddIngredient<CursedEnergy>(250)


        .AddTile<ShrineTile>()
                .Register();
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
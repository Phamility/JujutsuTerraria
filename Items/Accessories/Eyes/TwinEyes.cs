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
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using rail;
using JujutsuTerraria.Tiles;

namespace JujutsuTerraria.Items.Accessories.Eyes
{
    public class TwinEyes : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("The Twin Eyes");
            Tooltip.SetDefault("When equipped, reduce all cursed energy usages by 4");
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.value = Item.sellPrice(gold: 3); // How many coins the item is worth
            Item.rare = ItemRarityID.Pink;
            Item.accessory = true;

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                                .AddIngredient<CursedEnergy>(350)


                .AddIngredient(ItemID.SoulofSight, 15)




        .AddTile<ShrineTile>()

                .Register();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<TwinEyesBuff>(), 2);

        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
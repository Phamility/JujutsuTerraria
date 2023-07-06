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
    public class SixEyes : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs
 
        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("The Six Eyes");
            Tooltip.SetDefault("Throughout Heaven and Earth, I alone am the honored one!");
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 45;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ItemRarityID.Red;
            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<SixEyesBuff>(), 2);

     

        }
        public override void AddRecipes()
        {
            CreateRecipe()
                                .AddIngredient<CursedEnergy>(999)
                                .AddIngredient<NueEye>(1)
                                .AddIngredient<FishEye>(1)
                                .AddIngredient<EyeEye>(1)
                                .AddIngredient<TwinEyes>(1)
                                .AddIngredient<NebulaEye>(1)







        .AddTile<ShrineTile>()

                .Register();
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
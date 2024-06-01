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
using static Terraria.ModLoader.PlayerDrawLayer;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;

namespace JujutsuTerraria.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class NueWings : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs
 
        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            // DisplayName.SetDefault("Nue's Wings");
            // Tooltip.SetDefault("Allows flight and slow fall");
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(17, 2f, 1f);
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 26;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.35f; // Falling glide speed
            ascentWhenRising = 0.05f; // Rising speed
            maxCanAscendMultiplier = .5f;
            maxAscentMultiplier = 1f;
            constantAscend = 0.105f;
            if (player.HasBuff<FlightBuff>())
            {
                ascentWhenFalling = 0.50f; // Falling glide speed
                ascentWhenRising = 0.075f; // Rising speed
                maxCanAscendMultiplier = 1.25f;
                maxAscentMultiplier = 1.75f;
                constantAscend = 0.155f;
            }
            else
            {
                ascentWhenFalling = 0.35f; // Falling glide speed
                ascentWhenRising = 0.05f; // Rising speed
                maxCanAscendMultiplier = .5f;
                maxAscentMultiplier = 1f;
                constantAscend = 0.105f;

            }
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (player.HasBuff<FlightBuff>())
            {
                //acceleration += 1;
                speed += 1.05f;
            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<NueFeather>(15)
                .AddTile(TileID.WorkBenches)
                .SortBefore(Main.recipe.First(recipe => recipe.createItem.wingSlot != -1)) // Places this recipe before any wing so every wing stays together in the crafting menu.
                .Register();
        }
    }
}
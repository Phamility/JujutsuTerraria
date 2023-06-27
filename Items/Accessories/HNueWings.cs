using System; using TenShadows.Buffs;
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
using static Terraria.ModLoader.PlayerDrawLayer;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using TenShadows.Tiles;

namespace TenShadows.Items.Accessories
{
    [AutoloadEquip(EquipType.Wings)]
    public class HNueWings : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs
 
        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Honored Nue's Wings");
            Tooltip.SetDefault("Fly across both Heaven and Earth");
            ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(95, 7f, 2.2f);
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 30;
            Item.value = 1000000;
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f; // Falling glide speed
            ascentWhenRising = 0.09f; // Rising speed
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 1.75f;
            constantAscend = 0.135f;
            if (player.HasBuff<FlightBuff>())
            {
                ascentWhenFalling = 0.9f; // Falling glide speed
                ascentWhenRising = 0.15f; // Rising speed
                maxCanAscendMultiplier = 1.3f;
                maxAscentMultiplier = 2.75f;
                constantAscend = 0.165f;
            }
            else
            {
                ascentWhenFalling = 0.85f; // Falling glide speed
                ascentWhenRising = 0.09f; // Rising speed
                maxCanAscendMultiplier = 1f;
                maxAscentMultiplier = 1.75f;
                constantAscend = 0.135f;


            }
        }
        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            if (player.HasBuff<FlightBuff>())
            {
                //acceleration += 1;
                speed += 1.15f;
            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<NueWings>(1)
                                .AddIngredient<CursedEnergy>(300)
                                              .AddIngredient(ItemID.SoulofFlight, 15)

       .AddIngredient(ItemID.Ectoplasm, 10)

        .AddTile<ShrineTile>()
                .SortBefore(Main.recipe.First(recipe => recipe.createItem.wingSlot != -1)) // Places this recipe before any wing so every wing stays together in the crafting menu.
                .Register();
        }
    }
}
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
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using rail;
using TenShadows.Tiles;

namespace TenShadows.Items.Accessories.Eyes
{
    public class NebulaEye : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("The Nebulated Eye");
            Tooltip.SetDefault("When equipped, increase maximum mana by 60 and reduce mana usage by 15%");
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statManaMax2 += 60;
            player.manaCost -= .15f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                                .AddIngredient<CursedEnergy>(500)


                .AddIngredient(ItemID.FragmentNebula, 8)




        .AddTile<ShrineTile>()

                .Register();
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
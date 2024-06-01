using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using JujutsuTerraria.Items.Materials;

using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace JujutsuTerraria.Tiles
{
    public class Shrine : ModItem
    {
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Jujutsu Shrine");
            // Tooltip.SetDefault("Used for crafting items containing cursed energy");
        }
        public override void SetDefaults()
        {
       
            Item.DefaultToPlaceableTile(ModContent.TileType<ShrineTile>());
            Item.width = 34; // The item texture's width
            Item.height = 34; // The item texture's height
            Item.value = 0;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 10)

                .AddIngredient<CursedEnergy>(25)

                                .AddTile(TileID.DemonAltar)

                .Register();
        }
        public virtual void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;

            Vector2 tileCenter = new Point16(i, j).ToWorldCoordinates();
            const float range = 100 * 16;  // 20 tiles
            if (player.DistanceSQ(tileCenter) <= range * range)
                player.AddBuff(BuffID.Ichor, 2);

        }
    }
}
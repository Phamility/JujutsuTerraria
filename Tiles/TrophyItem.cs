using System; using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TenShadows.Tiles
{
    public class TrophyItem : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nue Trophy");
        }
        public override void SetDefaults()
        {

            // Vanilla has many useful methods like these, use them! This substitutes setting Item.createTile and Item.placeStyle aswell as setting a few values that are common across all placeable items
            Item.DefaultToPlaceableTile(ModContent.TileType<Trophy>());
            Item.maxStack = 99;
            Item.width = 34;
            Item.height = 32;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 1);
        }
    }
}


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
using static Terraria.ModLoader.PlayerDrawLayer;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Techniques.ARestrictions;
using JujutsuTerraria.Tiles;
using JujutsuTerraria.Items.Materials;

namespace JujutsuTerraria.Items.Techniques.ARestrictions
{
    public class StandardBV : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unbranded Binding Vow");
            Tooltip.SetDefault("A new journey begins");



        }

        public override void SetDefaults()
        {
       //     Item.consumable = true;
            Item.width = 38;
            Item.height = 42;
            Item.maxStack = 9999;
            Item.value = 0; // Makes the item worth 1 gold.
            Item.rare = ItemRarityID.White;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(100)
                .AddIngredient(ItemID.Chain, 10)

            .AddTile<ShrineTile>()
                .Register();

        }


    }
}


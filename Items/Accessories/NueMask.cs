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
using TenShadows.Tiles;

namespace TenShadows.Items.Accessories
{
    public class NueMask : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs
 
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Nue's Mask");
            Tooltip.SetDefault("Boost the effects while in 'Zone' state");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true;

        }
        private int timer;
        private int damage;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MP>().NueMaskOn = true;
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
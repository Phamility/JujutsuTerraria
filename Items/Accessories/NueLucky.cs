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

namespace JujutsuTerraria.Items.Accessories
{
    public class NueLucky : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Nue's Lucky Feathers");
            Tooltip.SetDefault("1 defense\nIncreases max health by 20\nIncreases max mana by 20 ");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 32;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
          //  Item.expert = true;

        }
   
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statDefense += 1;
            player.statLifeMax2 += 20;
            player.statManaMax2 += 20;





        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
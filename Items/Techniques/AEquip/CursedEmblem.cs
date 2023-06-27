using System;
using TenShadows.Buffs;
using TenShadows.Buffs;
using TenShadows.Buffs;
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
using TenShadows.Tiles;

using Terraria.ModLoader;
using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using rail;
using TenShadows.Ancients;

namespace TenShadows.Items.Techniques.AEquip
{
    public class CursedEmblem : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Jujutsu Emblem");
            Tooltip.SetDefault("15% increased cursed damage");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;

        }
 
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage<CursedDamage>() += 0.15f;


        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
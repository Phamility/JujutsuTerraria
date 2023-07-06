using System;
using JujutsuTerraria.Buffs;
using JujutsuTerraria.Buffs;
using JujutsuTerraria.Buffs;
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
using JujutsuTerraria.Tiles;

using Terraria.ModLoader;
using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using rail;
using JujutsuTerraria.Ancients;
using JujutsuTerraria.Items.Techniques.Blood;
using Terraria.Utilities;

namespace JujutsuTerraria.Items.Techniques.ARestrictions;
    public class RestrictionBoots : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Binding Vow: Speed");
            Tooltip.SetDefault("12% movement speed\nHowever, defense is reduced by 4");
        }

        public override void SetDefaults()
        {
        Item.width = 32;
        Item.height = 32;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        Item.prefix = 0;


    }
    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient<StandardBV>(1)
            .Register();
    }
    public override bool? PrefixChance(int pre, UnifiedRandom rand)
    {
        if (pre == -1)
        {
            return false;
        }
        return false;
    }
    public override bool CanEquipAccessory(Player player, int slot, bool modded)
    {

        if (modded){
            return true;
        }
        else
        {
            return false;
        } 
    }
    public override void UpdateAccessory(Player player, bool hideVisual)
        {
    //   player.moveSpeed *= 1.2f;
        player.maxRunSpeed *= 1.12f;
       player.accRunSpeed *= 1.12f;
    //    player.runAcceleration *= 1.12f; 
        player.statDefense -= 4;

        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }

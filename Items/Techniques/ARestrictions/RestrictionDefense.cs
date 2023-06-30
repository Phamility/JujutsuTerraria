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
using Terraria.Utilities;


namespace TenShadows.Items.Techniques.ARestrictions;
    public class RestrictionDefense : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Binding Vow: Defense");
            Tooltip.SetDefault("4 defense\nHowever, damage is reduced by 12%");
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
        player.statDefense += 4;
        player.GetDamage(DamageClass.Generic) -= 0.12f;


        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }

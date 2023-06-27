using System;
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
using Terraria.ModLoader;
using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;
using rail;

namespace TenShadows.Items.Techniques.AEquip
{
    public class HeavenlyCursed : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Heavenly Restriction | Cursed");
            Tooltip.SetDefault("Boost cursed, magic, and summon damage by 18%\nHowever, reduce max health by 100 and defense by 5");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.value = 10000;
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;

        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<HeavenlyCursedBuff>(), 2);




        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
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
using Terraria.Utilities;
using TenShadows.Ancients;

namespace TenShadows.Items.Techniques.ARestrictions
{
    public class HeavenlyPhysical : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            DisplayName.SetDefault("Physical Heavenly Restriction");
            Tooltip.SetDefault("8% cursed, melee and ranged damage\n4% movement speed\n2 defense\nHowever, you are unable to utilize cursed energy, mana, and minions\nCounts as a binding vow");
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.prefix = 0;


        }
        public override bool? PrefixChance(int pre, UnifiedRandom rand)
        {
            if (pre == -1)
            {
                return false;
            }
            return false;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int AddMore = 0;
            if (Item.favorited) { AddMore = 2; } else { AddMore = 0; }
            Player player = Main.LocalPlayer;
            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MPArmors>().MakiDamageNumber}% cursed, melee, and ranged damage") { OverrideColor = Color.White };
            tooltips.Insert(1 + AddMore, tooltip);

                TooltipLine Cock = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MPArmors>().MakiMoveNumber}% movement speed") { OverrideColor = Color.White };
                tooltips.Insert(2 + AddMore, Cock);
           
          
            
                TooltipLine Cock2 = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MPArmors>().MakiDefenseNumber} defense") { OverrideColor = Color.White };
                tooltips.Insert(3 + AddMore, Cock);


        }
        public override bool CanEquipAccessory(Player player, int slot, bool modded)
        {

            if (modded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(ModContent.BuffType<HeavenlyBuff>(), 2);




        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
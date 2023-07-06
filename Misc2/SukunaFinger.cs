using System; using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using JujutsuTerraria.NPCS;
using JujutsuTerraria.Items.Accessories;
using JujutsuTerraria.Items.Accessories.Eyes;

using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;

using IL.Terraria.GameContent.Personalities;
using On.Terraria.GameContent.Personalities;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using rail;
using JujutsuTerraria.Tiles;

namespace JujutsuTerraria.Misc2
{
    public class SukunaFinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Suspicious Looking Finger");
            Tooltip.SetDefault("Permanately increases damage by 1%\nOnly 20 fingers can be consumed per character");

            // This set is one that every boss bag should have.
            // It will create a glowing effect around the item when dropped in the world.
            // It will also let our boss bag drop dev armor..

        }

        public override void SetDefaults()
        {
            // Item.CloneDefaults(ItemID.LifeFruit);

            Item.maxStack = 99;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 45;
            Item.useTime = 45;
            Item.width = 22;
            Item.height = 42;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.NPCDeath13;

            Item.value = Item.sellPrice(gold: 5);

        }
        public override bool CanUseItem(Player player)
        {
            if (player.GetModPlayer<MP2>().FingersConsumed < 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public override bool? UseItem(Player player)
        {
            player.GetModPlayer<MP2>().FingersConsumed += 1;
            return true;
        }
    }
}

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
using static Terraria.ModLoader.PlayerDrawLayer;
using TenShadows.Projectiles;

namespace TenShadows.Items.Materials
{
    public class Horn : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Horn");
            Tooltip.SetDefault("Pointy!");

        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 23;
            Item.maxStack = 99;
            Item.value = 500; // Makes the item worth 1 gold.
            Item.rare = ItemRarityID.Orange;
        }




    }
}


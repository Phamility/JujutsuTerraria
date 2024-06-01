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
using Terraria.GameContent.Personalities;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.PlayerDrawLayer;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Tiles;

namespace JujutsuTerraria.Items.Materials
{
    public class BloodMat : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Converged Blood");
            // Tooltip.SetDefault("All that for a drop of blood.");

        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 22;
            Item.maxStack = 999;
            Item.value = 0; // Makes the item worth 1 gold.
            Item.rare = ItemRarityID.White;
        }
  
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile<ShrineTile>()
               .Register();
        }
        public override void OnCreated(ItemCreationContext context)
        {
            Player player = Main.LocalPlayer;
            player.statLife -= 20;
            if (player.statLife <= 0)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " used up too much blood!"), 20, 0);
            }
        }
 
   


    }
}


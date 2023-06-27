using System; using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using TenShadows.NPCS;
using TenShadows.Items.Accessories;
using TenShadows.Misc2;
using TenShadows.Items.Accessories.Eyes;

namespace TenShadows.Items.Materials
{
    public class NueBossBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag (Nue)");
            Tooltip.SetDefault("Right Click to open");

            // This set is one that every boss bag should have.
            // It will create a glowing effect around the item when dropped in the world.
            // It will also let our boss bag drop dev armor..
            ItemID.Sets.BossBag[Type] = true;
            ItemID.Sets.PreHardmodeLikeBossBag[Type] = true; // ..But this set ensures that dev armor will only be dropped on special world seeds, since that's the behavior of pre-hardmode boss bags.

        //    Item.ResearchUnlockCount = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 99;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Purple;
            Item.expert = true; // This makes sure that "Expert" displays in the tooltip and the item name color changes
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            // We have to replicate the expert drops from MinionBossBody here

            //     itemLoot.Add(ItemDropRule.NotScalingWithLuck(ModContent.ItemType<MinionBossMask>(), 7));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<NueMask>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<NueLucky>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<NueEye>(), 2));

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<NueFeather>(), 1, 30, 45));
            itemLoot.Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<Nue>()));
        }
    }
}

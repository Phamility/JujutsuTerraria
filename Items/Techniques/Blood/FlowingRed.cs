using System;
using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using Terraria.ID;
using TenShadows.Projectiles;
using TenShadows.Items.Materials;

namespace TenShadows.Items.Techniques.Blood
{
    public class FlowingRed : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flowing Red Scale");
            Tooltip.SetDefault("Gain 12% cursed damage for 2 minutes\nDuring this time, life regeneration is slightly decreased");
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            player.itemLocation -= new Vector2(0 * player.direction, 3);
        }

  
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {

            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"Costs 50 life per use") { OverrideColor = Color.Red };

            tooltips.Insert(1, tooltip);
        }
        public override bool? UseItem(Player player)
        {
            player.AddBuff(ModContent.BuffType<FlowingRedBuff>(), 2 * 60 * 60);
            return true;
         
        }
        public override void UseAnimation(Player player)
        {
            int losslife;
            losslife = 50;
            player.statLife -= losslife;
            if (player.statLife <= 0)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " used up too much blood!"), losslife, 0);
            }
        }

        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;


        public override void SetDefaults()
        {
            Item.UseSound = SoundID.NPCDeath19; // What sound should play when using the item
                                                //  Item.healLife = 25; // While we change the actual healing value in GetHealLife, Item.healLife still needs to be higher than 0 for the item to be considered a healing item
                                                //  Item.potion = false; // Makes it so this item applies potion sickness on use and allows it to be used with quick heal
                                                //   Item.damage = 9;
            Item.width = 24;
            Item.height = 26;
            Cost = 15;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.HoldUp; // How you use the item (swinging, holding out, etc.)
                                                   //  Item.knockBack = 3;
            Item.rare = ItemRarityID.Blue; // The color that the item's name will be in-game.
            Item.noMelee = true;
            //      item.shootSpeed = 4f;
            Item.autoReuse = false;
            // Item.shoot = NPCID.Bunny;

        }


    }
}





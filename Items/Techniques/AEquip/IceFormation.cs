using System;
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
using Terraria.GameContent.Personalities;
using Terraria.ModLoader.Utilities;
using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;

namespace JujutsuTerraria.Items.Techniques.AEquip
{
    public class IceFormation : ModItem
    {
        // To see how this config option was added, see ExampleModConfig.cs

        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            // DisplayName.SetDefault("Ice Formation");
            // Tooltip.SetDefault("Continuously form ice around you that will damage enemies");
        }

        public override void SetDefaults()
        {
            // Item.Prefix(-1);
            Item.width = 28;
            Item.height = 25;
            Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.shoot = ModContent.ProjectileType<IceProj>();
            //  Item.expert = true;

        }
        private int timer;
        private int damage;
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.HasBuff(ModContent.BuffType<JJKBuff>()) == false)
            {
                timer++;
                if (timer > 15)
                {
                    Vector2 position = player.position - new Vector2(Main.rand.Next(-150, 150), Main.rand.Next(-150, 150));

                    var entitySource = player.GetSource_FromAI();
                    int type = ModContent.ProjectileType<IceProj>();
                    if (Main.expertMode == true)
                    {
                        damage = 28;

                    }
                    else
                    {
                        damage = 11;

                    }



                    Projectile.NewProjectile(entitySource, position, -Vector2.UnitY, type, damage, 0f, player.whoAmI);

                    timer = 0;
                }

            }
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
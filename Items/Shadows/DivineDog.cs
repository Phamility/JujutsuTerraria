using System; using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.Audio;

using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Tiles;
using Terraria.Localization;

using Mono.Cecil;
using static Terraria.ModLoader.PlayerDrawLayer;
using System.Threading;
using System.Reflection;
using JujutsuTerraria.Ancients;


namespace JujutsuTerraria.Items.Shadows
{
    public class DivineDog : ModItem

    {

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Wolf Silhouette");
            // Tooltip.SetDefault("Summons a pair of divine dogs to fight by your side\nOnly one pair of divine dogs can be summoned at a time\nOccupies zero minion slots\nDivine dogs' damage increases with each boss defeated, however, it cannot be changed!");
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<CursedEnergy>(50)

                .AddTile<ShrineTile>()
                .Register();
        }
        public static int MYDAMAGE;

        public override void SetDefaults()
        {
            Cost = 8;
            Item.damage = MYDAMAGE;
            Item.width = 32;
            Item.height = 28;

            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.White;
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            // Item.crit = 0;
            Item.UseSound = SoundID.NPCHit6;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<DogSummon>();
        }
        public static int NewTimer;

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            damage *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
            Item.damage = MYDAMAGE;

        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            crit = 0;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine cock = tooltips.Find(x => x.Name == "CritChance" && x.Mod == "Terraria");
            TooltipLine cock2 = tooltips.Find(x => x.Name == "Damage" && x.Mod == "Terraria");

            // Get the vanilla damage tooltip
 
            tooltips.Remove(cock);
            tooltips.Remove(cock2);

            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"Costs {Cost - Reduction} cursed energy") { OverrideColor = Color.DodgerBlue };
            tooltips.Insert(1, tooltip);
            TooltipLine tooltip2 = new TooltipLine(Mod, "Ten Shadows: Cost", $"{MYDAMAGE} cursed damage") { OverrideColor = Color.White };
            if (Item.favorited) { tooltips.Insert(4, tooltip2); } else { tooltips.Insert(2, tooltip2); }
        }
        public override bool? UseItem(Player player)
        {

            bool once = false;
            for (int i = 0; i < Main.InventorySlotsTotal; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>() && once == false)
                {
                    if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= 1;
                        once = true;


                    }
                    else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= 6;
                        once = true;


                    }
                    else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
                    {
                        player.inventory[InventoryNumber].stack -= 8;
                        once = true;


                    }
                    else
                    {
                        player.inventory[InventoryNumber].stack -= 10;
                        once = true;

                    }
                }
            }
            return true;

        }
        public override void UpdateInventory(Player player)
        {
            Cost = 8;

            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
            {
                Reduction = Cost - 1;
            }
            else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
            {

                Reduction = 4;
            }
            else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
            {

                Reduction = 2;
            }
            else
            {
                Reduction = 0;
            }

            if (DogSummon.KYS == true && DogSummon2.KYS == true)
            {
                NewTimer++;
                if (NewTimer > 20)
                {
                    NewTimer = 0;
                    DogSummon.KYS = false;
                    DogSummon2.KYS = false;

                }
            }
        }
        public static int positive;



        public int InventoryNumber;
        public int Cost;
        public int Reduction = 0;
        public override bool CanUseItem(Player player) {
            bool Condition1;
            bool Condition2 = false;
            Cost = 8;
            if (player.HasBuff(ModContent.BuffType<SixEyesBuff>()) || player.HasBuff(ModContent.BuffType<JJKBuff>()))
            {
                Reduction = Cost - 1;
            }
            else if (player.HasBuff(ModContent.BuffType<TwinEyesBuff>()))
            {

                Reduction = 4;
            }
            else if (player.HasBuff(ModContent.BuffType<NueEyeBuff>()))
            {

                Reduction = 2;
            }
            else
            {
                Reduction = 0;
            }
            if (player.HasBuff<DivineDogBuff>() || player.HasBuff<HeavenlyBuff>())
            {

                Condition1 = false;
            }
            else
            {
                Condition1 = true;
            }

                for (int i = 0; i < 58; i++)
            {
                if (player.inventory[i].type == ModContent.ItemType<CursedEnergy>())
                {
                    if (player.inventory[i].stack >= Cost - Reduction)
                    {
                        InventoryNumber = i;
                        Condition2 = true;
                    }
                    else
                    {
                        Condition2 = false;
                    }
                }
    
            }

  if(Condition1 == true && ( Condition2 == true))
            {
                return true;
            }
            else
            {
                return false;
            }

            }
        
        private int timer;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            DogSummon.KYS = false;
            DogSummon2.KYS = false;
            timer = 0;
            Vector2 position2 = player.position + new Vector2(0, 0);

            player.AddBuff(ModContent.BuffType<DivineDogBuff>(), 2);

          Projectile.NewProjectileDirect(source, position, velocity, type, MYDAMAGE, knockback, player.whoAmI);
            type = ModContent.ProjectileType<DogSummon2>();

            Projectile.NewProjectileDirect(source, position2, velocity, type, MYDAMAGE, knockback, player.whoAmI);

            //Projectile.NewProjectile(Main.MouseWorld.X, player.position.Y - 800, 0f, 0f, ProjectileID.Bomb, damage, 4, player.whoAmI);


            return false;

        }

    }
}



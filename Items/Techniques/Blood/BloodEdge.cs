using System; using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Ancients;
using Terraria.Utilities;
using JujutsuTerraria.Tiles;
using Terraria.Audio;

namespace JujutsuTerraria.Items.Techniques.Blood
{
    public class BloodEdge : ModItem

    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blood Edge");
            // Tooltip.SetDefault("A lethal blade composed of its wielder's blood");
        }
   
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Cutlass);

            Item.damage = 75;
            Item.width = 40;
           // Item.mana = 8;
            Item.height = 48;
           // Item.healLife = -4;
       //     Item.useTime = 28;
         //   Item.useAnimation = 28;
           // Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.knockBack = 4;
            Item.rare = ItemRarityID.LightRed; // The color that the item's name will be in-game.
            Item.DamageType = ModContent.GetInstance<CursedDamage>();
          //  Item.UseSound = SoundID.Item102;

          //  Item.noMelee = true;
            //      item.shootSpeed = 4f;
        //    Item.shoot = ProjectileID.WoodenArrowFriendly;
      //     Item.useAmmo = AmmoID.Arrow;
            Item.autoReuse = true;
          //  Item.shoot = ModContent.ProjectileType<NueFriendlyFeather>();
          // COCK TEST 2
        }
        public int positive;

        public override void AddRecipes()
        {
            CreateRecipe()
                                              .AddIngredient(ItemID.Cutlass)

                .AddIngredient<CursedEnergy>(300)
                                .AddIngredient(ItemID.SoulofNight, 6)

                .AddTile<ShrineTile>()
                .Register();

        }

        public override void UseAnimation(Player player)
        {
            int losslife;
            losslife = Main.rand.Next(1, 4);
            player.statLife -= losslife;
            if (player.statLife <= 0)
            {
                player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " used up too much blood!"), losslife, 0);
            }
        }
        public override int ChoosePrefix(UnifiedRandom rand)
        {
            var prefixchooser = new WeightedRandom<int>();
            prefixchooser.Add(PrefixID.Broken, 2);
            prefixchooser.Add(PrefixID.Damaged, 2);
            prefixchooser.Add(PrefixID.Slow, 2);
            prefixchooser.Add(PrefixID.Annoying, 2);
            prefixchooser.Add(PrefixID.Quick, 2);
            prefixchooser.Add(PrefixID.Deadly, 2);
            prefixchooser.Add(PrefixID.Demonic, 2);
            prefixchooser.Add(PrefixID.Godly, 2);
            prefixchooser.Add(PrefixID.Ruthless, 2);
            prefixchooser.Add(PrefixID.Unpleasant, 2);
            prefixchooser.Add(PrefixID.Hurtful, 2);

            prefixchooser.Add(PrefixID.Sharp, 2);
            prefixchooser.Add(PrefixID.Legendary, 2);
            int choice = prefixchooser;
            if ((Item.damage > 0) && Item.maxStack == 1)
            {
                return choice;
            }
            return -1;
        }
        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            damage += ExampleDamagePlayer.ModPlayer(player).exampleDamageAdd;
            damage *= ExampleDamagePlayer.ModPlayer(player).exampleDamageMult;
        }
        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                SoundEngine.PlaySound(SoundID.NPCHit53, target.position);
                int pos;
                int dustType;
                target.life += damageDone;
                bool cock = false;
                damageDone *= player.GetModPlayer<MP>().ZoneDamage;
                if (damageDone > target.life )
                {
                    damageDone = target.life - Main.rand.Next(5, 30);
                    cock = true;
                }
                target.life -= damageDone;
                CombatText.clearAll();
                CombatText.clearAll();   

                for (int i = 0; i < 30; i++)
                {
                    if (Main.rand.Next(1, 3) == 2)
                    {
                        pos = 1;
                    }
                    else
                    {
                        pos = -1;
                    }
                    if (Main.rand.Next(1, 4) == 2)
                    {
                        dustType = ModContent.DustType<CustomDust>();
                    }
                    else
                    {
                        if (Main.rand.Next(1, 3) == 2)
                        {
                            dustType = ModContent.DustType<CustomDust2>();
                        }
                        else
                        {
                            dustType = ModContent.DustType<CustomDust3>();

                        }
                    }
                    var dust = Dust.NewDustDirect(target.position, target.width, target.height, dustType);

                    dust.velocity.X += Main.rand.NextFloat(.5f, 1f) * pos;
                    dust.velocity.Y += Main.rand.NextFloat(.5f, 1f) * pos;

                    dust.scale *= 1f + Main.rand.NextFloat(-0.05f, 0.05f);
                }
                player.AddBuff(ModContent.BuffType<ZoneBuff>(), 60 * player.GetModPlayer<MP>().ZoneDuration);

               if (damageDone >= 0) {
                    CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkRed, damageDone, true, false); }
            }
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {

            crit = player.GetModPlayer<MP>().ZoneChance;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // Get the vanilla damage tooltip
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
            if (tt != null)
            {
                // We want to grab the last word of the tooltip, which is the translated word for 'damage' (depending on what language the player is using)
                // So we split the string by whitespace, and grab the last word from the returned arrays to get the damage word, and the first to get the damage shown in the tooltip
                string[] splitText = tt.Text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                // Change the tooltip text
                tt.Text = damageValue + " cursed damage";
            }
            TooltipLine tooltip = new TooltipLine(Mod, "Ten Shadows: Cost", $"Costs 1-3 life per use") { OverrideColor = Color.Red };

            tooltips.Insert(1, tooltip);
            TooltipLine COCK = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
            tooltips.Remove(COCK);
            Player player = Main.LocalPlayer;
            TooltipLine BLACKFLASHCHANCE = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MP>().ZoneChance}% black flash chance") { OverrideColor = Color.White };
            if (Item.favorited == true)
            {
                tooltips.Insert(5, BLACKFLASHCHANCE);
            }
            else
            {
                tooltips.Insert(3, BLACKFLASHCHANCE);
            }
        }

    }
}



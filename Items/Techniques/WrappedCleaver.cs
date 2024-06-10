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
using JujutsuTerraria.Tiles;

using Terraria.ModLoader;
using JujutsuTerraria.Projectiles;
using JujutsuTerraria.Items.Materials;
using rail;
using JujutsuTerraria.Ancients;
using Steamworks;
using Terraria.Utilities;

namespace JujutsuTerraria.Items.Techniques
{
    public class WrappedCleaver : ModItem
    {
        public override void SetStaticDefaults()
        {
            // These wings use the same values as the solar wings
            // Fly time: 180 ticks = 3 seconds
            // Fly speed: 9
            // Acceleration multiplier: 2.5
            // DisplayName.SetDefault("Wrapped Cleaver");
            // Tooltip.SetDefault("When combined with Ratio, Wrapped Cleaver's black flash chance increases by 7%");
        }

        public override void SetDefaults()
        {
            Item.width = 42; // The item texture's width.
            Item.height = 34; // The item texture's height.
            Item.scale = 1.04f;
            Item.useStyle = ItemUseStyleID.Swing; // The useStyle of the Item.
            Item.useTime = 23; // The time span of using the weapon. Remember in terraria, 60 frames is a second.
            Item.useAnimation = 23; // The time span of the using animation of the weapon, suggest setting it the same as useTime.
            Item.autoReuse = true; // Whether the weapon can be used more than once automatically by holding the use button.

            Item.DamageType = ModContent.GetInstance<CursedDamage>();
            Item.damage = 26; // The damage your item deals.
            Item.knockBack = 5; // The force of knockback of the weapon. Maximum is 20
            Item.crit = 7; // The critical strike chance the weapon has. The player, by default, has a 4% critical strike chance.
            Item.value = Item.buyPrice(gold: 3); // The value of the weapon in copper coins.
            Item.rare = ItemRarityID.Green; // Give this item our custom rarity.
            Item.UseSound = SoundID.Item1; // The sound when the weapon is being used.
        }

        public int PersonalBlackFlashAdd;
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
                if (damageDone > target.life)
                {
                    damageDone = target.life - Main.rand.Next(5, 30);
                    cock = true;
                }
                target.life -= damageDone;
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

               if (damageDone >= 0) { CombatText.NewText(new Rectangle((int)target.position.X, (int)target.position.Y, target.width, target.height), Color.DarkRed, damageDone, true, false); }
            }
        }
        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {

        }
        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            if (player.HasBuff<CleaverBuff>() == true)
            {
                PersonalBlackFlashAdd = 7;
            }
            else
            {
                PersonalBlackFlashAdd = 0;

            }
            crit = player.GetModPlayer<MP>().ZoneChance + PersonalBlackFlashAdd;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;
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
            TooltipLine COCK = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
            tooltips.Remove(COCK);

            TooltipLine BLACKFLASHCHANCE = new TooltipLine(Mod, "Ten Shadows: Cost", $"{player.GetModPlayer<MP>().ZoneChance + PersonalBlackFlashAdd}% black flash chance") { OverrideColor = Color.White };
            if (Item.favorited == true)
            {
                tooltips.Insert(4, BLACKFLASHCHANCE);
            }
            else
            {
                tooltips.Insert(2, BLACKFLASHCHANCE);
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
            if((Item.damage > 0) && Item.maxStack == 1)
            {
                return choice;
            }
            return -1;
                }
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                // Emit dusts when the sword is swung
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
            }
        }

       

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

    }
}
using JujutsuTerraria.Buffs;
using JujutsuTerraria.Items.Materials;
using JujutsuTerraria.Items.Shadows;
using JujutsuTerraria.Items.Techniques.Domains;
using JujutsuTerraria.Items.Techniques.Tech;
using JujutsuTerraria.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
namespace JujutsuTerraria.Ancients
{
    // This class stores necessary player info for our custom damage class, such as damage multipliers, additions to knockback and crit, and our custom resource that governs the usage of the weapons of this damage class.
    public class ExampleDamagePlayer : ModPlayer
    {
        public static ExampleDamagePlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<ExampleDamagePlayer>();
        }

        // Vanilla only really has damage multipliers in code
        // And crit and knockback is usually just added to
        // As a modder, you could make separate variables for multipliers and simple addition bonuses
        public float exampleDamageAdd;
        public float exampleDamageMult = 1f;
        public float exampleKnockback;
        public int exampleCrit;

        public override void ResetEffects()
        {
            ResetVariables();
        }

        public override void UpdateDead()
        {
            ResetVariables();
        }

        private void ResetVariables()
        {
            exampleDamageAdd = 0f;
            exampleDamageMult = 1f;
            exampleKnockback = 0f;
            exampleCrit = 0;
        }

        public int InventoryNumber;

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
        

            if (KeyBindSystem.JujutsuTerraria3.JustPressed)
            {
                for (int i = 0; i < 58; i++)
                {

                    if (Player.inventory[i].type == ModContent.ItemType<RestlessGambler>())
                    {
                        var NT = Player.inventory[i].ModItem as RestlessGambler;
                        if (NT.CanUseItem(Player) == true)
                        {
                            NT.UseItem(Player);
                        }
                    }
                }
            }

            if (KeyBindSystem.JujutsuTerraria2.JustPressed)
            {
                for (int i = 0; i < 58; i++)
                {

                    if (Player.inventory[i].type == ModContent.ItemType<RoundDeer>())
                    {
                        var NT = Player.inventory[i].ModItem as RoundDeer;
                        if (NT.CanUseItem(Player) == true)
                        {
                            NT.UseItem(Player);
                            Player.statLife += 25;
                            CombatText.NewText(new Rectangle((int)Player.position.X, (int)Player.position.Y, Player.width, Player.height), Color.LimeGreen, 25, true, false);

                        }
                    }
                }
            }
            if (KeyBindSystem.JujutsuTerraria1.JustPressed)
            {
                for (int i = 0; i < 58; i++)
                {

                    if (Player.inventory[i].type == ModContent.ItemType<NobleTiger>())
                    {
                        var NT = Player.inventory[i].ModItem as NobleTiger;
                        if (NT.CanUseItem(Player) == true)
                        {
                            NT.UseItem(Player);
                        }
                    }
                }
            }

            if (KeyBindSystem.JujutsuTerraria.JustPressed)
            {
                for (int i = 0; i < 58; i++)
                {

                    if (Player.inventory[i].type == ModContent.ItemType<FlyingNue>())
                    {
                        var NT = Player.inventory[i].ModItem as FlyingNue;
                        if (NT.CanUseItem(Player) == true)
                        {
                            NT.UseItem(Player);
                        }
                    }
                }
            }







        }
    }
}
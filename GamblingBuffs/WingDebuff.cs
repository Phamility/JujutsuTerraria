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
namespace JujutsuTerraria.GamblingBuffs
{
    public class WingDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Failure's Wing Nullification");
            Description.SetDefault("Reduce the effects of wings incredibly");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
  
    }
    public class CritDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Failure's Critical Chance");
            Description.SetDefault("Decreases critical chance by 50%");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetCritChance(DamageClass.Generic) -= 50;

        }
    }
    public class DamageDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Failure's Damage");
            Description.SetDefault("Decreases damage by 40%");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) -= .40f;

        }
    }
    public class DefenseDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Failure's Damage Reduction");
            Description.SetDefault("Decreases defense by 40");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 40;

        }
    }
    public class HealthDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Failure's Life");
            Description.SetDefault("Decreases max health by 250!");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.statLifeMax2 -= 250;

        }
    }
    public class ManaCostsDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Failure's Mana Efficiency");
            Description.SetDefault("Increase mana costs by 50%");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.manaCost += .5f;

        }
    }

    public class SummonDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Failure's Summoning");
            Description.SetDefault("Decrease max number of minions by 3");
            Main.debuff[Type] = true;
            // Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = false; // The time remaining won't display on this buff
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.maxMinions -= 3;
        }
    }
}

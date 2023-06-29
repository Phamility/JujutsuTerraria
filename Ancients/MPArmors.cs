using System;
using TenShadows.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TenShadows.GamblingBuffs;
using TenShadows.Items.Shadows;
using TenShadows.Projectiles;
using Terraria.GameContent;
using Terraria.Localization;
using Terraria.Enums;

using Terraria;
using Terraria.ModLoader;
using System.Numerics;
using TenShadows.Items.Techniques;
using System.Drawing.Imaging;
using TenShadows.Items.Materials;
using TenShadows.Ancients;
using TenShadows.Armor;

namespace TenShadows.Ancients
{
    public class MPArmors : ModPlayer
    {
        public bool NobaraHeadOn;


        public override void ResetEffects()
        {
            NobaraHeadOn = false;
        }
    }
}

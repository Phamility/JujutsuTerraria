using Terraria.ModLoader;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Graphics;
using System;
using JujutsuTerraria.Buffs;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


using Terraria.Audio;

//using static Terraria.ModLoader.ModContent;

using JujutsuTerraria.Ancients;
using JujutsuTerraria.Items.Shadows;
using JujutsuTerraria.Items.Techniques;
using Terraria.GameContent;
using ReLogic.Content;
using System.Transactions;
using static System.Formats.Asn1.AsnWriter;
namespace JujutsuTerraria
{
	public class JujutsuTerraria : Mod
	{
        public override void Load()
        {
            // ...other Load stuff goes here

            if (Main.netMode != NetmodeID.Server)
            {
                Ref<Effect> screenRef = new Ref<Effect>(ModContent.Request<Effect>("JujutsuTerraria/ShockwaveEffect", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value); // The path to the compiled shader file.
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(screenRef, "Shockwave"), EffectPriority.VeryHigh);
                Filters.Scene["Shockwave"].Load();
            }
        }
    }
}
using Terraria;
using Terraria.ModLoader;
using System;
using TenShadows.Buffs;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using TenShadows.Items.Materials;
using TenShadows.Items.Shadows;
using Terraria.DataStructures;
using Terraria.ID;

using static Humanizer.In;
using static Terraria.ModLoader.PlayerDrawLayer;
using TenShadows.Ancients;

namespace TenShadows.Projectiles
{
    public class CustomDust3 : ModDust
    {
        public override void OnSpawn(Dust dust)
        {
            dust.velocity *= 0.8f; // Multiply the dust's start velocity by 0.4, slowing it down
            dust.noGravity = true; // Makes the dust have no gravity.
            dust.noLight = true; // Makes the dust emit no light.
            dust.scale *= 1.5f; // Multiplies the dust's initial scale by 1.5.
        }

        public override bool Update(Dust dust)
        { // Calls every frame the dust is active
            dust.position += dust.velocity;
            dust.rotation += dust.velocity.X * 0.15f;
            dust.scale *= 0.975f;

            float light = 0.35f * dust.scale;

            Lighting.AddLight(dust.position, Color.DarkRed.ToVector3() * .5f);

            if (dust.scale < 0.5f)
            {
                dust.active = false;
            }

            return false; // Return false to prevent vanilla behavior.
        }
    }
}
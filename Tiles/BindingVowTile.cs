using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Microsoft.Xna.Framework;

using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using JujutsuTerraria.Items.Shadows;
using System; using JujutsuTerraria.Buffs;

namespace JujutsuTerraria.Tiles
{
    public class BindingVowTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;


            // Placement

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(0, 2);

            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.addTile(Type);

            //  AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

            // Etc
            HitSound = SoundID.Dig;
            DustType = DustID.Stone;
            AddMapEntry(new Color(163, 162, 162), Language.GetText("Binding Vow"));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            AmPlaced = false;
            //Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, ModContent.ItemType<BindingVow>());
           
        }

       


        public static bool AmPlaced = false;
        public override void PlaceInWorld(int i, int j, Item item)
        {
            AmPlaced = true;
            Player player = Main.LocalPlayer;
            if (player.HasBuff<BindingVowBuff>() == false)
            {
                player.AddBuff(ModContent.BuffType<BindingVowDebuff>(), 6);

            }
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            AmPlaced = true;

            Player player = Main.LocalPlayer;
            Vector2 tileCenter = new Point16(i, j).ToWorldCoordinates();


            const float range = 125 * 16;  // 20 tiles
            if (player.DistanceSQ(tileCenter) <= range * range)
            {
                player.AddBuff(ModContent.BuffType<BindingVowBuff>(), 6);
            }
            else
            {
                if (player.HasBuff<BindingVowBuff>() == false)
                {
                    player.AddBuff(ModContent.BuffType<BindingVowDebuff>(), 6);
                }
            }
        }

    }
}
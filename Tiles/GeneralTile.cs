using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Microsoft.Xna.Framework;

using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using TenShadows.Items.Shadows;
using TenShadows.Buffs;

namespace TenShadows.Tiles
{
    public class GeneralTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            // Properties
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true;


            // Placement
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 18, 18 };
            TileObjectData.addTile(Type);
            AnimationFrameHeight = 54;

            //  AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);

            // Etc
            DustType = DustID.Wraith;
            AddMapEntry(new Color(255, 105, 73), Language.GetText("Wheel Silhouette"));
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 48, ModContent.ItemType<General>());
        }


        public override void AnimateTile(ref int frame, ref int frameCounter)
        {

            // Spend 9 ticks on each of 6 frames, looping
            frameCounter++;
            if (frameCounter >= 9)
            {
                frameCounter = 0;
                if (++frame >= 4)
                {
                    frame = 0;
                }
            }

        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            Player player = Main.LocalPlayer;
            Vector2 tileCenter = new Point16(i, j).ToWorldCoordinates();
            const float range = 14 * 16;  // 20 tiles
            if (player.DistanceSQ(tileCenter) <= range * range)
                player.AddBuff(ModContent.BuffType<GeneralBuff>(), 6);
        }

    }
}